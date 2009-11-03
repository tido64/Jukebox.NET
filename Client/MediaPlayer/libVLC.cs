using System;
using System.Runtime.InteropServices;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client.MediaPlayer
{
	/// <summary>
	/// VLC media player 1.0.x wrapper. Implements libVLC.
	/// 
	/// New feature in 2.0!
	/// </summary>
	/// <remarks>
	/// Documentation:
	/// http://wiki.videolan.org/LibVLC
	/// http://www.videolan.org/developers/vlc/doc/doxygen/html/group__libvlc.html
	/// http://www.helyar.net/2009/libvlc-media-player-in-c/
	/// </remarks>
	sealed class libVLC : AbstractMediaPlayer
	{
		#region libvlc external API

		#region libvlc.h

		[DllImport("libvlc")]
		private static extern void libvlc_exception_init(ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern int libvlc_exception_raised(ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_exception_clear(ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern string libvlc_exception_get_message(ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] argv, ref libvlc_exception_t ex);

		[DllImport("libvlc")]
		private static extern void libvlc_release(IntPtr instance);

		public delegate void libvlc_callback_t(IntPtr p_event, IntPtr p_user_data);

		[DllImport("libvlc")]
		private static extern void libvlc_event_attach(IntPtr p_event_manager, libvlc_event_type_t i_event_type, libvlc_callback_t f_callback, IntPtr p_user_data, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_event_detach(IntPtr p_event_manager, libvlc_event_type_t i_event_type, libvlc_callback_t f_callback, IntPtr p_user_data, ref libvlc_exception_t p_e);

		#endregion

		#region libvlc_events.h

		enum libvlc_event_type_t : uint
		{
			libvlc_MediaMetaChanged,
			libvlc_MediaSubItemAdded,
			libvlc_MediaDurationChanged,
			libvlc_MediaPreparsedChanged,
			libvlc_MediaFreed,
			libvlc_MediaStateChanged,

			libvlc_MediaPlayerNothingSpecial,
			libvlc_MediaPlayerOpening,
			libvlc_MediaPlayerBuffering,
			libvlc_MediaPlayerPlaying,
			libvlc_MediaPlayerPaused,
			libvlc_MediaPlayerStopped,
			libvlc_MediaPlayerForward,
			libvlc_MediaPlayerBackward,
			libvlc_MediaPlayerEndReached,
			libvlc_MediaPlayerEncounteredError,
			libvlc_MediaPlayerTimeChanged,
			libvlc_MediaPlayerPositionChanged,
			libvlc_MediaPlayerSeekableChanged,
			libvlc_MediaPlayerPausableChanged,

			libvlc_MediaListItemAdded,
			libvlc_MediaListWillAddItem,
			libvlc_MediaListItemDeleted,
			libvlc_MediaListWillDeleteItem,

			libvlc_MediaListViewItemAdded,
			libvlc_MediaListViewWillAddItem,
			libvlc_MediaListViewItemDeleted,
			libvlc_MediaListViewWillDeleteItem,

			libvlc_MediaListPlayerPlayed,
			libvlc_MediaListPlayerNextItemSet,
			libvlc_MediaListPlayerStopped,

			libvlc_MediaDiscovererStarted,
			libvlc_MediaDiscovererEnded,

			libvlc_MediaPlayerTitleChanged,
			libvlc_MediaPlayerSnapshotTaken,
		}

		#endregion

		#region libvlc_media.h

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_new(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)]string psz_mrl, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_release(IntPtr p_meta_desc);

		#endregion

		#region libvlc_media_list.h

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_list_new(IntPtr p_instance, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_release(IntPtr p_ml);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_add_media(IntPtr p_ml, IntPtr p_m, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_insert_media(IntPtr p_ml, IntPtr p_m, int i_pos, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_remove_index(IntPtr p_ml, int i_pos, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_lock(IntPtr p_ml);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_unlock(IntPtr p_ml);

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_list_flat_view(IntPtr p_ml, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_list_event_manager(IntPtr p_ml, ref libvlc_exception_t p_e);

		#endregion

		#region libvlc_media_list_player.h

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_list_player_new(IntPtr p_instance, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_release(IntPtr p_mlp);

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_list_player_event_manager(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_set_media_player(IntPtr p_mlp, IntPtr p_mi, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_set_media_list(IntPtr p_mlp, IntPtr p_mlist, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_play(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_pause(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern int libvlc_media_list_player_is_playing(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_stop(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_next(IntPtr p_mlp, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_list_player_previous(IntPtr p_mlp, ref libvlc_exception_t p_e);

		#endregion

		#region libvlc_media_player.h

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_player_new(IntPtr p_instance, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_media_player_release(IntPtr p_mi);

		[DllImport("libvlc")]
		private static extern IntPtr libvlc_media_player_event_manager(IntPtr p_mi, ref libvlc_exception_t ex);

		[DllImport("libvlc")]
		private static extern void libvlc_media_player_set_drawable(IntPtr p_mi, IntPtr p_drawable, ref libvlc_exception_t p_e);

		//[DllImport("libvlc")]
		//private static extern void libvlc_video_set_deinterlace(IntPtr p_mi, int b_enable, ref char[] psz_mode, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern int libvlc_audio_get_volume(IntPtr p_instance, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_audio_set_volume(IntPtr p_instance, int i_volume, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern int libvlc_audio_get_track_count(IntPtr p_mi, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern int libvlc_audio_get_track(IntPtr p_mi, ref libvlc_exception_t p_e);

		[DllImport("libvlc")]
		private static extern void libvlc_audio_set_track(IntPtr p_mi, int track, ref libvlc_exception_t p_e);

		#endregion

		#region libvlc_structures.h

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct libvlc_exception_t
		{
			int b_raised;
			int i_code;
			[MarshalAs(UnmanagedType.LPStr)]
			string psz_message;
		}

		#endregion

		#endregion

		bool paused, playing;
		string[] parameters = new string[] {
				System.Windows.Forms.Application.ExecutablePath,
				"--sout-transcode-afilter=normvol",
				"--hotkeys-mousewheel-mode=2",
				"--deinterlace-mode=yadif2x",
				"--volume=512",
				"--fullscreen",
				"--quiet-synchro",
				"--no-video-title-show",
				"--mouse-hide-timeout=1",
				"--vout-filter=deinterlace", // will apply deinterlacing on ALL videos, including progressive
				"--no-spu",
				"--no-osd",
				"--one-instance",
				"--quiet",
				"--no-stats",
				"--intf=dummy",
				"--ignore-config"};
		internal IntPtr vlc_event_manager, vlc_instance, vlc_mi, vlc_ml, vlc_mlp;
		static libvlc_exception_t vlc_e;
		libvlc_callback_t vlc_track_change;

		System.Windows.Forms.Form vlc_window;

		public libVLC()
			: base("VLC media player", "vlc")
		{
			libvlc_exception_init(ref vlc_e);
			this.vlc_instance = libvlc_new(parameters.Length, parameters, ref vlc_e); ThrowException();
			this.vlc_track_change = TrackChanged;

			#region Initialize VLC window

			this.vlc_window = new System.Windows.Forms.Form();
			this.vlc_window.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.vlc_window.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.vlc_window.BackColor = System.Drawing.Color.Black;
			this.vlc_window.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.vlc_window.CausesValidation = false;
			this.vlc_window.ClientSize = new System.Drawing.Size(853, 480); //this.vlc_window.ClientSize = new System.Drawing.Size(Monitor.Width, Monitor.Height);
			this.vlc_window.ControlBox = false;
			this.vlc_window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.vlc_window.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; //this.vlc_window.Location = new System.Drawing.Point(Monitor.BoundStart, 0);
			this.vlc_window.Name = "Jukebox.NET";
			this.vlc_window.ShowIcon = false;
			this.vlc_window.ShowInTaskbar = false;
			this.vlc_window.TopMost = false;
			this.vlc_window.ResumeLayout(false);

			#endregion
		}

		private void ThrowException()
		{
			if (libvlc_exception_raised(ref vlc_e) != 0)
			{
				Shutdown();
				throw new Exception(libvlc_exception_get_message(ref vlc_e));
			}
			libvlc_exception_clear(ref vlc_e);
		}

		private void TrackChanged(IntPtr p_event, IntPtr p_user_data)
		{
			int playing = libvlc_media_list_player_is_playing(this.vlc_mlp, ref vlc_e);
			System.Windows.Forms.MessageBox.Show("test = " + playing.ToString());
			//TrackChange(this.CurrentlyPlaying);
		}

		#region AbstractMediaPlayer Members

		public override event TrackChange TrackChange;

		public override Media CurrentlyPlaying
		{
			get { throw new NotImplementedException(); }
		}

		public override bool IsPaused
		{
			get { return this.paused; }
		}

		public override Media NextTrack
		{
			get { throw new NotImplementedException(); }
		}

		public override void Add(Media m)
		{
			if (this.vlc_mi == IntPtr.Zero)
			{
				// Instantiate vlc objects
				this.vlc_ml = libvlc_media_list_new(this.vlc_instance, ref vlc_e); ThrowException();
				this.vlc_mlp = libvlc_media_list_player_new(this.vlc_instance, ref vlc_e); ThrowException();
				this.vlc_mi = libvlc_media_player_new(this.vlc_instance, ref vlc_e); ThrowException();

				// Attach to events
				//this.vlc_event_manager = libvlc_media_player_event_manager(this.vlc_mi, ref vlc_e); ThrowException();
				//libvlc_event_attach(this.vlc_event_manager, libvlc_event_type_t.libvlc_MediaPlayerOpening, this.vlc_track_change, IntPtr.Zero, ref vlc_e); ThrowException();
				//this.vlc_event_manager = libvlc_media_list_player_event_manager(this.vlc_mlp, ref vlc_e); ThrowException();
				//libvlc_event_attach(this.vlc_event_manager, libvlc_event_type_t.libvlc_MediaListPlayerNextItemSet, this.vlc_track_change, IntPtr.Zero, ref vlc_e); ThrowException();

				// Associate media list and player
				libvlc_media_list_player_set_media_list(this.vlc_mlp, this.vlc_ml, ref vlc_e); ThrowException();
				libvlc_media_list_player_set_media_player(this.vlc_mlp, this.vlc_mi, ref vlc_e); ThrowException();

				// Make drawable
				libvlc_media_player_set_drawable(this.vlc_mi, this.vlc_window.Handle, ref vlc_e); ThrowException();
				this.vlc_window.Visible = true;
			}

			libvlc_media_list_lock(this.vlc_ml);
			IntPtr media = libvlc_media_new(this.vlc_instance, m.Path, ref vlc_e); ThrowException();
			libvlc_media_list_add_media(this.vlc_ml, media, ref vlc_e); ThrowException();
			libvlc_media_release(media);
			libvlc_media_list_unlock(this.vlc_ml);

			if (!playing & !paused)
			{
				libvlc_media_list_player_play(this.vlc_mlp, ref vlc_e); ThrowException();

				playing = true;
				paused = false;
			}
		}

		public override void CycleAudioTracks()
		{
			int tracks = libvlc_audio_get_track_count(this.vlc_mi, ref vlc_e); ThrowException();
			int t = libvlc_audio_get_track(this.vlc_mi, ref vlc_e) + 1; ThrowException();
			if (t == tracks) t = 1; // Last track is no audio
			libvlc_audio_set_track(this.vlc_mi, t, ref vlc_e); ThrowException();
		}

		public override void Next()
		{
			libvlc_media_list_player_next(this.vlc_mlp, ref vlc_e); ThrowException();
		}

		public override void Pause()
		{
			libvlc_media_list_player_pause(this.vlc_mlp, ref vlc_e); ThrowException();

			if (this.playing)
				this.paused ^= true;
		}

		public override void Previous()
		{
			libvlc_media_list_player_previous(this.vlc_mlp, ref vlc_e); ThrowException();
		}

		public override void Random()
		{
			throw new NotImplementedException();
		}

		public override void Restart()
		{
			if (this.vlc_mi == IntPtr.Zero)
				return;

			libvlc_media_list_player_stop(this.vlc_mlp, ref vlc_e); ThrowException();
			this.playing = false;
			this.paused = false;
			this.vlc_window.Visible = false;

			if (this.vlc_mlp != IntPtr.Zero)
			{
				libvlc_media_list_player_release(this.vlc_mlp);
				this.vlc_mlp = IntPtr.Zero;
			}
			if (this.vlc_ml != IntPtr.Zero)
			{
				libvlc_media_list_release(this.vlc_ml);
				this.vlc_ml = IntPtr.Zero;
			}
			if (this.vlc_mi != IntPtr.Zero)
			{
				//libvlc_event_detach(this.vlc_event_manager, libvlc_event_type_t.libvlc_MediaListPlayerNextItemSet, this.vlc_track_change, IntPtr.Zero, ref vlc_e); ThrowException();
				libvlc_media_player_release(this.vlc_mi);
				this.vlc_mi = IntPtr.Zero;
			}
		}

		public override void Shutdown()
		{
			Restart();
			if (this.vlc_instance == IntPtr.Zero)
				return;
			libvlc_release(this.vlc_instance);
			this.vlc_instance = IntPtr.Zero;
		}

		public override void VIPAdd(Media m)
		{
			libvlc_media_list_lock(this.vlc_ml);
			IntPtr media = libvlc_media_new(this.vlc_instance, m.Path, ref vlc_e); ThrowException();
			libvlc_media_list_insert_media(this.vlc_ml, media, 1, ref vlc_e); ThrowException();
			libvlc_media_release(media);
			libvlc_media_list_unlock(this.vlc_ml);
		}

		public override void VolumeUp()
		{
			int cur_vol = libvlc_audio_get_volume(this.vlc_instance, ref vlc_e); ThrowException();
			if (cur_vol < 200)
				libvlc_audio_set_volume(this.vlc_instance, cur_vol + 10, ref vlc_e); ThrowException();
		}

		public override void VolumeDown()
		{
			int cur_vol = libvlc_audio_get_volume(this.vlc_instance, ref vlc_e); ThrowException();
			if (cur_vol > 0)
				libvlc_audio_set_volume(this.vlc_instance, cur_vol - 10, ref vlc_e); ThrowException();
		}

		#endregion
	}
}
