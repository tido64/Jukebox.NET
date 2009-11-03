namespace Jukebox.NET.Client
{
    partial class Console
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveOSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restartMediaPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusMediaPlayer = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBox = new System.Windows.Forms.TextBox();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip.Size = new System.Drawing.Size(314, 19);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(29, 19);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// controlToolStripMenuItem
			// 
			this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveOSDToolStripMenuItem,
            this.restartMediaPlayerToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem});
			this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
			this.controlToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
			this.controlToolStripMenuItem.Size = new System.Drawing.Size(51, 19);
			this.controlToolStripMenuItem.Text = "Control";
			// 
			// moveOSDToolStripMenuItem
			// 
			this.moveOSDToolStripMenuItem.Enabled = false;
			this.moveOSDToolStripMenuItem.Name = "moveOSDToolStripMenuItem";
			this.moveOSDToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.moveOSDToolStripMenuItem.Text = "Move OSD";
			this.moveOSDToolStripMenuItem.Click += new System.EventHandler(this.moveOSDToolStripMenuItem_Click);
			// 
			// restartMediaPlayerToolStripMenuItem
			// 
			this.restartMediaPlayerToolStripMenuItem.Name = "restartMediaPlayerToolStripMenuItem";
			this.restartMediaPlayerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.restartMediaPlayerToolStripMenuItem.Text = "Start media player";
			this.restartMediaPlayerToolStripMenuItem.Click += new System.EventHandler(this.restartMediaPlayerToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(36, 19);
			this.helpToolStripMenuItem.Text = "Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMediaPlayer});
			this.statusStrip.Location = new System.Drawing.Point(0, 193);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(314, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip";
			// 
			// statusMediaPlayer
			// 
			this.statusMediaPlayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusMediaPlayer.Name = "statusMediaPlayer";
			this.statusMediaPlayer.Size = new System.Drawing.Size(139, 17);
			this.statusMediaPlayer.Text = "No media player running";
			// 
			// textBox
			// 
			this.textBox.BackColor = System.Drawing.Color.White;
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox.CausesValidation = false;
			this.textBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
			this.textBox.Location = new System.Drawing.Point(0, 20);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox.ShortcutsEnabled = false;
			this.textBox.Size = new System.Drawing.Size(314, 170);
			this.textBox.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 215);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Jukebox.NET";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartMediaPlayerToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusMediaPlayer;
		private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveOSDToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
