using System.Data;

namespace Jukebox.NET.Common
{
	public class Media
	{
		public const string SQLiteCreateTable = "CREATE TABLE [media] (id INTEGER PRIMARY KEY, path TEXT UNIQUE, title TEXT, artist TEXT, lyrics TEXT)";
		public const string PrimaryKey = "id";

		public Media()
		{
			Id = -1;
			Artist = string.Empty;
			Lyrics = string.Empty;
			Path = string.Empty;
			Title = string.Empty;
		}

		public Media(DataRow dr)
		{
			Id = int.Parse(dr["id"].ToString());
			Artist = dr["artist"].ToString();
			Lyrics = dr["lyrics"].ToString();
			Path = dr["path"].ToString();
			Title = dr["title"].ToString();
		}

		public int Id { get; set; }
		public string Artist { get; set; }
		public string Lyrics { get; set; }
		public string Path { get; set; }
		public string RequestedBy { get; set; }
		public string Title { get; set; }

		public void ToRow(ref DataRow dr)
		{
			if (Id != -1)
				dr["id"] = Id.ToString();
			dr["artist"] = Artist;
			dr["lyrics"] = Lyrics;
			dr["path"] = Path;
			dr["title"] = Title;
		}

		#region Overrided methods

		public override bool Equals(object obj)
		{
			return Path == ((Media)obj).Path;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			if (Artist == string.Empty)
				return Title;
			return Title + " (" + Artist + ")";
		}

		#endregion
	}
}
