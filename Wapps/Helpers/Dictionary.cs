using System;
using System.Collections.Generic;

namespace Wapps
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
    }
}

