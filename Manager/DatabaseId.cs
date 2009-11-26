using System;
using System.Windows.Data;

using Jukebox.NET.Common;

namespace Jukebox.NET.Manager
{
	class DatabaseId : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value.ToString() == string.Empty)
				return value;

			long id = (long)value;
			return id + DatabaseManager.IdOffset;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			long id = (long)value;
			return id - DatabaseManager.IdOffset;
		}

		#endregion
	}
}
