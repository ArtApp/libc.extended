using System;
using NodaTime;
namespace libc.extended.Dating {
    public static class NodaTimeExt {
        /// <summary>
        ///     Returns DateTimeZone
        /// </summary>
        /// <param name="zoneId">[Continent]/[City] e.g: Asia/Tehran, Europe/London</param>
        /// <returns></returns>
        public static DateTimeZone GetZone(this string zoneId) {
            return DateTimeZoneProviders.Tzdb[zoneId];
        }
        /// <summary>
        ///     Returns CalendarSystem
        /// </summary>
        /// <param name="cal"></param>
        /// <returns></returns>
        public static CalendarSystem GetCalendarSystem(this Calendars cal) {
            if (cal == Calendars.Gregorian) return CalendarSystem.Gregorian;
            if (cal == Calendars.Persian) return CalendarSystem.PersianArithmetic;
            throw new ArgumentOutOfRangeException(nameof(cal));
        }
        public static Calendars GetCalendar(this CalendarSystem cs) {
            if (cs.Id == CalendarSystem.PersianArithmetic.Id) return Calendars.Persian;
            if (cs.Id == CalendarSystem.Gregorian.Id) return Calendars.Gregorian;
            throw new ArgumentOutOfRangeException(nameof(cs));
        }
    }
}