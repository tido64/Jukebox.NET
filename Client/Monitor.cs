using System.Windows.Forms;

namespace Jukebox.NET.Client
{
	/// <summary>
	/// Helper class for assigning the second monitor (whenever present) as viewing screen.
	/// </summary>
	class Monitor
	{
		/// <summary>
		/// Gets the number of monitors attached to this computer.
		/// </summary>
		public static int Count
		{
			get
			{
				return Screen.AllScreens.Length;
			}
		}

		/// <summary>
		/// Gets the device name of the viewing screen.
		/// </summary>
		public static string DeviceName
		{
			get
			{
				if (Screen.AllScreens.Length > 1)
					return Screen.AllScreens[1].DeviceName;
				return Screen.PrimaryScreen.DeviceName;
			}
		}

		/// <summary>
		/// Gets the starting x-coordinate of the viewing screen.
		/// </summary>
		public static int BoundStart
		{
			get
			{
				if (Screen.AllScreens.Length > 1)
					return Screen.PrimaryScreen.Bounds.Width;
				return 0;
			}
		}

		/// <summary>
		/// Gets the height of the viewing screen.
		/// </summary>
		public static int Height
		{
			get
			{
				if (Screen.AllScreens.Length > 1)
					return Screen.AllScreens[1].Bounds.Height;
				return Screen.PrimaryScreen.Bounds.Height;
			}
		}

		/// <summary>
		/// Gets the width of the viewing screen.
		/// </summary>
		public static int Width
		{
			get
			{
				if (Screen.AllScreens.Length > 1)
					return Screen.AllScreens[1].Bounds.Width;
				return Screen.PrimaryScreen.Bounds.Width;
			}
		}
	}
}
