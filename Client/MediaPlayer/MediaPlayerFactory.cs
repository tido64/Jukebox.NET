using System;
using System.Collections.Generic;

namespace Jukebox.NET.Client.MediaPlayer
{
	sealed class MediaPlayerFactory
	{
		public static readonly List<string> Players = new List<string>() { "MPlayer", "VLC media player" };

		public static AbstractMediaPlayer Create()
		{
			switch (Properties.Settings.Default.MediaPlayer)
			{
				//case "Media Player Classic":
				//    return new MPC();
				case "MPlayer":
				    return new MPlayer();
				case "VLC media player":
					return new VLC();
				default:
					//return new libVLC();
					//return new MPlayer();
					return null;
			}
		}
	}
}
