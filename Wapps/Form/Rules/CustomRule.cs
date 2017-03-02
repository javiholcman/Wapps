using System;
using Wapps.ViewModels;
using Wapps.ViewModels.Results;

namespace Wapps.Validations
{
	public class CustomRule : IRule
	{
		ValidationDelegate _validation;

		public CustomRule(ValidationDelegate validation)
		{
			_validation = validation;
		}

		public FieldErrorResult Validate(Field target)
		{
			return (FieldErrorResult)_validation(target);
		}
	}

	public static class CustomRuleExtension
	{
		public static Rules Custom(this Rules rules, ValidationDelegate validation)
		{
			var customRule = new CustomRule(validation);
			rules.Add(customRule);
			return rules;
		}
	}
}
