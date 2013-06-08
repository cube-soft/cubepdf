/* ------------------------------------------------------------------------- */
/*
 *  Utility.cs
 *
 *  Copyright (c) 2009, CubeSoft, Inc.
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
using System.Runtime.InteropServices;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// Utility
    /// 
    /// <summary>
    /// 雑多な補助メソッドを定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Utility
    {
        /* ------------------------------------------------------------- */
        ///
        /// WorkingDirectory
        ///
        /// <summary>
        /// CubePDF の作業用ディレクトリへのパスを取得、または設定します。
        /// </summary>
        ///
        /* ------------------------------------------------------------- */
        public static string WorkingDirectory
        {
            get { return _work; }
            set { _work = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CurrentDirectory
        ///
        /// <summary>
        /// 現在の実行ディレクトリへのパスを返します。
        /// </summary>
        /// 
        /// <remarks>
        /// GetEntryAssembly メソッドが失敗した場合には、CurrentDirectory
        /// 環境変数の値を返す事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static string CurrentDirectory
        {
            get
            {
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
        /// 引数に指定された拡張子が関連付けされているかどうかを確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsAssociate(string ext)
        {
            IntPtr key = IntPtr.Zero;
            var status = AssocQueryKey(0x40 /* ASSOCF_VERIFY */, 1 /* ASSOCKEY_SHELLEXECCLASS */, ext, "open", out key);
            return status == 0;
        }

        #region Win32APIs

        /* ----------------------------------------------------------------- */
        ///
        /// AssocQueryKey
        ///
        /// <remarks>
        /// 本来、引数の flags は ASSOCF、key は ASSOCKEY と言う enum 型で
        /// 定義される模様。
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773468.aspx
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint AssocQueryKey(uint flags, uint key, string pszAssoc, string pszExtra, out IntPtr phkeyOut);

        #endregion

        #region Variables
        private static string _work = System.IO.Path.GetTempPath();
        #endregion
    }
} // namespace CubePdf
