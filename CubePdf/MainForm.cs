/* ------------------------------------------------------------------------- */
///
/// MainForm.cs
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
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainForm
    ///
    /// <summary>
    /// CubePDF メイン画面を表示するための Windows フォームクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm (constructor)
        /// 
        /// <summary>
        /// 引数に指定されたユーザ設定を用いて、オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MainForm(UserSetting setting)
        {
            InitializeComponent();
            InitializeComboBox();

            _setting = setting;
            UpgradeSetting(_setting);
            LoadSetting(_setting);

            var name    = Properties.Resources.ProductName;
            var version = _setting.Version;
            var edition = (IntPtr.Size == 4) ? "x86" : "x64";
            Text = string.Format("{0} {1} ({2})", name, version, edition);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeComboBox
        /// 
        /// <summary>
        /// 各種コンボボックスに表示される文字列を初期化します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void InitializeComboBox()
        {
            // FileType
            FileTypeCombBox.Items.Clear();
            foreach (Parameter.FileTypes id in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                FileTypeCombBox.Items.Add(Appearance.GetString(id));
            }
            FileTypeCombBox.SelectedIndex = 0;

            // PDF Version
            PdfVersionComboBox.Items.Clear();
            foreach (Parameter.PdfVersions id in Enum.GetValues(typeof(Parameter.PdfVersions)))
            {
                var str = Appearance.GetString(id);
                if (string.IsNullOrEmpty(str)) continue;
                PdfVersionComboBox.Items.Add(str);
            }
            PdfVersionComboBox.SelectedIndex = 0;

            // Resolution
            ResolutionComboBox.Items.Clear();
            foreach (Parameter.Resolutions id in Enum.GetValues(typeof(Parameter.Resolutions)))
            {
                ResolutionComboBox.Items.Add(Appearance.GetString(id));
            }
            ResolutionComboBox.SelectedIndex = 0;

            // ExistedFile
            ExistedFileComboBox.Items.Clear();
            foreach (Parameter.ExistedFiles id in Enum.GetValues(typeof(Parameter.ExistedFiles)))
            {
                ExistedFileComboBox.Items.Add(Appearance.GetString(id));
            }
            ExistedFileComboBox.SelectedIndex = 0;

            // PostProcess
            PostProcessComboBox.Items.Clear();
            foreach (Parameter.PostProcesses id in Enum.GetValues(typeof(Parameter.PostProcesses)))
            {
                PostProcessComboBox.Items.Add(Appearance.GetString(id));
            }
            PostProcessComboBox.SelectedIndex = 0;

            // Advance モードに設定されていない PostProcess
            PostProcessLiteComboBox.Items.Clear();
            PostProcessLiteComboBox.Items.Add(Appearance.GetString(Parameter.PostProcesses.Open));
            PostProcessLiteComboBox.Items.Add(Appearance.GetString(Parameter.PostProcesses.None));
            PostProcessLiteComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Varlidation methods when the convert button is pushed

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidPassword
        ///
        /// <summary>
        /// パスワードのチェックを行います。パスワードダイアログと
        /// 確認ダイアログの入力値が一致しない場合にはエラーメッセージを
        /// 表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsValidPassword(bool enabled, string password, string confirm)
        {
            if (enabled && password != confirm)
            {
                ShowError(Properties.Resources.PasswordUnmatched);
                MainTabControl.SelectedTab = SecurityTabPage;
                return false;
            }
            else return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidOutput
        /// 
        /// <summary>
        /// 出力先パスが正しいかどうかをチェックします。出力先パスが指定
        /// されていない場合はエラーメッセージを表示します。また、指定された
        /// 出力先パスが既に存在する場合には確認ダイアログを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsValidOutput(int do_existed_file)
        {
            if (string.IsNullOrEmpty(OutputPathTextBox.Text))
            {
                ShowError(Properties.Resources.FileNotSpecified);
                return false;
            }

            var extension = System.IO.Path.GetExtension(OutputPathTextBox.Text);
            var compared = Parameter.GetExtension(Translator.ToFileType(FileTypeCombBox.SelectedIndex));
            if (extension != compared) OutputPathTextBox.Text += compared;

            if (System.IO.File.Exists(OutputPathTextBox.Text) &&
                Translator.ToExistedFile(ExistedFileComboBox.SelectedIndex) != Parameter.ExistedFiles.Rename)
            {
                var message = string.Format(Properties.Resources.FileExists, OutputPathTextBox.Text,
                    Appearance.GetString((Parameter.ExistedFiles)do_existed_file));
                var result = MessageBox.Show(message, Properties.Resources.OverwritePrompt,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return result != DialogResult.Cancel;
            }
            else return true;
        }

        #endregion

        #region Synchronize user settings

        /* ----------------------------------------------------------------- */
        ///
        /// LoadSetting
        /// 
        /// <summary>
        /// UserSetting の情報を各種 GUI コンポーネントに反映します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: 仮想プリンタ経由の場合は InputPath にその値を設定する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadSetting(UserSetting setting)
        {
            UserProgramTextBox.Text = setting.UserProgram;
            OutputPathTextBox.Text  = setting.OutputPath;
            InputPathTextBox.Text   = setting.InputPath;
            ConvertButton.Enabled   = !string.IsNullOrEmpty(InputPathTextBox.Text);

            // コンボボックスのインデックス関連
            FileTypeCombBox.SelectedIndex      = Translator.ToIndex(setting.FileType);
            PdfVersionComboBox.SelectedIndex   = Translator.ToIndex(setting.PDFVersion);
            ResolutionComboBox.SelectedIndex   = Translator.ToIndex(setting.Resolution);
            ExistedFileComboBox.SelectedIndex  = Translator.ToIndex(setting.ExistedFile);

            // ラジオボタンのフラグ関連
            if (setting.Orientation == Parameter.Orientations.Portrait) PortraitRadioButton.Checked = true;
            else if (setting.Orientation == Parameter.Orientations.Landscape) LandscapeRadioButton.Checked = true;
            else AutoRadioButton.Checked = true;

            // チェックボックスのフラグ関連
            EmbedFontCheckBox.Checked    = setting.EmbedFont;
            GrayscaleCheckBox.Checked    = setting.Grayscale;
            ImageFilterCheckBox.Checked  = (setting.ImageFilter == Parameter.ImageFilters.DCTEncode) ? true : false;
            WebOptimizeCheckBox.Checked  = setting.WebOptimize;
            UpdateCheckBox.Checked       = setting.CheckUpdate;

            // ポストプロセス関連
            _postproc = setting.AdvancedMode ? PostProcessComboBox : PostProcessLiteComboBox;
            _postproc.SelectedIndex = Math.Min(Translator.ToIndex(setting.PostProcess), Math.Max(_postproc.Items.Count - 1, 0));
            PostProcessPanel.Enabled = setting.AdvancedMode;
            PostProcessPanel.Visible = setting.AdvancedMode;
            PostProcessLabel.Visible = setting.AdvancedMode;
            PostProcessLiteComboBox.Enabled = !setting.AdvancedMode;
            PostProcessLiteComboBox.Visible = !setting.AdvancedMode;
            PostProcessLiteLabel.Visible    = !setting.AdvancedMode;

            // 入力パスを選択可能にするかどうか
            InputPathLabel.Visible = setting.SelectInputFile;
            InputPathPanel.Visible = setting.SelectInputFile;
            InputPathPanel.Enabled = setting.SelectInputFile && setting.InputPath.Length == 0;

            _messages.Add(new Message(Message.Levels.Debug, "CubePdf.MainForm.LoadSetting"));
            _messages.Add(new Message(Message.Levels.Debug, setting.ToString()));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveSetting
        /// 
        /// <summary>
        /// 各種 GUI コンポーネントの情報を UserSetting に反映します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveSetting(UserSetting setting)
        {
            setting.OutputPath  = GetDirectoryName(OutputPathTextBox.Text);
            setting.InputPath   = GetDirectoryName(InputPathTextBox.Text);
            setting.UserProgram = UserProgramTextBox.Text;

            // コンボボックスのインデックス関連
            setting.FileType     = Translator.ToFileType(FileTypeCombBox.SelectedIndex);
            setting.PDFVersion   = Translator.ToPdfVersion(PdfVersionComboBox.SelectedIndex);
            setting.Resolution   = Translator.ToResolution(ResolutionComboBox.SelectedIndex);
            setting.ExistedFile  = Translator.ToExistedFile(ExistedFileComboBox.SelectedIndex);
            setting.PostProcess  = Translator.ToPostProcess(_postproc.SelectedIndex);
            setting.DownSampling = Parameter.DownSamplings.Bicubic;

            // ラジオボタンのフラグ関連
            setting.Orientation  = PortraitRadioButton.Checked ? Parameter.Orientations.Portrait :
                LandscapeRadioButton.Checked ? Parameter.Orientations.Landscape : Parameter.Orientations.Auto;

            // チェックボックスのフラグ関連
            setting.EmbedFont    = EmbedFontCheckBox.Checked;
            setting.Grayscale    = GrayscaleCheckBox.Checked;
            setting.ImageFilter  = ImageFilterCheckBox.Checked ? Parameter.ImageFilters.DCTEncode : Parameter.ImageFilters.FlateEncode;
            setting.WebOptimize  = WebOptimizeCheckBox.Checked;
            setting.CheckUpdate  = UpdateCheckBox.Checked;

            // 文書プロパティ
            setting.Document.Title    = DocTitleTextBox.Text;
            setting.Document.Author   = DocAuthorTextBox.Text;
            setting.Document.Subtitle = DocSubtitleTextBox.Text;
            setting.Document.Keyword  = DocKeywordTextBox.Text;

            // セキュリティ
            if (OwnerPasswordCheckBox.Checked)
            {
                setting.Permission.Password       = OwnerPasswordTextBox.Text;
                setting.Permission.AllowPrint     = AllowPrintCheckBox.Checked;
                setting.Permission.AllowCopy      = AllowCopyCheckBox.Checked;
                setting.Permission.AllowFormInput = AllowFormInputCheckBox.Checked;
                setting.Permission.AllowModify    = AllowModifyCheckBox.Checked;

                if (RequiredUserPasswordCheckBox.Checked)
                {
                    setting.Password = UserPasswordCheckBox.Checked ? UserPasswordTextBox.Text : OwnerPasswordTextBox.Text;
                }
            }

            _messages.Add(new Message(Message.Levels.Debug, "CubePdf.MainForm.SaveSetting"));
            _messages.Add(new Message(Message.Levels.Debug, setting.ToString()));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpgradeSetting
        /// 
        /// <summary>
        /// 古いバージョンからの移行した場合、レジストリの整合性を取るため
        /// にアップグレードを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpgradeSetting(UserSetting setting)
        {
            var v1 = @"Software\CubePDF";
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(v1, false) != null)
            {
                setting.UpgradeFromV1(v1);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(v1, false);
            }
        }

        #endregion

        #region Event handlers for main form

        /* ----------------------------------------------------------------- */
        ///
        /// OnShown
        ///  
        /// <summary>
        /// CubePDF メイン画面が表示された時に実行されるイベントハンドラ
        /// です。CubePDF のメイン画面が起動した際に他のウィンドウに
        /// 隠れてしまう場合があるのでその対策を行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            SettingButton.BackgroundImage = Properties.Resources.button_setting_disable;
            SettingButton.Enabled = false;
            Activate();
            TopMost = true;
            TopMost = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnKeyDown
        ///
        /// <summary>
        /// キーボードのキーが押下された時に実行されるイベントハンドラです。
        /// エンターキーに「変換」ボタンを対応させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter) ConvertButton_Click(ConvertButton, e);
        }

        #endregion

        #region Event handlers for buttons

        /* ----------------------------------------------------------------- */
        ///
        /// ConvertButton_Click
        ///
        /// <summary>
        /// 変換ボタンが押下された時に実行されるイベントハンドラです。
        /// 各種設定のチェックを行った後、変換処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // 各種チェック
            if (!IsValidPassword(OwnerPasswordCheckBox.Checked, OwnerPasswordTextBox.Text, ConfirmOwnerPasswordTextBox.Text)) return;
            if (!IsValidPassword(OwnerPasswordCheckBox.Checked & UserPasswordCheckBox.Checked, UserPasswordTextBox.Text, ConfirmUserPasswordTextBox.Text)) return;
            if (!IsValidOutput(ExistedFileComboBox.SelectedIndex)) return;

            // ライブラリが存在してるかどうかをログに記録。
            if (!System.IO.Directory.Exists(_setting.LibPath)) _messages.Add(new Message(Message.Levels.Debug, string.Format("{0}: file not found", _setting.LibPath)));
            if (!System.IO.Directory.Exists(_setting.LibPath + @"\lib")) _messages.Add(new Message(Message.Levels.Debug, string.Format("{0}\\lib: file not found", _setting.LibPath)));

            ConvertButton.Enabled   = false;
            SettingButton.Visible   = false;            
            ExecProgressBar.Visible = true;

            SaveSetting(_setting);
            _setting.InputPath  = InputPathTextBox.Text;
            _setting.OutputPath = OutputPathTextBox.Text;

            ConvertBackgroundWorker.RunWorkerAsync();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExitButton_Click
        ///
        /// <summary>
        /// 終了ボタンが押下された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (_setting.DeleteOnClose) System.IO.File.Delete(_setting.InputPath);
            _messages.Add(new Message(Message.Levels.Debug, "CubePdf.MainForm.ExitButton_Click"));
            ShowMessage();
            Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingButton_Click
        ///
        /// <summary>
        /// 設定を保存ボタンが押下された時に実行されるイベントハンドラです。
        /// 現在の設定をレジストリに保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SettingButton_Click(object sender, EventArgs e)
        {
            SaveSetting(_setting);
            _setting.SaveSetting = Parameter.SaveSettings.None; // deprecated
            _setting.Save();
            SettingButton.BackgroundImage = Properties.Resources.button_setting_disable;
            SettingButton.Enabled = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OutputPathButton_Click
        ///
        /// <summary>
        /// 出力先ファイルのボタンが押下された時に実行されるイベントハンドラ
        /// です。ファイル保存ダイアログを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OutputPathButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.FileName = !string.IsNullOrEmpty(OutputPathTextBox.Text) ?
                System.IO.Path.GetFileNameWithoutExtension(OutputPathTextBox.Text) :
                System.IO.Path.GetFileNameWithoutExtension(InputPathTextBox.Text);
            dialog.Filter = Properties.Resources.OutputPathFilter;
            dialog.FilterIndex = FileTypeCombBox.SelectedIndex + 1;
            dialog.OverwritePrompt = false;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            if (dialog.FilterIndex != 0 && dialog.FilterIndex - 1 != FileTypeCombBox.SelectedIndex)
            {
                FileTypeCombBox.SelectedIndex = dialog.FilterIndex - 1;
            }

            // 拡張子が選択されているファイルタイプと異なる場合は、末尾に拡張子を追加する。
            // ただし、入力された拡張子がユーザのコンピュータに登録されている場合は、それを優先する。
            var extension = System.IO.Path.GetExtension(OutputPathTextBox.Text);
            var type = Translator.ToFileType(FileTypeCombBox.SelectedIndex);
            var compared = Parameter.GetExtension(type);
            OutputPathTextBox.Text = dialog.FileName;
            if (extension != compared) OutputPathTextBox.Text += compared;
            SettingChanged(sender, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InputPathButton_Click
        ///
        /// <summary>
        /// 入力ファイルのボタンが押下された時に実行されるイベントハンドラ
        /// です。変換元のファイルを選択するためのダイアログを表示します。
        /// </summary>
        /// 
        /// <remarks>
        /// このボタンは、通常時は非表示に設定されています。SelectInput
        /// 設定が有効な場合のみ操作する事が可能です。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void InputPathButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = Properties.Resources.InputPathFilter;
            if (!string.IsNullOrEmpty(InputPathTextBox.Text)) dialog.FileName = InputPathTextBox.Text;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            InputPathTextBox.Text = dialog.FileName;
            ConvertButton.Enabled = !string.IsNullOrEmpty(InputPathTextBox.Text);
            SettingChanged(sender, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UserProgramButton_Click
        ///
        /// <summary>
        /// ユーザプログラムを選択するためのダイアログを表示します。
        /// </summary>
        ///
        /// <remarks>
        /// このボタンは、通常時は非表示に設定されています。AdvanceMode
        /// 設定が有効な場合のみ操作する事が可能です。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void UserProgramButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = Properties.Resources.UserProgramFilter;
            if (!string.IsNullOrEmpty(UserProgramTextBox.Text)) dialog.FileName = UserProgramTextBox.Text;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            UserProgramTextBox.Text = dialog.FileName;
            SettingChanged(sender, e);
        }

        #endregion

        #region Event handlers for combo-boxes

        /* ----------------------------------------------------------------- */
        ///
        /// FileTypeCombBox_SelectedIndexChanged
        /// 
        /// <summary>
        /// ファイルの種類が変更された時に実行されるイベントハンドラです。
        /// 変換するファイルの種類によって無効なオプションがあるため、
        /// 有効/無効を切り替えます。
        /// </summary>
        /// 
        /// <remarks>
        /// ファイルタイプに依存するオプションは以下の通りです。
        /// 
        /// PDF, PS, EPS: フォントの埋め込み
        /// PDF: バージョン、 文書プロパティ、セキュリティ、Web 最適化
        /// 
        /// 尚、Ghostscript のバグで現在のところ PS, EPS は
        /// グレースケール設定ができないようです。また，フォント埋め込みを
        /// 無効にすると文字化けが発生するので、暫定的に強制無効設定にして
        /// います。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void FileTypeCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as ComboBox;
            if (control == null) return;

            var id = Translator.ToFileType(control.SelectedIndex);
            var ispdf = (id == Parameter.FileTypes.PDF);
            var isbmp = (id == Parameter.FileTypes.BMP || id == Parameter.FileTypes.JPEG || id == Parameter.FileTypes.PNG || id == Parameter.FileTypes.TIFF);
            var isweb = WebOptimizeCheckBox.Checked;
            var issecurity = (RequiredUserPasswordCheckBox.Checked || OwnerPasswordCheckBox.Checked);

            PdfVersionComboBox.Enabled  = ispdf;
            DocPanel.Enabled            = ispdf;
            SecurityGroupBox.Enabled    = ispdf && !isweb;
            EmbedFontCheckBox.Enabled   = false; //!is_bitmap;
            ImageFilterCheckBox.Enabled = !isbmp;
            WebOptimizeCheckBox.Enabled = ispdf && !issecurity;

            // 出力パスの拡張子を変更後のファイルタイプに合わせる．
            if (!string.IsNullOrEmpty(OutputPathTextBox.Text))
            {
                OutputPathTextBox.Text = System.IO.Path.ChangeExtension(OutputPathTextBox.Text, Parameter.GetExtension(id));
            }
            SettingChanged(sender, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcessComboBox_SelectedIndexChanged
        ///
        /// <summary>
        /// ポストプロセスが変更された時に実行されるイベントハンドラです。
        /// ユーザプログラムが選択された場合、ユーザプログラムを指定する
        /// 項目を有効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void PostProcessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as ComboBox;
            if (control == null) return;

            var id = Translator.ToPostProcess(control.SelectedIndex);
            var isuser = (id == Parameter.PostProcesses.UserProgram);

            UserProgramTextBox.Enabled = isuser;
            UserProgramButton.Enabled  = isuser;
            SettingChanged(sender, e);
        }

        #endregion

        #region Event handlers for check-boxes

        /* ----------------------------------------------------------------- */
        ///
        /// WebOptimizeCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// Web 表示用に最適化オプションの設定が変更された時に実行される
        /// イベントハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Web 表示用に最適化オプションとパスワード関連のオプションは
        /// 同時に設定できないため、このオプションが有効になっている間は、
        /// パスワードを設定できないようにします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void WebOptimizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox control = sender as CheckBox;
            if (control == null) return;

            SecurityGroupBox.Enabled = !control.Checked;
            SettingChanged(sender, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OwnerPasswordCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// パスワードによるセキュリティを有効にする、チェックボックスの
        /// 設定が変更された時に実行されるイベントハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// パスワード関連のオプションはWeb 表示用に最適化オプションと同時に
        /// 設定する事ができないため、このオプションが有効な間は Web 表示用に
        /// 最適化オプションを有効化できないようにします。
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        private void OwnerPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox control = sender as CheckBox;
            if (control == null) return;

            ConfirmOwnerPasswordTextBox.BackColor = control.Checked ? SystemColors.Window : SystemColors.Control;
            SecurityPanel.Enabled = control.Checked;
            WebOptimizeCheckBox.Enabled = !control.Checked;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RequiredUserPasswordCheckBox_CheckedChanged
        ///
        /// <summary>
        /// PDF ファイルを開く際にパスワードを要求する、チェックボックスの
        /// 設定が変更された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RequiredUserPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var control = sender as CheckBox;
            if (control == null) return;

            UserPasswordCheckBox.Enabled = control.Checked;
            UserPasswordPanel.Enabled = (control.Checked & UserPasswordCheckBox.Checked);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UserPasswordCheckBox_CheckedChanged
        ///
        /// <summary>
        /// 閲覧専用のパスワードを設定する、チェックボックスの設定が変更
        /// された時に実行されるイベントハンドラです。このチェックボックスが
        /// 有効な場合、ユーザパスワードを入力するためのテキストボックスを
        /// 入力可能な状態にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UserPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox control = sender as CheckBox;
            if (control == null) return;

            UserPasswordPanel.Enabled = control.Checked;
        }

        #endregion

        #region Event handlers about version dialog

        /* ----------------------------------------------------------------- */
        ///
        /// HeaderPictureBox_Click
        ///
        /// <summary>
        /// 上部のロゴ画像をクリックした時に実行されるイベントハンドラです。
        /// バージョン情報を表示したダイアログが表示されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_Click(object sender, EventArgs e)
        {
            CubePdf.VersionDialog version = new VersionDialog(_setting.Version);
            version.ShowDialog(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HeaderPictureBox_MouseEnter
        ///
        /// <summary>
        /// 上部のロゴ画像にマウスが触れた時に実行されるイベントハンドラ
        /// です。ツールチップで「CubePDF について」と言う文字列を表示
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;

            Cursor = Cursors.Hand;

            _tips.Hide(control);
            _tips.ToolTipTitle = string.Empty;
            _tips.IsBalloon    = false;
            _tips.InitialDelay = 500;
            _tips.ReshowDelay  = 100;
            _tips.AutoPopDelay = 1000;
            _tips.Show(Properties.Resources.About, control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HeaderPictureBox_MouseLeave
        ///
        /// <summary>
        /// 上部のロゴ画像からマウスが離れた時に実行されるイベントハンドラ
        /// です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void HeaderPictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;

            var control = sender as Control;
            if (control == null) return;
            _tips.Hide(control);
        }

        #endregion

        #region Other event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// SettingChanged
        ///
        /// <summary>
        /// ユーザによって各種設定が変更された時に実行されるイベントハンドラ
        /// です。「設定を保存」ボタンが押下できるようになります。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SettingChanged(object sender, EventArgs e)
        {
            SettingButton.BackgroundImage = Properties.Resources.button_setting;
            SettingButton.Enabled = true;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// テキストボックス上での出力パスに関する仕掛け
        /* ----------------------------------------------------------------- */
        #region Gimmicks for helping to input output path

        /* ----------------------------------------------------------------- */
        /// 
        /// PathTextBox_TextChanged
        /// 
        /// <summary>
        /// ファイル名を入力するテキストボックスの内容が変更された時に
        /// 実行されるイベントハンドラです。入力したファイル名に、
        /// ファイル名として無効な文字が含まれていないかチェックすします。
        /// </summary>
        /// 
        /// <remarks>
        /// テキストボックスに表示されている文字列はファイルへのパスなので、
        /// ディレクトリ区切りを表す "\"（バックスラッシュ）は除外します。
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            var control = sender as TextBox;
            if (control == null) return;

            _tips.Hide(control);

            char[] invalids = { '/', '*', '"', '<', '>', '|', '?', ':' };
            var index = control.Text.IndexOfAny(invalids);
            if (index == 1 && control.Text[index] == ':') index = control.Text.IndexOfAny(invalids, 2);
            if (index >= 0)
            {
                var pos = control.SelectionStart;
                control.Text = control.Text.Remove(index, 1);
                control.SelectionStart = Math.Max(pos - 1, 0);

                _tips.ToolTipTitle = Properties.Resources.InvalidFilenameTitle;
                _tips.IsBalloon = false;
                _tips.InitialDelay = 500;
                _tips.ReshowDelay = 100;
                _tips.AutoPopDelay = 1000;
                _tips.Show(Properties.Resources.InvalidFilename, control);
            }

            SettingChanged(sender, e);
        }

        /* ----------------------------------------------------------------- */
        /// 
        /// PathTextBox_Leave
        /// 
        /// <summary>
        /// テキストボックスがフォーカスを失った時に実行されるイベント
        /// ハンドラです。ツールチップを非表示にします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void PathTextBox_Leave(object sender, EventArgs e)
        {
            var control = sender as TextBox;
            if (control == null) return;
            _tips.Hide(control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OutputPathTextBox_Click
        /// 
        /// <summary>
        /// 出力ファイルが表示されているテキストボックスをクリックした
        /// 時に実行されるイベントハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// 出力パスの表示されたテキストボックスを最初にクリックした時に、
        /// ファイル名を変更しやすいように、その部分だけ選択状態にします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void OutputPathTextBox_Click(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            if (control == null) return;

            if (control.Tag == null && control.Text.Length > 0)
            {
                if (control.Text[control.Text.Length - 1] == '\\') control.SelectionStart = control.Text.Length;
                else
                {
                    string dir = System.IO.Path.GetDirectoryName(control.Text);
                    int pos = (dir != null) ? dir.Length : 0;
                    while (pos < control.Text.Length && control.Text[pos] == '\\') pos += 1;
                    control.Select(pos, Math.Max(control.Text.Length - pos, 0));
                }
            }
            control.Tag = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OutputPathTextBox_Leave
        ///
        /// <summary>
        /// 出力ファイルが表示されているテキストボックスからフォーカスが
        /// 外れた時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OutputPathTextBox_Leave(object sender, EventArgs e)
        {
            PathTextBox_Leave(sender, e);
            Control control = sender as Control;
            if (control == null) return;
            control.Tag = null;
        }

        /* ----------------------------------------------------------------- */
        /// 
        /// InputPathTextBox_TextChanged
        /// 
        /// <summary>
        /// 入力ファイル名を入力するテキストボックスの内容が変更された時に
        /// 実行されるイベントハンドラです。テキストボックスの内容が空の
        /// 間は、変換ボタンを押下できないようにします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void InputPathTextBox_TextChanged(object sender, EventArgs e)
        {
            var control = sender as TextBox;
            if (control == null) return;

            PathTextBox_TextChanged(sender, e);
            ConvertButton.Enabled = !string.IsNullOrEmpty(InputPathTextBox.Text);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  パスワードの打ち間違えに関する仕掛け
        /* ----------------------------------------------------------------- */
        #region Gimicks for password dialogs

        /* ----------------------------------------------------------------- */
        ///
        /// UserPasswordTextBox_TextChanged
        ///
        /// <summary>
        /// ユーザパスワードのテキストボックスの内容が変更された時に実行
        /// されるイベントハンドラです。
        /// パスワード確認のためのテキストボックスの内容を消去します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UserPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            ConfirmUserPasswordTextBox.BackColor = SystemColors.Window;
            if (ConfirmUserPasswordTextBox.Text.Length > 0) ConfirmUserPasswordTextBox.Text = "";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfirmUserPasswordTextBox_TextChanged
        ///
        /// <summary>
        /// パスワード確認のためのテキストボックスの内容が変更された時に
        /// 実行されるイベントハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// パスワードダイアログと確認ダイアログの値が異なる間は、
        /// 背景色を赤色に設定します。背景色が白色に戻るタイミングは、以下の
        /// 通りです。
        /// 
        /// 1. パスワードが一致した場合
        /// 2. パスワードダイアログの入力が変化した場合
        /// 3. 確認ダイアログの入力値が空になった場合
        /// 4. チェックボックスで有効/無効が変化した場合
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void ConfirmUserPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            if (control == null) return;
            if (control.Text.Length > 0 && UserPasswordTextBox.Text != control.Text)
            {
                control.BackColor = Color.FromArgb(255, 102, 102);
            }
            else control.BackColor = SystemColors.Window;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OwnerPasswordTextBox_TextChanged
        ///
        /// <summary>
        /// オーナパスワードのテキストボックスの内容が変更された時に実行
        /// されるイベントハンドラです。
        /// パスワード確認のためのテキストボックスの内容を消去します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OwnerPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            ConfirmOwnerPasswordTextBox.BackColor = SystemColors.Window;
            if (ConfirmOwnerPasswordTextBox.Text.Length > 0) ConfirmOwnerPasswordTextBox.Text = "";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConfirmOwnerPasswordTextBox_TextChanged
        /// 
        /// <summary>
        /// パスワード確認のためのテキストボックスの内容が変更された時に
        /// 実行されるイベントハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// パスワードダイアログと確認ダイアログの値が異なる間は、
        /// 背景色を赤色に設定します。背景色が白色に戻るタイミングは、以下の
        /// 通りです。
        /// 
        /// 1. パスワードが一致した場合
        /// 2. パスワードダイアログの入力が変化した場合
        /// 3. 確認ダイアログの入力値が空になった場合
        /// 4. チェックボックスで有効/無効が変化した場合
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        private void ConfirmOwnerPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            if (control == null) return;
            if (control.Text.Length > 0 && OwnerPasswordTextBox.Text != control.Text)
            {
                control.BackColor = Color.FromArgb(255, 102, 102);
            }
            else control.BackColor = SystemColors.Window;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  バックグラウンドワーカ（メイン処理）
        /* ----------------------------------------------------------------- */
        #region Event handlers for background worker

        /* ----------------------------------------------------------------- */
        ///
        /// ConvertBackgroundWorker_DoWork
        ///
        /// <summary>
        /// BackgroundWorker オブジェクトが非同期での実行を開始した時に
        /// 実行されるイベントハンドラです。メインとなる変換処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConvertBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Converter converter = new Converter();
            bool status = converter.Run(_setting);
            converter.Messages.Add(new Message(Message.Levels.Info, String.Format("CubePdf.Converter.Run: {0}", status.ToString())));
            e.Result = converter.Messages;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConvertBackgroundWorker_RunWorkerCompleted
        ///
        /// <summary>
        /// BackgroundWorker オブジェクトが非同期での実行を終了した時に
        /// 実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ConvertBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null) _messages.AddRange((List<CubePdf.Message>)e.Result);
            ShowMessage();
            if (_setting.DeleteOnClose && System.IO.File.Exists(_setting.InputPath)) System.IO.File.Delete(_setting.InputPath);
            Close();
        }

        #endregion

        #region Methods about messages

        /* ----------------------------------------------------------------- */
        ///
        /// ShowError
        /// 
        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                Properties.Resources.ErrorTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowWarning
        /// 
        /// <summary>
        /// 警告メッセージを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowWarning(string message)
        {
            MessageBox.Show(
                message,
                Properties.Resources.WarningTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowMessage
        ///
        /// <summary>
        /// エラーメッセージの表示、およびログファイルへの書き込みを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// 複数のエラーメッセージが存在する場合、ダイアログには最後の Error
        /// レベル以上のメッセージを表示します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowMessage()
        {
            var error = string.Empty;
            var warn  = string.Empty;
            foreach (var message in _messages)
            {
                Trace.WriteLine(message.ToString());
                if (message.Level == Message.Levels.Error || message.Level == Message.Levels.Fatal) error = message.Value;
                else if (message.Level == Message.Levels.Warn) warn = message.Value;
            }

            var iserror = !string.IsNullOrEmpty(error);
            var description = iserror ? error : warn;
            if (string.IsNullOrEmpty(description)) return;

            if (iserror) ShowError(description);
            else ShowWarning(description);
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoryName
        ///
        /// <summary>
        /// パスからディレクトリ部分の名前を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            return System.IO.Directory.Exists(path) ? path : System.IO.Path.GetDirectoryName(path);
        }

        #endregion

        #region Variables
        private UserSetting _setting;
        private ComboBox _postproc;
        private ToolTip _tips = new ToolTip();
        private List<CubePdf.Message> _messages = new List<Message>();
        #endregion
    }
}
