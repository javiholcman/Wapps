using System;

using Xamarin.Forms;

namespace Wapps.Forms
{
	public class CrossImage : ContentPage
	{
		public CrossImage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


