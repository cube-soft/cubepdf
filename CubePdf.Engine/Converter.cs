/* ------------------------------------------------------------------------- */
///
/// Converter.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.IO;
using System.Collections.Generic;

namespace CubePdf {
    /* --------------------------------------------------------------------- */
    ///
    /// Converter
    ///
    /// <summary>
    /// ファイルを変換するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Converter
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// Converter (constructor)
        ///
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Converter()
        {
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        ///
        /// <summary>
        /// 引数に指定されたメッセージを格納するコンテナを用いて、
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Converter(List<CubePdf.Message> messages)
        {
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
        /// ファイル変換処理を実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// 文書プロパティ、パスワード関連とファイル結合は iTextSharp
        /// を利用します。出力パスに指定されたファイルが既に存在する場合、
        /// ExistedFile プロパティの指定（上書き、先頭に結合、末尾に結合、
        /// リネーム）に従います。
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting) {
            CreateWorkDirectory(setting);

            var status = true;
            try
            {
                Convert(setting);

                if (setting.FileType == Parameter.FileTypes.PDF)
                {
                    var modifier = new PdfModifier(_escaped, _messages);
                    status = modifier.Run(setting);
                    _messages.Add(new Message(Message.Levels.Info, String.Format("CubePdf.PDFModifier.Run: {0}", status)));
                }

                if (status)
                {
                    var postproc = new PostProcess(_messages);
                    status = postproc.Run(setting);
                    _messages.Add(new Message(Message.Levels.Info, String.Format("CubePdf.PostProcess.Run: {0}", status)));
                }
            }
            catch (Exception err)
            {
                RecoverIf(setting);
                _messages.Add(new Message(Message.Levels.Error, err));
                status = false;
            }
            finally { Sweep(setting); }

            return status;
        }

        #endregion

        #region Operation methods

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Ghostscript に必要な設定を行った後、変換処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Convert(UserSetting setting)
        {
            var gs = new Ghostscript.Converter(_messages);
            gs.Device = Parameter.Device(setting.FileType, setting.Grayscale);
            gs.AddInclude(System.IO.Path.Combine(setting.LibPath, "lib"));
            gs.PageRotation = setting.PageRotation;
            gs.Resolution = Parameter.ResolutionValue(setting.Resolution);

            ConfigImageOperations(setting, gs);
            if (Parameter.IsImageType(setting.FileType)) ConfigImage(setting, gs);
            else ConfigDocument(setting, gs);
            EscapeIf(setting);

            gs.AddSource(setting.InputPath);
            gs.Destination = setting.OutputPath;
            gs.Run();
            
            AddDebug("Convert: succeed");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Edit
        ///
        /// <summary>
        /// Ghostscript で変換された PDF ファイルに対して、必要な後編集を
        /// 行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Edit(UserSetting setting)
        {

        }

        #endregion

        #region Configuration methods

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigImage
        ///
        /// <summary>
        /// bmp, png, jpeg, gif のビットマップ系ファイルに変換するために
        /// 必要なオプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigImage(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("GraphicsAlphaBits", 4);
            gs.AddOption("TextAlphaBits", 4);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigDocument
        ///
        /// <summary>
        /// pdf, ps, eps, svg のベクター系ファイルに変換するために必要な
        /// オプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigDocument(UserSetting setting, Ghostscript.Converter gs) {
            if (setting.FileType == Parameter.FileTypes.PDF) this.ConfigPdf(setting, gs);
            else {
                if (setting.EmbedFont) {
                    gs.AddOption("EmbedAllFonts", true);
                    gs.AddOption("SubsetFonts", true);
                }
                else gs.AddOption("EmbedAllFonts", false);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigPdf
        ///
        /// <summary>
        /// PDF ファイルに変換するために必要なオプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigPdf(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("CompatibilityLevel", Parameter.PdfVersionValue(setting.PDFVersion));
            gs.AddOption("UseFlateCompression", true);

            if (setting.PDFVersion == Parameter.PdfVersions.VerPDFA) this.ConfigPdfA(setting, gs);
            else if (setting.PDFVersion == Parameter.PdfVersions.VerPDFX) this.ConfigPdfX(setting, gs);
            else {
                if (setting.EmbedFont) {
                    gs.AddOption("EmbedAllFonts", true);
                    gs.AddOption("SubsetFonts", true);
                }
                else gs.AddOption("EmbedAllFonts", false);

                if (setting.Grayscale) {
                    gs.AddOption("ProcessColorModel", "/DeviceGray");
                    gs.AddOption("ColorConversionStrategy", "/Gray");
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigPdfA
        ///
        /// <summary>
        /// PDF/A 形式に変換するのに必要なオプションを設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// PDF/A の主な要求項目は以下の通り:
        /// 
        /// - デバイス独立カラーまたは PDF/A-1 OutputIntent 指定でカラーの
        ///   再現性を保証する
        /// - 基本 14 フォントを含む全てのフォントの埋め込み
        /// - PDF/Aリーダは，システムのフォントでなく埋め込みフォントで
        ///   表示すること
        /// - XMPメタデータの埋め込み
        /// - タグ付きPDFとする(PDF/A-1aのみ)
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigPdfA(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("PDFA");
            gs.AddOption("EmbedAllFonts", true);
            gs.AddOption("SubsetFonts", true);
            if (setting.Grayscale) {
                gs.AddOption("ProcessColorModel", "/DeviceGray");
                gs.AddOption("ColorConversionStrategy", "/Gray");
            }
            gs.AddOption("UseCIEColor");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigPdfX
        /// 
        /// <summary>
        /// PDF/X 形式に変換するのに必要なオプションを設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// PDF/X(1-a) の主な要求項目は以下の通り:
        /// 
        /// - すべてのイメージのカラーは CMYKか 特色
        /// - 基本 14 フォントを含む全てのフォントの埋め込み
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        private void ConfigPdfX(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("PDFX");
            gs.AddOption("EmbedAllFonts", true);
            gs.AddOption("SubsetFonts", true);
            if (setting.Grayscale) {
                gs.AddOption("ProcessColorModel", "/DeviceGray");
                gs.AddOption("ColorConversionStrategy", "/Gray");
            }
            else {
                gs.AddOption("ProcessColorModel", "/DeviceCMYK");
                gs.AddOption("ColorConversionStrategy", "/CMYK");
            }
            gs.AddOption("UseCIEColor");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigImageOperations
        ///
        /// <summary>
        /// 画像に関わるオプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigImageOperations(UserSetting setting, Ghostscript.Converter gs) {
            // 解像度
            var resolution = Parameter.ResolutionValue(setting.Resolution);
            gs.AddOption("ColorImageResolution", resolution);
            gs.AddOption("GrayImageResolution", resolution);
            gs.AddOption("MonoImageResolution", (resolution < 300) ? 300 : 1200);

            // 画像圧縮
            gs.AddOption("AutoFilterColorImages", false);
            gs.AddOption("AutoFilterGrayImages",  false);
            gs.AddOption("AutoFilterMonoImages",  false);
            gs.AddOption("ColorImageFilter", "/" + setting.ImageFilter.ToString());
            gs.AddOption("GrayImageFilter",  "/" + setting.ImageFilter.ToString());
            gs.AddOption("MonoImageFilter",  "/" + setting.ImageFilter.ToString());

            // ダウンサンプリング
            if (setting.DownSampling == Parameter.DownSamplings.None) {
                gs.AddOption("DownsampleColorImages", false);
                gs.AddOption("DownsampleGrayImages",  false);
                gs.AddOption("DownsampleMonoImages",  false);
            }
            else {
                gs.AddOption("DownsampleColorImages", true);
                gs.AddOption("DownsampleGrayImages",  true);
                gs.AddOption("DownsampleMonoImages",  true);
                gs.AddOption("ColorImageDownsampleType", "/" + setting.DownSampling.ToString());
                gs.AddOption("GrayImageDownsampleType",  "/" + setting.DownSampling.ToString());
                gs.AddOption("MonoImageDownsampleType", "/" + setting.DownSampling.ToString());
            }
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// FileExists
        ///
        /// <summary>
        /// ユーザ設定で指定されたファイルが存在するかどうか判別します。
        /// </summary>
        /// 
        /// <remarks>
        /// いくつかのファイルタイプでは、example-001.ext と言ったファイル名を
        /// 生成する事があるので、そのケースもチェックします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool FileExists(UserSetting setting)
        {
            if (File.Exists(setting.OutputPath)) return true;
            else if (setting.FileType == Parameter.FileTypes.EPS ||
                     setting.FileType == Parameter.FileTypes.BMP ||
                     setting.FileType == Parameter.FileTypes.JPEG ||
                     setting.FileType == Parameter.FileTypes.PNG ||
                     setting.FileType == Parameter.FileTypes.TIFF ||
                     setting.FileType == Parameter.FileTypes.SVG)
            {
                var dir = Path.GetDirectoryName(setting.OutputPath);
                var basename = Path.GetFileNameWithoutExtension(setting.OutputPath);
                var ext = Path.GetExtension(setting.OutputPath);
                if (File.Exists(System.IO.Path.Combine(dir, basename + "-001" + ext))) return true;
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EscapeIf
        ///
        /// <summary>
        /// 結合オプションなどの関係で既に存在する同名ファイルを退避させます。
        /// </summary>
        /// 
        /// <remarks>
        /// リネームの場合は、退避させる代わりに UserSetting.OutputPath
        /// プロパティの値を変更します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void EscapeIf(UserSetting setting)
        {
            if (!FileExists(setting)) return;

            var is_merge = setting.ExistedFile == Parameter.ExistedFiles.MergeTail ||
                           setting.ExistedFile == Parameter.ExistedFiles.MergeHead;

            if (setting.ExistedFile == Parameter.ExistedFiles.Rename)
            {
                var directory = System.IO.Path.GetDirectoryName(setting.OutputPath);
                var basename  = System.IO.Path.GetFileNameWithoutExtension(setting.OutputPath);
                var extension = System.IO.Path.GetExtension(setting.OutputPath);

                for (var i = 2; i < 10000; ++i)
                {
                    var old = System.IO.Path.GetFileName(setting.OutputPath);
                    var filename = string.Format("{0}({1}){2}", basename, i, extension);
                    setting.OutputPath = System.IO.Path.Combine(directory, filename);
                    AddDebug(string.Format("Rename: {0} -> {1}", old, filename));
                    if (!FileExists(setting)) break;
                }
            }
            else if (setting.FileType == Parameter.FileTypes.PDF && is_merge)
            {
                _escaped = Path.Combine(Utility.WorkingDirectory, System.IO.Path.GetRandomFileName());
                System.IO.File.Copy(setting.OutputPath, _escaped, true);
                AddDebug(string.Format("Escape: {0} -> {1}", setting.OutputPath, _escaped));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RecoverIf
        ///
        /// <summary>
        /// 結合オプションなどの関係で退避させたファイルが存在する状況で
        /// エラーが発生した場合、退避させたファイルを復帰させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RecoverIf(UserSetting setting)
        {
            if (string.IsNullOrEmpty(_escaped) || !System.IO.File.Exists(_escaped)) return;
            if (!System.IO.File.Exists(setting.OutputPath))
            {
                CubePdf.Misc.File.Move(_escaped, setting.OutputPath, true);
                AddDebug(string.Format("Recover: {0} -> {1}", _escaped, setting.OutputPath));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Sweep
        /// 
        /// <summary>
        /// 不要なファイルやディレクトリを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Sweep(UserSetting setting)
        {
            try
            {
                var work = Utility.WorkingDirectory;
                if (System.IO.Directory.Exists(work))
                {
                    System.IO.Directory.Delete(work, true);
                    AddDebug(string.Format("DeleteWorkingDirectory: {0}", work));
                }

                if (setting.DeleteOnClose && File.Exists(setting.InputPath))
                {
                    System.IO.File.Delete(setting.InputPath);
                    AddDebug(string.Format("DeleteOnClose: {0}", setting.InputPath));
                }
            }
            catch (Exception err) { AddDebug(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateWorkDirectory
        ///
        /// <summary>
        /// 作業用ディレクトリを作成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateWorkDirectory(UserSetting setting)
        {
            Utility.WorkingDirectory = Path.Combine(setting.LibPath, Path.GetRandomFileName());
            if (File.Exists(Utility.WorkingDirectory)) File.Delete(Utility.WorkingDirectory);
            if (Directory.Exists(Utility.WorkingDirectory)) Directory.Delete(Utility.WorkingDirectory, true);
            Directory.CreateDirectory(Utility.WorkingDirectory);
            AddDebug(string.Format("CreateWorkDirectory: {0}", Utility.WorkingDirectory));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddDebug
        /// 
        /// <summary>
        /// デバッグ用メッセージを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddDebug(string message)
        {
            _messages.Add(new Message(Message.Levels.Debug, message));
        }

        #endregion

        #region Variables
        private string _escaped = null; // null 以外ならマージが必要
        private List<CubePdf.Message> _messages = null;
        #endregion
    }
}
