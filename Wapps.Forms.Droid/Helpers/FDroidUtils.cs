using System;
using System.Reflection;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Wapps.Forms.Droid
{
	public static class FDroidUtils
	{
		public static TNativeView FindRenderer<TNativeView>(IVisualElementRenderer parent, string name)
		{
			return (TNativeView)FindRenderer(parent, name);
		}

		public static object FindRenderer (IVisualElementRenderer parent, string name)
		{
			var element = parent.Element.FindByName<VisualElement>(name);
			if (element == null)
			{
				var field = parent.Element.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
				if (field == null)
				{
					throw new Exception("FindRenderer not find with name:" + name);
				}
				element = field.GetValue(parent.Element) as VisualElement;
			}
			var renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(element);
			return renderer;
		}

		public static object FindRenderer(Element elem, string name)
		{
			var element = elem.FindByName<VisualElement>(name);
			var subrender = Xamarin.Forms.Platform.Android.Platform.GetRenderer(element);
			var view = subrender.GetType().GetProperty("Control").GetValue(subrender) as Android.Views.View;
			return view;
		}

		public static TNativeView FindRenderer<TNativeView>(Element elem, string name)
		{
			return (TNativeView)FindRenderer(elem, name);
		}

		public static int Dp(int dp)
		{
			return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp,  Android.App.Application.Context.Resources.DisplayMetrics);
		}
	}
}
