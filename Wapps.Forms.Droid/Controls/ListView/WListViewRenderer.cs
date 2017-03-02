using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Wapps.Forms.Controls;
using Wapps.Forms.Controls.Droid;
using Wapps.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WListView), typeof(WListViewViewRenderer))]

namespace Wapps.Forms.Controls.Droid
{
	public class WListViewViewRenderer : ListViewRenderer
	{
		public WListViewViewRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			var view = (WListView)e.NewElement;

			if (view != null)
			{
				SetSelectionEnabled(view);
				SetSelectionColor(view);
				view.ScrollToTopDelegate = ScrollToTop;
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
			else if (e.PropertyName == WListView.SelectionColorProperty.PropertyName)
				SetSelectionColor(view);
		}

		void SetSelectionColor(WListView view)
		{
			if (view.SelectionColor != Color.Default)
			{
				Control.Selector = new ColorDrawable(view.SelectionColor.ToAndroid());
			}
		}

		void SetSelectionEnabled(WListView view)
		{
			if (!view.SelectionEnabled)
			{
				Control.Selector = new ColorDrawable(Android.Graphics.Color.Transparent);
				Control.CacheColorHint = Android.Graphics.Color.Transparent;
				Control.SoundEffectsEnabled = false;
			}
		}

		void ScrollToTop()
		{
			Control.SmoothScrollToPosition(0);
		}

		private int _mPosition;

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			if ((Element as WListView).ScrollEnabled)
				return base.DispatchTouchEvent(e);
			
			if (e.ActionMasked == MotionEventActions.Down)
			{
				// Record the position the list the touch landed on
				_mPosition = this.Control.PointToPosition((int)e.GetX(), (int)e.GetY());
				return base.DispatchTouchEvent(e);
			}

			if (e.ActionMasked == MotionEventActions.Move)
			{
				// Ignore move eents
				return true;
			}

			if (e.ActionMasked == MotionEventActions.Up)
			{
				// Check if we are still within the same view
				if (this.Control.PointToPosition((int)e.GetX(), (int)e.GetY()) == _mPosition)
				{
					base.DispatchTouchEvent(e);
				}
				else
				{
					// Clear pressed state, cancel the action
					Pressed = false;
					Invalidate();
					return true;
				}
			}

			return base.DispatchTouchEvent(e);
		}

	}
}
