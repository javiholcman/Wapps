using System;
using System.Collections.Generic;
using Wapps.ViewModels.Results;
using XLabs.Ioc;

namespace Wapps.ViewModels
{
	public abstract class ViewModelBase : ObservableObject
	{
		bool _isBusy;
		public bool IsBusy
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

		public Dictionary<string, object> InputArgs { get; set; } = new Dictionary<string, object>();

		public virtual void OnViewAppearing()
		{
			
		}

		public virtual void OnViewDisappearing()
		{
			
		}

		#region ViewModel Results

		public event Action<IList<ViewModelResult>> ResultSetted;

		protected void SetResult(ViewModelResult result)
		{
			SetResults(new List<ViewModelResult> { result });
		}

		protected void SetResults(IList<ViewModelResult> results)
		{
			ResultSetted?.Invoke(results);
		}

		#endregion

		#region Navigation

		public Action<Type, Dictionary<string, object>> NavigateToDelegate { get; set; }

		public void NavigateTo<T>(Dictionary<string, object> args = null)
		{
			NavigateToDelegate?.Invoke(typeof(T), args);
		}

		#endregion

		protected string Localize(string key)
		{
			return key;
		}

	}
}
