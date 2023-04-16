using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WoWDeveloperAssistant.Conditions_Creator;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Misc
{
    public static class Utils
    {
        public enum BuildVersions : uint
        {
            BUILD_UNKNOWN,
            BUILD_8_0_1,
            BUILD_8_1_0,
            BUILD_8_1_5,
            BUILD_8_2_0,
            BUILD_8_2_5,
            BUILD_8_3_0,
            BUILD_8_3_7,
            BUILD_9_0_1,
            BUILD_9_0_2,
            BUILD_9_0_5,
            BUILD_9_1_0,
            BUILD_9_1_5,
            BUILD_9_2_0,
            BUILD_9_2_5,
            BUILD_9_2_7,
            BUILD_10_0_2,
            BUILD_10_0_5,
            BUILD_10_0_7
        };

        public static string GetValueWithoutComma(this float value)
        {
            return value.ToString().Replace(",", ".");
        }

        public static void RecalculateIdsAndGuids(this IEnumerable<Waypoint> list, uint baseId)
        {
            uint id = 0;
            uint guid = 0;

            var scriptIdsDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery($"SELECT MAX(id) FROM `waypoint_scripts` WHERE `id` LIKE '{baseId}%';") : null;
            var scriptGuidsDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery($"SELECT MAX(guid) FROM `waypoint_scripts` WHERE `id` LIKE '{baseId}%';") : null;

            if (scriptIdsDs != null && scriptIdsDs.Tables["table"].Rows.Count != 0 && scriptIdsDs.Tables["table"].Rows[0][0] != DBNull.Value)
            {
                id = Convert.ToUInt32(scriptIdsDs.Tables["table"].Rows[0][0]) + 1;
            }

            if (scriptGuidsDs != null && scriptGuidsDs.Tables["table"].Rows.Count != 0 && scriptGuidsDs.Tables["table"].Rows[0][0] != DBNull.Value)
            {
                guid = Convert.ToUInt32(scriptGuidsDs.Tables["table"].Rows[0][0]) + 1;
            }

            if (id == 0 || guid == 0)
            {
                id = baseId * 100;
                guid = id;
            }

            foreach (Waypoint waypoint in list)
            {
                if (waypoint.HasScripts())
                {
                    foreach (WaypointScript script in waypoint.scripts)
                    {
                        script.SetId(id);
                        script.SetGuid(guid);
                        guid++;
                    }

                    id++;
                }
            }
        }

        public static string GetScriptIds(this IEnumerable<Waypoint> list)
        {
            string ids = "";

            List<uint> scriptIds = (from waypoint in list where waypoint.HasScripts() select waypoint.GetScriptId()).ToList();

            for (int i = 0; i < scriptIds.Count; i++)
            {
                if (i + 1 < scriptIds.Count)
                    ids += scriptIds[i] + ", ";
                else
                    ids += scriptIds[i];
            }

            return ids;
        }

        public static uint GetScriptsCount(this IEnumerable<Waypoint> list)
        {
            return list.Where(waypoint => waypoint.scripts.Count > 0).Aggregate<Waypoint, uint>(0, (current, waypoint) => current + (uint) waypoint.scripts.Count);
        }

        public static uint GetPointsWithScriptsCount(this IEnumerable<Waypoint> list)
        {
            uint pointsCount = 0;

            foreach (var waypoint in list.Where(waypoint => waypoint.scripts.Count > 0))
            {
                pointsCount++;
            }

            return pointsCount;
        }

        public static Waypoint GetLastWaypointWithTime(this List<Waypoint> list, TimeSpan time)
        {
            Waypoint waypoint = new Waypoint();
            bool loopStopped = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].moveStartTime == time)
                {
                    do
                    {
                        if (i + 1 < list.Count && list[i + 1].moveStartTime != time)
                        {
                            waypoint = list[i];
                            loopStopped = true;
                            break;
                        }

                        if (i + 1 > list.Count)
                        {
                            loopStopped = true;
                            break;
                        }

                        i++;
                    }
                    while (!loopStopped);
                }

                if (loopStopped)
                    break;
            }

            return waypoint;
        }

        public static UpdateObjectPacket? GetUpdatePacketForCreatureWithGuid(this IEnumerable<object> list, string guid)
        {
            foreach (var updateObjectPacket in list.Cast<UpdateObjectPacket>().Where(updateObjectPacket => updateObjectPacket.guid == guid))
            {
                return updateObjectPacket;
            }

            return null;
        }

        public static uint GetObjectstWithTypeCount(this IEnumerable<Packet> list, Packet.PacketTypes type)
        {
            uint packetsCount = 0;

            foreach (var packet in list.Where(packet => packet.packetType == type))
            {
                packetsCount++;
            }

            return packetsCount;
        }

        public static void AddSourceFromEmotePacket(this SortedDictionary<long, Packet> dict, EmotePacket emotePacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_EMOTE && packet.index == index))
            {
                packet.parsedPacketsList.Add(emotePacket);
                return;
            }
        }

        public static void AddSourceFromAuraUpdatePacket(this SortedDictionary<long, Packet> dict, AuraUpdatePacket auraPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_AURA_UPDATE && packet.index == index))
            {
                packet.parsedPacketsList.Add(auraPacket);
                return;
            }
        }

        public static void AddSourceFromSpellPacket(this SortedDictionary<long, Packet> dict, SpellStartPacket spellPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_SPELL_START && packet.index == index))
            {
                packet.parsedPacketsList.Add(spellPacket);
                return;
            }
        }

        public static void AddSourceFromMovementPacket(this SortedDictionary<long, Packet> dict, MonsterMovePacket movementPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_ON_MONSTER_MOVE && packet.index == index))
            {
                packet.parsedPacketsList.Add(movementPacket);
                return;
            }
        }

        public static void AddSourceFromUpdatePacket(this SortedDictionary<long, Packet> dict, UpdateObjectPacket updatePacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_UPDATE_OBJECT && packet.index == index))
            {
                packet.parsedPacketsList.Add(updatePacket);
                return;
            }
        }

        public static void AddSourceFromAttackStopPacket(this SortedDictionary<long, Packet> dict, AttackStopPacket attackStopPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_ATTACK_STOP && packet.index == index))
            {
                packet.parsedPacketsList.Add(attackStopPacket);
                return;
            }
        }

        public static void AddSourceFromSetAiAnimKitPacket(this SortedDictionary<long, Packet> dict, SetAiAnimKitPacket animKitPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_SET_AI_ANIM_KIT && packet.index == index))
            {
                packet.parsedPacketsList.Add(animKitPacket);
                return;
            }
        }

        public static void AddSourceFromPlayOneShotAnimKitPacket(this SortedDictionary<long, Packet> dict, PlayOneShotAnimKitPacket playOneShotAnimKitPacket, long index)
        {
            foreach (var packet in dict.Values.Where(packet => packet.packetType == Packet.PacketTypes.SMSG_PLAY_ONE_SHOT_ANIM_KIT && packet.index == index))
            {
                packet.parsedPacketsList.Add(playOneShotAnimKitPacket);
                return;
            }
        }

        public static bool ContainPacketWithIndex(this IEnumerable<Packet> list, long index)
        {
            return list.Any(packet => packet.index == index);
        }

        public static List<Packet> GetPacketsForCreatureWithGuid(this IEnumerable<Packet> list, string guid)
        {
            return list.Where(packet => packet.HasCreatureWithGuid(guid)).ToList();
        }

        public static string GetTimeWithoutMilliseconds(this TimeSpan span)
        {
            return $"{span.Hours}:{span.Minutes}:{span.Seconds}";
        }

        public static string ToFormattedString(this TimeSpan span)
        {
            return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
        }

        public static string ToFormattedStringWithMilliseconds(this TimeSpan span)
        {
            return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}:{span.Milliseconds}";
        }

        public static TimeSpan GetMinTimeSpanFromList(IEnumerable<TimeSpan> timeSpanList)
        {
            List<TimeSpan> sortedList = new List<TimeSpan>(from time in timeSpanList orderby time.TotalSeconds ascending select time);

            if (sortedList.Count != 0)
                return sortedList[0];

            return new TimeSpan();
        }

        public static TimeSpan GetMaxTimeSpanFromList(IEnumerable<TimeSpan> timeSpanList)
        {
            List<TimeSpan> sortedList = new List<TimeSpan>(from time in timeSpanList orderby time.TotalSeconds descending select time);

            if (sortedList.Count != 0)
                return sortedList[0];

            return new TimeSpan();
        }

        public static TimeSpan GetAverageTimeSpanFromList(List<TimeSpan> timeSpanList)
        {
            int average = timeSpanList.Sum(time => (int) time.TotalSeconds);

            return new TimeSpan(0, 0, average / timeSpanList.Count);
        }

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public static string AddSpacesCount(uint count)
        {
            string spaces = "";

            for (uint i = 0; i < count; i++)
            {
                spaces += ' ';
            }

            return spaces;
        }

        public static uint GetMapIdForTransport(uint transportEntry)
        {
            var mapIdDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery($"SELECT `data6` FROM `gameobject_template` WHERE `entry` = {transportEntry};") : null;

            if (mapIdDs != null)
                return Convert.ToUInt16(mapIdDs.Tables["table"].Rows[0][0].ToString());

            return 0;
        }

        public static Dictionary<uint, string> GetCreatureNamesFromDB()
        {
            Dictionary<uint, string> namesDict = new Dictionary<uint, string>();

            var creatureNameDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery("SELECT `entry`, `Name1` FROM `creature_template_wdb`;") : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    namesDict.Add((uint)row[0], row[1].ToString());
                }
            }

            return namesDict;
        }

        public static Dictionary<uint, string> GetQuestNamesFromDB()
        {
            Dictionary<uint, string> namesDict = new Dictionary<uint, string>();

            var creatureNameDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery("SELECT `ID`, `LogTitle` FROM `quest_template`;") : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    namesDict.Add((uint)row[0], row[1].ToString());
                }
            }

            return namesDict;
        }

        public static bool IsTxtFileValidForParse(string fileName, string[] lines, BuildVersions buildVersion)
        {
            if (lines[0] != "# TrinityCore - WowPacketParser")
            {
                MessageBox.Show(fileName + " is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }

        public static string GetCreatureEntries(this Dictionary<string, Creature> creatures)
        {
            string output = "";

            foreach (uint entry in creatures.Select(x => x.Value.entry).Distinct())
            {
                output += $"{entry}, ";
            }

            return output.Remove(output.Length - 2);
        }
    }
}
