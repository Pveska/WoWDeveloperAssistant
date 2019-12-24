using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Misc
{
    public static class LineGetters
    {
        public static string GetGuidFromLine(string line, BuildVersions buidVersion, bool objectFieldGuid = false, bool unitGuid = false, bool senderGuid = false, bool moverGuid = false, bool attackerGuid = false, bool casterGuid = false, bool updateAuraGuid = false)
        {
            if (!line.Contains("Creature/0") && !line.Contains("Vehicle/0"))
                return "";

            if (objectFieldGuid && buidVersion == BuildVersions.BUILD_8_0_1)
            {
                Regex guidRegex = new Regex(@"OBJECT_FIELD_GUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("OBJECT_FIELD_GUID: Full: ", "");
            }
            else if (unitGuid)
            {
                Regex guidRegex = new Regex(@"UnitGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("UnitGUID: Full: ", "");
            }
            else if (senderGuid)
            {
                Regex guidRegex = new Regex(@"SenderGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("SenderGUID: Full: ", "");
            }
            else if (moverGuid)
            {
                Regex guidRegex = new Regex(@"MoverGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("MoverGUID: Full: ", "");
            }
            else if (attackerGuid)
            {
                Regex guidRegex = new Regex(@"Attacker Guid: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Attacker Guid: Full: ", "");
            }
            else if (casterGuid)
            {
                Regex guidRegex = new Regex(@"CasterGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("CasterGUID: Full: ", "");
            }
            else
            {
                Regex guidRegex = new Regex(@"ObjectGuid: Full:{1}\s*\w{20,}");
                Regex guidRegexSecond = new Regex(@"Object GUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("ObjectGuid: Full: ", "");
                if (guidRegexSecond.IsMatch(line))
                    return guidRegexSecond.Match(line).ToString().Replace("Object GUID: Full: ", "");

            }

            return "";
        }

        public static TimeSpan GetTimeSpanFromLine(string timeSpanLine)
        {
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int milliseconds = 0;

            Regex dateRegex = new Regex(@"Time:{1}\s+\d+/+\d+/+\d+");
            Regex timeRegex = new Regex(@"\d+:+\d+:+.+\s{1}Number");

            if (dateRegex.IsMatch(timeSpanLine))
            {
                string[] date = dateRegex.Match(timeSpanLine).ToString().Replace("Time: ", "").Split('/');

                days = Convert.ToInt32(date[1]);
            }

            if (timeRegex.IsMatch(timeSpanLine))
            {
                string[] time = timeRegex.Match(timeSpanLine).ToString().Replace(" Number", "").Split(':');

                hours = Convert.ToInt32(time[0]);
                minutes = Convert.ToInt32(time[1]);

                string tempTime = time[2];
                string[] splittedTime = tempTime.Split('.');

                seconds = Convert.ToInt32(splittedTime[0]);
                milliseconds = Convert.ToInt32(splittedTime[1]);
            }

            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }

        public static bool IsCreatureLine(string updateTypeLine)
        {
            if (updateTypeLine.Contains("ObjectGuid: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("SenderGUID: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("MoverGUID: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("Attacker Guid: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            return false;
        }

        public static string GetPacketTimeFromStringInSeconds(string line)
        {
            Regex timeRegex = new Regex(@"\d+:+\d+:+\d+");

            if (timeRegex.IsMatch(line))
            {
                TimePacket packet;
                string[] splittedLine = timeRegex.Match(line).ToString().Split(':');

                packet.hours = splittedLine[0];
                packet.minutes = splittedLine[1];
                packet.seconds = splittedLine[2];

                return ((Convert.ToUInt64(packet.hours) * 3600) + (Convert.ToUInt64(packet.minutes) * 60) + Convert.ToUInt64(packet.seconds)).ToString();
            }

            return "";
        }

        public static BuildVersions GetBuildVersion(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                if (line.Contains("Detected build:"))
                {
                    if (line.Contains("V8_0_1"))
                        return BuildVersions.BUILD_8_0_1;
                    if (line.Contains("V8_1_0"))
                        return BuildVersions.BUILD_8_1_0;
                    if (line.Contains("V8_1_5"))
                        return BuildVersions.BUILD_8_1_5;
                    if (line.Contains("V8_2_0"))
                        return BuildVersions.BUILD_8_2_0;
                    if (line.Contains("V8_2_5"))
                        return BuildVersions.BUILD_8_2_5;
                    if (line.Contains("V8_3_0"))
                        return BuildVersions.BUILD_8_3_0;
                    return BuildVersions.BUILD_UNKNOWN;
                }
            }

            return BuildVersions.BUILD_UNKNOWN;
        }

        public static string GetAreatriggerEntryFromLine(string line)
        {
            Regex entryRegex = new Regex(@"Entry:{1}\s*\d+");

            if (entryRegex.IsMatch(line))
            {
                return entryRegex.Match(line).ToString().Replace("Entry: ", "");
            }

            return "";
        }
    }
}
