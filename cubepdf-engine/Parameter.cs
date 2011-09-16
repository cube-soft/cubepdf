/* ------------------------------------------------------------------------- */
/*
 *  Parameter.cs
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

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    ///  Parameter
    ///  
    ///  <summary>
    ///  各種パラメータの値を定義したクラス．
    ///  </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Parameter {
        /* ----------------------------------------------------------------- */
        /// 各種パラメータの型 (enum) 定義
        /* ----------------------------------------------------------------- */
        #region Type definitions

        /* ----------------------------------------------------------------- */
        /// FileTypes
        /* ----------------------------------------------------------------- */
        public enum FileTypes : int {
            PDF, PS, EPS, SVG, PNG, JPEG, BMP, TIFF,
        };

        /* ----------------------------------------------------------------- */
        /// PDFVersions
        /* ----------------------------------------------------------------- */
        public enum PDFVersions : int {
            Ver1_7, Ver1_6, Ver1_5, Ver1_4, Ver1_3, Ver1_2, VerPDFA, VerPDFX
        };

        /* ----------------------------------------------------------------- */
        ///
        /// ExistedFiles
        /// 
        /// <summary>
        /// 出力ファイルが既に存在する場合の処理．上書き，先頭に結合，
        /// 末尾に結合の 3種類．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public enum ExistedFiles : int {
            Overwrite, MergeHead, MergeTail, Rename,
        };

        /* ----------------------------------------------------------------- */
        /// PostProcesses
        /* ----------------------------------------------------------------- */
        public enum PostProcesses : int {
            Open, None, UserProgram,
        };

        /* ----------------------------------------------------------------- */
        /// Resolutions
        /* ----------------------------------------------------------------- */
        public enum Resolutions : int {
            Resolution72, Resolution150, Resolution300, Resolution450, Resolution600,
        };

        /* ----------------------------------------------------------------- */
        /// DownSamplings
        /* ----------------------------------------------------------------- */
        public enum DownSamplings : int {
            None, Average, Bicubic, Subsample,
        };

        #endregion

        /* ----------------------------------------------------------------- */
        //  各種チェックメソッド
        /* ----------------------------------------------------------------- */
        #region Checking methods

        /* ----------------------------------------------------------------- */
        /// IsImageType
        /* ----------------------------------------------------------------- */
        public static bool IsImageType(FileTypes id) {
            if (id == FileTypes.PNG || id == FileTypes.JPEG || id == FileTypes.BMP || id == FileTypes.TIFF) return true;
            else return false;
        }

        /* ----------------------------------------------------------------- */
        /// IsDocumentType
        /* ----------------------------------------------------------------- */
        public static bool IsDocumentType(FileTypes id) {
            return !IsImageType(id);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  各種 types から別の何らかの値に変換するメソッド群
        /* ----------------------------------------------------------------- */
        #region Translation methods

        /* ----------------------------------------------------------------- */
        /// FileTypeValue
        /* ----------------------------------------------------------------- */
        public static string FileTypeValue(FileTypes id) {
            switch (id) {
            case FileTypes.PDF: return "PDF";
            case FileTypes.PS: return "PS";
            case FileTypes.EPS: return "EPS";
            case FileTypes.PNG: return "PNG";
            case FileTypes.JPEG: return "JPEG";
            case FileTypes.BMP: return "BMP";
            case FileTypes.TIFF: return "TIFF";
            case FileTypes.SVG: return "SVG";
            default: break;
            }
            return "";
        }

        /* ----------------------------------------------------------------- */
        /// Extension
        /* ----------------------------------------------------------------- */
        public static string Extension(FileTypes id) {
            switch (id) {
            case FileTypes.PDF:  return ".pdf";
            case FileTypes.PS:   return ".ps";
            case FileTypes.EPS:  return ".eps";
            case FileTypes.PNG:  return ".png";
            case FileTypes.JPEG: return ".jpg";
            case FileTypes.BMP:  return ".bmp";
            case FileTypes.TIFF: return ".tiff";
            case FileTypes.SVG:  return ".svg";
            default: break;
            }
            return "";
        }

        /* ----------------------------------------------------------------- */
        /// PDFVersionValue
        /* ----------------------------------------------------------------- */
        public static double PDFVersionValue(PDFVersions id) {
            switch (id) {
            case PDFVersions.Ver1_7: return 1.7;
            case PDFVersions.Ver1_6: return 1.6;
            case PDFVersions.Ver1_5: return 1.5;
            case PDFVersions.Ver1_4: return 1.4;
            case PDFVersions.Ver1_3: return 1.3;
            case PDFVersions.Ver1_2: return 1.2;
            case PDFVersions.VerPDFA: return 1.3;
            case PDFVersions.VerPDFX: return 1.3;
            }
            return 1.7;
        }

        /* ----------------------------------------------------------------- */
        /// ResolutionValue
        /* ----------------------------------------------------------------- */
        public static int ResolutionValue(Resolutions id) {
            switch (id) {
            case Resolutions.Resolution72:  return 72;
            case Resolutions.Resolution150: return 150;
            case Resolutions.Resolution300: return 300;
            case Resolutions.Resolution450: return 450;
            case Resolutions.Resolution600: return 600;
            default: break;
            }
            return 300;
        }

        #endregion

#if HAVE_GHOSTSCRIPT
        /* ----------------------------------------------------------------- */
        //  GsConverter (Ghostscript) が使用するメソッド群
        /* ----------------------------------------------------------------- */
        #region Ghostscript extensions

        /* ----------------------------------------------------------------- */
        /// Device
        /* ----------------------------------------------------------------- */
        public static Ghostscript.Devices Device(FileTypes id, bool grayscale) {
            switch (id) {
            case FileTypes.PDF:  return Ghostscript.Devices.PDF;
            case FileTypes.PS:   return Ghostscript.Devices.PS;
            case FileTypes.EPS:  return Ghostscript.Devices.EPS;
            case FileTypes.PNG:  return grayscale ? Ghostscript.Devices.PNG_Gray : Ghostscript.Devices.PNG;
            case FileTypes.JPEG: return grayscale ? Ghostscript.Devices.JPEG_Gray : Ghostscript.Devices.JPEG;
            case FileTypes.BMP:  return grayscale ? Ghostscript.Devices.BMP_Gray : Ghostscript.Devices.BMP;
            case FileTypes.TIFF: return grayscale ? Ghostscript.Devices.TIFF_Gray : Ghostscript.Devices.TIFF;
            case FileTypes.SVG:  return Ghostscript.Devices.SVG;
            default:
                break;
            }
            return Ghostscript.Devices.PDF;
        }

        #endregion
#endif
    }
}
