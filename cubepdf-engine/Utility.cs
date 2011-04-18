/* ------------------------------------------------------------------------- */
/*
 *  PdfUtility.cs
 *
 *  Copyright (c) 2009 - 2010, clown. All rights reserved.
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

        /* ------------------------------------------------------------- */
        /// SetupLog
        /* ------------------------------------------------------------- */
        public static void SetupLog(string src) {
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
        }

        /* ------------------------------------------------------------- */
        //  変数定義
        /* ------------------------------------------------------------- */
        #region Variables
        private static string _work = System.IO.Path.GetTempPath();
        #endregion
    }
} // namespace CubePDF
