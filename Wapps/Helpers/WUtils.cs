using System;
using System.IO;
using System.Reflection;

namespace Wapps
{
	public static class WUtils
	{
		public static string ReadFile (Assembly assembly, string resourceId)
		{
			Stream stream = assembly.GetManifestResourceStream(resourceId);
			string text = "";
			using (var reader = new System.IO.StreamReader(stream))
			{
				text = reader.ReadToEnd();
			}
			return text;
		}
	}
}
