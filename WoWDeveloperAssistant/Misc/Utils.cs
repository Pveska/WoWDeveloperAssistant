using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Misc
{
    public static class Utils
    {
        public enum BuildVersions : uint
        {
            BUILD_UNKNOWN = 0,
            BUILD_8_0_1   = 1,
            BUILD_8_1_0   = 2,
            BUILD_8_1_5   = 3,
            BUILD_8_2_0   = 4,
            BUILD_8_2_5   = 5,
            BUILD_8_3_0   = 6,
            BUILD_8_3_7   = 7,
            BUILD_9_0_1   = 8, ///< Prepatch
            BUILD_9_0_2   = 9, ///< Shadowlands
        };

        public static string GetValueWithoutComma(this float value)
        {
            return value.ToString().Replace(",", ".");
        }

        public static void RecalculateIdsAndGuids(this IEnumerable<Waypoint> list, uint baseId)
        {
            uint id = baseId * 100;
            uint guid = id;

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
            foreach (var updateObjectPacket in list.Cast<UpdateObjectPacket>().Where(updateObjectPacket => updateObjectPacket.creatureGuid == guid))
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
    }
}
