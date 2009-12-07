using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Jukebox.NET.Manager
{
	/// <summary>
	/// Currently only notifies of updates.
	/// </summary>
	class AutoUpdate
	{
		private static void CheckOnline()
		{
			WebClient wc = new WebClient();
			wc.Credentials = CredentialCache.DefaultNetworkCredentials;
			wc.Encoding = Encoding.UTF8;
			string latest;
			try
			{
				latest = Encoding.UTF8.GetString(wc.DownloadData("http://www.haicongastudios.net/jukebox/latest")).Trim();
				if (latest != Assembly.GetExecutingAssembly().GetName().Version.ToString())
				{
					DialogResult res = MessageBox.Show("There is a newer version of Jukebox.NET for download!\nDo you want to download it?",
						"Jukebox.NET " + latest, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (res == DialogResult.Yes)
					{
						Process proc = new Process();
						proc.StartInfo.FileName = "http://www.haicongastudios.net/jukebox/";
						proc.Start();
					}
				}
			}
			catch { }
		}

		public static void Check()
		{
			Thread t = new Thread(new ThreadStart(CheckOnline));
			t.Start();
		}
	}
}
