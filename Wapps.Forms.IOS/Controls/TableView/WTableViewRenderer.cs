using System;
using CoreGraphics;
using Wapps.Forms.Controls;
using Wapps.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WTableView), typeof(WTableViewRenderer))]

namespace Wapps.Forms.IOS.Controls
{
    public class WTableViewRenderer : TableViewRenderer
    {
        public WTableViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            var view = (WTableView)e.NewElement;

            if (e.NewElement != null)
            {
                SetSeparatorColor(view);
                SetSelectionEnabled(view);
                view.ScrollToTopDelegate = ScrollToTop;
            }

            if (e.OldElement != null)
            {
                ((WTableView)e.OldElement).ScrollToTopDelegate = null;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (WTableView)Element;

            if (e.PropertyName == WTableView.SeparatorColorProperty.PropertyName)
                SetSeparatorColor(view);
            else if (e.PropertyName == WTableView.SelectionEnabledProperty.PropertyName)
                SetSelectionEnabled(view);
        }

        void SetSeparatorColor(WTableView view)
        {
            Control.SeparatorColor = view.SeparatorColor.ToUIColor();
        }

        void SetSelectionEnabled(WTableView view)
        {

        }

        void ScrollToTop()
        {
            Control.ScrollRectToVisible(new CGRect(0, 0, 1, 1), true);
        }
    }
}
