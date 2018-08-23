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
        public WListViewRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            var view = (WListView)e.NewElement;
            if (view != null)
            {
                SetSelectionEnabled(view);
                SetScrollEnabled(view);
                view.ScrollToTopDelegate = ScrollToTop;
                Control.TableFooterView = new UIView();
                Control.SectionHeaderHeight = UIKit.UITableView.AutomaticDimension;
                Control.SectionFooterHeight = UIKit.UITableView.AutomaticDimension;
                Control.EstimatedSectionHeaderHeight = 0;
                Control.EstimatedSectionFooterHeight = 0;
            }

            if (e.OldElement != null)
            {
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
    }

}
