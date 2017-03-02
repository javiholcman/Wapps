using System;

namespace Wapps
{
	public static class WeakReference_Extension
	{
		public static T GetTarget<T>(this WeakReference<T> weakRef) where T : class
		{
			T obj;
			if (weakRef.TryGetTarget(out obj))
			{
				return obj;
			}
			else {
				return default(T);
			}
		}
	}
}

