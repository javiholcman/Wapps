using System;

namespace Wapps.Core
{
    public class RequiredRule : IRule
    {
        public FieldErrorResult Validate(Field target)
        {
            if (target.Value == null)
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsRequired);

            if (target.Value is string && ((string)target.Value) == "")
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsInvalid);

            return null;
        }
    }

    public static class RequiredRuleExtension
    {
        public static Rules Required(this Rules rules)
        {
            var requiredRule = new RequiredRule();
            rules.Add(requiredRule);
            return rules;
        }
    }
}
