using System;
using System.Windows.Input;

namespace Wapps.ViewModels
{
	public class Command : ICommand
	{
		//
		// Fields
		//
		private readonly Func<object, bool> _canExecute;

		private readonly Action<object> _execute;

		//
		// Constructors
		//
		public Command(Action execute, Func<bool> canExecute) : this(delegate (object o)
		{
			execute();
		}, (object o) => canExecute())
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			if (canExecute == null)
			{
				throw new ArgumentNullException("canExecute");
			}
		}

		public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
		{
			if (canExecute == null)
			{
				throw new ArgumentNullException("canExecute");
			}
			this._canExecute = canExecute;
		}

		public Command(Action execute) : this(delegate (object o)
		{
			execute();
		})
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
		}

		public Command(Action<object> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
		}

		//
		// Methods
		//
		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute(parameter);
		}

		public void ChangeCanExecute()
		{
			EventHandler canExecuteChanged = this.CanExecuteChanged;
			if (canExecuteChanged != null)
			{
				canExecuteChanged(this, EventArgs.Empty);
			}
		}

		public void Execute(object parameter)
		{
			this._execute(parameter);
		}

		public event EventHandler CanExecuteChanged;
	}
}
