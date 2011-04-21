/* ------------------------------------------------------------------------- */
/*
 *  PDFModifier.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft, Inc. All rights reserved.
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
 */
/* ------------------------------------------------------------------------- */
using System;
using System.IO;
using Container = System.Collections.Generic;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// PDFModifier
    /* --------------------------------------------------------------------- */
    class PDFModifier {
        public PDFModifier() { }

        public PDFModifier(string escaped) {
            _escaped = escaped;
        }

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting) {
            bool status = true;

            if (_escaped != null) status &= this.Merge(setting, _escaped);
            status &= this.AddInformation(setting);
            if (setting.WebOptimize) this.WebOptimize(setting); // Web 最適化のエラーは容認する

            return status;
        }

        /* ----------------------------------------------------------------- */
        /// Merge
        /* ----------------------------------------------------------------- */
        private bool Merge(UserSetting setting, string escaped) {
            // Nothing to do.
            if (escaped == null ||
                (setting.ExistedFile != Parameter.ExistedFiles.MergeHead &&
                setting.ExistedFile != Parameter.ExistedFiles.MergeTail)) return true;

            string tmp = Utility.WorkingDirectory + '\\' + Path.GetRandomFileName();
            string head = (setting.ExistedFile == Parameter.ExistedFiles.MergeHead) ? setting.OutputPath : escaped;
            string tail = (setting.ExistedFile == Parameter.ExistedFiles.MergeTail) ? setting.OutputPath : escaped;

            bool status = true;
            try {
                iTextSharp.text.pdf.PdfReader reader_head = new iTextSharp.text.pdf.PdfReader(head);
                iTextSharp.text.pdf.PdfReader reader_tail = new iTextSharp.text.pdf.PdfReader(tail);
                using (FileStream fs = new FileStream(tmp, FileMode.Create)) {
                    iTextSharp.text.pdf.PdfCopyFields copy = new iTextSharp.text.pdf.PdfCopyFields(fs);
                    copy.AddDocument(reader_head);
                    copy.AddDocument(reader_tail);
                }
                reader_head.Close();
                reader_tail.Close();
            }
            catch (Exception err) {
                _message = err.Message;
                status = false;
            }
            finally {
                if (File.Exists(setting.OutputPath)) File.Delete(setting.OutputPath);
                if (!status || !File.Exists(tmp)) File.Move(escaped, setting.OutputPath);
                else File.Move(tmp, setting.OutputPath);

                if (File.Exists(tmp)) File.Delete(tmp);
                if (File.Exists(escaped)) File.Delete(escaped);
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddInformation
        ///
        /// <summary>
        /// iText Sharp を用いて以下の情報を付与する．
        ///   - 文書プロパティ
        ///   - 文書を開くためのパスワード (UserPassword)
        ///   - 権限編集のためのパスワード (OwnerPassword)
        ///   - 各種パーミッション
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool AddInformation(UserSetting setting) {
            if (!File.Exists(setting.OutputPath)) return false;

            string tmp = Utility.WorkingDirectory + '\\' + Path.GetRandomFileName();
            
            iTextSharp.text.pdf.PdfReader reader = null;
            bool status = true;
            try {
                File.Move(setting.OutputPath, tmp);
                reader = new iTextSharp.text.pdf.PdfReader(tmp);
                iTextSharp.text.pdf.PdfStamper writer = new iTextSharp.text.pdf.PdfStamper(reader,
                    new FileStream(setting.OutputPath, FileMode.Create), PDFVersionToiText(setting.PDFVersion));
                
                // 文書プロパティ
                Container.Dictionary<string, string> info = new Container.Dictionary<string,string>();
                info["Title"] = setting.Document.Title;
                info["Author"] = setting.Document.Author;
                info["Subject"] = setting.Document.Subtitle;
                info["Keywords"] = setting.Document.Keyword;
                info["Creator"] = "CubePDF";
                info["Producer"] = "CubePDF";
                writer.MoreInfo = info;

                // パスワード and/or パーミッション
                string user = (setting.Password.Length > 0) ? setting.Password : null;
                string owner = (setting.Permission.Password.Length > 0) ? setting.Permission.Password : null;
                if (owner == null && user != null) owner = user;
                int permission = this.PermissionToiText(setting.Permission);
                if (user != null || owner != null) writer.SetEncryption(iTextSharp.text.pdf.PdfWriter.STANDARD_ENCRYPTION_128, user, owner, permission);
                
                writer.Close();
            }
            catch (Exception err) {
                _message = err.Message;
                status = false;
            }
            finally {
                if (reader != null) reader.Close();
                if (File.Exists(tmp)) {
                    if (!File.Exists(setting.OutputPath)) File.Move(tmp, setting.OutputPath);
                    else {
                        FileInfo fi = new FileInfo(setting.OutputPath);
                        if (fi.Length == 0) File.Move(tmp, setting.OutputPath);
                        else File.Delete(tmp);
                    }
                }
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        /// WebOptimize
        /* ----------------------------------------------------------------- */
        private bool WebOptimize(UserSetting setting) {
            string tmp = Utility.WorkingDirectory + '\\' + Path.GetRandomFileName();
            bool status = true;
            try {
                if (File.Exists(tmp)) File.Delete(tmp);
                File.Move(setting.OutputPath, tmp);
                Ghostscript.Converter gs = new CubePDF.Ghostscript.Converter(Ghostscript.Device.PDF_Opt);
                gs.AddInclude(setting.LibPath + @"\lib");
                gs.AddSource(tmp);
                gs.Destination = setting.OutputPath;

                gs.AddOption("CompatibilityLevel", Parameter.PDFVersionValue(setting.PDFVersion));
                gs.AddOption("UseFlateCompression", false);

                if (setting.EmbedFont) {
                    gs.AddOption("EmbedAllFonts", true);
                    gs.AddOption("SubsetFonts", true);
                }

                if (setting.Grayscale) {
                    gs.AddOption("ProcessColorModel", "/DeviceGray");
                    gs.AddOption("ColorConversionStrategy", "/Gray");
                }

                gs.Run();
            }
            catch (Exception err) {
                status = false;
                _message = String.Format("{0}\n{1}", "Web 表示用の最適化に失敗しました。通常のPDFファイルを出力します。", err.Message);
            }
            finally {
                if (!File.Exists(setting.OutputPath)) File.Move(tmp, setting.OutputPath);
                else {
                    FileInfo fi = new FileInfo(setting.OutputPath);
                    if (fi.Length == 0) File.Move(tmp, setting.OutputPath);
                    else File.Delete(tmp);
                }
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        //  UserSetting から iText のパラメータへ変換する
        /* ----------------------------------------------------------------- */
        #region Convert user setting to iText

        /* ----------------------------------------------------------------- */
        /// PDFVersionToiText
        /* ----------------------------------------------------------------- */
        private char PDFVersionToiText(Parameter.PDFVersions id) {
            switch (id) {
            case Parameter.PDFVersions.Ver1_7: return iTextSharp.text.pdf.PdfWriter.VERSION_1_7;
            case Parameter.PDFVersions.Ver1_6: return iTextSharp.text.pdf.PdfWriter.VERSION_1_6;
            case Parameter.PDFVersions.Ver1_5: return iTextSharp.text.pdf.PdfWriter.VERSION_1_5;
            case Parameter.PDFVersions.Ver1_4: return iTextSharp.text.pdf.PdfWriter.VERSION_1_4;
            case Parameter.PDFVersions.Ver1_3: return iTextSharp.text.pdf.PdfWriter.VERSION_1_3;
            case Parameter.PDFVersions.Ver1_2: return iTextSharp.text.pdf.PdfWriter.VERSION_1_2;
            default: break;
            }

            return iTextSharp.text.pdf.PdfWriter.VERSION_1_7;
        }

        /* ----------------------------------------------------------------- */
        /// PermissionToiText
        /* ----------------------------------------------------------------- */
        private int PermissionToiText(PermissionProperty permission) {
            int dest =
                iTextSharp.text.pdf.PdfWriter.AllowAssembly |
                iTextSharp.text.pdf.PdfWriter.AllowCopy |
                iTextSharp.text.pdf.PdfWriter.AllowFillIn |
                iTextSharp.text.pdf.PdfWriter.AllowModifyAnnotations |
                iTextSharp.text.pdf.PdfWriter.AllowModifyContents |
                iTextSharp.text.pdf.PdfWriter.AllowPrinting |
                iTextSharp.text.pdf.PdfWriter.AllowScreenReaders;

            if (permission.Password.Length > 0 && !permission.AllowPrint) {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowPrinting;
            }

            if (permission.Password.Length > 0 && !permission.AllowCopy) {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowCopy;
            }

            if (permission.Password.Length > 0 && !permission.AllowFormInput) {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowFillIn;
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowModifyAnnotations;
            }

            if (permission.Password.Length > 0 && !permission.AllowModify) {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowModifyContents;
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowScreenReaders;
            }

            return dest;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// ErrorMessage
        /* ----------------------------------------------------------------- */
        public string ErrorMessage {
            get { return _message; }
        }

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        string _escaped;
        string _message = "";
        #endregion
    }
}
