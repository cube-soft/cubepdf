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
            resources.ApplyResources(this.LayoutPanel, "LayoutPanel");
            this.LayoutPanel.Controls.Add(this.MainTabControl, 0, 1);
            this.LayoutPanel.Controls.Add(this.HeaderPictureBox, 0, 0);
            this.LayoutPanel.Controls.Add(this.ButtonsPanel, 0, 2);
            this.LayoutPanel.Name = "LayoutPanel";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.GeneralTabPage);
            this.MainTabControl.Controls.Add(this.DocTabPage);
            this.MainTabControl.Controls.Add(this.SecurityTabPage);
            this.MainTabControl.Controls.Add(this.DetailTabPage);
            resources.ApplyResources(this.MainTabControl, "MainTabControl");
            this.MainTabControl.HotTrack = true;
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GeneralTabPage.Controls.Add(this.GeneralPanel);
            resources.ApplyResources(this.GeneralTabPage, "GeneralTabPage");
            this.GeneralTabPage.Name = "GeneralTabPage";
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
            resources.ApplyResources(this.GeneralPanel, "GeneralPanel");
            this.GeneralPanel.Name = "GeneralPanel";
            // 
            // InputPathPanel
            // 
            this.InputPathPanel.Controls.Add(this.InputPathButton);
            this.InputPathPanel.Controls.Add(this.InputPathTextBox);
            resources.ApplyResources(this.InputPathPanel, "InputPathPanel");
            this.InputPathPanel.Name = "InputPathPanel";
            // 
            // InputPathButton
            // 
            this.InputPathButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.InputPathButton, "InputPathButton");
            this.InputPathButton.Name = "InputPathButton";
            this.InputPathButton.UseVisualStyleBackColor = false;
            // 
            // InputPathTextBox
            // 
            resources.ApplyResources(this.InputPathTextBox, "InputPathTextBox");
            this.InputPathTextBox.Name = "InputPathTextBox";
            // 
            // OutputPathPanel
            // 
            this.OutputPathPanel.Controls.Add(this.ExistedFileComboBox);
            this.OutputPathPanel.Controls.Add(this.OutputPathButton);
            this.OutputPathPanel.Controls.Add(this.OutputPathTextBox);
            resources.ApplyResources(this.OutputPathPanel, "OutputPathPanel");
            this.OutputPathPanel.Name = "OutputPathPanel";
            // 
            // ExistedFileComboBox
            // 
            this.ExistedFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistedFileComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.ExistedFileComboBox, "ExistedFileComboBox");
            this.ExistedFileComboBox.Name = "ExistedFileComboBox";
            // 
            // OutputPathButton
            // 
            this.OutputPathButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.OutputPathButton, "OutputPathButton");
            this.OutputPathButton.Name = "OutputPathButton";
            this.OutputPathButton.UseVisualStyleBackColor = false;
            // 
            // OutputPathTextBox
            // 
            resources.ApplyResources(this.OutputPathTextBox, "OutputPathTextBox");
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            // 
            // PostProcessPanel
            // 
            this.PostProcessPanel.Controls.Add(this.UserProgramTextBox);
            this.PostProcessPanel.Controls.Add(this.UserProgramButton);
            this.PostProcessPanel.Controls.Add(this.PostProcessComboBox);
            resources.ApplyResources(this.PostProcessPanel, "PostProcessPanel");
            this.PostProcessPanel.Name = "PostProcessPanel";
            // 
            // UserProgramTextBox
            // 
            resources.ApplyResources(this.UserProgramTextBox, "UserProgramTextBox");
            this.UserProgramTextBox.Name = "UserProgramTextBox";
            // 
            // UserProgramButton
            // 
            this.UserProgramButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.UserProgramButton, "UserProgramButton");
            this.UserProgramButton.Name = "UserProgramButton";
            this.UserProgramButton.UseVisualStyleBackColor = false;
            // 
            // PostProcessComboBox
            // 
            resources.ApplyResources(this.PostProcessComboBox, "PostProcessComboBox");
            this.PostProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessComboBox.FormattingEnabled = true;
            this.PostProcessComboBox.Name = "PostProcessComboBox";
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.ResolutionComboBox, "ResolutionComboBox");
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            // 
            // PdfVersionComboBox
            // 
            this.PdfVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PdfVersionComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.PdfVersionComboBox, "PdfVersionComboBox");
            this.PdfVersionComboBox.Name = "PdfVersionComboBox";
            // 
            // FileTypeCombBox
            // 
            this.FileTypeCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileTypeCombBox.FormattingEnabled = true;
            resources.ApplyResources(this.FileTypeCombBox, "FileTypeCombBox");
            this.FileTypeCombBox.Name = "FileTypeCombBox";
            // 
            // InputPathLabel
            // 
            resources.ApplyResources(this.InputPathLabel, "InputPathLabel");
            this.InputPathLabel.Name = "InputPathLabel";
            // 
            // PostProcessLabel
            // 
            resources.ApplyResources(this.PostProcessLabel, "PostProcessLabel");
            this.PostProcessLabel.Name = "PostProcessLabel";
            // 
            // OutputPathLabel
            // 
            resources.ApplyResources(this.OutputPathLabel, "OutputPathLabel");
            this.OutputPathLabel.Name = "OutputPathLabel";
            // 
            // ResolutionLabel
            // 
            resources.ApplyResources(this.ResolutionLabel, "ResolutionLabel");
            this.ResolutionLabel.Name = "ResolutionLabel";
            // 
            // PDFVersionLabel
            // 
            resources.ApplyResources(this.PDFVersionLabel, "PDFVersionLabel");
            this.PDFVersionLabel.Name = "PDFVersionLabel";
            // 
            // FileTypeLabel
            // 
            resources.ApplyResources(this.FileTypeLabel, "FileTypeLabel");
            this.FileTypeLabel.Name = "FileTypeLabel";
            // 
            // DocTabPage
            // 
            this.DocTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DocTabPage.Controls.Add(this.DocPanel);
            resources.ApplyResources(this.DocTabPage, "DocTabPage");
            this.DocTabPage.Name = "DocTabPage";
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
            resources.ApplyResources(this.DocPanel, "DocPanel");
            this.DocPanel.Name = "DocPanel";
            // 
            // DocTitleLabel
            // 
            resources.ApplyResources(this.DocTitleLabel, "DocTitleLabel");
            this.DocTitleLabel.Name = "DocTitleLabel";
            // 
            // DocAuthorLabel
            // 
            resources.ApplyResources(this.DocAuthorLabel, "DocAuthorLabel");
            this.DocAuthorLabel.Name = "DocAuthorLabel";
            // 
            // DocSubtitleLabel
            // 
            resources.ApplyResources(this.DocSubtitleLabel, "DocSubtitleLabel");
            this.DocSubtitleLabel.Name = "DocSubtitleLabel";
            // 
            // DocKeywordLabel
            // 
            resources.ApplyResources(this.DocKeywordLabel, "DocKeywordLabel");
            this.DocKeywordLabel.Name = "DocKeywordLabel";
            // 
            // DocTitleTextBox
            // 
            resources.ApplyResources(this.DocTitleTextBox, "DocTitleTextBox");
            this.DocTitleTextBox.Name = "DocTitleTextBox";
            // 
            // DocAuthorTextBox
            // 
            resources.ApplyResources(this.DocAuthorTextBox, "DocAuthorTextBox");
            this.DocAuthorTextBox.Name = "DocAuthorTextBox";
            // 
            // DocSubtitleTextBox
            // 
            resources.ApplyResources(this.DocSubtitleTextBox, "DocSubtitleTextBox");
            this.DocSubtitleTextBox.Name = "DocSubtitleTextBox";
            // 
            // DocKeywordTextBox
            // 
            resources.ApplyResources(this.DocKeywordTextBox, "DocKeywordTextBox");
            this.DocKeywordTextBox.Name = "DocKeywordTextBox";
            // 
            // SecurityTabPage
            // 
            this.SecurityTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SecurityTabPage.Controls.Add(this.SecurityPanel);
            this.SecurityTabPage.Controls.Add(this.OwnerPasswordCheckBox);
            resources.ApplyResources(this.SecurityTabPage, "SecurityTabPage");
            this.SecurityTabPage.Name = "SecurityTabPage";
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
            resources.ApplyResources(this.SecurityPanel, "SecurityPanel");
            this.SecurityPanel.Name = "SecurityPanel";
            // 
            // UserPasswordPanel
            // 
            this.UserPasswordPanel.Controls.Add(this.UserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordLabel);
            this.UserPasswordPanel.Controls.Add(this.UserPasswordTextBox);
            this.UserPasswordPanel.Controls.Add(this.ConfirmUserPasswordTextBox);
            resources.ApplyResources(this.UserPasswordPanel, "UserPasswordPanel");
            this.UserPasswordPanel.Name = "UserPasswordPanel";
            // 
            // UserPasswordLabel
            // 
            resources.ApplyResources(this.UserPasswordLabel, "UserPasswordLabel");
            this.UserPasswordLabel.Name = "UserPasswordLabel";
            // 
            // ConfirmUserPasswordLabel
            // 
            resources.ApplyResources(this.ConfirmUserPasswordLabel, "ConfirmUserPasswordLabel");
            this.ConfirmUserPasswordLabel.Name = "ConfirmUserPasswordLabel";
            // 
            // UserPasswordTextBox
            // 
            resources.ApplyResources(this.UserPasswordTextBox, "UserPasswordTextBox");
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            // 
            // ConfirmUserPasswordTextBox
            // 
            resources.ApplyResources(this.ConfirmUserPasswordTextBox, "ConfirmUserPasswordTextBox");
            this.ConfirmUserPasswordTextBox.Name = "ConfirmUserPasswordTextBox";
            // 
            // UserPasswordCheckBox
            // 
            resources.ApplyResources(this.UserPasswordCheckBox, "UserPasswordCheckBox");
            this.UserPasswordCheckBox.Name = "UserPasswordCheckBox";
            this.UserPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordTextBox
            // 
            resources.ApplyResources(this.OwnerPasswordTextBox, "OwnerPasswordTextBox");
            this.OwnerPasswordTextBox.Name = "OwnerPasswordTextBox";
            // 
            // ConfirmOwnerPasswordTextBox
            // 
            resources.ApplyResources(this.ConfirmOwnerPasswordTextBox, "ConfirmOwnerPasswordTextBox");
            this.ConfirmOwnerPasswordTextBox.Name = "ConfirmOwnerPasswordTextBox";
            // 
            // AllowPrintCheckBox
            // 
            resources.ApplyResources(this.AllowPrintCheckBox, "AllowPrintCheckBox");
            this.AllowPrintCheckBox.Name = "AllowPrintCheckBox";
            this.AllowPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowCopyCheckBox
            // 
            resources.ApplyResources(this.AllowCopyCheckBox, "AllowCopyCheckBox");
            this.AllowCopyCheckBox.Name = "AllowCopyCheckBox";
            this.AllowCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordLabel
            // 
            resources.ApplyResources(this.OwnerPasswordLabel, "OwnerPasswordLabel");
            this.OwnerPasswordLabel.Name = "OwnerPasswordLabel";
            // 
            // ConfirmOwnerPasswordLabel
            // 
            resources.ApplyResources(this.ConfirmOwnerPasswordLabel, "ConfirmOwnerPasswordLabel");
            this.ConfirmOwnerPasswordLabel.Name = "ConfirmOwnerPasswordLabel";
            // 
            // PermissionLabel
            // 
            resources.ApplyResources(this.PermissionLabel, "PermissionLabel");
            this.PermissionLabel.Name = "PermissionLabel";
            // 
            // AllowFormInputCheckBox
            // 
            resources.ApplyResources(this.AllowFormInputCheckBox, "AllowFormInputCheckBox");
            this.AllowFormInputCheckBox.Name = "AllowFormInputCheckBox";
            this.AllowFormInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowModifyCheckBox
            // 
            resources.ApplyResources(this.AllowModifyCheckBox, "AllowModifyCheckBox");
            this.AllowModifyCheckBox.Name = "AllowModifyCheckBox";
            this.AllowModifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // RequiredUserPasswordCheckBox
            // 
            resources.ApplyResources(this.RequiredUserPasswordCheckBox, "RequiredUserPasswordCheckBox");
            this.RequiredUserPasswordCheckBox.Name = "RequiredUserPasswordCheckBox";
            this.RequiredUserPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // OwnerPasswordCheckBox
            // 
            resources.ApplyResources(this.OwnerPasswordCheckBox, "OwnerPasswordCheckBox");
            this.OwnerPasswordCheckBox.Name = "OwnerPasswordCheckBox";
            this.OwnerPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // DetailTabPage
            // 
            this.DetailTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetailTabPage.Controls.Add(this.DetailPanel);
            resources.ApplyResources(this.DetailTabPage, "DetailTabPage");
            this.DetailTabPage.Name = "DetailTabPage";
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
            resources.ApplyResources(this.DetailPanel, "DetailPanel");
            this.DetailPanel.Name = "DetailPanel";
            // 
            // OrientationPanel
            // 
            this.OrientationPanel.Controls.Add(this.PortraitRadioButton);
            this.OrientationPanel.Controls.Add(this.LandscapeRadioButton);
            this.OrientationPanel.Controls.Add(this.AutoRadioButton);
            resources.ApplyResources(this.OrientationPanel, "OrientationPanel");
            this.OrientationPanel.Name = "OrientationPanel";
            // 
            // PortraitRadioButton
            // 
            resources.ApplyResources(this.PortraitRadioButton, "PortraitRadioButton");
            this.PortraitRadioButton.Name = "PortraitRadioButton";
            this.PortraitRadioButton.UseVisualStyleBackColor = true;
            // 
            // LandscapeRadioButton
            // 
            resources.ApplyResources(this.LandscapeRadioButton, "LandscapeRadioButton");
            this.LandscapeRadioButton.Name = "LandscapeRadioButton";
            this.LandscapeRadioButton.UseVisualStyleBackColor = true;
            // 
            // AutoRadioButton
            // 
            resources.ApplyResources(this.AutoRadioButton, "AutoRadioButton");
            this.AutoRadioButton.Checked = true;
            this.AutoRadioButton.Name = "AutoRadioButton";
            this.AutoRadioButton.TabStop = true;
            this.AutoRadioButton.UseVisualStyleBackColor = true;
            // 
            // OrientationLabel
            // 
            resources.ApplyResources(this.OrientationLabel, "OrientationLabel");
            this.OrientationLabel.Name = "OrientationLabel";
            // 
            // OptionLabel
            // 
            resources.ApplyResources(this.OptionLabel, "OptionLabel");
            this.OptionLabel.Name = "OptionLabel";
            // 
            // OthersLabel
            // 
            resources.ApplyResources(this.OthersLabel, "OthersLabel");
            this.OthersLabel.Name = "OthersLabel";
            // 
            // EmbedFontCheckBox
            // 
            resources.ApplyResources(this.EmbedFontCheckBox, "EmbedFontCheckBox");
            this.EmbedFontCheckBox.Name = "EmbedFontCheckBox";
            this.EmbedFontCheckBox.UseVisualStyleBackColor = true;
            // 
            // GrayscaleCheckBox
            // 
            resources.ApplyResources(this.GrayscaleCheckBox, "GrayscaleCheckBox");
            this.GrayscaleCheckBox.Name = "GrayscaleCheckBox";
            this.GrayscaleCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImageFilterCheckBox
            // 
            resources.ApplyResources(this.ImageFilterCheckBox, "ImageFilterCheckBox");
            this.ImageFilterCheckBox.Name = "ImageFilterCheckBox";
            this.ImageFilterCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebOptimizeCheckBox
            // 
            resources.ApplyResources(this.WebOptimizeCheckBox, "WebOptimizeCheckBox");
            this.WebOptimizeCheckBox.Name = "WebOptimizeCheckBox";
            this.WebOptimizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // UpdateCheckBox
            // 
            resources.ApplyResources(this.UpdateCheckBox, "UpdateCheckBox");
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostProcessLiteLabel
            // 
            resources.ApplyResources(this.PostProcessLiteLabel, "PostProcessLiteLabel");
            this.PostProcessLiteLabel.Name = "PostProcessLiteLabel";
            // 
            // PostProcessLiteComboBox
            // 
            this.PostProcessLiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessLiteComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.PostProcessLiteComboBox, "PostProcessLiteComboBox");
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            // 
            // HeaderPictureBox
            // 
            resources.ApplyResources(this.HeaderPictureBox, "HeaderPictureBox");
            this.HeaderPictureBox.Image = global::CubePdf.Properties.Resources.Header;
            this.HeaderPictureBox.Name = "HeaderPictureBox";
            this.HeaderPictureBox.TabStop = false;
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.SettingButton);
            this.ButtonsPanel.Controls.Add(this.ExecProgressBar);
            this.ButtonsPanel.Controls.Add(this.ConvertButton);
            this.ButtonsPanel.Controls.Add(this.ExitButton);
            resources.ApplyResources(this.ButtonsPanel, "ButtonsPanel");
            this.ButtonsPanel.Name = "ButtonsPanel";
            // 
            // SettingButton
            // 
            this.SettingButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SettingButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SettingButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.SettingButton, "SettingButton");
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.UseVisualStyleBackColor = false;
            // 
            // ExecProgressBar
            // 
            resources.ApplyResources(this.ExecProgressBar, "ExecProgressBar");
            this.ExecProgressBar.Name = "ExecProgressBar";
            this.ExecProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // ConvertButton
            // 
            this.ConvertButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(39)))), ((int)(((byte)(45)))));
            this.ConvertButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ConvertButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.ConvertButton, "ConvertButton");
            this.ConvertButton.ForeColor = System.Drawing.Color.White;
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.UseVisualStyleBackColor = false;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ExitButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ExitButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.ExitButton, "ExitButton");
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.LayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
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

