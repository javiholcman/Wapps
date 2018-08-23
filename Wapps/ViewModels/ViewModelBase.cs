using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wapps.Core
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected bool _loaded = false;

        bool _isBusy;
        public virtual bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
        }

        Dictionary<string, object> _inputArgs = new Dictionary<string, object>();
        public virtual Dictionary<string, object> InputArgs
        {
            get { return _inputArgs; }
            set { _inputArgs = value; }
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

        public void InvokeLoaded()
        {
            if (!_loaded)
            {
                Loaded();
                _loaded = true;
            }
        }

        #region ViewModel Results

        public event Action<IList<ViewModelResult>> ResultSetted;

        public virtual void SetResult(ViewModelResult result)
        {
            SetResults(new List<ViewModelResult> { result });
        }

        public virtual void SetResults(IList<ViewModelResult> results)
        {
            ResultSetted?.Invoke(results);
        }

        #endregion

        #region NavigateTo

        public Action<Type, Dictionary<string, object>, TaskCompletionSource<Dictionary<string, object>>> NavigateToDelegate { get; set; }

        public virtual void NavigateTo<T>(Dictionary<string, object> args = null)
        {
            NavigateToDelegate?.Invoke(typeof(T), args, this.ModalCompletionTask);
        }

        #endregion

        #region NavigateModalTo

        public bool IsModal
        {
            get { return ModalCompletionTask != null; }
        }

        public Action<Type, Dictionary<string, object>, TaskCompletionSource<Dictionary<string, object>>> NavigateModalToDelegate { get; set; }
        public TaskCompletionSource<Dictionary<string, object>> ModalCompletionTask { get; set; }

        public async virtual Task<Dictionary<string, object>> NavigateModalTo<T>(Dictionary<string, object> args = null)
        {
            if (NavigateModalToDelegate == null)
                throw new Exception("No view suscribed to NavigateModalTo");

            var tcs = new TaskCompletionSource<Dictionary<string, object>>();
            NavigateModalToDelegate(typeof(T), args, tcs);
            return await tcs.Task;
        }

        #endregion

        #region NavigateClose

        public Action NavigateCloseDelegate { get; set; }

        public virtual void NavigateClose(Dictionary<string, object> args = null)
        {
            if (IsModal)
            {
                ModalCompletionTask.SetResult(args);
                NavigateCloseDelegate();
            }
            else
            {
                NavigateBackDelegate?.Invoke();
            }
        }

        #endregion

        #region NavigateBack

        public bool ShouldCloseModalOnBack { get; set; } = false;

        public Action NavigateBackDelegate { get; set; }

        public void NavigateBack(Dictionary args = null)
        {
            if (ShouldCloseModalOnBack && IsModal)
            {
                ModalCompletionTask.SetResult(args);
                NavigateCloseDelegate();
            }
            else
            {
                NavigateBackDelegate?.Invoke();
            }
        }

        #endregion

        #region AskAsync

        public Action<TaskCompletionSource<string>, string, string, string[]> AskAsyncDelegate { get; set; }

        public virtual async Task<string> AskAsync(string title, string message, params string[] options)
        {
            if (AskAsyncDelegate == null)
                throw new Exception("AskAsync - Not AskAsyncDelegate setted");

            var tcs = new TaskCompletionSource<string>();
            AskAsyncDelegate(tcs, title, message, options);
            return await tcs.Task;
        }

        #endregion

        public string Localize(string key)
        {
            return key;
        }

    }
}
