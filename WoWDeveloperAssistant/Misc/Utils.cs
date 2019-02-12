using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WoWDeveloperAssistant.Misc
{
    public static class Utils
    {
        public static string ToFormattedString(this TimeSpan span)
        {
            return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
        }

        public static TimeSpan GetMinTimeSpanFromList(List<TimeSpan> timeSpanList)
        {
            List<TimeSpan> sortedList = new List<TimeSpan>(from time in timeSpanList orderby time.TotalSeconds ascending select time);

            if (sortedList.Count != 0)
                return sortedList[0];

            return new TimeSpan();
        }

        public static TimeSpan GetMaxTimeSpanFromList(List<TimeSpan> timeSpanList)
        {
            List<TimeSpan> sortedList = new List<TimeSpan>(from time in timeSpanList orderby time.TotalSeconds descending select time);

            if (sortedList.Count != 0)
                return sortedList[0];

            return new TimeSpan();
        }

        public static TimeSpan GetAverageTimeSpanFromList(List<TimeSpan> timeSpanList)
        {
            int average = 0;

            foreach (TimeSpan time in timeSpanList)
            {
                average += (int)time.TotalSeconds;
            }

            return new TimeSpan(0, 0, average / timeSpanList.Count);
        }
    }
}
