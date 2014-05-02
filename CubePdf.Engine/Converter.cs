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
            try
            {
                CreateWorkDirectory(setting);
                RunConverter(setting);
                RunEditor(setting);
                RunPostProcess(setting);
            }
            catch (Exception err)
            {
                RecoverIf(setting);
                AddMessage(err);
                return false;
            }
            finally { Sweep(setting); }

            return true;
        }

        #endregion

        #region Operation methods

        /* ----------------------------------------------------------------- */
        ///
        /// RunConverter
        ///
        /// <summary>
        /// Ghostscript に必要な設定を行った後、変換処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RunConverter(UserSetting setting)
        {
            EscapeIf(setting);
            var gs = Configure(setting, setting.InputPath, setting.OutputPath);
            gs.Run();
            AddMessage("CubePdf.Converter.RunConverter: success");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RunWebOptimize
        ///
        /// <summary>
        /// Ghostscript を利用して Web 最適化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RunWebOptimize(UserSetting setting, string src, string dest)
        {
            var gs = Configure(setting, src, dest);
            gs.AddOption("FastWebView");
            gs.Run();
            AddMessage("CubePdf.Converter.RunWebOptimize: success");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RunEditor
        ///
        /// <summary>
        /// Ghostscript で変換したファイルに対して、必要な後処理を実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// 現在、PDF ファイル以外への後処理は存在しません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void RunEditor(UserSetting setting)
        {
            if (setting.FileType != Parameter.FileTypes.PDF) return;

            var editor = new Editor();
            editor.Version      = setting.PDFVersion;
            editor.Document     = setting.Document;
            editor.Permission   = setting.Permission;
            editor.UserPassword = setting.Password;
            
            // 結合順序を考慮してファイルを追加する。
            var head = setting.ExistedFile == Parameter.ExistedFiles.MergeHead || !string.IsNullOrEmpty(_escaped);
            var tail = setting.ExistedFile == Parameter.ExistedFiles.MergeTail || !string.IsNullOrEmpty(_escaped);
            if (tail) editor.Files.Add(_escaped);
            editor.Files.Add(setting.OutputPath);
            if (head) editor.Files.Add(_escaped);

            var tmp = System.IO.Path.Combine(Path.WorkingDirectory, System.IO.Path.GetRandomFileName());
            editor.Run(tmp);
            AddMessage(string.Format("CubePdf.PageBinder.Save: {0}", tmp));

            if (setting.WebOptimize)
            {
                var src = tmp;
                tmp = System.IO.Path.Combine(Path.WorkingDirectory, System.IO.Path.GetRandomFileName());
                RunWebOptimize(setting, src, tmp);
            }

            if (System.IO.File.Exists(tmp)) CubePdf.Misc.File.Copy(tmp, setting.OutputPath, true);
            AddMessage("CubePdf.Converter.RunEditor: success");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RunPostProcess
        ///
        /// <summary>
        /// ポストプロセスを実行します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void RunPostProcess(UserSetting setting)
        {
            var process = new PostProcess(_messages);
            process.Verb          = setting.PostProcess;
            process.FileName      = setting.OutputPath;
            process.UserProgram   = setting.UserProgram;
            process.UserArguments = setting.UserArguments;
            process.Run();
            AddMessage("CubePdf.Converter.RunPostProcess: success");
        }

        #endregion

        #region Configuration methods

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        /// 
        /// <summary>
        /// Ghostscript オブジェクトを生成し、必要な設定を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Ghostscript.Converter Configure(UserSetting setting, string src, string dest)
        {
            var gs = new Ghostscript.Converter(_messages);
            if (!string.IsNullOrEmpty(setting.LibPath)) gs.AddInclude(System.IO.Path.Combine(setting.LibPath, "lib"));
            gs.Device = Parameter.Device(setting.FileType, setting.Grayscale);
            gs.PageRotation = setting.PageRotation;
            gs.Resolution = Parameter.ResolutionValue(setting.Resolution);

            ConfigureCommonImage(setting, gs);
            if (Parameter.IsImageType(setting.FileType)) ConfigureBitmap(setting, gs);
            else ConfigureDocument(setting, gs);

            gs.AddSource(src);
            gs.Destination = dest;

            return gs;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigureCommonImage
        ///
        /// <summary>
        /// 画像に関わるオプションを設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// 全てのファイルタイプ共通の設定です。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigureCommonImage(UserSetting setting, Ghostscript.Converter gs)
        {
            gs.AddOption("ColorConversionStrategy", setting.Grayscale ? "/Gray" : "/RGB");
            gs.AddOption("DownsampleColorImages", true);
            gs.AddOption("DownsampleGrayImages",  true);
            gs.AddOption("DownsampleMonoImages",  true);

            // 解像度
            var resolution = Parameter.ResolutionValue(setting.Resolution);
            gs.AddOption("ColorImageResolution", resolution);
            gs.AddOption("GrayImageResolution",  resolution);
            gs.AddOption("MonoImageResolution",  resolution);

            // 画像圧縮
            gs.AddOption("AutoFilterColorImages", false);
            gs.AddOption("AutoFilterGrayImages",  false);
            gs.AddOption("AutoFilterMonoImages",  false);
            gs.AddOption("ColorImageFilter", "/" + setting.ImageFilter.ToString());
            gs.AddOption("GrayImageFilter",  "/" + setting.ImageFilter.ToString());
            gs.AddOption("MonoImageFilter",  "/" + setting.ImageFilter.ToString());

            // ダウンサンプリング
            if (setting.DownSampling != Parameter.DownSamplings.None)
            {
                gs.AddOption("ColorImageDownsampleType", "/" + setting.DownSampling.ToString());
                gs.AddOption("GrayImageDownsampleType",  "/" + setting.DownSampling.ToString());
                gs.AddOption("MonoImageDownsampleType",  "/" + setting.DownSampling.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigureBitmap
        ///
        /// <summary>
        /// BMP, PNG, JPEG のビットマップ系ファイルに変換するために必要な
        /// オプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigureBitmap(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("GraphicsAlphaBits", 4);
            gs.AddOption("TextAlphaBits", 4);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigureDocument
        ///
        /// <summary>
        /// PDF, PostScript, EPS のファイルに変換するために必要なオプションを
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigureDocument(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("EmbedAllFonts", setting.EmbedFont);
            if (setting.EmbedFont) gs.AddOption("SubsetFonts", true);
            if (setting.FileType == Parameter.FileTypes.PDF) ConfigurePdf(setting, gs);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigurePdf
        ///
        /// <summary>
        /// PDF ファイルに変換するために必要なオプションを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfigurePdf(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("CompatibilityLevel", Parameter.PdfVersionValue(setting.PDFVersion));
            gs.AddOption("UseFlateCompression", true);
            if (setting.PDFVersion == Parameter.PdfVersions.VerPDFA) ConfigurePdfA(setting, gs);
            if (setting.PDFVersion == Parameter.PdfVersions.VerPDFX) ConfigurePdfX(setting, gs);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigurePdfA
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
        private void ConfigurePdfA(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("PDFA");
            gs.AddOption("EmbedAllFonts", true);
            gs.AddOption("SubsetFonts", true);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigurePdfX
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
        private void ConfigurePdfX(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("PDFX");
            gs.AddOption("EmbedAllFonts", true);
            gs.AddOption("SubsetFonts", true);
            if (!setting.Grayscale) gs.AddOption("ColorConversionStrategy", "/CMYK");
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
            if (System.IO.File.Exists(setting.OutputPath)) return true;
            else if (setting.FileType == Parameter.FileTypes.EPS ||
                     setting.FileType == Parameter.FileTypes.BMP ||
                     setting.FileType == Parameter.FileTypes.JPEG ||
                     setting.FileType == Parameter.FileTypes.PNG ||
                     setting.FileType == Parameter.FileTypes.TIFF)
            {
                var dir = System.IO.Path.GetDirectoryName(setting.OutputPath);
                var basename = System.IO.Path.GetFileNameWithoutExtension(setting.OutputPath);
                var ext = System.IO.Path.GetExtension(setting.OutputPath);
                if (System.IO.File.Exists(System.IO.Path.Combine(dir, basename + "-001" + ext))) return true;
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
                    AddMessage(string.Format("Rename: {0} -> {1}", old, filename));
                    if (!FileExists(setting)) break;
                }
            }
            else if (setting.FileType == Parameter.FileTypes.PDF && is_merge)
            {
                _escaped = System.IO.Path.Combine(Path.WorkingDirectory, System.IO.Path.GetRandomFileName());
                System.IO.File.Copy(setting.OutputPath, _escaped, true);
                AddMessage(string.Format("Escape: {0} -> {1}", setting.OutputPath, _escaped));
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
                AddMessage(string.Format("Recover: {0} -> {1}", _escaped, setting.OutputPath));
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
                var work = Path.WorkingDirectory;
                if (System.IO.Directory.Exists(work))
                {
                    System.IO.Directory.Delete(work, true);
                    AddMessage(string.Format("DeleteWorkingDirectory: {0}", work));
                }

                if (setting.DeleteOnClose && System.IO.File.Exists(setting.InputPath))
                {
                    System.IO.File.Delete(setting.InputPath);
                    AddMessage(string.Format("DeleteOnClose: {0}", setting.InputPath));
                }
            }
            catch (Exception err) { AddMessage(err, true); }
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
            var work = System.IO.Path.Combine(setting.LibPath, System.IO.Path.GetRandomFileName());
            if (System.IO.File.Exists(work)) System.IO.File.Delete(work);
            if (System.IO.Directory.Exists(work)) System.IO.Directory.Delete(work, true);
            System.IO.Directory.CreateDirectory(work);
            Path.WorkingDirectory = work;
            AddMessage(string.Format("CreateWorkDirectory: {0}", work));
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
        private void AddMessage(string message)
        {
            _messages.Add(new Message(Message.Levels.Debug, message));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddMessage
        /// 
        /// <summary>
        /// デバッグ用メッセージを追加します。
        /// </summary>
        /// 
        /// <remarks>
        /// Message.Levels.Error で追加するとスタックトレースが表示されない
        /// ので、2 種類のレベルで追加します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void AddMessage(Exception error, bool debug_only = false)
        {
            if (!debug_only) _messages.Add(new Message(Message.Levels.Error, error));
            _messages.Add(new Message(Message.Levels.Debug, error));
        }

        #endregion

        #region Variables
        private string _escaped = null; // null 以外ならマージが必要
        private List<CubePdf.Message> _messages = null;
        #endregion
    }
}
