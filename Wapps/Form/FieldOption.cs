using System;
using System.Collections.Generic;

namespace Wapps.Core
{
    public class FieldOption
    {
        public string Text { get; set; }

        public string Icon { get; set; }

        public object Source { get; set; }

        public FieldOption()
        {

        }

        public FieldOption(object source, string text, string icon = null)
        {
            Source = source;
            Text = text;
            Icon = icon;
        }
    }

    public static class IEnumerable_FieldOptions
    {
        public static List<FieldOption> ToFieldOptions<T>(this IEnumerable<T> source, Func<T, FieldOption> predicate)
        {
            var options = new List<FieldOption>();
            foreach (var item in source)
            {
                var option = predicate.Invoke(item);
                options.Add(option);
            }
            return options;
        }
    }

}
