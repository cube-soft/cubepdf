namespace CubePdfUtility
{
    partial class DummyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DummyForm));
            this.UpdateNotifyIcon = new System.Windows.Forms.NotifyIcon();
            this.UpdateContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.ConfirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateNotifyIcon
            // 
            this.UpdateNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.UpdateNotifyIcon.BalloonTipText = "アップデートの確認";
            this.UpdateNotifyIcon.BalloonTipTitle = "アップデートの確認";
            this.UpdateNotifyIcon.ContextMenuStrip = this.UpdateContextMenuStrip;
            this.UpdateNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("UpdateNotifyIcon.Icon")));
            this.UpdateNotifyIcon.Text = "CubePDF";
            this.UpdateNotifyIcon.Visible = true;
            this.UpdateNotifyIcon.BalloonTipClicked += new System.EventHandler(this.Run);
            this.UpdateNotifyIcon.DoubleClick += new System.EventHandler(this.Run);
            // 
            // UpdateContextMenuStrip
            // 
            this.UpdateContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfirmToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.UpdateContextMenuStrip.Name = "UpdateContextMenuStrip";
            this.UpdateContextMenuStrip.Size = new System.Drawing.Size(160, 48);
            // 
            // ConfirmToolStripMenuItem
            // 
            this.ConfirmToolStripMenuItem.Name = "ConfirmToolStripMenuItem";
            this.ConfirmToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ConfirmToolStripMenuItem.Text = "アップデートの確認";
            this.ConfirmToolStripMenuItem.Click += new System.EventHandler(this.Run);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ExitToolStripMenuItem.Text = "終了";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // DummyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(128)));
            this.Name = "DummyForm";
            this.Text = "Form1";
            this.UpdateContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon UpdateNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip UpdateContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ConfirmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
    }
}

