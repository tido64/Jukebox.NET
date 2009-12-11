using System.Xml.Serialization;

namespace Jukebox.NET
{
	[XmlRoot("config")]
	public class Config
	{
		public Config()
		{
			this.Player = "MPlayer";
			this.Path = "mplayer.exe";
			this.OSD = new OnScreenDisplay();
			this.Font = new FontFace();
		}

		[XmlElement("player", IsNullable=false)]
		public string Player { get; set; }

		[XmlElement("path", IsNullable=false)]
		public string Path { get; set; }

		[XmlElement("osd", IsNullable=false)]
		public OnScreenDisplay OSD { get; set; }

		[XmlElement("font")]
		public FontFace Font { get; set; }

		[XmlElement("drive")]
		public string Drive { get; set; }

		public class FontFace
		{
			public FontFace()
			{
				this.Size = 48;
				this.Face = "Segoe UI";
			}

			[XmlAttribute("size")]
			public double Size { get; set; }

			[XmlText()]
			public string Face { get; set; }
		}

		public class OnScreenDisplay
		{
			public OnScreenDisplay()
			{
				this.Interval = 60;
				this.Media = 15;
				this.Requested = 5;
			}

			[XmlAttribute("interval")]
			public int Interval { get; set; }

			[XmlAttribute("media")]
			public int Media { get; set; }

			[XmlAttribute("requested")]
			public int Requested { get; set; }
		}
	}
}
