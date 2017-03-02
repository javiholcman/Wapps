using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WPicker), typeof(WPickerRenderer))]

namespace Wapps.Forms.IOS.Controls
{
	public class WPickerRenderer : PickerRenderer
	{
		public WPickerRenderer()
		{
		}

		/// <summary>
		/// The on element changed callback.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			var view = e.NewElement as WPicker;

			if (view != null)
			{
				SetFont(view);
				SetHorizontalTextAlignment(view);
				SetBorder(view);
				SetPlaceholder(view);
				SetPlaceholderColor(view);
			}
		}

		/// <summary>
		/// The on element property changed callback
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			var view = Element as WPicker;

			if (e.PropertyName == WPicker.FontProperty.PropertyName || e.PropertyName == WPicker.FontSizeProperty.PropertyName)
				SetFont(view);
			else if (e.PropertyName == WPicker.HorizontalTextAlignmentProperty.PropertyName)
				SetHorizontalTextAlignment(view);
			else if (e.PropertyName == WPicker.HasBorderProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WPicker.PlaceholderColorProperty.PropertyName)
				SetPlaceholderColor(view);
			else if (e.PropertyName == WPicker.PlaceholderProperty.PropertyName)
				SetPlaceholder(view);
		}

		/// <summary>
		/// Sets the text alignment.
		/// </summary>
		/// <param name="view">The view.</param>
		void SetHorizontalTextAlignment(WPicker view)
		{
			switch (view.HorizontalTextAlignment)
			{
				case TextAlignment.Center:
					Control.TextAlignment = UITextAlignment.Center;
					break;
				case TextAlignment.End:
					Control.TextAlignment = UITextAlignment.Right;
					break;
				case TextAlignment.Start:
					Control.TextAlignment = UITextAlignment.Left;
					break;
			}
		}

		/// <summary>
		/// Sets the font.
		/// </summary>
		/// <param name="view">The view.</param>
		void SetFont(WPicker view)
		{
			Control.Font = view.Font.WithSize(view.FontSize).ToUIFont();
		}

		/// <summary>
		/// Sets the border.
		/// </summary>
		/// <param name="view">The view.</param>
		void SetBorder(WPicker view)
		{
			Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
		}

		/// <summary>
		/// Sets the placeholder.
		/// </summary>
		void SetPlaceholder(WPicker view)
		{
			Control.Placeholder = view.Placeholder;
		}

		/// <summary>
		/// Sets the color of the placeholder text.
		/// </summary>
		/// <param name="view">The view.</param>
		void SetPlaceholderColor(WPicker view)
		{
			if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderColor != Color.Default)
			{
				NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes() { ForegroundColor = view.PlaceholderColor.ToUIColor() });
				Control.AttributedPlaceholder = placeholderString;
			}
		}

	}
}
