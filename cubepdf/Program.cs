using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Cryptography;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Certify()) {
                if (args.Length > 0) {
                    Application.Run(new MainForm(args[0]));
                }
                else {
                    Application.Run(new MainForm());
                }
            }
            else {
                if (MessageBox.Show(
                        Properties.Settings.Default.ERROR_CERTIFY,
                        "Certification error",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes
                    ) {
                    try {
                        System.Diagnostics.Process.Start("http://www.cube-soft.jp/cubepdf/");
                    }
                    catch (System.Exception /*err*/) { }
                }
            }
        }

        private static bool Certify() {
            try {
                var registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\CubePDF", false);
                var password = "www.cube-soft.jp";
                string encrypt = (string)registry.GetValue("InstallKey");
                if (encrypt == null) return true; // NOTE: 暫定処理
                else {
                    string limit = Cube.DES.Decrypt(encrypt, password);
                    if (System.DateTime.Now > System.DateTime.Parse(limit).AddMonths(6)) return false;
                    else return true;
                }
            }
            catch (System.Exception /*e*/) {
                return true; // NOTE: 暫定処理
            }
        }
    }
}
