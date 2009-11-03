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
			this.label_path = new System.Windows.Forms.Label();
			this.groupBox_osd = new System.Windows.Forms.GroupBox();
			this.numericUpDown_request = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_media = new System.Windows.Forms.NumericUpDown();
			this.label_request = new System.Windows.Forms.Label();
			this.label_next = new System.Windows.Forms.Label();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_save = new System.Windows.Forms.Button();
			this.textBox_path = new System.Windows.Forms.TextBox();
			this.checkBox_autostart = new System.Windows.Forms.CheckBox();
			this.groupBox_startup.SuspendLayout();
			this.groupBox_osd.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_request)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_media)).BeginInit();
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
			this.comboBox_mediaplayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_mediaplayer.FormattingEnabled = true;
			this.comboBox_mediaplayer.Location = new System.Drawing.Point(82, 19);
			this.comboBox_mediaplayer.Name = "comboBox_mediaplayer";
			this.comboBox_mediaplayer.Size = new System.Drawing.Size(299, 21);
			this.comboBox_mediaplayer.Sorted = true;
			this.comboBox_mediaplayer.TabIndex = 1;
			// 
			// groupBox_startup
			// 
			this.groupBox_startup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox_startup.Controls.Add(this.button_browse);
			this.groupBox_startup.Controls.Add(this.textBox_path);
			this.groupBox_startup.Controls.Add(this.comboBox_mediaplayer);
			this.groupBox_startup.Controls.Add(this.checkBox_autostart);
			this.groupBox_startup.Controls.Add(this.label_path);
			this.groupBox_startup.Controls.Add(this.label_mediaplayer);
			this.groupBox_startup.Location = new System.Drawing.Point(12, 12);
			this.groupBox_startup.Name = "groupBox_startup";
			this.groupBox_startup.Size = new System.Drawing.Size(387, 100);
			this.groupBox_startup.TabIndex = 4;
			this.groupBox_startup.TabStop = false;
			this.groupBox_startup.Text = "Startup";
			// 
			// button_browse
			// 
			this.button_browse.Location = new System.Drawing.Point(306, 47);
			this.button_browse.Name = "button_browse";
			this.button_browse.Size = new System.Drawing.Size(75, 23);
			this.button_browse.TabIndex = 4;
			this.button_browse.Text = "Browse...";
			this.button_browse.UseVisualStyleBackColor = true;
			this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
			// 
			// label_path
			// 
			this.label_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_path.AutoSize = true;
			this.label_path.Location = new System.Drawing.Point(6, 52);
			this.label_path.Name = "label_path";
			this.label_path.Size = new System.Drawing.Size(32, 13);
			this.label_path.TabIndex = 0;
			this.label_path.Text = "Path:";
			this.label_path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox_osd
			// 
			this.groupBox_osd.Controls.Add(this.numericUpDown_request);
			this.groupBox_osd.Controls.Add(this.numericUpDown_media);
			this.groupBox_osd.Controls.Add(this.label_request);
			this.groupBox_osd.Controls.Add(this.label_next);
			this.groupBox_osd.Location = new System.Drawing.Point(12, 119);
			this.groupBox_osd.Name = "groupBox_osd";
			this.groupBox_osd.Size = new System.Drawing.Size(387, 79);
			this.groupBox_osd.TabIndex = 5;
			this.groupBox_osd.TabStop = false;
			this.groupBox_osd.Text = "OSD";
			// 
			// numericUpDown_request
			// 
			this.numericUpDown_request.Location = new System.Drawing.Point(209, 45);
			this.numericUpDown_request.Name = "numericUpDown_request";
			this.numericUpDown_request.Size = new System.Drawing.Size(64, 20);
			this.numericUpDown_request.TabIndex = 0;
			// 
			// numericUpDown_media
			// 
			this.numericUpDown_media.Location = new System.Drawing.Point(216, 19);
			this.numericUpDown_media.Name = "numericUpDown_media";
			this.numericUpDown_media.Size = new System.Drawing.Size(64, 20);
			this.numericUpDown_media.TabIndex = 0;
			// 
			// label_request
			// 
			this.label_request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_request.AutoSize = true;
			this.label_request.Location = new System.Drawing.Point(6, 47);
			this.label_request.Name = "label_request";
			this.label_request.Size = new System.Drawing.Size(319, 13);
			this.label_request.TabIndex = 0;
			this.label_request.Text = "Requested media should be displayed for                         seconds.";
			this.label_request.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_next
			// 
			this.label_next.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.label_next.AutoSize = true;
			this.label_next.Location = new System.Drawing.Point(6, 21);
			this.label_next.Name = "label_next";
			this.label_next.Size = new System.Drawing.Size(326, 13);
			this.label_next.TabIndex = 0;
			this.label_next.Text = "Current/next media should be displayed for                         seconds.";
			this.label_next.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(318, 204);
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
			this.button_save.Location = new System.Drawing.Point(237, 204);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(75, 23);
			this.button_save.TabIndex = 6;
			this.button_save.Text = "Save";
			this.button_save.UseVisualStyleBackColor = true;
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// textBox_path
			// 
			this.textBox_path.Location = new System.Drawing.Point(44, 49);
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
			this.checkBox_autostart.Location = new System.Drawing.Point(313, 76);
			this.checkBox_autostart.Name = "checkBox_autostart";
			this.checkBox_autostart.Size = new System.Drawing.Size(68, 17);
			this.checkBox_autostart.TabIndex = 3;
			this.checkBox_autostart.Text = "Autostart";
			this.checkBox_autostart.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.button_save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(411, 236);
			this.ControlBox = false;
			this.Controls.Add(this.button_save);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.groupBox_osd);
			this.Controls.Add(this.groupBox_startup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SettingsForm";
			this.ShowInTaskbar = false;
			this.Text = System.Windows.Forms.Application.ProductName + " settings";
			this.groupBox_startup.ResumeLayout(false);
			this.groupBox_startup.PerformLayout();
			this.groupBox_osd.ResumeLayout(false);
			this.groupBox_osd.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_request)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_media)).EndInit();
			this.ResumeLayout(false);

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
	}
}