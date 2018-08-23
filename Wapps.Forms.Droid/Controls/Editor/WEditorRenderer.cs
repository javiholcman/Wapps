using System;
using Android.Content;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WEditor), typeof(Wapps.Forms.Droid.WEditorRenderer))]
namespace Wapps.Forms.Droid
{
	public class WEditorRenderer : EditorRenderer
	{
        public WEditorRenderer(Context ctx) : base(ctx)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var element = e.NewElement as WEditor;
				this.Control.Hint = element.Placeholder;
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == WEditor.PlaceholderProperty.PropertyName)
			{
				var element = this.Element as WEditor;
				this.Control.Hint = element.Placeholder;
			}
		}
	}
}
