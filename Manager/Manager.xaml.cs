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
		readonly string exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "medialist.html");
		readonly string[] separators = new string[3] { " - ", " -- ", " _ " };
		string filter;

		#region Events

		private void Load(object sender, RoutedEventArgs e)
		{
			if (this.textBox_filter.Text == string.Empty)
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

			this.textBox_search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(StartSearch);

			if (DatabaseManager.Instance.DataTable.Rows.Count == 0)
				this.Scan(sender, e);

			this.RefreshView(sender, e);
		}

		/// <summary>
		/// Ask the user to save changes when he closes the window.
		/// </summary>
		private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (DatabaseManager.Instance.HasChanges)
				if (MessageBox.Show("Do you want to save?", Jukebox.NET.Manager.App.ResourceAssembly.GetName().Name, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					Save(sender, null);
			if (this.textBox_filter.Text != this.filter)
			{
				TextWriter tw = File.CreateText("search.pattern");
				tw.Write(this.textBox_filter.Text.Trim());
				tw.Close();
			}
		}

		private void StartSearch(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			DatabaseManager.Instance.DataTable.DefaultView.RowFilter = string.Format("title LIKE '%{0}%'", this.textBox_search.Text);
		}

		#endregion

		#region Buttons

		private void CleanUp(object sender, RoutedEventArgs e)
		{
			try
			{
				DatabaseManager.Instance.Execute("ALTER TABLE [media] ADD " + MediaTable.AltAudio + " INTEGER");
				DatabaseManager.Instance.Load();
			}
			catch { }
			foreach (DataRow row in DatabaseManager.Instance.DataTable.Rows)
			{
				string p = row[MediaTable.Path].ToString();
				if (p.ToLower().Contains("track 1"))
				{
					row[MediaTable.AltAudio] = 1;
					row[MediaTable.Path] = p.Replace("\\Track 1", "");
				}
				else
				{
					row[MediaTable.AltAudio] = 0;
					row[MediaTable.Path] = p.Replace("\\Track 0", "").Replace("_track 0", "");
				}
			}
			DatabaseManager.Instance.Commit();
		}

		private void Export(object sender, RoutedEventArgs e)
		{
			if (DatabaseManager.Instance.HasChanges)
			{
				MessageBox.Show("The database contains changes. Please save or undo them in order to perform an export.",
					"Please save or undo changes",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				return;
			}

			Exporter ex = new Exporter(Application.ResourceAssembly.GetName().Name);
			if (this.DataGrid.SelectedItems.Count > 1)
				foreach (DataRowView r in this.DataGrid.SelectedItems)
					ex.Add(new Media(r.Row));
			else
				foreach (DataRowView r in this.DataGrid.Items)
					ex.Add(new Media(r.Row));

			ex.WriteHTML(this.exportPath);
			using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
			{
				proc.StartInfo.FileName = this.exportPath;
				proc.Start();
			}
		}

		/// <summary>
		/// Commits any changes to the database.
		/// </summary>
		private void Save(object sender, RoutedEventArgs e)
		{
			if (!DatabaseManager.Instance.HasChanges)
				return;

			int rows = DatabaseManager.Instance.Commit();
			string error = DatabaseManager.Instance.LastMessage;
			if (error != string.Empty)
				MessageBox.Show(error, "An error has occured while updating the database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			else
				MessageBox.Show("Committed " + rows.ToString() + " rows.", "Database updated", MessageBoxButton.OK, MessageBoxImage.Information);
			this.RefreshView(sender, e);
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
			DatabaseManager.Instance.DataTable.BeginLoadData();
			foreach (Media m in media)
			{
				DataRow r = DatabaseManager.Instance.DataTable.NewRow();
				m.ToRow(ref r);
				DatabaseManager.Instance.DataTable.Rows.Add(r);
			}
			DatabaseManager.Instance.DataTable.EndLoadData();
			media.Clear();
		}

		private void Switch(object sender, RoutedEventArgs e)
		{
			foreach (DataRowView r in this.DataGrid.SelectedItems)
			{
				string tmp = r.Row["artist"].ToString();
				r.Row["artist"] = r.Row["title"];
				r.Row["title"] = tmp;
			}
		}

		#endregion

		#region Internal

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

		private void RefreshView(object sender, RoutedEventArgs e)
		{
			DatabaseManager.Instance.Load();
			this.DataContext = DatabaseManager.Instance.DataTable.DefaultView;
			this.Status.Text = string.Format("Total: {0}", DatabaseManager.Instance.DataTable.Rows.Count);
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
	}
}
