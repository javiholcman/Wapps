using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public class WEditor : Editor
	{
		#region Placeholder Property

		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create ("Placeholder", typeof(string), typeof(string), string.Empty);
		/// <summary>
		/// Gets or sets the placeholder.
		/// </summary>
		/// <value>The placeholder.</value>
		public string Placeholder 
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}

		#endregion

		public WEditor()
		{
		}
	}
}

