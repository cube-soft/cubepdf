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

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    ///  Appearance
    ///  
    ///  <summary>
    ///  CubePDF メイン画面のコンボボックスに表示する文字列を定義した
    ///  クラス．Parameter クラスの各種パラメータに対応する文字列を
    ///  定義する．
    ///  </summary>
    ///  
    /* --------------------------------------------------------------------- */
    class Appearance {
        /* ----------------------------------------------------------------- */
        /// FileTypeString
        /* ----------------------------------------------------------------- */
        public static string FileTypeString(Parameter.FileTypes index) {
            switch (index) {
            case Parameter.FileTypes.PDF:  return "PDF";
            case Parameter.FileTypes.PS:   return "PS";
            case Parameter.FileTypes.EPS:  return "EPS";
            case Parameter.FileTypes.PNG:  return "PNG";
            case Parameter.FileTypes.JPEG: return "JPEG";
            case Parameter.FileTypes.BMP:  return "BMP";
            case Parameter.FileTypes.TIFF: return "TIFF";
            case Parameter.FileTypes.SVG:  return "SVG";
            default: break;
            }
            return "Unknown";
        }

        /* ----------------------------------------------------------------- */
        /// FileFilterString
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
        /// PDFVersionString
        /* ----------------------------------------------------------------- */
        public static string PDFVersionString(Parameter.PDFVersions index) {
            return Parameter.PDFVersionValue(index).ToString();
        }

        /* ----------------------------------------------------------------- */
        /// ExistedFileString
        /* ----------------------------------------------------------------- */
        public static string ExistedFileString(Parameter.ExistedFiles index) {
            switch (index) {
            case Parameter.ExistedFiles.Overwrite: return "上書き";
            case Parameter.ExistedFiles.MergeHead: return "先頭に結合";
            case Parameter.ExistedFiles.MergeTail: return "末尾に結合";
            default: break;
            }
            return "Unknown";
        }

        /* ----------------------------------------------------------------- */
        /// PostProcessString
        /* ----------------------------------------------------------------- */
        public static string PostProcessString(Parameter.PostProcesses index) {
            switch (index) {
            case Parameter.PostProcesses.Open: return "開く";
            case Parameter.PostProcesses.None: return "何もしない";
            case Parameter.PostProcesses.UserProgram: return "ユーザープログラム";
            default: break;
            }
            return "Unknown";
        }
        
        /* ----------------------------------------------------------------- */
        /// ResolutionString
        /* ----------------------------------------------------------------- */
        public static string ResolutionString(Parameter.Resolutions index) {
            return Parameter.ResolutionValue(index).ToString();
        }

        /* ----------------------------------------------------------------- */
        /// DownSamplingString
        /* ----------------------------------------------------------------- */
        public static string DownSamplingString(Parameter.DownSamplings index) {
            switch (index) {
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
