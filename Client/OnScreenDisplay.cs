using System;
using System.Threading;
using System.Windows.Forms;

using Jukebox.NET.Client.MediaPlayer;
using Jukebox.NET.Common;

namespace Jukebox.NET.Client
{
	public partial class OnScreenDisplay : Form
	{
		#region Private members

		private delegate void Display();
		private event Display DisplayUp;

		// Settings
		private const double maxOpacity = 0.8;
		private const int speed = 30;

		// Flags and timers
		private bool showing = false;
		private System.Threading.Timer t_DisplayDown, t_DisplayNext;

		// Cache
		private readonly string version = Application.ProductName + " v" + Application.ProductVersion;
		private readonly AbstractMediaPlayer mediaPlayer;
		private string next = string.Empty;

		// Input
		private int input = 0;
		private HotKeys hotkeys = null;
		private Media media;

		#endregion

		public OnScreenDisplay(AbstractMediaPlayer mp)
		{
			this.Visible = false;
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			this.mediaPlayer = mp;
			this.mediaPlayer.TrackChange += new TrackChange(this.ShowCurrent);

			this.DisplayUp += new Display(this.FadeIn);
			this.t_DisplayDown = new System.Threading.Timer(new TimerCallback(this.FadeOut), null, Timeout.Infinite, Timeout.Infinite);
			this.Opacity = 0;
			this.TextDisplay.Text = this.version;
			this.Visible = true;

			this.hotkeys = new HotKeys(this.Handle);

			this.DisplayUp();
		}

		#region Display animation

		private void FadeIn()
		{
			this.Focus();
			if (!this.showing)
			{
				this.showing = true;
				for (int i = (int)(this.Opacity * 10); i < maxOpacity * 10; i++)
				{
					this.Opacity = i / 10.0;
					this.Update();
					Thread.Sleep(speed);
				}
				this.Opacity = maxOpacity;
			}
		}

		private void FadeOut(object state)
		{
			if (this.next != string.Empty)
			{
				this.TextDisplay.Text = this.next;
				this.next = string.Empty;
				this.t_DisplayDown.Change(Properties.Settings.Default.TimeToDisplayMedia, Timeout.Infinite);
			}
			else
			{
				this.showing = false;
				for (int i = (int)(this.Opacity * 10); i > 0; i--)
				{
					this.Opacity = i / 10.0;
					this.Update();
					Thread.Sleep(speed);
				}
				this.Opacity = 0;
				this.TextDisplay.Text = string.Empty;
			}
		}

		#endregion

		#region Display content

		private void Show(string s)
		{
			this.TextDisplay.Text = s;
			this.DisplayUp();
			this.t_DisplayDown.Change(Properties.Settings.Default.TimeToDisplayMedia, Timeout.Infinite);
		}

		private void ShowCurrent(Media m)
		{
			if (m == null)
				return;

			int id = m.Id + DatabaseManager.IdOffset;
			this.Show("Now: " + id.ToString() + ". " + m.ToString());
			this.ShowNext(null);
		}

		private void ShowNext(object state)
		{
			this.t_DisplayNext = new System.Threading.Timer(
				new TimerCallback(this.ShowNext),
				null,
				Properties.Settings.Default.Interval,
				Timeout.Infinite);

			// Check to see if we're at the bottom of the playlist
			Media m = this.mediaPlayer.NextTrack;
			if (m == null) return;

			int id = m.Id + DatabaseManager.IdOffset;
			string next = "Next: " + id.ToString() + ". " + m.ToString();
			if (this.showing)
				this.next = next;
			else
				this.Show(next);
		}

		/// <summary>
		/// Display user input and show suggestions.
		/// </summary>
		/// <param name="choice">User input</param>
		private void ShowRequest()
		{
			this.t_DisplayDown.Change(Timeout.Infinite, Timeout.Infinite);
			if (!this.TextDisplay.Text.StartsWith(">") & !this.TextDisplay.Text.StartsWith("+") & !this.TextDisplay.Text.Equals(this.version))
				this.next = this.TextDisplay.Text;

			this.TextDisplay.Text = string.Format("> {0}{1} ({2}?)",
				this.input.ToString(),
				new string('_', Math.Max(4 - this.input.ToString().Length, 0)),
				this.media == null ? string.Empty : this.media.Title);
			this.DisplayUp();
		}

		private void ShowRequested()
		{
			if (this.media == null)
				this.TextDisplay.Text = "+ Please try again.";
			else
				this.TextDisplay.Text = string.Format("+ {0}. {1}", this.media.Id + DatabaseManager.IdOffset, this.media);
			this.t_DisplayDown.Change(Properties.Settings.Default.TimeToDisplayRequest, Timeout.Infinite);

			// Reset input
			this.input = 0;
			this.media = null;
		}

		#endregion

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			if ((m.Msg == HotKeys.WM_HOTKEY))
			{
				int vk = m.WParam.ToInt32();
				if (this.mediaPlayer.IsPaused)
				{
					if ((Keys)vk == HotKeys.Pause)
						this.mediaPlayer.Pause();
				}
				else if (vk > 9)
				{
					switch ((Keys)vk)
					{
						case HotKeys.Add:
							if (this.media != null)
								this.mediaPlayer.Add(this.media);
							this.ShowRequested();
							break;
						case HotKeys.Current:
							ShowCurrent(this.mediaPlayer.CurrentlyPlaying);
							break;
						case HotKeys.Cycle:
							this.mediaPlayer.CycleAudioTracks();
							break;
						case HotKeys.Next:
							this.mediaPlayer.Next();
							break;
						case HotKeys.Pause:
							this.mediaPlayer.Pause();
							break;
						case HotKeys.Previous:
							this.mediaPlayer.Previous();
							break;
						case HotKeys.VIPAdd:
							if (this.media != null)
								this.mediaPlayer.VIPAdd(this.media);
							this.ShowRequested();
							break;
						case HotKeys.VolumeDown:
							this.mediaPlayer.VolumeDown();
							break;
						case HotKeys.VolumeUp:
							this.mediaPlayer.VolumeUp();
							break;
					}
				}
				else
				{
					if (this.input == 0 && vk == 0)
						return;

					// User input differs from the actual internal input
					if (this.input * 10 > int.MaxValue / 10)
						this.input /= 10;
					this.input = this.input * 10 + vk;

					// Rounding up
					int choice = this.input;
					while (choice <= DatabaseManager.IdOffset)
						choice *= 10;

					// Find media in database
					try
					{
						this.media = DatabaseManager.Instance.Find(choice);
					}
					catch
					{
						this.media = null;
					}
					this.ShowRequest();
				}
			}
			base.WndProc(ref m);
		}
	}
}
