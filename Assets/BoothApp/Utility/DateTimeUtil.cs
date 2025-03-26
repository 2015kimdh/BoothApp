using System;

namespace BoothApp.Utility
{
    public static class DateTimeUtil
    {
        public static string DateTimeNowToString()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
        }
        
        public static DateTime DateTimeStringToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "yyyy-MM-ddTHH\\:mm\\:sszzz", null);
        }
    }
}