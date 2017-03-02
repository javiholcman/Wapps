using System;
using Android.Graphics.Drawables;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WLabel), typeof(Wapps.Forms.Droid.WLabelRenderer))]

namespace Wapps.Forms.Droid
{
	public class WLabelRenderer : LabelRenderer
	{
		public WLabelRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			base.OnElementChanged(e);

			var view = (WLabel)e.NewElement;
			if (view != null)
			{
				SetBorder(view);
				SetLines(view);
				SetPadding(view);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (WLabel)Element;

			if (e.PropertyName == WLabel.LinesProperty.PropertyName)
				SetLines(view);
			else if (e.PropertyName == WLabel.BorderRadiusProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WLabel.BorderColorProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WLabel.BorderWidthProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WLabel.PaddingProperty.PropertyName)
				SetPadding(view);
		}

		void SetBorder(WLabel view)
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
				Control.SetBackgroundColor(Color.Transparent.ToAndroid());
				Control.SetBackground(_gradientBackground);
			}
		}

		protected override void UpdateBackgroundColor()
		{
			var view = Element as WLabel;
			if (view != null && view.BorderRadius > 0)
				return;

			base.UpdateBackgroundColor();
		}


		void SetLines(WLabel view)
		{
			if (view.Lines > -1)
			{
				this.Control.SetLines(view.Lines);
			}
		}

		void SetPadding(WLabel view)
		{
			var pxLeft = FDroidUtils.Dp((int)view.Padding.Left);
			var pxTop = FDroidUtils.Dp((int)view.Padding.Top);
			var pxRight = FDroidUtils.Dp((int)view.Padding.Right);
			var pxBottom = FDroidUtils.Dp((int)view.Padding.Bottom);

			Control.SetPadding(pxLeft, pxTop, pxRight, pxBottom);
		}

	}
}
