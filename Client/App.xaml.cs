using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

using Jukebox.NET.Common;

namespace Jukebox.NET
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			using (Kryp2 kryp2 = new Kryp2())
			{
				if (!kryp2.is_valid())
					Application.Current.Shutdown();
			}

			XmlSerializer xs = new XmlSerializer(typeof(Config));
			XmlTextReader xtr = new XmlTextReader("config.xml");
			try
			{
				Config = (Config)xs.Deserialize(xtr);
				xtr.Close();
			}
			catch
			{
				xtr.Close();
				Application.Current.Shutdown(1);
			}
			if (!MediaPlayer.MediaPlayerFactory.CanCreate(Config.Player) || !File.Exists(Config.Path))
				Application.Current.Shutdown(1);
			if (App.Config.Drive.Length != 1)
				App.Config.Drive = null;

			AssemblyName asm = Assembly.GetExecutingAssembly().GetName();
			Name = asm.Name;
			NameAndVersion = asm.Name + " v" + asm.Version.Major + "." + asm.Version.Minor;
			this.Exit += new ExitEventHandler(App_Exit);
		}

		public static Config Config { get; private set; }
		public static string Name { get; private set; }
		public static string NameAndVersion { get; private set; }

		void App_Exit(object sender, ExitEventArgs e)
		{
			if (Config.Changed)
			{
				XmlSerializer xs = new XmlSerializer(typeof(Config));
				XmlTextWriter xtw = new XmlTextWriter("config.xml", Encoding.UTF8);
				xtw.Indentation = 1;
				xtw.IndentChar = '	';
				xtw.WriteStartDocument(true);
				XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
				xsn.Add(string.Empty, string.Empty);
				xs.Serialize(xtw, Config, xsn);
				xtw.Close();
			}
		}
	}
}
