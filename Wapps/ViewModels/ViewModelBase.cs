using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wapps.ViewModels.Results;

namespace Wapps.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        bool _isBusy;
        public virtual bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetProperty<bool>(ref _isBusy, value, "IsBusy");
            }
        }

        protected bool _loaded = false;
        Dictionary<string, object> _inputArgs = new Dictionary<string, object>();
        public virtual Dictionary<string, object> InputArgs
        {
            get { return _inputArgs; }
            set
            {
                _inputArgs = value;
                if (!_loaded)
                {
                    Loaded();
                    _loaded = true;
                }
            }
        }

        public virtual void Loaded()
        {

        }

        public virtual void OnViewAppearing()
        {

        }

        public virtual void OnViewDisappearing()
        {

        }

        #region ViewModel Results

        public event Action<IList<ViewModelResult>> ResultSetted;

        public void SetResult(ViewModelResult result)
        {
            SetResults(new List<ViewModelResult> { result });
        }

        public void SetResults(IList<ViewModelResult> results)
        {
            ResultSetted?.Invoke(results);
        }

        #endregion

        #region Navigation

        public Action<TaskCompletionSource<string>, string, string, string[]> AskDelegate { get; set; }

        public TaskCompletionSource<Dictionary<string, object>> ModalCompletionTask { get; set; }

        public Action<Type, Dictionary<string, object>, TaskCompletionSource<Dictionary<string, object>>> NavigateModalToDelegate { get; set; }

        public Action DismissModalDelegate { get; set; }


        public Action<Type, Dictionary<string, object>> NavigateToDelegate { get; set; }

        public void NavigateTo<T>(Dictionary<string, object> args = null)
        {
            NavigateToDelegate?.Invoke(typeof(T), args);
        }

        public async Task<Dictionary<string, object>> NavigateModalTo<T>(Dictionary<string, object> args = null)
        {
            if (NavigateModalToDelegate == null)
                throw new Exception("No view suscribed to NavigateModalTo");

            var tcs = new TaskCompletionSource<Dictionary<string, object>>();
            NavigateModalToDelegate(typeof(T), args, tcs);
            return await tcs.Task;
        }

        protected void DismissModal(Dictionary<string, object> args = null)
        {
            if (ModalCompletionTask == null)
                throw new Exception("The view is not modal");

            ModalCompletionTask.SetResult(args);

            DismissModalDelegate();
        }

        protected async Task<string> Ask(string title, string message, params string[] options)
        {
            var tcs = new TaskCompletionSource<string>();
            AskDelegate(tcs, title, message, options);
            return await tcs.Task;
        }

        #endregion

        protected string Localize(string key)
        {
            return key;
        }

    }
}
