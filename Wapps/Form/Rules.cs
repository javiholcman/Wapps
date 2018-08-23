using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wapps.Core
{
    public class Rules
    {
        public Field Target { get; set; }

        List<IRule> _rules = new List<IRule>();

        public T FindRule<T>() where T : IRule
        {
            return (T)_rules.Find((obj) => obj.GetType() == typeof(T));
        }

        public List<FieldErrorResult> Validate()
        {
            var errors = new List<FieldErrorResult>();
            foreach (var rule in _rules)
            {
                var error = rule.Validate(Target);
                if (error != null)
                {
                    errors.Add(error);
                }
            }
            return errors;
        }

        public bool IsValid()
        {
            return this.Validate().Count == 0;
        }

        public void Add(IRule rule)
        {
            _rules.Add(rule);
        }

        public static Rules Create()
        {
            return new Rules();
        }
    }

    public interface IRule
    {
        FieldErrorResult Validate(Field target);
    }

}
