using System;
using System.Text.RegularExpressions;
using static WoWDeveloperAssistant.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant
{
    public static class LineGetters
    {
        public static string GetGuidFromLine(string line, BuildVersions buidVersion, bool objectFieldGuid = false, bool unitGuid = false, bool senderGuid = false, bool moverGuid = false, bool attackerGuid = false, bool casterGuid = false)
        {
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
                Regex guidRegex = new Regex(@"Object Guid: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Object Guid: Full: ", "");
            }

            return "";
        }

        public static TimeSpan GetTimeSpanFromLine(string dateLine)
        {
            string[] time;

            Regex timeRegex = new Regex(@"\d+:+\d+:+\d+");
            if (timeRegex.IsMatch(dateLine))
            {
                time = timeRegex.Match(dateLine).ToString().Split(':');
            }
            else
                return new TimeSpan();

            return new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
        }

        public static bool IsCreatureLine(string updateTypeLine)
        {
            if (updateTypeLine.Contains("Object Guid: Full:") &&
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
            Regex timeRegex = timeRegex = new Regex(@"\d+:+\d+:+\d+");

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

        public static BuildVersions GetBuildVersion(string[] lines)
        {
            foreach (var line in lines)
            {
                if (line.Contains("Detected build:"))
                {
                    if (line.Contains("V8_0_1"))
                        return BuildVersions.BUILD_8_0_1;
                    else if (line.Contains("V8_1_0"))
                        return BuildVersions.BUILD_8_1_0;
                    else if (line.Contains("V8_1_5"))
                        return BuildVersions.BUILD_8_1_5;
                    else
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
