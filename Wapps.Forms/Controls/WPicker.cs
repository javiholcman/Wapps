using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public class WPicker : Picker
	{

		#region Property: Font

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontProperty = BindableProperty.Create("Font", typeof(Font), typeof(WPicker), new Font());

		/// <summary>
		/// Gets or sets the Font
		/// </summary>
		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		#endregion

		#region Property: FontSize

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(WPicker), (double)14);

		/// <summary>
		/// Gets or sets the Font
		/// </summary>
		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

		#endregion

		#region Property: HorizontalTextAlignment

		/// <summary>
		/// The XA lign property.
		/// </summary>
		public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(TextAlignment), typeof(WPicker), TextAlignment.Start);

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

		#region Property: HasBorder

		/// <summary>
		/// The has border property.
		/// </summary>
		public static readonly BindableProperty HasBorderProperty = BindableProperty.Create("HasBorder", typeof(bool), typeof(WPicker), true);

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Wapps.Forms.Controls.WPicker"/> has border.
		/// </summary>
		/// <value><c>true</c> if has border; otherwise, <c>false</c>.</value>
		public bool HasBorder
		{
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		#endregion

		#region Property: Placeholder

		/// <summary>
		/// The placeholder property.
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(WPicker), "");

		/// <summary>
		/// Gets or sets the placeholder.
		/// </summary>
		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}

		#endregion

		#region Property: PlaceholderTextColor

		/// <summary>
		/// The placeholder text color property.
		/// </summary>
		public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(WPicker), Color.Default);

		/// <summary>
		/// Gets or sets the color of the placeholder text.
		/// </summary>
		/// <value>The color of the placeholder text.</value>
		public Color PlaceholderColor
		{
			get { return (Color)GetValue(PlaceholderColorProperty); }
			set { SetValue(PlaceholderColorProperty, value); }
		}

		#endregion

		#region Property: SelectedItem 

		public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(WPicker), null);
		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set
			{
				if (value == null)
					SelectedIndex = -1;
				else
					SelectedIndex = Items.IndexOf(value.ToString());

				SetValue(SelectedItemProperty, value);
			}
		}

		#endregion

		#region Property BindableItems

		public static readonly BindableProperty BindableItemsProperty = BindableProperty.Create("BindableItems", typeof(IList<string>), typeof(WPicker), new List<string> ());

		public IList<string> BindableItems 
		{
			get { return (IList<string>)GetValue(BindableItemsProperty); }
			set
			{
				SetValue(BindableItemsProperty, value);
				base.Items.Clear();
				foreach (var val in value)
				{
					base.Items.Add(val);
				}
			}
		}

		#endregion

		public WPicker()
		{
			FontSize = 15;	
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			if (Parent != null)
			{
				SelectedIndexChanged += TNPicker_SelectedIndexChanged;
			}
			else
			{
				SelectedIndexChanged -= TNPicker_SelectedIndexChanged;
			}
		}

		void TNPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedIndex == -1)
				SetValue(SelectedItemProperty, null);
			else
				SetValue(SelectedItemProperty, Items[SelectedIndex]);
		}
	}
}
