using System;
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

            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\CubeSoft\CubePDF");
            var hklm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\CubeSoft\CubePDF", false);
            try {
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
            catch (System.Exception /*e*/) { }
        }
    }
}
