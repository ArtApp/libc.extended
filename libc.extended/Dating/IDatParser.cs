using System;

namespace libc.extended.Dating
{
    public interface IDatParser
    {
        bool TryParse(string dateTime, ZoneInfo zoneInfo, out Dat res, out Exception exception,
            string dateDelimiter = "/", string timeDelimiter = ":");
    }
}