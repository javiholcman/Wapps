using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.Forms;

namespace Wapps.Forms.IOS.Controls
{
	public class PageRenderer <T> : Xamarin.Forms.Platform.iOS.PageRenderer where T : Page
	{
		public PageRenderer()
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			MapComponents();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.FixNavigationBarItems();
		}

		protected TNativeView FindRenderer<TNativeView>(string name)
		{
			return FIOSUtils.FindRenderer<TNativeView> (this, name);
		}

		protected object FindNativeView(string name)
		{
			return FIOSUtils.FindRenderer (this, name);
		}

		protected void MapComponents()
		{
			var props = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(Renderer), true).Count() > 0).ToList();
			foreach (var prop in props)
			{
				var view = FindNativeView(prop.Name);
				if (view == null)
				{
					throw new Exception("Wapps Mapping Components - Not found xamarin forms with name '" + prop.Name + "'");
				}
				prop.SetValue(this, (UIView)view);
			}
		}

		protected T Page {
			get {
				return (T)this.Element;
			}
		}
	
		protected void FixNavigationBarItems()
		{
			var LeftNavList = new List<UIBarButtonItem>();
			var rightNavList = new List<UIBarButtonItem>();

			var navigationItem = this.NavigationController.TopViewController.NavigationItem;

			for (var i = 0; i < Page.ToolbarItems.Count; i++)
			{

				var ItemPriority = Page.ToolbarItems[i].Priority;

				if (ItemPriority == 1)
				{
					UIBarButtonItem LeftNavItems = navigationItem.RightBarButtonItems[i];
					LeftNavList.Add(LeftNavItems);
				}
				else if (ItemPriority == 0)
				{
					UIBarButtonItem RightNavItems = navigationItem.RightBarButtonItems[i];
					rightNavList.Add(RightNavItems);
				}
			}

			navigationItem.SetLeftBarButtonItems(LeftNavList.ToArray(), false);
			navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);
		}

	}

	public class Renderer : Attribute
	{

	}

}

