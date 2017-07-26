using System;
using System.Collections.Generic;
using Wapps.ViewModels;
using Wapps.ViewModels.Results;
using Xamarin.Forms;

namespace Wapps.Forms.ViewModels
{
	public class ContentPageBase : ContentPage
	{
		public ContentPageBase()
		{
			base.SetBinding(Page.IsBusyProperty, new Binding("IsBusy", 0, null, null, null, null));
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
				ViewModel.NavigateModalToDelegate = NavigateModalTo;
				ViewModel.ResultSetted += ResultSetted;
			}
			else 
			{
				ViewModel.NavigateToDelegate -= NavigateTo;
				ViewModel.NavigateModalToDelegate = null;				
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel?.OnViewAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			ViewModel?.OnViewDisappearing();
		}

		public virtual void NavigateTo(Type viewModelType, Dictionary<string, object> args)
		{
			var page = ViewsManager.CreateView (viewModelType) as ContentPageBase;
			page.ViewModel.InputArgs = args;
			Navigation.PushAsync(page, true);
		}

		public virtual void ResultSetted(IList<ViewModelResult> results)
		{	
			if (results.Count == 0)
				return;

			if (results.Count == 1)
			{
				var result = results[0];
				if (result is ErrorResult || result is FieldErrorResult)
					DisplayAlert("Error", result.Message, "Ok");

				if (result is SuccessResult)
					DisplayAlert("Ok", result.Message, "Ok");
			}
			else 
			{
				string message = "";
				foreach (var result in results)
					message += result.Message + "\n";

				if (!string.IsNullOrEmpty(message))
					message = message.Substring(0, message.Length - 1);
				
				DisplayAlert("Error", message, "Ok");
			}
		}
		
        protected virtual void NavigateModalTo(Type vmType, Dictionary<string, object> args, System.Threading.Tasks.TaskCompletionSource<Dictionary<string, object>> tsc)
        {
            var page = ViewsManager.CreateView(vmType) as ContentPageBase;
            var viewModel = page.ViewModel as ViewModelBase;
            viewModel.ModalCompletionTask = tsc;
            viewModel.InputArgs = args;
            Navigation.PushModalAsync(new NavigationPage(page), true);
        }		
	}
}
