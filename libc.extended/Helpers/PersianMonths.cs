using System;
using System.Collections.Generic;
using libc.extended.BoolOperations;
namespace libc.extended.Helpers {
    public static class PersianMonths {
        private static readonly Dictionary<string, (int num, Func<string, bool> resolver)> dic =
            new Dictionary<string, (int num, Func<string, bool> resolver)> {
                {
                    "فروردین", (1, s => {
                        return new OrBool()
                            .Add(s.Contains("فرو"))
                            .Add(s.Contains("فـرو"))
                            .Add(s.Contains("دین"))
                            .Add(s.Contains("دیـن"))
                            .Add(s.Contains("رور"))
                            .Value();
                    })
                }, {
                    "اردیبهشت", (2, s => {
                        return new OrBool()
                            .Add(s.Contains("بهشت"))
                            .Add(s.Contains("بـهشت"))
                            .Add(s.Contains("بهـشت"))
                            .Add(s.Contains("بهشـت"))
                            .Add(s.Contains("بـهـشت"))
                            .Add(s.Contains("بـهشـت"))
                            .Add(s.Contains("بهـشـت"))
                            .Add(s.Contains("هشت"))
                            .Add(s.Contains("هـشت"))
                            .Add(s.Contains("هشـت"))
                            .Add(s.Contains("هـشـت"))
                            .Add(s.Contains("شت"))
                            .Add(s.Contains("شـت"))
                            .Add(s.Contains("ارد"))
                            .Add(s.Contains("دیب"))
                            .Add(s.Contains("دیـب"))
                            .Value();
                    })
                }, {
                    "خرداد", (3, s => {
                        return new OrBool()
                            .Add(s.Contains("خردا"))
                            .Add(s.Contains("خـردا"))
                            .Add(s.Contains("خرد"))
                            .Add(s.Contains("خـرد"))
                            .Add(s.Contains("خر"))
                            .Add(s.Contains("خـر"))
                            .Add(s.Contains("خ"))
                            .Value();
                    })
                }, {
                    "تیر", (4, s => {
                        return new OrBool()
                            .Add(s.Contains("یر"))
                            .Add(s.Contains("یـر"))
                            .Add(s.Contains("تـی"))
                            .Value();
                    })
                }, {
                    "مرداد", (5, s => {
                        return new OrBool()
                            .Add(s.Contains("مردا"))
                            .Add(s.Contains("مرد"))
                            .Add(s.Contains("مر"))
                            .Add(s.Contains("مـردا"))
                            .Add(s.Contains("مـرد"))
                            .Add(s.Contains("مـر"))
                            .Value();
                    })
                }, {
                    "شهریور", (6, s => {
                        return new OrBool()
                            .Add(s.Contains("ریور"))
                            .Add(s.Contains("ریـور"))
                            .Add(s.Contains("شهر"))
                            .Add(s.Contains("شـهر"))
                            .Add(s.Contains("شهـر"))
                            .Add(s.Contains("شـهـر"))
                            .Add(s.Contains("شهری"))
                            .Add(s.Contains("شه"))
                            .Add(s.Contains("شـه"))
                            .Add(s.Contains("ریو"))
                            .Add(s.Contains("ریـو"))
                            .Add(s.Contains("ری"))
                            .Value();
                    })
                }, {
                    "مهر", (7, s => {
                        return new OrBool()
                            .Add(s.Contains("مه"))
                            .Add(s.Contains("مـه"))
                            .Add(s.Contains("مهـر"))
                            .Add(s.Contains("مـهـر"))
                            .Value();
                    })
                }, {
                    "آبان", (8, s => {
                        return new OrBool()
                            .Add(s.Contains("آب"))
                            .Add(s.Contains("اب"))
                            .Add(s.Contains("بان"))
                            .Add(s.Contains("بـان"))
                            .Add(s.Contains("ان"))
                            .Add(s.Contains("با"))
                            .Add(s.Contains("بـا"))
                            .Value();
                    })
                }, {
                    "آذر", (9, s => {
                        return new OrBool()
                            .Add(s.Contains("آذ"))
                            .Add(s.Contains("اذ"))
                            .Add(s.Contains("ذر"))
                            .Value();
                    })
                }, {
                    "دی", (10, s => {
                        return false;
                    })
                }, {
                    "بهمن", (11, s => {
                        return new OrBool()
                            .Add(s.Contains("همن"))
                            .Add(s.Contains("هـمن"))
                            .Add(s.Contains("هـمـن"))
                            .Add(s.Contains("همـن"))
                            .Add(s.Contains("مـن"))
                            .Add(s.Contains("من"))
                            .Add(s.Contains("بهم"))
                            .Add(s.Contains("بـهم"))
                            .Add(s.Contains("بـهـم"))
                            .Add(s.Contains("بهـم"))
                            .Value();
                    })
                }, {
                    "اسفند", (12, s => {
                        return new OrBool()
                            .Add(s.Contains("اس"))
                            .Add(s.Contains("اسـ"))
                            .Add(s.Contains("فند"))
                            .Add(s.Contains("فـند"))
                            .Add(s.Contains("فنـد"))
                            .Add(s.Contains("فـنـد"))
                            .Add(s.Contains("سف"))
                            .Add(s.Contains("سـف"))
                            .Add(s.Contains("سـفـ"))
                            .Value();
                    })
                }
            };
        public static bool GetMonth(string monthString, out int monthNumber, out string monthName) {
            if (!string.IsNullOrWhiteSpace(monthString)) {
                var month = StringNormalizer.Run(monthString.Trim());
                foreach (var item in dic)
                    if (item.Key.Equals(month, StringComparison.InvariantCultureIgnoreCase) ||
                        item.Value.resolver(month)) {
                        monthNumber = item.Value.num;
                        monthName = item.Key;
                        return true;
                    }
            }
            monthNumber = -1;
            monthName = null;
            return false;
        }
    }
}