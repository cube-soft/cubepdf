/* ------------------------------------------------------------------------- */
/*
 *  VersionDialog.cs
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
using System.Windows.Forms;

namespace CubePdf {
    /* --------------------------------------------------------------------- */
    /// VersionDialog
    /* --------------------------------------------------------------------- */
    public partial class VersionDialog : Form {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public VersionDialog(string version) {
            InitializeComponent();
            var edition = (IntPtr.Size == 4) ? "x86" : "x64";
            this.VersionLabel.Text = String.Format("Version: {0} ({1})", version, edition);
        }

        /* ----------------------------------------------------------------- */
        /// VersionDialog_Shown
        /* ----------------------------------------------------------------- */
        private void VersionDialog_Shown(object sender, EventArgs e) {
            OKButton.Focus();
        }

        /* ----------------------------------------------------------------- */
        /// CubePDFLinkLabel_LinkClicked
        /* ----------------------------------------------------------------- */
        private void CubePDFLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(CubePDFLinkLabel.Text);
        }

        /* ----------------------------------------------------------------- */
        /// OKButton_Click
        /* ----------------------------------------------------------------- */
        private void OKButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
