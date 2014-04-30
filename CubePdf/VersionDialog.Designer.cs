namespace CubePdf
{
    partial class VersionDialog
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
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.TitlePictureBox = new System.Windows.Forms.PictureBox();
            this.CubePDFLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OSVersionLabel = new System.Windows.Forms.Label();
            this.DotNetVersionLabel = new System.Windows.Forms.Label();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.IsSplitterFixed = true;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.MainSplitContainer.Panel1.Controls.Add(this.DotNetVersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.OSVersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.CopyrightLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.VersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.TitlePictureBox);
            this.MainSplitContainer.Panel1.Controls.Add(this.CubePDFLinkLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.LogoPictureBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.CloseButton);
            this.MainSplitContainer.Size = new System.Drawing.Size(344, 252);
            this.MainSplitContainer.SplitterDistance = 200;
            this.MainSplitContainer.SplitterWidth = 2;
            this.MainSplitContainer.TabIndex = 0;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CopyrightLabel.Location = new System.Drawing.Point(20, 142);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(300, 12);
            this.CopyrightLabel.TabIndex = 100;
            this.CopyrightLabel.Text = "Copyright (c) 2010 CubeSoft, Inc.";
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLabel
            // 
            this.VersionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VersionLabel.Location = new System.Drawing.Point(20, 72);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(300, 12);
            this.VersionLabel.TabIndex = 100;
            this.VersionLabel.Text = "Version 1.0.0 (x86)";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitlePictureBox
            // 
            this.TitlePictureBox.Image = global::CubePdf.Properties.Resources.title;
            this.TitlePictureBox.Location = new System.Drawing.Point(147, 12);
            this.TitlePictureBox.Name = "TitlePictureBox";
            this.TitlePictureBox.Size = new System.Drawing.Size(98, 48);
            this.TitlePictureBox.TabIndex = 12;
            this.TitlePictureBox.TabStop = false;
            // 
            // CubePDFLinkLabel
            // 
            this.CubePDFLinkLabel.Location = new System.Drawing.Point(20, 160);
            this.CubePDFLinkLabel.Name = "CubePDFLinkLabel";
            this.CubePDFLinkLabel.Size = new System.Drawing.Size(300, 12);
            this.CubePDFLinkLabel.TabIndex = 2;
            this.CubePDFLinkLabel.TabStop = true;
            this.CubePDFLinkLabel.Text = "http://www.cube-soft.jp/cubepdf/";
            this.CubePDFLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CubePDFLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CubePdfLinkLabel_LinkClicked);
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Image = global::CubePdf.Properties.Resources.logo;
            this.LogoPictureBox.Location = new System.Drawing.Point(105, 12);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(32, 42);
            this.LogoPictureBox.TabIndex = 11;
            this.LogoPictureBox.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(125, 13);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(100, 25);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "OK";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // OSVersionLabel
            // 
            this.OSVersionLabel.Location = new System.Drawing.Point(20, 92);
            this.OSVersionLabel.Name = "OSVersionLabel";
            this.OSVersionLabel.Size = new System.Drawing.Size(300, 12);
            this.OSVersionLabel.TabIndex = 100;
            this.OSVersionLabel.Text = "Microsoft Windows NT 6.1.7601 Service Pack 1";
            this.OSVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DotNetVersionLabel
            // 
            this.DotNetVersionLabel.Location = new System.Drawing.Point(20, 110);
            this.DotNetVersionLabel.Name = "DotNetVersionLabel";
            this.DotNetVersionLabel.Size = new System.Drawing.Size(300, 12);
            this.DotNetVersionLabel.TabIndex = 100;
            this.DotNetVersionLabel.Text = ".NET Framework 4.0.30319.18408";
            this.DotNetVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 252);
            this.Controls.Add(this.MainSplitContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CubePDF について";
            this.Shown += new System.EventHandler(this.VersionDialog_Shown);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.LinkLabel CubePDFLinkLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox TitlePictureBox;
        private System.Windows.Forms.Label OSVersionLabel;
        private System.Windows.Forms.Label DotNetVersionLabel;


    }
}