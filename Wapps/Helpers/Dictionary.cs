using System;
using System.Collections.Generic;

namespace Wapps.Core
{
    public class Dictionary : Dictionary<string, object>
    {
        public Dictionary()
        {
        }

        public Dictionary(string key, object value)
        {
            this[key] = value;
        }

        public Dictionary(string key, object value, string key2, object value2)
        {
            this[key] = value;
            this[key2] = value2;
        }

        public new object this[string key]
        {
            get
            {
                if (!ContainsKey(key))
                    return null;
                else
                    return base[key];
            }
            set { base[key] = value; }
        }
    }

    public static class DictionaryExtension
    {
        public static object GetOrNull(this Dictionary<string, object> sender, string key)
        {
            if (!sender.ContainsKey(key))
                return null;
            else
                return sender[key];
        }
    }
}

