using System;
using Wapps.ViewModels;
using Wapps.ViewModels.Results;

namespace Wapps.Validations
{
	public class LengthRule : IRule
	{
		public int Min { get; set; }

		public int Max { get; set; }

		public LengthRule(int min, int max)
		{
			Min = min;
			Max = max;
		}

		public FieldErrorResult Validate(Field target)
		{
			if (string.IsNullOrEmpty((string)target.Value))
				return null;

			if (target.Value.ToString().Length < Min)
				return new FieldErrorResult(target.Title, RulesLocalizations.Current.TooShort);

			if (target.Value.ToString().Length > Max)
				return new FieldErrorResult(target.Title, RulesLocalizations.Current.TooLong);

			return null;
		}
	}

	public static class LengthRuleExtension
	{
		public static Rules Length(this Rules rules, int min, int max)
		{
			var lengthRule = new LengthRule(min, max);
			rules.Add(lengthRule);
			return rules;
		}
	}
}
