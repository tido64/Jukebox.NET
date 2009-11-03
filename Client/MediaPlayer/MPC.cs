using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client.MediaPlayer
{
	sealed class MPC : AbstractMediaPlayer
	{
		private const int nil = 0;
		private IList<Media> playlist;
		private Process mpc;

		public MPC(string path)
			: base("Media Player Classic", "MPC")
		{
			this.mpc = new Process();
			//this.mpc.StartInfo.Arguments = "-fs -slave";
			this.mpc.StartInfo.CreateNoWindow = true;
			this.mpc.StartInfo.FileName = path;
			this.mpc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

			this.playlist = new List<Media>();

			foreach (Process proc in Process.GetProcessesByName(ShortName))
				proc.Kill();
			this.mpc.Start();
		}

		// Activate an application window.
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		private void SendMessage(Keys mod, Keys key)
		{
			SendMessage(this.mpc.Handle, (int)mod, (IntPtr)key, IntPtr.Zero);
		}

		#region AbstractMediaPlayer Members

		public override event NextUpEvent NextUpEvent;

		public override Media CurrentlyPlaying
		{
			get { throw new System.NotImplementedException(); }
		}

		public override Media NextUp
		{
			get { throw new System.NotImplementedException(); }
		}

		public override void Add(Media media)
		{
			throw new System.NotImplementedException();
		}

		public override void CycleAudioTracks()
		{
			SendMessage(nil, Keys.A);
		}

		public override void Next()
		{
			SendMessage(Keys.Alt, Keys.Next);
		}

		public override void Pause()
		{
			SendMessage(nil, Keys.Space);
		}

		public override void Previous()
		{
			SendMessage(Keys.Alt, Keys.Prior);
		}

		public override void Restart()
		{
			Shutdown();
			try
			{
				this.mpc.CloseMainWindow();
			}
			catch
			{
				foreach (Process proc in Process.GetProcessesByName(ShortName))
					proc.Kill();
			}
			this.mpc.Start();
		}

		public override void Shutdown()
		{
			SendMessage(Keys.Alt, Keys.X);
		}

		public override void VIPAdd(Media media)
		{
			throw new System.NotImplementedException();
		}

		public override void VolumeUp()
		{
			SendMessage(nil, Keys.Up);
		}

		public override void VolumeDown()
		{
			SendMessage(nil, Keys.Down);
		}

		#endregion
	}
}
