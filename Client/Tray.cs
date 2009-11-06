using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Jukebox.NET.Client.MediaPlayer;
using Jukebox.NET.Common;

namespace Jukebox.NET.Client
{
	public delegate void TrackChange(Media m);

	/// <summary>
	/// Jukebox.NET tray application and program flow.
	/// </summary>
	/// <remarks>
	/// TODO:
	/// - When playlist is exhausted, randomly add new songs to play until input
	/// - Show the next two or three
	/// - Show requester (log in candy)
	///
	/// Lower priority:
	/// - CD+G on max two lines, using pictures as background
	/// </remarks>
	class Tray : IDisposable
	{
		private const string startMediaPlayer = "Start media player";
		private AbstractMediaPlayer mediaPlayer = null;
		private ContextMenu trayMenu;
		private NotifyIcon trayIcon;
		private OnScreenDisplay osd = null;

		public Tray()
		{
			#region Initialize components

			ComponentResourceManager resources = new ComponentResourceManager(typeof(Properties.Resources));

			this.trayMenu = new ContextMenu();
			this.trayMenu.MenuItems.Add(startMediaPlayer, Restart);
			this.trayMenu.MenuItems.Add("Settings", ChangeSettings);
			this.trayMenu.MenuItems.Add("-");
			this.trayMenu.MenuItems.Add("Exit", Exit);

			this.trayIcon = new NotifyIcon();
			this.trayIcon.Text = Application.ProductName + " v" + Application.ProductVersion;
			this.trayIcon.Icon = ((Icon)(resources.GetObject("stock_music_library")));
			this.trayIcon.ContextMenu = this.trayMenu;
			this.trayIcon.Visible = true;

			#endregion

			if (Properties.Settings.Default.Autostart)
				Restart(this, null);
		}

		private void ChangeSettings(object sender, EventArgs e)
		{
			SettingsForm sf = new SettingsForm();
			sf.Visible = true;
		}

		private void Exit(object sender, EventArgs e)
		{
			this.Dispose();
			Application.Exit();
		}

		private void Restart(object sender, EventArgs e)
		{
			this.trayMenu.MenuItems[0].Enabled = false;
			this.trayMenu.MenuItems[0].Text = "Starting " + Properties.Settings.Default.MediaPlayer + "...";

			if (this.mediaPlayer == null)
				Start();
			else
				this.mediaPlayer.Restart();

			if (mediaPlayer == null)
				this.trayMenu.MenuItems[0].Text = startMediaPlayer;
			else
				this.trayMenu.MenuItems[0].Text = "Restart " + this.mediaPlayer.Name;
			this.trayMenu.MenuItems[0].Enabled = true;
		}

		private void Start()
		{
			// Start media player
			try
			{
				this.mediaPlayer = MediaPlayerFactory.Create();
			}
			catch (Exception e)
			{
				MessageBox.Show(Properties.Settings.Default.MediaPlayer
					+ " could not be started. Please verify your configuration and try again." + Program.endl + Program.endl + "Exception: " + e.ToString(),
					"Failed to start media player",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}
			if (this.mediaPlayer == null)
				return;
			if (this.osd == null)
				this.osd = new OnScreenDisplay(this.mediaPlayer);
			DatabaseManager.Instance.Load();
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (this.mediaPlayer != null)
				this.mediaPlayer.Shutdown();
			if (this.osd != null)
				this.osd.Dispose();
			this.trayIcon.Dispose();
		}

		#endregion
	}
}
