/* ------------------------------------------------------------------------- */
///
/// VersionDialog.cs
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
using System.Windows.Forms;
using Cube.Log;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// VersionDialog
    ///
    /// <summary>
    /// バージョン情報を表示するための Windows フォームクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class VersionDialog : Form
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// VersionDialog (constructor)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionDialog()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => { Close(); };
        }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionDialog (constructor)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionDialog(string version)
            : this()
        {
            var edition = (IntPtr.Size == 4) ? "x86" : "x64";
            VersionLabel.Text = string.Format("Version {0} ({1})", version, edition);
            OSVersionLabel.Text = Environment.OSVersion.ToString();
            DotNetVersionLabel.Text = string.Format(".NET Framework {0}", Environment.Version.ToString());
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// VersionDialog_Shown
        ///
        /// <summary>
        /// ダイアログが表示された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void VersionDialog_Shown(object sender, EventArgs e)
        {
            CloseButton.Focus();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CubePdfLinkLabel_LinkClicked
        ///
        /// <summary>
        /// リンクラベルがクリックされた時に実行されるイベントハンドラです。
        /// CubePDF の Web ページへ移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CubePdfLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var control = sender as LinkLabel;
            try { System.Diagnostics.Process.Start(control.Text); }
            catch (Exception err) { this.LogWarn(err.Message, err); }
        }

        #endregion
    }
}
