using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jukebox.NET.Client
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
		public const Keys
			Add = Keys.Add,			// Try and add the current id
			Current = Keys.F5,		// Show currently playing
			Cycle = Keys.F10,		// Cycle audio tracks
			Next = Keys.F9,			// Advance in playlist
			Pause = Keys.F8,		// Pause
			Previous = Keys.F7,		// Regress in playlist
			VIPAdd = Keys.F6,		// Try and add the current id (VIP)
			VolumeDown = Keys.F11,	// Decrease volume
			VolumeUp = Keys.F12;	// Increase volume

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
		/// <param name="hWnd">Main form</param>
		public HotKeys(IntPtr hWnd)
		{
			this.handle = hWnd;

			// Register numeric keys
			RegisterHotKey(this.handle, 0, 0, (int)Keys.NumPad0);
			RegisterHotKey(this.handle, 1, 0, (int)Keys.NumPad1);
			RegisterHotKey(this.handle, 2, 0, (int)Keys.NumPad2);
			RegisterHotKey(this.handle, 3, 0, (int)Keys.NumPad3);
			RegisterHotKey(this.handle, 4, 0, (int)Keys.NumPad4);
			RegisterHotKey(this.handle, 5, 0, (int)Keys.NumPad5);
			RegisterHotKey(this.handle, 6, 0, (int)Keys.NumPad6);
			RegisterHotKey(this.handle, 7, 0, (int)Keys.NumPad7);
			RegisterHotKey(this.handle, 8, 0, (int)Keys.NumPad8);
			RegisterHotKey(this.handle, 9, 0, (int)Keys.NumPad9);

			// Register hot keys
			RegisterHotKey(this.handle, Add.GetHashCode(), 0, (int)Add);
			RegisterHotKey(this.handle, Current.GetHashCode(), (int)MOD, (int)Current);
			RegisterHotKey(this.handle, Cycle.GetHashCode(), (int)MOD, (int)Cycle);
			RegisterHotKey(this.handle, Next.GetHashCode(), (int)MOD, (int)Next);
			RegisterHotKey(this.handle, Pause.GetHashCode(), (int)MOD, (int)Pause);
			RegisterHotKey(this.handle, Previous.GetHashCode(), (int)MOD, (int)Previous);
			RegisterHotKey(this.handle, VIPAdd.GetHashCode(), (int)MOD, (int)VIPAdd);
			RegisterHotKey(this.handle, VolumeDown.GetHashCode(), (int)MOD, (int)VolumeDown);
			RegisterHotKey(this.handle, VolumeUp.GetHashCode(), (int)MOD, (int)VolumeUp);
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
