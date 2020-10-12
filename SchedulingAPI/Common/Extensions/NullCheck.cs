using System;

namespace Common.Extensions
{
    public static class NullCheck
    {
        public static T IfNull<T>(this T obj)
        {
            if (obj == null) return (T)Activator.CreateInstance(typeof(T));
            return obj;
        }

        public static int Value(this int? val) => val.HasValue ? val.Value : 0;
        public static long Value(this long? val) => val.HasValue ? val.Value : 0;
        public static decimal Value(this decimal? val) => val.HasValue ? val.Value : 0;
    }
}
