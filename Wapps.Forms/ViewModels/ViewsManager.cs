using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms
{
	public static class ViewsManager
	{
		static Dictionary<Type, Type> _rel = new Dictionary<Type, Type>();

		public static void Register<TViewModel, TView>()
		{
			_rel[typeof(TViewModel)] = typeof(TView);
		}

		public static Page CreateView(Type viewModelType)
		{
			var viewType = _rel[viewModelType];
			if (viewType == null)
				throw new Exception("ViewType type not setted for " + viewModelType.Name + ". Use ViewsManager.Register <TViewModel, TView> ()");

			var view = Activator.CreateInstance(viewType) as Page;
			var viewModel = Activator.CreateInstance(viewModelType);
			view.BindingContext = viewModel;
			return view;
		}

		public static Page CreateView<TViewModel>()
		{
			return CreateView(typeof(TViewModel));
		}
	}
}
