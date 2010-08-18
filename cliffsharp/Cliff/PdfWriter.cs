/* ------------------------------------------------------------------------- */
/*
 *  PdfWriter.cs
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
 *  Last-modified: Mon 05 Apr 2010 20:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using Container = System.Collections.Generic;

namespace Cliff {
    namespace PDF {
        /* ----------------------------------------------------------------- */
        /*
         *  IWritable
         *  
         *  ユーザは，各 PDF オブジェクトを記述するためのクラスを必要に
         *  応じて定義し，そのクラスのインスタンスを Pdf.Writer に渡す
         *  形式で新たな PDF を作成する．
         *  
         *  ユーザが新たな PDF オブジェクト出力クラスを作成する際には，
         *  この IWritable インターフェースを継承し，以下の Write メソッド
         *  を実装する必要がある．
         *  
         */
        /* ----------------------------------------------------------------- */
        public interface IWritable {
            void Write(System.IO.FileStream output, Cliff.PDF.Writer manager);
        }
        
        /* ----------------------------------------------------------------- */
        //  Writer
        /* ----------------------------------------------------------------- */
        public class Writer : IDisposable {
            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public Writer(double version = Writer.LatestVersion) {
                this.version_ = version;
                this.Init();
            }
            
            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public Writer(System.String path, double version = Writer.LatestVersion) {
                this.version_ = version;
                this.Init();
                this.Open(path);
            }

            /* ------------------------------------------------------------- */
            //  Destructor
            /* ------------------------------------------------------------- */
            ~Writer() {
                this.Dispose();
            }

            /* ------------------------------------------------------------- */
            //  Open
            /* ------------------------------------------------------------- */
            public void Open(System.String path) {
                this.path_ = path;
                this.output_ = new System.IO.FileStream(this.path_, System.IO.FileMode.Create);
                this.WriteHeader(this.output_);
            }

            /* ------------------------------------------------------------- */
            //  Dispose
            /* ------------------------------------------------------------- */
            public void Dispose() {
                if (this.output_ != null) {
                    this.WriteFooter(this.output_);
                    this.output_.Dispose();
                    this.output_ = null;
                }
            }

            /* ------------------------------------------------------------- */
            /*
             *  NewIndex
             *  
             *  Pdf.Writer クラスは，各 PDF オブジェクトのインデックス番号
             *  と出力位置を管理している．ユーザが新たな PDF オブジェクトを
             *  出力する際には，まず始めに NewIndex メソッドを呼び，出力
             *  するためのインデックス番号を取得する必要がある．
             *  
             *  NewIndex メソッドは，主にユーザが作成する IWritable を
             *  継承したクラスの Write メソッドの中で使用される．
             */
            /* ------------------------------------------------------------- */
            public uint NewIndex() {
                index_++;
                this.xref_.Add(index_, this.output_.Position);
                return index_;
            }

            /* ------------------------------------------------------------- */
            /*
             *  Write
             *  
             *  Write メソッドは，引数に指定されたバイト配列をそのまま
             *  出力ファイルに書き込む．指定されるバイト配列は，以下の
             *  書式に従っている必要がある．
             *  
             *  N 0 obj
             *   ... （各オブジェクトで必要なデータの記述）
             *  endobj
             *  
             *  ※N は NewIndex メソッドで取得したインデックス番号．
             */
            /* ------------------------------------------------------------- */
            public void Write(byte[] obj) {
                this.output_.Write(obj, 0, obj.Length);
                this.output_.WriteByte((byte)'\n');
            }

            /* ------------------------------------------------------------- */
            /*
             *  Write
             *  
             *  引数に指定された PDF オブジェクトクラスの Write メソッド
             *  を実行する．指定される PDF オブジェクトクラスは，それぞれ
             *  の目的に応じて，適切に Write メソッドを実装する必要がある．
             */
            /* ------------------------------------------------------------- */
            public void Write<Type>(Type obj)
                where Type : IWritable {
                obj.Write(this.output_, this);
            }

            /* ------------------------------------------------------------- */
            //  accessor
            /* ------------------------------------------------------------- */
            public double Version {
                get { return this.version_; }
                set { this.version_ = value; }
            }

            public uint Index {
                get { return this.index_; }
            }

            public Container.Dictionary<System.String, System.String> Trailer {
                get { return this.trailer_; }
                set { this.trailer_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  Init (private)
            /* ------------------------------------------------------------- */
            private void Init() {
                this.path_ = null;
                this.output_ = null;
                this.index_ = 0;
                this.xref_ = new Container.SortedDictionary<uint, long>();
                this.trailer_ = new Container.Dictionary<string, string>();
            }

            /* ------------------------------------------------------------- */
            //  WriteHeader (private)
            /* ------------------------------------------------------------- */
            private void WriteHeader(System.IO.FileStream output) {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                writer.WriteLine("%PDF-{0:F1}", this.version_);
                writer.Flush();
            }

            /* ------------------------------------------------------------- */
            //  WriteFooter (private)
            /* ------------------------------------------------------------- */
            private void WriteFooter(System.IO.FileStream output) {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                long startxref = output.Position;

                // 1. write xref
                writer.WriteLine("xref");
                writer.WriteLine("0 {0}", this.xref_.Count + 1);
                writer.WriteLine("0000000000 65535 f ");
                foreach (Container.KeyValuePair<uint, long> elem in this.xref_) {
                    writer.WriteLine("{0:D10} 00000 n ", elem.Value);
                }

                // 2. write trailer
                writer.WriteLine("trailer");
                writer.WriteLine("<<");
                foreach (Container.KeyValuePair<System.String, System.String> elem in this.trailer_) {
                    writer.WriteLine("/{0} {1}", elem.Key, elem.Value);
                }
                writer.WriteLine(">>");
                
                // 3. write startxref
                writer.WriteLine("startxref");
                writer.WriteLine("{0}", startxref);
                writer.WriteLine("%%EOF");

                writer.Flush();
            }

            /* ------------------------------------------------------------- */
            //  constant variables
            /* ------------------------------------------------------------- */
            public const double LatestVersion = 1.7;

            /* ------------------------------------------------------------- */
            //  member variables
            /* ------------------------------------------------------------- */
            private System.String path_;
            private System.IO.FileStream output_;
            private double version_;
            private uint index_;
            private Container.SortedDictionary<uint, long> xref_;
            private Container.Dictionary<System.String, System.String> trailer_;
        }
    } // namespace PDF
} // namespace Cliff
