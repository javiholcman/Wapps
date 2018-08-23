using System;
using System.Text.RegularExpressions;

namespace Wapps.Core
{
    public class RegExpRule : IRule
    {
        string _regExp;

        public RegExpRule(string regExp)
        {
            _regExp = regExp;
        }

        public FieldErrorResult Validate(Field target)
        {
            string validEmailPattern = _regExp;
            Regex regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty((string)target.Value))
                return null;

            Match match = regex.Match((string)target.Value);

            if (!match.Success)
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsInvalid);

            return null;
        }
    }

    public static class RegExpRuleExtension
    {
        public static Rules RegExp(this Rules rules, string regExp)
        {
            var regExpRule = new RegExpRule(regExp);
            rules.Add(regExpRule);
            return rules;
        }
    }
}
