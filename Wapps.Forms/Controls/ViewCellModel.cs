using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wapps.Forms
{
	public class WViewCellModel : INotifyPropertyChanged
	{

		Dictionary<string, object> _values;

		public event PropertyChangedEventHandler PropertyChanged;

		public WViewCellModel()
		{
			_values = new Dictionary<string, object>();
		}

		protected void SetValue(string property, object value)
		{
			object oldValue = null;
			if (_values.ContainsKey(property))
			{
				oldValue = _values[property];
			}
			_values[property] = value;
			if (oldValue != value)
			{
				Changed (property);
			}
		}

		protected T GetValue<T>(string property, T defaultValue = default(T))
		{
			if (!_values.ContainsKey(property))
			{
				_values[property] = defaultValue;
			}
			return (T)_values[property];
		}

		protected void Changed(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged (this, new PropertyChangedEventArgs(property));
			}
		}

		/*
		 Sample of bindeable property
		const string ImageLikeProperty = "ImageLike";
		public string ImageLike
		{
			get { return GetValue<string>(ImageLikeProperty, ""); }
			set { SetValue(ImageLikeProperty, value); }
		}
		*/
	}
}
