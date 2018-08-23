using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Wapps.Forms.Controls;
using Wapps.Forms.Controls.Droid;
using Wapps.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WTableView), typeof(WTableViewRenderer))]

namespace Wapps.Forms.Controls.Droid
{
    public class WTableViewRenderer : TableViewRenderer
    {
        public WTableViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            var view = (WTableView)e.NewElement;

            if (view != null)
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

        void SetSelectionEnabled(WTableView view)
        {
            if (!view.SelectionEnabled)
            {
                Control.Selector = new ColorDrawable(Android.Graphics.Color.Transparent);
                Control.SoundEffectsEnabled = false;
            }
        }

        void SetSeparatorColor(WTableView view)
        {
            if (view.SeparatorColor != Color.Transparent)
            {
                Control.Divider = new ColorDrawable(view.SeparatorColor.ToAndroid());
                Control.DividerHeight = FDroidUtils.Dp(1);
            }
            else
            {
                Control.Divider = null;
                Control.DividerHeight = 0;
            }
        }

        void ScrollToTop()
        {
            Control.SmoothScrollToPosition(0);
        }
    }

    /*
	// to remove header of tableview
	public class WTextCellRenderer : Xamarin.Forms.Platform.Android.TextCellRenderer
	{
		protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var view = base.GetCellCore(item, convertView, parent, context) as ViewGroup;

			if (item is TextCell)
				if (string.IsNullOrEmpty((item as TextCell).Text))
				{
					if (view != null)
					{
						view.Visibility = ViewStates.Gone;
						while (view.ChildCount > 0)
							view.RemoveViewAt(0);
						view.SetMinimumHeight(0);
						view.SetPadding(0, 0, 0, 0);
					}
				}

			return view;
		}
	}
    */

}
