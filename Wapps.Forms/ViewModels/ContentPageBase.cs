using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wapps.Core;
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
            get { return BindingContext as ViewModelBase; }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                ViewModel.NavigateToDelegate = NavigateTo;
                ViewModel.NavigateModalToDelegate = NavigateModalTo;
                ViewModel.NavigateCloseDelegate = NavigateClose;
                ViewModel.NavigateBackDelegate = NavigateBack;
                ViewModel.AskAsyncDelegate = AskAsync;
                ViewModel.ResultSetted += ResultSetted;
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

        protected virtual void OnViewModelLoaded()
        {

        }

        protected virtual void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        public virtual void NavigateTo(Type viewModelType, Dictionary<string, object> args, System.Threading.Tasks.TaskCompletionSource<Dictionary<string, object>> tsc)
        {
            var page = ViewsManager.CreateView(viewModelType) as ContentPageBase;
            page.ViewModel.ModalCompletionTask = tsc;
            page.ViewModel.InputArgs = (args == null ? new Dictionary() : args); ;
            page.ViewModel.ShouldCloseModalOnBack = false;
            page.ViewModel.InvokeLoaded();
            page.OnViewModelLoaded();
            Navigation.PushAsync(page, true);
        }

        public virtual void NavigateModalTo(Type vmType, Dictionary<string, object> args, System.Threading.Tasks.TaskCompletionSource<Dictionary<string, object>> tsc)
        {
            var page = ViewsManager.CreateView(vmType) as ContentPageBase;
            page.ViewModel.ModalCompletionTask = tsc;
            page.ViewModel.InputArgs = (args == null ? new Dictionary() : args);
            page.ViewModel.ShouldCloseModalOnBack = true;
            page.ViewModel.InvokeLoaded();
            page.OnViewModelLoaded();
            Navigation.PushModalAsync(new NavigationPage(page), true);
        }

        public virtual void NavigateBack()
        {
            Navigation.PopAsync();
        }

        public virtual void NavigateClose()
        {
            Navigation.PopModalAsync();
        }

        public async virtual void AskAsync(TaskCompletionSource<string> tcs, string title, string message, string[] buttons)
        {
            bool resp = false;

            if (buttons.Length == 0)
                await DisplayAlert(title, message, "Ok");
            else if (buttons.Length == 1)
                await DisplayAlert(title, message, buttons[0]);
            else if (buttons.Length == 2)
                resp = await DisplayAlert(title, message, buttons[0], buttons[1]);

            if (!resp)
                tcs.SetResult((buttons.Length > 1 ? buttons[1] : ""));
            else
                tcs.SetResult(buttons[0]);
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
    }
}
