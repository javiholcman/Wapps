using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Wapps.Core
{
    public static class Dynamic
    {
        public static bool HasMember(object dynObj, string memberName)
        {
            return GetMemberNames(dynObj).Contains(memberName);
        }

        public static IEnumerable<string> GetMemberNames(object dynObj)
        {
            var metaObjProvider = dynObj as IDynamicMetaObjectProvider;

            if (null == metaObjProvider) throw new InvalidOperationException(
                "The supplied object must be a dynamic object " +
                "(i.e. it must implement IDynamicMetaObjectProvider)"
            );

            var metaObj = metaObjProvider.GetMetaObject(
                Expression.Constant(metaObjProvider)
            );

            var memberNames = metaObj.GetDynamicMemberNames();

            return memberNames;
        }
    }
}
