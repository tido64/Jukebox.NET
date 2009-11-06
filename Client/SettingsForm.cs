using System;
using System.Windows.Forms;

namespace Jukebox.NET.Client
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();

			this.comboBox_mediaplayer.Items.AddRange(MediaPlayer.MediaPlayerFactory.Players.ToArray());
			this.comboBox_mediaplayer.SelectedIndex = MediaPlayer.MediaPlayerFactory.Players.BinarySearch(Properties.Settings.Default.MediaPlayer);
			this.numericUpDown_font.Value = (decimal)Properties.Settings.Default.FontSize;
			this.numericUpDown_interval.Value = Properties.Settings.Default.Interval / 1000;
			this.numericUpDown_media.Value = Properties.Settings.Default.TimeToDisplayMedia / 1000;
			this.numericUpDown_request.Value = Properties.Settings.Default.TimeToDisplayRequest / 1000;
		}

		#region Button handlers

		private void button_browse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.CheckFileExists = true;
				ofd.CheckPathExists = true;
				ofd.Filter = "Executable files (*.exe)|*.exe";
				ofd.Multiselect = false;
				ofd.Title = "Select path for " + this.comboBox_mediaplayer.SelectedItem.ToString() + ":";
				if (ofd.ShowDialog() == DialogResult.OK)
					this.textBox_path.Text = ofd.FileName;
			}
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.FontSize = (float)this.numericUpDown_font.Value;
			Properties.Settings.Default.Interval = (int)this.numericUpDown_interval.Value * 1000;
			Properties.Settings.Default.MediaPlayer = this.comboBox_mediaplayer.SelectedItem.ToString();
			Properties.Settings.Default.PathToExe = this.textBox_path.Text;
			Properties.Settings.Default.TimeToDisplayMedia = (int)this.numericUpDown_media.Value * 1000;
			Properties.Settings.Default.TimeToDisplayRequest = (int)this.numericUpDown_request.Value * 1000;
			Properties.Settings.Default.Save();
			this.Dispose();
		}

		#endregion
	}
}
