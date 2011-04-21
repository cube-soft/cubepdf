/* ------------------------------------------------------------------------- */
/*
 *  GsDevice.cs
 *
 *  Copyright (c) 2009 - 2011, CubeSoft, Inc. All rights reserved.
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
        /// Papers
        /* ------------------------------------------------------------- */
        public enum Papers {
            Unknown,
            A3,
            A4,
            A5,
            B3,
            B4,
            B5,
            Ledger,
            Legal,
            Letter,
        };

        /* ------------------------------------------------------------- */
        ///
        /// DeviceExt
        ///
        /// <summary>
        /// Paper から GhostScript の用紙サイズ名を取得するための
        /// 拡張メソッド用クラス．
        /// </summary>
        ///
        /* ------------------------------------------------------------- */
        public abstract class PaperExt {
            public static System.String Argument(Papers e) {
                switch (e) {
                    case Papers.Unknown: return "";
                    case Papers.A3: return "-sPAPERSIZE=a3";
                    case Papers.A4: return "-sPAPERSIZE=a4";
                    case Papers.A5: return "-sPAPERSIZE=a5";
                    case Papers.B3: return "-sPAPERSIZE=b3";
                    case Papers.B4: return "-sPAPERSIZE=b4";
                    case Papers.B5: return "-sPAPERSIZE=b5";
                    case Papers.Ledger: return "-sPAPERSIZE=ledger";
                    case Papers.Legal: return "-sPAPERSIZE=legal";
                    case Papers.Letter: return "-sPAPERSIZE=letter";
                    default: throw new ArgumentOutOfRangeException("e");
                }
            }
        };
    } // namespace Ghostscript
} // namespace CubePDF
