namespace CubePDF {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.UpdateNotifier = new System.Windows.Forms.NotifyIcon(this.components);
            this.UpdateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateNotifier
            // 
            this.UpdateNotifier.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.UpdateNotifier.BalloonTipText = "アップデートの確認";
            this.UpdateNotifier.BalloonTipTitle = "アップデートの確認";
            this.UpdateNotifier.ContextMenuStrip = this.UpdateContextMenu;
            this.UpdateNotifier.Icon = ((System.Drawing.Icon)(resources.GetObject("UpdateNotifier.Icon")));
            this.UpdateNotifier.Text = "CubePDF";
            this.UpdateNotifier.Visible = true;
            this.UpdateNotifier.BalloonTipClicked += new System.EventHandler(this.UpdateMenu_Click);
            this.UpdateNotifier.DoubleClick += new System.EventHandler(this.UpdateMenu_Click);
            // 
            // UpdateContextMenu
            // 
            this.UpdateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateMenu,
            this.ExitMenu});
            this.UpdateContextMenu.Name = "contextMenuStrip1";
            this.UpdateContextMenu.Size = new System.Drawing.Size(158, 48);
            // 
            // UpdateMenu
            // 
            this.UpdateMenu.Name = "UpdateMenu";
            this.UpdateMenu.Size = new System.Drawing.Size(157, 22);
            this.UpdateMenu.Text = "アップデートの確認";
            this.UpdateMenu.Click += new System.EventHandler(this.UpdateMenu_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(157, 22);
            this.ExitMenu.Text = "終了";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "Form1";
            this.Text = "Form1";
            this.UpdateContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon UpdateNotifier;
        private System.Windows.Forms.ContextMenuStrip UpdateContextMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
    }
}

