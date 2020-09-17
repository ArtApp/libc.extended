using System;
using System.Collections.Generic;
using System.Linq;
using libc.extended.Extensions;
namespace libc.extended.EnumerationHelpers {
    public static class EnumExtensions {
        public static IEnumerable<Enum> GetFlags(this Enum input) {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
        public static T MergeFlags<T>(this IEnumerable<T> flags) {
            var list = flags
                .Distinct()
                .Select(a => a.ToString())
                .ConcatString(",");
            return (T) Enum.Parse(typeof(T), list, true);
        }
        public static string ConvertFlags(this Enum input) {
            var k = input.GetFlags().Select(a => a.Convert());
            return k.ConcatString("|");
        }
        public static string Convert(this Enum n) {
            return EnumDescriptionTypeConverter.Convert(n);
        }
    }
}