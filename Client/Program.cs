//#define CONSOLE

using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Jukebox.NET.Common;

namespace Jukebox.NET.Client
{
	static class Program
	{
		public const string endl = "\r\n";
#if (CONSOLE)
		private static Form main = null;
#else
		private static Tray main = null;
#endif

		[STAThread]
		static void Main(string[] args)
		{
			//using (Kryp2 kryp2 = new Kryp2())
			//{
			//    if (!kryp2.is_valid())
			//        return;
			//}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

#if (CONSOLE)
			main = new Console();

			SingleInstance application = new SingleInstance();
			application.StartupNextInstance += new StartupNextInstanceEventHandler(application_StartupNextInstance);
			application.Run(args);
#else
			main = new Tray();
			Application.Run();
#endif
		}

#if (CONSOLE)
		static void application_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
			if (main.WindowState == FormWindowState.Minimized)
				main.WindowState = FormWindowState.Normal;
			main.Activate();
		}

		private class SingleInstance : WindowsFormsApplicationBase
		{
			public SingleInstance()
			{
				this.IsSingleInstance = true;
				this.EnableVisualStyles = true;
				this.ShutdownStyle = Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses;
			}

			protected override void OnCreateMainForm()
			{
				this.MainForm = main;
			}
		}
#endif
	}
}
