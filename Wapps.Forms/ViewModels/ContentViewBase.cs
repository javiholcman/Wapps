using System;
using System.Collections.Generic;
using Wapps.ViewModels;
using Wapps.ViewModels.Results;
using Xamarin.Forms;

namespace Wapps.Forms.ViewModels
{
	public class ContentViewBase : ContentView
	{
		public ContentViewBase()
		{
			
		}

		public ViewModelBase ViewModel
		{
			get
			{
				return BindingContext as ViewModelBase;
			}
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (ViewModel != null)
			{
				ViewModel.NavigateToDelegate += NavigateTo;
				ViewModel.ResultSetted += ResultSetted;
			}
			else 
			{
				ViewModel.NavigateToDelegate -= NavigateTo;
				ViewModel.ResultSetted -= ResultSetted;
			}
		}

		public virtual void NavigateTo(Type viewModelType, Dictionary<string, object> args)
		{
			(Parent as ContentPageBase)?.NavigateTo(viewModelType, args);
		}

		public virtual void ResultSetted(IList<ViewModelResult> results)
		{
			(Parent as ContentPageBase)?.ResultSetted(results);
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			if (Parent != null)
			{
				ViewModel?.OnViewAppearing();
			}
			else 
			{
				ViewModel?.OnViewDisappearing();
			}
		}
	}
}
