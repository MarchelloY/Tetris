using System;

namespace Utils
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> action, T args)
        {
            action?.Invoke(args);
        }

        public static void SafeInvoke<T, TK>(this Action<T, TK> action, T args1, TK args2)
        {
            action?.Invoke(args1, args2);
        }

        public static void SafeInvoke<T, TK, TM>(this Action<T, TK, TM> action, T args1, TK args2, TM args3)
        {
            action?.Invoke(args1, args2, args3);
        }
    }
}