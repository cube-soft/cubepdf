namespace CubePDF
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
            this.HeaderPictureBox = new System.Windows.Forms.PictureBox();
            this._MainPanel = new System.Windows.Forms.Panel();
            this.ExecProgressBar = new System.Windows.Forms.ProgressBar();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.GeneralTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._FileTypeLabel = new System.Windows.Forms.Label();
            this._PDFVersionLabel = new System.Windows.Forms.Label();
            this.FileTypeCombBox = new System.Windows.Forms.ComboBox();
            this.PDFVersionComboBox = new System.Windows.Forms.ComboBox();
            this._ResolutionLabel = new System.Windows.Forms.Label();
            this._OutputPathLabel = new System.Windows.Forms.Label();
            this.ResolutionComboBox = new System.Windows.Forms.ComboBox();
            this._OutputPathPanel = new System.Windows.Forms.Panel();
            this.OutputPathButton = new System.Windows.Forms.Button();
            this.ExistedFileComboBox = new System.Windows.Forms.ComboBox();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this._PostProcessLabel = new System.Windows.Forms.Label();
            this._InputPathLabel = new System.Windows.Forms.Label();
            this._PostProcessPanel = new System.Windows.Forms.Panel();
            this.UserProgramTextBox = new System.Windows.Forms.TextBox();
            this.UserProgramButton = new System.Windows.Forms.Button();
            this.PostProcessComboBox = new System.Windows.Forms.ComboBox();
            this._InputpathPanel = new System.Windows.Forms.Panel();
            this.InputPathTextBox = new System.Windows.Forms.TextBox();
            this.InputPathButton = new System.Windows.Forms.Button();
            this.DocTabPage = new System.Windows.Forms.TabPage();
            this.DocTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._DocTitleLabel = new System.Windows.Forms.Label();
            this._DocAuthorLabel = new System.Windows.Forms.Label();
            this._DocSubtitleLabel = new System.Windows.Forms.Label();
            this._DocKeywordLabel = new System.Windows.Forms.Label();
            this.DocTitleTextBox = new System.Windows.Forms.TextBox();
            this.DocAuthorTextBox = new System.Windows.Forms.TextBox();
            this.DocSubtitleTextBox = new System.Windows.Forms.TextBox();
            this.DocKeywordTextBox = new System.Windows.Forms.TextBox();
            this.SecurityTabPage = new System.Windows.Forms.TabPage();
            this.OwnerPasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.OwnerPasswordTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._OwnerPasswordLabel = new System.Windows.Forms.Label();
            this._ConfirmOwnerPasswordLabel = new System.Windows.Forms.Label();
            this._PermissionLabel = new System.Windows.Forms.Label();
            this.OwnerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmOwnerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.AllowPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowFormInputCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowModifyCheckBox = new System.Windows.Forms.CheckBox();
            this.OwnerPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.UserPasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.UserPasswordTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._UserPasswordLabel = new System.Windows.Forms.Label();
            this._ConfirmUserPasswordLabel = new System.Windows.Forms.Label();
            this.UserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmUserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.DetailTabPage = new System.Windows.Forms.TabPage();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._DownSamplingLabel = new System.Windows.Forms.Label();
            this._OptionLabel = new System.Windows.Forms.Label();
            this._OthersLabel = new System.Windows.Forms.Label();
            this.DownSamplingComboBox = new System.Windows.Forms.ComboBox();
            this.PageLotationCheckBox = new System.Windows.Forms.CheckBox();
            this.EmbedFontCheckBox = new System.Windows.Forms.CheckBox();
            this.GrayscaleCheckBox = new System.Windows.Forms.CheckBox();
            this.WebOptimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveSettingCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateCheckBox = new System.Windows.Forms.CheckBox();
            this._PostProcessLiteLabel = new System.Windows.Forms.Label();
            this.PostProcessLiteComboBox = new System.Windows.Forms.ComboBox();
            this.ConvertBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this._patch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).BeginInit();
            this._MainPanel.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.GeneralTableLayoutPanel.SuspendLayout();
            this._OutputPathPanel.SuspendLayout();
            this._PostProcessPanel.SuspendLayout();
            this._InputpathPanel.SuspendLayout();
            this.DocTabPage.SuspendLayout();
            this.DocTableLayoutPanel.SuspendLayout();
            this.SecurityTabPage.SuspendLayout();
            this.OwnerPasswordGroupBox.SuspendLayout();
            this.OwnerPasswordTableLayoutPanel.SuspendLayout();
            this.UserPasswordGroupBox.SuspendLayout();
            this.UserPasswordTableLayoutPanel.SuspendLayout();
            this.DetailTabPage.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._patch)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPictureBox
            // 
            this.HeaderPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPictureBox.Image = global::CubePDF.Properties.Resources.header;
            this.HeaderPictureBox.Location = new System.Drawing.Point(0, 0);
            this.HeaderPictureBox.Name = "HeaderPictureBox";
            this.HeaderPictureBox.Size = new System.Drawing.Size(494, 80);
            this.HeaderPictureBox.TabIndex = 0;
            this.HeaderPictureBox.TabStop = false;
            this.HeaderPictureBox.Click += new System.EventHandler(this.HeaderPictureBox_Click);
            this.HeaderPictureBox.MouseEnter += new System.EventHandler(this.HeaderPictureBox_MouseEnter);
            this.HeaderPictureBox.MouseLeave += new System.EventHandler(this.HeaderPictureBox_MouseLeave);
            // 
            // _MainPanel
            // 
            this._MainPanel.BackgroundImage = global::CubePDF.Properties.Resources.background;
            this._MainPanel.Controls.Add(this.ExecProgressBar);
            this._MainPanel.Controls.Add(this.ExitButton);
            this._MainPanel.Controls.Add(this.ConvertButton);
            this._MainPanel.Controls.Add(this.MainTabControl);
            this._MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainPanel.Location = new System.Drawing.Point(0, 80);
            this._MainPanel.Name = "_MainPanel";
            this._MainPanel.Size = new System.Drawing.Size(494, 421);
            this._MainPanel.TabIndex = 1;
            // 
            // ExecProgressBar
            // 
            this.ExecProgressBar.Location = new System.Drawing.Point(12, 395);
            this.ExecProgressBar.Name = "ExecProgressBar";
            this.ExecProgressBar.Size = new System.Drawing.Size(200, 15);
            this.ExecProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ExecProgressBar.TabIndex = 3;
            this.ExecProgressBar.Visible = false;
            // 
            // ExitButton
            // 
            this.ExitButton.BackgroundImage = global::CubePDF.Properties.Resources.buttion_cancel;
            this.ExitButton.Location = new System.Drawing.Point(365, 362);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(117, 49);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.BackgroundImage = global::CubePDF.Properties.Resources.buttion_convert;
            this.ConvertButton.Location = new System.Drawing.Point(222, 362);
            this.ConvertButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(137, 49);
            this.ConvertButton.TabIndex = 1;
            this.ConvertButton.UseVisualStyleBackColor = true;
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
            this.MainTabControl.Size = new System.Drawing.Size(470, 355);
            this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTabControl.TabIndex = 3;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.BackColor = System.Drawing.Color.White;
            this.GeneralTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.GeneralTabPage.Controls.Add(this.GeneralTableLayoutPanel);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(462, 329);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "一般";
            // 
            // GeneralTableLayoutPanel
            // 
            this.GeneralTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.GeneralTableLayoutPanel.ColumnCount = 2;
            this.GeneralTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.GeneralTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.GeneralTableLayoutPanel.Controls.Add(this._FileTypeLabel, 0, 0);
            this.GeneralTableLayoutPanel.Controls.Add(this._PDFVersionLabel, 0, 1);
            this.GeneralTableLayoutPanel.Controls.Add(this.FileTypeCombBox, 1, 0);
            this.GeneralTableLayoutPanel.Controls.Add(this.PDFVersionComboBox, 1, 1);
            this.GeneralTableLayoutPanel.Controls.Add(this._ResolutionLabel, 0, 2);
            this.GeneralTableLayoutPanel.Controls.Add(this._OutputPathLabel, 0, 3);
            this.GeneralTableLayoutPanel.Controls.Add(this.ResolutionComboBox, 1, 2);
            this.GeneralTableLayoutPanel.Controls.Add(this._OutputPathPanel, 1, 3);
            this.GeneralTableLayoutPanel.Controls.Add(this._PostProcessLabel, 0, 4);
            this.GeneralTableLayoutPanel.Controls.Add(this._InputPathLabel, 0, 5);
            this.GeneralTableLayoutPanel.Controls.Add(this._PostProcessPanel, 1, 4);
            this.GeneralTableLayoutPanel.Controls.Add(this._InputpathPanel, 1, 5);
            this.GeneralTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.GeneralTableLayoutPanel.Location = new System.Drawing.Point(20, 20);
            this.GeneralTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.GeneralTableLayoutPanel.Name = "GeneralTableLayoutPanel";
            this.GeneralTableLayoutPanel.RowCount = 6;
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.GeneralTableLayoutPanel.Size = new System.Drawing.Size(422, 156);
            this.GeneralTableLayoutPanel.TabIndex = 0;
            // 
            // _FileTypeLabel
            // 
            this._FileTypeLabel.AutoSize = true;
            this._FileTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FileTypeLabel.Location = new System.Drawing.Point(3, 3);
            this._FileTypeLabel.Margin = new System.Windows.Forms.Padding(3);
            this._FileTypeLabel.Name = "_FileTypeLabel";
            this._FileTypeLabel.Size = new System.Drawing.Size(94, 20);
            this._FileTypeLabel.TabIndex = 0;
            this._FileTypeLabel.Text = "ファイルタイプ：";
            this._FileTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _PDFVersionLabel
            // 
            this._PDFVersionLabel.AutoSize = true;
            this._PDFVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PDFVersionLabel.Location = new System.Drawing.Point(3, 29);
            this._PDFVersionLabel.Margin = new System.Windows.Forms.Padding(3);
            this._PDFVersionLabel.Name = "_PDFVersionLabel";
            this._PDFVersionLabel.Size = new System.Drawing.Size(94, 20);
            this._PDFVersionLabel.TabIndex = 0;
            this._PDFVersionLabel.Text = "PDF バージョン：";
            this._PDFVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileTypeCombBox
            // 
            this.FileTypeCombBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTypeCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileTypeCombBox.FormattingEnabled = true;
            this.FileTypeCombBox.Location = new System.Drawing.Point(103, 3);
            this.FileTypeCombBox.Name = "FileTypeCombBox";
            this.FileTypeCombBox.Size = new System.Drawing.Size(316, 20);
            this.FileTypeCombBox.TabIndex = 1;
            this.FileTypeCombBox.SelectedIndexChanged += new System.EventHandler(this.FileTypeCombBox_SelectedIndexChanged);
            // 
            // PDFVersionComboBox
            // 
            this.PDFVersionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDFVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PDFVersionComboBox.FormattingEnabled = true;
            this.PDFVersionComboBox.Location = new System.Drawing.Point(103, 29);
            this.PDFVersionComboBox.Name = "PDFVersionComboBox";
            this.PDFVersionComboBox.Size = new System.Drawing.Size(316, 20);
            this.PDFVersionComboBox.TabIndex = 2;
            // 
            // _ResolutionLabel
            // 
            this._ResolutionLabel.AutoSize = true;
            this._ResolutionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._ResolutionLabel.Location = new System.Drawing.Point(3, 55);
            this._ResolutionLabel.Margin = new System.Windows.Forms.Padding(3);
            this._ResolutionLabel.Name = "_ResolutionLabel";
            this._ResolutionLabel.Size = new System.Drawing.Size(47, 20);
            this._ResolutionLabel.TabIndex = 0;
            this._ResolutionLabel.Text = "解像度：";
            this._ResolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _OutputPathLabel
            // 
            this._OutputPathLabel.AutoSize = true;
            this._OutputPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OutputPathLabel.Location = new System.Drawing.Point(3, 81);
            this._OutputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this._OutputPathLabel.Name = "_OutputPathLabel";
            this._OutputPathLabel.Size = new System.Drawing.Size(94, 20);
            this._OutputPathLabel.TabIndex = 0;
            this._OutputPathLabel.Text = "出力ファイル：";
            this._OutputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionComboBox.FormattingEnabled = true;
            this.ResolutionComboBox.Location = new System.Drawing.Point(103, 55);
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            this.ResolutionComboBox.Size = new System.Drawing.Size(316, 20);
            this.ResolutionComboBox.TabIndex = 3;
            // 
            // _OutputPathPanel
            // 
            this._OutputPathPanel.Controls.Add(this.OutputPathButton);
            this._OutputPathPanel.Controls.Add(this.ExistedFileComboBox);
            this._OutputPathPanel.Controls.Add(this.OutputPathTextBox);
            this._OutputPathPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OutputPathPanel.Location = new System.Drawing.Point(103, 81);
            this._OutputPathPanel.Name = "_OutputPathPanel";
            this._OutputPathPanel.Size = new System.Drawing.Size(316, 20);
            this._OutputPathPanel.TabIndex = 7;
            // 
            // OutputPathButton
            // 
            this.OutputPathButton.BackColor = System.Drawing.Color.Transparent;
            this.OutputPathButton.Location = new System.Drawing.Point(208, 0);
            this.OutputPathButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.OutputPathButton.Name = "OutputPathButton";
            this.OutputPathButton.Size = new System.Drawing.Size(35, 20);
            this.OutputPathButton.TabIndex = 5;
            this.OutputPathButton.Text = "...";
            this.OutputPathButton.UseVisualStyleBackColor = false;
            this.OutputPathButton.Click += new System.EventHandler(this.OutputPathButton_Click);
            // 
            // ExistedFileComboBox
            // 
            this.ExistedFileComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExistedFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistedFileComboBox.FormattingEnabled = true;
            this.ExistedFileComboBox.Location = new System.Drawing.Point(246, 0);
            this.ExistedFileComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.ExistedFileComboBox.Name = "ExistedFileComboBox";
            this.ExistedFileComboBox.Size = new System.Drawing.Size(70, 20);
            this.ExistedFileComboBox.TabIndex = 6;
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.OutputPathTextBox.Location = new System.Drawing.Point(0, 0);
            this.OutputPathTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(205, 19);
            this.OutputPathTextBox.TabIndex = 4;
            this.OutputPathTextBox.Click += new System.EventHandler(this.OutputPathTextBox_Click);
            this.OutputPathTextBox.Leave += new System.EventHandler(this.OutputPathTextBox_Leave);
            // 
            // _PostProcessLabel
            // 
            this._PostProcessLabel.AutoSize = true;
            this._PostProcessLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._PostProcessLabel.Location = new System.Drawing.Point(3, 107);
            this._PostProcessLabel.Margin = new System.Windows.Forms.Padding(3);
            this._PostProcessLabel.Name = "_PostProcessLabel";
            this._PostProcessLabel.Size = new System.Drawing.Size(75, 20);
            this._PostProcessLabel.TabIndex = 0;
            this._PostProcessLabel.Text = "ポストプロセス：";
            this._PostProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _InputPathLabel
            // 
            this._InputPathLabel.AutoSize = true;
            this._InputPathLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._InputPathLabel.Location = new System.Drawing.Point(3, 133);
            this._InputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this._InputPathLabel.Name = "_InputPathLabel";
            this._InputPathLabel.Size = new System.Drawing.Size(69, 20);
            this._InputPathLabel.TabIndex = 0;
            this._InputPathLabel.Text = "入力ファイル：";
            this._InputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _PostProcessPanel
            // 
            this._PostProcessPanel.Controls.Add(this.UserProgramTextBox);
            this._PostProcessPanel.Controls.Add(this.UserProgramButton);
            this._PostProcessPanel.Controls.Add(this.PostProcessComboBox);
            this._PostProcessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PostProcessPanel.Location = new System.Drawing.Point(103, 107);
            this._PostProcessPanel.Name = "_PostProcessPanel";
            this._PostProcessPanel.Size = new System.Drawing.Size(316, 20);
            this._PostProcessPanel.TabIndex = 10;
            // 
            // UserProgramTextBox
            // 
            this.UserProgramTextBox.Location = new System.Drawing.Point(98, 0);
            this.UserProgramTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramTextBox.Name = "UserProgramTextBox";
            this.UserProgramTextBox.Size = new System.Drawing.Size(180, 19);
            this.UserProgramTextBox.TabIndex = 21;
            // 
            // UserProgramButton
            // 
            this.UserProgramButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.UserProgramButton.Location = new System.Drawing.Point(281, 0);
            this.UserProgramButton.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramButton.Name = "UserProgramButton";
            this.UserProgramButton.Size = new System.Drawing.Size(35, 20);
            this.UserProgramButton.TabIndex = 22;
            this.UserProgramButton.Text = "...";
            this.UserProgramButton.UseVisualStyleBackColor = true;
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
            this.PostProcessComboBox.TabIndex = 20;
            this.PostProcessComboBox.SelectedIndexChanged += new System.EventHandler(this.PostProcessComboBox_SelectedIndexChanged);
            // 
            // _InputpathPanel
            // 
            this._InputpathPanel.Controls.Add(this.InputPathTextBox);
            this._InputpathPanel.Controls.Add(this.InputPathButton);
            this._InputpathPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._InputpathPanel.Location = new System.Drawing.Point(103, 133);
            this._InputpathPanel.Name = "_InputpathPanel";
            this._InputpathPanel.Size = new System.Drawing.Size(316, 20);
            this._InputpathPanel.TabIndex = 11;
            // 
            // InputPathTextBox
            // 
            this.InputPathTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.InputPathTextBox.Location = new System.Drawing.Point(0, 0);
            this.InputPathTextBox.Name = "InputPathTextBox";
            this.InputPathTextBox.Size = new System.Drawing.Size(278, 19);
            this.InputPathTextBox.TabIndex = 23;
            // 
            // InputPathButton
            // 
            this.InputPathButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.InputPathButton.Location = new System.Drawing.Point(281, 0);
            this.InputPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.InputPathButton.Name = "InputPathButton";
            this.InputPathButton.Size = new System.Drawing.Size(35, 20);
            this.InputPathButton.TabIndex = 24;
            this.InputPathButton.Text = "...";
            this.InputPathButton.UseVisualStyleBackColor = true;
            this.InputPathButton.Click += new System.EventHandler(this.InputPathButton_Click);
            // 
            // DocTabPage
            // 
            this.DocTabPage.BackColor = System.Drawing.Color.White;
            this.DocTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.DocTabPage.Controls.Add(this.DocTableLayoutPanel);
            this.DocTabPage.Location = new System.Drawing.Point(4, 22);
            this.DocTabPage.Name = "DocTabPage";
            this.DocTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DocTabPage.Size = new System.Drawing.Size(462, 329);
            this.DocTabPage.TabIndex = 1;
            this.DocTabPage.Text = "文書プロパティ";
            // 
            // DocTableLayoutPanel
            // 
            this.DocTableLayoutPanel.ColumnCount = 2;
            this.DocTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.DocTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DocTableLayoutPanel.Controls.Add(this._DocTitleLabel, 0, 0);
            this.DocTableLayoutPanel.Controls.Add(this._DocAuthorLabel, 0, 1);
            this.DocTableLayoutPanel.Controls.Add(this._DocSubtitleLabel, 0, 2);
            this.DocTableLayoutPanel.Controls.Add(this._DocKeywordLabel, 0, 3);
            this.DocTableLayoutPanel.Controls.Add(this.DocTitleTextBox, 1, 0);
            this.DocTableLayoutPanel.Controls.Add(this.DocAuthorTextBox, 1, 1);
            this.DocTableLayoutPanel.Controls.Add(this.DocSubtitleTextBox, 1, 2);
            this.DocTableLayoutPanel.Controls.Add(this.DocKeywordTextBox, 1, 3);
            this.DocTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.DocTableLayoutPanel.Location = new System.Drawing.Point(20, 20);
            this.DocTableLayoutPanel.Name = "DocTableLayoutPanel";
            this.DocTableLayoutPanel.RowCount = 4;
            this.DocTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DocTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DocTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DocTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DocTableLayoutPanel.Size = new System.Drawing.Size(422, 104);
            this.DocTableLayoutPanel.TabIndex = 0;
            // 
            // _DocTitleLabel
            // 
            this._DocTitleLabel.AutoSize = true;
            this._DocTitleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._DocTitleLabel.Location = new System.Drawing.Point(3, 3);
            this._DocTitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this._DocTitleLabel.Name = "_DocTitleLabel";
            this._DocTitleLabel.Size = new System.Drawing.Size(46, 20);
            this._DocTitleLabel.TabIndex = 0;
            this._DocTitleLabel.Text = "タイトル：";
            this._DocTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _DocAuthorLabel
            // 
            this._DocAuthorLabel.AutoSize = true;
            this._DocAuthorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._DocAuthorLabel.Location = new System.Drawing.Point(3, 29);
            this._DocAuthorLabel.Margin = new System.Windows.Forms.Padding(3);
            this._DocAuthorLabel.Name = "_DocAuthorLabel";
            this._DocAuthorLabel.Size = new System.Drawing.Size(47, 20);
            this._DocAuthorLabel.TabIndex = 0;
            this._DocAuthorLabel.Text = "作成者：";
            this._DocAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _DocSubtitleLabel
            // 
            this._DocSubtitleLabel.AutoSize = true;
            this._DocSubtitleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._DocSubtitleLabel.Location = new System.Drawing.Point(3, 55);
            this._DocSubtitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this._DocSubtitleLabel.Name = "_DocSubtitleLabel";
            this._DocSubtitleLabel.Size = new System.Drawing.Size(65, 20);
            this._DocSubtitleLabel.TabIndex = 0;
            this._DocSubtitleLabel.Text = "サブタイトル：";
            this._DocSubtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _DocKeywordLabel
            // 
            this._DocKeywordLabel.AutoSize = true;
            this._DocKeywordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._DocKeywordLabel.Location = new System.Drawing.Point(3, 81);
            this._DocKeywordLabel.Margin = new System.Windows.Forms.Padding(3);
            this._DocKeywordLabel.Name = "_DocKeywordLabel";
            this._DocKeywordLabel.Size = new System.Drawing.Size(59, 20);
            this._DocKeywordLabel.TabIndex = 0;
            this._DocKeywordLabel.Text = "キーワード：";
            this._DocKeywordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocTitleTextBox
            // 
            this.DocTitleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocTitleTextBox.Location = new System.Drawing.Point(103, 4);
            this.DocTitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocTitleTextBox.Name = "DocTitleTextBox";
            this.DocTitleTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocTitleTextBox.TabIndex = 1;
            // 
            // DocAuthorTextBox
            // 
            this.DocAuthorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocAuthorTextBox.Location = new System.Drawing.Point(103, 30);
            this.DocAuthorTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocAuthorTextBox.Name = "DocAuthorTextBox";
            this.DocAuthorTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocAuthorTextBox.TabIndex = 2;
            // 
            // DocSubtitleTextBox
            // 
            this.DocSubtitleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocSubtitleTextBox.Location = new System.Drawing.Point(103, 56);
            this.DocSubtitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocSubtitleTextBox.Name = "DocSubtitleTextBox";
            this.DocSubtitleTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocSubtitleTextBox.TabIndex = 3;
            // 
            // DocKeywordTextBox
            // 
            this.DocKeywordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocKeywordTextBox.Location = new System.Drawing.Point(103, 82);
            this.DocKeywordTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DocKeywordTextBox.Name = "DocKeywordTextBox";
            this.DocKeywordTextBox.Size = new System.Drawing.Size(316, 19);
            this.DocKeywordTextBox.TabIndex = 4;
            // 
            // SecurityTabPage
            // 
            this.SecurityTabPage.BackColor = System.Drawing.Color.White;
            this.SecurityTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.SecurityTabPage.Controls.Add(this.OwnerPasswordGroupBox);
            this.SecurityTabPage.Controls.Add(this.UserPasswordGroupBox);
            this.SecurityTabPage.Location = new System.Drawing.Point(4, 22);
            this.SecurityTabPage.Name = "SecurityTabPage";
            this.SecurityTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SecurityTabPage.Size = new System.Drawing.Size(462, 329);
            this.SecurityTabPage.TabIndex = 2;
            this.SecurityTabPage.Text = "セキュリティ";
            // 
            // OwnerPasswordGroupBox
            // 
            this.OwnerPasswordGroupBox.Controls.Add(this.OwnerPasswordTableLayoutPanel);
            this.OwnerPasswordGroupBox.Controls.Add(this.OwnerPasswordCheckBox);
            this.OwnerPasswordGroupBox.Location = new System.Drawing.Point(20, 114);
            this.OwnerPasswordGroupBox.Name = "OwnerPasswordGroupBox";
            this.OwnerPasswordGroupBox.Size = new System.Drawing.Size(422, 200);
            this.OwnerPasswordGroupBox.TabIndex = 2;
            this.OwnerPasswordGroupBox.TabStop = false;
            this.OwnerPasswordGroupBox.Text = "許可";
            // 
            // OwnerPasswordTableLayoutPanel
            // 
            this.OwnerPasswordTableLayoutPanel.ColumnCount = 2;
            this.OwnerPasswordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.OwnerPasswordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this._OwnerPasswordLabel, 0, 0);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this._ConfirmOwnerPasswordLabel, 0, 1);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this._PermissionLabel, 0, 2);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.OwnerPasswordTextBox, 1, 0);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.ConfirmOwnerPasswordTextBox, 1, 1);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.AllowPrintCheckBox, 1, 2);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.AllowCopyCheckBox, 1, 3);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.AllowFormInputCheckBox, 1, 4);
            this.OwnerPasswordTableLayoutPanel.Controls.Add(this.AllowModifyCheckBox, 1, 5);
            this.OwnerPasswordTableLayoutPanel.Enabled = false;
            this.OwnerPasswordTableLayoutPanel.Location = new System.Drawing.Point(12, 40);
            this.OwnerPasswordTableLayoutPanel.Name = "OwnerPasswordTableLayoutPanel";
            this.OwnerPasswordTableLayoutPanel.RowCount = 6;
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.OwnerPasswordTableLayoutPanel.Size = new System.Drawing.Size(398, 156);
            this.OwnerPasswordTableLayoutPanel.TabIndex = 2;
            // 
            // _OwnerPasswordLabel
            // 
            this._OwnerPasswordLabel.AutoSize = true;
            this._OwnerPasswordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._OwnerPasswordLabel.Location = new System.Drawing.Point(3, 3);
            this._OwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this._OwnerPasswordLabel.Name = "_OwnerPasswordLabel";
            this._OwnerPasswordLabel.Size = new System.Drawing.Size(58, 20);
            this._OwnerPasswordLabel.TabIndex = 0;
            this._OwnerPasswordLabel.Text = "パスワード：";
            this._OwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ConfirmOwnerPasswordLabel
            // 
            this._ConfirmOwnerPasswordLabel.AutoSize = true;
            this._ConfirmOwnerPasswordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._ConfirmOwnerPasswordLabel.Location = new System.Drawing.Point(3, 29);
            this._ConfirmOwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this._ConfirmOwnerPasswordLabel.Name = "_ConfirmOwnerPasswordLabel";
            this._ConfirmOwnerPasswordLabel.Size = new System.Drawing.Size(92, 20);
            this._ConfirmOwnerPasswordLabel.TabIndex = 0;
            this._ConfirmOwnerPasswordLabel.Text = "パスワードの確認：";
            this._ConfirmOwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _PermissionLabel
            // 
            this._PermissionLabel.AutoSize = true;
            this._PermissionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._PermissionLabel.Location = new System.Drawing.Point(3, 55);
            this._PermissionLabel.Margin = new System.Windows.Forms.Padding(3);
            this._PermissionLabel.Name = "_PermissionLabel";
            this._PermissionLabel.Size = new System.Drawing.Size(78, 20);
            this._PermissionLabel.TabIndex = 0;
            this._PermissionLabel.Text = "許可する操作：";
            this._PermissionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OwnerPasswordTextBox
            // 
            this.OwnerPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OwnerPasswordTextBox.Location = new System.Drawing.Point(103, 3);
            this.OwnerPasswordTextBox.Name = "OwnerPasswordTextBox";
            this.OwnerPasswordTextBox.PasswordChar = '*';
            this.OwnerPasswordTextBox.Size = new System.Drawing.Size(292, 19);
            this.OwnerPasswordTextBox.TabIndex = 1;
            this.OwnerPasswordTextBox.TextChanged += new System.EventHandler(this.OwnerPasswordTextBox_TextChanged);
            // 
            // ConfirmOwnerPasswordTextBox
            // 
            this.ConfirmOwnerPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfirmOwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmOwnerPasswordTextBox.Location = new System.Drawing.Point(103, 29);
            this.ConfirmOwnerPasswordTextBox.Name = "ConfirmOwnerPasswordTextBox";
            this.ConfirmOwnerPasswordTextBox.PasswordChar = '*';
            this.ConfirmOwnerPasswordTextBox.Size = new System.Drawing.Size(292, 19);
            this.ConfirmOwnerPasswordTextBox.TabIndex = 2;
            this.ConfirmOwnerPasswordTextBox.TextChanged += new System.EventHandler(this.ConfirmOwnerPasswordTextBox_TextChanged);
            // 
            // AllowPrintCheckBox
            // 
            this.AllowPrintCheckBox.AutoSize = true;
            this.AllowPrintCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.AllowPrintCheckBox.Location = new System.Drawing.Point(103, 57);
            this.AllowPrintCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowPrintCheckBox.Name = "AllowPrintCheckBox";
            this.AllowPrintCheckBox.Size = new System.Drawing.Size(48, 16);
            this.AllowPrintCheckBox.TabIndex = 3;
            this.AllowPrintCheckBox.Text = "印刷";
            this.AllowPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowCopyCheckBox
            // 
            this.AllowCopyCheckBox.AutoSize = true;
            this.AllowCopyCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.AllowCopyCheckBox.Location = new System.Drawing.Point(103, 83);
            this.AllowCopyCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowCopyCheckBox.Name = "AllowCopyCheckBox";
            this.AllowCopyCheckBox.Size = new System.Drawing.Size(131, 16);
            this.AllowCopyCheckBox.TabIndex = 4;
            this.AllowCopyCheckBox.Text = "テキストや画像のコピー";
            this.AllowCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowFormInputCheckBox
            // 
            this.AllowFormInputCheckBox.AutoSize = true;
            this.AllowFormInputCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.AllowFormInputCheckBox.Location = new System.Drawing.Point(103, 109);
            this.AllowFormInputCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowFormInputCheckBox.Name = "AllowFormInputCheckBox";
            this.AllowFormInputCheckBox.Size = new System.Drawing.Size(148, 16);
            this.AllowFormInputCheckBox.TabIndex = 5;
            this.AllowFormInputCheckBox.Text = "フォームフィールドへの入力";
            this.AllowFormInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowModifyCheckBox
            // 
            this.AllowModifyCheckBox.AutoSize = true;
            this.AllowModifyCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.AllowModifyCheckBox.Location = new System.Drawing.Point(103, 135);
            this.AllowModifyCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AllowModifyCheckBox.Name = "AllowModifyCheckBox";
            this.AllowModifyCheckBox.Size = new System.Drawing.Size(181, 16);
            this.AllowModifyCheckBox.TabIndex = 6;
            this.AllowModifyCheckBox.Text = "ページの挿入、回転、および削除";
            this.AllowModifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordCheckBox
            // 
            this.OwnerPasswordCheckBox.AutoSize = true;
            this.OwnerPasswordCheckBox.Location = new System.Drawing.Point(12, 18);
            this.OwnerPasswordCheckBox.Name = "OwnerPasswordCheckBox";
            this.OwnerPasswordCheckBox.Size = new System.Drawing.Size(163, 16);
            this.OwnerPasswordCheckBox.TabIndex = 1;
            this.OwnerPasswordCheckBox.Text = "指定した操作のみを許可する";
            this.OwnerPasswordCheckBox.UseVisualStyleBackColor = true;
            this.OwnerPasswordCheckBox.CheckedChanged += new System.EventHandler(this.OwnerPasswordCheckBox_CheckedChanged);
            // 
            // UserPasswordGroupBox
            // 
            this.UserPasswordGroupBox.Controls.Add(this.UserPasswordTableLayoutPanel);
            this.UserPasswordGroupBox.Controls.Add(this.UserPasswordCheckBox);
            this.UserPasswordGroupBox.Location = new System.Drawing.Point(20, 8);
            this.UserPasswordGroupBox.Name = "UserPasswordGroupBox";
            this.UserPasswordGroupBox.Size = new System.Drawing.Size(422, 100);
            this.UserPasswordGroupBox.TabIndex = 1;
            this.UserPasswordGroupBox.TabStop = false;
            this.UserPasswordGroupBox.Text = "セキュリティ";
            // 
            // UserPasswordTableLayoutPanel
            // 
            this.UserPasswordTableLayoutPanel.ColumnCount = 2;
            this.UserPasswordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.UserPasswordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.UserPasswordTableLayoutPanel.Controls.Add(this._UserPasswordLabel, 0, 0);
            this.UserPasswordTableLayoutPanel.Controls.Add(this._ConfirmUserPasswordLabel, 0, 1);
            this.UserPasswordTableLayoutPanel.Controls.Add(this.UserPasswordTextBox, 1, 0);
            this.UserPasswordTableLayoutPanel.Controls.Add(this.ConfirmUserPasswordTextBox, 1, 1);
            this.UserPasswordTableLayoutPanel.Enabled = false;
            this.UserPasswordTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.UserPasswordTableLayoutPanel.Location = new System.Drawing.Point(12, 40);
            this.UserPasswordTableLayoutPanel.Name = "UserPasswordTableLayoutPanel";
            this.UserPasswordTableLayoutPanel.RowCount = 2;
            this.UserPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.UserPasswordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.UserPasswordTableLayoutPanel.Size = new System.Drawing.Size(398, 52);
            this.UserPasswordTableLayoutPanel.TabIndex = 2;
            // 
            // _UserPasswordLabel
            // 
            this._UserPasswordLabel.AutoSize = true;
            this._UserPasswordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._UserPasswordLabel.Location = new System.Drawing.Point(3, 3);
            this._UserPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this._UserPasswordLabel.Name = "_UserPasswordLabel";
            this._UserPasswordLabel.Size = new System.Drawing.Size(58, 20);
            this._UserPasswordLabel.TabIndex = 0;
            this._UserPasswordLabel.Text = "パスワード：";
            this._UserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ConfirmUserPasswordLabel
            // 
            this._ConfirmUserPasswordLabel.AutoSize = true;
            this._ConfirmUserPasswordLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._ConfirmUserPasswordLabel.Location = new System.Drawing.Point(3, 29);
            this._ConfirmUserPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this._ConfirmUserPasswordLabel.Name = "_ConfirmUserPasswordLabel";
            this._ConfirmUserPasswordLabel.Size = new System.Drawing.Size(92, 20);
            this._ConfirmUserPasswordLabel.TabIndex = 0;
            this._ConfirmUserPasswordLabel.Text = "パスワードの確認：";
            this._ConfirmUserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserPasswordTextBox
            // 
            this.UserPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UserPasswordTextBox.Location = new System.Drawing.Point(103, 3);
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            this.UserPasswordTextBox.PasswordChar = '*';
            this.UserPasswordTextBox.Size = new System.Drawing.Size(292, 19);
            this.UserPasswordTextBox.TabIndex = 1;
            this.UserPasswordTextBox.TextChanged += new System.EventHandler(this.UserPasswordTextBox_TextChanged);
            // 
            // ConfirmUserPasswordTextBox
            // 
            this.ConfirmUserPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfirmUserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmUserPasswordTextBox.Location = new System.Drawing.Point(103, 29);
            this.ConfirmUserPasswordTextBox.Name = "ConfirmUserPasswordTextBox";
            this.ConfirmUserPasswordTextBox.PasswordChar = '*';
            this.ConfirmUserPasswordTextBox.Size = new System.Drawing.Size(292, 19);
            this.ConfirmUserPasswordTextBox.TabIndex = 2;
            this.ConfirmUserPasswordTextBox.TextChanged += new System.EventHandler(this.ConfirmUserPasswordTextBox_TextChanged);
            // 
            // UserPasswordCheckBox
            // 
            this.UserPasswordCheckBox.AutoSize = true;
            this.UserPasswordCheckBox.Location = new System.Drawing.Point(12, 18);
            this.UserPasswordCheckBox.Name = "UserPasswordCheckBox";
            this.UserPasswordCheckBox.Size = new System.Drawing.Size(188, 16);
            this.UserPasswordCheckBox.TabIndex = 1;
            this.UserPasswordCheckBox.Text = "文書を開くときにパスワードを求める";
            this.UserPasswordCheckBox.UseVisualStyleBackColor = true;
            this.UserPasswordCheckBox.CheckedChanged += new System.EventHandler(this.UserPasswordCheckBox_CheckedChanged);
            // 
            // DetailTabPage
            // 
            this.DetailTabPage.BackColor = System.Drawing.Color.White;
            this.DetailTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.DetailTabPage.Controls.Add(this.DetailTableLayoutPanel);
            this.DetailTabPage.Location = new System.Drawing.Point(4, 22);
            this.DetailTabPage.Name = "DetailTabPage";
            this.DetailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DetailTabPage.Size = new System.Drawing.Size(462, 329);
            this.DetailTabPage.TabIndex = 3;
            this.DetailTabPage.Text = "詳細設定";
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this._DownSamplingLabel, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this._OptionLabel, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this._OthersLabel, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.DownSamplingComboBox, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.PageLotationCheckBox, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.EmbedFontCheckBox, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.GrayscaleCheckBox, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.WebOptimizeCheckBox, 1, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.SaveSettingCheckBox, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.UpdateCheckBox, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this._PostProcessLiteLabel, 0, 7);
            this.DetailTableLayoutPanel.Controls.Add(this.PostProcessLiteComboBox, 1, 7);
            this.DetailTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(20, 20);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 8;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(422, 208);
            this.DetailTableLayoutPanel.TabIndex = 0;
            // 
            // _DownSamplingLabel
            // 
            this._DownSamplingLabel.AutoSize = true;
            this._DownSamplingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._DownSamplingLabel.Location = new System.Drawing.Point(3, 3);
            this._DownSamplingLabel.Margin = new System.Windows.Forms.Padding(3);
            this._DownSamplingLabel.Name = "_DownSamplingLabel";
            this._DownSamplingLabel.Size = new System.Drawing.Size(91, 20);
            this._DownSamplingLabel.TabIndex = 0;
            this._DownSamplingLabel.Text = "ダウンサンプリング：";
            this._DownSamplingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _OptionLabel
            // 
            this._OptionLabel.AutoSize = true;
            this._OptionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._OptionLabel.Location = new System.Drawing.Point(3, 29);
            this._OptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this._OptionLabel.Name = "_OptionLabel";
            this._OptionLabel.Size = new System.Drawing.Size(54, 20);
            this._OptionLabel.TabIndex = 0;
            this._OptionLabel.Text = "オプション：";
            this._OptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _OthersLabel
            // 
            this._OthersLabel.AutoSize = true;
            this._OthersLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._OthersLabel.Location = new System.Drawing.Point(3, 133);
            this._OthersLabel.Margin = new System.Windows.Forms.Padding(3);
            this._OthersLabel.Name = "_OthersLabel";
            this._OthersLabel.Size = new System.Drawing.Size(42, 20);
            this._OthersLabel.TabIndex = 0;
            this._OthersLabel.Text = "その他：";
            this._OthersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DownSamplingComboBox
            // 
            this.DownSamplingComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownSamplingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DownSamplingComboBox.FormattingEnabled = true;
            this.DownSamplingComboBox.Location = new System.Drawing.Point(103, 3);
            this.DownSamplingComboBox.Name = "DownSamplingComboBox";
            this.DownSamplingComboBox.Size = new System.Drawing.Size(316, 20);
            this.DownSamplingComboBox.TabIndex = 1;
            // 
            // PageLotationCheckBox
            // 
            this.PageLotationCheckBox.AutoSize = true;
            this.PageLotationCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.PageLotationCheckBox.Location = new System.Drawing.Point(103, 31);
            this.PageLotationCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.PageLotationCheckBox.Name = "PageLotationCheckBox";
            this.PageLotationCheckBox.Size = new System.Drawing.Size(112, 16);
            this.PageLotationCheckBox.TabIndex = 2;
            this.PageLotationCheckBox.Text = "ページの自動回転";
            this.PageLotationCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmbedFontCheckBox
            // 
            this.EmbedFontCheckBox.AutoSize = true;
            this.EmbedFontCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.EmbedFontCheckBox.Location = new System.Drawing.Point(103, 57);
            this.EmbedFontCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.EmbedFontCheckBox.Name = "EmbedFontCheckBox";
            this.EmbedFontCheckBox.Size = new System.Drawing.Size(112, 16);
            this.EmbedFontCheckBox.TabIndex = 3;
            this.EmbedFontCheckBox.Text = "フォントの埋め込み";
            this.EmbedFontCheckBox.UseVisualStyleBackColor = true;
            // 
            // GrayscaleCheckBox
            // 
            this.GrayscaleCheckBox.AutoSize = true;
            this.GrayscaleCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.GrayscaleCheckBox.Location = new System.Drawing.Point(103, 83);
            this.GrayscaleCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.GrayscaleCheckBox.Name = "GrayscaleCheckBox";
            this.GrayscaleCheckBox.Size = new System.Drawing.Size(90, 16);
            this.GrayscaleCheckBox.TabIndex = 4;
            this.GrayscaleCheckBox.Text = "グレースケール";
            this.GrayscaleCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebOptimizeCheckBox
            // 
            this.WebOptimizeCheckBox.AutoSize = true;
            this.WebOptimizeCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.WebOptimizeCheckBox.Location = new System.Drawing.Point(103, 109);
            this.WebOptimizeCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.WebOptimizeCheckBox.Name = "WebOptimizeCheckBox";
            this.WebOptimizeCheckBox.Size = new System.Drawing.Size(126, 16);
            this.WebOptimizeCheckBox.TabIndex = 5;
            this.WebOptimizeCheckBox.Text = "Web表示用に最適化";
            this.WebOptimizeCheckBox.UseVisualStyleBackColor = true;
            this.WebOptimizeCheckBox.CheckedChanged += new System.EventHandler(this.WebOptimizeCheckBox_CheckedChanged);
            // 
            // SaveSettingCheckBox
            // 
            this.SaveSettingCheckBox.AutoSize = true;
            this.SaveSettingCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.SaveSettingCheckBox.Location = new System.Drawing.Point(103, 135);
            this.SaveSettingCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.SaveSettingCheckBox.Name = "SaveSettingCheckBox";
            this.SaveSettingCheckBox.Size = new System.Drawing.Size(100, 16);
            this.SaveSettingCheckBox.TabIndex = 6;
            this.SaveSettingCheckBox.Text = "設定を保存する";
            this.SaveSettingCheckBox.UseVisualStyleBackColor = true;
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.AutoSize = true;
            this.UpdateCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.UpdateCheckBox.Location = new System.Drawing.Point(103, 161);
            this.UpdateCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(174, 16);
            this.UpdateCheckBox.TabIndex = 7;
            this.UpdateCheckBox.Text = "起動時にアップデートを確認する";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // _PostProcessLiteLabel
            // 
            this._PostProcessLiteLabel.AutoSize = true;
            this._PostProcessLiteLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._PostProcessLiteLabel.Location = new System.Drawing.Point(3, 185);
            this._PostProcessLiteLabel.Margin = new System.Windows.Forms.Padding(3);
            this._PostProcessLiteLabel.Name = "_PostProcessLiteLabel";
            this._PostProcessLiteLabel.Size = new System.Drawing.Size(75, 20);
            this._PostProcessLiteLabel.TabIndex = 8;
            this._PostProcessLiteLabel.Text = "ポストプロセス：";
            this._PostProcessLiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PostProcessLiteComboBox
            // 
            this.PostProcessLiteComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PostProcessLiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessLiteComboBox.FormattingEnabled = true;
            this.PostProcessLiteComboBox.Location = new System.Drawing.Point(103, 185);
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            this.PostProcessLiteComboBox.Size = new System.Drawing.Size(316, 20);
            this.PostProcessLiteComboBox.TabIndex = 9;
            // 
            // ConvertBackgroundWorker
            // 
            this.ConvertBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConvertBackgroundWorker_DoWork);
            this.ConvertBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConvertBackgroundWorker_RunWorkerCompleted);
            // 
            // _patch
            // 
            this._patch.BackgroundImage = global::CubePDF.Properties.Resources.background;
            this._patch.Location = new System.Drawing.Point(400, 80);
            this._patch.Name = "_patch";
            this._patch.Size = new System.Drawing.Size(83, 18);
            this._patch.TabIndex = 1;
            this._patch.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 501);
            this.Controls.Add(this._patch);
            this.Controls.Add(this._MainPanel);
            this.Controls.Add(this.HeaderPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubePDF";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).EndInit();
            this._MainPanel.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.GeneralTabPage.ResumeLayout(false);
            this.GeneralTableLayoutPanel.ResumeLayout(false);
            this.GeneralTableLayoutPanel.PerformLayout();
            this._OutputPathPanel.ResumeLayout(false);
            this._OutputPathPanel.PerformLayout();
            this._PostProcessPanel.ResumeLayout(false);
            this._PostProcessPanel.PerformLayout();
            this._InputpathPanel.ResumeLayout(false);
            this._InputpathPanel.PerformLayout();
            this.DocTabPage.ResumeLayout(false);
            this.DocTableLayoutPanel.ResumeLayout(false);
            this.DocTableLayoutPanel.PerformLayout();
            this.SecurityTabPage.ResumeLayout(false);
            this.OwnerPasswordGroupBox.ResumeLayout(false);
            this.OwnerPasswordGroupBox.PerformLayout();
            this.OwnerPasswordTableLayoutPanel.ResumeLayout(false);
            this.OwnerPasswordTableLayoutPanel.PerformLayout();
            this.UserPasswordGroupBox.ResumeLayout(false);
            this.UserPasswordGroupBox.PerformLayout();
            this.UserPasswordTableLayoutPanel.ResumeLayout(false);
            this.UserPasswordTableLayoutPanel.PerformLayout();
            this.DetailTabPage.ResumeLayout(false);
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._patch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HeaderPictureBox;
        private System.Windows.Forms.Panel _MainPanel;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage DocTabPage;
        private System.Windows.Forms.TabPage SecurityTabPage;
        private System.Windows.Forms.TabPage DetailTabPage;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ProgressBar ExecProgressBar;
        private System.Windows.Forms.TableLayoutPanel GeneralTableLayoutPanel;
        private System.Windows.Forms.Label _FileTypeLabel;
        private System.Windows.Forms.Label _PDFVersionLabel;
        private System.Windows.Forms.ComboBox PDFVersionComboBox;
        private System.Windows.Forms.Label _ResolutionLabel;
        private System.Windows.Forms.Label _OutputPathLabel;
        private System.Windows.Forms.ComboBox ResolutionComboBox;
        private System.Windows.Forms.Panel _OutputPathPanel;
        private System.Windows.Forms.ComboBox ExistedFileComboBox;
        private System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.Button OutputPathButton;
        private System.Windows.Forms.Label _PostProcessLabel;
        private System.Windows.Forms.Label _InputPathLabel;
        private System.Windows.Forms.Panel _PostProcessPanel;
        private System.Windows.Forms.Panel _InputpathPanel;
        private System.Windows.Forms.TableLayoutPanel DocTableLayoutPanel;
        private System.Windows.Forms.Label _DocTitleLabel;
        private System.Windows.Forms.Label _DocAuthorLabel;
        private System.Windows.Forms.Label _DocSubtitleLabel;
        private System.Windows.Forms.Label _DocKeywordLabel;
        private System.Windows.Forms.TextBox DocTitleTextBox;
        private System.Windows.Forms.TextBox DocAuthorTextBox;
        private System.Windows.Forms.TextBox DocSubtitleTextBox;
        private System.Windows.Forms.TextBox DocKeywordTextBox;
        private System.Windows.Forms.ComboBox PostProcessComboBox;
        private System.Windows.Forms.TextBox UserProgramTextBox;
        private System.Windows.Forms.Button UserProgramButton;
        private System.Windows.Forms.Button InputPathButton;
        private System.Windows.Forms.TextBox InputPathTextBox;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label _DownSamplingLabel;
        private System.Windows.Forms.Label _OptionLabel;
        private System.Windows.Forms.Label _OthersLabel;
        private System.Windows.Forms.ComboBox DownSamplingComboBox;
        private System.Windows.Forms.CheckBox PageLotationCheckBox;
        private System.Windows.Forms.CheckBox EmbedFontCheckBox;
        private System.Windows.Forms.CheckBox GrayscaleCheckBox;
        private System.Windows.Forms.CheckBox WebOptimizeCheckBox;
        private System.Windows.Forms.CheckBox SaveSettingCheckBox;
        private System.Windows.Forms.CheckBox UpdateCheckBox;
        private System.Windows.Forms.GroupBox UserPasswordGroupBox;
        private System.Windows.Forms.TableLayoutPanel UserPasswordTableLayoutPanel;
        private System.Windows.Forms.CheckBox UserPasswordCheckBox;
        private System.Windows.Forms.Label _UserPasswordLabel;
        private System.Windows.Forms.Label _ConfirmUserPasswordLabel;
        private System.Windows.Forms.TextBox UserPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmUserPasswordTextBox;
        private System.Windows.Forms.GroupBox OwnerPasswordGroupBox;
        private System.Windows.Forms.CheckBox OwnerPasswordCheckBox;
        private System.Windows.Forms.TableLayoutPanel OwnerPasswordTableLayoutPanel;
        private System.Windows.Forms.Label _OwnerPasswordLabel;
        private System.Windows.Forms.Label _ConfirmOwnerPasswordLabel;
        private System.Windows.Forms.Label _PermissionLabel;
        private System.Windows.Forms.TextBox OwnerPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmOwnerPasswordTextBox;
        private System.Windows.Forms.CheckBox AllowPrintCheckBox;
        private System.Windows.Forms.CheckBox AllowCopyCheckBox;
        private System.Windows.Forms.CheckBox AllowFormInputCheckBox;
        private System.Windows.Forms.CheckBox AllowModifyCheckBox;
        private System.Windows.Forms.ComboBox FileTypeCombBox;
        private System.Windows.Forms.Label _PostProcessLiteLabel;
        private System.Windows.Forms.ComboBox PostProcessLiteComboBox;
        private System.ComponentModel.BackgroundWorker ConvertBackgroundWorker;
        private System.Windows.Forms.PictureBox _patch;
    }
}

