/* ------------------------------------------------------------------------- */
///
/// Program.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CubePdf
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
            var setting = new UserSetting(false);

            SetupLog(dir + @"\cubepdf.log");
            Trace.WriteLine(String.Format("{0} [INFO] CubePDF version {1} ({2})", DateTime.Now.ToString(), setting.Version, ((IntPtr.Size == 4) ? "x86" : "x64")));
            Trace.WriteLine(String.Format("{0} [INFO] Arguments", DateTime.Now.ToString()));
            foreach (var s in args) Trace.WriteLine("\t" + s);

            SetupUserSetting(setting, args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(setting));

            Trace.Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetupUserSetting
        ///
        /// <summary>
        /// プログラムに指定された引数等を用いて、UserSetting オブジェクトを
        /// 初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetupUserSetting(UserSetting setting, string[] args)
        {
            var cmdline = new CommandLine(args);

            var docname = cmdline.Arguments.ContainsKey("DocumentName") ? cmdline.Arguments["DocumentName"] : "";
            bool is_config = false;
            try
            {
                if (docname.Length > 0 && Path.GetExtension(docname) == setting.Extension && File.Exists(docname)) is_config = true;
            }
            catch (Exception /* err */)
            {
                // docname に Windows のファイル名に使用できない記号が含まれる
                // 場合に例外が送出されるので、その対策。
                is_config = false;
            }

            if (is_config) setting.Load(docname);
            else
            {
                setting.Load();
                var filename = FileNameModifier.GetFileName(docname);
                if (filename != null)
                {
                    string ext = Parameter.Extension((Parameter.FileTypes)setting.FileType);
                    filename = Path.ChangeExtension(filename, ext);
                    string dir = (setting.OutputPath.Length == 0 || Directory.Exists(setting.OutputPath)) ?
                        setting.OutputPath : Path.GetDirectoryName(setting.OutputPath);
                    setting.OutputPath = dir + '\\' + filename;
                }
            }

            setting.InputPath = cmdline.Arguments.ContainsKey("InputFile") ? cmdline.Arguments["InputFile"] : "";
            setting.DeleteOnClose = cmdline.Arguments.ContainsKey("DeleteOnClose");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetupLog
        ///
        /// <summary>
        /// トレースログを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetupLog(string src)
        {
            try
            {
                if (System.IO.File.Exists(src))
                {
                    using (System.IO.StreamWriter stream = new System.IO.StreamWriter(src, false))
                    {
                        // 空の内容で上書きする
                    }
                }

                Trace.Listeners.Remove("Default");
                Trace.Listeners.Add(new TextWriterTraceListener(src));
                Trace.AutoFlush = true;
            }
            catch (Exception /* err */) { }
        }
    }
}
