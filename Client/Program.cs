using System;
using System.Windows.Forms;

using Jukebox.NET.Common;

namespace Jukebox.NET.Client
{
	static class Program
	{
		public const string endl = "\r\n";
		private static Tray main = null;

		[STAThread]
		static void Main(string[] args)
		{
			using (Kryp2 kryp2 = new Kryp2())
			{
				if (!kryp2.is_valid())
					return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			main = new Tray();
			Application.Run();
		}
	}
}
