using System;
using UIKit;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WStackLayout), typeof(WStackLayoutRenderer))]

namespace Wapps.Forms.IOS
{
	public class WStackLayoutRenderer : VisualElementRenderer <StackLayout>
	{
		public WStackLayoutRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			var view = (WStackLayout)e.NewElement;
			if (view != null)
			{
				SetBorder(view);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (WStackLayout)Element;

			if (e.PropertyName == WStackLayout.BorderRadiusProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WStackLayout.BorderColorProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WStackLayout.BorderWidthProperty.PropertyName)
				SetBorder(view);
		}

		void SetBorder(WStackLayout view)
		{
			if (view.BorderRadius > 0)
			{
				NativeView.Layer.CornerRadius = view.BorderRadius;
				NativeView.ClipsToBounds = true;
				NativeView.BackgroundColor = view.BackgroundColor.ToUIColor();
			}

			if (view.BorderWidth > 0)
				NativeView.Layer.BorderWidth = view.BorderWidth;

			if (view.BorderColor != Color.Default)
				NativeView.Layer.BorderColor = view.BorderColor.ToUIColor().CGColor;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
		}
	}
}
