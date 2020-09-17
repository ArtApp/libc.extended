using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace libc.extended.Extensions {
    public static class StringExtensions {
        public static string RemoveStartingDirectorySeparator(this string s) {
            return s.RemoveStarting("/").RemoveStarting("\\");
        }
        public static string ChangeNumbers(this string input) {
            var persian = new[] {
                "۰",
                "۱",
                "۲",
                "۳",
                "۴",
                "۵",
                "۶",
                "۷",
                "۸",
                "۹"
            };
            for (var j = 0; j < persian.Length; j++) input = input.Replace(persian[j], j.ToString());
            return input;
        }
        public static bool Has(this string s, string substr) {
            return s?.Contains(substr) ?? false;
        }
        public static bool Starts(this string s, string sub) {
            return s?.StartsWith(sub) ?? false;
        }
        public static string ConcatString<T>(this IEnumerable<T> list,
            string delimiter, string startString = "", string endString = "") {
            var res = new StringBuilder("");
            var k = list?.ToList() ?? new List<T>();
            if (k.Any()) {
                foreach (var o in k) res.AppendFormat("{0}{1}", o, delimiter);
                res.Remove(res.Length - delimiter.Length, delimiter.Length);
            }
            res.Insert(0, startString);
            res.Append(endString);
            return res.ToString();
        }
        public static string RemoveStarting(this string s, string starting) {
            if (string.IsNullOrEmpty(s)) return s;
            return s.StartsWith(starting) ? s.Substring(starting.Length) : s;
        }
        public static string RemoveStarting(this string s, char starting) {
            if (string.IsNullOrEmpty(s)) return s;
            return s[0] == starting ? s.Substring(1) : s;
        }
        public static string RemoveEnding(this string s, string str) {
            if (string.IsNullOrWhiteSpace(s)) return s;
            if (s.EndsWith(str)) return s.Substring(0, s.Length - str.Length);
            return s;
        }
        public static string Shorten(this string val, int len) {
            if (string.IsNullOrEmpty(val) || val.Length <= len) return val;
            return $"{val.Substring(0, len)}...";
        }
        public static string StartingFrom(this string s, string from) {
            if (s == null || from == null) return null;
            var i = s.IndexOf(from);
            if (i >= 0)
                try {
                    return s.Substring(i);
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            return null;
        }
        public static string StartingAfter(this string s, string after) {
            if (s == null || after == null) return null;
            var i = s.IndexOf(after);
            if (i >= 0)
                try {
                    return s.Substring(i + after.Length);
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            return null;
        }
        public static string GetBetween(this string s, string first, string end) {
            if (s == null || first == null || end == null) return null;
            var a = s.IndexOf(first);
            if (a == -1) return null;
            var i1 = a + first.Length;
            var i2 = s.IndexOf(end, i1);
            if (i2 == -1 || i2 < i1) return null;
            if (i2 == i1) return string.Empty;
            var res = s.Substring(i1, i2 - i1);
            return res;
        }
        public static List<int> Indices(this string s, string find) {
            var res = new List<int>();
            if (s != null && find != null)
                while (true) {
                    var i = s.IndexOf(find);
                    if (i >= 0) {
                        res.Add(i);
                        s = s.Substring(i + find.Length);
                    } else {
                        break;
                    }
                }
            return res;
        }
        public static bool IsAbsoluteUrl(this string url) {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}