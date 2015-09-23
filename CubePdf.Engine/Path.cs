/* ------------------------------------------------------------------------- */
///
/// Path.cs
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
using System.Runtime.InteropServices;
using IoEx = System.IO;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// Path
    /// 
    /// <summary>
    /// CubePDF プロジェクトで使用するパスに関するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Path
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
            get { return _WorkingDirectory; }
            set { _WorkingDirectory = value; }
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
                if (exec != null) return IoEx.Path.GetDirectoryName(exec.Location);
                else return System.Environment.CurrentDirectory;
            }
        }

        #region Variables
        private static string _WorkingDirectory = IoEx.Path.GetTempPath();
        #endregion
    }
}
