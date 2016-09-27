using System;
using System.Reflection;

namespace MayLily.DataAccess.FluentMigrator
{
    public static class ReflectionUtils
    {
        public static TResult CreateDelegateFor<TType, TResult>(string methodName, Type[] types)
            where TResult : class
        {
            return CreateDelegateFor<TType, TResult>(methodName, types, BindingFlags.Public | BindingFlags.Instance);
        }

        public static TResult CreateDelegateFor<TType, TResult>(string methodName, Type[] types, BindingFlags bindingFlags)
            where TResult : class
        {
            var methodInfo = typeof(TType).GetMethod(methodName, bindingFlags, null, types, null);
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Unable to find method with the provided name '{0}'.".Fmt(methodName));
            }

            return Delegate.CreateDelegate(typeof(TResult), methodInfo) as TResult;
        }

        public static TResult CreateDelegateFor<TResult>(Type type, string methodName, Type[] types, BindingFlags bindingFlags)
            where TResult : class
        {
            var methodInfo = type.GetMethod(methodName, bindingFlags, null, types, null);
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Unable to find method with the provided name '{0}'.".Fmt(methodName));
            }

            return Delegate.CreateDelegate(typeof(TResult), methodInfo) as TResult;
        }

        public static TResult CreateDelegateFor<TResult>(Type type, string methodName)
            where TResult : class
        {
            var methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Unable to find method with the provided name '{0}'.".Fmt(methodName));
            }

            return Delegate.CreateDelegate(typeof(TResult), methodInfo) as TResult;
        }
    }
}
