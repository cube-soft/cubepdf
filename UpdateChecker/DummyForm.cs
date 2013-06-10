/* ------------------------------------------------------------------------- */
///
/// DummyForm.cs
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
using System.Windows.Forms;
using System.Collections.Generic;

namespace CubePdfUtility
{
    /* --------------------------------------------------------------------- */
    ///
    /// DummyForm
    /// 
    /// <summary>
    /// タスクトレイに通知アイコンを表示させるためのダミーフォームです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DummyForm : Form
    {
        /* ----------------------------------------------------------------- */
        ///
        /// DummyForm (constructor)
        /// 
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DummyForm()
        {
            InitializeComponent();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DummyForm (constructor)
        /// 
        /// <summary>
        /// サーバからのレスポンスを利用して、オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DummyForm(IDictionary<string, string> response)
            : this()
        {
            UpdateNotifyIcon.Text = response["MESSAGE"];
            UpdateNotifyIcon.BalloonTipText = response["MESSAGE"];
            _url = response["URL"];
            UpdateNotifyIcon.ShowBalloonTip(30000);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// <summary>
        /// 最新バージョンを取得するための Web ページへ移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Run(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(_url); }
            catch (Exception /* err */) { /* Nothing to do */ }
            finally { Exit(sender, e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Exit
        /// 
        /// <summary>
        /// アプリケーションを終了します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Exit(object sender, EventArgs e)
        {
            UpdateNotifyIcon.Visible = false;
            Application.Exit();
        }

        #region Variables
        private string _url = "http://www.cube-soft.jp/cubepdfutility/";
        #endregion
    }
}
