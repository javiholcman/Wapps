using System;
using Xamarin.Forms;

namespace Wapps.Forms
{
	public static class FUtils
	{
		public static void InvokeOnMainThread(TimeSpan afterDelay, Action action)
		{
			Device.StartTimer(afterDelay, delegate
			{
				action();
				return false;
			});
		}

		public static void InvokeOnMainThread(double afterDelayMilliseconds, Action action)
		{
			Device.StartTimer(TimeSpan.FromMilliseconds(afterDelayMilliseconds), delegate
			{
				action();
				return false;
			});
		}

	}
}
