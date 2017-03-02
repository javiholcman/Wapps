// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WCheckBox.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	/// <summary>
	/// The check box.
	/// </summary>
	public class WCheckBox : View
	{

		#region CheckedChanged Event

		/// <summary>
		/// The checked changed event.
		/// </summary>
		public event EventHandler<EventArgs<bool>> CheckedChanged;

		static void OnCheckedPropertyChanged(BindableObject bindable, bool oldvalue, bool newvalue)
		{
			var checkBox = (WCheckBox)bindable;
			checkBox.Checked = newvalue;
		}

		#endregion

		#region Checked Property

		public static readonly BindableProperty CheckedProperty = BindableProperty.Create<WCheckBox, bool>(p => p.Checked, false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);
		public bool Checked
		{
			get
			{
				return this.GetValue<bool>(CheckedProperty);
			}

			set
			{
				if (this.Checked != value) {
					this.SetValue(CheckedProperty, value);
					this.CheckedChanged.Invoke(this, value);
				}
			}
		}

		#endregion

		#region CheckedImage Property

		public static readonly BindableProperty CheckedImageProperty = BindableProperty.Create<WCheckBox, string>(p => p.CheckedImage, string.Empty, BindingMode.TwoWay);
		/// <summary>
		/// Gets or sets a value indicating the image checked.
		/// </summary>
		/// <value>The checked image.</value>
		/// <remarks>
		/// Overwrites the default image property if set when checkbox is checked.
		/// </remarks>
		public string CheckedImage
		{
			get { return this.GetValue<string>(CheckedImageProperty); }
			set { this.SetValue(CheckedImageProperty, value); }
		}

		#endregion

		#region UncheckedImage Property

		public static readonly BindableProperty UncheckedImageProperty = BindableProperty.Create<WCheckBox, string>(p => p.UncheckedImage, string.Empty, BindingMode.TwoWay);
		/// <summary>
		/// Gets or sets a value indicating the image unchecked.
		/// </summary>
		/// <value>The unchecked image.</value>
		/// <remarks>
		/// Overwrites the default image property if set when checkbox is unchecked.
		/// </remarks>
		public string UncheckedImage
		{
			get
			{
				return this.GetValue<string>(UncheckedImageProperty);
			}

			set
			{
				this.SetValue(UncheckedImageProperty, value);
			}
		}

		#endregion

		#region CheckedText Property

		public static readonly BindableProperty CheckedTextProperty = BindableProperty.Create<WCheckBox, string>(p => p.CheckedText, string.Empty, BindingMode.TwoWay);
		/// <summary>
		/// Gets or sets a value indicating the checked text.
		/// </summary>
		/// <value>The checked state.</value>
		/// <remarks>
		/// Overwrites the default text property if set when checkbox is checked.
		/// </remarks>
		public string CheckedText
		{
			get
			{
				return this.GetValue<string>(CheckedTextProperty);
			}

			set
			{
				this.SetValue(CheckedTextProperty, value);
			}
		}

		#endregion

		#region UncheckedText Property

		public static readonly BindableProperty UncheckedTextProperty = BindableProperty.Create<WCheckBox, string>(p => p.UncheckedText, string.Empty);
		/// <summary>
		/// Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>The checked state.</value>
		/// <remarks>
		/// Overwrites the default text property if set when checkbox is checked.
		/// </remarks>
		public string UncheckedText
		{
			get
			{
				return this.GetValue<string>(UncheckedTextProperty);
			}

			set
			{
				this.SetValue(UncheckedTextProperty, value);
			}
		}

		#endregion

		#region DefaultText Property

		public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create<WCheckBox, string>(p => p.Text, string.Empty);

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string DefaultText
		{
			get
			{
				return this.GetValue<string>(DefaultTextProperty);
			}

			set
			{
				this.SetValue(DefaultTextProperty, value);
			}
		}

		#endregion

		#region TextColor Property

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create<WCheckBox, Color>(p => p.TextColor, Color.Default);
		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		/// <value>The color of the text.</value>
		public Color TextColor
		{
			get
			{
				return this.GetValue<Color>(TextColorProperty);
			}

			set
			{
				this.SetValue(TextColorProperty, value);
			}
		}

		#endregion

		#region FontSize Property

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create<WCheckBox, double>(p => p.FontSize, -1);

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public double FontSize
		{
			get
			{
				return (double) GetValue(FontSizeProperty);
			}
			set
			{
				SetValue(FontSizeProperty, value);
			}
		}

		#endregion

		#region FontName Property

		public static readonly BindableProperty FontNameProperty = BindableProperty.Create<WCheckBox, string>(p => p.FontName, string.Empty);

		/// <summary>
		/// Gets or sets the name of the font.
		/// </summary>
		/// <value>The name of the font.</value>
		public string FontName
		{
			get
			{
				return (string) GetValue(FontNameProperty);
			}
			set
			{
				SetValue(FontNameProperty, value);
			}
		}

		#endregion

		#region Text Property

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return this.Checked
					? (string.IsNullOrEmpty(this.CheckedText) ? this.DefaultText : this.CheckedText)
						: (string.IsNullOrEmpty(this.UncheckedText) ? this.DefaultText : this.UncheckedText);
			}
		}

		#endregion

	}
}
