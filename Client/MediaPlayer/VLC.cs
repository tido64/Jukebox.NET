using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client.MediaPlayer
{
	/// <summary>
	/// An interface for VLC media player 1.0.x using the RC interface.
	/// </summary>
	/// <remarks>
	/// Documentation:
	/// http://wiki.videolan.org/VLC_command-line_help
	/// http://www.videolan.org/doc/play-howto/en/ch04.html
	/// http://www.videolan.org/doc/play-howto/en/apb.html
	/// http://www.videolan.org/doc/vlc-user-guide/en/ch05.html
	/// http://n0tablog.wordpress.com/2009/02/09/controlling-vlc-via-rc-remote-control-interface-using-a-unix-domain-socket-and-no-programming/
	/// </remarks>
	sealed class VLC : AbstractMediaPlayer
	{
		#region Private members

		// Settings
		private const int
			buffersize = 1024,
			monitorInterval = 2000,
			port = 1337,
			tcpWait = 100;
		private const string host = "localhost";

		private readonly char[] whitespace = new char[] { '\0', '\r', '\n' };

		private bool paused = false, randomMode = false;
		private int playlistPtr = 0;
		private List<Media> playlist, vipPlaylist;
		private NetworkStream vlcRC;
		private Process vlc;
		private TcpClient vlcClient;
		private Timer timer;
		private UTF8Encoding utf8;

		#endregion

		public VLC()
			: base("VLC media player", "vlc")
		{
			this.utf8 = new UTF8Encoding();
			this.vlc = new Process();
			this.vlc.StartInfo.Arguments
				= "--swscale-mode=9 "
				+ "--sout-transcode-afilter=normvol "
				+ "--rc-quiet "
				+ "--rc-host=localhost:" + port + " "
				+ "--no-qt-privacy-ask "
				+ "--hotkeys-mousewheel-mode=2 "
				+ "--deinterlace-mode=bob "
				+ "--fullscreen "
				+ "--no-embedded-video "
				+ "--quiet-synchro "
				+ "--no-video-title-show "
				+ "--mouse-hide-timeout=1 "
				+ "--no-video-deco "
				//+ "--video-filter=deinterlacing "
				+ "--no-spu "
				+ "--no-osd "
				//+ "--audio-track=" + audiotrack + " "
				+ "--one-instance "
				+ "--quiet "
				+ "--no-show-intf "
				+ "--no-stats "
				+ "--intf=rc "
				+ "--key-toggle-fullscreen= "
				+ "--key-leave-fullscreen= "
				+ "--key-play-pause= "
				+ "--key-faster= "
				+ "--key-slower= "
				+ "--key-rate-normal= "
				+ "--key-rate-faster-fine= "
				+ "--key-rate-slower-fine= "
				+ "--key-next= "
				+ "--key-prev= "
				+ "--key-stop= "
				+ "--key-position= "
				+ "--key-jump-extrashort= "
				+ "--key-jump+extrashort= "
				+ "--key-jump-short= "
				+ "--key-jump+short= "
				+ "--key-jump-medium= "
				+ "--key-jump+medium= "
				+ "--key-jump-long= "
				+ "--key-jump+long= "
				+ "--key-frame-next= "
				//+ "--key-quit= "
				+ "--key-vol-up= "
				+ "--key-vol-down= "
				+ "--key-vol-mute= "
				+ "--key-subdelay-up= "
				+ "--key-subdelay-down= "
				+ "--key-audiodelay-up= "
				+ "--key-audiodelay-down= "
				+ "--key-audio-track= "
				+ "--key-audiodevice-cycle= "
				+ "--key-subtitle-track= "
				+ "--key-aspect-ratio= "
				+ "--key-crop= "
				+ "--key-toggle-autoscale= "
				+ "--key-incr-scalefactor= "
				+ "--key-decr-scalefactor= "
				+ "--key-deinterlace= "
				+ "--key-snapshot= "
				+ "--key-history-back= "
				+ "--key-history-forward= "
				+ "--key-record= "
				+ "--key-dump= "
				+ "--key-zoom= "
				+ "--key-unzoom= "
				+ "--key-wallpaper= "
				+ "--key-menu-on= "
				+ "--key-menu-off= "
				+ "--key-menu-right= "
				+ "--key-menu-left= "
				+ "--key-menu-up= "
				+ "--key-menu-down= "
				+ "--key-menu-select= "
				+ "--key-crop-top= "
				+ "--key-uncrop-top= "
				+ "--key-crop-left= "
				+ "--key-uncrop-left= "
				+ "--key-crop-bottom= "
				+ "--key-uncrop-bottom= "
				+ "--key-crop-right= "
				+ "--key-uncrop-right= "
				+ "--key-random= "
				+ "--key-loop= "
				+ "--key-zoom-quarter= "
				+ "--key-zoom-half= "
				+ "--key-zoom-original= "
				+ "--key-zoom-double= "
				+ "--key-set-bookmark1= "
				+ "--key-set-bookmark2= "
				+ "--key-set-bookmark3= "
				+ "--key-set-bookmark4= "
				+ "--key-set-bookmark5= "
				+ "--key-set-bookmark6= "
				+ "--key-set-bookmark7= "
				+ "--key-set-bookmark8= "
				+ "--key-set-bookmark9= "
				+ "--key-set-bookmark10= "
				+ "--key-play-bookmark1= "
				+ "--key-play-bookmark2= "
				+ "--key-play-bookmark3= "
				+ "--key-play-bookmark4= "
				+ "--key-play-bookmark5= "
				+ "--key-play-bookmark6= "
				+ "--key-play-bookmark7= "
				+ "--key-play-bookmark8= "
				+ "--key-play-bookmark9= "
				+ "--key-play-bookmark10= "
				+ "--ignore-config";
            this.vlc.StartInfo.CreateNoWindow = true;
			this.vlc.StartInfo.FileName = Properties.Settings.Default.PathToExe;

			// Kill any leftover processes
			foreach (Process proc in Process.GetProcessesByName(ShortName))
				proc.Kill();
			Start();

			this.playlist = new List<Media>();
			this.timer = new Timer(new TimerCallback(Monitor), null, Timeout.Infinite, Timeout.Infinite);
			this.vipPlaylist = new List<Media>();
			this.TrackChange += new TrackChange(AutoAudioTrackChange);
		}

		#region Private methods

		private void AutoAudioTrackChange(Media m)
		{
			//if (this.vipPlaylist.Count > 0)
			//{
			//    this.Control("stop");
			//    this.Control("clear");

			//    this.playlist.RemoveRange(0, this.playlistPtr);
			//    this.vipPlaylist.AddRange(this.playlist);
			//    this.playlist = this.vipPlaylist;
			//    this.vipPlaylist.Clear();

			//    Control("add " + this.playlist[0].Path.Replace(@"\", "/"));
			//    for (int i = 1; i < this.playlist.Count; i++)
			//        Control("enqueue " + this.playlist[i].Path.Replace(@"\", "/"));
			//    this.playlistPtr = -1;
			//}
			Thread.Sleep(1000);
			if (m.Path.ToLower().Contains("track 1"))
				Control("atrack 2");
		}

		private string Control(string cmd)
		{
			if (this.vlc.HasExited)
			{
				this.timer.Change(Timeout.Infinite, Timeout.Infinite);
				System.Windows.Forms.MessageBox.Show(
					"VLC has terminated unexpectedly. Please restart it from the tray.",
					"VLC is no more",
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Information);
				return string.Empty;
			}

			string response = string.Empty;
			lock (this.vlcRC)
			{
				byte[] c = Encoding.UTF8.GetBytes(cmd + Program.endl);
				this.vlcRC.Write(c, 0, c.Length);	// Send the command to VLC
				Thread.Sleep(tcpWait);
				c = new byte[buffersize];
				int tmp;
				do
				{
					tmp = this.vlcRC.Read(c, 0, buffersize);
					response += this.utf8.GetString(c);
				} while (tmp == 1024);
				response = response.TrimEnd(this.whitespace);
			}
			return response;
		}

		private string IsPlaying()
		{
			string current = null, status = Control("status");
			try
			{
				current = status.Substring(status.IndexOf('/'));
				current = current.Substring(0, current.IndexOf("\r\n") - 2);
			}
			catch { }
			return current;
		}

		/// <summary>
		/// Monitors when the next item starts playing. Due to VLC
		/// not being able to to provide accurate time readings,
		/// the monitor must frequently poll VLC for any changes.
		/// </summary>
		/// <param name="state"></param>
		private void Monitor(object state)
		{
			string current = this.IsPlaying();
			if ((current == null) | this.randomMode)
				return;

			lock (this.playlist)
			{
				// Make sure the media path has the right syntax, then
				// check to see if the current status has changed
				string id = this.playlist[this.playlistPtr].Path.Replace('\\', '/');
				id = id.Substring(id.IndexOf('/'));

				if (id != current)
				{
					this.playlistPtr++;
					if (this.playlistPtr == this.playlist.Count)
					{
						this.playlistPtr--;
						return;
					}
					TrackChange(this.playlist[this.playlistPtr]);
				}
			}
		}

		/// <summary>
		/// Properly shuts down VLC and frees all resources.
		/// </summary>
		/// <param name="quit">Whether to terminate VLC</param>
		private void Shutdown(bool quit)
		{
			if (this.timer != null)
				this.timer.Change(Timeout.Infinite, Timeout.Infinite);
			if (!this.vlc.HasExited)
			{
				this.vlcClient.Close();
				this.vlcRC.Close();
				this.vlcRC.Dispose();
				this.vlc.Kill();
				this.vlc.WaitForExit();
			}
			if (quit)
				this.vlc.Close();
			this.playlist.Clear();
		}

		/// <summary>
		/// Starts up VLC and makes a TCP connection to it. Also
		/// clears the playlist.
		/// </summary>
		private void Start()
		{
			this.vlc.Start();
			this.vlc.WaitForInputIdle();
			this.vlcClient = new TcpClient();
			this.vlcClient.Connect(host, port);
			this.vlcRC = this.vlcClient.GetStream();
			Control("clear");
			Control("loop off");
		}

		#endregion

		#region AbstractMediaPlayer Members

		public override event TrackChange TrackChange;

		public override Media CurrentlyPlaying { get { return this.playlist[this.playlistPtr]; } }
		public override bool IsPaused { get { return this.paused; } }
		public override Media NextTrack
		{
			get
			{
				int n = this.playlistPtr + 1;
				if (n >= this.playlist.Count)
					return null;
				return this.playlist[n];
			}
		}

		public override void Add(Media media)
		{
			if ((IsPlaying() == null) | this.randomMode) // if it's not currently playing
			{
				Control("add " + media.Path.Replace(@"\", "/"));
				this.playlistPtr = 0;
				this.playlist.Clear();
				this.playlist.Add(media);
				TrackChange(CurrentlyPlaying);
				this.timer.Change(monitorInterval, monitorInterval);
			}
			else
			{
				Control("enqueue " + media.Path.Replace(@"\", "/"));
				this.playlist.Add(media);
			}
		}

		public override void CycleAudioTracks()
		{
			string[] response = Control("atrack").Split(this.whitespace, StringSplitOptions.RemoveEmptyEntries);
			int track = 1, tracks = 0;
			for (int i = 1; i < response.Length - 2; i++)
			{
				if (response[i].StartsWith("|"))
				{
					tracks++;
					if (response[i].TrimEnd().EndsWith("*"))
						track = tracks;
				}
			}
			tracks--;
			if (track > tracks)
				track = 1;
			Control("atrack " + track.ToString());
		}

		public override void Next()
		{
			if (this.playlistPtr < this.playlist.Count - 1)
			{
				if (this.paused)
					this.Control("pause");
				lock (this.playlist)
				{
					this.Control("next");
					this.playlistPtr++;
					TrackChange(CurrentlyPlaying);
				}
			}
		}

		public override void Pause()
		{
			if (this.paused)
				this.timer.Change(monitorInterval, monitorInterval);
			else
				this.timer.Change(Timeout.Infinite, Timeout.Infinite);
			Control("pause");
			this.paused ^= true;
		}

		public override void Previous()
		{
			if (this.playlistPtr > 0)
			{
				if (this.paused)
					this.Control("pause");
				lock (this.playlist)
				{
					this.Control("prev");
					this.playlistPtr--;
					TrackChange(CurrentlyPlaying);
				}
			}
		}

		public override void Random()
		{
			if (this.randomMode)
			{
				Control("random off");
				Control("loop off");
				Control("clear");
				this.playlist.Clear();
				this.playlistPtr = 0;
			}
			else
			{
				Control("random on");
				Control("loop on");
				this.randomMode = true;
			}
		}

		public override void Restart()
		{
			Shutdown(false);
			Start();
		}

		public override void Shutdown()
		{
			Shutdown(true);
		}

		public override void VIPAdd(Media media)
		{
			this.vipPlaylist.Add(media);
		}

		public override void VolumeUp()
		{
			Control("volup");
		}

		public override void VolumeDown()
		{
			Control("voldown");
		}

		#endregion
	}
}
