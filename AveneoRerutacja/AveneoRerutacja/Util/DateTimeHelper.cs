using System;

namespace AveneoRerutacja.Util
{
    public static class DateTimeHelper
    {
        public static bool IsDateTimeNull(DateTime date) => date.ToString("yyyy-MM-dd") == "0001-01-01";
    }
}