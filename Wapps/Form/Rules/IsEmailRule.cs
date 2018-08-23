using System;
using System.Text.RegularExpressions;

namespace Wapps.Core
{
    public class IsEmailRule : IRule
    {
        public FieldErrorResult Validate(Field target)
        {
            string validEmailPattern = "^(?!\\.)(\"([^\"\\r\\\\]|\\\\[\"\\r\\\\])*\"|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\\.)\\.)*)(?<!\\.)@[a-z0-9][\\w\\.-]*[a-z0-9]\\.[a-z][a-z\\.]*[a-z]$";
            Regex regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty((string)target.Value))
                return null;

            Match match = regex.Match((string)target.Value);

            if (!match.Success)
                return new FieldErrorResult(target.Title, RulesLocalizations.Current.IsInvalid);

            return null;
        }
    }

    public static class IsEmailRuleExtension
    {
        public static Rules IsEmail(this Rules rules)
        {
            var isEmailRule = new IsEmailRule();
            rules.Add(isEmailRule);
            return rules;
        }
    }
}
