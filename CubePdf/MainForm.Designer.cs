namespace CubePdf
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ConvertBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SettingButton = new System.Windows.Forms.Button();
            this.ExecProgressBar = new System.Windows.Forms.ProgressBar();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.GeneralPanel = new System.Windows.Forms.Panel();
            this.InputPathPanel = new System.Windows.Forms.Panel();
            this.InputPathButton = new System.Windows.Forms.Button();
            this.InputPathTextBox = new System.Windows.Forms.TextBox();
            this.OutputPathPanel = new System.Windows.Forms.Panel();
            this.ExistedFileComboBox = new System.Windows.Forms.ComboBox();
            this.OutputPathButton = new System.Windows.Forms.Button();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.PostProcessPanel = new System.Windows.Forms.Panel();
            this.UserProgramTextBox = new System.Windows.Forms.TextBox();
            this.UserProgramButton = new System.Windows.Forms.Button();
            this.PostProcessComboBox = new System.Windows.Forms.ComboBox();
            this.ResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.PdfVersionComboBox = new System.Windows.Forms.ComboBox();
            this.FileTypeCombBox = new System.Windows.Forms.ComboBox();
            this.InputPathLabel = new System.Windows.Forms.Label();
            this.PostProcessLabel = new System.Windows.Forms.Label();
            this.OutputPathLabel = new System.Windows.Forms.Label();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.PDFVersionLabel = new System.Windows.Forms.Label();
            this.FileTypeLabel = new System.Windows.Forms.Label();
            this.DocTabPage = new System.Windows.Forms.TabPage();
            this.DocPanel = new System.Windows.Forms.Panel();
            this.DocTitleLabel = new System.Windows.Forms.Label();
            this.DocAuthorLabel = new System.Windows.Forms.Label();
            this.DocSubtitleLabel = new System.Windows.Forms.Label();
            this.DocKeywordLabel = new System.Windows.Forms.Label();
            this.DocTitleTextBox = new System.Windows.Forms.TextBox();
            this.DocAuthorTextBox = new System.Windows.Forms.TextBox();
            this.DocSubtitleTextBox = new System.Windows.Forms.TextBox();
            this.DocKeywordTextBox = new System.Windows.Forms.TextBox();
            this.SecurityTabPage = new System.Windows.Forms.TabPage();
            this.SecurityGroupBox = new System.Windows.Forms.GroupBox();
            this.SecurityPanel = new System.Windows.Forms.Panel();
            this.UserPasswordPanel = new System.Windows.Forms.Panel();
            this.UserPasswordLabel = new System.Windows.Forms.Label();
            this.ConfirmUserPasswordLabel = new System.Windows.Forms.Label();
            this.UserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmUserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.OwnerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmOwnerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.AllowPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.OwnerPasswordLabel = new System.Windows.Forms.Label();
            this.ConfirmOwnerPasswordLabel = new System.Windows.Forms.Label();
            this.PermissionLabel = new System.Windows.Forms.Label();
            this.AllowFormInputCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowModifyCheckBox = new System.Windows.Forms.CheckBox();
            this.RequiredUserPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.OwnerPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.DetailTabPage = new System.Windows.Forms.TabPage();
            this.DetailPanel = new System.Windows.Forms.Panel();
            this.AutoRadioButton = new System.Windows.Forms.RadioButton();
            this.LandscapeRadioButton = new System.Windows.Forms.RadioButton();
            this.PortraitRadioButton = new System.Windows.Forms.RadioButton();
            this.OrientationLabel = new System.Windows.Forms.Label();
            this.OptionLabel = new System.Windows.Forms.Label();
            this.OthersLabel = new System.Windows.Forms.Label();
            this.EmbedFontCheckBox = new System.Windows.Forms.CheckBox();
            this.GrayscaleCheckBox = new System.Windows.Forms.CheckBox();
            this.ImageFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.WebOptimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.PostProcessLiteLabel = new System.Windows.Forms.Label();
            this.PostProcessLiteComboBox = new System.Windows.Forms.ComboBox();
            this.HeaderPictureBox = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.GeneralPanel.SuspendLayout();
            this.InputPathPanel.SuspendLayout();
            this.OutputPathPanel.SuspendLayout();
            this.PostProcessPanel.SuspendLayout();
            this.DocTabPage.SuspendLayout();
            this.DocPanel.SuspendLayout();
            this.SecurityTabPage.SuspendLayout();
            this.SecurityGroupBox.SuspendLayout();
            this.SecurityPanel.SuspendLayout();
            this.UserPasswordPanel.SuspendLayout();
            this.DetailTabPage.SuspendLayout();
            this.DetailPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ConvertBackgroundWorker
            // 
            this.ConvertBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConvertBackgroundWorker_DoWork);
            this.ConvertBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConvertBackgroundWorker_RunWorkerCompleted);
            // 
            // MainPanel
            // 
            this.MainPanel.BackgroundImage = global::CubePdf.Properties.Resources.Background;
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainPanel.Controls.Add(this.SettingButton);
            this.MainPanel.Controls.Add(this.ExecProgressBar);
            this.MainPanel.Controls.Add(this.ExitButton);
            this.MainPanel.Controls.Add(this.ConvertButton);
            this.MainPanel.Controls.Add(this.MainTabControl);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 80);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(500, 421);
            this.MainPanel.TabIndex = 1;
            // 
            // SettingButton
            // 
            this.SettingButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SettingButton.BackgroundImage = global::CubePdf.Properties.Resources.SettingButton;
            this.SettingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SettingButton.Location = new System.Drawing.Point(12, 378);
            this.SettingButton.Margin = new System.Windows.Forms.Padding(0);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(99, 32);
            this.SettingButton.TabIndex = 3;
            this.SettingButton.UseVisualStyleBackColor = false;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // ExecProgressBar
            // 
            this.ExecProgressBar.Location = new System.Drawing.Point(12, 395);
            this.ExecProgressBar.Name = "ExecProgressBar";
            this.ExecProgressBar.Size = new System.Drawing.Size(200, 15);
            this.ExecProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ExecProgressBar.TabIndex = 4;
            this.ExecProgressBar.Visible = false;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ExitButton.BackgroundImage = global::CubePdf.Properties.Resources.CancelButton;
            this.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitButton.Location = new System.Drawing.Point(369, 362);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(119, 50);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ConvertButton.BackgroundImage = global::CubePdf.Properties.Resources.ConvertButton;
            this.ConvertButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ConvertButton.Location = new System.Drawing.Point(227, 362);
            this.ConvertButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(139, 50);
            this.ConvertButton.TabIndex = 1;
            this.ConvertButton.UseVisualStyleBackColor = false;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.GeneralTabPage);
            this.MainTabControl.Controls.Add(this.DocTabPage);
            this.MainTabControl.Controls.Add(this.SecurityTabPage);
            this.MainTabControl.Controls.Add(this.DetailTabPage);
            this.MainTabControl.HotTrack = true;
            this.MainTabControl.Location = new System.Drawing.Point(12, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(476, 355);
            this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTabControl.TabIndex = 3;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GeneralTabPage.Controls.Add(this.GeneralPanel);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(468, 329);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "一般";
            // 
            // GeneralPanel
            // 
            this.GeneralPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GeneralPanel.Controls.Add(this.InputPathPanel);
            this.GeneralPanel.Controls.Add(this.OutputPathPanel);
            this.GeneralPanel.Controls.Add(this.PostProcessPanel);
            this.GeneralPanel.Controls.Add(this.ResolutionComboBox);
            this.GeneralPanel.Controls.Add(this.PdfVersionComboBox);
            this.GeneralPanel.Controls.Add(this.FileTypeCombBox);
            this.GeneralPanel.Controls.Add(this.InputPathLabel);
            this.GeneralPanel.Controls.Add(this.PostProcessLabel);
            this.GeneralPanel.Controls.Add(this.OutputPathLabel);
            this.GeneralPanel.Controls.Add(this.ResolutionLabel);
            this.GeneralPanel.Controls.Add(this.PDFVersionLabel);
            this.GeneralPanel.Controls.Add(this.FileTypeLabel);
            this.GeneralPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralPanel.Location = new System.Drawing.Point(3, 3);
            this.GeneralPanel.Name = "GeneralPanel";
            this.GeneralPanel.Size = new System.Drawing.Size(462, 323);
            this.GeneralPanel.TabIndex = 1;
            // 
            // InputPathPanel
            // 
            this.InputPathPanel.Controls.Add(this.InputPathButton);
            this.InputPathPanel.Controls.Add(this.InputPathTextBox);
            this.InputPathPanel.Location = new System.Drawing.Point(120, 151);
            this.InputPathPanel.Name = "InputPathPanel";
            this.InputPathPanel.Size = new System.Drawing.Size(316, 20);
            this.InputPathPanel.TabIndex = 26;
            // 
            // InputPathButton
            // 
            this.InputPathButton.BackColor = System.Drawing.SystemColors.Control;
            this.InputPathButton.Location = new System.Drawing.Point(276, 0);
            this.InputPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.InputPathButton.Name = "InputPathButton";
            this.InputPathButton.Size = new System.Drawing.Size(40, 20);
            this.InputPathButton.TabIndex = 20;
            this.InputPathButton.Text = "...";
            this.InputPathButton.UseVisualStyleBackColor = false;
            this.InputPathButton.Click += new System.EventHandler(this.InputPathButton_Click);
            // 
            // InputPathTextBox
            // 
            this.InputPathTextBox.Location = new System.Drawing.Point(0, 1);
            this.InputPathTextBox.Name = "InputPathTextBox";
            this.InputPathTextBox.Size = new System.Drawing.Size(273, 19);
            this.InputPathTextBox.TabIndex = 19;
            this.InputPathTextBox.TextChanged += new System.EventHandler(this.InputPathTextBox_TextChanged);
            this.InputPathTextBox.Leave += new System.EventHandler(this.PathTextBox_Leave);
            // 
            // OutputPathPanel
            // 
            this.OutputPathPanel.Controls.Add(this.ExistedFileComboBox);
            this.OutputPathPanel.Controls.Add(this.OutputPathButton);
            this.OutputPathPanel.Controls.Add(this.OutputPathTextBox);
            this.OutputPathPanel.Location = new System.Drawing.Point(120, 99);
            this.OutputPathPanel.Name = "OutputPathPanel";
            this.OutputPathPanel.Size = new System.Drawing.Size(316, 20);
            this.OutputPathPanel.TabIndex = 12;
            // 
            // ExistedFileComboBox
            // 
            this.ExistedFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistedFileComboBox.FormattingEnabled = true;
            this.ExistedFileComboBox.Location = new System.Drawing.Point(246, 0);
            this.ExistedFileComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.ExistedFileComboBox.Name = "ExistedFileComboBox";
            this.ExistedFileComboBox.Size = new System.Drawing.Size(70, 20);
            this.ExistedFileComboBox.TabIndex = 15;
            this.ExistedFileComboBox.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // OutputPathButton
            // 
            this.OutputPathButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutputPathButton.Location = new System.Drawing.Point(203, 0);
            this.OutputPathButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.OutputPathButton.Name = "OutputPathButton";
            this.OutputPathButton.Size = new System.Drawing.Size(40, 20);
            this.OutputPathButton.TabIndex = 14;
            this.OutputPathButton.Text = "...";
            this.OutputPathButton.UseVisualStyleBackColor = false;
            this.OutputPathButton.Click += new System.EventHandler(this.OutputPathButton_Click);
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Location = new System.Drawing.Point(0, 1);
            this.OutputPathTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(200, 19);
            this.OutputPathTextBox.TabIndex = 13;
            this.OutputPathTextBox.Click += new System.EventHandler(this.OutputPathTextBox_Click);
            this.OutputPathTextBox.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            this.OutputPathTextBox.Leave += new System.EventHandler(this.OutputPathTextBox_Leave);
            // 
            // PostProcessPanel
            // 
            this.PostProcessPanel.Controls.Add(this.UserProgramTextBox);
            this.PostProcessPanel.Controls.Add(this.UserProgramButton);
            this.PostProcessPanel.Controls.Add(this.PostProcessComboBox);
            this.PostProcessPanel.Location = new System.Drawing.Point(120, 125);
            this.PostProcessPanel.Name = "PostProcessPanel";
            this.PostProcessPanel.Size = new System.Drawing.Size(316, 20);
            this.PostProcessPanel.TabIndex = 11;
            // 
            // UserProgramTextBox
            // 
            this.UserProgramTextBox.Location = new System.Drawing.Point(98, 0);
            this.UserProgramTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramTextBox.Name = "UserProgramTextBox";
            this.UserProgramTextBox.Size = new System.Drawing.Size(175, 19);
            this.UserProgramTextBox.TabIndex = 17;
            this.UserProgramTextBox.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            this.UserProgramTextBox.Leave += new System.EventHandler(this.PathTextBox_Leave);
            // 
            // UserProgramButton
            // 
            this.UserProgramButton.BackColor = System.Drawing.SystemColors.Control;
            this.UserProgramButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.UserProgramButton.Location = new System.Drawing.Point(276, 0);
            this.UserProgramButton.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramButton.Name = "UserProgramButton";
            this.UserProgramButton.Size = new System.Drawing.Size(40, 20);
            this.UserProgramButton.TabIndex = 18;
            this.UserProgramButton.Text = "...";
            this.UserProgramButton.UseVisualStyleBackColor = false;
            this.UserProgramButton.Click += new System.EventHandler(this.UserProgramButton_Click);
            // 
            // PostProcessComboBox
            // 
            this.PostProcessComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.PostProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessComboBox.FormattingEnabled = true;
            this.PostProcessComboBox.Location = new System.Drawing.Point(0, 0);
            this.PostProcessComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.PostProcessComboBox.Name = "PostProcessComboBox";
            this.PostProcessComboBox.Size = new System.Drawing.Size(95, 20);
            this.PostProcessComboBox.TabIndex = 16;
            this.PostProcessComboBox.SelectedIndexChanged += new System.EventHandler(this.PostProcessComboBox_SelectedIndexChanged);
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionComboBox.FormattingEnabled = true;
            this.ResolutionComboBox.Location = new System.Drawing.Point(120, 73);
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            this.ResolutionComboBox.Size = new System.Drawing.Size(316, 20);
            this.ResolutionComboBox.TabIndex = 12;
            this.ResolutionComboBox.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // PdfVersionComboBox
            // 
            this.PdfVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PdfVersionComboBox.FormattingEnabled = true;
            this.PdfVersionComboBox.Location = new System.Drawing.Point(120, 47);
            this.PdfVersionComboBox.Name = "PdfVersionComboBox";
            this.PdfVersionComboBox.Size = new System.Drawing.Size(316, 20);
            this.PdfVersionComboBox.TabIndex = 11;
            this.PdfVersionComboBox.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // FileTypeCombBox
            // 
            this.FileTypeCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileTypeCombBox.FormattingEnabled = true;
            this.FileTypeCombBox.Location = new System.Drawing.Point(120, 21);
            this.FileTypeCombBox.Name = "FileTypeCombBox";
            this.FileTypeCombBox.Size = new System.Drawing.Size(316, 20);
            this.FileTypeCombBox.TabIndex = 10;
            this.FileTypeCombBox.SelectedIndexChanged += new System.EventHandler(this.FileTypeCombBox_SelectedIndexChanged);
            // 
            // InputPathLabel
            // 
            this.InputPathLabel.AutoSize = true;
            this.InputPathLabel.Location = new System.Drawing.Point(22, 155);
            this.InputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.InputPathLabel.Name = "InputPathLabel";
            this.InputPathLabel.Size = new System.Drawing.Size(69, 12);
            this.InputPathLabel.TabIndex = 100;
            this.InputPathLabel.Text = "入力ファイル：";
            this.InputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PostProcessLabel
            // 
            this.PostProcessLabel.AutoSize = true;
            this.PostProcessLabel.Location = new System.Drawing.Point(20, 129);
            this.PostProcessLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PostProcessLabel.Name = "PostProcessLabel";
            this.PostProcessLabel.Size = new System.Drawing.Size(75, 12);
            this.PostProcessLabel.TabIndex = 100;
            this.PostProcessLabel.Text = "ポストプロセス：";
            this.PostProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OutputPathLabel
            // 
            this.OutputPathLabel.AutoSize = true;
            this.OutputPathLabel.Location = new System.Drawing.Point(22, 102);
            this.OutputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OutputPathLabel.Name = "OutputPathLabel";
            this.OutputPathLabel.Size = new System.Drawing.Size(69, 12);
            this.OutputPathLabel.TabIndex = 100;
            this.OutputPathLabel.Text = "出力ファイル：";
            this.OutputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(20, 76);
            this.ResolutionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(47, 12);
            this.ResolutionLabel.TabIndex = 100;
            this.ResolutionLabel.Text = "解像度：";
            this.ResolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PDFVersionLabel
            // 
            this.PDFVersionLabel.AutoSize = true;
            this.PDFVersionLabel.Location = new System.Drawing.Point(20, 50);
            this.PDFVersionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PDFVersionLabel.Name = "PDFVersionLabel";
            this.PDFVersionLabel.Size = new System.Drawing.Size(82, 12);
            this.PDFVersionLabel.TabIndex = 100;
            this.PDFVersionLabel.Text = "PDF バージョン：";
            this.PDFVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileTypeLabel
            // 
            this.FileTypeLabel.AutoSize = true;
            this.FileTypeLabel.Location = new System.Drawing.Point(20, 24);
            this.FileTypeLabel.Margin = new System.Windows.Forms.Padding(3);
            this.FileTypeLabel.Name = "FileTypeLabel";
            this.FileTypeLabel.Size = new System.Drawing.Size(71, 12);
            this.FileTypeLabel.TabIndex = 100;
            this.FileTypeLabel.Text = "ファイルタイプ：";
            this.FileTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocTabPage
            // 
            this.DocTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DocTabPage.Controls.Add(this.DocPanel);
            this.DocTabPage.Location = new System.Drawing.Point(4, 22);
            this.DocTabPage.Name = "DocTabPage";
            this.DocTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DocTabPage.Size = new System.Drawing.Size(468, 329);
            this.DocTabPage.TabIndex = 1;
            this.DocTabPage.Text = "文書プロパティ";
            // 
            // DocPanel
            // 
            this.DocPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DocPanel.Controls.Add(this.DocTitleLabel);
            this.DocPanel.Controls.Add(this.DocAuthorLabel);
            this.DocPanel.Controls.Add(this.DocSubtitleLabel);
            this.DocPanel.Controls.Add(this.DocKeywordLabel);
            this.DocPanel.Controls.Add(this.DocTitleTextBox);
            this.DocPanel.Controls.Add(this.DocAuthorTextBox);
            this.DocPanel.Controls.Add(this.DocSubtitleTextBox);
            this.DocPanel.Controls.Add(this.DocKeywordTextBox);
            this.DocPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocPanel.Location = new System.Drawing.Point(3, 3);
            this.DocPanel.Name = "DocPanel";
            this.DocPanel.Size = new System.Drawing.Size(462, 323);
            this.DocPanel.TabIndex = 1;
            // 
            // DocTitleLabel
            // 
            this.DocTitleLabel.AutoSize = true;
            this.DocTitleLabel.Location = new System.Drawing.Point(20, 24);
            this.DocTitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocTitleLabel.Name = "DocTitleLabel";
            this.DocTitleLabel.Size = new System.Drawing.Size(46, 12);
            this.DocTitleLabel.TabIndex = 7;
            this.DocTitleLabel.Text = "タイトル：";
            this.DocTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocAuthorLabel
            // 
            this.DocAuthorLabel.AutoSize = true;
            this.DocAuthorLabel.Location = new System.Drawing.Point(19, 50);
            this.DocAuthorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocAuthorLabel.Name = "DocAuthorLabel";
            this.DocAuthorLabel.Size = new System.Drawing.Size(47, 12);
            this.DocAuthorLabel.TabIndex = 8;
            this.DocAuthorLabel.Text = "作成者：";
            this.DocAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocSubtitleLabel
            // 
            this.DocSubtitleLabel.AutoSize = true;
            this.DocSubtitleLabel.Location = new System.Drawing.Point(19, 76);
            this.DocSubtitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocSubtitleLabel.Name = "DocSubtitleLabel";
            this.DocSubtitleLabel.Size = new System.Drawing.Size(65, 12);
            this.DocSubtitleLabel.TabIndex = 5;
            this.DocSubtitleLabel.Text = "サブタイトル：";
            this.DocSubtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocKeywordLabel
            // 
            this.DocKeywordLabel.AutoSize = true;
            this.DocKeywordLabel.Location = new System.Drawing.Point(20, 102);
            this.DocKeywordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocKeywordLabel.Name = "DocKeywordLabel";
            this.DocKeywordLabel.Size = new System.Drawing.Size(59, 12);
            this.DocKeywordLabel.TabIndex = 100;
            this.DocKeywordLabel.Text = "キーワード：";
            this.DocKeywordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocTitleTextBox
            // 
            this.DocTitleTextBox.Location = new System.Drawing.Point(120, 21);
            this.DocTitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocTitleTextBox.Name = "DocTitleTextBox";
            this.DocTitleTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocTitleTextBox.TabIndex = 30;
            // 
            // DocAuthorTextBox
            // 
            this.DocAuthorTextBox.Location = new System.Drawing.Point(120, 47);
            this.DocAuthorTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocAuthorTextBox.Name = "DocAuthorTextBox";
            this.DocAuthorTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocAuthorTextBox.TabIndex = 31;
            // 
            // DocSubtitleTextBox
            // 
            this.DocSubtitleTextBox.Location = new System.Drawing.Point(120, 73);
            this.DocSubtitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocSubtitleTextBox.Name = "DocSubtitleTextBox";
            this.DocSubtitleTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocSubtitleTextBox.TabIndex = 32;
            // 
            // DocKeywordTextBox
            // 
            this.DocKeywordTextBox.Location = new System.Drawing.Point(120, 99);
            this.DocKeywordTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocKeywordTextBox.Name = "DocKeywordTextBox";
            this.DocKeywordTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocKeywordTextBox.TabIndex = 33;
            // 
            // SecurityTabPage
            // 
            this.SecurityTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SecurityTabPage.Controls.Add(this.SecurityGroupBox);
            this.SecurityTabPage.Location = new System.Drawing.Point(4, 22);
            this.SecurityTabPage.Name = "SecurityTabPage";
            this.SecurityTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SecurityTabPage.Size = new System.Drawing.Size(468, 329);
            this.SecurityTabPage.TabIndex = 2;
            this.SecurityTabPage.Text = "セキュリティ";
            // 
            // SecurityGroupBox
            // 
            this.SecurityGroupBox.Controls.Add(this.SecurityPanel);
            this.SecurityGroupBox.Controls.Add(this.OwnerPasswordCheckBox);
            this.SecurityGroupBox.Location = new System.Drawing.Point(20, 8);
            this.SecurityGroupBox.Name = "SecurityGroupBox";
            this.SecurityGroupBox.Size = new System.Drawing.Size(422, 315);
            this.SecurityGroupBox.TabIndex = 100;
            this.SecurityGroupBox.TabStop = false;
            this.SecurityGroupBox.Text = "セキュリティ";
            // 
            // SecurityPanel
            // 
            this.SecurityPanel.Controls.Add(this.UserPasswordPanel);
            this.SecurityPanel.Controls.Add(this.UserPasswordCheckBox);
            this.SecurityPanel.Controls.Add(this.OwnerPasswordTextBox);
            this.SecurityPanel.Controls.Add(this.ConfirmOwnerPasswordTextBox);
            this.SecurityPanel.Controls.Add(this.AllowPrintCheckBox);
            this.SecurityPanel.Controls.Add(this.AllowCopyCheckBox);
            this.SecurityPanel.Controls.Add(this.OwnerPasswordLabel);
            this.SecurityPanel.Controls.Add(this.ConfirmOwnerPasswordLabel);
            this.SecurityPanel.Controls.Add(this.PermissionLabel);
            this.SecurityPanel.Controls.Add(this.AllowFormInputCheckBox);
            this.SecurityPanel.Controls.Add(this.AllowModifyCheckBox);
            this.SecurityPanel.Controls.Add(this.RequiredUserPasswordCheckBox);
            this.SecurityPanel.Enabled = false;
            this.SecurityPanel.Location = new System.Drawing.Point(1, 42);
            this.SecurityPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SecurityPanel.Name = "SecurityPanel";
            this.SecurityPanel.Size = new System.Drawing.Size(420, 270);
            this.SecurityPanel.TabIndex = 11;
            // 
            // UserPasswordPanel
            // 
            this.UserPasswordPanel.Controls.Add(this.UserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.UserPasswordTextBox);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordTextBox);
            this.UserPasswordPanel.Enabled = false;
            this.UserPasswordPanel.Location = new System.Drawing.Point(138, 100);
            this.UserPasswordPanel.Name = "UserPasswordPanel";
            this.UserPasswordPanel.Size = new System.Drawing.Size(265, 50);
            this.UserPasswordPanel.TabIndex = 37;
            // 
            // UserPasswordLabel
            // 
            this.UserPasswordLabel.AutoSize = true;
            this.UserPasswordLabel.Location = new System.Drawing.Point(2, 6);
            this.UserPasswordLabel.Margin = new System.Windows.Forms.Padding(0);
            this.UserPasswordLabel.Name = "UserPasswordLabel";
            this.UserPasswordLabel.Size = new System.Drawing.Size(58, 12);
            this.UserPasswordLabel.TabIndex = 100;
            this.UserPasswordLabel.Text = "パスワード：";
            this.UserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmUserPasswordLabel
            // 
            this.ConfirmUserPasswordLabel.AutoSize = true;
            this.ConfirmUserPasswordLabel.Location = new System.Drawing.Point(2, 31);
            this.ConfirmUserPasswordLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ConfirmUserPasswordLabel.Name = "ConfirmUserPasswordLabel";
            this.ConfirmUserPasswordLabel.Size = new System.Drawing.Size(92, 12);
            this.ConfirmUserPasswordLabel.TabIndex = 100;
            this.ConfirmUserPasswordLabel.Text = "パスワードの確認：";
            this.ConfirmUserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserPasswordTextBox
            // 
            this.UserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UserPasswordTextBox.Location = new System.Drawing.Point(100, 3);
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            this.UserPasswordTextBox.PasswordChar = '*';
            this.UserPasswordTextBox.Size = new System.Drawing.Size(162, 19);
            this.UserPasswordTextBox.TabIndex = 56;
            this.UserPasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // ConfirmUserPasswordTextBox
            // 
            this.ConfirmUserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmUserPasswordTextBox.Location = new System.Drawing.Point(100, 28);
            this.ConfirmUserPasswordTextBox.Name = "ConfirmUserPasswordTextBox";
            this.ConfirmUserPasswordTextBox.PasswordChar = '*';
            this.ConfirmUserPasswordTextBox.Size = new System.Drawing.Size(162, 19);
            this.ConfirmUserPasswordTextBox.TabIndex = 57;
            this.ConfirmUserPasswordTextBox.TextChanged += new System.EventHandler(this.ConfirmPasswordTextBox_TextChanged);
            // 
            // UserPasswordCheckBox
            // 
            this.UserPasswordCheckBox.AutoSize = true;
            this.UserPasswordCheckBox.Enabled = false;
            this.UserPasswordCheckBox.Location = new System.Drawing.Point(140, 80);
            this.UserPasswordCheckBox.Name = "UserPasswordCheckBox";
            this.UserPasswordCheckBox.Size = new System.Drawing.Size(181, 16);
            this.UserPasswordCheckBox.TabIndex = 55;
            this.UserPasswordCheckBox.Text = "閲覧専用のパスワードを設定する";
            this.UserPasswordCheckBox.UseVisualStyleBackColor = true;
            this.UserPasswordCheckBox.CheckedChanged += new System.EventHandler(this.UserPasswordCheckBox_CheckedChanged);
            // 
            // OwnerPasswordTextBox
            // 
            this.OwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OwnerPasswordTextBox.Location = new System.Drawing.Point(120, 8);
            this.OwnerPasswordTextBox.Name = "OwnerPasswordTextBox";
            this.OwnerPasswordTextBox.PasswordChar = '*';
            this.OwnerPasswordTextBox.Size = new System.Drawing.Size(280, 19);
            this.OwnerPasswordTextBox.TabIndex = 52;
            this.OwnerPasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // ConfirmOwnerPasswordTextBox
            // 
            this.ConfirmOwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmOwnerPasswordTextBox.Location = new System.Drawing.Point(120, 33);
            this.ConfirmOwnerPasswordTextBox.Name = "ConfirmOwnerPasswordTextBox";
            this.ConfirmOwnerPasswordTextBox.PasswordChar = '*';
            this.ConfirmOwnerPasswordTextBox.Size = new System.Drawing.Size(280, 19);
            this.ConfirmOwnerPasswordTextBox.TabIndex = 53;
            this.ConfirmOwnerPasswordTextBox.TextChanged += new System.EventHandler(this.ConfirmPasswordTextBox_TextChanged);
            // 
            // AllowPrintCheckBox
            // 
            this.AllowPrintCheckBox.AutoSize = true;
            this.AllowPrintCheckBox.Location = new System.Drawing.Point(122, 154);
            this.AllowPrintCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowPrintCheckBox.Name = "AllowPrintCheckBox";
            this.AllowPrintCheckBox.Size = new System.Drawing.Size(100, 16);
            this.AllowPrintCheckBox.TabIndex = 58;
            this.AllowPrintCheckBox.Text = "印刷を許可する";
            this.AllowPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowCopyCheckBox
            // 
            this.AllowCopyCheckBox.AutoSize = true;
            this.AllowCopyCheckBox.Location = new System.Drawing.Point(122, 180);
            this.AllowCopyCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowCopyCheckBox.Name = "AllowCopyCheckBox";
            this.AllowCopyCheckBox.Size = new System.Drawing.Size(183, 16);
            this.AllowCopyCheckBox.TabIndex = 59;
            this.AllowCopyCheckBox.Text = "テキストや画像のコピーを許可する";
            this.AllowCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordLabel
            // 
            this.OwnerPasswordLabel.AutoSize = true;
            this.OwnerPasswordLabel.Location = new System.Drawing.Point(17, 11);
            this.OwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OwnerPasswordLabel.Name = "OwnerPasswordLabel";
            this.OwnerPasswordLabel.Size = new System.Drawing.Size(58, 12);
            this.OwnerPasswordLabel.TabIndex = 100;
            this.OwnerPasswordLabel.Text = "パスワード：";
            this.OwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmOwnerPasswordLabel
            // 
            this.ConfirmOwnerPasswordLabel.AutoSize = true;
            this.ConfirmOwnerPasswordLabel.Location = new System.Drawing.Point(17, 36);
            this.ConfirmOwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.ConfirmOwnerPasswordLabel.Name = "ConfirmOwnerPasswordLabel";
            this.ConfirmOwnerPasswordLabel.Size = new System.Drawing.Size(92, 12);
            this.ConfirmOwnerPasswordLabel.TabIndex = 100;
            this.ConfirmOwnerPasswordLabel.Text = "パスワードの確認：";
            this.ConfirmOwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PermissionLabel
            // 
            this.PermissionLabel.AutoSize = true;
            this.PermissionLabel.Location = new System.Drawing.Point(17, 59);
            this.PermissionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PermissionLabel.Name = "PermissionLabel";
            this.PermissionLabel.Size = new System.Drawing.Size(35, 12);
            this.PermissionLabel.TabIndex = 100;
            this.PermissionLabel.Text = "操作：";
            this.PermissionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AllowFormInputCheckBox
            // 
            this.AllowFormInputCheckBox.AutoSize = true;
            this.AllowFormInputCheckBox.Location = new System.Drawing.Point(122, 206);
            this.AllowFormInputCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowFormInputCheckBox.Name = "AllowFormInputCheckBox";
            this.AllowFormInputCheckBox.Size = new System.Drawing.Size(200, 16);
            this.AllowFormInputCheckBox.TabIndex = 60;
            this.AllowFormInputCheckBox.Text = "フォームフィールドへの入力を許可する";
            this.AllowFormInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowModifyCheckBox
            // 
            this.AllowModifyCheckBox.AutoSize = true;
            this.AllowModifyCheckBox.Location = new System.Drawing.Point(122, 232);
            this.AllowModifyCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowModifyCheckBox.Name = "AllowModifyCheckBox";
            this.AllowModifyCheckBox.Size = new System.Drawing.Size(233, 16);
            this.AllowModifyCheckBox.TabIndex = 61;
            this.AllowModifyCheckBox.Text = "ページの挿入、回転、および削除を許可する";
            this.AllowModifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // RequiredUserPasswordCheckBox
            // 
            this.RequiredUserPasswordCheckBox.AutoSize = true;
            this.RequiredUserPasswordCheckBox.Location = new System.Drawing.Point(120, 58);
            this.RequiredUserPasswordCheckBox.Name = "RequiredUserPasswordCheckBox";
            this.RequiredUserPasswordCheckBox.Size = new System.Drawing.Size(227, 16);
            this.RequiredUserPasswordCheckBox.TabIndex = 54;
            this.RequiredUserPasswordCheckBox.Text = "PDFファイルを開く際にパスワードを要求する";
            this.RequiredUserPasswordCheckBox.UseVisualStyleBackColor = true;
            this.RequiredUserPasswordCheckBox.CheckedChanged += new System.EventHandler(this.RequiredUserPasswordCheckBox_CheckedChanged);
            // 
            // OwnerPasswordCheckBox
            // 
            this.OwnerPasswordCheckBox.AutoSize = true;
            this.OwnerPasswordCheckBox.Location = new System.Drawing.Point(20, 24);
            this.OwnerPasswordCheckBox.Name = "OwnerPasswordCheckBox";
            this.OwnerPasswordCheckBox.Size = new System.Drawing.Size(201, 16);
            this.OwnerPasswordCheckBox.TabIndex = 51;
            this.OwnerPasswordCheckBox.Text = "パスワードによるセキュリティを設定する";
            this.OwnerPasswordCheckBox.UseVisualStyleBackColor = true;
            this.OwnerPasswordCheckBox.CheckedChanged += new System.EventHandler(this.OwnerPasswordCheckBox_CheckedChanged);
            // 
            // DetailTabPage
            // 
            this.DetailTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetailTabPage.Controls.Add(this.DetailPanel);
            this.DetailTabPage.Location = new System.Drawing.Point(4, 22);
            this.DetailTabPage.Name = "DetailTabPage";
            this.DetailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DetailTabPage.Size = new System.Drawing.Size(468, 329);
            this.DetailTabPage.TabIndex = 3;
            this.DetailTabPage.Text = "詳細設定";
            // 
            // DetailPanel
            // 
            this.DetailPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetailPanel.Controls.Add(this.AutoRadioButton);
            this.DetailPanel.Controls.Add(this.LandscapeRadioButton);
            this.DetailPanel.Controls.Add(this.PortraitRadioButton);
            this.DetailPanel.Controls.Add(this.OrientationLabel);
            this.DetailPanel.Controls.Add(this.OptionLabel);
            this.DetailPanel.Controls.Add(this.OthersLabel);
            this.DetailPanel.Controls.Add(this.EmbedFontCheckBox);
            this.DetailPanel.Controls.Add(this.GrayscaleCheckBox);
            this.DetailPanel.Controls.Add(this.ImageFilterCheckBox);
            this.DetailPanel.Controls.Add(this.WebOptimizeCheckBox);
            this.DetailPanel.Controls.Add(this.UpdateCheckBox);
            this.DetailPanel.Controls.Add(this.PostProcessLiteLabel);
            this.DetailPanel.Controls.Add(this.PostProcessLiteComboBox);
            this.DetailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailPanel.Location = new System.Drawing.Point(3, 3);
            this.DetailPanel.Name = "DetailPanel";
            this.DetailPanel.Size = new System.Drawing.Size(462, 323);
            this.DetailPanel.TabIndex = 0;
            // 
            // AutoRadioButton
            // 
            this.AutoRadioButton.AutoSize = true;
            this.AutoRadioButton.Checked = true;
            this.AutoRadioButton.Location = new System.Drawing.Point(240, 22);
            this.AutoRadioButton.Name = "AutoRadioButton";
            this.AutoRadioButton.Size = new System.Drawing.Size(47, 16);
            this.AutoRadioButton.TabIndex = 73;
            this.AutoRadioButton.TabStop = true;
            this.AutoRadioButton.Text = "自動";
            this.AutoRadioButton.UseVisualStyleBackColor = true;
            this.AutoRadioButton.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // LandscapeRadioButton
            // 
            this.LandscapeRadioButton.AutoSize = true;
            this.LandscapeRadioButton.Location = new System.Drawing.Point(180, 22);
            this.LandscapeRadioButton.Name = "LandscapeRadioButton";
            this.LandscapeRadioButton.Size = new System.Drawing.Size(35, 16);
            this.LandscapeRadioButton.TabIndex = 72;
            this.LandscapeRadioButton.Text = "横";
            this.LandscapeRadioButton.UseVisualStyleBackColor = true;
            this.LandscapeRadioButton.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // PortraitRadioButton
            // 
            this.PortraitRadioButton.AutoSize = true;
            this.PortraitRadioButton.Location = new System.Drawing.Point(120, 22);
            this.PortraitRadioButton.Name = "PortraitRadioButton";
            this.PortraitRadioButton.Size = new System.Drawing.Size(35, 16);
            this.PortraitRadioButton.TabIndex = 71;
            this.PortraitRadioButton.Text = "縦";
            this.PortraitRadioButton.UseVisualStyleBackColor = true;
            this.PortraitRadioButton.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.Location = new System.Drawing.Point(20, 24);
            this.OrientationLabel.Name = "OrientationLabel";
            this.OrientationLabel.Size = new System.Drawing.Size(72, 12);
            this.OrientationLabel.TabIndex = 100;
            this.OrientationLabel.Text = "ページの向き：";
            // 
            // OptionLabel
            // 
            this.OptionLabel.AutoSize = true;
            this.OptionLabel.Location = new System.Drawing.Point(20, 50);
            this.OptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OptionLabel.Name = "OptionLabel";
            this.OptionLabel.Size = new System.Drawing.Size(54, 12);
            this.OptionLabel.TabIndex = 100;
            this.OptionLabel.Text = "オプション：";
            this.OptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OthersLabel
            // 
            this.OthersLabel.AutoSize = true;
            this.OthersLabel.Location = new System.Drawing.Point(20, 154);
            this.OthersLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OthersLabel.Name = "OthersLabel";
            this.OthersLabel.Size = new System.Drawing.Size(42, 12);
            this.OthersLabel.TabIndex = 100;
            this.OthersLabel.Text = "その他：";
            this.OthersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmbedFontCheckBox
            // 
            this.EmbedFontCheckBox.AutoSize = true;
            this.EmbedFontCheckBox.Location = new System.Drawing.Point(120, 49);
            this.EmbedFontCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.EmbedFontCheckBox.Name = "EmbedFontCheckBox";
            this.EmbedFontCheckBox.Size = new System.Drawing.Size(112, 16);
            this.EmbedFontCheckBox.TabIndex = 74;
            this.EmbedFontCheckBox.Text = "フォントの埋め込み";
            this.EmbedFontCheckBox.UseVisualStyleBackColor = true;
            this.EmbedFontCheckBox.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // GrayscaleCheckBox
            // 
            this.GrayscaleCheckBox.AutoSize = true;
            this.GrayscaleCheckBox.Location = new System.Drawing.Point(120, 75);
            this.GrayscaleCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.GrayscaleCheckBox.Name = "GrayscaleCheckBox";
            this.GrayscaleCheckBox.Size = new System.Drawing.Size(90, 16);
            this.GrayscaleCheckBox.TabIndex = 75;
            this.GrayscaleCheckBox.Text = "グレースケール";
            this.GrayscaleCheckBox.UseVisualStyleBackColor = true;
            this.GrayscaleCheckBox.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // ImageFilterCheckBox
            // 
            this.ImageFilterCheckBox.AutoSize = true;
            this.ImageFilterCheckBox.Location = new System.Drawing.Point(120, 101);
            this.ImageFilterCheckBox.Name = "ImageFilterCheckBox";
            this.ImageFilterCheckBox.Size = new System.Drawing.Size(143, 16);
            this.ImageFilterCheckBox.TabIndex = 76;
            this.ImageFilterCheckBox.Text = "画像をJPEG形式に圧縮";
            this.ImageFilterCheckBox.UseVisualStyleBackColor = true;
            this.ImageFilterCheckBox.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // WebOptimizeCheckBox
            // 
            this.WebOptimizeCheckBox.AutoSize = true;
            this.WebOptimizeCheckBox.Location = new System.Drawing.Point(120, 127);
            this.WebOptimizeCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.WebOptimizeCheckBox.Name = "WebOptimizeCheckBox";
            this.WebOptimizeCheckBox.Size = new System.Drawing.Size(126, 16);
            this.WebOptimizeCheckBox.TabIndex = 77;
            this.WebOptimizeCheckBox.Text = "Web表示用に最適化";
            this.WebOptimizeCheckBox.UseVisualStyleBackColor = true;
            this.WebOptimizeCheckBox.CheckedChanged += new System.EventHandler(this.WebOptimizeCheckBox_CheckedChanged);
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.AutoSize = true;
            this.UpdateCheckBox.Location = new System.Drawing.Point(120, 153);
            this.UpdateCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(174, 16);
            this.UpdateCheckBox.TabIndex = 78;
            this.UpdateCheckBox.Text = "起動時にアップデートを確認する";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            this.UpdateCheckBox.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // PostProcessLiteLabel
            // 
            this.PostProcessLiteLabel.AutoSize = true;
            this.PostProcessLiteLabel.Location = new System.Drawing.Point(20, 184);
            this.PostProcessLiteLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PostProcessLiteLabel.Name = "PostProcessLiteLabel";
            this.PostProcessLiteLabel.Size = new System.Drawing.Size(75, 12);
            this.PostProcessLiteLabel.TabIndex = 100;
            this.PostProcessLiteLabel.Text = "ポストプロセス：";
            this.PostProcessLiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PostProcessLiteComboBox
            // 
            this.PostProcessLiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessLiteComboBox.FormattingEnabled = true;
            this.PostProcessLiteComboBox.Location = new System.Drawing.Point(120, 181);
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            this.PostProcessLiteComboBox.Size = new System.Drawing.Size(316, 20);
            this.PostProcessLiteComboBox.TabIndex = 79;
            this.PostProcessLiteComboBox.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // HeaderPictureBox
            // 
            this.HeaderPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPictureBox.Image = global::CubePdf.Properties.Resources.Header;
            this.HeaderPictureBox.Location = new System.Drawing.Point(0, 0);
            this.HeaderPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderPictureBox.Name = "HeaderPictureBox";
            this.HeaderPictureBox.Size = new System.Drawing.Size(500, 80);
            this.HeaderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HeaderPictureBox.TabIndex = 0;
            this.HeaderPictureBox.TabStop = false;
            this.HeaderPictureBox.Click += new System.EventHandler(this.HeaderPictureBox_Click);
            this.HeaderPictureBox.MouseEnter += new System.EventHandler(this.HeaderPictureBox_MouseEnter);
            this.HeaderPictureBox.MouseLeave += new System.EventHandler(this.HeaderPictureBox_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(500, 501);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.HeaderPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubePDF";
            this.MainPanel.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.GeneralTabPage.ResumeLayout(false);
            this.GeneralPanel.ResumeLayout(false);
            this.GeneralPanel.PerformLayout();
            this.InputPathPanel.ResumeLayout(false);
            this.InputPathPanel.PerformLayout();
            this.OutputPathPanel.ResumeLayout(false);
            this.OutputPathPanel.PerformLayout();
            this.PostProcessPanel.ResumeLayout(false);
            this.PostProcessPanel.PerformLayout();
            this.DocTabPage.ResumeLayout(false);
            this.DocPanel.ResumeLayout(false);
            this.DocPanel.PerformLayout();
            this.SecurityTabPage.ResumeLayout(false);
            this.SecurityGroupBox.ResumeLayout(false);
            this.SecurityGroupBox.PerformLayout();
            this.SecurityPanel.ResumeLayout(false);
            this.SecurityPanel.PerformLayout();
            this.UserPasswordPanel.ResumeLayout(false);
            this.UserPasswordPanel.PerformLayout();
            this.DetailTabPage.ResumeLayout(false);
            this.DetailPanel.ResumeLayout(false);
            this.DetailPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HeaderPictureBox;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage DocTabPage;
        private System.Windows.Forms.TabPage SecurityTabPage;
        private System.Windows.Forms.TabPage DetailTabPage;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ProgressBar ExecProgressBar;
        private System.Windows.Forms.GroupBox SecurityGroupBox;
        private System.ComponentModel.BackgroundWorker ConvertBackgroundWorker;
        private System.Windows.Forms.Panel GeneralPanel;
        private System.Windows.Forms.Panel InputPathPanel;
        private System.Windows.Forms.Button InputPathButton;
        private System.Windows.Forms.TextBox InputPathTextBox;
        private System.Windows.Forms.Panel OutputPathPanel;
        private System.Windows.Forms.ComboBox ExistedFileComboBox;
        private System.Windows.Forms.Button OutputPathButton;
        private System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.Panel PostProcessPanel;
        private System.Windows.Forms.TextBox UserProgramTextBox;
        private System.Windows.Forms.Button UserProgramButton;
        private System.Windows.Forms.ComboBox PostProcessComboBox;
        private System.Windows.Forms.ComboBox ResolutionComboBox;
        private System.Windows.Forms.ComboBox PdfVersionComboBox;
        private System.Windows.Forms.ComboBox FileTypeCombBox;
        private System.Windows.Forms.Label InputPathLabel;
        private System.Windows.Forms.Label PostProcessLabel;
        private System.Windows.Forms.Label OutputPathLabel;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.Label PDFVersionLabel;
        private System.Windows.Forms.Label FileTypeLabel;
        private System.Windows.Forms.Panel DocPanel;
        private System.Windows.Forms.Label DocTitleLabel;
        private System.Windows.Forms.Label DocAuthorLabel;
        private System.Windows.Forms.Label DocSubtitleLabel;
        private System.Windows.Forms.Label DocKeywordLabel;
        private System.Windows.Forms.TextBox DocTitleTextBox;
        private System.Windows.Forms.TextBox DocAuthorTextBox;
        private System.Windows.Forms.TextBox DocSubtitleTextBox;
        private System.Windows.Forms.TextBox DocKeywordTextBox;
        private System.Windows.Forms.Panel DetailPanel;
        private System.Windows.Forms.Label OptionLabel;
        private System.Windows.Forms.Label OthersLabel;
        private System.Windows.Forms.CheckBox EmbedFontCheckBox;
        private System.Windows.Forms.CheckBox GrayscaleCheckBox;
        private System.Windows.Forms.CheckBox ImageFilterCheckBox;
        private System.Windows.Forms.CheckBox WebOptimizeCheckBox;
        private System.Windows.Forms.CheckBox UpdateCheckBox;
        private System.Windows.Forms.Label PostProcessLiteLabel;
        private System.Windows.Forms.ComboBox PostProcessLiteComboBox;
        private System.Windows.Forms.CheckBox OwnerPasswordCheckBox;
        private System.Windows.Forms.Panel SecurityPanel;
        private System.Windows.Forms.CheckBox UserPasswordCheckBox;
        private System.Windows.Forms.TextBox OwnerPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmOwnerPasswordTextBox;
        private System.Windows.Forms.CheckBox AllowPrintCheckBox;
        private System.Windows.Forms.CheckBox AllowCopyCheckBox;
        private System.Windows.Forms.Label OwnerPasswordLabel;
        private System.Windows.Forms.Label ConfirmOwnerPasswordLabel;
        private System.Windows.Forms.Label PermissionLabel;
        private System.Windows.Forms.CheckBox AllowFormInputCheckBox;
        private System.Windows.Forms.CheckBox AllowModifyCheckBox;
        private System.Windows.Forms.CheckBox RequiredUserPasswordCheckBox;
        private System.Windows.Forms.Panel UserPasswordPanel;
        private System.Windows.Forms.Label UserPasswordLabel;
        private System.Windows.Forms.Label ConfirmUserPasswordLabel;
        private System.Windows.Forms.TextBox UserPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmUserPasswordTextBox;
        private System.Windows.Forms.Button SettingButton;
        private System.Windows.Forms.Label OrientationLabel;
        private System.Windows.Forms.RadioButton AutoRadioButton;
        private System.Windows.Forms.RadioButton LandscapeRadioButton;
        private System.Windows.Forms.RadioButton PortraitRadioButton;
    }
}

