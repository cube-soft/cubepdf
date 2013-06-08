/* ------------------------------------------------------------------------- */
/*
 *  Appearance.cs
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
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Text;

namespace CubePdf {
    /* --------------------------------------------------------------------- */
    ///
    ///  Appearance
    ///  
    ///  <summary>
    ///  CubePDF メイン画面のコンボボックスに表示する文字列を定義した
    ///  クラスです。Parameter クラスの各種パラメータに対応する文字列を
    ///  定義しています。
    ///  </summary>
    ///  
    /* --------------------------------------------------------------------- */
    class Appearance {
        /* ----------------------------------------------------------------- */
        ///
        /// FileFilterString
        ///
        /// <summary>
        /// 保存ファイルのフィルターに適用する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string FileFilterString() {
            StringBuilder dest = new StringBuilder();
            dest.Append("PDF ファイル (*.pdf)|*.pdf");
            dest.Append("|");
            dest.Append("PS ファイル (*.ps)|*.ps");
            dest.Append("|");
            dest.Append("EPS ファイル (*.eps)|*.eps");
            dest.Append("|");
            dest.Append("SVG ファイル (*.svg)|*.svg");
            dest.Append("|");
            dest.Append("PNG ファイル (*.png)|*.png");
            dest.Append("|");
            dest.Append("JPEG ファイル (*.jpg;*.jpeg)|*.jpg;*.jpeg");
            dest.Append("|");
            dest.Append("BMP ファイル (*.bmp)|*.bmp");
            dest.Append("|");
            dest.Append("TIFF ファイル (*.tiff;*.tif)|*.tiff;*.tif");
            return dest.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileTypeString
        ///
        /// <summary>
        /// FileTypes の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string FileTypeString(Parameter.FileTypes id) {
            return Parameter.FileTypeValue(id);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PDFVersionString
        ///
        /// <summary>
        /// PDFVersions の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string PDFVersionString(Parameter.PDFVersions id) {
            if (id == Parameter.PDFVersions.VerPDFA) return ""; //"PDF/A";
            else if (id == Parameter.PDFVersions.VerPDFX) return ""; //"PDF/X";
            return Parameter.PDFVersionValue(id).ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExistedFileString
        ///
        /// <summary>
        /// ExistedFiles の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string ExistedFileString(Parameter.ExistedFiles id) {
            switch (id) {
            case Parameter.ExistedFiles.Overwrite: return "上書き";
            case Parameter.ExistedFiles.MergeHead: return "先頭に結合";
            case Parameter.ExistedFiles.MergeTail: return "末尾に結合";
            case Parameter.ExistedFiles.Rename: return "リネーム";
            default: break;
            }
            return "Unknown";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcessString
        ///
        /// <summary>
        /// PostProcesses の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string PostProcessString(Parameter.PostProcesses id) {
            switch (id) {
            case Parameter.PostProcesses.Open: return "開く";
            case Parameter.PostProcesses.None: return "何もしない";
            case Parameter.PostProcesses.UserProgram: return "ユーザープログラム";
            default: break;
            }
            return "Unknown";
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// ResolutionString
        ///
        /// <summary>
        /// Resolutions の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string ResolutionString(Parameter.Resolutions id) {
            return Parameter.ResolutionValue(id).ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DownSamplingString
        ///
        /// <summary>
        /// DownSamplings の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string DownSamplingString(Parameter.DownSamplings id) {
            switch (id) {
            case Parameter.DownSamplings.None:      return "なし";
            case Parameter.DownSamplings.Average:   return "平均化";
            case Parameter.DownSamplings.Bicubic:   return "バイキュービック";
            case Parameter.DownSamplings.Subsample: return "サブサンプル";
            default: break;
            }
            return "Unknown";
        }
    }
}
