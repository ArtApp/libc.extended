using System.ComponentModel;
using libc.extended.EnumerationHelpers;

namespace libc.extended.Dating
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Calendars
    {
        [Description("Gregorian")]
        Gregorian,

        [Description("PersianArithmetic")]
        Persian
    }
}