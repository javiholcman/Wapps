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
using Android.Content;

[assembly: ExportRenderer(typeof(WPicker), typeof(WPickerRenderer))]

namespace Wapps.Forms.Controls.Droid
{
    /// <summary>
    /// Class WEntryRenderer.
    /// </summary>
    public class WPickerRenderer : PickerRenderer
    {
        public WPickerRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var view = (WPicker)e.NewElement;

            if (view != null)
            {
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

            var view = (WPicker)Element;

            if (e.PropertyName == WPicker.HasBorderProperty.PropertyName)
                SetHasBorder(view);
            else if (e.PropertyName == WPicker.PlaceholderColorProperty.PropertyName)
                SetPlaceholderColor(view);
            else if (e.PropertyName == WPicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
                SetHorizontalTextAlignment(view);
        }

        void SetHorizontalTextAlignment(WPicker view)
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

        void SetPlaceholderColor(WPicker view)
        {
            if (view.PlaceholderColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderColor.ToAndroid());
            }
        }

        void SetPlaceholder(WPicker view)
        {
            Control.Hint = view.Placeholder;
        }

        void SetHasBorder(WPicker view)
        {
            if (!view.HasBorder)
            {
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());
                Control.SetPadding(0, 0, 0, 0);
            }
        }

    }
}

