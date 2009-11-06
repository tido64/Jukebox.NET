using System;
using System.Windows.Forms;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client.MediaPlayer
{
	public abstract class AbstractMediaPlayer
	{
		public abstract event TrackChange TrackChange;

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
