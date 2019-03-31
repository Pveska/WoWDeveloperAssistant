using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WoWDeveloperAssistant.Misc
{
    public static class Utils
    {
        public enum BuildVersions : uint
        {
            BUILD_UNKNOWN = 0,
            BUILD_8_0_1   = 1,
            BUILD_8_1_0   = 2,
            BUILD_8_1_5   = 3
        };

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

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
