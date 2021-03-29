using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class WaypointsHelper
    {
        public static void CreateReturnPath(TextBox textBox)
        {
            if (!textBox.Text.Contains("@PATH"))
                return;

            // Getting array with strings that contains points only
            List<string> pointStrings = textBox.Text.Split("\r\n".ToCharArray()).Where(x => x.Contains("(@PATH, ")).ToList();

            // Add new line for each of point strings and remove any colons
            for (int i = 0; i < pointStrings.Count; i++)
            {
                pointStrings[i] = pointStrings[i].Replace(");", "),") + "\r\n";
            }

            // Calculate return path
            for (int i = pointStrings.Count - 2; i > 0; i--)
            {
                if (GetPositionFromPointLine(pointStrings[i]) != GetPositionFromPointLine(pointStrings.First()))
                {
                    pointStrings.Add(pointStrings[i]);
                }
                else
                    break;
            }

            // Recalculate point ids
            for (int i = 1; i <= pointStrings.Count; i++)
            {
                Regex pointIdRegex = new Regex(@"@PATH,\s{1}\d+");
                if (pointIdRegex.IsMatch(pointStrings[i - 1]))
                {
                    pointStrings[i - 1] = pointStrings[i - 1].Replace(pointIdRegex.Match(pointStrings[i - 1]).ToString(), "@PATH, " + i);
                }
            }

            // Add colon for last point string
            pointStrings[pointStrings.Count - 1] = pointStrings.Last().Replace("),", ");");

            // Clear all strings on TextBox that contains points data
            textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf("(@PATH"));

            // Add points to TextBox
            foreach (string pointString in pointStrings)
            {
                textBox.Text += pointString;
            }
        }

        public static void RecalculatePointIds(TextBox textBox)
        {
            if (!textBox.Text.Contains("@PATH"))
                return;

            List<string> pointStrings = textBox.Text.Split("\r\n".ToCharArray()).Where(x => x.Contains("(@PATH, ")).ToList();

            // Add new line for each of point strings and remove any colons
            for (int i = 0; i < pointStrings.Count; i++)
            {
                pointStrings[i] = pointStrings[i].Replace(");", "),") + "\r\n";
            }

            // Recalculate point ids
            for (int i = 1; i <= pointStrings.Count; i++)
            {
                Regex pointIdRegex = new Regex(@"@PATH,\s{1}\d+");
                if (pointIdRegex.IsMatch(pointStrings[i - 1]))
                {
                    pointStrings[i - 1] = pointStrings[i - 1].Replace(pointIdRegex.Match(pointStrings[i - 1]).ToString(), "@PATH, " + i);
                }
            }

            // Add colon for last point string
            pointStrings[pointStrings.Count - 1] = pointStrings.Last().Replace("),", ");");

            // Clear all strings on TextBox that contains points data
            textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf("(@PATH"));

            // Add recalculated points to TextBox
            foreach (string pointString in pointStrings)
            {
                textBox.Text += pointString;
            }
        }

        private static Position GetPositionFromPointLine(string line)
        {
            return new Position(GetRoundedValueFromStringCoordinate(line.Split(',')[2]), GetRoundedValueFromStringCoordinate(line.Split(',')[3]), GetRoundedValueFromStringCoordinate(line.Split(',')[4]));
        }

        private static float GetRoundedValueFromStringCoordinate(string coor)
        {
            return float.Parse(Math.Round(float.Parse(coor.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat)).ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
