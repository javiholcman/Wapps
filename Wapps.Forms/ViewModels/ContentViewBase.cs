using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Wapps.Core;
using System.Threading.Tasks;

namespace Wapps.Forms.ViewModels
{
    public class ContentViewBase : ContentView
    {
        public ContentViewBase()
        {

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
                ViewModel.NavigateToDelegate = NavigateTo;
                ViewModel.NavigateModalToDelegate = NavigateModalTo;
                ViewModel.NavigateCloseDelegate = NavigateClose;
                ViewModel.NavigateBackDelegate = NavigateBack;
                ViewModel.AskAsyncDelegate = AskAsync;

                FUtils.InvokeOnMainThread(100, () =>
                {
                    ViewModel.InvokeLoaded();
                });
            }
        }

        public virtual void NavigateTo(Type viewModelType, Dictionary<string, object> args, System.Threading.Tasks.TaskCompletionSource<Dictionary<string, object>> tsc)
        {
            FindPage()?.NavigateTo(viewModelType, args, tsc);
        }

        public virtual void ResultSetted(IList<ViewModelResult> results)
        {
            FindPage()?.ResultSetted(results);
        }

        protected virtual void NavigateModalTo(Type vmType, Dictionary<string, object> args, System.Threading.Tasks.TaskCompletionSource<Dictionary<string, object>> tsc)
        {
            FindPage()?.NavigateModalTo(vmType, args, tsc);
        }

        public virtual void AskAsync(TaskCompletionSource<string> tcs, string title, string message, string[] buttons)
        {
            FindPage()?.AskAsync(tcs, title, message, buttons);
        }

        public virtual void NavigateBack()
        {
            FindPage()?.NavigateBack();
        }

        public virtual void NavigateClose()
        {
            FindPage()?.NavigateClose();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent != null)
                ViewModel?.OnViewAppearing();
            else
                ViewModel?.OnViewDisappearing();
        }

        ContentPageBase FindPage()
        {
            Element parent = this.Parent;

            do
            {
                if (parent is ContentPageBase)
                    return parent as ContentPageBase;

                parent = parent.Parent;
            }
            while (parent != null);

            return null;
        }
    }
}
