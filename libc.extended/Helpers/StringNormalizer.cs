using System.Collections.Generic;
using System.Text;
namespace libc.extended.Helpers {
    public static class StringNormalizer {
        private static readonly IDictionary<char, char> map;
        static StringNormalizer() {
            map = new Dictionary<char, char> {
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9',
                ['۰'] = '0',
                ['ك'] = 'ک',
                ['ي'] = 'ی'
            };
        }
        public static string Run(string input) {
            if (string.IsNullOrWhiteSpace(input)) return input;
            var res = new StringBuilder();
            foreach (var ch in input) {
                var item = ch;
                if (map.ContainsKey(item)) item = map[item];
                res.Append(item);
            }
            return res.ToString();
        }
    }
}