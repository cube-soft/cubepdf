/* ------------------------------------------------------------------------- */
/*
 *  GsDevice.cs
 *
 *  Copyright (c) 2009 - 2011, clown. All rights reserved.
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
        public enum Paper {
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
        /*
         *  DeviceExt
         *  
         *  Paper から GhostScript の用紙サイズ名を取得するための
         *  拡張メソッド用クラス．
         */
        /* ------------------------------------------------------------- */
        public abstract class PaperExt {
            public static System.String Argument(Paper e) {
                switch (e) {
                    case Paper.Unknown: return "";
                    case Paper.A3: return "-sPAPERSIZE=a3";
                    case Paper.A4: return "-sPAPERSIZE=a4";
                    case Paper.A5: return "-sPAPERSIZE=a5";
                    case Paper.B3: return "-sPAPERSIZE=b3";
                    case Paper.B4: return "-sPAPERSIZE=b4";
                    case Paper.B5: return "-sPAPERSIZE=b5";
                    case Paper.Ledger: return "-sPAPERSIZE=ledger";
                    case Paper.Legal: return "-sPAPERSIZE=legal";
                    case Paper.Letter: return "-sPAPERSIZE=letter";
                    default: throw new ArgumentOutOfRangeException("e");
                }
            }
        };
    } // namespace Ghostscript
} // namespace Cliff
