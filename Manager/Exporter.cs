using System.Collections;
using System.Collections.Generic;
using System.IO;

using Jukebox.NET.Common;

namespace Jukebox.NET.Manager
{
	sealed class Exporter
	{
		private const int constant = DatabaseManager.IdOffset;
		private const string css = "body { font-family: Arial, sans-serif; }", footer = "</table>\n</body>\n</html>\n";
		private readonly string header;
		private SortedList<string, Media> media;

		public Exporter(string title)
		{
			this.header = "<html>\n<head>\n	<meta charset=\"utf-8\">\n	<title>" + title + "</title>\n	<style>" + css + "</style>\n</head>\n<body>\n<table>\n";
			this.media = new SortedList<string, Media>();
		}

		public void Add(Media m)
		{
			m.Id += constant;
			this.media.Add(m.ToString() + m.Id.ToString(), m);
		}

		public void WriteHTML(string filename)
		{
			string body = string.Empty;
			using (IEnumerator<KeyValuePair<string, Media>> m = this.media.GetEnumerator())
			{
				while (m.MoveNext())
				{
					body += "<tr><td>" + m.Current.Value.Id + ".</td><td>" + m.Current.Value.Title;
					if (m.Current.Value.Artist == string.Empty)
						body += "</td></tr>\n";
					else
						body += " (" + m.Current.Value.Artist + ")</td></tr>\n";
				}
			}
			using (TextWriter tw = new StreamWriter(filename))
			{
				tw.Write(this.header + body + footer);
				tw.Close();
			}
		}
	}
}
