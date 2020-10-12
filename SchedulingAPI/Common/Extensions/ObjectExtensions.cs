
using System.Linq;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetNested(this object prop, string val)
        {
            if (!val.Contains("."))
                return prop.GetType().GetProperty(val).GetValue(prop);
            var split = val.Split(".");

            return GetNested(prop.GetType().GetProperty(split.FirstOrDefault()).GetValue(prop), string.Join(".", split.Skip(1)));
        }
    }
}
