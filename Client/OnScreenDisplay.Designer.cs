namespace Jukebox.NET.Client
{
	partial class OnScreenDisplay
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
				this.t_DisplayDown.Dispose();
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
			this.TextDisplay = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OSDtext
			// 
			this.TextDisplay.AutoSize = true;
			this.TextDisplay.Font = new System.Drawing.Font("Segoe UI", Properties.Settings.Default.FontSize, System.Drawing.FontStyle.Bold);
			this.TextDisplay.ForeColor = System.Drawing.Color.White;
			this.TextDisplay.Name = "OSDtext";
			this.TextDisplay.Padding = new System.Windows.Forms.Padding(5);
			this.TextDisplay.TabIndex = 0;
			// 
			// OSD
			// 
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(Monitor.Width, 0);
			this.ControlBox = false;
			this.Controls.Add(this.TextDisplay);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Location = new System.Drawing.Point(Monitor.BoundStart, 0);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OSD";
			this.Opacity = 0;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label TextDisplay;
	}
}
