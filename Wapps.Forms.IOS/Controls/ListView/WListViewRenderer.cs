using System;
using System.Collections;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Wapps.Forms;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS;
using Wapps.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WListView), typeof(WListViewRenderer))]

namespace Wapps.Forms.IOS.Controls
{
	public class WListViewRenderer : ListViewRenderer
	{
		public DidDisplayCellHandler DidDisplayCell;

		public WListViewRenderer()
		{
			
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				Element.ItemAppearing += Element_ItemAppearing;
			}

			var view = (WListView)e.NewElement;
			if (view != null)
			{
				SetSelectionEnabled(view);
				SetScrollEnabled(view);
				view.ScrollToTopDelegate = ScrollToTop;
			}

			if (e.OldElement != null)
			{
				e.OldElement.ItemAppearing -= Element_ItemAppearing;
				((WListView)e.OldElement).ScrollToTopDelegate = null;
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (WListView)Element;

			if (e.PropertyName == WListView.SelectionEnabledProperty.PropertyName)
				SetSelectionEnabled(view);
			else if (e.PropertyName == WListView.ScrollEnabledProperty.PropertyName)
				SetScrollEnabled(view);
		}

		void SetSelectionEnabled(WListView view)
		{
			Control.AllowsSelection = view.SelectionEnabled;
		}

		void SetScrollEnabled(WListView view)
		{
			Control.ScrollEnabled = view.ScrollEnabled;
		}

		void ScrollToTop()
		{
			Control.ScrollRectToVisible(new CGRect(0, 0, 1, 1), true);
		}

		void Element_ItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			FUtils.InvokeOnMainThread(0, delegate ()
			{
				var index = ((IList)this.Element.ItemsSource).IndexOf(e.Item);
				var cell = this.Control.CellAt(Foundation.NSIndexPath.FromRowSection(index, 0));

				if (cell == null)
					return;

				var view = ((WListView)Element);
				if (view.SelectionColor != Color.Default)
				{
					var bgView = new UIView();
					bgView.BackgroundColor = view.SelectionColor.ToUIColor();
					cell.SelectedBackgroundView = bgView;
				}

				if (DidDisplayCell != null)
				{
					ViewCell formsCell = null;
					if (cell is INativeElementView)
					{
						formsCell = ((INativeElementView)cell).Element as ViewCell;
					}
					DidDisplayCell(this, cell, formsCell);
				}
			});
		}
	}

	public delegate void DidDisplayCellHandler (WListViewRenderer sender, UITableViewCell cell, ViewCell formsCell);

}
