using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CubePDF {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                //MessageBox.Show("ネットワークに接続されていないため、アップデートを確認できませんでした。", "CubePDF Update Checker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledException);

            try {
                var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\CubeSoft\CubePDF");
                var hklm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\CubeSoft\CubePDF", false);
                var updater = new Cube.Updater("www.cube-soft.jp");
                string version = (string)hklm.GetValue("Version", "1.0.0");
                if (args.Length > 0 && args[0] == "install") {
                    updater.Parse("cubepdf", version, true);
                }
                else {
                    string last = (string)registry.GetValue("LastCheckUpdate");
                    if (last == null || System.DateTime.Now > System.DateTime.Parse(last).AddDays(1)) {
                        var response = updater.Parse("cubepdf", version, false);
                        if (response != null &&
                            response.ContainsKey("UPDATE") && response["UPDATE"] == "1" &&
                            response.ContainsKey("MESSAGE") &&
                            response.ContainsKey("URL")) {
                            new Form1(response);
                            Application.Run();
                        }
                    }
                }
            }
            catch (System.Exception /* err */) {
                //MessageBox.Show("Exception");
                return;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Application_ThreadException
        /// 
        /// <summary>
        /// 未処理例外をキャッチするイベント・ハンドラ
        /// （Windowsアプリケーション用）
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            //MessageBox.Show("Application_ThreadException");
            return;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Application_ThreadException
        ///
        /// <summary>
        /// 未処理例外をキャッチするイベント・ハンドラ（主にコンソール・アプリケーション用）
        /// </summary>
        ///         
        /* ----------------------------------------------------------------- */
        public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            //MessageBox.Show("Application_UnhandledException");
            return;
        }
    }
}
