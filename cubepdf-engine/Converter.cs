/* ------------------------------------------------------------------------- */
/*
 *  Converter.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft Inc. All rights reserved.
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
using System.Text;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// Converter
    /* --------------------------------------------------------------------- */
    public class Converter {
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
            _setting = setting;
            _gs = new CubePDF.Ghostscript.Converter(Parameter.Device((Parameter.FileTypes)_setting.FileType, _setting.Grayscale));
            _gs.AddInclude(_setting.LibPath);
            _gs.AddSource(setting.InputPath);

            // AddFont
            //_gs.AddFont(System.Environment.GetEnvironmentVariable("windir") + @"\Fonts");
            
            // rotation
            _gs.PageRotation = _setting.PageRotation;

            // Resolution
            //_gs.Resolution = Parameter.ResolutionString(setting.Resolution);

            // ダウンサンプリング
            _gs.AddOption("ColorImageResolution", Parameter.ResolutionString((Parameter.Resolutions)setting.Resolution));
            _gs.AddOption("GrayImageResolution", Parameter.ResolutionString((Parameter.Resolutions)setting.Resolution));
            _gs.AddOption("MonoImageResolution", 300);

            if (setting.DownSampling == Parameter.DownSamplings.None)
            {
                _gs.AddOption("DownsampleColorImages", false);
                _gs.AddOption("AutoFilterColorImages", false);
                _gs.AddOption("DownsampleGrayImages", false);
                _gs.AddOption("AutoFilterGrayImages", false);
                _gs.AddOption("DownsampleMonoImages", false);
                _gs.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (setting.DownSampling == Parameter.DownSamplings.Average)
            {
                _gs.AddOption("DownsampleColorImages", true);
                _gs.AddOption("ColorImageDownsampleType", "/Average");
                _gs.AddOption("AutoFilterColorImages", true);
                _gs.AddOption("DownsampleGrayImages", true);
                _gs.AddOption("GrayImageDownsampleType", "/Average");
                _gs.AddOption("AutoFilterGrayImages", true);
                _gs.AddOption("DownsampleMonoImages", true);
                _gs.AddOption("MonoImageDownsampleType", "/Average");
                _gs.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (setting.DownSampling == Parameter.DownSamplings.Bicubic)
            {
                _gs.AddOption("DownsampleColorImages", true);
                _gs.AddOption("ColorImageDownsampleType", "/Bicubic");
                _gs.AddOption("AutoFilterColorImages", true);
                _gs.AddOption("DownsampleGrayImages", true);
                _gs.AddOption("GrayImageDownsampleType", "/Bicubic");
                _gs.AddOption("AutoFilterGrayImages", true);
                _gs.AddOption("DownsampleMonoImages", true);
                _gs.AddOption("MonoImageDownsampleType", "/Bicubic");
                _gs.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (setting.DownSampling == Parameter.DownSamplings.Subsample)
            {
                _gs.AddOption("DownsampleColorImages", true);
                _gs.AddOption("ColorImageDownsampleType", "/Subsample");
                _gs.AddOption("AutoFilterColorImages", true);
                _gs.AddOption("DownsampleGrayImages", true);
                _gs.AddOption("GrayImageDownsampleType", "/Subsample");
                _gs.AddOption("AutoFilterGrayImages", true);
                _gs.AddOption("DownsampleMonoImages", true);
                _gs.AddOption("MonoImageDownsampleType", "/Subsample");
                _gs.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            
            // ファイルタイプに依存するオプション
            if (setting.FileType == Parameter.FileTypes.PNG ||
                setting.FileType == Parameter.FileTypes.JPEG ||
                setting.FileType == Parameter.FileTypes.BMP ||
                setting.FileType == Parameter.FileTypes.TIFF)
            {
                _gs.AddOption("GraphicsAlphaBits", 4);
                _gs.AddOption("TextAlphaBits", 4);
            }
            else
            {
                // フォントの埋め込み
                if (setting.EmbedFont)
                {
                    _gs.AddOption("EmbedAllFonts", "true");
                    _gs.AddOption("SubsetFonts", "true");
                }

                if (setting.FileType == (int)Parameter.FileTypes.PDF)
                {
                    // PDF バージョン
                    _gs.AddOption("CompatibilityLevel", Parameter.VersionString((Parameter.PDFVersions)setting.PDFVersion));

                    _gs.AddOption("UseFlateCompression", "false");

                    // グレースケール
                    if (setting.Grayscale)
                    {
                        _gs.AddOption("ProcessColorModel", "/DeviceGray");
                        _gs.AddOption("ColorConversionStrategy", "/Gray");
                    }
                }
            }

            _gs.Destination = setting.OutputPath;

            // 以下の場合、マージ先のファイル(outputPath)のファイルを退避する必要がある
            // そもそもマージ先のファイルが無い場合のポリシーは？
            if (System.IO.File.Exists(setting.OutputPath) &&
                (setting.ExistedFile == Parameter.ExistedFiles.MergeTail ||
                 setting.ExistedFile == Parameter.ExistedFiles.MergeHead))
            {
                // TODO: C:\Windows\CubePDF\<tmppath> に展開するようにする．
                evacuatedFilePath = System.IO.Path.GetTempFileName(); // 書き込み権限の無い場所が与えられるかもしれないので、調整が必要らしい
                System.IO.File.Copy(setting.OutputPath, evacuatedFilePath, true); // evacuatedFileが消去されるのはマージ後

            }

            _gs.Convert();
            return true;
        }

        /* ----------------------------------------------------------------- */
        /// 変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        Ghostscript.Converter _gs = null;
        UserSetting _setting = null;
        /// <summary>
        /// evacuatedFilePathはマージの際、退避したファイルのパス。
        /// null以外ならマージが必要なことを示す。
        /// </summary>
        private string evacuatedFilePath = null;
        #endregion
    }
}
