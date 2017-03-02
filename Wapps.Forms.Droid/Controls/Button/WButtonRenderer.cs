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

[assembly: ExportRenderer(typeof(WButton), typeof(WButtonRenderer))]

namespace Wapps.Forms.Controls.Droid
{
	/// <summary>
	/// Class WEntryRenderer.
	/// </summary>
	public class WButtonRenderer : ButtonRenderer
	{
		
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			var view = (WButton)e.NewElement;

			if (view != null)
			{
				SetPadding(view);
				SetHorizontalTextAlignment(view);
				if (view.BorderRadius == 0)
				{
					Control.SetBackgroundColor(Color.Transparent.ToAndroid());
				}
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

			var view = (WButton)Element;

			if (e.PropertyName == WButton.PaddingProperty.PropertyName)
				SetPadding(view);
			else if (e.PropertyName == WButton.HorizontalTextAlignmentProperty.PropertyName)
				SetHorizontalTextAlignment(view);
		}

		void SetPadding(WButton view)
		{
			var pxLeft = FDroidUtils.Dp((int)view.Padding.Left);
			var pxTop = FDroidUtils.Dp((int)view.Padding.Top);
			var pxRight = FDroidUtils.Dp((int)view.Padding.Right);
			var pxBottom = FDroidUtils.Dp((int)view.Padding.Bottom);

			Control.SetPadding(pxLeft, pxTop, pxRight, pxBottom);
		}

		void SetHorizontalTextAlignment(WButton view)
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

	}
}

