using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public class WStackLayout : StackLayout
	{
		#region Property: BorderRadius

		public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create("BorderRadius", typeof(int), typeof(WStackLayout), 0);

		/// <summary>
		/// Gets or sets the Lines
		/// </summary>
		public int BorderRadius
		{
			get { return (int)GetValue(BorderRadiusProperty); }
			set { SetValue(BorderRadiusProperty, value); }
		}

		#endregion

		#region Property: BorderColor

		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(WStackLayout), Color.Transparent);

		/// <summary>
		/// Gets or sets the Lines
		/// </summary>
		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		#endregion

		#region Property: BorderWidth

		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(WStackLayout), 0);

		/// <summary>
		/// Gets or sets the Lines
		/// </summary>
		public int BorderWidth
		{
			get { return (int)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		#endregion

		public WStackLayout()
		{
		}
	}
}
