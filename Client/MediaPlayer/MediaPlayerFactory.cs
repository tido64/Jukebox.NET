using System;
using System.Collections.Generic;

namespace Jukebox.NET.MediaPlayer
{
	sealed class MediaPlayerFactory
	{
		public static readonly List<string> Players = new List<string>() { "MPlayer" };

		public static AbstractMediaPlayer Create(IntPtr hWnd)
		{
			switch (App.Config.Player.ToLower())//Properties.Settings.Default.MediaPlayer)
			{
				//case "Media Player Classic":
				//    return new MPC();
				case "mplayer":
				    return new MPlayer(hWnd);
				//case "VLC media player":
				//    return new VLC();
				default:
					return null;
			}
		}
	}
}
