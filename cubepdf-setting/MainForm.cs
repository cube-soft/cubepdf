/* ------------------------------------------------------------------------- */
/*
 *  MainForm.cs
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
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// MainForm
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form {
        /* ----------------------------------------------------------------- */
        //  初期化
        /* ----------------------------------------------------------------- */
        #region Initialize operations

        /* ----------------------------------------------------------------- */
        ///  Constructor
        /* ----------------------------------------------------------------- */
        public MainForm() {
            InitializeComponent();
            InitializeComboAppearance();
            _setting = new UserSetting(true);
            this.LoadSetting(_setting);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitializeComboAppearance
        /// 
        /// <summary>
        /// 各種コンボボックスに表示される文字列を設定する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void InitializeComboAppearance() {
            // FileType
            this.FileTypeCombBox.Items.Clear();
            foreach (Parameter.FileTypes index in Enum.GetValues(typeof(Parameter.FileTypes))) {
                this.FileTypeCombBox.Items.Add(Appearance.FileTypeString(index));
            }
            this.FileTypeCombBox.SelectedIndex = 0;

            // PDFVersion
            this.PDFVersionComboBox.Items.Clear();
            foreach (Parameter.PDFVersions index in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                this.PDFVersionComboBox.Items.Add(Appearance.PDFVersionString(index));
            }
            this.PDFVersionComboBox.SelectedIndex = 0;

            // Resolution
            this.ResolutionComboBox.Items.Clear();
            foreach (Parameter.Resolutions index in Enum.GetValues(typeof(Parameter.Resolutions))) {
                this.ResolutionComboBox.Items.Add(Appearance.ResolutionString(index));
            }
            this.ResolutionComboBox.SelectedIndex = 0;

            // ExistedFile
            this.ExistedFileComboBox.Items.Clear();
            foreach (Parameter.ExistedFiles index in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                this.ExistedFileComboBox.Items.Add(Appearance.ExistedFileString(index));
            }
            this.ExistedFileComboBox.SelectedIndex = 0;

            // PostProcess
            this.PostProcessComboBox.Items.Clear();
            foreach (Parameter.PostProcesses index in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                this.PostProcessComboBox.Items.Add(Appearance.PostProcessString(index));
            }
            this.PostProcessComboBox.SelectedIndex = 0;

            // Advance モードに設定されていない PostProcess
            this.PostProcessLiteComboBox.Items.Clear();
            this.PostProcessLiteComboBox.Items.Add(Appearance.PostProcessString(Parameter.PostProcesses.Open));
            this.PostProcessLiteComboBox.Items.Add(Appearance.PostProcessString(Parameter.PostProcesses.None));
            this.PostProcessLiteComboBox.SelectedIndex = 0;
            
            // DownSampling
            this.DownSamplingComboBox.Items.Clear();
            foreach (Parameter.DownSamplings index in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                this.DownSamplingComboBox.Items.Add(Appearance.DownSamplingString(index));
            }
            this.DownSamplingComboBox.SelectedIndex = 0;
        }
        
        #endregion

        /* ----------------------------------------------------------------- */
        //  UserSetting と GUI コンポーネントの同期
        /* ----------------------------------------------------------------- */
        #region Synchronize user settings

        /* ----------------------------------------------------------------- */
        ///
        /// LoadSetting
        /// 
        /// <summary>
        /// UserSetting の情報を各種 GUI コンポーネントに反映する．
        /// TODO: 仮想プリンタ経由の場合は InputPath にその値を設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadSetting(UserSetting setting) {
            this.UserProgramTextBox.Text = setting.UserProgram;

            // コンボボックスのインデックス関連
            this.FileTypeCombBox.SelectedIndex = setting.FileType;
            this.PDFVersionComboBox.SelectedIndex = setting.PDFVersion;
            this.ResolutionComboBox.SelectedIndex = setting.Resolution;
            this.ExistedFileComboBox.SelectedIndex = setting.ExistedFile;
            this.PostProcessComboBox.SelectedIndex = setting.PostProcess;
            this.DownSamplingComboBox.SelectedIndex = setting.DownSampling;

            // チェックボックスのフラグ関連
            this.PageLotationCheckBox.Checked = setting.PageRotation;
            this.EmbedFontCheckBox.Checked = setting.EmbedFont;
            this.GrayscaleCheckBox.Checked = setting.Grayscale;
            this.WebOptimizeCheckBox.Checked = setting.WebOptimize;
            this.SaveSettingCheckBox.Checked = setting.SaveSetting;
            this.UpdateCheckBox.Checked = setting.CheckUpdate;

            // ポストプロセス関連
            _postproc = setting.AdvancedMode ? this.PostProcessComboBox : this.PostProcessLiteComboBox;
            this._PostProcessPanel.Enabled = setting.AdvancedMode;
            this._PostProcessPanel.Visible = setting.AdvancedMode;
            this._PostProcessLabel.Visible = setting.AdvancedMode;
            this.PostProcessLiteComboBox.Enabled = !setting.AdvancedMode;
            this.PostProcessLiteComboBox.Visible = !setting.AdvancedMode;
            this._PostProcessLiteLabel.Visible = !setting.AdvancedMode;

            // 入力パスを選択可能にするかどうか
            this._InputpathPanel.Enabled = setting.SelectInputFile;
            this._InputpathPanel.Visible = setting.SelectInputFile;
            this._InputPathLabel.Visible = setting.SelectInputFile;

            // 入出力フォルダを表示
            this.InputPathTextBox.Text = _setting.InputPath;
            this.OutputPathTextBox.Text = _setting.OutputPath;

            // バージョンを表示
            var edition = (IntPtr.Size == 4) ? "x86" : "x64";
            this._VersionLabel.Text = String.Format("Version: {0} ({1})", _setting.Version, edition);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveSetting
        /// 
        /// <summary>
        /// 各種 GUI コンポーネントの情報を UserSetting に反映する．
        /// 
        /// TODO: パーミッションの保存．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveSetting(UserSetting setting, bool save_input) {
            setting.OutputPath = this.OutputPathTextBox.Text;
            if (save_input) setting.InputPath = this.InputPathTextBox.Text;
            setting.UserProgram = this.UserProgramTextBox.Text;

            // コンボボックスのインデックス関連
            setting.FileType = this.FileTypeCombBox.SelectedIndex;
            setting.PDFVersion = this.PDFVersionComboBox.SelectedIndex;
            setting.Resolution = this.ResolutionComboBox.SelectedIndex;
            setting.ExistedFile = this.ExistedFileComboBox.SelectedIndex;
            setting.PostProcess = _postproc.SelectedIndex;
            setting.DownSampling = this.DownSamplingComboBox.SelectedIndex;

            // チェックボックスのフラグ関連
            setting.PageRotation = this.PageLotationCheckBox.Checked;
            setting.EmbedFont = this.EmbedFontCheckBox.Checked;
            setting.Grayscale = this.GrayscaleCheckBox.Checked;
            setting.WebOptimize = this.WebOptimizeCheckBox.Checked;
            setting.SaveSetting = this.SaveSettingCheckBox.Checked;
            setting.CheckUpdate = this.UpdateCheckBox.Checked;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メインフォームのイベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region Main form event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_Shown
        ///  
        /// <summary>
        /// CubePDF のメイン画面が起動した際に他のウィンドウに隠れてしまう
        /// 場合があるのでその対策．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_Shown(object sender, EventArgs e) {
            this.Activate();
            this.TopMost = true;
            this.TopMost = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Form_KeyDown
        ///
        /// <summary>
        /// エンターキーに「変換」ボタンを対応させる．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) this.ConvertButton_Click(this.ConvertButton, e);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  各種ボタンのイベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region Button event handlers

        /* ----------------------------------------------------------------- */
        ///  ConvertButton_Click
        /* ----------------------------------------------------------------- */
        private void ConvertButton_Click(object sender, EventArgs e) {
            this.SaveSetting(_setting, this._InputpathPanel.Enabled);
            _setting.Save();
            this.Close();
        }

        /* ----------------------------------------------------------------- */
        ///  ExitButton_Click
        /* ----------------------------------------------------------------- */
        private void ExitButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        /* ----------------------------------------------------------------- */
        /// OutputPathButton_Click
        /* ----------------------------------------------------------------- */
        private void OutputPathButton_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = Appearance.FileFilterString();
            dialog.FilterIndex = this.FileTypeCombBox.SelectedIndex + 1;
            dialog.OverwritePrompt = false;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            // NOTE: ドット付ファイル名を指定された場合にどうするか検討する．
            this.OutputPathTextBox.Text = dialog.FileName;
            if (dialog.FilterIndex != 0 && dialog.FilterIndex - 1 != this.FileTypeCombBox.SelectedIndex) {
                this.FileTypeCombBox.SelectedIndex = dialog.FilterIndex - 1;
            }
        }

        /* ----------------------------------------------------------------- */
        /// InputPathButton_Click
        /* ----------------------------------------------------------------- */
        private void InputPathButton_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = Properties.Settings.Default.InputPathFilter;
            if (this.InputPathTextBox.Text.Length > 0) dialog.FileName = this.InputPathTextBox.Text;
            if (dialog.ShowDialog() != DialogResult.OK) return;
            
            this.InputPathTextBox.Text = dialog.FileName;
        }

        /* ----------------------------------------------------------------- */
        /// UserProgramButton_Click
        /* ----------------------------------------------------------------- */
        private void UserProgramButton_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = Properties.Settings.Default.UserProgramFilter;
            if (this.UserProgramTextBox.Text.Length > 0) dialog.FileName = this.UserProgramTextBox.Text;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            this.UserProgramTextBox.Text = dialog.FileName;
        }

        /* ----------------------------------------------------------------- */
        /// CubePDFLinkLabel_LinkClicked
        /* ----------------------------------------------------------------- */
        private void CubePDFLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Control control = sender as Control;
            if (control == null) return;
            System.Diagnostics.Process.Start(control.Text);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  各種コンボボックスのイベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region Combobox event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// FileTypeCombBox_SelectedIndexChanged
        /// 
        /// <summary>
        /// ファイルタイプによって無効なオプションがあるため，有効/無効
        /// を切り替える．ファイルタイプに依存するオプションは以下の通り:
        /// 
        /// PDF, PS, EPS, SVG: フォントの埋め込み
        /// PDF: バージョン, 文書プロパティ，セキュリティ，Web 最適化
        /// BMP, JPEG, PNG: 解像度
        /// 
        /// NOTE: Ghostscript のバグで現在のところ PS, EPS, SVG は
        /// グレースケール設定ができない．また，フォント埋め込みを無効に
        /// すると文字化けが発生するので，暫定的に無効にしておく．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void FileTypeCombBox_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox control = sender as ComboBox;
            if (control == null) return;

            Parameter.FileTypes index = (Parameter.FileTypes)control.SelectedIndex;
            bool is_pdf = (index == Parameter.FileTypes.PDF);
            bool is_bitmap = (index == Parameter.FileTypes.BMP || index == Parameter.FileTypes.JPEG || index == Parameter.FileTypes.PNG);
            bool is_grayscale = !(index == Parameter.FileTypes.PS || index == Parameter.FileTypes.EPS || index == Parameter.FileTypes.SVG);
            
            this.PDFVersionComboBox.Enabled = is_pdf;
            this.ResolutionComboBox.Enabled = is_bitmap;
            this.EmbedFontCheckBox.Enabled = false; // 本来は !is_bitmap;
            this.GrayscaleCheckBox.Enabled = is_grayscale;
            this.WebOptimizeCheckBox.Enabled = is_pdf;

            // 出力パスの拡張子を変更後のファイルタイプに合わせる．
            if (this.OutputPathTextBox.Text.Length == 0) return;
            this.OutputPathTextBox.Text = Path.ChangeExtension(this.OutputPathTextBox.Text, Parameter.Extension(index));
        }

        /* ----------------------------------------------------------------- */
        /// PostProcessComboBox_SelectedIndexChanged
        /* ----------------------------------------------------------------- */
        private void PostProcessComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox control = sender as ComboBox;
            if (control == null) return;

            Parameter.PostProcesses index = (Parameter.PostProcesses)control.SelectedIndex;
            bool is_user_program = (index == Parameter.PostProcesses.UserProgram);

            this.UserProgramTextBox.Enabled = is_user_program;
            this.UserProgramButton.Enabled = is_user_program;
        }
        
        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private UserSetting _setting;
        private ComboBox _postproc;
        #endregion
    }
}
