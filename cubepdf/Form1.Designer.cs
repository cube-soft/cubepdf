namespace CubePDF
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.CancelBox = new System.Windows.Forms.Button();
            this.MakePDFBox = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.SelectUserProgramButton = new System.Windows.Forms.Button();
            this.UserProgramTextBox = new System.Windows.Forms.TextBox();
            this.PostProcessComboBox = new System.Windows.Forms.ComboBox();
            this.PostProcessLabel = new System.Windows.Forms.Label();
            this.existedFileComboBox = new System.Windows.Forms.ComboBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.InputPathTextBox = new System.Windows.Forms.TextBox();
            this.InputPathLabel = new System.Windows.Forms.Label();
            this.ResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.VersionComboBox = new System.Windows.Forms.ComboBox();
            this.FileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TextPropertyPanel = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.KeywordTextBox = new System.Windows.Forms.TextBox();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SubTitleTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PermissionGroupBox = new System.Windows.Forms.GroupBox();
            this.OwnerPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.OwnerPasswordPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.OwnerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.OwnerPasswordConfirmTextBox = new System.Windows.Forms.TextBox();
            this.PageInsertEtcEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.InputFormEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.CopyEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.PrintEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.SecurityGroupBox = new System.Windows.Forms.GroupBox();
            this.UserPasswordPanel = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.UserPasswordConfirmTextBox = new System.Windows.Forms.TextBox();
            this.UserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.SaveOptionsCheckBox = new System.Windows.Forms.CheckBox();
            this.PostProcessLiteComboBox = new System.Windows.Forms.ComboBox();
            this.PostProcessLiteLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.WebOptimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GrayCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoFontCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoPageRotationCheckBox = new System.Windows.Forms.CheckBox();
            this.DownSamplingComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.BackgroundPatchPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.TextPropertyPanel.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.PermissionGroupBox.SuspendLayout();
            this.OwnerPasswordPanel.SuspendLayout();
            this.SecurityGroupBox.SuspendLayout();
            this.UserPasswordPanel.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPatchPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::CubePDF.Properties.Resources.background;
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.CancelBox);
            this.panel1.Controls.Add(this.MakePDFBox);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 423);
            this.panel1.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Gainsboro;
            this.progressBar.Location = new System.Drawing.Point(12, 396);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // CancelBox
            // 
            this.CancelBox.BackgroundImage = global::CubePDF.Properties.Resources.buttion_cancel;
            this.CancelBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CancelBox.Location = new System.Drawing.Point(368, 362);
            this.CancelBox.Margin = new System.Windows.Forms.Padding(0);
            this.CancelBox.Name = "CancelBox";
            this.CancelBox.Size = new System.Drawing.Size(117, 49);
            this.CancelBox.TabIndex = 13;
            this.CancelBox.UseVisualStyleBackColor = true;
            this.CancelBox.Click += new System.EventHandler(this.CancelBox_Click);
            // 
            // MakePDFBox
            // 
            this.MakePDFBox.BackgroundImage = global::CubePDF.Properties.Resources.buttion_convert;
            this.MakePDFBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MakePDFBox.Location = new System.Drawing.Point(225, 362);
            this.MakePDFBox.Margin = new System.Windows.Forms.Padding(0);
            this.MakePDFBox.Name = "MakePDFBox";
            this.MakePDFBox.Size = new System.Drawing.Size(137, 49);
            this.MakePDFBox.TabIndex = 12;
            this.MakePDFBox.UseVisualStyleBackColor = true;
            this.MakePDFBox.Click += new System.EventHandler(this.MakePDFBox_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(12, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(473, 355);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.tabPage1.Controls.Add(this.OutputPathTextBox);
            this.tabPage1.Controls.Add(this.SelectUserProgramButton);
            this.tabPage1.Controls.Add(this.UserProgramTextBox);
            this.tabPage1.Controls.Add(this.PostProcessComboBox);
            this.tabPage1.Controls.Add(this.PostProcessLabel);
            this.tabPage1.Controls.Add(this.existedFileComboBox);
            this.tabPage1.Controls.Add(this.SelectFileButton);
            this.tabPage1.Controls.Add(this.InputPathTextBox);
            this.tabPage1.Controls.Add(this.InputPathLabel);
            this.tabPage1.Controls.Add(this.ResolutionComboBox);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.SaveFileButton);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.VersionComboBox);
            this.tabPage1.Controls.Add(this.FileTypeComboBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(465, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "一般";
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Location = new System.Drawing.Point(120, 102);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(192, 19);
            this.OutputPathTextBox.TabIndex = 4;
            this.OutputPathTextBox.Click += new System.EventHandler(this.FilePathTextBoxFocused);
            this.OutputPathTextBox.Leave += new System.EventHandler(this.FilePathTextBoxLeaved);
            // 
            // SelectUserProgramButton
            // 
            this.SelectUserProgramButton.BackColor = System.Drawing.Color.LightGray;
            this.SelectUserProgramButton.Location = new System.Drawing.Point(394, 130);
            this.SelectUserProgramButton.Name = "SelectUserProgramButton";
            this.SelectUserProgramButton.Size = new System.Drawing.Size(33, 20);
            this.SelectUserProgramButton.TabIndex = 10;
            this.SelectUserProgramButton.Text = "...";
            this.SelectUserProgramButton.UseVisualStyleBackColor = false;
            this.SelectUserProgramButton.Click += new System.EventHandler(this.SelectUserProgramButton_Click);
            // 
            // UserProgramTextBox
            // 
            this.UserProgramTextBox.Location = new System.Drawing.Point(220, 131);
            this.UserProgramTextBox.Name = "UserProgramTextBox";
            this.UserProgramTextBox.Size = new System.Drawing.Size(166, 19);
            this.UserProgramTextBox.TabIndex = 9;
            this.UserProgramTextBox.Click += new System.EventHandler(this.FilePathTextBoxFocused);
            this.UserProgramTextBox.Leave += new System.EventHandler(this.FilePathTextBoxLeaved);
            // 
            // PostProcessComboBox
            // 
            this.PostProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessComboBox.FormattingEnabled = true;
            this.PostProcessComboBox.Location = new System.Drawing.Point(119, 131);
            this.PostProcessComboBox.Name = "PostProcessComboBox";
            this.PostProcessComboBox.Size = new System.Drawing.Size(95, 20);
            this.PostProcessComboBox.TabIndex = 8;
            this.PostProcessComboBox.SelectedIndexChanged += new System.EventHandler(this.PostProcessComboBox_SelectedIndexChanged);
            // 
            // PostProcessLabel
            // 
            this.PostProcessLabel.AutoSize = true;
            this.PostProcessLabel.Location = new System.Drawing.Point(23, 134);
            this.PostProcessLabel.Name = "PostProcessLabel";
            this.PostProcessLabel.Size = new System.Drawing.Size(71, 12);
            this.PostProcessLabel.TabIndex = 33;
            this.PostProcessLabel.Text = "ポストプロセス:";
            // 
            // existedFileComboBox
            // 
            this.existedFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.existedFileComboBox.FormattingEnabled = true;
            this.existedFileComboBox.Location = new System.Drawing.Point(357, 101);
            this.existedFileComboBox.Name = "existedFileComboBox";
            this.existedFileComboBox.Size = new System.Drawing.Size(70, 20);
            this.existedFileComboBox.TabIndex = 7;
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.BackColor = System.Drawing.Color.LightGray;
            this.SelectFileButton.Location = new System.Drawing.Point(394, 160);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(33, 20);
            this.SelectFileButton.TabIndex = 12;
            this.SelectFileButton.TabStop = false;
            this.SelectFileButton.Text = "...";
            this.SelectFileButton.UseVisualStyleBackColor = false;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // InputPathTextBox
            // 
            this.InputPathTextBox.Location = new System.Drawing.Point(120, 161);
            this.InputPathTextBox.Name = "InputPathTextBox";
            this.InputPathTextBox.Size = new System.Drawing.Size(266, 19);
            this.InputPathTextBox.TabIndex = 11;
            this.InputPathTextBox.Click += new System.EventHandler(this.FilePathTextBoxFocused);
            this.InputPathTextBox.Leave += new System.EventHandler(this.FilePathTextBoxLeaved);
            // 
            // InputPathLabel
            // 
            this.InputPathLabel.AutoSize = true;
            this.InputPathLabel.Location = new System.Drawing.Point(23, 164);
            this.InputPathLabel.Name = "InputPathLabel";
            this.InputPathLabel.Size = new System.Drawing.Size(65, 12);
            this.InputPathLabel.TabIndex = 18;
            this.InputPathLabel.Text = "入力ファイル:";
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionComboBox.FormattingEnabled = true;
            this.ResolutionComboBox.Location = new System.Drawing.Point(120, 73);
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            this.ResolutionComboBox.Size = new System.Drawing.Size(307, 20);
            this.ResolutionComboBox.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(23, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 12);
            this.label16.TabIndex = 17;
            this.label16.Text = "解像度:";
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.BackColor = System.Drawing.Color.LightGray;
            this.SaveFileButton.Location = new System.Drawing.Point(318, 101);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(33, 20);
            this.SaveFileButton.TabIndex = 6;
            this.SaveFileButton.TabStop = false;
            this.SaveFileButton.Text = "...";
            this.SaveFileButton.UseVisualStyleBackColor = false;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "出力ファイル:";
            // 
            // VersionComboBox
            // 
            this.VersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionComboBox.FormattingEnabled = true;
            this.VersionComboBox.Location = new System.Drawing.Point(120, 45);
            this.VersionComboBox.Name = "VersionComboBox";
            this.VersionComboBox.Size = new System.Drawing.Size(307, 20);
            this.VersionComboBox.TabIndex = 2;
            // 
            // FileTypeComboBox
            // 
            this.FileTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileTypeComboBox.FormattingEnabled = true;
            this.FileTypeComboBox.Location = new System.Drawing.Point(120, 17);
            this.FileTypeComboBox.Name = "FileTypeComboBox";
            this.FileTypeComboBox.Size = new System.Drawing.Size(307, 20);
            this.FileTypeComboBox.TabIndex = 1;
            this.FileTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.FileTypeComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PDF バージョン:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ファイルタイプ:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.tabPage2.Controls.Add(this.TextPropertyPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(465, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "文書プロパティ";
            // 
            // TextPropertyPanel
            // 
            this.TextPropertyPanel.BackColor = System.Drawing.Color.Transparent;
            this.TextPropertyPanel.Controls.Add(this.label8);
            this.TextPropertyPanel.Controls.Add(this.KeywordTextBox);
            this.TextPropertyPanel.Controls.Add(this.AuthorTextBox);
            this.TextPropertyPanel.Controls.Add(this.TitleTextBox);
            this.TextPropertyPanel.Controls.Add(this.label10);
            this.TextPropertyPanel.Controls.Add(this.label11);
            this.TextPropertyPanel.Controls.Add(this.SubTitleTextBox);
            this.TextPropertyPanel.Controls.Add(this.label9);
            this.TextPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPropertyPanel.Location = new System.Drawing.Point(3, 3);
            this.TextPropertyPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TextPropertyPanel.Name = "TextPropertyPanel";
            this.TextPropertyPanel.Size = new System.Drawing.Size(459, 324);
            this.TextPropertyPanel.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "タイトル:";
            // 
            // KeywordTextBox
            // 
            this.KeywordTextBox.Location = new System.Drawing.Point(117, 95);
            this.KeywordTextBox.Name = "KeywordTextBox";
            this.KeywordTextBox.Size = new System.Drawing.Size(307, 19);
            this.KeywordTextBox.TabIndex = 4;
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Location = new System.Drawing.Point(117, 40);
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.Size = new System.Drawing.Size(307, 19);
            this.AuthorTextBox.TabIndex = 2;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(117, 15);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(307, 19);
            this.TitleTextBox.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "サブタイトル:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "キーワード:";
            // 
            // SubTitleTextBox
            // 
            this.SubTitleTextBox.Location = new System.Drawing.Point(117, 68);
            this.SubTitleTextBox.Name = "SubTitleTextBox";
            this.SubTitleTextBox.Size = new System.Drawing.Size(307, 19);
            this.SubTitleTextBox.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "作成者:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.tabPage3.Controls.Add(this.PermissionGroupBox);
            this.tabPage3.Controls.Add(this.SecurityGroupBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(465, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "セキュリティ";
            // 
            // PermissionGroupBox
            // 
            this.PermissionGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.PermissionGroupBox.Controls.Add(this.OwnerPasswordCheckBox);
            this.PermissionGroupBox.Controls.Add(this.OwnerPasswordPanel);
            this.PermissionGroupBox.Location = new System.Drawing.Point(23, 120);
            this.PermissionGroupBox.Name = "PermissionGroupBox";
            this.PermissionGroupBox.Size = new System.Drawing.Size(410, 197);
            this.PermissionGroupBox.TabIndex = 1;
            this.PermissionGroupBox.TabStop = false;
            this.PermissionGroupBox.Text = "許可";
            // 
            // OwnerPasswordCheckBox
            // 
            this.OwnerPasswordCheckBox.AutoSize = true;
            this.OwnerPasswordCheckBox.Location = new System.Drawing.Point(6, 18);
            this.OwnerPasswordCheckBox.Name = "OwnerPasswordCheckBox";
            this.OwnerPasswordCheckBox.Size = new System.Drawing.Size(293, 16);
            this.OwnerPasswordCheckBox.TabIndex = 4;
            this.OwnerPasswordCheckBox.Text = "セキュリティ機能及び指定機能の変更にパスワードが必要";
            this.OwnerPasswordCheckBox.UseVisualStyleBackColor = true;
            this.OwnerPasswordCheckBox.CheckedChanged += new System.EventHandler(this.OwnerPasswordCheckBox_CheckedChanged);
            // 
            // OwnerPasswordPanel
            // 
            this.OwnerPasswordPanel.Controls.Add(this.label4);
            this.OwnerPasswordPanel.Controls.Add(this.OwnerPasswordTextBox);
            this.OwnerPasswordPanel.Controls.Add(this.label14);
            this.OwnerPasswordPanel.Controls.Add(this.OwnerPasswordConfirmTextBox);
            this.OwnerPasswordPanel.Controls.Add(this.PageInsertEtcEnableCheckBox);
            this.OwnerPasswordPanel.Controls.Add(this.label15);
            this.OwnerPasswordPanel.Controls.Add(this.InputFormEnableCheckBox);
            this.OwnerPasswordPanel.Controls.Add(this.CopyEnableCheckBox);
            this.OwnerPasswordPanel.Controls.Add(this.PrintEnableCheckBox);
            this.OwnerPasswordPanel.Location = new System.Drawing.Point(0, 40);
            this.OwnerPasswordPanel.Name = "OwnerPasswordPanel";
            this.OwnerPasswordPanel.Size = new System.Drawing.Size(404, 151);
            this.OwnerPasswordPanel.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "許可する操作:";
            // 
            // OwnerPasswordTextBox
            // 
            this.OwnerPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OwnerPasswordTextBox.Location = new System.Drawing.Point(120, 4);
            this.OwnerPasswordTextBox.Name = "OwnerPasswordTextBox";
            this.OwnerPasswordTextBox.PasswordChar = '*';
            this.OwnerPasswordTextBox.Size = new System.Drawing.Size(276, 19);
            this.OwnerPasswordTextBox.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "パスワード:";
            // 
            // OwnerPasswordConfirmTextBox
            // 
            this.OwnerPasswordConfirmTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OwnerPasswordConfirmTextBox.Location = new System.Drawing.Point(120, 31);
            this.OwnerPasswordConfirmTextBox.Name = "OwnerPasswordConfirmTextBox";
            this.OwnerPasswordConfirmTextBox.PasswordChar = '*';
            this.OwnerPasswordConfirmTextBox.Size = new System.Drawing.Size(276, 19);
            this.OwnerPasswordConfirmTextBox.TabIndex = 6;
            // 
            // PageInsertEtcEnableCheckBox
            // 
            this.PageInsertEtcEnableCheckBox.AutoSize = true;
            this.PageInsertEtcEnableCheckBox.Location = new System.Drawing.Point(120, 123);
            this.PageInsertEtcEnableCheckBox.Name = "PageInsertEtcEnableCheckBox";
            this.PageInsertEtcEnableCheckBox.Size = new System.Drawing.Size(181, 16);
            this.PageInsertEtcEnableCheckBox.TabIndex = 10;
            this.PageInsertEtcEnableCheckBox.Text = "ページの挿入、回転、および削除";
            this.PageInsertEtcEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "パスワードの確認:";
            // 
            // InputFormEnableCheckBox
            // 
            this.InputFormEnableCheckBox.AutoSize = true;
            this.InputFormEnableCheckBox.Location = new System.Drawing.Point(120, 101);
            this.InputFormEnableCheckBox.Name = "InputFormEnableCheckBox";
            this.InputFormEnableCheckBox.Size = new System.Drawing.Size(138, 16);
            this.InputFormEnableCheckBox.TabIndex = 9;
            this.InputFormEnableCheckBox.Text = "フォームフィールドの入力";
            this.InputFormEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // CopyEnableCheckBox
            // 
            this.CopyEnableCheckBox.AutoSize = true;
            this.CopyEnableCheckBox.Location = new System.Drawing.Point(120, 79);
            this.CopyEnableCheckBox.Name = "CopyEnableCheckBox";
            this.CopyEnableCheckBox.Size = new System.Drawing.Size(131, 16);
            this.CopyEnableCheckBox.TabIndex = 8;
            this.CopyEnableCheckBox.Text = "テキストや画像のコピー";
            this.CopyEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrintEnableCheckBox
            // 
            this.PrintEnableCheckBox.AutoSize = true;
            this.PrintEnableCheckBox.Location = new System.Drawing.Point(120, 57);
            this.PrintEnableCheckBox.Name = "PrintEnableCheckBox";
            this.PrintEnableCheckBox.Size = new System.Drawing.Size(48, 16);
            this.PrintEnableCheckBox.TabIndex = 7;
            this.PrintEnableCheckBox.Text = "印刷";
            this.PrintEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // SecurityGroupBox
            // 
            this.SecurityGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.SecurityGroupBox.Controls.Add(this.UserPasswordPanel);
            this.SecurityGroupBox.Controls.Add(this.UserPasswordCheckBox);
            this.SecurityGroupBox.Location = new System.Drawing.Point(23, 12);
            this.SecurityGroupBox.Name = "SecurityGroupBox";
            this.SecurityGroupBox.Size = new System.Drawing.Size(416, 102);
            this.SecurityGroupBox.TabIndex = 0;
            this.SecurityGroupBox.TabStop = false;
            this.SecurityGroupBox.Text = "セキュリティ";
            // 
            // UserPasswordPanel
            // 
            this.UserPasswordPanel.Controls.Add(this.label13);
            this.UserPasswordPanel.Controls.Add(this.label12);
            this.UserPasswordPanel.Controls.Add(this.UserPasswordConfirmTextBox);
            this.UserPasswordPanel.Controls.Add(this.UserPasswordTextBox);
            this.UserPasswordPanel.Location = new System.Drawing.Point(8, 40);
            this.UserPasswordPanel.Name = "UserPasswordPanel";
            this.UserPasswordPanel.Size = new System.Drawing.Size(402, 53);
            this.UserPasswordPanel.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "パスワードの確認:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "パスワード:";
            // 
            // UserPasswordConfirmTextBox
            // 
            this.UserPasswordConfirmTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UserPasswordConfirmTextBox.Location = new System.Drawing.Point(112, 30);
            this.UserPasswordConfirmTextBox.Name = "UserPasswordConfirmTextBox";
            this.UserPasswordConfirmTextBox.PasswordChar = '*';
            this.UserPasswordConfirmTextBox.Size = new System.Drawing.Size(276, 19);
            this.UserPasswordConfirmTextBox.TabIndex = 3;
            // 
            // UserPasswordTextBox
            // 
            this.UserPasswordTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UserPasswordTextBox.Location = new System.Drawing.Point(112, 3);
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            this.UserPasswordTextBox.PasswordChar = '*';
            this.UserPasswordTextBox.Size = new System.Drawing.Size(276, 19);
            this.UserPasswordTextBox.TabIndex = 2;
            // 
            // UserPasswordCheckBox
            // 
            this.UserPasswordCheckBox.AutoSize = true;
            this.UserPasswordCheckBox.Location = new System.Drawing.Point(6, 18);
            this.UserPasswordCheckBox.Name = "UserPasswordCheckBox";
            this.UserPasswordCheckBox.Size = new System.Drawing.Size(182, 16);
            this.UserPasswordCheckBox.TabIndex = 1;
            this.UserPasswordCheckBox.Text = "文書を開くときにパスワードが必要";
            this.UserPasswordCheckBox.UseVisualStyleBackColor = true;
            this.UserPasswordCheckBox.CheckedChanged += new System.EventHandler(this.UserPasswordCheckBox_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.BackgroundImage = global::CubePDF.Properties.Resources.background_tab;
            this.tabPage4.Controls.Add(this.SaveOptionsCheckBox);
            this.tabPage4.Controls.Add(this.PostProcessLiteComboBox);
            this.tabPage4.Controls.Add(this.PostProcessLiteLabel);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.UpdateCheckBox);
            this.tabPage4.Controls.Add(this.WebOptimizeCheckBox);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.GrayCheckBox);
            this.tabPage4.Controls.Add(this.AutoFontCheckBox);
            this.tabPage4.Controls.Add(this.AutoPageRotationCheckBox);
            this.tabPage4.Controls.Add(this.DownSamplingComboBox);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(465, 330);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "詳細設定";
            // 
            // SaveOptionsCheckBox
            // 
            this.SaveOptionsCheckBox.AutoSize = true;
            this.SaveOptionsCheckBox.Location = new System.Drawing.Point(119, 134);
            this.SaveOptionsCheckBox.Name = "SaveOptionsCheckBox";
            this.SaveOptionsCheckBox.Size = new System.Drawing.Size(100, 16);
            this.SaveOptionsCheckBox.TabIndex = 38;
            this.SaveOptionsCheckBox.Text = "設定を保存する";
            this.SaveOptionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostProcessLiteComboBox
            // 
            this.PostProcessLiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PostProcessLiteComboBox.FormattingEnabled = true;
            this.PostProcessLiteComboBox.Location = new System.Drawing.Point(119, 181);
            this.PostProcessLiteComboBox.Name = "PostProcessLiteComboBox";
            this.PostProcessLiteComboBox.Size = new System.Drawing.Size(308, 20);
            this.PostProcessLiteComboBox.TabIndex = 36;
            this.PostProcessLiteComboBox.SelectedIndexChanged += new System.EventHandler(this.PostProcessComboBox_SelectedIndexChanged);
            // 
            // PostProcessLiteLabel
            // 
            this.PostProcessLiteLabel.AutoSize = true;
            this.PostProcessLiteLabel.Location = new System.Drawing.Point(23, 184);
            this.PostProcessLiteLabel.Name = "PostProcessLiteLabel";
            this.PostProcessLiteLabel.Size = new System.Drawing.Size(71, 12);
            this.PostProcessLiteLabel.TabIndex = 37;
            this.PostProcessLiteLabel.Text = "ポストプロセス:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "その他:";
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.AutoSize = true;
            this.UpdateCheckBox.Location = new System.Drawing.Point(119, 156);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(174, 16);
            this.UpdateCheckBox.TabIndex = 34;
            this.UpdateCheckBox.Text = "起動時にアップデートを確認する";
            this.UpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebOptimizeCheckBox
            // 
            this.WebOptimizeCheckBox.AutoSize = true;
            this.WebOptimizeCheckBox.Location = new System.Drawing.Point(119, 111);
            this.WebOptimizeCheckBox.Name = "WebOptimizeCheckBox";
            this.WebOptimizeCheckBox.Size = new System.Drawing.Size(130, 16);
            this.WebOptimizeCheckBox.TabIndex = 5;
            this.WebOptimizeCheckBox.Text = "Web 表示用に最適化";
            this.WebOptimizeCheckBox.UseVisualStyleBackColor = true;
            this.WebOptimizeCheckBox.CheckedChanged += new System.EventHandler(this.WebOptimizeCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "オプション:";
            // 
            // GrayCheckBox
            // 
            this.GrayCheckBox.AutoSize = true;
            this.GrayCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.GrayCheckBox.Location = new System.Drawing.Point(120, 89);
            this.GrayCheckBox.Name = "GrayCheckBox";
            this.GrayCheckBox.Size = new System.Drawing.Size(90, 16);
            this.GrayCheckBox.TabIndex = 4;
            this.GrayCheckBox.Text = "グレースケール";
            this.GrayCheckBox.UseVisualStyleBackColor = false;
            // 
            // AutoFontCheckBox
            // 
            this.AutoFontCheckBox.AutoSize = true;
            this.AutoFontCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AutoFontCheckBox.Location = new System.Drawing.Point(120, 67);
            this.AutoFontCheckBox.Name = "AutoFontCheckBox";
            this.AutoFontCheckBox.Size = new System.Drawing.Size(112, 16);
            this.AutoFontCheckBox.TabIndex = 3;
            this.AutoFontCheckBox.Text = "フォントの埋め込み";
            this.AutoFontCheckBox.UseVisualStyleBackColor = false;
            // 
            // AutoPageRotationCheckBox
            // 
            this.AutoPageRotationCheckBox.AutoSize = true;
            this.AutoPageRotationCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AutoPageRotationCheckBox.Location = new System.Drawing.Point(120, 45);
            this.AutoPageRotationCheckBox.Name = "AutoPageRotationCheckBox";
            this.AutoPageRotationCheckBox.Size = new System.Drawing.Size(112, 16);
            this.AutoPageRotationCheckBox.TabIndex = 2;
            this.AutoPageRotationCheckBox.Text = "ページの自動回転";
            this.AutoPageRotationCheckBox.UseVisualStyleBackColor = false;
            // 
            // DownSamplingComboBox
            // 
            this.DownSamplingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DownSamplingComboBox.FormattingEnabled = true;
            this.DownSamplingComboBox.Location = new System.Drawing.Point(120, 17);
            this.DownSamplingComboBox.Name = "DownSamplingComboBox";
            this.DownSamplingComboBox.Size = new System.Drawing.Size(307, 20);
            this.DownSamplingComboBox.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(23, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 12);
            this.label17.TabIndex = 32;
            this.label17.Text = "ダウンサンプリング:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::CubePDF.Properties.Resources.header;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(496, 80);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // BackgroundPatchPictureBox
            // 
            this.BackgroundPatchPictureBox.BackgroundImage = global::CubePDF.Properties.Resources.background;
            this.BackgroundPatchPictureBox.Location = new System.Drawing.Point(400, 80);
            this.BackgroundPatchPictureBox.Name = "BackgroundPatchPictureBox";
            this.BackgroundPatchPictureBox.Size = new System.Drawing.Size(85, 21);
            this.BackgroundPatchPictureBox.TabIndex = 6;
            this.BackgroundPatchPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(496, 503);
            this.Controls.Add(this.BackgroundPatchPictureBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubePDF";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.TextPropertyPanel.ResumeLayout(false);
            this.TextPropertyPanel.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.PermissionGroupBox.ResumeLayout(false);
            this.PermissionGroupBox.PerformLayout();
            this.OwnerPasswordPanel.ResumeLayout(false);
            this.OwnerPasswordPanel.PerformLayout();
            this.SecurityGroupBox.ResumeLayout(false);
            this.SecurityGroupBox.PerformLayout();
            this.UserPasswordPanel.ResumeLayout(false);
            this.UserPasswordPanel.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPatchPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox FileTypeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox VersionComboBox;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox KeywordTextBox;
        private System.Windows.Forms.TextBox SubTitleTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox PermissionGroupBox;
        private System.Windows.Forms.GroupBox SecurityGroupBox;
        private System.Windows.Forms.Panel UserPasswordPanel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox UserPasswordConfirmTextBox;
        private System.Windows.Forms.TextBox UserPasswordTextBox;
        private System.Windows.Forms.CheckBox UserPasswordCheckBox;
        private System.Windows.Forms.TextBox OwnerPasswordConfirmTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox OwnerPasswordTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox OwnerPasswordCheckBox;
        private System.Windows.Forms.Panel OwnerPasswordPanel;
        private System.Windows.Forms.CheckBox PageInsertEtcEnableCheckBox;
        private System.Windows.Forms.CheckBox InputFormEnableCheckBox;
        private System.Windows.Forms.CheckBox CopyEnableCheckBox;
        private System.Windows.Forms.CheckBox PrintEnableCheckBox;
        private System.Windows.Forms.Button MakePDFBox;
        private System.Windows.Forms.Button CancelBox;
        private System.Windows.Forms.ComboBox ResolutionComboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox WebOptimizeCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox GrayCheckBox;
        private System.Windows.Forms.CheckBox AutoFontCheckBox;
        private System.Windows.Forms.CheckBox AutoPageRotationCheckBox;
        private System.Windows.Forms.ComboBox DownSamplingComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.TextBox InputPathTextBox;
        private System.Windows.Forms.Label InputPathLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox UpdateCheckBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ComboBox existedFileComboBox;
        private System.Windows.Forms.Label PostProcessLabel;
        private System.Windows.Forms.ComboBox PostProcessLiteComboBox;
        private System.Windows.Forms.Label PostProcessLiteLabel;
        private System.Windows.Forms.CheckBox SaveOptionsCheckBox;
        private System.Windows.Forms.Button SelectUserProgramButton;
        private System.Windows.Forms.TextBox UserProgramTextBox;
        private System.Windows.Forms.ComboBox PostProcessComboBox;
        private System.Windows.Forms.Panel TextPropertyPanel;
        private System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.PictureBox BackgroundPatchPictureBox;
    }
}

