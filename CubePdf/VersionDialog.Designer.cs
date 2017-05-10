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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionDialog));
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.DotNetVersionLabel = new System.Windows.Forms.Label();
            this.OSVersionLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.CubePDFLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            resources.ApplyResources(this.MainSplitContainer, "MainSplitContainer");
            this.MainSplitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            resources.ApplyResources(this.MainSplitContainer.Panel1, "MainSplitContainer.Panel1");
            this.MainSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.MainSplitContainer.Panel1.Controls.Add(this.DotNetVersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.OSVersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.CopyrightLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.VersionLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.CubePDFLinkLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.LogoPictureBox);
            // 
            // MainSplitContainer.Panel2
            // 
            resources.ApplyResources(this.MainSplitContainer.Panel2, "MainSplitContainer.Panel2");
            this.MainSplitContainer.Panel2.Controls.Add(this.CloseButton);
            // 
            // DotNetVersionLabel
            // 
            resources.ApplyResources(this.DotNetVersionLabel, "DotNetVersionLabel");
            this.DotNetVersionLabel.Name = "DotNetVersionLabel";
            // 
            // OSVersionLabel
            // 
            resources.ApplyResources(this.OSVersionLabel, "OSVersionLabel");
            this.OSVersionLabel.Name = "OSVersionLabel";
            // 
            // CopyrightLabel
            // 
            resources.ApplyResources(this.CopyrightLabel, "CopyrightLabel");
            this.CopyrightLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CopyrightLabel.Name = "CopyrightLabel";
            // 
            // VersionLabel
            // 
            resources.ApplyResources(this.VersionLabel, "VersionLabel");
            this.VersionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VersionLabel.Name = "VersionLabel";
            // 
            // CubePDFLinkLabel
            // 
            resources.ApplyResources(this.CubePDFLinkLabel, "CubePDFLinkLabel");
            this.CubePDFLinkLabel.Name = "CubePDFLinkLabel";
            this.CubePDFLinkLabel.TabStop = true;
            this.CubePDFLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CubePdfLinkLabel_LinkClicked);
            // 
            // LogoPictureBox
            // 
            resources.ApplyResources(this.LogoPictureBox, "LogoPictureBox");
            this.LogoPictureBox.Image = global::CubePdf.Properties.Resources.Logo;
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.TabStop = false;
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // VersionDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.MainSplitContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.VersionDialog_Shown);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.ResumeLayout(false);
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
        private System.Windows.Forms.Label OSVersionLabel;
        private System.Windows.Forms.Label DotNetVersionLabel;


    }
}