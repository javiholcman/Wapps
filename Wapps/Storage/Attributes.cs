using System;
using System.Collections.Generic;
using SQLite;

namespace Wapps.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ManyToOneAttribute : IgnoreAttribute
    {
        public string FkId { get; set; }

        public ManyToOneAttribute(string fkId)
        {
            FkId = fkId;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class OneToAttribute : IgnoreAttribute
    {
        public string FkId { get; set; }

        public OneToAttribute(string fkId)
        {
            FkId = fkId;
        }
    }
}

