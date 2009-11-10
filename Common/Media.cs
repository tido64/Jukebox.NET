using System.Data;

namespace Jukebox.NET.Common
{
	public class Media
	{
		public const string SQLiteCreateTable = "CREATE TABLE [media] (id INTEGER PRIMARY KEY, path TEXT UNIQUE, title TEXT, artist TEXT, audio_track INTEGER, lyrics TEXT)";
		public const string PrimaryKey = "id";

		public Media()
		{
			this.Id = -1;
			this.AudioTrack = 1;
			this.Artist = string.Empty;
			this.Lyrics = string.Empty;
			this.Path = string.Empty;
			this.Title = string.Empty;
		}

		public Media(DataRow dr)
		{
			this.Id = int.Parse(dr["id"].ToString());
			this.Artist = dr["artist"].ToString();
			//this.AudioTrack = (int)dr["audio_track"];
			this.Lyrics = dr["lyrics"].ToString();
			this.Path = dr["path"].ToString();
			this.Title = dr["title"].ToString();
		}

		public int Id { get; set; }
		public string Artist { get; set; }
		public int AudioTrack { get; set; }
		public string Lyrics { get; set; }
		public string Path { get; set; }
		public string RequestedBy { get; set; }
		public string Title { get; set; }

		public void ToRow(ref DataRow dr)
		{
			if (Id != -1)
				dr["id"] = this.Id.ToString();
			dr["artist"] = this.Artist;
			//dr["audio_track"] = this.AudioTrack.ToString();
			dr["lyrics"] = this.Lyrics;
			dr["path"] = this.Path;
			dr["title"] = this.Title;
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
