using System;

namespace Wapps
{
	public class Response<T> where T : new()
	{
		public Error Error { get; private set; }

		public T Output { get; private set; }

		public Response(T output)
		{
			this.Output = output;
		}

		public Response(Error error)
		{
			this.Error = error;
		}
	}
}

