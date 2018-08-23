using System;
using System.Collections.Generic;

namespace Wapps.Core
{
    public class FieldMultiple : Dictionary<string, Field>
    {
        public bool IsEnabled { get; set; } = true;

        public virtual bool IsValid()
        {
            return false;
        }

        public FieldMultiple()
        {
        }
    }
}
