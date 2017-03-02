using System;
using System.Collections.Generic;
using System.Linq;
using Wapps.ViewModels.Results;

namespace Wapps.ViewModels
{
	public class Form
	{
		public Form()
		{
		}

		public List<Field> Fields { get; set; }

		public virtual List<ViewModelResult> Validate()
		{
			var results = new List<ViewModelResult>();
			foreach (var field in Fields)
			{
				if (!field.IsEnabled)
					continue;
				
				var result = field.Validate();
				if (result != null)
				{
					results.Add(result);
				}
			}
			return results;
		}

		public virtual bool IsValid()
		{
			var results = Validate();
			if (results.Count == 0)
			{
				return true;
			}
			else if (results.Count == results.Where(p => p is SuccessResult).Count())
			{
				return true;
			}
			else {
				return false;
			}
		}

	}
}
