using System;
using System.Threading;
using System.Windows.Forms;

using Jukebox.NET.Client.MediaPlayer;
using Jukebox.NET.Common;

namespace Jukebox.NET.Client
{
	public delegate void NextUpEvent();

	/// <summary>
	/// Jukebox.NET main form and program flow.
	/// </summary>
	/// <remarks>
	/// TODO:
	/// - 1 = 1000
	/// - When playlist is exhausted, randomly add new songs to play until input
	/// - VIP button
	///
	/// Lower priority:
	/// - Dual-monitor support
	/// - OSD mover
	/// - CD+G on max two lines, using pictures as background
	/// </remarks>
	public sealed partial class Console : Form
	{
		public const string endl = "\r\n";
		private AbstractMediaPlayer mediaPlayer = null;
		private OSD osd = null;
		private int choice = -1, input = 0;
		private System.Threading.Timer displayTimer = null;

		public Console()
		{
			Application.ApplicationExit += new EventHandler(OnApplicationExit);
			InitializeComponent();
			this.textBox.Text = this.Text + " v" + Application.ProductVersion + endl
				;// +Application.Copyright + endl + endl;
			if (Properties.Settings.Default.Autostart)
				InitializeExternalComponents();
		}

		#region Private methods

		/// <summary>
		/// Loads the database, and initializes the media player and on-screen display.
		/// </summary>
		private void InitializeExternalComponents()
		{
			CheckForIllegalCrossThreadCalls = false;

			// Load database
			PrintToConsole("[!] Loading database\t\t");
			DatabaseManager.Instance.Load();
			PrintToConsole("[ok]" + endl);

			// Start media player
			PrintToConsole("[!] Starting media player\t\t");
			try
			{
				this.mediaPlayer = MediaPlayerFactory.Create();
			}
			catch (Exception e)
			{
				MessageBox.Show("Please verify your configuration." + endl
					+ "This application will now exit." + endl + endl
					+ "Exception: " + e.ToString(),
					"Failed to start media player",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				Application.Exit();
				return;
			}
			this.mediaPlayer.NextUpEvent += new NextUpEvent(OnNextUp);
			this.statusMediaPlayer.Text = this.mediaPlayer.Name + " is running";
			this.restartMediaPlayerToolStripMenuItem.Text = "Restart " + this.mediaPlayer.Name;
			this.restartMediaPlayerToolStripMenuItem.Enabled = true;
			PrintToConsole("[ok]" + endl);

			osd = new OSD();
			this.moveOSDToolStripMenuItem.Enabled = true;

			this.osd.KeyPress += new KeyPressEventHandler(KeyPressHandler);
			this.textBox.KeyPress += new KeyPressEventHandler(KeyPressHandler);
		}

		/// <summary>
		/// Key-press handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyPressHandler(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar))
			{
				switch ((Keys)e.KeyChar)
				{
					case Keys.Enter:
						if (choice > 0)
						{
							try
							{
								Media media = DatabaseManager.Instance.FetchById(this.choice);
								this.mediaPlayer.Add(media);
								PrintToConsole("[*] Added " + media.ToString() + endl);
								this.osd.ShowRequest(media.ToString());
							}
							catch
							{
								this.osd.ShowRequest("??");
							}
							this.choice = 0;
						}
						break;
					case Keys.Space:
						this.mediaPlayer.Pause();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Play/pause" + endl);
						break;
					case Keys.A: // cycle audio tracks (#)
						this.mediaPlayer.CycleAudioTracks();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Cycle audio tracks" + endl);
						break;
					case Keys.P:
						osd.Down(null);
						this.mediaPlayer.Previous();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Previous" + endl);
						break;
					case Keys.N:
						osd.Down(null);
						this.mediaPlayer.Next();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Next" + endl);
						break;
					case Keys.Up:
						this.mediaPlayer.VolumeUp();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Volume up" + endl);
						break;
					case Keys.Down:
						this.mediaPlayer.VolumeDown();
						PrintToConsole("[*] " + this.mediaPlayer.Name + " > Volume down" + endl);
						break;
					case Keys.C:
						this.osd.ShowCurrent(this.mediaPlayer);
						break;
					default:
						PrintToConsole("[?] Key: " + e.KeyChar.ToString() + endl);
						break;
				}
			}
			else
			{
				// User input differs from the actual internal input
				if (this.input * 10 > int.MaxValue / 10)
					this.input /= 10;
				this.input = this.input * 10 + int.Parse(e.KeyChar.ToString());
				this.choice = this.input;
				int offset = 0;
				while (this.choice * Math.Pow(10, offset) <= DatabaseManager.IdOffset)
					offset++;
				this.osd.ShowRequest(this.choice, offset);
			}
		}

		/// <summary>
		/// Closes the media player on application exit.
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="args">Not used</param>
		private void OnApplicationExit(object sender, EventArgs args)
		{
			if (this.mediaPlayer != null)
				this.mediaPlayer.Shutdown();
		}

		/// <summary>
		/// Displays the currently playing title and schedules the display of next title.
		/// </summary>
		private void OnNextUp()
		{
			this.osd.ShowCurrent(this.mediaPlayer);
			try
			{
				if (this.displayTimer != null)
				{
					this.displayTimer.Change(Timeout.Infinite, Timeout.Infinite);
					this.displayTimer.Dispose();
					this.displayTimer = null;
				}
				this.displayTimer = new System.Threading.Timer(new TimerCallback(this.osd.ShowNext),
					this.mediaPlayer,
					90000,
					Timeout.Infinite);
			}
			catch { }
		}

		/// <summary>
		/// Prints given string to debug console.
		/// </summary>
		/// <param name="i">Debug text</param>
		private void PrintToConsole(string i)
		{
			this.textBox.Text += i;
			this.textBox.SelectionStart = textBox.Text.Length;
			this.textBox.ScrollToCaret();
		}

		#endregion

		#region Tool strip menu handlers

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult reply = MessageBox.Show("Are you sure you want to quit?",
				"Exit " + Application.ProductName + "?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (reply == DialogResult.Yes)
				Application.Exit();
		}

		private void restartMediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mediaPlayer == null)
			{
				InitializeExternalComponents();
				this.statusMediaPlayer.Text = this.mediaPlayer.Name + " is running";
			}
			else
			{
				PrintToConsole("[!] Restarting " + this.mediaPlayer.Name + "\t\t");
				this.statusMediaPlayer.Text = this.mediaPlayer.Name + " is restarting";
				this.mediaPlayer.Restart();
				PrintToConsole("[ok]" + endl);
				this.statusMediaPlayer.Text = this.mediaPlayer.Name + " is running";
			}
		}

		private void moveOSDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (osd != null)
			{
				PrintToConsole("[!] Moving OSD\t\t\t\t");
				// find screen dimensions (detect secondary display possible?)
				// move OSD
				MessageBox.Show("Primary monitor size: " + SystemInformation.PrimaryMonitorSize
					+ "\r\nVirtual screen: " + SystemInformation.VirtualScreen);
				PrintToConsole("[ok]" + endl);
			}

			// for testing purposes only
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Coming soon ;)");
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("This is v1.0 (sneak-preview)");
		}

		#endregion
	}
}
