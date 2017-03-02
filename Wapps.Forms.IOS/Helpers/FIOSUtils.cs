using System;
using System.Reflection;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Wapps.Forms.IOS
{
	public static class FIOSUtils
	{
		public static TNativeView FindRenderer<TNativeView>(IVisualElementRenderer parent, string name)
		{
			return (TNativeView)FindRenderer(parent, name);
		}

		public static object FindRenderer(IVisualElementRenderer renderer, string name)
		{
			var element = renderer.Element.FindByName<VisualElement>(name);
			if (element == null)
			{
				var field = renderer.Element.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
				if (field == null)
				{
					throw new Exception("FindNativeView not find with name:" + name);
				}
				element = field.GetValue(renderer.Element) as VisualElement;
			}
			return Xamarin.Forms.Platform.iOS.Platform.GetRenderer(element).NativeView;
		}

		public static object FindRenderer(Element parent, string name)
		{
			var element = parent.FindByName<VisualElement>(name);
			return Xamarin.Forms.Platform.iOS.Platform.GetRenderer(element).NativeView;
		}

		public static TNativeView FindRenderer<TNativeView>(Element parent, string name)
		{
			return (TNativeView)FindRenderer(parent, name);
		}
	}
}
