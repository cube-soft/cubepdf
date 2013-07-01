/* ------------------------------------------------------------------------- */
///
/// Program.cs
///
/// Copyright (c) 2013 CubeSoft, Inc. All rights reserved.
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
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CubePdfUtility
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
        /* ----------------------------------------------------------------- */
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledException);

            try
            {
                var checker = new CubePdf.Settings.UpdateChecker(@"Software\CubeSoft\CubePDF\v2");
                checker.ProductName = "cubepdf";
                checker.CheckInterval = 1; // [day]

                if (args.Length > 0 && args[0] == "install") checker.Notify();
                else
                {
                    var response = checker.GetResponse();
                    if (response != null &&
                        response.ContainsKey("UPDATE") && response["UPDATE"] == "1" &&
                        response.ContainsKey("MESSAGE") &&
                        response.ContainsKey("URL"))
                    {
                        new DummyForm(response);
                        Application.Run();
                    }
                }
            }
            catch (Exception err) { Trace.TraceError(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Application_ThreadException
        /// 
        /// <summary>
        /// 未処理例外をキャッチするイベントハンドラです。
        /// （Windowsアプリケーション用）
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Trace.TraceError(e.Exception.ToString());
            return;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Application_ThreadException
        ///
        /// <summary>
        /// 未処理例外をキャッチするイベントハンドラです。
        /// （主にコンソール・アプリケーション用）
        /// </summary>
        ///         
        /* ----------------------------------------------------------------- */
        public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.TraceError("UnhandledException was occured");
            return;
        }
    }
}
