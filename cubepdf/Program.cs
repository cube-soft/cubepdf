/* ------------------------------------------------------------------------- */
/*
 *  Program.cs
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
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CubePDF
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var exec = System.Reflection.Assembly.GetEntryAssembly();
            var dir = System.IO.Path.GetDirectoryName(exec.Location);
            SetupLog(dir + @"\cubepdf.log");
            Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf.exe start");
            Trace.WriteLine(DateTime.Now.ToString() + ": Arguments:");
            foreach (var s in args) Trace.WriteLine("\t" + s);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0) Application.Run(new MainForm(args));
            else Application.Run(new MainForm());

            Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf.exe end");
            Trace.Close();
        }

        /* ----------------------------------------------------------------- */
        /// SetupLog
        /* ----------------------------------------------------------------- */
        private static void SetupLog(string src) {
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
        }
    }
}
