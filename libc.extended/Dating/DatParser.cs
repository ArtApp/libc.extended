using System;
using System.Linq;
using libc.extended.Resources;
namespace libc.extended.Dating {
    public class DatParser : IDatParser {
        public bool TryParse(string dateTime, ZoneInfo zoneInfo, out Dat res, out Exception exception,
            string dateDelimiter = "/", string timeDelimiter = ":") {
            return tryParse(zoneInfo, dateTime, out res, out exception, dateDelimiter, timeDelimiter);
        }
        private static bool tryParse(ZoneInfo zoneInfo, string text,
            out Dat res, out Exception exception, string dateDelimiter = "/", string timeDelimiter = ":") {
            exception = null;
            try {
                var parts = text.Split(' ');
                switch (parts.Length) {
                    case 2: {
                        var dateStr = parts[0];
                        if (!parseDate(dateStr, out var year, out var month,
                            out var day, dateDelimiter)) {
                            res = null;
                            return false;
                        }
                        var timeStr = parts[1];
                        if (!parseTime(timeStr, out var hour, out var minute,
                            out var second, timeDelimiter)) {
                            res = null;
                            return false;
                        }
                        res = new Dat(zoneInfo, year, month, day, hour,
                            minute, second);
                        return res.IsValid();
                    }
                    case 1: {
                        var dateStr = parts[0];
                        if (!parseDate(dateStr, out var year, out var month,
                            out var day, dateDelimiter)) {
                            res = null;
                            return false;
                        }
                        res = new Dat(zoneInfo, year, month, day);
                        return res.IsValid();
                    }
                }
                throw new Exception(Tran.Instance.Get("SpaceNotFoundInDateString"));
            } catch (Exception ex) {
                exception = ex;
                res = null;
                return false;
            }
        }
        private static bool parseTime(string timeStr, out int hour, out int minute, out int second,
            string timeDelimiter = ":") {
            var s = timeStr.Trim();
            if (s.Contains(timeDelimiter)) {
                var parts = s.Split(new[] {
                    timeDelimiter
                }, StringSplitOptions.RemoveEmptyEntries);
                switch (parts.Length) {
                    case 2: {
                        // [hour]:[minute]
                        second = 0;
                        var m = new[] {
                            int.TryParse(parts[0], out hour),
                            int.TryParse(parts[1], out minute)
                        };
                        return m.Aggregate(true, (b, b1) => b && b1);
                    }
                    case 3: {
                        // [hour]:[minute]:[second]
                        var m = new[] {
                            int.TryParse(parts[0], out hour),
                            int.TryParse(parts[1], out minute),
                            int.TryParse(parts[2], out second)
                        };
                        return m.Aggregate(true, (b, b1) => b && b1);
                    }
                }
                throw new ArgumentException(nameof(timeStr));
            }

            //[hours]
            minute = 0;
            second = 0;
            return int.TryParse(s, out hour);
        }
        private static bool parseDate(string dateStr, out int year, out int month, out int day,
            string dateDelimiter = "/") {
            var s = dateStr.Trim();
            if (s.Contains(dateDelimiter)) {
                var parts = s.Split(new[] {
                    dateDelimiter
                }, StringSplitOptions.RemoveEmptyEntries);
                switch (parts.Length) {
                    case 2: {
                        // [year]/[month]
                        day = 0;
                        var m = new[] {
                            int.TryParse(parts[0], out year),
                            int.TryParse(parts[1], out month)
                        };
                        return m.Aggregate(true, (b, b1) => b && b1);
                    }
                    case 3: {
                        // [year]/[month]/[day]
                        var m = new[] {
                            int.TryParse(parts[0], out year),
                            int.TryParse(parts[1], out month),
                            int.TryParse(parts[2], out day)
                        };
                        return m.Aggregate(true, (b, b1) => b && b1);
                    }
                }
                throw new ArgumentException(nameof(dateStr));
            }

            //[year]
            month = 0;
            day = 0;
            return int.TryParse(s, out year);
        }
    }
}