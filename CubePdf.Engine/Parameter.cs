/* ------------------------------------------------------------------------- */
///
/// Parameter.cs
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

namespace CubePdf {
    /* --------------------------------------------------------------------- */
    ///
    ///  Parameter
    ///  
    ///  <summary>
    ///  各種パラメータの値を定義したクラスです。
    ///  </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Parameter {
        #region Type definitions

        /* ----------------------------------------------------------------- */
        ///
        /// FileTypes
        ///
        /// <summary>
        /// CubePDF が変換可能なファイル形式を表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum FileTypes : int
        {
            PDF, PS, EPS, PNG, JPEG, BMP, TIFF,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// PDFVersions
        ///
        /// <summary>
        /// PDF のバージョンを表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum PdfVersions : int
        {
            Ver1_7, Ver1_6, Ver1_5, Ver1_4, Ver1_3, Ver1_2, VerPDFA, VerPDFX
        };

        /* ----------------------------------------------------------------- */
        ///
        /// ExistedFiles
        /// 
        /// <summary>
        /// 出力ファイルが既に存在する場合の処理を表す列挙型です。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public enum ExistedFiles : int
        {
            Overwrite, MergeHead, MergeTail, Rename,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcesses
        ///
        /// <summary>
        /// CubePDF の変換終了後に実行するプロセスの種類を表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum PostProcesses : int
        {
            Open, None, UserProgram,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// Resolutions
        ///
        /// <summary>
        /// 変換時の解像度を表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum Resolutions : int
        {
            Resolution72, Resolution150, Resolution300, Resolution450, Resolution600,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// Orientations
        ///
        /// <summary>
        /// ページの向きを表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum Orientations : int
        {
            Portrait, Landscape, Auto,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// DownSamplings
        ///
        /// <summary>
        /// ダウンサンプリングの方法を表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum DownSamplings : int
        {
            None, Average, Bicubic, Subsample,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// ImageFilters
        ///
        /// <summary>
        /// 画像ファイルの圧縮方法を表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum ImageFilters : int
        {
            FlateEncode, DCTEncode, CCITTFaxEncode,
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveSettings
        ///
        /// <summary>
        /// 各種設定をレジストリに反映するかどうかを決定する列挙型です。
        /// </summary>
        /// 
        /// <remarks>
        /// 設定可能な値は、以下の通り:
        /// 
        /// None: 保存しない
        /// Save: 保存する
        /// Reset: 初期値にリセットする
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public enum SaveSettings : int
        {
            None, Save, Reset
        }

        #endregion

        #region Translating methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToValue
        ///
        /// <summary>
        /// それぞれのファイル形式に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToValue(FileTypes id)
        {
            switch (id)
            {
                case FileTypes.PDF: return "PDF";
                case FileTypes.PS: return "PS";
                case FileTypes.EPS: return "EPS";
                case FileTypes.PNG: return "PNG";
                case FileTypes.JPEG: return "JPEG";
                case FileTypes.BMP: return "BMP";
                case FileTypes.TIFF: return "TIFF";
                default: break;
            }
            return "";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToValue
        ///
        /// <summary>
        /// PdfVersions 列挙型の各値に対応する実際の数値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static double ToValue(PdfVersions id)
        {
            switch (id)
            {
                case PdfVersions.Ver1_7: return 1.7;
                case PdfVersions.Ver1_6: return 1.6;
                case PdfVersions.Ver1_5: return 1.5;
                case PdfVersions.Ver1_4: return 1.4;
                case PdfVersions.Ver1_3: return 1.3;
                case PdfVersions.Ver1_2: return 1.2;
                case PdfVersions.VerPDFA: return 1.3;
                case PdfVersions.VerPDFX: return 1.3;
            }
            return 1.7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToValue
        /// 
        /// <summary>
        /// Resolutions 列挙型の各値に対応する実際の数値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int ToValue(Resolutions id)
        {
            switch (id)
            {
                case Resolutions.Resolution72: return 72;
                case Resolutions.Resolution150: return 150;
                case Resolutions.Resolution300: return 300;
                case Resolutions.Resolution450: return 450;
                case Resolutions.Resolution600: return 600;
                default: break;
            }
            return 300;
        }

        #endregion

        #region Checking methods

        /* ----------------------------------------------------------------- */
        ///
        /// IsImageType
        ///
        /// <summary>
        /// ファイルの種類がビットマップイメージかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsImageType(FileTypes id) {
            if (id == FileTypes.PNG || id == FileTypes.JPEG || id == FileTypes.BMP || id == FileTypes.TIFF) return true;
            else return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsDocumentType
        ///
        /// <summary>
        /// ファイルの種類が「ドキュメント」であるかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsDocumentType(FileTypes id) {
            return !IsImageType(id);
        }

        #endregion

        #region Getting methods

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// ファイル形式に対応する拡張子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetExtension(FileTypes id)
        {
            switch (id)
            {
                case FileTypes.PDF: return ".pdf";
                case FileTypes.PS: return ".ps";
                case FileTypes.EPS: return ".eps";
                case FileTypes.PNG: return ".png";
                case FileTypes.JPEG: return ".jpg";
                case FileTypes.BMP: return ".bmp";
                case FileTypes.TIFF: return ".tiff";
                default: break;
            }
            return "";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDevice
        ///
        /// <summary>
        /// 引数に指定されたファイル形式とグレースケールかどうかを表す値から
        /// 対応する Ghostscript.Devices 列挙型の値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Ghostscript.Devices GetDevice(FileTypes id, bool grayscale) {
            switch (id) {
            case FileTypes.PDF:  return Ghostscript.Devices.PDF;
            case FileTypes.PS:   return Ghostscript.Devices.PS;
            case FileTypes.EPS:  return Ghostscript.Devices.EPS;
            case FileTypes.PNG:  return grayscale ? Ghostscript.Devices.PNG_Gray : Ghostscript.Devices.PNG;
            case FileTypes.JPEG: return grayscale ? Ghostscript.Devices.JPEG_Gray : Ghostscript.Devices.JPEG;
            case FileTypes.BMP:  return grayscale ? Ghostscript.Devices.BMP_Gray : Ghostscript.Devices.BMP;
            case FileTypes.TIFF: return grayscale ? Ghostscript.Devices.TIFF_Gray : Ghostscript.Devices.TIFF;
            default:
                break;
            }
            return Ghostscript.Devices.PDF;
        }

        #endregion
    }
}
