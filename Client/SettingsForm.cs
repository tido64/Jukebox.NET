using System;
using System.Windows.Forms;

namespace Jukebox.NET.Client
{
	public partial class SettingsForm : Form
	{
		private OpenFileDialog openFileDialog = null;

		public SettingsForm()
		{
			InitializeComponent();

			this.comboBox_mediaplayer.Items.AddRange(MediaPlayer.MediaPlayerFactory.Players.ToArray());
			this.comboBox_mediaplayer.SelectedIndex = MediaPlayer.MediaPlayerFactory.Players.BinarySearch(Properties.Settings.Default.MediaPlayer);
			this.numericUpDown_media.Value = Properties.Settings.Default.MediaDisplayLifeSpan / 1000;
			this.numericUpDown_request.Value = Properties.Settings.Default.RequestDisplayLifeSpan / 1000;

			this.comboBox_mediaplayer.SelectedIndexChanged += new EventHandler(MediaPlayerChanged);
			this.numericUpDown_media.ValueChanged += new EventHandler(MediaDisplayLifeChanged);
			this.numericUpDown_request.ValueChanged += new EventHandler(RequestDisplayLifeChanged);
			this.textBox_path.TextChanged += new EventHandler(PathChanged);

			this.CenterToScreen();
		}

		#region Button handlers

		private void button_browse_Click(object sender, EventArgs e)
		{
			if (this.openFileDialog == null)
			{
				this.openFileDialog = new OpenFileDialog();
				this.openFileDialog.CheckFileExists = true;
				this.openFileDialog.CheckPathExists = true;
				this.openFileDialog.Filter = "Executable files (*.exe)|*.exe";
				this.openFileDialog.Multiselect = false;
			}
			this.openFileDialog.Title = "Select path for " + this.comboBox_mediaplayer.SelectedItem.ToString() + ":";
			if (this.openFileDialog.ShowDialog() == DialogResult.OK)
				this.textBox_path.Text = this.openFileDialog.FileName;
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.Save();
			Properties.Settings.Default.Reload();
			Dispose();
		}

		#endregion

		#region Change events

		private void MediaPlayerChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.MediaPlayer = this.comboBox_mediaplayer.SelectedItem.ToString();
		}

		private void MediaDisplayLifeChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.MediaDisplayLifeSpan = int.Parse(this.numericUpDown_media.Value.ToString()) * 1000;
		}

		private void PathChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.PathToExe = this.textBox_path.Text;
		}

		private void RequestDisplayLifeChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.RequestDisplayLifeSpan = int.Parse(this.numericUpDown_request.Value.ToString()) * 1000;
		}

		#endregion
	}
}
