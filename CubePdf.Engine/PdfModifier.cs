/* ------------------------------------------------------------------------- */
///
/// PdfModifier.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// PdfModifier
    ///
    /// <summary>
    /// Ghostscript を用いて変換した PDF ファイルに対して、追加で必要な
    /// 修正を行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PdfModifier
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// PdfModifier (constructor)
        ///
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PdfModifier()
        {
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PdfModifier (constructor)
        ///
        /// <summary>
        /// 結合の対象となるファイルを用いて、オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PdfModifier(string escaped)
        {
            _escaped = escaped;
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PdfModifier (constructor)
        ///
        /// <summary>
        /// 結合の対象となるファイル、およびメッセージを格納するための
        /// コンテナを用いて、オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PdfModifier(string escaped, List<CubePdf.Message> messages)
        {
            _escaped = escaped;
            _messages = messages;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Messages
        ///
        /// <summary>
        /// メッセージ一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public List<CubePdf.Message> Messages
        {
            get { return _messages; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Run 
        ///
        /// <summary>
        /// 引数に指定されたユーザ設定にしたがって、Ghostscript で生成された
        /// PDF ファイルに対して追加で必要な修正を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting)
        {
            bool status = true;

            if (!string.IsNullOrEmpty(_escaped)) status &= this.Merge(setting, _escaped);
            if (status) status &= this.AddInformation(setting);
            if (status && setting.WebOptimize) this.WebOptimize(setting); // Web 最適化のエラーは容認する

            return status;
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// Merge
        ///
        /// <summary>
        /// 2 つの PDF ファイルを結合します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool Merge(UserSetting setting, string escaped)
        {
            // Nothing to do.
            if (string.IsNullOrEmpty(escaped) ||
                (setting.ExistedFile != Parameter.ExistedFiles.MergeHead &&
                setting.ExistedFile != Parameter.ExistedFiles.MergeTail)) return true;

            string tmp = Utility.WorkingDirectory + '\\' + System.IO.Path.GetRandomFileName();
            string head = (setting.ExistedFile == Parameter.ExistedFiles.MergeHead) ? setting.OutputPath : escaped;
            string tail = (setting.ExistedFile == Parameter.ExistedFiles.MergeTail) ? setting.OutputPath : escaped;

            bool status = true;
            try
            {
                iTextSharp.text.pdf.PdfReader reader_head = Open(head, setting.Permission.Password);
                iTextSharp.text.pdf.PdfReader reader_tail = Open(tail, setting.Permission.Password);

                using (var fs = new System.IO.FileStream(tmp, System.IO.FileMode.Create))
                {
                    iTextSharp.text.pdf.PdfCopyFields copy = new iTextSharp.text.pdf.PdfCopyFields(fs);
                    copy.AddDocument(reader_head);
                    copy.AddDocument(reader_tail);
                    copy.Close();
                }
                reader_head.Close();
                reader_tail.Close();
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Error, err));
                _messages.Add(new Message(Message.Levels.Debug, err));
                status = false;
            }
            finally
            {
                if (CubePdf.Misc.File.Exists(setting.OutputPath)) CubePdf.Misc.File.Delete(setting.OutputPath, true);
                if (!status || !CubePdf.Misc.File.Exists(tmp)) CubePdf.Misc.File.Move(escaped, setting.OutputPath, true);
                else CubePdf.Misc.File.Move(tmp, setting.OutputPath, true);

                if (CubePdf.Misc.File.Exists(tmp)) CubePdf.Misc.File.Delete(tmp, false);
                if (CubePdf.Misc.File.Exists(escaped)) CubePdf.Misc.File.Delete(escaped, false);
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddInformation
        ///
        /// <summary>
        /// PDF ファイルに対して、引数に指定されたユーザ設定にしたがって
        /// 必要な情報を付与します。
        /// </summary>
        /// 
        /// <remarks>
        /// iTextSharp を用いて以下の情報を付与します。
        /// 
        /// - 文書プロパティ
        /// - 文書を開くためのパスワード (UserPassword)
        /// - 権限編集のためのパスワード (OwnerPassword)
        /// - 各種パーミッション
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool AddInformation(UserSetting setting)
        {
            if (!CubePdf.Misc.File.Exists(setting.OutputPath)) return false;

            string tmp = Utility.WorkingDirectory + '\\' + System.IO.Path.GetRandomFileName();

            iTextSharp.text.pdf.PdfReader reader = null;
            bool status = true;
            try
            {
                CubePdf.Misc.File.Move(setting.OutputPath, tmp, true);
                reader = new iTextSharp.text.pdf.PdfReader(tmp);
                var writer = new iTextSharp.text.pdf.PdfStamper(reader,
                    new System.IO.FileStream(setting.OutputPath, System.IO.FileMode.Create), PdfVersionToiText(setting.PDFVersion));

                // 文書プロパティ
                Dictionary<string, string> info = new Dictionary<string, string>();
                info["Title"] = setting.Document.Title;
                info["Author"] = setting.Document.Author;
                info["Subject"] = setting.Document.Subtitle;
                info["Keywords"] = setting.Document.Keyword;
                info["Creator"] = Properties.Resources.ProductName;
                info["Producer"] = Properties.Resources.ProductName;
                writer.MoreInfo = info;

                // デバッグログ
                var message = new System.Text.StringBuilder();
                message.AppendLine("iTextSharp.text.pdf.PdfStamper.MoreInfo");
                message.AppendLine(String.Format("\tTitle    = {0}", info["Title"]));
                message.AppendLine(String.Format("\tAuthor   = {0}", info["Author"]));
                message.AppendLine(String.Format("\tSubject  = {0}", info["Subject"]));
                message.Append(    String.Format("\tKeywords = {0}", info["Keywords"]));
                _messages.Add(new Message(Message.Levels.Debug, message.ToString()));

                // パスワード and/or パーミッション
                string user  = !string.IsNullOrEmpty(setting.Password) ? setting.Password : string.Empty;
                string owner = !string.IsNullOrEmpty(setting.Permission.Password) ? setting.Permission.Password : string.Empty;
                if (string.IsNullOrEmpty(owner) && !string.IsNullOrEmpty(user)) owner = user;
                int permission = this.PermissionToiText(setting.Permission);
                if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(owner))
                {
                    writer.SetEncryption(iTextSharp.text.pdf.PdfWriter.STANDARD_ENCRYPTION_128, user, owner, permission);
                }

                writer.Close();
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Error, err));
                _messages.Add(new Message(Message.Levels.Debug, err));
                status = false;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (CubePdf.Misc.File.Exists(tmp))
                {
                    if (!CubePdf.Misc.File.Exists(setting.OutputPath)) CubePdf.Misc.File.Move(tmp, setting.OutputPath, true);
                    else
                    {
                        var fi = new System.IO.FileInfo(setting.OutputPath);
                        if (fi.Length == 0) CubePdf.Misc.File.Move(tmp, setting.OutputPath, true);
                        else CubePdf.Misc.File.Delete(tmp, false);
                    }
                }
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WebOptimize
        ///
        /// <summary>
        /// Web に最適化された PDF ファイルに変換します。
        /// </summary>
        /// 
        /// <remarks>
        /// iTextSharp には Web 最適化オプションが存在しないため、
        /// Ghostscript を再度使用して変換を行います。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool WebOptimize(UserSetting setting)
        {
            string tmp = Utility.WorkingDirectory + '\\' + System.IO.Path.GetRandomFileName();
            Ghostscript.Converter gs = new CubePdf.Ghostscript.Converter(_messages);
            gs.Device = Ghostscript.Devices.PDF_Opt;
            bool status = true;
            try
            {
                if (CubePdf.Misc.File.Exists(tmp)) CubePdf.Misc.File.Delete(tmp, true);
                CubePdf.Misc.File.Move(setting.OutputPath, tmp, true);
                gs.AddInclude(setting.LibPath + @"\lib");
                gs.Resolution = Parameter.ResolutionValue(setting.Resolution);
                gs.PageRotation = setting.PageRotation;

                gs.AddOption("CompatibilityLevel", Parameter.PdfVersionValue(setting.PDFVersion));
                gs.AddOption("UseFlateCompression", true);

                if (setting.EmbedFont)
                {
                    gs.AddOption("EmbedAllFonts", true);
                    gs.AddOption("SubsetFonts", true);
                }
                else gs.AddOption("EmbedAllFonts", false);

                if (setting.Grayscale)
                {
                    gs.AddOption("ProcessColorModel", "/DeviceGray");
                    gs.AddOption("ColorConversionStrategy", "/Gray");
                }

                gs.AddSource(tmp);
                gs.Destination = setting.OutputPath;
                gs.Run();
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Warn, "CubePdf.PDFModifier.WebOptimize: False"));
                _messages.Add(new Message(Message.Levels.Debug, err));
                status = false;
            }
            finally
            {
                if (!CubePdf.Misc.File.Exists(setting.OutputPath)) CubePdf.Misc.File.Move(tmp, setting.OutputPath, true);
                else
                {
                    var fi = new System.IO.FileInfo(setting.OutputPath);
                    if (fi.Length == 0) CubePdf.Misc.File.Move(tmp, setting.OutputPath, true);
                    else CubePdf.Misc.File.Delete(tmp, false);
                }
            }

            return status;
        }

        #endregion

        #region Convert UserSetting to iText

        /* ----------------------------------------------------------------- */
        ///
        /// PdfVersionToiText
        ///
        /// <summary>
        /// PdfVersions 列挙型を対応する iTextSharp の型に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private char PdfVersionToiText(Parameter.PdfVersions id)
        {
            switch (id)
            {
                case Parameter.PdfVersions.Ver1_7: return iTextSharp.text.pdf.PdfWriter.VERSION_1_7;
                case Parameter.PdfVersions.Ver1_6: return iTextSharp.text.pdf.PdfWriter.VERSION_1_6;
                case Parameter.PdfVersions.Ver1_5: return iTextSharp.text.pdf.PdfWriter.VERSION_1_5;
                case Parameter.PdfVersions.Ver1_4: return iTextSharp.text.pdf.PdfWriter.VERSION_1_4;
                case Parameter.PdfVersions.Ver1_3: return iTextSharp.text.pdf.PdfWriter.VERSION_1_3;
                case Parameter.PdfVersions.Ver1_2: return iTextSharp.text.pdf.PdfWriter.VERSION_1_2;
                default: break;
            }

            return iTextSharp.text.pdf.PdfWriter.VERSION_1_7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PermissionToiText
        ///
        /// <summary>
        /// PermissionProperty オブジェクトを iTextSharp で利用可能な数値に
        /// 変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int PermissionToiText(PermissionProperty permission)
        {
            int dest =
                iTextSharp.text.pdf.PdfWriter.AllowAssembly |
                iTextSharp.text.pdf.PdfWriter.AllowCopy |
                iTextSharp.text.pdf.PdfWriter.AllowFillIn |
                iTextSharp.text.pdf.PdfWriter.AllowModifyAnnotations |
                iTextSharp.text.pdf.PdfWriter.AllowModifyContents |
                iTextSharp.text.pdf.PdfWriter.AllowPrinting |
                iTextSharp.text.pdf.PdfWriter.AllowScreenReaders;

            if (permission.Password.Length > 0 && !permission.AllowPrint)
            {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowPrinting;
            }

            if (permission.Password.Length > 0 && !permission.AllowCopy)
            {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowCopy;
            }

            if (permission.Password.Length > 0 && !permission.AllowFormInput)
            {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowFillIn;
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowModifyAnnotations;
            }

            if (permission.Password.Length > 0 && !permission.AllowModify)
            {
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowModifyContents;
                dest &= ~iTextSharp.text.pdf.PdfWriter.AllowScreenReaders;
            }

            return dest;
        }

        #endregion

        #region Static methods

        /* ----------------------------------------------------------------- */
        ///
        /// IsProtected
        /// 
        /// <summary>
        /// 指定した PDF ファイルがパスワードによるセキュリティが設定され
        /// ているかどうか判定する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static bool IsProtected(string path)
        {
            bool status = false;
            try
            {
                var test = new iTextSharp.text.pdf.PdfReader(path);
                status = !test.IsOpenedWithFullPermissions;
                test.Close();
            }
            catch (iTextSharp.text.pdf.BadPasswordException /* err */)
            {
                status = true;
            }
            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidPassword
        /// 
        /// <summary>
        /// 指定したパスワードが有効であるかどうかを判定する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static bool IsValidPassword(string path, string password)
        {
            var status = true;
            try
            {
                var test = new iTextSharp.text.pdf.PdfReader(path, System.Text.Encoding.UTF8.GetBytes(password));
                if (!test.IsOpenedWithFullPermissions) status = false;
                test.Close();
            }
            catch (Exception /* err */)
            {
                status = false;
            }
            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open
        /// 
        /// <summary>
        /// PDF ファイルを開く。パスワードが必要な場合は、第 2 引数で指定
        /// されたパスワードで試行する。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static iTextSharp.text.pdf.PdfReader Open(string path, string password)
        {
            iTextSharp.text.pdf.PdfReader dest = null;
            if (!IsProtected(path)) dest = new iTextSharp.text.pdf.PdfReader(path);
            else if (IsValidPassword(path, password))
            {
                dest = new iTextSharp.text.pdf.PdfReader(path, System.Text.Encoding.UTF8.GetBytes(password));
            }
            else throw new Exception(String.Format("パスワードで保護されているPDFファイルに結合する事はできません。", path));

            return dest;
        }

        #endregion

        #region Variables
        string _escaped;
        List<CubePdf.Message> _messages = null;
        #endregion
    }
}
