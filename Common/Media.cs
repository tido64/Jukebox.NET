using System.Data;

namespace Jukebox.NET.Common
{
	public class Media
	{
		public static string SQLiteCreateTable = string.Format("CREATE TABLE [media] ({0} INTEGER PRIMARY KEY, {1} TEXT UNIQUE, {2} TEXT, {3} TEXT, {4} INTEGER, {5} TEXT)",
			MediaTable.Id, MediaTable.Path, MediaTable.Title, MediaTable.Artist, MediaTable.AltAudio, MediaTable.Lyrics);
		public const string PrimaryKey = MediaTable.Id;

		public Media()
		{
			this.Id = -1;
			this.Artist = string.Empty;
			this.Lyrics = string.Empty;
			this.Path = string.Empty;
			this.Title = string.Empty;
		}

		public Media(DataRow dr)
		{
			this.Id = int.Parse(dr[MediaTable.Id].ToString());
			this.AltAudio = int.Parse(dr[MediaTable.AltAudio].ToString()) > 0 ? true : false;
			this.Artist = dr[MediaTable.Artist].ToString();
			this.Lyrics = dr[MediaTable.Lyrics].ToString();
			this.Path = dr[MediaTable.Path].ToString();
			this.Title = dr[MediaTable.Title].ToString();
		}

		public int Id { get; set; }
		public bool AltAudio { get; set; }
		public string Artist { get; set; }
		public string Lyrics { get; set; }
		public string Path { get; set; }
		public string RequestedBy { get; set; }
		public string Title { get; set; }

		public void ToRow(ref DataRow dr)
		{
			if (Id != -1)
				dr[MediaTable.Id] = this.Id.ToString();
			dr[MediaTable.AltAudio] = this.AltAudio ? 1 : 0;
			dr[MediaTable.Artist] = this.Artist;
			dr[MediaTable.Lyrics] = this.Lyrics;
			dr[MediaTable.Path] = this.Path;
			dr[MediaTable.Title] = this.Title;
		}

		#region Overrided methods

		public override bool Equals(object obj)
		{
			return this.Path == ((Media)obj).Path;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			if (this.Artist == string.Empty)
				return this.Title;
			return this.Title + " (" + this.Artist + ")";
		}

		#endregion
	}
}
