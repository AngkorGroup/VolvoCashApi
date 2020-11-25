
using System;

namespace VolvoCash.CrossCutting.Utils
{
    public class DateTimeParser
    {
        public static DateTime ParseString(string dateAsString,string dateFormat)
        {
            return DateTime.ParseExact(dateAsString, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static DateTime? TryParseString(string dateAsString, string dateFormat)
        {
            if (!string.IsNullOrEmpty(dateAsString))
            {
                return DateTime.ParseExact(dateAsString, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}
