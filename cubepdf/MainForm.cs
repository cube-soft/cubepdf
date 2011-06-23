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
using System.Diagnostics;
using System.Collections.Generic;
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
            this.Initialize(null);
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  Constructor
        ///  
        /// <summary>
        /// 仮想プリンタ経由で実行された場合のコンストラクタ．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public MainForm(string[] args) {
            this.Initialize(args);
        }

        /* ----------------------------------------------------------------- */
        /// Initialize
        /* ----------------------------------------------------------------- */
        private void Initialize(string[] args) {
            InitializeComponent();
            InitializeComboAppearance();
            _setting = new UserSetting(true);
            this.UpgradeSetting(_setting);
            this.LoadSetting(_setting);

            // 入力パスの初期化
            if (args != null && args.Length > 0 && File.Exists(args[0])) {
                this.InputPathTextBox.Text = args[0];
                this._InputpathPanel.Enabled = false;
            }

            // 出力パスの初期化
            string filename = Environment.GetEnvironmentVariable("REDMON_FILENAME");
            if (filename == null && args != null && args.Length > 1) filename = args[1];
            if (filename != null) {
                string ext = Parameter.Extension((Parameter.FileTypes)_setting.FileType);
                filename = Path.ChangeExtension(filename, ext);
                string dir = (_setting.OutputPath.Length == 0 || Directory.Exists(_setting.OutputPath)) ?
                    _setting.OutputPath : Path.GetDirectoryName(_setting.OutputPath);
                this.OutputPathTextBox.Text = dir + '\\' + filename;
            }
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
            foreach (Parameter.FileTypes id in Enum.GetValues(typeof(Parameter.FileTypes))) {
                this.FileTypeCombBox.Items.Add(Appearance.FileTypeString(id));
            }
            this.FileTypeCombBox.SelectedIndex = 0;

            // PDFVersion
            this.PDFVersionComboBox.Items.Clear();
            foreach (Parameter.PDFVersions id in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                this.PDFVersionComboBox.Items.Add(Appearance.PDFVersionString(id));
            }
            this.PDFVersionComboBox.SelectedIndex = 0;

            // Resolution
            this.ResolutionComboBox.Items.Clear();
            foreach (Parameter.Resolutions id in Enum.GetValues(typeof(Parameter.Resolutions))) {
                this.ResolutionComboBox.Items.Add(Appearance.ResolutionString(id));
            }
            this.ResolutionComboBox.SelectedIndex = 0;

            // ExistedFile
            this.ExistedFileComboBox.Items.Clear();
            foreach (Parameter.ExistedFiles id in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                this.ExistedFileComboBox.Items.Add(Appearance.ExistedFileString(id));
            }
            this.ExistedFileComboBox.SelectedIndex = 0;

            // PostProcess
            this.PostProcessComboBox.Items.Clear();
            foreach (Parameter.PostProcesses id in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                this.PostProcessComboBox.Items.Add(Appearance.PostProcessString(id));
            }
            this.PostProcessComboBox.SelectedIndex = 0;

            // Advance モードに設定されていない PostProcess
            this.PostProcessLiteComboBox.Items.Clear();
            this.PostProcessLiteComboBox.Items.Add(Appearance.PostProcessString(Parameter.PostProcesses.Open));
            this.PostProcessLiteComboBox.Items.Add(Appearance.PostProcessString(Parameter.PostProcesses.None));
            this.PostProcessLiteComboBox.SelectedIndex = 0;
            
            // DownSampling
            this.DownSamplingComboBox.Items.Clear();
            foreach (Parameter.DownSamplings id in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                this.DownSamplingComboBox.Items.Add(Appearance.DownSamplingString(id));
            }
            this.DownSamplingComboBox.SelectedIndex = 0;
        }
        
        #endregion
        
        /* ----------------------------------------------------------------- */
        //  チェックメソッド
        /* ----------------------------------------------------------------- */
        #region Checking methods
        
        /* ----------------------------------------------------------------- */
        ///
        /// CheckPassword
        ///
        /// <summary>
        /// パスワードダイアログと確認ダイアログの入力値が一致しない場合に
        /// エラーメッセージを表示する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CheckPassword(bool enabled, string password, string confirm) {
            bool status = true;
            if (enabled) status = (password == confirm);
            if (!status) {
                MessageBox.Show(
                    Properties.Settings.Default.PasswordUnmatched,
                    Properties.Settings.Default.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.MainTabControl.SelectedTab = this.SecurityTabPage;
            }
            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckOutput
        /// 
        /// <summary>
        /// 出力先パスが正しいかどうかを判別する．出力先パスが指定されて
        /// いない場合はエラーメッセージを表示する．また，指定された出力先
        /// パスが既に存在する場合には確認ダイアログを表示する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CheckOutput(string path, int do_existed_file) {
            if (path.Length == 0) {
                MessageBox.Show(
                    Properties.Settings.Default.FileNotSpecified,
                    Properties.Settings.Default.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
            else if (File.Exists(path) && Translator.IndexToExistedFile(this.ExistedFileComboBox.SelectedIndex) != Parameter.ExistedFiles.Rename) {
                // {0} は既に存在します。{1}しますか？
                string message = String.Format(Properties.Settings.Default.FileExists,
                    path, Appearance.ExistedFileString((Parameter.ExistedFiles)do_existed_file));
                if (MessageBox.Show(
                        message,
                        Properties.Settings.Default.OverwritePrompt,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.Cancel) {
                    return false;
                }
            }
            return true;
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
            this.FileTypeCombBox.SelectedIndex      = Translator.FileTypeToIndex(setting.FileType);
            this.PDFVersionComboBox.SelectedIndex   = Translator.PDFVersionToIndex(setting.PDFVersion);
            this.ResolutionComboBox.SelectedIndex   = Translator.ResolutionToIndex(setting.Resolution);
            this.ExistedFileComboBox.SelectedIndex  = Translator.ExistedFileToIndex(setting.ExistedFile);
            this.DownSamplingComboBox.SelectedIndex = Translator.DownSamplingToIndex(setting.DownSampling);

            // チェックボックスのフラグ関連
            this.PageLotationCheckBox.Checked = setting.PageRotation;
            this.EmbedFontCheckBox.Checked = setting.EmbedFont;
            this.GrayscaleCheckBox.Checked = setting.Grayscale;
            this.WebOptimizeCheckBox.Checked = setting.WebOptimize;
            this.SaveSettingCheckBox.Checked = setting.SaveSetting;
            this.UpdateCheckBox.Checked = setting.CheckUpdate;

            // ポストプロセス関連
            _postproc = setting.AdvancedMode ? this.PostProcessComboBox : this.PostProcessLiteComboBox;
            _postproc.SelectedIndex = Translator.PostProcessToIndex(setting.PostProcess);
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
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveSetting
        /// 
        /// <summary>
        /// 各種 GUI コンポーネントの情報を UserSetting に反映する．
        /// 仮想プリンタ経由など tmp パスが入力パスのテキストボックスに
        /// 設定されている場合，save_input を false にして反映しない
        /// ようにする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveSetting(UserSetting setting, bool save_input) {
            setting.OutputPath = this.OutputPathTextBox.Text;
            if (save_input) setting.InputPath = this.InputPathTextBox.Text;
            setting.UserProgram = this.UserProgramTextBox.Text;

            // コンボボックスのインデックス関連
            setting.FileType     = Translator.IndexToFileType(this.FileTypeCombBox.SelectedIndex);
            setting.PDFVersion   = Translator.IndexToPDFVersion(this.PDFVersionComboBox.SelectedIndex);
            setting.Resolution   = Translator.IndexToResolution(this.ResolutionComboBox.SelectedIndex);
            setting.ExistedFile  = Translator.IndexToExistedFile(this.ExistedFileComboBox.SelectedIndex);
            setting.PostProcess  = Translator.IndexToPostProcess(_postproc.SelectedIndex);
            setting.DownSampling = Translator.IndexToDownSampling(this.DownSamplingComboBox.SelectedIndex);

            // チェックボックスのフラグ関連
            setting.PageRotation = this.PageLotationCheckBox.Checked;
            setting.EmbedFont = this.EmbedFontCheckBox.Checked;
            setting.Grayscale = this.GrayscaleCheckBox.Checked;
            setting.WebOptimize = this.WebOptimizeCheckBox.Checked;
            setting.SaveSetting = this.SaveSettingCheckBox.Checked;
            setting.CheckUpdate = this.UpdateCheckBox.Checked;

            // 文書プロパティ
            setting.Document.Title = this.DocTitleTextBox.Text;
            setting.Document.Author = this.DocAuthorTextBox.Text;
            setting.Document.Subtitle = this.DocSubtitleTextBox.Text;
            setting.Document.Keyword = this.DocKeywordTextBox.Text;

            // パスワード
            if (this.UserPasswordCheckBox.Checked) setting.Password = this.UserPasswordTextBox.Text;
            if (this.OwnerPasswordCheckBox.Checked) {
                setting.Permission.Password = this.OwnerPasswordTextBox.Text;
                setting.Permission.AllowPrint = this.AllowPrintCheckBox.Checked;
                setting.Permission.AllowCopy = this.AllowCopyCheckBox.Checked;
                setting.Permission.AllowFormInput = this.AllowFormInputCheckBox.Checked;
                setting.Permission.AllowModify = this.AllowModifyCheckBox.Checked;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpgradeSetting
        /// 
        /// <summary>
        /// 古いバージョンからの以降の場合，レジストリの整合性を取るため
        /// にアップグレードを行う．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpgradeSetting(UserSetting setting) {
            string v1 = @"Software\CubePDF";
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(v1, false) != null) {
                setting.UpgradeFromV1(v1);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(v1, false);
            }
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
            // 各種チェック
            if (!this.CheckPassword(this.UserPasswordCheckBox.Checked, this.UserPasswordTextBox.Text, this.ConfirmUserPasswordTextBox.Text)) return;
            if (!this.CheckPassword(this.OwnerPasswordCheckBox.Checked, this.OwnerPasswordTextBox.Text, this.ConfirmOwnerPasswordTextBox.Text)) return;
            if (!this.CheckOutput(this.OutputPathTextBox.Text, this.ExistedFileComboBox.SelectedIndex)) return;

            this.ExecProgressBar.Visible = true;
            this.ConvertBackgroundWorker.RunWorkerAsync();
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
            dialog.FileName = (this.OutputPathTextBox.TextLength > 0) ?
                Path.GetFileNameWithoutExtension(this.OutputPathTextBox.Text) :
                Path.GetFileNameWithoutExtension(this.InputPathTextBox.Text);
            dialog.Filter = Appearance.FileFilterString();
            dialog.FilterIndex = this.FileTypeCombBox.SelectedIndex + 1;
            dialog.OverwritePrompt = false;
            if (dialog.ShowDialog() != DialogResult.OK) return;
            
            if (dialog.FilterIndex != 0 && dialog.FilterIndex - 1 != this.FileTypeCombBox.SelectedIndex) {
                this.FileTypeCombBox.SelectedIndex = dialog.FilterIndex - 1;
            }

            // 拡張子が選択されているファイルタイプと異なる場合は，末尾に拡張子を追加する．
            string ext = Parameter.Extension(Translator.IndexToFileType(this.FileTypeCombBox.SelectedIndex));
            this.OutputPathTextBox.Text = dialog.FileName;
            if (Path.GetExtension(this.OutputPathTextBox.Text) != ext) this.OutputPathTextBox.Text += ext;
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

            Parameter.FileTypes id = Translator.IndexToFileType(control.SelectedIndex);
            bool is_pdf = (id == Parameter.FileTypes.PDF);
            bool is_bitmap = (id == Parameter.FileTypes.BMP || id == Parameter.FileTypes.JPEG || id == Parameter.FileTypes.PNG || id == Parameter.FileTypes.TIFF);
            bool is_grayscale = !(id == Parameter.FileTypes.PS || id == Parameter.FileTypes.EPS || id == Parameter.FileTypes.SVG);
            
            this.PDFVersionComboBox.Enabled = is_pdf;
            this.ResolutionComboBox.Enabled = is_bitmap;
            this.DocTableLayoutPanel.Enabled = is_pdf;
            this.UserPasswordGroupBox.Enabled = is_pdf;
            this.OwnerPasswordGroupBox.Enabled = is_pdf;
            this.EmbedFontCheckBox.Enabled = false; // 本来は !is_bitmap;
            this.GrayscaleCheckBox.Enabled = is_grayscale;
            this.WebOptimizeCheckBox.Enabled = is_pdf;

            // 出力パスの拡張子を変更後のファイルタイプに合わせる．
            if (this.OutputPathTextBox.Text.Length == 0) return;
            this.OutputPathTextBox.Text = Path.ChangeExtension(this.OutputPathTextBox.Text, Parameter.Extension(id));
        }

        /* ----------------------------------------------------------------- */
        /// PostProcessComboBox_SelectedIndexChanged
        /* ----------------------------------------------------------------- */
        private void PostProcessComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox control = sender as ComboBox;
            if (control == null) return;

            Parameter.PostProcesses id = Translator.IndexToPostProcess(control.SelectedIndex);
            bool is_user_program = (id == Parameter.PostProcesses.UserProgram);

            this.UserProgramTextBox.Enabled = is_user_program;
            this.UserProgramButton.Enabled = is_user_program;
        }
        
        #endregion

        /* ----------------------------------------------------------------- */
        //  各種チェックボックスのイベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region Checkbox event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// WebOptimizeCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// Web 表示用に最適化オプションとパスワード関連のオプションは
        /// 同時に設定できない．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WebOptimizeCheckBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox control = sender as CheckBox;
            if (control == null) return;
            
            this.UserPasswordGroupBox.Enabled = !control.Checked;
            this.OwnerPasswordGroupBox.Enabled = !control.Checked;
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  UserPasswordCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// Web 表示用に最適化オプションとパスワード関連のオプションは
        /// 同時に設定できない．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void UserPasswordCheckBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox control = sender as CheckBox;
            if (control == null) return;

            this.ConfirmUserPasswordTextBox.BackColor = control.Checked ? SystemColors.Window : SystemColors.Control;
            this.UserPasswordTableLayoutPanel.Enabled = control.Checked;
            this.WebOptimizeCheckBox.Enabled = (!control.Checked && !this.OwnerPasswordCheckBox.Checked);
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  OwnerPasswordCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// Web 表示用に最適化オプションとパスワード関連のオプションは
        /// 同時に設定できない．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void OwnerPasswordCheckBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox control = sender as CheckBox;
            if (control == null) return;

            this.ConfirmOwnerPasswordTextBox.BackColor = control.Checked ? SystemColors.Window : SystemColors.Control;
            this.OwnerPasswordTableLayoutPanel.Enabled = control.Checked;
            this.WebOptimizeCheckBox.Enabled = (!control.Checked && !this.UserPasswordCheckBox.Checked);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  バージョンダイアログを表示するためのイベントハンドラ
        /* ----------------------------------------------------------------- */
        #region Event handlers about version dialog

        /* ----------------------------------------------------------------- */
        /// HeaderPictureBox_Click
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_Click(object sender, EventArgs e) {
            CubePDF.VersionDialog version = new VersionDialog(_setting.Version);
            version.ShowDialog(this);
        }

        /* ----------------------------------------------------------------- */
        /// HeaderPictureBox_MouseEnter
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_MouseEnter(object sender, EventArgs e) {
            Control control = sender as Control;
            if (control == null) return;

            this.Cursor = Cursors.Hand;
            ToolTip tips = new ToolTip();
            tips.InitialDelay = 500;
            tips.ReshowDelay = 1000;
            tips.AutoPopDelay = 1000;
            tips.SetToolTip(control, Properties.Settings.Default.About);
        }

        /* ----------------------------------------------------------------- */
        /// HeaderPictureBox_MouseLeave
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_MouseLeave(object sender, EventArgs e) {
            this.Cursor = Cursors.Default;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  テキストボックス上での出力パスの変更を容易にするための仕掛け
        /* ----------------------------------------------------------------- */
        #region Gimmicks for helping to input output-path

        /* ----------------------------------------------------------------- */
        ///
        /// OutputPathTextBox_Click
        /// 
        /// <summary>
        /// 出力パスの表示されたテキストボックスを最初にクリックした時に
        /// ファイル名の部分だけ選択状態にする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OutputPathTextBox_Click(object sender, EventArgs e) {
            TextBox control = sender as TextBox;
            if (control == null) return;

            if (control.Tag == null && control.Text.Length > 0) {
                if (control.Text[control.Text.Length - 1] == '\\') control.SelectionStart = control.Text.Length;
                else {
                    string dir = Path.GetDirectoryName(control.Text);
                    int pos = (dir != null) ? dir.Length : 0;
                    while (pos < control.Text.Length && control.Text[pos] == '\\') pos += 1;
                    control.Select(pos, Math.Max(control.Text.Length - pos, 0));
                }
            }
            control.Tag = true;
        }

        /* ----------------------------------------------------------------- */
        /// OutputPathTextBox_Leave
        /* ----------------------------------------------------------------- */
        private void OutputPathTextBox_Leave(object sender, EventArgs e) {
            Control control = sender as Control;
            if (control == null) return;
            control.Tag = null;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  パスワードの打ち間違えに関する仕掛け
        /* ----------------------------------------------------------------- */
        #region Gimicks for password dialogs

        /* ----------------------------------------------------------------- */
        /// UserPasswordTextBox_TextChanged
        /* ----------------------------------------------------------------- */
        private void UserPasswordTextBox_TextChanged(object sender, EventArgs e) {
            this.ConfirmUserPasswordTextBox.BackColor = SystemColors.Window;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfirmUserPasswordTextBox_TextChanged
        ///
        /// <summary>
        /// パスワードダイアログと確認ダイアログの値が異なる間は，
        /// 背景色を赤色に設定する．背景色が白色に戻るタイミングは，
        ///   1. パスワードが一致した場合
        ///   2. パスワードダイアログの入力が変化した場合
        ///   3. 確認ダイアログの入力値が空になった場合
        ///   4. チェックボックスで有効/無効が変化した場合
        /// の 4 通りである．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfirmUserPasswordTextBox_TextChanged(object sender, EventArgs e) {
            TextBox control = sender as TextBox;
            if (control == null) return;
            if (control.Text.Length > 0 && this.UserPasswordTextBox.Text != control.Text) {
                control.BackColor = Color.FromArgb(255, 102, 102);
            }
            else control.BackColor = SystemColors.Window;
        }

        /* ----------------------------------------------------------------- */
        /// OwnerPasswordTextBox_TextChanged
        /* ----------------------------------------------------------------- */
        private void OwnerPasswordTextBox_TextChanged(object sender, EventArgs e) {
            this.ConfirmOwnerPasswordTextBox.BackColor = SystemColors.Window;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfirmOwnerPasswordTextBox_TextChanged
        /// 
        /// <summary>
        /// パスワードダイアログと確認ダイアログの値が異なる間は，
        /// 背景色を赤色に設定する．背景色が白色に戻るタイミングは，
        ///   1. パスワードが一致した場合
        ///   2. パスワードダイアログの入力が変化した場合
        ///   3. 確認ダイアログの入力値が空になった場合
        ///   4. チェックボックスで有効/無効が変化した場合
        /// の 4 通りである．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void ConfirmOwnerPasswordTextBox_TextChanged(object sender, EventArgs e) {
            TextBox control = sender as TextBox;
            if (control == null) return;
            if (control.Text.Length > 0 && this.OwnerPasswordTextBox.Text != control.Text) {
                control.BackColor = Color.FromArgb(255, 102, 102);
            }
            else control.BackColor = SystemColors.Window;
        }

        #endregion
        
        /* ----------------------------------------------------------------- */
        //  バックグラウンドワーカーのイベントハンドラ
        /* ----------------------------------------------------------------- */
        #region Background worker

        /* ----------------------------------------------------------------- */
        /// ConvertBackgroundWorker_DoWork
        /* ----------------------------------------------------------------- */
        private void ConvertBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            // GUI の設定を UserSetting に保存する
            this.SaveSetting(_setting, this._InputpathPanel.Enabled);
            if (_setting.SaveSetting) {
                _setting.SaveSetting = false; // 「設定を保存」の項目はレジストリには保存しない．
                _setting.Save();
            }
            _setting.InputPath = this.InputPathTextBox.Text;
            
            // 変換の実行
            Converter converter = new Converter();
            if (!converter.Run(_setting) && converter.Messages.Count > 0) e.Result = converter.Messages;
            else e.Result = null;
        }

        /* ----------------------------------------------------------------- */
        /// ConvertBackgroundWorker_RunWorkerCompleted
        /* ----------------------------------------------------------------- */
        private void ConvertBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (e.Result != null) this.ShowErrorMessage((List<CubePDF.Message>)e.Result);
            this.Close();
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  その他のメソッド群
        /* ----------------------------------------------------------------- */
        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// ShowErrorMessage
        ///
        /// <summary>
        /// エラーメッセージの表示，およびログファイルへの書き込みを行う．
        /// エラーメッセージとしてダイアログに表示させるのは，最後の
        /// Error レベル以上のメッセージである．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowErrorMessage(List<CubePDF.Message> messages) {
            string src = Utility.CurrentDirectory + @"\cubepdf.log";
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
            
            string error = "";
            foreach (CubePDF.Message message in messages) {
                Trace.WriteLine(message.ToString());
                if (message.Level == Message.Levels.Error || message.Level == Message.Levels.Fatal) {
                    error = message.Value;
                }
            }

            Trace.Close();
            if (error.Length > 0) MessageBox.Show(error, "CubePDF エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
