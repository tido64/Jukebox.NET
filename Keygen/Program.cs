using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;

using Jukebox.NET.Common;

namespace Jukebox.NET.Keygen
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (TextWriter tw = new StreamWriter(Kryp2.license_file))
			{
				Kryp2 kryp = new Kryp2();
				ProcessStartInfo self_destruct = new ProcessStartInfo("cmd.exe", "/C CHOICE /C Y /N /T 2 /D Y & DEL \"" + System.Windows.Forms.Application.ExecutablePath + "\"");
				self_destruct.CreateNoWindow = true;
				self_destruct.WindowStyle = ProcessWindowStyle.Hidden;
				tw.Write(kryp.retrieve_unique().TrimEnd('='));
				Process.Start(self_destruct);
				tw.Close();
			}
		}
	}
}
