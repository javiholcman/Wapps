using System;
using System.Collections.Generic;

namespace Wapps.ViewModels.Results
{
	public abstract class ViewModelResult
	{
		public virtual string Code { get; protected set; }

		public virtual string Message { get; protected set; }
	}

	public class FieldErrorResult : ViewModelResult
	{
		public string Field { get; set; } // "Username"

		public string Reason { get; set; } // "Is Invalid"

		public override string Message
		{
			get
			{
				return string.Format("{0} {1}", Field, Reason);
			}
			protected set
			{
				// do nothing
			}
		}

		public FieldErrorResult(string field, string reason, string code = null)
		{
			Code = code;
			Field = field;
			Reason = reason;
		}

	}

	public class SuccessResult : ViewModelResult
	{
		public SuccessResult(string message, string code = null)
		{
			Code = code;
			Message = message;
		}
	}

	public class ErrorResult : ViewModelResult
	{
		public ErrorResult(string message, string code = null)
		{
			Code = code;
			Message = message;
		}
	}

}
