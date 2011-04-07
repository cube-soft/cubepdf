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
            this._PostProcessLiteLabel = new System.Windows.Forms.Label();
            this.PostProcessLiteComboBox = new System.Windows.Forms.ComboBox();
            this.VersionTabPage = new System.Windows.Forms.TabPage();
            this.UpdateCheckBox = new System.Windows.Forms.CheckBox();
            this._CopyrightLabel = new System.Windows.Forms.Label();
            this._VersionLabel = new System.Windows.Forms.Label();
            this._TitlePictureBox = new System.Windows.Forms.PictureBox();
            this.CubePDFLinkLabel = new System.Windows.Forms.LinkLabel();
            this._LogoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).BeginInit();
            this._MainPanel.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.GeneralTableLayoutPanel.SuspendLayout();
            this._OutputPathPanel.SuspendLayout();
            this._PostProcessPanel.SuspendLayout();
            this._InputpathPanel.SuspendLayout();
            this.DetailTabPage.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.VersionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._LogoPictureBox)).BeginInit();
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
            // 
            // _MainPanel
            // 
            this._MainPanel.BackgroundImage = global::CubePDF.Properties.Resources.background;
            this._MainPanel.Controls.Add(this.ExitButton);
            this._MainPanel.Controls.Add(this.ConvertButton);
            this._MainPanel.Controls.Add(this.MainTabControl);
            this._MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainPanel.Location = new System.Drawing.Point(0, 80);
            this._MainPanel.Name = "_MainPanel";
            this._MainPanel.Size = new System.Drawing.Size(494, 421);
            this._MainPanel.TabIndex = 1;
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
            this.MainTabControl.Controls.Add(this.DetailTabPage);
            this.MainTabControl.Controls.Add(this.VersionTabPage);
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
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 21);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(462, 330);
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
            this._OutputPathLabel.Text = "出力フォルダ：";
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
            // DetailTabPage
            // 
            this.DetailTabPage.BackColor = System.Drawing.Color.White;
            this.DetailTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.DetailTabPage.Controls.Add(this.DetailTableLayoutPanel);
            this.DetailTabPage.Location = new System.Drawing.Point(4, 21);
            this.DetailTabPage.Name = "DetailTabPage";
            this.DetailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DetailTabPage.Size = new System.Drawing.Size(462, 330);
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
            this.DetailTableLayoutPanel.Controls.Add(this._PostProcessLiteLabel, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.PostProcessLiteComboBox, 1, 6);
            this.DetailTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(20, 20);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 7;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(422, 182);
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
            // 
            // SaveSettingCheckBox
            // 
            this.SaveSettingCheckBox.AutoSize = true;
            this.SaveSettingCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.SaveSettingCheckBox.Location = new System.Drawing.Point(103, 135);
            this.SaveSettingCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.SaveSettingCheckBox.Name = "SaveSettingCheckBox";
            this.SaveSettingCheckBox.Size = new System.Drawing.Size(167, 16);
            this.SaveSettingCheckBox.TabIndex = 6;
            this.SaveSettingCheckBox.Text = "実行時の設定を常に保存する";
            this.SaveSettingCheckBox.UseVisualStyleBackColor = true;
            // 
            // _PostProcessLiteLabel
            // 
            this._PostProcessLiteLabel.AutoSize = true;
            this._PostProcessLiteLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._PostProcessLiteLabel.Location = new System.Drawing.Point(3, 159);
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
            this.PostProcessLiteComboBox.Location = new System.Drawing.Point(103, 159);
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            this.PostProcessLiteComboBox.Size = new System.Drawing.Size(316, 20);
            this.PostProcessLiteComboBox.TabIndex = 9;
            // 
            // VersionTabPage
            // 
            this.VersionTabPage.BackColor = System.Drawing.Color.White;
            this.VersionTabPage.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.VersionTabPage.Controls.Add(this.UpdateCheckBox);
            this.VersionTabPage.Controls.Add(this._CopyrightLabel);
            this.VersionTabPage.Controls.Add(this._VersionLabel);
            this.VersionTabPage.Controls.Add(this._TitlePictureBox);
            this.VersionTabPage.Controls.Add(this.CubePDFLinkLabel);
            this.VersionTabPage.Controls.Add(this._LogoPictureBox);
            this.VersionTabPage.Location = new System.Drawing.Point(4, 21);
            this.VersionTabPage.Name = "VersionTabPage";
            this.VersionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VersionTabPage.Size = new System.Drawing.Size(462, 330);
            this.VersionTabPage.TabIndex = 4;
            this.VersionTabPage.Text = "バージョン";
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.AutoSize = true;
            this.UpdateCheckBox.Location = new System.Drawing.Point(140, 152);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(174, 16);
            this.UpdateCheckBox.TabIndex = 18;
            this.UpdateCheckBox.Text = "起動時にアップデートを確認する";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // _CopyrightLabel
            // 
            this._CopyrightLabel.AutoSize = true;
            this._CopyrightLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._CopyrightLabel.Location = new System.Drawing.Point(140, 116);
            this._CopyrightLabel.Name = "_CopyrightLabel";
            this._CopyrightLabel.Size = new System.Drawing.Size(153, 12);
            this._CopyrightLabel.TabIndex = 15;
            this._CopyrightLabel.Text = "Copyright (c) 2010 CubeSoft.";
            // 
            // _VersionLabel
            // 
            this._VersionLabel.AutoSize = true;
            this._VersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._VersionLabel.Location = new System.Drawing.Point(140, 100);
            this._VersionLabel.Name = "_VersionLabel";
            this._VersionLabel.Size = new System.Drawing.Size(44, 12);
            this._VersionLabel.TabIndex = 14;
            this._VersionLabel.Text = "Version";
            // 
            // _TitlePictureBox
            // 
            this._TitlePictureBox.Image = global::CubePDF.Properties.Resources.title;
            this._TitlePictureBox.Location = new System.Drawing.Point(178, 40);
            this._TitlePictureBox.Name = "_TitlePictureBox";
            this._TitlePictureBox.Size = new System.Drawing.Size(98, 48);
            this._TitlePictureBox.TabIndex = 17;
            this._TitlePictureBox.TabStop = false;
            // 
            // CubePDFLinkLabel
            // 
            this.CubePDFLinkLabel.AutoSize = true;
            this.CubePDFLinkLabel.Location = new System.Drawing.Point(140, 132);
            this.CubePDFLinkLabel.Name = "CubePDFLinkLabel";
            this.CubePDFLinkLabel.Size = new System.Drawing.Size(178, 12);
            this.CubePDFLinkLabel.TabIndex = 13;
            this.CubePDFLinkLabel.TabStop = true;
            this.CubePDFLinkLabel.Text = "http://www.cube-soft.jp/cubepdf/";
            this.CubePDFLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CubePDFLinkLabel_LinkClicked);
            // 
            // _LogoPictureBox
            // 
            this._LogoPictureBox.Image = global::CubePDF.Properties.Resources.logo;
            this._LogoPictureBox.Location = new System.Drawing.Point(140, 40);
            this._LogoPictureBox.Name = "_LogoPictureBox";
            this._LogoPictureBox.Size = new System.Drawing.Size(32, 42);
            this._LogoPictureBox.TabIndex = 16;
            this._LogoPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 501);
            this.Controls.Add(this._MainPanel);
            this.Controls.Add(this.HeaderPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubePDF 設定";
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
            this.DetailTabPage.ResumeLayout(false);
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.VersionTabPage.ResumeLayout(false);
            this.VersionTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HeaderPictureBox;
        private System.Windows.Forms.Panel _MainPanel;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage DetailTabPage;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Button ExitButton;
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
        private System.Windows.Forms.ComboBox FileTypeCombBox;
        private System.Windows.Forms.TabPage VersionTabPage;
        private System.Windows.Forms.Label _CopyrightLabel;
        private System.Windows.Forms.Label _VersionLabel;
        private System.Windows.Forms.PictureBox _TitlePictureBox;
        private System.Windows.Forms.LinkLabel CubePDFLinkLabel;
        private System.Windows.Forms.PictureBox _LogoPictureBox;
        private System.Windows.Forms.CheckBox UpdateCheckBox;
        private System.Windows.Forms.Label _PostProcessLiteLabel;
        private System.Windows.Forms.ComboBox PostProcessLiteComboBox;
    }
}

