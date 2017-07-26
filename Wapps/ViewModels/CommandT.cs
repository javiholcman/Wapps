using System;
using System.Reflection;

namespace Wapps.ViewModels
{
    public class Command<T> : Command
    {
        //
        // Constructors
        //
        public Command(Action<T> execute) : base(delegate (object o)
        {
            if (Command<T>.IsValidParameter(o))
            {
                execute((T)((object)o));
            }
        })
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
        }

        public Command(Action<T> execute, Func<T, bool> canExecute) : base(delegate (object o)
        {
            if (Command<T>.IsValidParameter(o))
            {
                execute((T)((object)o));
            }
        }, (object o) => Command<T>.IsValidParameter(o) && canExecute((T)((object)o)))
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
        }

        //
        // Static Methods
        //
        private static bool IsValidParameter(object o)
        {
            if (o != null)
            {
                return o is T;
            }
            Type typeFromHandle = typeof(T);
            return Nullable.GetUnderlyingType(typeFromHandle) != null || !typeFromHandle.GetTypeInfo().IsValueType;
        }
    }
}
