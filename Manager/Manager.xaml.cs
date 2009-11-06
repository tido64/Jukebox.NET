using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;

using Jukebox.NET.Common;

namespace Jukebox.NET.Manager
{
	/// <summary>
	/// Interaction logic for Manager.xaml
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/aa970683.aspx</remarks>
	public partial class Manager : Window
	{
		#region Code-behind

		readonly string exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "medialist.html");
		readonly string[] separators = new string[3] { " - ", " -- ", " _ " };
		string filter;

		/// <summary>
		/// Ask the user to save changes when he closes the window.
		/// </summary>
		private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (DatabaseManager.Instance.DataSet.HasChanges())
				if (MessageBox.Show("Do you want to save?", Jukebox.NET.Manager.App.ResourceAssembly.GetName().Name, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					Save(sender, null);
			if (this.textBox_filter.Text != this.filter)
			{
				TextWriter tw = File.CreateText("search.pattern");
				tw.Write(this.textBox_filter.Text.Trim());
				tw.Close();
			}
		}

		/// <summary>
		/// Loads filter patterns. Creates the file if it doesn't exist.
		/// </summary>
		private void LoadFilter()
		{
			try
			{
				TextReader tr = new StreamReader("search.pattern");
				this.textBox_filter.Text = tr.ReadLine().Trim();
				tr.Close();
				this.filter = this.textBox_filter.Text;
			}
			catch
			{
				TextWriter tw = File.CreateText("search.pattern");
				this.filter = "*.mkv;*.mp3;*.ogm;*.vob";
				tw.Write(this.filter);
				this.textBox_filter.Text = this.filter;
				tw.Close();
			}
		}

		/// <summary>
		/// Parses file for artist and title string.
		/// </summary>
		/// <param name="filepath">Path to the file to parse</param>
		/// <returns>A media object representing the file</returns>
		private Media Parse(string filePath)
		{
			string filename = Path.GetFileNameWithoutExtension(filePath);
			string[] names = filename.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);

			Media m = new Media();
			m.Path = filePath;
			if (names.Length < 2)
				m.Title = filename;
			else
			{
				m.Title = names[0].Trim();
				m.Artist = names[1].Trim();
				for (int i = 2; i < names.Length; i++)
					m.Artist += separators[0] + names[i].Trim();
			}
			return m;
		}

		/// <summary>
		/// Scans a directory for media and adds them to the database.
		/// </summary>
		private void Scan(IList<Media> media, string path)
		{
			string[] patterns = this.textBox_filter.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			DirectoryInfo root = new DirectoryInfo(path);
			foreach (string pattern in patterns)
				foreach (FileInfo fi in root.GetFiles(pattern, SearchOption.AllDirectories))
					media.Add(Parse(fi.FullName));
		}

		#endregion

		#region Interaction

		private void Export(object sender, RoutedEventArgs e)
		{
			if (DatabaseManager.Instance.DataSet.HasChanges())
			{
				MessageBox.Show("The database contains changes. Please save or undo them in order to perform an export.",
					"Please save or undo changes",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				return;
			}

			Exporter ex = new Exporter(Application.ResourceAssembly.GetName().Name);
			if (this.dataGrid.SelectedItems.Count > 1)
				foreach (DataRowView r in this.dataGrid.SelectedItems)
					ex.Add(new Media(r.Row));
			else
				foreach (DataRowView r in this.dataGrid.Items)
					ex.Add(new Media(r.Row));

			ex.WriteHTML(this.exportPath);
			using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
			{
				proc.StartInfo.FileName = this.exportPath;
				proc.Start();
			}
		}

		private void Load(object sender, RoutedEventArgs e)
        {
			if (this.textBox_filter.Text == string.Empty)
				this.LoadFilter();

			if (DatabaseManager.Instance.DataSet.Tables[0].Rows.Count == 0)
			    this.Scan(sender, e);

			this.DataContext = DatabaseManager.Instance.DataSet.Tables[0].DefaultView;
		}

		private void Refresh(object sender, RoutedEventArgs e)
		{
			DatabaseManager.Instance.Load();
			this.Load(sender, e);
		}

		/// <summary>
		/// Commits any changes to the database. Will try to rename the file to
		/// "title -- artist" (or "title" if the artist field is empty)
		/// whenever a row has been modified in any way.
		/// </summary>
		private void Save(object sender, RoutedEventArgs e)
		{
			if (!DatabaseManager.Instance.DataSet.HasChanges())
				return;

			// Identify any new/modified rows and automatically rename files.
			foreach (DataRow dr in DatabaseManager.Instance.DataSet.Tables[0].Rows)
			{
				if (dr.RowState != DataRowState.Modified)
					continue;

				string path = dr["path"].ToString();
				if (!File.Exists(path))
					continue;

				string filename = Path.GetFileNameWithoutExtension(path);
				string newPath = string.Empty;

				if (dr["artist"].ToString().Trim() != string.Empty)
				{
					if (filename != dr["title"].ToString() + " -- " + dr["artist"].ToString())
						newPath = Path.Combine(Path.GetDirectoryName(path), dr["title"].ToString() + " -- " + dr["artist"].ToString() + Path.GetExtension(path));
				}
				else if (filename != dr["title"].ToString())
					newPath = Path.Combine(Path.GetDirectoryName(path), dr["title"].ToString() + Path.GetExtension(path));

				if (newPath != string.Empty)
				{
					File.Move(path, newPath);
					dr["path"] = newPath;
				}
			}

			int rows = DatabaseManager.Instance.Commit();
			if (rows == 0)
				MessageBox.Show(DatabaseManager.Instance.LastMessage, "An error has occured while updating the database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			else
				MessageBox.Show("Committed " + rows.ToString() + " rows.", "Database updated", MessageBoxButton.OK, MessageBoxImage.Information);
			this.Refresh(sender, e);
		}

		private void Scan(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
			fbd.Description = "Choose a directory to add to the database:";
			fbd.ShowNewFolderButton = false;
			System.Windows.Forms.DialogResult dr = fbd.ShowDialog();

			if (dr != System.Windows.Forms.DialogResult.OK)
				return;

			List<Media> media = new List<Media>();
			this.Scan(media, fbd.SelectedPath);
			DatabaseManager.Instance.DataSet.Tables[0].BeginLoadData();
			foreach (Media m in media)
			{
				DataRow r = DatabaseManager.Instance.DataSet.Tables[0].NewRow();
				m.ToRow(ref r);
				DatabaseManager.Instance.DataSet.Tables[0].Rows.Add(r);
			}
			DatabaseManager.Instance.DataSet.Tables[0].EndLoadData();
			media.Clear();
		}

		private void Switch(object sender, RoutedEventArgs e)
		{
			string tmp;
			foreach (DataRowView r in this.dataGrid.SelectedItems)
			{
				tmp = r.Row["artist"].ToString();
				r.Row["artist"] = r.Row["title"];
				r.Row["title"] = tmp;
			}
		}

		#endregion
	}
}
