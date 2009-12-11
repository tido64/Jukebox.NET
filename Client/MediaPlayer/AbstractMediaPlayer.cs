using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Jukebox.NET.Common;

namespace Jukebox.NET
{
	public delegate void MediaChange(Media m);
}

namespace Jukebox.NET.MediaPlayer
{
	public abstract class AbstractMediaPlayer
	{
		public abstract event MediaChange MediaChange;

		public AbstractMediaPlayer(string name, string shortName)
		{
			this.Name = name;
			this.ShortName = shortName;
		}

		public string Name { get; private set; }
		public string ShortName { get; private set; }

		public abstract Media CurrentlyPlaying { get; }
		public abstract bool IsPaused { get; }
		public abstract Media NextTrack { get; }

		public abstract void Add(Media media);
		public abstract void CycleAudioTracks();
		public abstract List<Media> GetNextTracks(int tracks);
		public abstract void Next();
		public abstract void Pause();
		public abstract void Previous();
		public abstract void Random();
		public abstract void Restart();
		public abstract void Shutdown();
		public abstract void VIPAdd(Media media);
		public abstract void VolumeUp();
		public abstract void VolumeDown();
	}
}
