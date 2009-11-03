// Resources: http://msdn.microsoft.com/en-us/library/bb387004.aspx

using System.Data;
using System.Data.SQLite;

namespace Jukebox.NET.Common
{
	public sealed class DatabaseManager
	{
		public const int IdOffset = 999;

		#region Private members

		private static readonly DatabaseManager instance = new DatabaseManager();

		private object db_lock;
		private string db_last_msg;
		private const string sql_conn_str = "Data Source=media.sdb;Version=3;Compress=True;";
		private const string sql_adapter_str = "SELECT * FROM [media]";
		private DataSet db_dataset;
		private SQLiteDataAdapter sql_adapter;
		private SQLiteConnection sql_conn;

		#endregion

		#region Singleton pattern

		private DatabaseManager()
		{
			this.db_last_msg = string.Empty;
			this.db_lock = 0;
			this.sql_conn = new SQLiteConnection(sql_conn_str);
			this.sql_adapter = new SQLiteDataAdapter(sql_adapter_str, this.sql_conn);
			this.sql_adapter.ContinueUpdateOnError = true; // Ignore errors on failed row update

			// Create SQLite command builder for data adapter
			SQLiteFactory sql_factory = new SQLiteFactory();
			SQLiteCommandBuilder sql_builder = (SQLiteCommandBuilder)sql_factory.CreateCommandBuilder();
			sql_builder.SetAllValues = false;
			sql_builder.DataAdapter = this.sql_adapter;

			this.sql_conn.Open();
		}

		~DatabaseManager()
		{
			this.sql_conn.Close();
		}

		public static DatabaseManager Instance
		{
			get { return instance; }
		}

		#endregion

		/// <summary>
		/// Facility to fetch the last error message.
		/// </summary>
		public string LastMessage
		{
			get
			{
				string tmp = this.db_last_msg;
				this.db_last_msg = string.Empty;
				return tmp;
			}
		}

		/// <summary>
		/// Commits any changes made to the database.
		/// </summary>
		/// <returns>The number of rows affected.</returns>
		public int Commit()
		{
			int rows = 0;
			lock (this.db_lock)
			{
				try
				{
					using (SQLiteTransaction transaction = this.sql_conn.BeginTransaction())
					{
						rows = this.sql_adapter.Update(this.db_dataset);
						transaction.Commit();
					}
				}
				catch (SQLiteException e)
				{
					this.db_last_msg = "SQLite internal message:\n" + e.Message.Replace("\r\n", "; ") + ".";
				}
			}
			return rows;
		}

		public DataSet DataSet
		{
			get
			{
				if (this.db_dataset == null)
					Load();
				return this.db_dataset;
			}
		}

		/// <summary>
		/// Fetches media given an id.
		/// </summary>
		/// <param name="id">The media to fetch</param>
		/// <returns>The media with given id</returns>
		public Media FetchById(int id)
		{
			id -= IdOffset;
			DataRow dr = this.db_dataset.Tables[0].Rows.Find(id);
			return new Media(dr);
		}

		/// <summary>
		/// Opens a connection to the database and populates the dataset.
		/// </summary>
		public void Load()
		{
			lock (this.db_lock)
			{
				if (this.db_dataset != null)
					this.db_dataset.Dispose();
				this.db_dataset = new DataSet("media");
				try
				{
					this.sql_adapter.Fill(this.db_dataset);
				}
				catch // database is most likely empty: create the necessary tables
				{
					using (SQLiteTransaction sql_transaction = this.sql_conn.BeginTransaction())
					using (SQLiteCommand sql_cmd = this.sql_conn.CreateCommand())
					{
						sql_cmd.Transaction = sql_transaction;
						sql_cmd.CommandText = Media.SQLiteCreateTable;
						sql_cmd.ExecuteNonQuery();
						sql_transaction.Commit();
					}
					this.sql_adapter.Fill(this.db_dataset);
				}
				this.db_dataset.Tables[0].PrimaryKey = new DataColumn[]{ this.db_dataset.Tables[0].Columns[Media.PrimaryKey] };
			}
		}
	}
}