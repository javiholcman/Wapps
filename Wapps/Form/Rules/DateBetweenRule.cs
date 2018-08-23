using System;

namespace Wapps.Core
{
    public class DateBetweenRule : IRule
    {
        public DateTime Min { get; set; }

        public DateTime Max { get; set; }

        public DateBetweenRule(DateTime min, DateTime max)
        {
            Min = min;
            Max = max;
        }

        public FieldErrorResult Validate(Field target)
        {
            if (target.Value == null)
                return null;

            if (((DateTime)target.Value) < Min)
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsInvalid);

            if (((DateTime)target.Value) > Max)
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsInvalid);

            return null;
        }

    }

    public static class DateBetweenRuleExtension
    {
        public static Rules DateBetween(this Rules rules, DateTime min, DateTime max)
        {
            var dateBetweenRule = new DateBetweenRule(min, max);
            rules.Add(dateBetweenRule);
            return rules;
        }
    }
}
