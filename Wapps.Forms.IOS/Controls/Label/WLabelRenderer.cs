using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WLabel), typeof(Wapps.Forms.IOS.Controls.WLabelRenderer))]

namespace Wapps.Forms.IOS.Controls
{
	public class WLabelRenderer : LabelRenderer
	{
		public WLabelRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			if (e.NewElement != null && Control == null)
				SetNativeControl(new WUILabel() { BackgroundColor = UIColor.Clear });
			
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
			if (view.BorderRadius > 0)
			{
				Control.Layer.CornerRadius = view.BorderRadius;
				Control.ClipsToBounds = true;
				Control.BackgroundColor = view.BackgroundColor.ToUIColor();
				this.BackgroundColor = UIColor.Clear;
			}

			if (view.BorderWidth > 0)
				Control.Layer.BorderWidth = view.BorderWidth;

			if (view.BorderColor != Color.Default)
				Control.Layer.BorderColor = view.BorderColor.ToUIColor().CGColor;
		}

		void SetLines(WLabel view)
		{
			if (view.Lines > -1)
			{
				Control.Lines = view.Lines;
			}
		}

		void SetPadding(WLabel view)
		{
			var label = (WUILabel)Control;
			label.EdgeInsets = new UIEdgeInsets((nfloat)view.Padding.Top, (nfloat)view.Padding.Left, (nfloat)view.Padding.Bottom, (nfloat)view.Padding.Right);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var view = Element as WLabel;
			if (view == null || Control.Frame.IsEmpty || (Control as WUILabel).EdgeInsets.Equals(UIEdgeInsets.Zero))
				return;

			Control.Frame = new CGRect(Control.Frame.X, Control.Frame.Y, Control.Frame.Width + view.Padding.Left + view.Padding.Right, Control.Frame.Height + view.Padding.Top + view.Padding.Bottom);
		}
	}
}
