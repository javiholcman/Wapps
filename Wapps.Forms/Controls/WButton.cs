using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public class WButton : Button
	{
		#region Property: Padding

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty PaddingProperty = BindableProperty.Create("Padding", typeof(Thickness), typeof(WButton), new Thickness ());

		/// <summary>
		/// Gets or sets the Padding
		/// </summary>
		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}

		#endregion

		#region Property: HorizontalTextAlignment

		/// <summary>
		/// The XA lign property.
		/// </summary>
		public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(TextAlignment), typeof(WButton), TextAlignment.Start);

		/// <summary>
		/// Gets or sets the horizontal text alignment.
		/// </summary>
		/// <value>The horizontal text alignment.</value>
		public TextAlignment HorizontalTextAlignment
		{
			get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
			set { SetValue(HorizontalTextAlignmentProperty, value); }
		}

		#endregion

		public WButton()
		{
		}
	}
}
