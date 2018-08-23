using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Wapps.Core
{
    public class Group<T> : ObservableCollection<T>
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Tag { get; set; }

        public object BindingItem { get; set; }
    }

    public class GroupList<T> : ObservableCollection<Group<T>>
    {
        public static GroupList<T> GroupBy(IList<T> orig, Func<T, object> funcGroupBinding, Func<T, object> funcGroupProp = null)
        {
            IOrderedEnumerable<T> ordered;

            if (funcGroupProp == null)
                ordered = orig.OrderBy(funcGroupBinding);
            else
                ordered = orig.OrderBy(funcGroupProp);

            var groupList = new GroupList<T>();

            Group<T> group = null;

            foreach (var p in ordered)
            {
                var groupValue = (funcGroupProp != null ? funcGroupProp(p) : funcGroupBinding(p));
                var groupBind = funcGroupBinding(p);

                if (group == null || (group.BindingItem != null && groupBind != null && false == group.BindingItem.Equals(groupBind)))
                {
                    group = new Group<T> { BindingItem = groupBind, Title = groupValue.ToString() };
                    groupList.Add(group);
                }

                group.Add(p);
            }

            return groupList;
        }

        public static GroupList<T> IndexGroupBy(IList<T> orig, Func<T, object> funcGroupProp)
        {
            var ordered = orig.OrderBy(funcGroupProp);

            var groupList = new GroupList<T>();

            Group<T> group = null;

            foreach (var p in ordered)
            {
                var groupValue = funcGroupProp(p).ToString().First().ToString().ToUpper();

                if (group == null || group.Title != groupValue)
                {
                    group = new Group<T> { Title = groupValue };
                    groupList.Add(group);
                }

                group.Add(p);
            }

            return groupList;
        }
    }
}
