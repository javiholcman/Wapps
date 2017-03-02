using System;
using Android.Graphics.Drawables;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WStackLayout), typeof(Wapps.Forms.Droid.WStackLayoutRenderer))]

namespace Wapps.Forms.Droid
{
	public class WStackLayoutRenderer : VisualElementRenderer<StackLayout>
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

		protected override void UpdateBackgroundColor()
		{
			var view = Element as WStackLayout;
			if (view != null && view.BorderRadius > 0)
				return;

			base.UpdateBackgroundColor();
		}

		void SetBorder(WStackLayout view)
		{
			if (view.BorderRadius > 0 || view.BorderWidth > 0)
			{
				// creating gradient drawable for the curved background
				var _gradientBackground = new GradientDrawable();
				_gradientBackground.SetShape(ShapeType.Rectangle);
				_gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

				// Thickness of the stroke line
				_gradientBackground.SetStroke(FDroidUtils.Dp(view.BorderWidth), view.BorderColor.ToAndroid());

				// Radius for the curves
				_gradientBackground.SetCornerRadius(FDroidUtils.Dp(view.BorderRadius));

				// set the background of the label
				ViewGroup.SetBackgroundColor(Color.Transparent.ToAndroid());
				ViewGroup.SetBackground(_gradientBackground);
			}
		}
	}
}
