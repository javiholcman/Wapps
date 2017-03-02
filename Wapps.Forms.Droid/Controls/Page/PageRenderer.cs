using System;
using System.Linq;
using Xamarin.Forms;

namespace Wapps.Forms.Droid
{
	public class PageRenderer<T> : Xamarin.Forms.Platform.Android.PageRenderer where T : Page
	{
		bool _firstTime = true;
		public PageRenderer()
		{

		}

		protected override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			if (_firstTime)
			{
				MapComponents();
				Loaded();
			}
			DidAppear();
			_firstTime = false;
		}

		protected virtual void Loaded()
		{

		}

		protected virtual void DidAppear()
		{

		}

		protected override void OnDetachedFromWindow()
		{
			base.OnDetachedFromWindow();
			this.DidDisappear();
		}

		protected virtual void DidDisappear()
		{

		}

		protected TNativeView FindRenderer<TNativeView>(string name)
		{
			return FDroidUtils.FindRenderer<TNativeView>(this, name);
		}

		protected object FindRenderer(string name)
		{
			return FDroidUtils.FindRenderer(this, name);
		}

		protected void MapComponents()
		{
			var props = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(Renderer), true).Count() > 0).ToList();
			foreach (var prop in props)
			{
				var view = FindRenderer(prop.Name);
				if (view == null)
				{
					throw new Exception("Wapps Mapping Components - Not found xamarin forms with name '" + prop.Name + "'");
				}
				prop.SetValue(this, (Android.Views.View)view);
			}
		}

		protected T Page
		{
			get
			{
				return (T)this.Element;
			}
		}
	}

	public class Renderer : Attribute
	{

	}
}
