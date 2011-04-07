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
        public static string WorkingDirectory() {
            return System.Environment.GetEnvironmentVariable("windir") + @"\CubePDF\";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CurrentDirectory
        ///
        /// <summary>
        /// 現在の実行ディレクトリへのパスを返す．
        /// </summary>
        ///
        /// <returns>現在の実行ディレクトリへのパス</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string CurrentDirectory() {
            var exec = System.Reflection.Assembly.GetEntryAssembly();
            return System.IO.Path.GetDirectoryName(exec.Location) + '\\';
        }

        /* ------------------------------------------------------------- */
        /// SetupLog
        /* ------------------------------------------------------------- */
        public static void SetupLog(string src) {
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
        }
    }
} // namespace CubePDF
