using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string value) => string.Join("", value.FirstOrDefault().ToString().ToUpper().Concat(value.Skip(1)));
        public static bool ContainsIgnoreCase(this string bValue, string value) => bValue.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        public static string CamelCaseToKebabCase(this string value, string format = "{0}") => string.Format(format, string.Join("_", Regex.Split(value, @"(?<!^)(?=[A-Z])").Select(v => v.ToLower())));
    }
}
