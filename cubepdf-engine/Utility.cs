/* ------------------------------------------------------------------------- */
/*
 *  PdfUtility.cs
 *
 *  Copyright (c) 2009 - 2010, CubeSoft, Inc. All rights reserved.
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
 *  Last-modified: Mon 05 Apr 2010 20:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Container = System.Collections.Generic;

namespace CubePDF {
    public abstract class Utility {
        /* ------------------------------------------------------------- */
        /// WorkingDirectory
        /* ------------------------------------------------------------- */
        public static string WorkingDirectory {
            get { return _work; }
            set { _work = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CurrentDirectory
        ///
        /// <summary>
        /// 現在の実行ディレクトリへのパスを返す．GetEntryAssembly
        /// メソッドが失敗した場合には，CurrentDirectory 環境変数の値を
        /// 返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string CurrentDirectory {
            get {
                var exec = System.Reflection.Assembly.GetEntryAssembly();
                if (exec != null) return System.IO.Path.GetDirectoryName(exec.Location);
                else return System.Environment.CurrentDirectory;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsAssociate
        ///
        /// <summary>
        /// 引数に指定された拡張子が関連付けされているかどうかチェックする。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsAssociate(string ext)
        {
            uint size = 0;
            AssocQueryString(0x40 /* ASSOCF_VERIFY */, 2 /* ASSOCSTR_EXECUTABLE */, ext, null, null, ref size);
            return size > 0;
        }

        /* ----------------------------------------------------------------- */
        //  Win32 APIs
        /* ----------------------------------------------------------------- */
        #region Win32APIs

        /* ----------------------------------------------------------------- */
        ///
        /// AssocQueryString
        ///
        /// <summary>
        /// NOTE: 本来、引数の flags は ASSOCF、str は ASSOCSTR と言う
        /// enum 型で定義される。
        /// 
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773471.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint AssocQueryString(uint flags, uint str, string pszAssoc, string pszExtra, System.Text.StringBuilder pszOut, ref uint pcchOut);

        #endregion
        
        /* ------------------------------------------------------------- */
        //  変数定義
        /* ------------------------------------------------------------- */
        #region Variables
        private static string _work = System.IO.Path.GetTempPath();
        #endregion
    }
} // namespace CubePDF
