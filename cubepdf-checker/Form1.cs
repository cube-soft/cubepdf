using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Container = System.Collections.Generic;

namespace CubePDF {
    public partial class Form1 : Form {
        private string uri_ = "http://www.cube-soft.jp/cubepdf/";

        public Form1() {
            InitializeComponent();
        }

        public Form1(Container.Dictionary<string, string> response) {
            InitializeComponent();
            this.UpdateNotifier.Text = response["MESSAGE"];
            this.UpdateNotifier.BalloonTipText = response["MESSAGE"];
            this.uri_ = response["URL"];
            this.UpdateNotifier.ShowBalloonTip(30000);
        }

        private void ExitMenu_Click(object sender, EventArgs e) {
            this.Exit();
        }

        private void UpdateMenu_Click(object sender, EventArgs e) {
            try {
                System.Diagnostics.Process.Start(this.uri_);
            }
            catch (System.Exception /*err*/) {
                // Nothing to do.
            }
            finally {
                this.Exit();
            }
        }

        private void Exit() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\CubePDF");
            registry.SetValue("LastCheckUpdate", System.DateTime.Now.ToString());

            this.UpdateNotifier.Visible = false;
            Application.Exit();
        }
    }
}
