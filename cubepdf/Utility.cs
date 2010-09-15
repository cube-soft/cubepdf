/* ------------------------------------------------------------------------- */
/*
 *  Utility.cs
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
 *
 *  Last-modified: Sat 18 Jul 2010 00:21:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    //  Utility
    /* --------------------------------------------------------------------- */
    public abstract class Utility {        
        /* ----------------------------------------------------------------- */
        ///
        /// UTF8ToUnicode
        ///
        /// <summary>
        /// UTF-8からUnicode(UTF-16LE)へ変換する．
        /// </summary>
        /// <returns>変換後のバイト配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static byte[] UTF8ToUnicode(System.String src) {
            Encoding utf8 = Encoding.UTF8;
            byte[] buffer = utf8.GetBytes(src);
            Encoding unicode = Encoding.GetEncoding("unicodeFFFE");
            return Encoding.Convert(utf8, unicode, buffer);
        }
    }
}
