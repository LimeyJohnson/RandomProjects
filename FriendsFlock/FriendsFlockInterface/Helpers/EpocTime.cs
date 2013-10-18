using System;

namespace EpocTimeHelper
{
    public static class EpocTime
    {
        public static DateTime EpocToDateTime(long epocTime)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(epocTime);

            TimeSpan timeSpan = DateTime.UtcNow - dt;

            return dt;
        }

        public static TimeSpan EpocToTimeSpan(long epoctime)
        {
            DateTime dt = EpocToDateTime(epoctime);

            TimeSpan timeSpan = DateTime.UtcNow - dt;

            return timeSpan;
        }

        public static string TimeSpanString(TimeSpan timeSpan)
        {
            string timeSpanString;

            if (timeSpan.TotalDays > 1)
            {
                if (timeSpan.TotalDays > 2)
                    timeSpanString = string.Format("Updated {0} days ago", timeSpan.Days);
                else
                    timeSpanString = string.Format("Updated {0} day ago", timeSpan.Days);
            }
            else if (timeSpan.TotalHours > 1)
            {
                if (timeSpan.TotalHours > 2)
                    timeSpanString = string.Format("Updated {0} hours ago", timeSpan.Hours);
                else
                    timeSpanString = string.Format("Updated {0} hours ago", timeSpan.Hours);
            }
            else if (timeSpan.TotalMinutes > 1)
            {
                if (timeSpan.TotalMinutes > 2)
                    timeSpanString = string.Format("Updated {0} minutes ago", timeSpan.Minutes);
                else
                    timeSpanString = string.Format("Updated {0} minute ago", timeSpan.Minutes);
            }
            else
                timeSpanString = string.Format("Updated {0} seconds ago", timeSpan.Seconds);

            return timeSpanString;
        }
    }
}