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
                    
                    string user = (UserPasswordCheckBox.Checked && UserPasswordTextBox.Text.Length > 0) ? UserPasswordTextBox.Text : null;
                    string owner = (OwnerPasswordCheckBox.Checked && OwnerPasswordTextBox.Text.Length > 0) ? OwnerPasswordTextBox.Text : null;
                    if (owner == null && user != null) owner = user;

                    int permission =
                        iTextPDF.PdfWriter.AllowAssembly |
                        iTextPDF.PdfWriter.AllowCopy |
                        iTextPDF.PdfWriter.AllowFillIn |
                        iTextPDF.PdfWriter.AllowModifyAnnotations |
                        iTextPDF.PdfWriter.AllowModifyContents |
                        iTextPDF.PdfWriter.AllowPrinting |
                        iTextPDF.PdfWriter.AllowScreenReaders;

                    if (OwnerPasswordCheckBox.Checked && !PrintEnableCheckBox.Checked) {
                        permission &= ~iTextPDF.PdfWriter.AllowPrinting;
                    }

                    if (OwnerPasswordCheckBox.Checked && !CopyEnableCheckBox.Checked) {
                        permission &= ~iTextPDF.PdfWriter.AllowCopy;
                    }

                    if (OwnerPasswordCheckBox.Checked && !InputFormEnableCheckBox.Checked) {
                        permission &= ~iTextPDF.PdfWriter.AllowFillIn;
                        permission &= ~iTextPDF.PdfWriter.AllowModifyAnnotations;
                    }

                    if (OwnerPasswordCheckBox.Checked && !PageInsertEtcEnableCheckBox.Checked) {
                        permission &= ~iTextPDF.PdfWriter.AllowModifyContents;
                        permission &= ~iTextPDF.PdfWriter.AllowScreenReaders;
                    }
                    
                    if (user != null || owner != null) writer.SetEncryption(iTextPDF.PdfWriter.STANDARD_ENCRYPTION_128, user, owner, permission);
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
