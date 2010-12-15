/* ------------------------------------------------------------------------- */
/*
 *  PdfObjects.cs
 *
 *  Copyright (c) 2010 CubeSoft Inc. All rights reserved.
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
 *  Last-modified: Sat 18 Jul 2010 00:21:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Text;
using System.Windows.Forms;
using Container = System.Collections.Generic;
using PDF = Cliff.PDF;
using iTextPDF = iTextSharp.text.pdf;
using System.IO;


namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    /// Catalog
    /// 
    /// <summary>
    /// PDF の Catalog オブジェクトを解析，編集するためのクラス．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class Catalog : Cliff.PDF.IWritable {
    public Catalog() { }
        /* ----------------------------------------------------------------- */
        /// constructor
        /* ----------------------------------------------------------------- */
        public Catalog(byte[] src) {
            this.Parse(src);
        }
        
        /* ----------------------------------------------------------------- */
        /// Parse
        /* ----------------------------------------------------------------- */
        public void Parse(byte[] src) {
            int pos = 0;
            while (src[pos] != (byte)'<') {
                pos++;
                if (pos >= src.Length) throw new System.Exception("invalid format");
            }
            
            System.String tmp = System.Text.Encoding.ASCII.GetString(src, pos, src.Length - pos);
            this.elements_ = Cliff.PDF.Utility.ParseDictionary(tmp);
        }
        
        /* ----------------------------------------------------------------- */
        /// Write
        /* ----------------------------------------------------------------- */
        public void Write(System.IO.FileStream output, Cliff.PDF.Writer manager) {
            uint index = manager.NewIndex();
            var writer = new System.IO.StreamWriter(output);
            
            writer.WriteLine("{0} 0 obj", index);
            writer.WriteLine("<<");
            foreach (var elem in this.elements_) {
                if (elem.Key == "Metadata") continue;
                writer.WriteLine("/{0} {1}", elem.Key.Trim(), elem.Value.Trim());
            }
            writer.WriteLine(">>");
            writer.WriteLine("endobj");
            writer.Flush();
        }
        
        private Container.Dictionary<System.String, System.String> elements_;
    }
    
    /* --------------------------------------------------------------------- */
    ///
    /// Information
    ///
    /// <summary>
    /// PDF の Info オブジェクトを解析，編集するためのクラス．
    /// Windows の PDF ファイルのプロパティ欄で，各種情報が文字化け
    /// しないようにするための処理．Windows は，プロパティ欄の情報は
    /// UTF-16 で記述しないと文字化けしてしまうので，各文字列を
    /// UTF-16 にエンコードした後に出力する．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class Information : Cliff.PDF.IWritable {
        /* ----------------------------------------------------------------- */
        /// constructor
        /* ----------------------------------------------------------------- */
        public Information() {
            this.MemberInitialize();
        }
        
        /* ----------------------------------------------------------------- */
        /// constructor
        /* ----------------------------------------------------------------- */
        public Information(byte[] src) {
            this.MemberInitialize();
            this.Parse(src);
        }
        
        /* ----------------------------------------------------------------- */
        /// Parse
        /* ----------------------------------------------------------------- */
        public void Parse(byte[] src) {
            int pos = 0;
            while (src[pos] != (byte)'<') {
                pos++;
                if (pos >= src.Length) throw new System.Exception("invalid format");
            }
            
            var tmp = System.Text.Encoding.ASCII.GetString(src, pos, src.Length - pos);
            this.elements_ = Cliff.PDF.Utility.ParseDictionary(tmp);
        }
        
        /* ----------------------------------------------------------------- */
        /// Write
        /* ----------------------------------------------------------------- */
        public void Write(System.IO.FileStream output, Cliff.PDF.Writer manager) {
            uint index = manager.NewIndex();
            var writer = new System.IO.StreamWriter(output);
            
            writer.WriteLine("{0} 0 obj", index);
            writer.WriteLine("<<");
            try {
                foreach (var elem in this.elements_) {
                    if (elem.Key == "Title") continue;
                    if (elem.Key == "Author") continue;
                    if (elem.Key == "Subject") continue;
                    if (elem.Key == "Keywords") continue;
                    else if (elem.Key == "Producer" && this.producer_ != "") writer.WriteLine("/{0} ({1})", elem.Key, this.producer_);
                    else if (elem.Key == "Creator" && this.creator_ != "") writer.WriteLine("/{0} ({1})", elem.Key, this.creator_);
                    else writer.WriteLine("/{0} {1}", elem.Key, elem.Value.Trim());
                }
                if (this.title_ != null) this.Write(writer, "Title", this.title_);
                if (this.subject_ != null) this.Write(writer, "Subject", this.subject_);
                if (this.author_ != null) this.Write(writer, "Author", this.author_);
                if (this.keyword_ != null) this.Write(writer, "Keywords", this.keyword_);
            }
            finally {
                writer.WriteLine(">>");
                writer.WriteLine("endobj");
                writer.Flush();
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// Write (private)
        /* ----------------------------------------------------------------- */
        private void Write(System.IO.StreamWriter writer, System.String key, byte[] value) {
            writer.Write("/{0} (", key);
            writer.Flush();
            writer.BaseStream.WriteByte(0xfe);
            writer.BaseStream.WriteByte(0xff);
            foreach (var c in value) {
                if (c == 0x28 || c == 0x29 || c == 0x5c) writer.BaseStream.WriteByte(0x5c);
                writer.BaseStream.WriteByte(c);
            }
            writer.BaseStream.Flush();
            writer.WriteLine(")");
        }
        
        /* ----------------------------------------------------------------- */
        /// MemberInitialize (private)
        /* ----------------------------------------------------------------- */
        private void MemberInitialize() {
            this.elements_ = null;
            this.title_ = null;
            this.subject_ = null;
            this.author_ = null;
            this.keyword_ = null;
            this.producer_ = copyright;
            this.creator_ = copyright;
        }
        
        /* ----------------------------------------------------------------- */
        /// Title
        /* ----------------------------------------------------------------- */
        public byte[] Title {
            get { return this.title_; }
            set { this.title_ = value; }
        }
        
        /* ----------------------------------------------------------------- */
        /// Subject
        /* ----------------------------------------------------------------- */
        public byte[] Subject {
            get { return this.subject_; }
            set { this.subject_ = value; }
        }
        
        /* ----------------------------------------------------------------- */
        /// Author
        /* ----------------------------------------------------------------- */
        public byte[] Author {
            get { return this.author_; }
            set { this.author_ = value; }
        }
        
        /* ----------------------------------------------------------------- */
        /// Keyword
        /* ----------------------------------------------------------------- */
        public byte[] Keyword {
            get { return this.keyword_; }
            set { this.keyword_ = value; }
        }
        
        /* ----------------------------------------------------------------- */
        /// Producer
        /* ----------------------------------------------------------------- */
        public string Producer {
            get { return this.producer_; }
            set { this.producer_ = value; }
        }
        
        Container.Dictionary<string, string> elements_;
        private byte[] title_;
        private byte[] subject_;
        private byte[] author_;
        private byte[] keyword_;
        private string producer_;
        private string creator_;
        private const string copyright = "CubePDF";
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MainForm
    /// 
    /// <summary>
    /// Ghostscript により生成された PDF ファイルへの修正部分のみを抽出．
    /// </summary>
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form {
        /* ----------------------------------------------------------------- */
        ///
        /// ModifyResult
        ///
        /// <summary>
        /// Ghostscriptで生成されたPDFファイルに対して，Ghostscriptでは
        /// 設定できない部分を修正する．
        ///
        /// TODO: Ghostscript が カタログオブジェクトは ID=1,
        /// 文書プロパティ用のオブジェクトは ID=2 で生成するため
        /// その ID で判別しているが，PDF のフォーマットでは必ずしも上記
        /// ID に設定される保証はない．別の部分で判別するように修正する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ModifyResult(string path)
        {
            
            if (!System.IO.File.Exists(path) ||
                FILE_TYPES[FileTypeComboBox.SelectedIndex] != Properties.Settings.Default.FILETYPE_PDF) return;

            var tmp = Cliff.Path.GetTempPath() + "TempOutput.pdf";
            if (System.IO.File.Exists(tmp)) System.IO.File.Delete(tmp);
            System.IO.File.Move(path, tmp);
            iTextPDF.PdfReader reader = null;
            try
            {
                reader = new iTextPDF.PdfReader(tmp);

                var info = reader.Info;
                info["Title"] = (TitleTextBox.TextLength > 0) ? TitleTextBox.Text : "";
                info["Author"] = (AuthorTextBox.TextLength > 0) ? AuthorTextBox.Text : "";
                info["Subject"] = (SubTitleTextBox.TextLength > 0) ? SubTitleTextBox.Text : "";
                info["Keywords"] = (KeywordTextBox.TextLength > 0) ? KeywordTextBox.Text : "";
                info["Creator"] = "CubePDF";
                info["Producer"] = "CubePDF";

               
                using (var os = new BufferedStream(new FileStream(path, FileMode.Create)))
                {
                    var writer = new iTextPDF.PdfStamper(reader, os, parsePDFVersion(VERSIONS[VersionComboBox.SelectedIndex]));
                    
                    //パスワードを設定する方法．User/Owner で設定されてない箇所は null
                    string user = (UserPasswordCheckBox.Enabled && UserPasswordTextBox.Text.Length > 0) ? UserPasswordTextBox.Text : null;
                    string owner = (OwnerPasswordCheckBox.Enabled && OwnerPasswordTextBox.Text.Length > 0) ? OwnerPasswordTextBox.Text : null;
                    int permission = 0;
                    if (PrintEnableCheckBox.Checked) permission |= iTextPDF.PdfWriter.AllowPrinting;
                    if (CopyEnableCheckBox.Checked) permission |= iTextPDF.PdfWriter.AllowCopy;

                    if (InputFormEnableCheckBox.Checked) {
                        permission |= iTextPDF.PdfWriter.AllowFillIn;
                        permission |= iTextPDF.PdfWriter.AllowModifyAnnotations;
                    }

                    if (PageInsertEtcEnableCheckBox.Checked) {
                        permission |= iTextPDF.PdfWriter.AllowModifyContents;
                        permission |= iTextPDF.PdfWriter.AllowScreenReaders;
                    }
                    writer.SetEncryption(iTextPDF.PdfWriter.STANDARD_ENCRYPTION_128, user, owner, permission);
                    writer.MoreInfo = info;
                    writer.Close();
                }
                System.IO.File.Delete(tmp);
            }
            finally
            {
                if (reader != null)
                  reader.Close();
            }
        }

        /// <summary>
        /// ModifyResultの補助関数
        /// 1.7 => PdfWriter.VERSION_1_7のように変換する
        /// (できればModifyResultの関数内関数にしたい)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private char parsePDFVersion(string s)
        {
            switch (s)
            {
                case "1.7": return iTextPDF.PdfWriter.VERSION_1_7; 
                case "1.6": return iTextPDF.PdfWriter.VERSION_1_6; 
                case "1.5": return iTextPDF.PdfWriter.VERSION_1_5; 
                case "1.4": return iTextPDF.PdfWriter.VERSION_1_4; 
                case "1.3": return iTextPDF.PdfWriter.VERSION_1_3; 
                case "1.2": return iTextPDF.PdfWriter.VERSION_1_2;
                default: return iTextPDF.PdfWriter.VERSION_1_7;
            }
        }
        

    }
}
