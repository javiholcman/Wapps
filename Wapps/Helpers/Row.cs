using System;
using System.Collections.Generic;
using Wapps.ViewModels;

namespace Wapps
{
	public class Row <T> : ObservableObject
	{
		public T Content { get; private set; }

		bool _selected;
		public bool Selected 
		{ 
			get { return _selected; } 
			set { _selected = value; NotifyPropertyChanged("Selected"); }
		}

		public Row(T content)
		{
			Content = content;
		}

		public static List<Row<T>> ToRowList(List<T> source)
		{
			var rows = new List<Row<T>>();
			foreach (var item in source)
			{
				rows.Add(new Row<T>(item));
			}
			return rows;
		}

		public static List<T> ToSourceList(List<Row<T>> rows)
		{
			var source = new List<T>();
			foreach (var row in rows)
			{
				source.Add(row.Content);
			}
			return source;
		}
	}
}
