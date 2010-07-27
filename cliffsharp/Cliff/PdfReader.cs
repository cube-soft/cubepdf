/* ------------------------------------------------------------------------- */
/*
 *  PdfReader.cs
 *
 *  Copyright (c) 2009 - 2010, clown. All rights reserved.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see < http://www.gnu.org/licenses/ >.
 *
 *  Last-modified: Tue 06 Apr 2010 05:38:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using Container = System.Collections.Generic;

namespace Cliff {
    namespace PDF {
        /* ----------------------------------------------------------------- */
        /*
         *  ObjectPosition
         * 
         *  入力 PDF ファイルの各オブジェクトの，（開始位置, バイト数）の
         *  情報を保持するためのクラス．Pdf.Reader の xref (cross
         *  reference table) の Value の型として使用される．
         */
        /* ----------------------------------------------------------------- */
        public class ObjectPosition {
            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public ObjectPosition() {
                this.offset_ = 0;
                this.length_ = 0;
            }

            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public ObjectPosition(long off, long len) {
                this.offset_ = off;
                this.length_ = len;
            }

            /* ------------------------------------------------------------- */
            //  accessor
            /* ------------------------------------------------------------- */
            public long Offset {
                get { return this.offset_; }
                set { this.offset_ = value; }
            }

            public long Length {
                get { return this.length_; }
                set { this.length_ = value; }
            }

            private long offset_;
            private long length_;
        };
        
        /* ----------------------------------------------------------------- */
        /*
         *  Reader
         *
         *  引数に指定された PDF ファイルを解析し，PDF ファイルを構成する
         *  各オブジェクトにアクセスできるようにするためのクラス．
         *  
         *  Reader クラスは，xref (cross reference) table，および trailer
         *  の情報を解析するのみで，各オブジェクトの内容はバイト配列
         *  で生のデータのまま返される．各オブジェクトの内容を解析する
         *  場合は，それぞれのオブジェクトにあった解析用クラスを別途用意
         *  する必要がある．
         */
        /* ----------------------------------------------------------------- */
        public class Reader : IDisposable {
            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public Reader() {
                this.Init();
            }

            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public Reader(System.String path) {
                this.Init();
                this.Open(path);
            }

            /* ------------------------------------------------------------- */
            //  Destructor
            /* ------------------------------------------------------------- */
            ~Reader() {
                this.Dispose();
            }

            /* ------------------------------------------------------------- */
            /*
             *  Open
             *  
             *  引数に指定された PDF ファイルを開き，xref (cross reference)
             *  table，および trailer の情報を解析する．
             */
            /* ------------------------------------------------------------- */
            public void Open(System.String path) {
                this.path_ = path;
                if (!System.IO.File.Exists(this.path_)) {
                    throw new System.IO.FileNotFoundException(this.path_ + " is not found", this.path_);
                }
                this.input_ = new System.IO.FileStream(this.path_, System.IO.FileMode.Open);
                if (!this.IsValid()) throw new System.Exception("invalid PDF file format");

                this.ReadVersion(this.input_);
                this.ReadInfo(this.input_);
            }

            /* ------------------------------------------------------------- */
            //  Dispose
            /* ------------------------------------------------------------- */
            public void Dispose() {
                if (this.input_ != null) {
                    this.input_.Dispose();
                    this.input_ = null;
                }
            }

            /* ------------------------------------------------------------- */
            /*
             *  IsValid
             *  
             *  PDF ファイルのフォーマットが正しいかどうかを判別する．
             *  PDF ファイルは，%PDF-1.X (1.X はバージョン) の行で始まり，
             *  %%EOF の行で終わる．
             */
            /* ------------------------------------------------------------- */
            public bool IsValid() {
                // %PDF-1.X のチェック
                byte[] chk = new byte[5];
                this.input_.Seek(0, System.IO.SeekOrigin.Begin);
                this.input_.Read(chk, 0, 5);
                if (!(chk[0] == (byte)'%' && chk[1] == (byte)'P' && chk[2] == (byte)'D' && chk[3] == (byte)'F' && chk[4] == (byte)'-')) return false;

                // %%EOF のチェック
                byte [] feof = new byte[2];
                this.input_.Seek(-2, System.IO.SeekOrigin.End);
                this.input_.Read(feof, 0, 2);

                long n = (feof[0] == 0x0d && feof[1] == 0x0a) ? 7 : ((feof[1] == 0x0a) ? 6 : 5);
                this.input_.Seek(-n, System.IO.SeekOrigin.End);
                this.input_.Read(chk, 0, (int)5);
                if (!(chk[0] == (byte)'%' && chk[1] == (byte)'%' && chk[2] == (byte)'E' && chk[3] == (byte)'O' && chk[4] == (byte)'F')) return false;
                
                return true;
            }

            /* ------------------------------------------------------------- */
            /*
             *  Count
             *  
             *  xref (cross reference) table の要素数を返す．PDF では，
             *  インデックス番号で各オブジェクトが管理されているが，
             *  このインデックス番号は 1 から始まる．
             *  
             *  したがって，有効な PDF オブジェクトのインデックス番号は
             *  [1,Count] となるので注意する必要がある．
             */
            /* ------------------------------------------------------------- */
            public int Count() {
                return this.XrefTable.Count;
            }

            /* ------------------------------------------------------------- */
            /*
             *  GetObject
             * 
             *  index に対応するオブジェクトを返す．オブジェクトは，
             *  バイト配列の形で生のデータのまま返される．オブジェクトの
             *  内容を解析する際には，それぞれのオブジェクトに応じた
             *  解析クラスを用意する必要がある．
             */
            /* ------------------------------------------------------------- */
            public byte[] GetObject(uint index) {
                if (!this.xref_.ContainsKey(index)) return null;
                ObjectPosition elem = this.xref_[index];
                byte [] dest = new byte[elem.Length];
                this.input_.Seek(elem.Offset, System.IO.SeekOrigin.Begin);
                this.input_.Read(dest, 0, (int)elem.Length);
                return dest;
            }

            /* ------------------------------------------------------------- */
            //  accessor
            /* ------------------------------------------------------------- */
            public double Version {
                get { return this.version_; }
            }

            public Container.SortedDictionary<uint, ObjectPosition> XrefTable {
                get { return this.xref_; }
            }

            public Container.Dictionary<System.String, System.String> Trailer {
                get { return this.trailer_; }
            }

            /* ------------------------------------------------------------- */
            //  Init (private)
            /* ------------------------------------------------------------- */
            private void Init() {
                this.path_ = null;
                this.input_ = null;
                this.xref_ = new Container.SortedDictionary<uint, ObjectPosition>();
                this.trailer_ = null;
            }

            /* ------------------------------------------------------------- */
            /*
             *  ReadLine (private)
             *  
             *  指定された StreamReader から次の一行を読み込む．
             *  ただし，コメント行（% で始まる行）は読み飛ばす．
             */
            /* ------------------------------------------------------------- */
            private System.String ReadLine(System.IO.StreamReader reader) {
                System.String line = null;
                while ((line = reader.ReadLine()) != null) {
                    if (line.Length > 0) {
                        if (line[0] != 0x0d && line[0] != 0x0a && line[0] != '%') return line.Trim();
                    }
                }
                return null;
            }

            /* ------------------------------------------------------------- */
            //  ReadVersion (private)
            /* ------------------------------------------------------------- */
            private void ReadVersion(System.IO.Stream input) {
                byte[] version = new byte[3];
                this.input_.Seek(5, System.IO.SeekOrigin.Begin);
                this.input_.Read(version, 0, 3);
                this.version_ = System.Convert.ToDouble(System.Text.Encoding.ASCII.GetString(version));
            }

            /* ------------------------------------------------------------- */
            /*
             *  ReadInfo (private)
             *  
             *  xref (cross reference) table，および trailer を読み込む．
             */
            /* ------------------------------------------------------------- */
            private void ReadInfo(System.IO.Stream input) {
                Container.List<long> pos = new Container.List<long>();
                
                long startxref = this.ReadStartXref(input, 128);
                input.Seek(startxref, System.IO.SeekOrigin.Begin);
                System.IO.StreamReader reader = new System.IO.StreamReader(input);

                // 1. read xref (cross reference) table.
                System.String buffer = null;
                buffer = this.ReadLine(reader);
                if (buffer == null || buffer != "xref") throw new System.Exception("cannot find xref table");
                buffer = this.ReadLine(reader);
                if (buffer == null) throw new System.Exception("cannot find xref table");

                uint n = System.Convert.ToUInt32(buffer.Substring(buffer.IndexOf(' ') + 1));
                Container.SortedDictionary<long, uint> map = this.MakeXrefTable(reader, n);
                
                uint index = 0;
                long first = 0;
                foreach (Container.KeyValuePair<long, uint> elem in map) {
                    if (first > 0) {
                        ObjectPosition x = new ObjectPosition(first, elem.Key - first);
                        this.xref_.Add(index, x);
                    }
                    index = elem.Value;
                    first = elem.Key;
                }
                ObjectPosition last = new ObjectPosition(first, startxref - first);
                this.xref_.Add(index, last);

                // 2. read trailer.
                System.String trailer = "";
                bool target = false;
                while ((buffer = this.ReadLine(reader)) != null) {
                    if (buffer == "startxref") break;
                    if (buffer == "trailer") target = true;
                    else if (target) trailer += buffer + " ";
                }

                if (trailer.Length > 0) {
                    this.trailer_ = Utility.ParseDictionary(trailer);
                }
            }

            /* ------------------------------------------------------------- */
            //  ReadStartXref (private)
            /* ------------------------------------------------------------- */
            private long ReadStartXref(System.IO.Stream input, uint bytes) {
                long dest = -1;
                input.Seek(-bytes, System.IO.SeekOrigin.End);
                System.IO.StreamReader reader = new System.IO.StreamReader(input);
                System.String line = null;
                bool target = false;
                while ((line = this.ReadLine(reader)) != null) {
                    if (line == "startxref") target = true;
                    else if (target) {
                        dest = System.Convert.ToInt64(line);
                        break;
                    }
                }
                return (dest > 0) ? dest : this.ReadStartXref(input, bytes * 2);
            }

            /* ------------------------------------------------------------- */
            //  MakeXrefTable (private)
            /* ------------------------------------------------------------- */
            private Container.SortedDictionary<long, uint> MakeXrefTable(System.IO.StreamReader reader, uint n) {
                Container.SortedDictionary<long, uint> dest = new Container.SortedDictionary<long, uint>();
                try {
                    for (uint i = 0; i < n; i++) {
                        System.String buffer = this.ReadLine(reader);
                        System.String [] token = buffer.Split();
                        if (token[2] == "n") dest.Add(System.Convert.ToInt64(token[0]), i);
                    }
                }
                catch {
                    throw new System.Exception("invalid xref table");
                }

                return dest;
            }

            /* ------------------------------------------------------------- */
            //  member variables
            /* ------------------------------------------------------------- */
            private System.String path_;
            private System.IO.FileStream input_;
            private double version_;
            private Container.SortedDictionary<uint, ObjectPosition> xref_;
            private Container.Dictionary<System.String, System.String> trailer_;
        };
    } // namespace PDF
} // namespace Cliff
