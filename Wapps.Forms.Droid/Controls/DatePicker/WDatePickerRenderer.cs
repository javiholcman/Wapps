using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using Wapps.Forms.Controls;
using Wapps.Forms.Controls.Droid;
using Wapps.Forms.Droid;
using Android.Views.InputMethods;

[assembly: ExportRenderer(typeof(WDatePicker), typeof(WDatePickerRenderer))]

namespace Wapps.Forms.Controls.Droid
{
	/// <summary>
	/// Class WEntryRenderer.
	/// </summary>
	public class WDatePickerRenderer : DatePickerRenderer
	{

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			var view = (WDatePicker)e.NewElement;

			if (view != null)
			{
				SetDefaultValues(view);
				SetSelectedDate(view);
				SetFont(view);
				SetHasBorder(view);
				SetHorizontalTextAlignment(view);
				SetPlaceholder(view);
				SetPlaceholderColor(view);
			}
		}

		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (WDatePicker)Element;

			if (e.PropertyName == WDatePicker.FontProperty.PropertyName)
				SetFont(view);
			else if (e.PropertyName == WDatePicker.HasBorderProperty.PropertyName)
				SetHasBorder(view);
			else if (e.PropertyName == WDatePicker.PlaceholderColorProperty.PropertyName)
				SetPlaceholderColor(view);
			else if (e.PropertyName == WDatePicker.HorizontalTextAlignmentProperty.PropertyName)
				SetHorizontalTextAlignment(view);
			else if (e.PropertyName == WDatePicker.PlaceholderProperty.PropertyName)
				SetPlaceholder(view);
			else if (e.PropertyName == WDatePicker.SelectedDateProperty.PropertyName)
				SetSelectedDate(view);
		}


		void SetDefaultValues(WDatePicker view)
		{
			Control.Gravity = GravityFlags.CenterVertical;
			Control.InputType = Control.InputType | InputTypes.TextFlagNoSuggestions;
		}

		void SetSelectedDate(WDatePicker view)
		{
			if (view.SelectedDate == null)
			{
				Control.Text = "";
			}
		}

		void SetFont(WDatePicker view)
		{
			var font = view.Font.WithSize(view.FontSize);
			Control.TextSize = font.ToScaledPixel();

			if (view.Font != Font.Default)
			{
				Control.Typeface = view.Font.ToExtendedTypeface(Context);
			}
		}

		void SetHorizontalTextAlignment(WDatePicker view)
		{
			// I need to implement because I will override the vertical align.
			switch (view.HorizontalTextAlignment)
			{
				case Xamarin.Forms.TextAlignment.Start:
					Control.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
					break;

				case Xamarin.Forms.TextAlignment.Center:
					Control.Gravity = GravityFlags.Center | GravityFlags.CenterVertical;
					break;

				case Xamarin.Forms.TextAlignment.End:
					Control.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
					break;
			}
		}

		void SetPlaceholderColor(WDatePicker view)
		{
			if (view.PlaceholderColor != Color.Default)
			{
				Control.SetHintTextColor(view.PlaceholderColor.ToAndroid());
			}
		}

		void SetPlaceholder(WDatePicker view)
		{
			Control.Hint = view.Placeholder;
		}

		void SetHasBorder(WDatePicker view)
		{
			if (!view.HasBorder)
			{
				Control.SetBackgroundColor(Color.Transparent.ToAndroid());
				Control.SetPadding(0, 0, 0, 0);
			}
		}

	}
}

