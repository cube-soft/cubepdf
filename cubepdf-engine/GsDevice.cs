/* ------------------------------------------------------------------------- */
/*
 *  GsDevice.cs
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

namespace CubePDF {
    namespace Ghostscript {
        /* ------------------------------------------------------------- */
        //  Device
        /* ------------------------------------------------------------- */
        public enum Device {
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
        };

        /* ------------------------------------------------------------- */
        /*
         *  DeviceExt
         *  
         *  Device から GhostScript のデバイス名を取得するための
         *  拡張メソッド用クラス．
         */
        /* ------------------------------------------------------------- */
        public abstract class DeviceExt {
            public static System.String Argument(Device e) {
                switch (e) {
                    case Device.Unknown:    return "";
                    case Device.PS:         return "-sDEVICE=pswrite";
                    case Device.EPS:        return "-sDEVICE=epswrite";
                    case Device.PDF:        return "-sDEVICE=pdfwrite";
                    case Device.PDF_Opt:    return ""; // 特殊デバイス
                    case Device.SVG:        return "-sDEVICE=svg";
                    case Device.JPEG:       return "-sDEVICE=jpeg";
                    case Device.JPEG_Gray:  return "-sDEVICE=jpeggray";
                    case Device.PNG:        return "-sDEVICE=png16m";
                    case Device.PNG_16:     return "-sDEVICE=png16";
                    case Device.PNG_256:    return "-sDEVICE=png256";
                    case Device.PNG_Gray:   return "-sDEVICE=pnggray";
                    case Device.PNG_Mono:   return "-sDEVICE=pngmono";
                    case Device.PNG_Alpha:  return "-sDEVICE=pngalpha";;
                    case Device.BMP:        return "-sDEVICE=bmp16m";
                    case Device.BMP_16:     return "-sDEVICE=bmp16";
                    case Device.BMP_256:    return "-sDEVICE=bmp256";
                    case Device.BMP_Gray:   return "-sDEVICE=bmpgray";
                    case Device.BMP_Mono:   return "-sDEVICE=bmpmono";
                    case Device.TIFF:       return "-sDEVICE=tiff24nc";
                    case Device.TIFF_Gray:  return "-sDEVICE=tiffgray";
                    case Device.TIFF_Mono:  return "-sDEVICE=tiffcrle";
                    default: throw new ArgumentOutOfRangeException("e");
                }
            }
        };
    } // namespace Ghostscript
} // namespace Cliff
