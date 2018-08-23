using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WDatePicker), typeof(WDatePickerRenderer))]

namespace Wapps.Forms.IOS.Controls
{
    public class WDatePickerRenderer : DatePickerRenderer
    {
        public WDatePickerRenderer()
        {
        }

        /// <summary>
        /// The on element changed callback.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as WDatePicker;

            if (view != null)
            {
                SetHorizontalTextAlignment(view);
                SetBorder(view);
                SetPlaceholder(view);
                SetPlaceholderColor(view);
                SetSelectedDate(view);
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
            var view = Element as WDatePicker;

            if (e.PropertyName == WDatePicker.HorizontalTextAlignmentProperty.PropertyName)
                SetHorizontalTextAlignment(view);
            else if (e.PropertyName == WDatePicker.HasBorderProperty.PropertyName)
                SetBorder(view);
            else if (e.PropertyName == WDatePicker.PlaceholderColorProperty.PropertyName)
                SetPlaceholderColor(view);
            else if (e.PropertyName == WDatePicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == WDatePicker.SelectedDateProperty.PropertyName)
                SetSelectedDate(view);
        }

        /// <summary>
        /// Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetHorizontalTextAlignment(WDatePicker view)
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
        void SetPlaceholder(WDatePicker view)
        {
            Control.Placeholder = view.Placeholder;
        }

        /// <summary>
        /// Sets the selected date.
        /// </summary>
        /// <param name="view">View.</param>
        void SetSelectedDate(WDatePicker view)
        {
            if (view.SelectedDate == null)
            {
                Control.Text = "";
            }
        }

        /// <summary>
        /// Sets the border.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetBorder(WDatePicker view)
        {
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetPlaceholderColor(WDatePicker view)
        {
            if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderColor != Color.Default)
            {
                NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes() { ForegroundColor = view.PlaceholderColor.ToUIColor() });
                Control.AttributedPlaceholder = placeholderString;
            }
        }

    }
}
