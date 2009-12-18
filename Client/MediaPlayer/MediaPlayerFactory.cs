using System;
using System.Collections.Generic;

namespace Jukebox.NET.MediaPlayer
{
	sealed class MediaPlayerFactory
	{
		private static readonly List<string> players = new List<string> { "mplayer" };

		public static bool CanCreate(string p)
		{
			return players.Contains(p.ToLowerInvariant());
		}

		public static AbstractMediaPlayer Create(IntPtr hWnd)
		{
			AbstractMediaPlayer p = null;
			switch (App.Config.Player.ToLowerInvariant())
			{
				//case "Media Player Classic":
				//    return new MPC();
				case "mplayer":
				    p = new MPlayer(hWnd);
					break;
			}
			return p;
		}
	}
}
