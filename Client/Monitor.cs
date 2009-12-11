using System;
using System.Management;
using System.Windows.Forms;

namespace Jukebox.NET
{
	/// <summary>
	/// Helper class for assigning the second monitor (whenever present) as viewing screen.
	/// </summary>
	class Monitor
	{
		private static uint adapter_ram;

		public static uint AdapterRAM
		{
			get
			{
				if (adapter_ram == 0)
				{
					ManagementClass mc = new ManagementClass("Win32_VideoController");
					ManagementObjectCollection moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
						adapter_ram = Math.Max((UInt32)mo["AdapterRAM"], adapter_ram);
					adapter_ram /= 1048576;
				}
				return adapter_ram;
			}
		}

		/// <summary>
		/// Gets the number of monitors attached to this computer.
		/// </summary>
		public static int Count
		{
			get { return Screen.AllScreens.Length; }
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
