using System.IO;
using System.Reflection;
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
			Config = (Config)xs.Deserialize(xtr);
			xtr.Close();
			if (!File.Exists(Config.Path))
				Application.Current.Shutdown(1);

			AssemblyName asm = Assembly.GetExecutingAssembly().GetName();
			Name = asm.Name;
			NameAndVersion = asm.Name + " v" + asm.Version.Major + "." + asm.Version.Minor;
		}

		public static Config Config { get; private set; }
		public static string Name { get; private set; }
		public static string NameAndVersion { get; private set; }
	}
}
