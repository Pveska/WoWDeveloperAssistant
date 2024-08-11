using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Misc
{
    public static class LineGetters
    {
        public static string GetGuidFromLine(string line, BuildVersions buidVersion, bool objectFieldGuid = false, bool unitGuid = false, bool senderGuid = false, bool moverGuid = false, bool attackerGuid = false, bool casterGuid = false, bool casterUnit = false, bool transportGuid = false, bool conversationActorGuid = false)
        {
            if (!line.Contains("TypeName: Creature; Full:") && !line.Contains("TypeName: Vehicle; Full:") && !line.Contains("TypeName: Player; Full:") && !line.Contains("TypeName: Transport; Full:"))
                return "";

            Regex objectTypeRegex = new Regex(@"[a-zA-Z]+;{1}\s{1}Full:{1}\s");

            if (objectFieldGuid && buidVersion == BuildVersions.BUILD_8_0_1)
            {
                Regex guidRegex = new Regex(@"OBJECT_FIELD_GUID: Full:{1}\s{1}[a-zA-Z]+;{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("OBJECT_FIELD_GUID: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (unitGuid)
            {
                Regex guidRegex = new Regex(@"UnitGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("UnitGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (senderGuid)
            {
                Regex guidRegex = new Regex(@"SenderGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("SenderGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (moverGuid)
            {
                Regex guidRegex = new Regex(@"MoverGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("MoverGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (attackerGuid)
            {
                Regex guidRegex = new Regex(@"Attacker Guid: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Attacker Guid: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (casterGuid)
            {
                Regex guidRegex = new Regex(@"CasterGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("CasterGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (casterUnit)
            {
                Regex guidRegex = new Regex(@"CasterUnit: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("CasterUnit: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (transportGuid)
            {
                Regex guidRegex = new Regex(@"TransportGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("TransportGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else if (conversationActorGuid)
            {
                Regex guidRegex = new Regex(@"ActorGUID: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("ActorGUID: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
            }
            else
            {
                Regex guidRegex = new Regex(@"ObjectGuid: TypeName:{1}\s{1}[a-zA-Z]+;{1}\s{1}Full:{1}\s{1}\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("ObjectGuid: TypeName: ", "").Replace(objectTypeRegex.Match(line).ToString(), "");
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
            if ((updateTypeLine.Contains("Creature") || updateTypeLine.Contains("Vehicle")) &&
                (updateTypeLine.Contains("ObjectGuid:") || updateTypeLine.Contains("SenderGUID:") ||
                updateTypeLine.Contains("MoverGUID:") || updateTypeLine.Contains("Attacker Guid:")))
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
                    else if (line.Contains("V8_1_0"))
                        return BuildVersions.BUILD_8_1_0;
                    else if (line.Contains("V8_1_5"))
                        return BuildVersions.BUILD_8_1_5;
                    else if (line.Contains("V8_2_0"))
                        return BuildVersions.BUILD_8_2_0;
                    else if (line.Contains("V8_2_5"))
                        return BuildVersions.BUILD_8_2_5;
                    else if (line.Contains("V8_3_0"))
                        return BuildVersions.BUILD_8_3_0;
                    else if (line.Contains("V8_3_7"))
                        return BuildVersions.BUILD_8_3_7;
                    else if (line.Contains("V9_0_1"))
                        return BuildVersions.BUILD_9_0_1;
                    else if (line.Contains("V9_0_2"))
                        return BuildVersions.BUILD_9_0_2;
                    else if (line.Contains("V9_0_5"))
                        return BuildVersions.BUILD_9_0_5;
                    else if (line.Contains("V9_1_0"))
                        return BuildVersions.BUILD_9_1_0;
                    else if (line.Contains("V9_1_5"))
                        return BuildVersions.BUILD_9_1_5;
                    else if (line.Contains("V9_2_0"))
                        return BuildVersions.BUILD_9_2_0;
                    else if (line.Contains("V9_2_5"))
                        return BuildVersions.BUILD_9_2_5;
                    else if (line.Contains("V9_2_7"))
                        return BuildVersions.BUILD_9_2_7;
                    else if (line.Contains("V10_0_2"))
                        return BuildVersions.BUILD_10_0_2;
                    else if (line.Contains("V10_0_5"))
                        return BuildVersions.BUILD_10_0_5;
                    else if (line.Contains("V10_0_7"))
                        return BuildVersions.BUILD_10_0_7;
                    else if (line.Contains("V10_1_0"))
                        return BuildVersions.BUILD_10_1_0;
                    else if (line.Contains("V10_1_5"))
                        return BuildVersions.BUILD_10_1_5;
                    else if (line.Contains("V10_1_7"))
                        return BuildVersions.BUILD_10_1_7;
                    else if (line.Contains("V10_2_0"))
                        return BuildVersions.BUILD_10_2_0;
                    else if (line.Contains("V10_2_5"))
                        return BuildVersions.BUILD_10_2_5;
                    else if (line.Contains("V10_2_6"))
                        return BuildVersions.BUILD_10_2_6;
                    else if (line.Contains("V10_2_7"))
                        return BuildVersions.BUILD_10_2_7;
                    else if (line.Contains("11_0_2"))
                        return BuildVersions.BUILD_10_2_7;

                    return BuildVersions.BUILD_UNKNOWN;
                }
            }

            return BuildVersions.BUILD_UNKNOWN;
        }

        public static string GetAreatriggerEntryFromLine(string line)
        {
            Regex entryRegex = new Regex(@"Entry:{1}\s*\d+");

            if (entryRegex.IsMatch(line))
                return entryRegex.Match(line).ToString().Replace("Entry: ", "");

            return "";
        }

        public static string GetSpellIdFromLine(string line)
        {
            Regex entryRegex = new Regex(@"SpellID:{1}\s*\d+");

            if (entryRegex.IsMatch(line))
                return entryRegex.Match(line).ToString().Replace("SpellID: ", "");

            return "";
        }

        public static long GetPacketNumberFromLine(string line)
        {
            Regex numberRegex = new Regex(@"Number:{1}\s{1}\w+");
            if (numberRegex.IsMatch(line))
                return Convert.ToInt64(numberRegex.Match(line).ToString().Replace("Number: ", ""));

            return 0;
        }

        public static bool IsConversationLine(this string value)
        {
            if (value.Contains("ObjectGuid: TypeName: Conversation; Full:"))
                return true;

            return false;
        }

        public static string GetLinkedIdFromLine(string line)
        {
            Regex linkedIdRegex = new Regex(@"'\S+'");

            if (linkedIdRegex.IsMatch(line))
                return linkedIdRegex.Match(line).ToString().Replace("'", "");

            return "";
        }

        public static uint GetEntryFromLine(string line)
        {
            Regex entryRegex = new Regex(@"'\S+', \w+");

            if (entryRegex.IsMatch(line))
                return Convert.ToUInt32(entryRegex.Match(line).ToString().Split(' ')[1]);

            return 0;
        }

        public static uint GetZoneIdFromLine(string line)
        {
            Regex zoneIdRegex = new Regex(@"\w+, \w+, '.+'{1}");

            if (zoneIdRegex.IsMatch(line))
                return Convert.ToUInt32(zoneIdRegex.Match(line).ToString().Split(',')[0].Replace(",", ""));

            return 0;
        }

        public static uint GetPhaseIdFromLine(string line)
        {
            Regex phaseIdRegex = new Regex(@" '.+'{1}, \w{1}, \w+");

            if (phaseIdRegex.IsMatch(line))
                return Convert.ToUInt32(phaseIdRegex.Match(line).ToString().Split(',')[2].Replace(" ", ""));

            return 0;
        }

        public static string GetCreatureNameFromLine(string line)
        {
            Regex creatureNameByAreaRegex = new Regex(@"-- .+ \(Area:");
            Regex creatureNameByObjectGuidRegex = new Regex(@"-- .+ \(ObjectGuid:");

            if (line.Contains("ObjectGuid"))
            {
                if (creatureNameByObjectGuidRegex.IsMatch(line))
                    return creatureNameByObjectGuidRegex.Match(line).ToString().Replace("--  ", "").Replace("-- ", "").Replace(" (ObjectGuid:", "").Replace("(", "").Replace(")", "");
            }
            else
            {
                if (creatureNameByAreaRegex.IsMatch(line))
                    return creatureNameByAreaRegex.Match(line).ToString().Replace("--  ", "").Replace("-- ", "").Replace(" (Area:", "").Replace("(", "").Replace(")", "");
            }


            return "";
        }

        public static uint GetMapIdFromLine(string line)
        {
            Regex mapIdRegex = new Regex(@"'\S+', \w+, \w+");

            if (mapIdRegex.IsMatch(line))
                return Convert.ToUInt32(mapIdRegex.Match(line).ToString().Split(' ')[2]);

            return 0;
        }
    }
}
