
using System;
using System.ComponentModel;
using UIKit;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WButton), typeof(WButtonRenderer))]

namespace Wapps.Forms.IOS
{
	public class WButtonRenderer : ButtonRenderer
	{
		public WButtonRenderer()
		{
		}

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
			Control.TitleEdgeInsets = new UIEdgeInsets((nfloat)view.Padding.Top, (nfloat)view.Padding.Left, (nfloat)view.Padding.Bottom, (nfloat)view.Padding.Right);	
		}

		void SetHorizontalTextAlignment(WButton view)
		{
			// I need to implement because I will override the vertical align.
			switch (view.HorizontalTextAlignment)
			{
				case Xamarin.Forms.TextAlignment.Start:
					Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
					break;

				case Xamarin.Forms.TextAlignment.Center:
					Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
					break;

				case Xamarin.Forms.TextAlignment.End:
					Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
					break;
			}
		}
	}
}
