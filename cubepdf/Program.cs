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
        /* ----------------------------------------------------------------- */
        ///
        /// Main
        /// 
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [STAThread]
        static void Main(string[] args)
        {
            var exec = System.Reflection.Assembly.GetEntryAssembly();
            var dir = System.IO.Path.GetDirectoryName(exec.Location);
            SetupLog(dir + @"\cubepdf.log");
            Trace.WriteLine(DateTime.Now.ToString() + ": Arguments:");
            foreach (var s in args) Trace.WriteLine("\t" + s);

            var setting = new UserSetting(true);
            SetupUserSetting(setting, args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(setting));

            Trace.Close();
        }

        /* ----------------------------------------------------------------- */
        /// SetupUserSetting
        /* ----------------------------------------------------------------- */
        private static void SetupUserSetting(UserSetting setting, string[] args)
        {
            var cmdline = new CommandLine(args);
            var filename = FileNameModifier.GetFileName(cmdline.Arguments.ContainsKey("DocumentName") ? cmdline.Arguments["DocumentName"] : "");
            if (filename != null) {
                string ext = Parameter.Extension((Parameter.FileTypes)setting.FileType);
                filename = System.IO.Path.ChangeExtension(filename, ext);
                string dir = (setting.OutputPath.Length == 0 || System.IO.Directory.Exists(setting.OutputPath)) ?
                    setting.OutputPath : System.IO.Path.GetDirectoryName(setting.OutputPath);
                setting.OutputPath = dir + '\\' + filename;
            }

            setting.InputPath = cmdline.Arguments.ContainsKey("InputFile") ? cmdline.Arguments["InputFile"] : "";
            setting.DeleteOnClose = cmdline.Arguments.ContainsKey("DeleteOnClose");
        }

        /* ----------------------------------------------------------------- */
        /// SetupLog
        /* ----------------------------------------------------------------- */
        private static void SetupLog(string src) {
            if (System.IO.File.Exists(src)) System.IO.File.Delete(src);
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
        }
    }
}
