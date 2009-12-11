using System;
using System.Runtime.InteropServices;

namespace Jukebox.NET
{
	class HotKeys
	{
		// Windows constants
		private const int
			MOD_ALT = 1,
			MOD_CTRL = 2,
			MOD_SHIFT = 4,
			MOD_WIN = 8;
		public const int WM_HOTKEY = 0x312;

		// Hot keys
		private const int MOD = MOD_CTRL;
		public const int
			Add = 107,			// Try and add the current id
			Current = 116,		// Show currently playing
			Cycle = 121,		// Cycle audio tracks
			Next = 120,			// Advance in playlist
			Pause = 119,		// Pause
			Previous = 118,		// Regress in playlist
			VIPAdd = 117,		// Try and add the current id (VIP)
			VolumeDown = 122,	// Decrease volume
			VolumeUp = 123;		// Increase volume

		#region Unmanaged code

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		#endregion

		private readonly IntPtr handle;

		/// <summary>
		/// Registers global hot keys.
		/// </summary>
		/// <param name="hWnd">Main window handle</param>
		public HotKeys(IntPtr hWnd)
		{
			this.handle = hWnd;

			// Register numeric keys
			RegisterHotKey(this.handle, 0, 0, 96);
			RegisterHotKey(this.handle, 1, 0, 97);
			RegisterHotKey(this.handle, 2, 0, 98);
			RegisterHotKey(this.handle, 3, 0, 99);
			RegisterHotKey(this.handle, 4, 0, 100);
			RegisterHotKey(this.handle, 5, 0, 101);
			RegisterHotKey(this.handle, 6, 0, 102);
			RegisterHotKey(this.handle, 7, 0, 103);
			RegisterHotKey(this.handle, 8, 0, 104);
			RegisterHotKey(this.handle, 9, 0, 105);

			// Register hot keys
			RegisterHotKey(this.handle, Add.GetHashCode(), 0, Add);
			RegisterHotKey(this.handle, Current.GetHashCode(), MOD, Current);
			RegisterHotKey(this.handle, Cycle.GetHashCode(), MOD, Cycle);
			RegisterHotKey(this.handle, Next.GetHashCode(), MOD, Next);
			RegisterHotKey(this.handle, Pause.GetHashCode(), MOD, Pause);
			RegisterHotKey(this.handle, Previous.GetHashCode(), MOD, Previous);
			RegisterHotKey(this.handle, VIPAdd.GetHashCode(), MOD, VIPAdd);
			RegisterHotKey(this.handle, VolumeDown.GetHashCode(), MOD, VolumeDown);
			RegisterHotKey(this.handle, VolumeUp.GetHashCode(), MOD, VolumeUp);
		}

		~HotKeys()
		{
			UnregisterHotKey(this.handle, 0);
			UnregisterHotKey(this.handle, 1);
			UnregisterHotKey(this.handle, 2);
			UnregisterHotKey(this.handle, 3);
			UnregisterHotKey(this.handle, 4);
			UnregisterHotKey(this.handle, 5);
			UnregisterHotKey(this.handle, 6);
			UnregisterHotKey(this.handle, 7);
			UnregisterHotKey(this.handle, 8);
			UnregisterHotKey(this.handle, 9);
			UnregisterHotKey(this.handle, Add.GetHashCode());
			UnregisterHotKey(this.handle, Current.GetHashCode());
			UnregisterHotKey(this.handle, Cycle.GetHashCode());
			UnregisterHotKey(this.handle, Next.GetHashCode());
			UnregisterHotKey(this.handle, Pause.GetHashCode());
			UnregisterHotKey(this.handle, Previous.GetHashCode());
			UnregisterHotKey(this.handle, VIPAdd.GetHashCode());
			UnregisterHotKey(this.handle, VolumeDown.GetHashCode());
			UnregisterHotKey(this.handle, VolumeUp.GetHashCode());
		}
	}
}
