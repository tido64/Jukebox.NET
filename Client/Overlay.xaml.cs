using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

using Jukebox.NET.Common;

namespace Jukebox.NET
{
	/// <summary>
	/// Interaction logic for Overlay.xaml
	/// </summary>
	public partial class Overlay : Window
	{
		private int input;
		private MediaPlayer.AbstractMediaPlayer player;
		private DispatcherTimer announcer;
		private HotKeys hotkeys;
		private Media media;
		private OnScreenDisplay osd;

		public Overlay()
		{
			InitializeComponent();
			this.Height = Monitor.Height;
			this.Left = Monitor.BoundStart;
			this.Title = App.Name;
			this.Width = Monitor.Width;
		}

		#region UI events

		private void Overlay_SourceInitialized(object sender, EventArgs e)
		{
			IntPtr hWnd = new WindowInteropHelper(this).Handle;

			this.osd = new OnScreenDisplay();
			this.osd.Owner = this;

			this.player = MediaPlayer.MediaPlayerFactory.Create(hWnd);
			this.player.MediaChange += new MediaChange(this.MediaChange);

			DatabaseManager.Instance.Load(App.Config.Drive);

			this.hotkeys = new HotKeys(hWnd);
			HwndSource src = HwndSource.FromHwnd(hWnd);
			src.AddHook(new HwndSourceHook(this.WndProc));

			this.announcer = new DispatcherTimer();
			this.announcer.Interval = TimeSpan.FromSeconds(App.Config.OSD.Interval);
			this.announcer.Tick += new EventHandler(this.AnnounceNext);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.announcer.Stop();
			this.osd.Close();
			this.player.Shutdown();
		}

		#endregion

		#region Player events

		private void AnnounceNext(object sender, EventArgs e)
		{
			Media m = this.player.NextTrack;
			if (m == null)
				return;
			this.osd.Text = "Next: " + (m.Id + DatabaseManager.IdOffset) + ". " + m;
		}

		private void MediaChange(Media m)
		{
			this.osd.Text = "Now: " + (m.Id + DatabaseManager.IdOffset) + ". " + m;
			this.announcer.Start();
		}

		#endregion

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg != HotKeys.WM_HOTKEY)
				return IntPtr.Zero;

			int vk = wParam.ToInt32();
			if (this.player.IsPaused)
			{
				if (vk == HotKeys.Pause)
					this.player.Pause();
			}
			else if (vk > 9)
			{
				switch (vk)
				{
					case HotKeys.Add:
						this.input = 0;
						this.osd.Persist = false;
						if (this.media == null)
							this.osd.Text = "+ Please try again.";
						else
						{
							this.osd.Text = "+ " + this.media;
							this.player.Add(this.media);
						}
						break;
					case HotKeys.Current:
						Media m = this.player.CurrentlyPlaying;
						this.osd.Text = "Now: " + (m.Id + DatabaseManager.IdOffset) + ". " + m;
						break;
					case HotKeys.Cycle:
						this.player.CycleAudioTracks();
						break;
					case HotKeys.Next:
						this.player.Next();
						break;
					case HotKeys.Pause:
						this.player.Pause();
						break;
					case HotKeys.Previous:
						this.player.Previous();
						break;
					case HotKeys.VIPAdd:
						this.input = 0;
						this.osd.Persist = false;
						if (this.media == null)
							this.osd.Text = "+ Please try again.";
						else
						{
							this.osd.Text = "+ " + this.media;
							this.player.VIPAdd(this.media);
						}
						break;
					case HotKeys.VolumeDown:
						this.player.VolumeDown();
						break;
					case HotKeys.VolumeUp:
						this.player.VolumeUp();
						break;
				}
			}
			else
			{
				if (this.input == 0 && vk == 0)
					return IntPtr.Zero;

				// User input differs from the actual internal input
				if (this.input * 10 > int.MaxValue / 10)
					this.input /= 10;
				this.input = this.input * 10 + vk;

				// Rounding up
				int choice = this.input;
				while (choice <= DatabaseManager.IdOffset)
					choice *= 10;

				// Find media in database
				string s = string.Empty;
				try
				{
					this.media = DatabaseManager.Instance.Find(choice);
					s += " (" + this.media.Title + ")";
				}
				catch
				{
					this.media = null;
					s += " (?)";
				}
				this.osd.Persist = true;
				this.osd.Text = "> " + this.input + new string('_', Math.Max(4 - this.input.ToString().Length, 0)) + s;
			}
			return IntPtr.Zero;
		}
	}
}
