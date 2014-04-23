/* ------------------------------------------------------------------------- */
///
/// Ghostscript/Device.cs
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

namespace CubePdf.Ghostscript {
    /* --------------------------------------------------------------------- */
    ///
    /// Devices
    ///
    /// <summary>
    /// Ghostscript で変換可能なデバイスを定義した enum 型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Devices
    {
        Unknown = 0,
        PS,
        EPS,
        PDF,
        PDF_Opt,
        SVG,
        JPEG,
        JPEG_Gray,
        PNG,
        PNG_Alpha,
        PNG_256,
        PNG_16,
        PNG_Gray,
        PNG_Mono,
        BMP,
        BMP_256,
        BMP_16,
        BMP_Gray,
        BMP_Mono,
        TIFF,
        TIFF_Gray,
        TIFF_Mono,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// DeviceExt
    ///
    /// <summary>
    /// Devices 型から GhostScript のデバイス名を取得するための拡張メソッド
    /// 用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class DeviceExt
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Argument
        /// 
        /// <summary>
        /// Devices の各値に対応する Ghostscript に指定する引数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string Argument(Devices e)
        {
            switch (e)
            {
                case Devices.Unknown: return "";
                case Devices.PS: return "-sDEVICE=ps2write";
                case Devices.EPS: return "-sDEVICE=eps2write";
                case Devices.PDF: return "-sDEVICE=pdfwrite";
                case Devices.PDF_Opt: return ""; // 特殊デバイス
                case Devices.SVG: return "-sDEVICE=svg";
                case Devices.JPEG: return "-sDEVICE=jpeg";
                case Devices.JPEG_Gray: return "-sDEVICE=jpeggray";
                case Devices.PNG: return "-sDEVICE=png16m";
                case Devices.PNG_16: return "-sDEVICE=png16";
                case Devices.PNG_256: return "-sDEVICE=png256";
                case Devices.PNG_Gray: return "-sDEVICE=pnggray";
                case Devices.PNG_Mono: return "-sDEVICE=pngmono";
                case Devices.PNG_Alpha: return "-sDEVICE=pngalpha"; ;
                case Devices.BMP: return "-sDEVICE=bmp16m";
                case Devices.BMP_16: return "-sDEVICE=bmp16";
                case Devices.BMP_256: return "-sDEVICE=bmp256";
                case Devices.BMP_Gray: return "-sDEVICE=bmpgray";
                case Devices.BMP_Mono: return "-sDEVICE=bmpmono";
                case Devices.TIFF: return "-sDEVICE=tiff24nc";
                case Devices.TIFF_Gray: return "-sDEVICE=tiffgray";
                case Devices.TIFF_Mono: return "-sDEVICE=tiffcrle";
                default: throw new ArgumentOutOfRangeException("e");
            }
        }
    }
} // namespace CubePDF
