namespace Jukebox.NET.Client
{
	partial class SettingsForm
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
			this.label_mediaplayer = new System.Windows.Forms.Label();
			this.comboBox_mediaplayer = new System.Windows.Forms.ComboBox();
			this.groupBox_startup = new System.Windows.Forms.GroupBox();
			this.button_browse = new System.Windows.Forms.Button();
			this.textBox_path = new System.Windows.Forms.TextBox();
			this.checkBox_autostart = new System.Windows.Forms.CheckBox();
			this.label_path = new System.Windows.Forms.Label();
			this.groupBox_osd = new System.Windows.Forms.GroupBox();
			this.numericUpDown_font = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_interval = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_media = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_request = new System.Windows.Forms.NumericUpDown();
			this.label_font = new System.Windows.Forms.Label();
			this.label_interval = new System.Windows.Forms.Label();
			this.label_next = new System.Windows.Forms.Label();
			this.label_request = new System.Windows.Forms.Label();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_save = new System.Windows.Forms.Button();
			this.flowLayoutPanel_control = new System.Windows.Forms.FlowLayoutPanel();
			this.label_drive = new System.Windows.Forms.Label();
			this.comboBox_drive = new System.Windows.Forms.ComboBox();
			this.groupBox_startup.SuspendLayout();
			this.groupBox_osd.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_font)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_interval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_media)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_request)).BeginInit();
			this.flowLayoutPanel_control.SuspendLayout();
			this.SuspendLayout();
			// 
			// label_mediaplayer
			// 
			this.label_mediaplayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_mediaplayer.AutoSize = true;
			this.label_mediaplayer.Location = new System.Drawing.Point(6, 22);
			this.label_mediaplayer.Name = "label_mediaplayer";
			this.label_mediaplayer.Size = new System.Drawing.Size(70, 13);
			this.label_mediaplayer.TabIndex = 0;
			this.label_mediaplayer.Text = "Media player:";
			this.label_mediaplayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox_mediaplayer
			// 
			this.comboBox_mediaplayer.FormattingEnabled = true;
			this.comboBox_mediaplayer.Location = new System.Drawing.Point(82, 19);
			this.comboBox_mediaplayer.Name = "comboBox_mediaplayer";
			this.comboBox_mediaplayer.Size = new System.Drawing.Size(225, 21);
			this.comboBox_mediaplayer.Sorted = true;
			this.comboBox_mediaplayer.TabIndex = 1;
			// 
			// groupBox_startup
			// 
			this.groupBox_startup.Controls.Add(this.button_browse);
			this.groupBox_startup.Controls.Add(this.textBox_path);
			this.groupBox_startup.Controls.Add(this.comboBox_mediaplayer);
			this.groupBox_startup.Controls.Add(this.checkBox_autostart);
			this.groupBox_startup.Controls.Add(this.label_path);
			this.groupBox_startup.Controls.Add(this.label_mediaplayer);
			this.groupBox_startup.Location = new System.Drawing.Point(5, 5);
			this.groupBox_startup.Name = "groupBox_startup";
			this.groupBox_startup.Size = new System.Drawing.Size(387, 74);
			this.groupBox_startup.TabIndex = 4;
			this.groupBox_startup.TabStop = false;
			this.groupBox_startup.Text = "Startup";
			// 
			// button_browse
			// 
			this.button_browse.Location = new System.Drawing.Point(306, 43);
			this.button_browse.Name = "button_browse";
			this.button_browse.Size = new System.Drawing.Size(75, 23);
			this.button_browse.TabIndex = 4;
			this.button_browse.Text = "Browse...";
			this.button_browse.UseVisualStyleBackColor = true;
			this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
			// 
			// textBox_path
			// 
			this.textBox_path.Location = new System.Drawing.Point(44, 45);
			this.textBox_path.Name = "textBox_path";
			this.textBox_path.ReadOnly = true;
			this.textBox_path.Size = new System.Drawing.Size(256, 20);
			this.textBox_path.TabIndex = 2;
			this.textBox_path.Text = global::Jukebox.NET.Client.Properties.Settings.Default.PathToExe;
			// 
			// checkBox_autostart
			// 
			this.checkBox_autostart.AutoSize = true;
			this.checkBox_autostart.Checked = global::Jukebox.NET.Client.Properties.Settings.Default.Autostart;
			this.checkBox_autostart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_autostart.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Jukebox.NET.Client.Properties.Settings.Default, "Autostart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox_autostart.Location = new System.Drawing.Point(313, 21);
			this.checkBox_autostart.Name = "checkBox_autostart";
			this.checkBox_autostart.Size = new System.Drawing.Size(68, 17);
			this.checkBox_autostart.TabIndex = 3;
			this.checkBox_autostart.Text = "Autostart";
			this.checkBox_autostart.UseVisualStyleBackColor = true;
			// 
			// label_path
			// 
			this.label_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_path.AutoSize = true;
			this.label_path.Location = new System.Drawing.Point(6, 48);
			this.label_path.Name = "label_path";
			this.label_path.Size = new System.Drawing.Size(32, 13);
			this.label_path.TabIndex = 0;
			this.label_path.Text = "Path:";
			this.label_path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox_osd
			// 
			this.groupBox_osd.Controls.Add(this.numericUpDown_font);
			this.groupBox_osd.Controls.Add(this.numericUpDown_interval);
			this.groupBox_osd.Controls.Add(this.numericUpDown_media);
			this.groupBox_osd.Controls.Add(this.numericUpDown_request);
			this.groupBox_osd.Controls.Add(this.label_font);
			this.groupBox_osd.Controls.Add(this.label_interval);
			this.groupBox_osd.Controls.Add(this.label_next);
			this.groupBox_osd.Controls.Add(this.label_request);
			this.groupBox_osd.Location = new System.Drawing.Point(5, 85);
			this.groupBox_osd.Name = "groupBox_osd";
			this.groupBox_osd.Size = new System.Drawing.Size(387, 86);
			this.groupBox_osd.TabIndex = 5;
			this.groupBox_osd.TabStop = false;
			this.groupBox_osd.Text = "OSD";
			// 
			// numericUpDown_font
			// 
			this.numericUpDown_font.Location = new System.Drawing.Point(333, 14);
			this.numericUpDown_font.Name = "numericUpDown_font";
			this.numericUpDown_font.Size = new System.Drawing.Size(48, 20);
			this.numericUpDown_font.TabIndex = 0;
			// 
			// numericUpDown_interval
			// 
			this.numericUpDown_interval.Location = new System.Drawing.Point(189, 14);
			this.numericUpDown_interval.Name = "numericUpDown_interval";
			this.numericUpDown_interval.Size = new System.Drawing.Size(48, 20);
			this.numericUpDown_interval.TabIndex = 0;
			// 
			// numericUpDown_media
			// 
			this.numericUpDown_media.Location = new System.Drawing.Point(196, 35);
			this.numericUpDown_media.Name = "numericUpDown_media";
			this.numericUpDown_media.Size = new System.Drawing.Size(48, 20);
			this.numericUpDown_media.TabIndex = 0;
			// 
			// numericUpDown_request
			// 
			this.numericUpDown_request.Location = new System.Drawing.Point(185, 56);
			this.numericUpDown_request.Name = "numericUpDown_request";
			this.numericUpDown_request.Size = new System.Drawing.Size(48, 20);
			this.numericUpDown_request.TabIndex = 0;
			// 
			// label_font
			// 
			this.label_font.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_font.AutoSize = true;
			this.label_font.Location = new System.Drawing.Point(275, 16);
			this.label_font.Name = "label_font";
			this.label_font.Size = new System.Drawing.Size(52, 13);
			this.label_font.TabIndex = 0;
			this.label_font.Text = "Font size:";
			this.label_font.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_interval
			// 
			this.label_interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_interval.AutoSize = true;
			this.label_interval.Location = new System.Drawing.Point(6, 16);
			this.label_interval.Name = "label_interval";
			this.label_interval.Size = new System.Drawing.Size(177, 13);
			this.label_interval.TabIndex = 0;
			this.label_interval.Text = "Seconds between each notification:";
			this.label_interval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_next
			// 
			this.label_next.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_next.AutoSize = true;
			this.label_next.Location = new System.Drawing.Point(6, 37);
			this.label_next.Name = "label_next";
			this.label_next.Size = new System.Drawing.Size(184, 13);
			this.label_next.TabIndex = 0;
			this.label_next.Text = "Seconds to show current/next media:";
			this.label_next.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_request
			// 
			this.label_request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_request.AutoSize = true;
			this.label_request.Location = new System.Drawing.Point(6, 58);
			this.label_request.Name = "label_request";
			this.label_request.Size = new System.Drawing.Size(173, 13);
			this.label_request.TabIndex = 0;
			this.label_request.Text = "Seconds to show requested media:";
			this.label_request.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(84, 3);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 6;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_save
			// 
			this.button_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_save.Location = new System.Drawing.Point(3, 3);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(75, 23);
			this.button_save.TabIndex = 6;
			this.button_save.Text = "Save";
			this.button_save.UseVisualStyleBackColor = true;
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// flowLayoutPanel_control
			// 
			this.flowLayoutPanel_control.AutoSize = true;
			this.flowLayoutPanel_control.Controls.Add(this.button_save);
			this.flowLayoutPanel_control.Controls.Add(this.button_cancel);
			this.flowLayoutPanel_control.Location = new System.Drawing.Point(230, 177);
			this.flowLayoutPanel_control.Name = "flowLayoutPanel_control";
			this.flowLayoutPanel_control.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanel_control.TabIndex = 6;
			// 
			// label_drive
			// 
			this.label_drive.AutoSize = true;
			this.label_drive.Location = new System.Drawing.Point(11, 185);
			this.label_drive.Name = "label_drive";
			this.label_drive.Size = new System.Drawing.Size(35, 13);
			this.label_drive.TabIndex = 7;
			this.label_drive.Text = "Drive:";
			// 
			// comboBox_drive
			// 
			this.comboBox_drive.FormattingEnabled = true;
			this.comboBox_drive.Items.AddRange(new object[] {
            "Default"});
			this.comboBox_drive.Location = new System.Drawing.Point(52, 182);
			this.comboBox_drive.Name = "comboBox_drive";
			this.comboBox_drive.Size = new System.Drawing.Size(64, 21);
			this.comboBox_drive.TabIndex = 8;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.button_save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(397, 214);
			this.ControlBox = false;
			this.Controls.Add(this.comboBox_drive);
			this.Controls.Add(this.label_drive);
			this.Controls.Add(this.flowLayoutPanel_control);
			this.Controls.Add(this.groupBox_osd);
			this.Controls.Add(this.groupBox_startup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SettingsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Jukebox.NET settings";
			this.groupBox_startup.ResumeLayout(false);
			this.groupBox_startup.PerformLayout();
			this.groupBox_osd.ResumeLayout(false);
			this.groupBox_osd.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_font)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_interval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_media)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_request)).EndInit();
			this.flowLayoutPanel_control.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_mediaplayer;
		private System.Windows.Forms.ComboBox comboBox_mediaplayer;
		private System.Windows.Forms.TextBox textBox_path;
		private System.Windows.Forms.CheckBox checkBox_autostart;
		private System.Windows.Forms.GroupBox groupBox_startup;
		private System.Windows.Forms.Button button_browse;
		private System.Windows.Forms.GroupBox groupBox_osd;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.NumericUpDown numericUpDown_media;
		private System.Windows.Forms.Label label_next;
		private System.Windows.Forms.NumericUpDown numericUpDown_request;
		private System.Windows.Forms.Label label_request;
		private System.Windows.Forms.Label label_path;
		private System.Windows.Forms.NumericUpDown numericUpDown_interval;
		private System.Windows.Forms.Label label_font;
		private System.Windows.Forms.Label label_interval;
		private System.Windows.Forms.NumericUpDown numericUpDown_font;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_control;
		private System.Windows.Forms.Label label_drive;
		private System.Windows.Forms.ComboBox comboBox_drive;
	}
}