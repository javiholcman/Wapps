using System;
using Wapps.Forms.Controls;
using Xamarin.Forms.Platform.Android;

namespace Wapps.Forms.Droid
{
	public class WEditorRenderer : EditorRenderer
	{
		public WEditorRenderer()
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

