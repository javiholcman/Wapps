using System;

namespace Wapps
{
	public class Error
	{
		public string Code { get; set; }

		public string Message { get; set; }

		public object Target { set; get; }

		public Error()
		{
		}

		public Error(string code, string message, object target = null)
		{
			this.Code = code;
			this.Message = message;
			this.Target = target;
		}
	}
}

