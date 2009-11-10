using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client.MediaPlayer
{
	/// <summary>
	/// MPlayer wrapper. Makes use of MPlayer's slave mode.
	/// 
	/// http://www.mplayerhq.hu/DOCS/man/en/mplayer.1.html
	/// http://www.mplayerhq.hu/DOCS/tech/slave.txt
	/// </summary>
	class MPlayer : AbstractMediaPlayer
	{
		#region Code for handling that persistent MPlayer console

		private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
		private const UInt32 SWP_NOSIZE = 0x0001;
		private const UInt32 SWP_NOMOVE = 0x0002;
		private const UInt32 SWP_HIDEWINDOW = 0x0080;
		private const UInt32 SWP_ASSASSINATE = SWP_NOSIZE | SWP_NOMOVE | SWP_HIDEWINDOW;

		[DllImport("user32")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

		#endregion

		private readonly string arguments;
		private bool filling, paused, playing;
		private int playlist_ptr;
		private EventHandler mp_event;
		private Form mp_window;
		private List<Media> playlist;
		private Panel mp_panel;
		private Process mplayer;
		private Random random;


		public MPlayer() : base("MPlayer", "mplayer")
		{
			#region Initialize window

			this.mp_panel = new Panel();
			this.mp_panel.Dock = DockStyle.Fill;
			this.mp_window = new Form();
			this.mp_window.AccessibleRole = AccessibleRole.None;
			this.mp_window.AutoValidate = AutoValidate.Disable;
			this.mp_window.BackColor = Color.Black;
			this.mp_window.BackgroundImageLayout = ImageLayout.None;
			this.mp_window.CausesValidation = false;
			this.mp_window.ClientSize = new Size(Monitor.Width, Monitor.Height);
			this.mp_window.ControlBox = false;
			this.mp_window.FormBorderStyle = FormBorderStyle.None;
			this.mp_window.Location = new Point(Monitor.BoundStart, 0);
			this.mp_window.Name = "Jukebox.NET > MPlayer";
			this.mp_window.ShowIcon = false;
			this.mp_window.ShowInTaskbar = false;
			this.mp_window.TopMost = false;
			this.mp_window.Controls.Add(this.mp_panel);
			this.mp_window.ResumeLayout(false);
			this.mp_window.StartPosition = FormStartPosition.Manual;
			this.mp_window.Show();

			#endregion

			this.mp_event = new EventHandler(mplayer_Exited);
			this.mplayer = new Process();
			this.mplayer.EnableRaisingEvents = true;
			this.mplayer.Exited += this.mp_event;
			this.mplayer.StartInfo.CreateNoWindow = false;
			this.mplayer.StartInfo.FileName = Properties.Settings.Default.PathToExe;
			this.mplayer.StartInfo.RedirectStandardInput = true;
			this.mplayer.StartInfo.UseShellExecute = false;
			this.mplayer.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

			this.arguments = "-really-quiet -input nodefault-bindings -nojoystick -nolirc -nomouseinput -slave -wid " + this.mp_panel.Handle + " -noborder -vo direct3d";
			this.playlist = new List<Media>();
		}

		private void mplayer_Exited(object sender, EventArgs e)
		{
			this.playing = false;
			this.playlist_ptr++;
			if (this.playlist_ptr == this.playlist.Count) // randomly add new media to play
			{
				if (this.random == null)
					this.random = new Random();
				int id = this.random.Next(DatabaseManager.StartId, DatabaseManager.IdOffset + DatabaseManager.Instance.DataSet.Tables[0].Rows.Count);
				this.playlist.Add(DatabaseManager.Instance.Find(id));
			}
			this.Play();
		}

		private void Command(string cmd)
		{
			if (!this.playing)
				return;
			this.mplayer.StandardInput.Write(cmd + "\n");
		}

		private void Play()
		{
			string audio = string.Empty;
			string media = this.playlist[this.playlist_ptr].Path.ToLower();
			if (media.Contains(".ogm") && media.Contains("track 1"))
				audio = " -aid 1";

			this.mplayer.StartInfo.Arguments = this.arguments + audio + " \"" + this.playlist[this.playlist_ptr].Path + "\"";
			this.mplayer.Start();
			do
			{
				this.mplayer.Refresh();
			} while (this.mplayer.MainWindowHandle == IntPtr.Zero);
			SetWindowPos(this.mplayer.MainWindowHandle, HWND_BOTTOM, 0, 0, 0, 0, SWP_ASSASSINATE);
			SetForegroundWindow(this.mp_window.Handle);

			this.playing = true;
			TrackChange(this.CurrentlyPlaying);

			if (media.Contains("track 1"))
				this.CycleAudioTracks();
			//if (this.CurrentlyPlaying.RequestedBy != null)
			//{
			//    this.Command("osd 1");
			//    this.Command("osd_show_text \"Performed by: " + this.CurrentlyPlaying.RequestedBy + "\" 600000 1");
			//}
		}

		#region AbstractMediaPlayer members

		public override event TrackChange TrackChange;

		public override Media CurrentlyPlaying
		{
			get
			{
				if (!this.playing)
					return null;
				return this.playlist[this.playlist_ptr];
			}
		}

		public override bool IsPaused
		{
			get { return this.paused; }
		}

		public override Media NextTrack
		{
			get
			{
				if (this.playlist_ptr + 1 >= this.playlist.Count)
					return null;
				return this.playlist[this.playlist_ptr + 1];
			}
		}

		public override void Add(Media media)
		{
			this.playlist.Add(media);
			if (!this.playing)
				this.Play();
		}

		public override List<Media> GetNextTracks(int tracks)
		{
			List<Media> t = new List<Media>();
			for (int i = this.playlist_ptr + 1; i < Math.Min(this.playlist_ptr + tracks, this.playlist.Count); i++)
				t.Add(this.playlist[i]);
			return t;
		}

		public override void CycleAudioTracks()
		{
			this.Command("switch_audio");
		}

		public override void Next()
		{
			if (this.playlist_ptr + 1 == this.playlist.Count)
				return;
			this.Command("quit");
		}

		public override void Pause()
		{
			this.Command("pause");
			this.paused ^= true;
		}

		public override void Previous()
		{
			if (this.playlist_ptr - 1 < 0)
				return;
			this.playlist_ptr -= 2;
			this.Command("quit");
		}

		public override void Random() { }

		public override void Restart()
		{
			this.mplayer.Exited -= this.mp_event;
			this.mplayer.Kill();
			this.mplayer.WaitForExit();
			this.mplayer.Exited += this.mp_event;
			this.playlist_ptr = 0;
			this.playlist.Clear();
			this.playing = false;
		}

		public override void Shutdown()
		{
			if (this.playing)
			{
				this.mplayer.Exited -= this.mp_event;
				this.Command("quit");
				this.mplayer.WaitForExit();
			}
			this.mplayer.Close();
		}

		public override void VIPAdd(Media media)
		{
			if (!this.playing)
				this.Add(media);
			else
				this.playlist.Insert(this.playlist_ptr + 1, media);
		}

		public override void VolumeUp() { }
		public override void VolumeDown() { }

		#endregion
	}
}
