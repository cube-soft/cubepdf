/* ------------------------------------------------------------------------- */
///
/// Program.cs
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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using IoEx = System.IO;

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
            var dir = IoEx.Path.GetDirectoryName(exec.Location);
            var setting = new UserSetting(false);

            SetupLog(args);

            var cmdline = new CubePdf.Settings.CommandLine(args);
            SetupUserSetting(setting, cmdline);
            CheckUpdate(setting);

            if (setting.EmergencyMode) ExecConvert(setting);
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(setting));
            }

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
        private static void SetupUserSetting(UserSetting setting, CubePdf.Settings.CommandLine cmdline)
        {
            try
            {
                if (cmdline.Options.ContainsKey("Language"))
                {
                    Thread.CurrentThread.CurrentUICulture =
                        new System.Globalization.CultureInfo(cmdline.Options["Language"]);
                    Cube.Log.Operations.Debug(typeof(Program), $"SetCulture:{Thread.CurrentThread.CurrentUICulture}");
                }
            }
            catch (Exception err) { Cube.Log.Operations.Warn(typeof(Program), err.Message, err); }

            var docname = cmdline.Options.ContainsKey("DocumentName") ? cmdline.Options["DocumentName"] : "";
            bool is_config = false;
            try
            {
                if (!string.IsNullOrEmpty(docname) &&
                    IoEx.Path.GetExtension(docname) == setting.Extension &&
                    IoEx.File.Exists(docname))
                {
                    is_config = true;
                }
            }
            catch (Exception err)
            {
                // docname に Windows のファイル名に使用できない記号が含まれる
                // 場合に例外が送出されるので、その対策。
                Cube.Log.Operations.Warn(typeof(Program), err.Message, err);
                is_config = false;
            }

            if (is_config) setting.LoadXml(docname);
            else
            {
                LoadUserSetting(setting, cmdline);
                var filename = DocumentName.CreateFileName(docname);
                if (filename != null)
                {
                    string ext = Parameter.GetExtension((Parameter.FileTypes)setting.FileType);
                    filename = IoEx.Path.ChangeExtension(filename, ext);
                    string dir = "";
                    if (setting.OutputPath == String.Empty) dir = setting.LibPath;
                    else
                    {
                        dir = (IoEx.Directory.Exists(setting.OutputPath)) ?
                            setting.OutputPath : IoEx.Path.GetDirectoryName(setting.OutputPath);
                    }
                    setting.OutputPath = dir + '\\' + filename;
                    Cube.Log.Operations.Debug(typeof(Program), setting.OutputPath);
                }
            }

            setting.UserName = cmdline.Options.ContainsKey("UserName") ? cmdline.Options["UserName"] : "";
            setting.InputPath = cmdline.Options.ContainsKey("InputFile") ? cmdline.Options["InputFile"] : "";
            setting.DeleteOnClose = cmdline.Options.ContainsKey("DeleteOnClose");

            if (IoEx.Directory.Exists(setting.LibPath))
                System.Environment.SetEnvironmentVariable("TEMP", setting.LibPath, EnvironmentVariableTarget.Process);
            else Cube.Log.Operations.Debug(typeof(Program), "LibPath Not Found");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadUserSetting
        ///
        /// <summary>
        /// レジストリからユーザ設定をロードします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void LoadUserSetting(UserSetting setting, CubePdf.Settings.CommandLine cmdline)
        {
            if (cmdline.Options.ContainsKey("Em"))
            {
                setting.Load(cmdline.Options["UserName"]);
                setting.EmergencyMode = true;
                setting.PostProcess = Parameter.PostProcesses.OpenFolder;
                
            }
            else setting.Load();
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
        private static void SetupLog(string[] args)
        {
            Cube.Log.Operations.Configure();
            Cube.Log.Operations.Info(typeof(Program), System.Reflection.Assembly.GetEntryAssembly());
            Cube.Log.Operations.Info(typeof(Program), Thread.CurrentThread.CurrentUICulture.ToString());

            var message = new System.Text.StringBuilder();
            message.AppendLine("Arguments");
            foreach (var s in args) message.AppendLine($"\t{s}");
            Cube.Log.Operations.Info(typeof(Program), message.ToString());

        }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckUpdate
        ///
        /// <summary>
        /// アップデートの確認が必要であるかどうかを判断し、必要であれば
        /// 確認用のプログラムを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void CheckUpdate(UserSetting setting)
        {
            try
            {
                if (!setting.CheckUpdate ||
                    string.IsNullOrEmpty(setting.InstallPath) ||
                    DateTime.Now <= setting.LastCheckUpdate.AddDays(1)) return;
                var path = IoEx.Path.Combine(setting.InstallPath, "cubepdf-checker.exe");
                if (IoEx.File.Exists(path)) Process.Start(path);
            }
            catch (Exception err) { Cube.Log.Operations.Warn(typeof(Program), err.Message, err); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        ///
        /// <summary>
        /// 変換処理を実行します。このメソッドは、CubePDF メイン画面が表示
        /// されない設定の場合に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void ExecConvert(UserSetting setting)
        {
            var converter = new Converter();
            converter.Run(setting);
            if (setting.DeleteOnClose && IoEx.File.Exists(setting.InputPath))
            {
                IoEx.File.Delete(setting.InputPath);
            }
        }
    }
}
