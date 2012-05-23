/* ------------------------------------------------------------------------- */
/*
 *  Converter.cs
 *
 *  Copyright (c) 2009 CubeSoft, Inc.
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
using System.Collections.Generic;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// Converter
    /* --------------------------------------------------------------------- */
    public class Converter
    {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter()
        {
            _messages = new List<CubePDF.Message>();
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter(List<CubePDF.Message> messages)
        {
            _messages = messages;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// NOTE: 文書プロパティ，パスワード関連とファイル結合は iText
        /// に任せる．出力パスに指定されたファイルが存在する場合がある．
        /// そのときは，_setting.ExistedFile の指定に従う．
        /// ExistedFile: 上書き，先頭に結合，末尾に結合
        /// 結合部分は，iText が行う．
        /// 
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting) {
            // Ghostscript に指定するパスに日本語が入るとエラーが発生する
            // 場合があるので，作業ディレクトリを変更する．
            this.CreateWorkingDirectory(setting);

            Ghostscript.Converter gs = new Ghostscript.Converter(_messages);
            gs.Device = Parameter.Device(setting.FileType, setting.Grayscale);
            bool status = true;
            try {
                gs.AddInclude(setting.LibPath + @"\lib");
                gs.PageRotation = setting.PageRotation;
                gs.Resolution = Parameter.ResolutionValue(setting.Resolution);

                this.ConfigImageOperations(setting, gs);
                if (Parameter.IsImageType(setting.FileType)) this.ConfigImage(setting, gs);
                else this.ConfigDocument(setting, gs);
                this.EscapeExistedFile(setting);

                gs.AddSource(setting.InputPath);
                gs.Destination = setting.OutputPath;
                gs.Run();
                
                if (setting.FileType == Parameter.FileTypes.PDF)
                {
                    PDFModifier modifier = new PDFModifier(_escaped, _messages);
                    status = modifier.Run(setting);
                    _messages.Add(new Message(Message.Levels.Info, String.Format("CubePDF.PDFModifier.Run: {0}", status.ToString())));
                }

                if (status)
                {
                    PostProcess postproc = new PostProcess(_messages);
                    status = postproc.Run(setting);
                    _messages.Add(new Message(Message.Levels.Info, String.Format("CubePDF.PostProcess.Run: {0}", status.ToString())));
                }
            }
            catch (Exception err) {
                _messages.Add(new Message(Message.Levels.Error, err));
                _messages.Add(new Message(Message.Levels.Debug, err));
                status = false;
            }
            finally {
                if (Directory.Exists(Utility.WorkingDirectory)) Directory.Delete(Utility.WorkingDirectory, true);
                if (setting.DeleteOnClose && File.Exists(setting.InputPath))
                {
                    _messages.Add(new Message(Message.Levels.Debug, String.Format("{0}: delete on close", setting.InputPath)));
                    File.Delete(setting.InputPath);
                }
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileExists
        ///
        /// <summary>
        /// ファイルが存在するかどうかをチェックする．いくつかのファイル
        /// タイプは，example-001.ext と言うファイル名を生成する場合が
        /// あるのでそれもチェックする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool FileExists(UserSetting setting) {
            if (File.Exists(setting.OutputPath)) return true;
            else if (setting.FileType == Parameter.FileTypes.EPS ||
                setting.FileType == Parameter.FileTypes.BMP ||
                setting.FileType == Parameter.FileTypes.JPEG ||
                setting.FileType == Parameter.FileTypes.PNG ||
                setting.FileType == Parameter.FileTypes.TIFF) {
                string dir = Path.GetDirectoryName(setting.OutputPath);
                string basename = Path.GetFileNameWithoutExtension(setting.OutputPath);
                string ext = Path.GetExtension(setting.OutputPath);
                if (File.Exists(dir + '\\' + basename + "-001" + ext)) return true;
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EscapeExistedFile
        ///
        /// <summary>
        /// マージオプションなどの関係で既に存在する同名ファイルを退避
        /// させる．リネームの場合は，setting.OutputPath の値を変更する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void EscapeExistedFile(UserSetting setting) {
            if (this.FileExists(setting)) {
                bool merge = (setting.ExistedFile == Parameter.ExistedFiles.MergeTail || setting.ExistedFile == Parameter.ExistedFiles.MergeHead);
                if (setting.ExistedFile == Parameter.ExistedFiles.Rename) {
                    string dir = Path.GetDirectoryName(setting.OutputPath);
                    string basename = Path.GetFileNameWithoutExtension(setting.OutputPath);
                    string ext = Path.GetExtension(setting.OutputPath);
                    for (int i = 2; i < 10000; ++i) {
                        setting.OutputPath = dir + '\\' + basename + '(' + i.ToString() + ')' + ext;
                        if (!this.FileExists(setting)) break;
                    }
                }
                else if (setting.FileType == Parameter.FileTypes.PDF  && merge) {
                    _escaped = Utility.WorkingDirectory + '\\' + Path.GetRandomFileName();
                    File.Copy(setting.OutputPath, _escaped, true);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// CreateWorkingDirectory
        /* ----------------------------------------------------------------- */
        public void CreateWorkingDirectory(UserSetting setting) {
            Utility.WorkingDirectory = setting.LibPath + '\\' + Path.GetRandomFileName();
            if (File.Exists(Utility.WorkingDirectory)) File.Delete(Utility.WorkingDirectory);
            if (Directory.Exists(Utility.WorkingDirectory)) Directory.Delete(Utility.WorkingDirectory, true);
            Directory.CreateDirectory(Utility.WorkingDirectory);
        }

        /* ----------------------------------------------------------------- */
        /// Messages
        /* ----------------------------------------------------------------- */
        public List<CubePDF.Message> Messages {
            get { return _messages; }
        }

        /* ----------------------------------------------------------------- */
        //  UserSetting の値を基に各種設定を行う
        /* ----------------------------------------------------------------- */
        #region Configuration

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigImage
        ///
        /// <summary>
        /// bmp, png, jpeg, gif のビットマップ系ファイルに変換するために
        /// 必要なオプションを設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void ConfigImage(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("GraphicsAlphaBits", 4);
            gs.AddOption("TextAlphaBits", 4);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfigDocument
        ///
        /// <summary>
        /// pdf, ps, eps, svg のベクター系ファイルに変換するために必要な
        /// オプションを設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void ConfigDocument(UserSetting setting, Ghostscript.Converter gs) {
            if (setting.FileType == Parameter.FileTypes.PDF) this.ConfigPDF(setting, gs);
            else {
                if (setting.EmbedFont) {
                    gs.AddOption("EmbedAllFonts", true);
                    gs.AddOption("SubsetFonts", true);
                }
                else gs.AddOption("EmbedAllFonts", false);
            }
        }

        /* ----------------------------------------------------------------- */
        /// ConfigPDF
        /* ----------------------------------------------------------------- */
        public void ConfigPDF(UserSetting setting, Ghostscript.Converter gs) {
            gs.AddOption("CompatibilityLevel", Parameter.PDFVersionValue(setting.PDFVersion));
            gs.AddOption("UseFlateCompression", true);

            if (setting.PDFVersion == Parameter.PDFVersions.VerPDFA) this.ConfigPDFA(setting, gs);
            else if (setting.PDFVersion == Parameter.PDFVersions.VerPDFX) this.ConfigPDFX(setting, gs);
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
        /// ConfigPDFA
        ///
        /// <summary>
        /// PDF/A の主な要求項目は以下の通り:
        /// - デバイス独立カラーまたは PDF/A-1 OutputIntent 指定でカラーの
        ///   再現性を保証する
        /// - 基本 14 フォントを含む全てのフォントの埋め込み
        /// - PDF/Aリーダは，システムのフォントでなく埋め込みフォントで
        ///   表示すること
        /// - XMPメタデータの埋め込み
        /// - タグ付きPDFとする(PDF/A-1aのみ)
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void ConfigPDFA(UserSetting setting, Ghostscript.Converter gs) {
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
        /// ConfigPDFX
        /// 
        /// <summary>
        /// PDF/X(1-a) の主な要求項目は以下の通り:
        /// - すべてのイメージのカラーは CMYKか 特色
        /// - 基本 14 フォントを含む全てのフォントの埋め込み
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void ConfigPDFX(UserSetting setting, Ghostscript.Converter gs) {
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
        /// ConfigImageOperations
        /* ----------------------------------------------------------------- */
        public void ConfigImageOperations(UserSetting setting, Ghostscript.Converter gs) {
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

        /* ----------------------------------------------------------------- */
        //  変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private string _escaped = null; // null 以外ならマージが必要
        private List<CubePDF.Message> _messages = null;
        #endregion
    }
}
