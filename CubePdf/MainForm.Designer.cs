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
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
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
            this.OrientationPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PortraitRadioButton = new System.Windows.Forms.RadioButton();
            this.LandscapeRadioButton = new System.Windows.Forms.RadioButton();
            this.AutoRadioButton = new System.Windows.Forms.RadioButton();
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
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.SettingButton = new Cube.Forms.Button();
            this.ExecProgressBar = new System.Windows.Forms.ProgressBar();
            this.ConvertButton = new Cube.Forms.Button();
            this.ExitButton = new Cube.Forms.Button();
            this.LayoutPanel.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.GeneralPanel.SuspendLayout();
            this.InputPathPanel.SuspendLayout();
            this.OutputPathPanel.SuspendLayout();
            this.PostProcessPanel.SuspendLayout();
            this.DocTabPage.SuspendLayout();
            this.DocPanel.SuspendLayout();
            this.SecurityTabPage.SuspendLayout();
            this.SecurityPanel.SuspendLayout();
            this.UserPasswordPanel.SuspendLayout();
            this.DetailTabPage.SuspendLayout();
            this.DetailPanel.SuspendLayout();
            this.OrientationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).BeginInit();
            this.ButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.MainTabControl, 0, 1);
            this.LayoutPanel.Controls.Add(this.HeaderPictureBox, 0, 0);
            this.LayoutPanel.Controls.Add(this.ButtonsPanel, 0, 2);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 3;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.LayoutPanel.Size = new System.Drawing.Size(500, 501);
            this.LayoutPanel.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.GeneralTabPage);
            this.MainTabControl.Controls.Add(this.DocTabPage);
            this.MainTabControl.Controls.Add(this.SecurityTabPage);
            this.MainTabControl.Controls.Add(this.DetailTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.HotTrack = true;
            this.MainTabControl.Location = new System.Drawing.Point(12, 70);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(12, 20, 12, 10);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(476, 351);
            this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTabControl.TabIndex = 4;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GeneralTabPage.Controls.Add(this.GeneralPanel);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 24);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(468, 323);
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
            this.GeneralPanel.Size = new System.Drawing.Size(462, 317);
            this.GeneralPanel.TabIndex = 1;
            // 
            // InputPathPanel
            // 
            this.InputPathPanel.Controls.Add(this.InputPathButton);
            this.InputPathPanel.Controls.Add(this.InputPathTextBox);
            this.InputPathPanel.Location = new System.Drawing.Point(120, 168);
            this.InputPathPanel.Name = "InputPathPanel";
            this.InputPathPanel.Size = new System.Drawing.Size(316, 24);
            this.InputPathPanel.TabIndex = 26;
            // 
            // InputPathButton
            // 
            this.InputPathButton.BackColor = System.Drawing.SystemColors.Control;
            this.InputPathButton.Location = new System.Drawing.Point(276, 0);
            this.InputPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.InputPathButton.Name = "InputPathButton";
            this.InputPathButton.Size = new System.Drawing.Size(40, 24);
            this.InputPathButton.TabIndex = 20;
            this.InputPathButton.Text = "...";
            this.InputPathButton.UseVisualStyleBackColor = false;
            // 
            // InputPathTextBox
            // 
            this.InputPathTextBox.Location = new System.Drawing.Point(0, 1);
            this.InputPathTextBox.Name = "InputPathTextBox";
            this.InputPathTextBox.Size = new System.Drawing.Size(273, 23);
            this.InputPathTextBox.TabIndex = 19;
            // 
            // OutputPathPanel
            // 
            this.OutputPathPanel.Controls.Add(this.ExistedFileComboBox);
            this.OutputPathPanel.Controls.Add(this.OutputPathButton);
            this.OutputPathPanel.Controls.Add(this.OutputPathTextBox);
            this.OutputPathPanel.Location = new System.Drawing.Point(120, 108);
            this.OutputPathPanel.Name = "OutputPathPanel";
            this.OutputPathPanel.Size = new System.Drawing.Size(316, 24);
            this.OutputPathPanel.TabIndex = 12;
            // 
            // ExistedFileComboBox
            // 
            this.ExistedFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistedFileComboBox.FormattingEnabled = true;
            this.ExistedFileComboBox.Location = new System.Drawing.Point(236, 0);
            this.ExistedFileComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.ExistedFileComboBox.Name = "ExistedFileComboBox";
            this.ExistedFileComboBox.Size = new System.Drawing.Size(80, 23);
            this.ExistedFileComboBox.TabIndex = 15;
            // 
            // OutputPathButton
            // 
            this.OutputPathButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutputPathButton.Location = new System.Drawing.Point(193, 0);
            this.OutputPathButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.OutputPathButton.Name = "OutputPathButton";
            this.OutputPathButton.Size = new System.Drawing.Size(40, 24);
            this.OutputPathButton.TabIndex = 14;
            this.OutputPathButton.Text = "...";
            this.OutputPathButton.UseVisualStyleBackColor = false;
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Location = new System.Drawing.Point(0, 1);
            this.OutputPathTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(190, 23);
            this.OutputPathTextBox.TabIndex = 13;
            // 
            // PostProcessPanel
            // 
            this.PostProcessPanel.Controls.Add(this.UserProgramTextBox);
            this.PostProcessPanel.Controls.Add(this.UserProgramButton);
            this.PostProcessPanel.Controls.Add(this.PostProcessComboBox);
            this.PostProcessPanel.Location = new System.Drawing.Point(120, 138);
            this.PostProcessPanel.Name = "PostProcessPanel";
            this.PostProcessPanel.Size = new System.Drawing.Size(316, 24);
            this.PostProcessPanel.TabIndex = 11;
            // 
            // UserProgramTextBox
            // 
            this.UserProgramTextBox.Location = new System.Drawing.Point(98, 0);
            this.UserProgramTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramTextBox.Name = "UserProgramTextBox";
            this.UserProgramTextBox.Size = new System.Drawing.Size(175, 23);
            this.UserProgramTextBox.TabIndex = 17;
            // 
            // UserProgramButton
            // 
            this.UserProgramButton.BackColor = System.Drawing.SystemColors.Control;
            this.UserProgramButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.UserProgramButton.Location = new System.Drawing.Point(276, 0);
            this.UserProgramButton.Margin = new System.Windows.Forms.Padding(0);
            this.UserProgramButton.Name = "UserProgramButton";
            this.UserProgramButton.Size = new System.Drawing.Size(40, 24);
            this.UserProgramButton.TabIndex = 18;
            this.UserProgramButton.Text = "...";
            this.UserProgramButton.UseVisualStyleBackColor = false;
            // 
            // PostProcessComboBox
            // 
            this.PostProcessComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.PostProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessComboBox.FormattingEnabled = true;
            this.PostProcessComboBox.Location = new System.Drawing.Point(0, 0);
            this.PostProcessComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.PostProcessComboBox.Name = "PostProcessComboBox";
            this.PostProcessComboBox.Size = new System.Drawing.Size(95, 23);
            this.PostProcessComboBox.TabIndex = 16;
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionComboBox.FormattingEnabled = true;
            this.ResolutionComboBox.Location = new System.Drawing.Point(120, 79);
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            this.ResolutionComboBox.Size = new System.Drawing.Size(316, 23);
            this.ResolutionComboBox.TabIndex = 12;
            // 
            // PdfVersionComboBox
            // 
            this.PdfVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PdfVersionComboBox.FormattingEnabled = true;
            this.PdfVersionComboBox.Location = new System.Drawing.Point(120, 50);
            this.PdfVersionComboBox.Name = "PdfVersionComboBox";
            this.PdfVersionComboBox.Size = new System.Drawing.Size(316, 23);
            this.PdfVersionComboBox.TabIndex = 11;
            // 
            // FileTypeCombBox
            // 
            this.FileTypeCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileTypeCombBox.FormattingEnabled = true;
            this.FileTypeCombBox.Location = new System.Drawing.Point(120, 21);
            this.FileTypeCombBox.Name = "FileTypeCombBox";
            this.FileTypeCombBox.Size = new System.Drawing.Size(316, 23);
            this.FileTypeCombBox.TabIndex = 10;
            // 
            // InputPathLabel
            // 
            this.InputPathLabel.AutoSize = true;
            this.InputPathLabel.Location = new System.Drawing.Point(20, 172);
            this.InputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.InputPathLabel.Name = "InputPathLabel";
            this.InputPathLabel.Size = new System.Drawing.Size(77, 15);
            this.InputPathLabel.TabIndex = 100;
            this.InputPathLabel.Text = "入力ファイル：";
            this.InputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PostProcessLabel
            // 
            this.PostProcessLabel.AutoSize = true;
            this.PostProcessLabel.Location = new System.Drawing.Point(20, 141);
            this.PostProcessLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PostProcessLabel.Name = "PostProcessLabel";
            this.PostProcessLabel.Size = new System.Drawing.Size(83, 15);
            this.PostProcessLabel.TabIndex = 100;
            this.PostProcessLabel.Text = "ポストプロセス：";
            this.PostProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OutputPathLabel
            // 
            this.OutputPathLabel.AutoSize = true;
            this.OutputPathLabel.Location = new System.Drawing.Point(20, 113);
            this.OutputPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OutputPathLabel.Name = "OutputPathLabel";
            this.OutputPathLabel.Size = new System.Drawing.Size(77, 15);
            this.OutputPathLabel.TabIndex = 100;
            this.OutputPathLabel.Text = "出力ファイル：";
            this.OutputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(20, 82);
            this.ResolutionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(55, 15);
            this.ResolutionLabel.TabIndex = 100;
            this.ResolutionLabel.Text = "解像度：";
            this.ResolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PDFVersionLabel
            // 
            this.PDFVersionLabel.AutoSize = true;
            this.PDFVersionLabel.Location = new System.Drawing.Point(20, 53);
            this.PDFVersionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PDFVersionLabel.Name = "PDFVersionLabel";
            this.PDFVersionLabel.Size = new System.Drawing.Size(91, 15);
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
            this.FileTypeLabel.Size = new System.Drawing.Size(79, 15);
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
            this.DocTabPage.Size = new System.Drawing.Size(468, 325);
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
            this.DocPanel.Size = new System.Drawing.Size(462, 319);
            this.DocPanel.TabIndex = 1;
            // 
            // DocTitleLabel
            // 
            this.DocTitleLabel.AutoSize = true;
            this.DocTitleLabel.Location = new System.Drawing.Point(20, 24);
            this.DocTitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocTitleLabel.Name = "DocTitleLabel";
            this.DocTitleLabel.Size = new System.Drawing.Size(54, 15);
            this.DocTitleLabel.TabIndex = 7;
            this.DocTitleLabel.Text = "タイトル：";
            this.DocTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocAuthorLabel
            // 
            this.DocAuthorLabel.AutoSize = true;
            this.DocAuthorLabel.Location = new System.Drawing.Point(19, 53);
            this.DocAuthorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocAuthorLabel.Name = "DocAuthorLabel";
            this.DocAuthorLabel.Size = new System.Drawing.Size(55, 15);
            this.DocAuthorLabel.TabIndex = 8;
            this.DocAuthorLabel.Text = "作成者：";
            this.DocAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocSubtitleLabel
            // 
            this.DocSubtitleLabel.AutoSize = true;
            this.DocSubtitleLabel.Location = new System.Drawing.Point(20, 82);
            this.DocSubtitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocSubtitleLabel.Name = "DocSubtitleLabel";
            this.DocSubtitleLabel.Size = new System.Drawing.Size(73, 15);
            this.DocSubtitleLabel.TabIndex = 5;
            this.DocSubtitleLabel.Text = "サブタイトル：";
            this.DocSubtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocKeywordLabel
            // 
            this.DocKeywordLabel.AutoSize = true;
            this.DocKeywordLabel.Location = new System.Drawing.Point(20, 111);
            this.DocKeywordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DocKeywordLabel.Name = "DocKeywordLabel";
            this.DocKeywordLabel.Size = new System.Drawing.Size(66, 15);
            this.DocKeywordLabel.TabIndex = 100;
            this.DocKeywordLabel.Text = "キーワード：";
            this.DocKeywordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocTitleTextBox
            // 
            this.DocTitleTextBox.Location = new System.Drawing.Point(120, 21);
            this.DocTitleTextBox.Name = "DocTitleTextBox";
            this.DocTitleTextBox.Size = new System.Drawing.Size(316, 23);
            this.DocTitleTextBox.TabIndex = 30;
            // 
            // DocAuthorTextBox
            // 
            this.DocAuthorTextBox.Location = new System.Drawing.Point(120, 50);
            this.DocAuthorTextBox.Name = "DocAuthorTextBox";
            this.DocAuthorTextBox.Size = new System.Drawing.Size(316, 23);
            this.DocAuthorTextBox.TabIndex = 31;
            // 
            // DocSubtitleTextBox
            // 
            this.DocSubtitleTextBox.Location = new System.Drawing.Point(120, 79);
            this.DocSubtitleTextBox.Name = "DocSubtitleTextBox";
            this.DocSubtitleTextBox.Size = new System.Drawing.Size(316, 23);
            this.DocSubtitleTextBox.TabIndex = 32;
            // 
            // DocKeywordTextBox
            // 
            this.DocKeywordTextBox.Location = new System.Drawing.Point(120, 108);
            this.DocKeywordTextBox.Name = "DocKeywordTextBox";
            this.DocKeywordTextBox.Size = new System.Drawing.Size(316, 23);
            this.DocKeywordTextBox.TabIndex = 33;
            // 
            // SecurityTabPage
            // 
            this.SecurityTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SecurityTabPage.Controls.Add(this.SecurityPanel);
            this.SecurityTabPage.Controls.Add(this.OwnerPasswordCheckBox);
            this.SecurityTabPage.Location = new System.Drawing.Point(4, 22);
            this.SecurityTabPage.Name = "SecurityTabPage";
            this.SecurityTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SecurityTabPage.Size = new System.Drawing.Size(468, 325);
            this.SecurityTabPage.TabIndex = 2;
            this.SecurityTabPage.Text = "セキュリティ";
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
            this.SecurityPanel.Location = new System.Drawing.Point(20, 42);
            this.SecurityPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SecurityPanel.Name = "SecurityPanel";
            this.SecurityPanel.Size = new System.Drawing.Size(420, 265);
            this.SecurityPanel.TabIndex = 52;
            // 
            // UserPasswordPanel
            // 
            this.UserPasswordPanel.Controls.Add(this.UserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.UserPasswordTextBox);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordTextBox);
            this.UserPasswordPanel.Enabled = false;
            this.UserPasswordPanel.Location = new System.Drawing.Point(120, 103);
            this.UserPasswordPanel.Margin = new System.Windows.Forms.Padding(0);
            this.UserPasswordPanel.Name = "UserPasswordPanel";
            this.UserPasswordPanel.Size = new System.Drawing.Size(297, 58);
            this.UserPasswordPanel.TabIndex = 37;
            // 
            // UserPasswordLabel
            // 
            this.UserPasswordLabel.AutoSize = true;
            this.UserPasswordLabel.Location = new System.Drawing.Point(0, 6);
            this.UserPasswordLabel.Margin = new System.Windows.Forms.Padding(0);
            this.UserPasswordLabel.Name = "UserPasswordLabel";
            this.UserPasswordLabel.Size = new System.Drawing.Size(89, 15);
            this.UserPasswordLabel.TabIndex = 100;
            this.UserPasswordLabel.Text = "閲覧パスワード：";
            this.UserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmUserPasswordLabel
            // 
            this.ConfirmUserPasswordLabel.AutoSize = true;
            this.ConfirmUserPasswordLabel.Location = new System.Drawing.Point(0, 35);
            this.ConfirmUserPasswordLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ConfirmUserPasswordLabel.Name = "ConfirmUserPasswordLabel";
            this.ConfirmUserPasswordLabel.Size = new System.Drawing.Size(99, 15);
            this.ConfirmUserPasswordLabel.TabIndex = 100;
            this.ConfirmUserPasswordLabel.Text = "パスワードの確認：";
            this.ConfirmUserPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserPasswordTextBox
            // 
            this.UserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UserPasswordTextBox.Location = new System.Drawing.Point(110, 3);
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            this.UserPasswordTextBox.PasswordChar = '*';
            this.UserPasswordTextBox.Size = new System.Drawing.Size(184, 23);
            this.UserPasswordTextBox.TabIndex = 56;
            // 
            // ConfirmUserPasswordTextBox
            // 
            this.ConfirmUserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmUserPasswordTextBox.Location = new System.Drawing.Point(110, 32);
            this.ConfirmUserPasswordTextBox.Name = "ConfirmUserPasswordTextBox";
            this.ConfirmUserPasswordTextBox.PasswordChar = '*';
            this.ConfirmUserPasswordTextBox.Size = new System.Drawing.Size(184, 23);
            this.ConfirmUserPasswordTextBox.TabIndex = 57;
            // 
            // UserPasswordCheckBox
            // 
            this.UserPasswordCheckBox.AutoSize = true;
            this.UserPasswordCheckBox.Enabled = false;
            this.UserPasswordCheckBox.Location = new System.Drawing.Point(120, 84);
            this.UserPasswordCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.UserPasswordCheckBox.Name = "UserPasswordCheckBox";
            this.UserPasswordCheckBox.Size = new System.Drawing.Size(182, 19);
            this.UserPasswordCheckBox.TabIndex = 55;
            this.UserPasswordCheckBox.Text = "閲覧専用のパスワードを設定する";
            this.UserPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordTextBox
            // 
            this.OwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OwnerPasswordTextBox.Location = new System.Drawing.Point(100, 4);
            this.OwnerPasswordTextBox.Name = "OwnerPasswordTextBox";
            this.OwnerPasswordTextBox.PasswordChar = '*';
            this.OwnerPasswordTextBox.Size = new System.Drawing.Size(317, 23);
            this.OwnerPasswordTextBox.TabIndex = 52;
            // 
            // ConfirmOwnerPasswordTextBox
            // 
            this.ConfirmOwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmOwnerPasswordTextBox.Location = new System.Drawing.Point(100, 33);
            this.ConfirmOwnerPasswordTextBox.Name = "ConfirmOwnerPasswordTextBox";
            this.ConfirmOwnerPasswordTextBox.PasswordChar = '*';
            this.ConfirmOwnerPasswordTextBox.Size = new System.Drawing.Size(317, 23);
            this.ConfirmOwnerPasswordTextBox.TabIndex = 53;
            // 
            // AllowPrintCheckBox
            // 
            this.AllowPrintCheckBox.AutoSize = true;
            this.AllowPrintCheckBox.Location = new System.Drawing.Point(100, 164);
            this.AllowPrintCheckBox.Name = "AllowPrintCheckBox";
            this.AllowPrintCheckBox.Size = new System.Drawing.Size(102, 19);
            this.AllowPrintCheckBox.TabIndex = 58;
            this.AllowPrintCheckBox.Text = "印刷を許可する";
            this.AllowPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowCopyCheckBox
            // 
            this.AllowCopyCheckBox.AutoSize = true;
            this.AllowCopyCheckBox.Location = new System.Drawing.Point(100, 189);
            this.AllowCopyCheckBox.Name = "AllowCopyCheckBox";
            this.AllowCopyCheckBox.Size = new System.Drawing.Size(185, 19);
            this.AllowCopyCheckBox.TabIndex = 59;
            this.AllowCopyCheckBox.Text = "テキストや画像のコピーを許可する";
            this.AllowCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordLabel
            // 
            this.OwnerPasswordLabel.AutoSize = true;
            this.OwnerPasswordLabel.Location = new System.Drawing.Point(0, 7);
            this.OwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OwnerPasswordLabel.Name = "OwnerPasswordLabel";
            this.OwnerPasswordLabel.Size = new System.Drawing.Size(89, 15);
            this.OwnerPasswordLabel.TabIndex = 100;
            this.OwnerPasswordLabel.Text = "編集パスワード：";
            this.OwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmOwnerPasswordLabel
            // 
            this.ConfirmOwnerPasswordLabel.AutoSize = true;
            this.ConfirmOwnerPasswordLabel.Location = new System.Drawing.Point(-3, 36);
            this.ConfirmOwnerPasswordLabel.Margin = new System.Windows.Forms.Padding(3);
            this.ConfirmOwnerPasswordLabel.Name = "ConfirmOwnerPasswordLabel";
            this.ConfirmOwnerPasswordLabel.Size = new System.Drawing.Size(99, 15);
            this.ConfirmOwnerPasswordLabel.TabIndex = 100;
            this.ConfirmOwnerPasswordLabel.Text = "パスワードの確認：";
            this.ConfirmOwnerPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PermissionLabel
            // 
            this.PermissionLabel.AutoSize = true;
            this.PermissionLabel.Location = new System.Drawing.Point(0, 63);
            this.PermissionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PermissionLabel.Name = "PermissionLabel";
            this.PermissionLabel.Size = new System.Drawing.Size(43, 15);
            this.PermissionLabel.TabIndex = 100;
            this.PermissionLabel.Text = "操作：";
            this.PermissionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AllowFormInputCheckBox
            // 
            this.AllowFormInputCheckBox.AutoSize = true;
            this.AllowFormInputCheckBox.Location = new System.Drawing.Point(100, 214);
            this.AllowFormInputCheckBox.Name = "AllowFormInputCheckBox";
            this.AllowFormInputCheckBox.Size = new System.Drawing.Size(201, 19);
            this.AllowFormInputCheckBox.TabIndex = 60;
            this.AllowFormInputCheckBox.Text = "フォームフィールドへの入力を許可する";
            this.AllowFormInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowModifyCheckBox
            // 
            this.AllowModifyCheckBox.AutoSize = true;
            this.AllowModifyCheckBox.Location = new System.Drawing.Point(100, 239);
            this.AllowModifyCheckBox.Name = "AllowModifyCheckBox";
            this.AllowModifyCheckBox.Size = new System.Drawing.Size(235, 19);
            this.AllowModifyCheckBox.TabIndex = 61;
            this.AllowModifyCheckBox.Text = "ページの挿入、回転、および削除を許可する";
            this.AllowModifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // RequiredUserPasswordCheckBox
            // 
            this.RequiredUserPasswordCheckBox.AutoSize = true;
            this.RequiredUserPasswordCheckBox.Location = new System.Drawing.Point(100, 62);
            this.RequiredUserPasswordCheckBox.Name = "RequiredUserPasswordCheckBox";
            this.RequiredUserPasswordCheckBox.Size = new System.Drawing.Size(229, 19);
            this.RequiredUserPasswordCheckBox.TabIndex = 54;
            this.RequiredUserPasswordCheckBox.Text = "PDFファイルを開く際にパスワードを要求する";
            this.RequiredUserPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordCheckBox
            // 
            this.OwnerPasswordCheckBox.AutoSize = true;
            this.OwnerPasswordCheckBox.Location = new System.Drawing.Point(20, 22);
            this.OwnerPasswordCheckBox.Name = "OwnerPasswordCheckBox";
            this.OwnerPasswordCheckBox.Size = new System.Drawing.Size(202, 19);
            this.OwnerPasswordCheckBox.TabIndex = 53;
            this.OwnerPasswordCheckBox.Text = "パスワードによるセキュリティを設定する";
            this.OwnerPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // DetailTabPage
            // 
            this.DetailTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetailTabPage.Controls.Add(this.DetailPanel);
            this.DetailTabPage.Location = new System.Drawing.Point(4, 22);
            this.DetailTabPage.Name = "DetailTabPage";
            this.DetailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DetailTabPage.Size = new System.Drawing.Size(468, 325);
            this.DetailTabPage.TabIndex = 3;
            this.DetailTabPage.Text = "詳細設定";
            // 
            // DetailPanel
            // 
            this.DetailPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetailPanel.Controls.Add(this.OrientationPanel);
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
            this.DetailPanel.Size = new System.Drawing.Size(462, 319);
            this.DetailPanel.TabIndex = 0;
            // 
            // OrientationPanel
            // 
            this.OrientationPanel.Controls.Add(this.PortraitRadioButton);
            this.OrientationPanel.Controls.Add(this.LandscapeRadioButton);
            this.OrientationPanel.Controls.Add(this.AutoRadioButton);
            this.OrientationPanel.Location = new System.Drawing.Point(120, 17);
            this.OrientationPanel.Name = "OrientationPanel";
            this.OrientationPanel.Size = new System.Drawing.Size(316, 24);
            this.OrientationPanel.TabIndex = 101;
            // 
            // PortraitRadioButton
            // 
            this.PortraitRadioButton.AutoSize = true;
            this.PortraitRadioButton.Location = new System.Drawing.Point(3, 3);
            this.PortraitRadioButton.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.PortraitRadioButton.Name = "PortraitRadioButton";
            this.PortraitRadioButton.Size = new System.Drawing.Size(37, 19);
            this.PortraitRadioButton.TabIndex = 74;
            this.PortraitRadioButton.Text = "縦";
            this.PortraitRadioButton.UseVisualStyleBackColor = true;
            // 
            // LandscapeRadioButton
            // 
            this.LandscapeRadioButton.AutoSize = true;
            this.LandscapeRadioButton.Location = new System.Drawing.Point(55, 3);
            this.LandscapeRadioButton.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.LandscapeRadioButton.Name = "LandscapeRadioButton";
            this.LandscapeRadioButton.Size = new System.Drawing.Size(37, 19);
            this.LandscapeRadioButton.TabIndex = 75;
            this.LandscapeRadioButton.Text = "横";
            this.LandscapeRadioButton.UseVisualStyleBackColor = true;
            // 
            // AutoRadioButton
            // 
            this.AutoRadioButton.AutoSize = true;
            this.AutoRadioButton.Checked = true;
            this.AutoRadioButton.Location = new System.Drawing.Point(107, 3);
            this.AutoRadioButton.Name = "AutoRadioButton";
            this.AutoRadioButton.Size = new System.Drawing.Size(49, 19);
            this.AutoRadioButton.TabIndex = 76;
            this.AutoRadioButton.TabStop = true;
            this.AutoRadioButton.Text = "自動";
            this.AutoRadioButton.UseVisualStyleBackColor = true;
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.Location = new System.Drawing.Point(20, 24);
            this.OrientationLabel.Name = "OrientationLabel";
            this.OrientationLabel.Size = new System.Drawing.Size(80, 15);
            this.OrientationLabel.TabIndex = 100;
            this.OrientationLabel.Text = "ページの向き：";
            // 
            // OptionLabel
            // 
            this.OptionLabel.AutoSize = true;
            this.OptionLabel.Location = new System.Drawing.Point(20, 48);
            this.OptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OptionLabel.Name = "OptionLabel";
            this.OptionLabel.Size = new System.Drawing.Size(62, 15);
            this.OptionLabel.TabIndex = 100;
            this.OptionLabel.Text = "オプション：";
            this.OptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OthersLabel
            // 
            this.OthersLabel.AutoSize = true;
            this.OthersLabel.Location = new System.Drawing.Point(20, 148);
            this.OthersLabel.Margin = new System.Windows.Forms.Padding(3);
            this.OthersLabel.Name = "OthersLabel";
            this.OthersLabel.Size = new System.Drawing.Size(50, 15);
            this.OthersLabel.TabIndex = 100;
            this.OthersLabel.Text = "その他：";
            this.OthersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmbedFontCheckBox
            // 
            this.EmbedFontCheckBox.AutoSize = true;
            this.EmbedFontCheckBox.Location = new System.Drawing.Point(120, 47);
            this.EmbedFontCheckBox.Name = "EmbedFontCheckBox";
            this.EmbedFontCheckBox.Size = new System.Drawing.Size(114, 19);
            this.EmbedFontCheckBox.TabIndex = 74;
            this.EmbedFontCheckBox.Text = "フォントの埋め込み";
            this.EmbedFontCheckBox.UseVisualStyleBackColor = true;
            // 
            // GrayscaleCheckBox
            // 
            this.GrayscaleCheckBox.AutoSize = true;
            this.GrayscaleCheckBox.Location = new System.Drawing.Point(120, 72);
            this.GrayscaleCheckBox.Name = "GrayscaleCheckBox";
            this.GrayscaleCheckBox.Size = new System.Drawing.Size(92, 19);
            this.GrayscaleCheckBox.TabIndex = 75;
            this.GrayscaleCheckBox.Text = "グレースケール";
            this.GrayscaleCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImageFilterCheckBox
            // 
            this.ImageFilterCheckBox.AutoSize = true;
            this.ImageFilterCheckBox.Location = new System.Drawing.Point(120, 97);
            this.ImageFilterCheckBox.Name = "ImageFilterCheckBox";
            this.ImageFilterCheckBox.Size = new System.Drawing.Size(144, 19);
            this.ImageFilterCheckBox.TabIndex = 76;
            this.ImageFilterCheckBox.Text = "画像をJPEG形式に圧縮";
            this.ImageFilterCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebOptimizeCheckBox
            // 
            this.WebOptimizeCheckBox.AutoSize = true;
            this.WebOptimizeCheckBox.Location = new System.Drawing.Point(120, 122);
            this.WebOptimizeCheckBox.Name = "WebOptimizeCheckBox";
            this.WebOptimizeCheckBox.Size = new System.Drawing.Size(132, 19);
            this.WebOptimizeCheckBox.TabIndex = 77;
            this.WebOptimizeCheckBox.Text = "Web表示用に最適化";
            this.WebOptimizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.AutoSize = true;
            this.UpdateCheckBox.Location = new System.Drawing.Point(120, 147);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(176, 19);
            this.UpdateCheckBox.TabIndex = 78;
            this.UpdateCheckBox.Text = "起動時にアップデートを確認する";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostProcessLiteLabel
            // 
            this.PostProcessLiteLabel.AutoSize = true;
            this.PostProcessLiteLabel.Location = new System.Drawing.Point(20, 175);
            this.PostProcessLiteLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PostProcessLiteLabel.Name = "PostProcessLiteLabel";
            this.PostProcessLiteLabel.Size = new System.Drawing.Size(83, 15);
            this.PostProcessLiteLabel.TabIndex = 100;
            this.PostProcessLiteLabel.Text = "ポストプロセス：";
            this.PostProcessLiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PostProcessLiteComboBox
            // 
            this.PostProcessLiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessLiteComboBox.FormattingEnabled = true;
            this.PostProcessLiteComboBox.Location = new System.Drawing.Point(120, 172);
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            this.PostProcessLiteComboBox.Size = new System.Drawing.Size(316, 23);
            this.PostProcessLiteComboBox.TabIndex = 79;
            // 
            // HeaderPictureBox
            // 
            this.HeaderPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderPictureBox.Image = global::CubePdf.Properties.Resources.Header;
            this.HeaderPictureBox.Location = new System.Drawing.Point(0, 0);
            this.HeaderPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderPictureBox.Name = "HeaderPictureBox";
            this.HeaderPictureBox.Size = new System.Drawing.Size(500, 50);
            this.HeaderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HeaderPictureBox.TabIndex = 3;
            this.HeaderPictureBox.TabStop = false;
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.SettingButton);
            this.ButtonsPanel.Controls.Add(this.ExecProgressBar);
            this.ButtonsPanel.Controls.Add(this.ConvertButton);
            this.ButtonsPanel.Controls.Add(this.ExitButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 431);
            this.ButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Padding = new System.Windows.Forms.Padding(12, 10, 12, 20);
            this.ButtonsPanel.Size = new System.Drawing.Size(500, 70);
            this.ButtonsPanel.TabIndex = 5;
            // 
            // SettingButton
            // 
            this.SettingButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SettingButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SettingButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingButton.Location = new System.Drawing.Point(12, 22);
            this.SettingButton.Margin = new System.Windows.Forms.Padding(0);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(99, 28);
            this.SettingButton.TabIndex = 7;
            this.SettingButton.Text = "設定の保存";
            this.SettingButton.UseVisualStyleBackColor = false;
            // 
            // ExecProgressBar
            // 
            this.ExecProgressBar.Location = new System.Drawing.Point(12, 35);
            this.ExecProgressBar.Name = "ExecProgressBar";
            this.ExecProgressBar.Size = new System.Drawing.Size(200, 15);
            this.ExecProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ExecProgressBar.TabIndex = 8;
            this.ExecProgressBar.Visible = false;
            // 
            // ConvertButton
            // 
            this.ConvertButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(39)))), ((int)(((byte)(45)))));
            this.ConvertButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ConvertButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ConvertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConvertButton.ForeColor = System.Drawing.Color.White;
            this.ConvertButton.Location = new System.Drawing.Point(220, 10);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(139, 40);
            this.ConvertButton.TabIndex = 6;
            this.ConvertButton.Text = "変換";
            this.ConvertButton.UseVisualStyleBackColor = false;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ExitButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ExitButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(365, 10);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(119, 40);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "キャンセル";
            this.ExitButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(500, 501);
            this.Controls.Add(this.LayoutPanel);
            this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubePDF";
            this.LayoutPanel.ResumeLayout(false);
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
            this.SecurityTabPage.PerformLayout();
            this.SecurityPanel.ResumeLayout(false);
            this.SecurityPanel.PerformLayout();
            this.UserPasswordPanel.ResumeLayout(false);
            this.UserPasswordPanel.PerformLayout();
            this.DetailTabPage.ResumeLayout(false);
            this.DetailPanel.ResumeLayout(false);
            this.DetailPanel.PerformLayout();
            this.OrientationPanel.ResumeLayout(false);
            this.OrientationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPictureBox)).EndInit();
            this.ButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private System.Windows.Forms.PictureBox HeaderPictureBox;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
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
        private System.Windows.Forms.TabPage DocTabPage;
        private System.Windows.Forms.Panel DocPanel;
        private System.Windows.Forms.Label DocTitleLabel;
        private System.Windows.Forms.Label DocAuthorLabel;
        private System.Windows.Forms.Label DocSubtitleLabel;
        private System.Windows.Forms.Label DocKeywordLabel;
        private System.Windows.Forms.TextBox DocTitleTextBox;
        private System.Windows.Forms.TextBox DocAuthorTextBox;
        private System.Windows.Forms.TextBox DocSubtitleTextBox;
        private System.Windows.Forms.TextBox DocKeywordTextBox;
        private System.Windows.Forms.TabPage SecurityTabPage;
        private System.Windows.Forms.TabPage DetailTabPage;
        private System.Windows.Forms.Panel DetailPanel;
        private System.Windows.Forms.Label OrientationLabel;
        private System.Windows.Forms.Label OptionLabel;
        private System.Windows.Forms.Label OthersLabel;
        private System.Windows.Forms.CheckBox EmbedFontCheckBox;
        private System.Windows.Forms.CheckBox GrayscaleCheckBox;
        private System.Windows.Forms.CheckBox ImageFilterCheckBox;
        private System.Windows.Forms.CheckBox WebOptimizeCheckBox;
        private System.Windows.Forms.CheckBox UpdateCheckBox;
        private System.Windows.Forms.Label PostProcessLiteLabel;
        private System.Windows.Forms.ComboBox PostProcessLiteComboBox;
        private System.Windows.Forms.Panel ButtonsPanel;
        private Cube.Forms.Button SettingButton;
        private Cube.Forms.Button ExitButton;
        private Cube.Forms.Button ConvertButton;
        private System.Windows.Forms.ProgressBar ExecProgressBar;
        private System.Windows.Forms.Panel SecurityPanel;
        private System.Windows.Forms.Panel UserPasswordPanel;
        private System.Windows.Forms.Label UserPasswordLabel;
        private System.Windows.Forms.Label ConfirmUserPasswordLabel;
        private System.Windows.Forms.TextBox UserPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmUserPasswordTextBox;
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
        private System.Windows.Forms.CheckBox OwnerPasswordCheckBox;
        private System.Windows.Forms.FlowLayoutPanel OrientationPanel;
        private System.Windows.Forms.RadioButton PortraitRadioButton;
        private System.Windows.Forms.RadioButton LandscapeRadioButton;
        private System.Windows.Forms.RadioButton AutoRadioButton;
    }
}

