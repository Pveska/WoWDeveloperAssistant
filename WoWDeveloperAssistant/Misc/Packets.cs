using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Parsed_File_Advisor;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Database_Advisor.CreatureFlagsAdvisor;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Misc
{
    [ProtoContract]
    public enum MoveType
    {
        [ProtoEnum]
        MOVE_UNKNOWN = 0,

        [ProtoEnum]
        MOVE_WALK    = 1,

        [ProtoEnum]
        MOVE_RUN     = 2,

        [ProtoEnum]
        MOVE_FLIGHT  = 3
    }

    [ProtoContract]
    public enum PacketType : byte
    {
        [ProtoEnum]
        UNKNOWN_PACKET = 0,

        [ProtoEnum]
        SMSG_UPDATE_OBJECT = 1,

        [ProtoEnum]
        SMSG_AI_REACTION = 2,

        [ProtoEnum]
        SMSG_SPELL_START = 3,

        [ProtoEnum]
        SMSG_CHAT = 4,

        [ProtoEnum]
        SMSG_ON_MONSTER_MOVE = 5,

        [ProtoEnum]
        SMSG_ATTACK_STOP = 6,

        [ProtoEnum]
        SMSG_AURA_UPDATE = 7,

        [ProtoEnum]
        SMSG_EMOTE = 8,

        [ProtoEnum]
        SMSG_SPELL_GO = 9,

        [ProtoEnum]
        SMSG_SET_AI_ANIM_KIT = 10,

        [ProtoEnum]
        CMSG_QUEST_GIVER_ACCEPT_QUEST = 11,

        [ProtoEnum]
        SMSG_QUEST_GIVER_QUEST_COMPLETE = 12,

        [ProtoEnum]
        CMSG_MOVE_START_FORWARD = 13,

        [ProtoEnum]
        CMSG_MOVE_STOP = 14,

        [ProtoEnum]
        CMSG_MOVE_HEARTBEAT = 15,

        [ProtoEnum]
        SMSG_QUEST_UPDATE_ADD_CREDIT = 16,

        [ProtoEnum]
        SMSG_QUEST_UPDATE_COMPLETE = 17,

        [ProtoEnum]
        SMSG_PLAY_ONE_SHOT_ANIM_KIT = 18,

        [ProtoEnum]
        SMSG_INIT_WORLD_STATES = 19
    }

    [ProtoContract]
    public enum PacketUpdateType
    {
        [ProtoEnum]
        Unknown = 0,

        [ProtoEnum]
        CreateObject = 1,

        [ProtoEnum]
        Values = 2,

        [ProtoEnum]
        Destroy = 3
    }

    [ProtoContract]
    public enum ObjectTypes
    {
        [ProtoEnum]
        Unknown = 0,

        [ProtoEnum]
        Creature = 1,

        [ProtoEnum]
        GameObject = 2,

        [ProtoEnum]
        Player = 3,

        [ProtoEnum]
        Conversation = 4
    }

    public static class Packets
    {
        [Serializable]
        public struct Packet
        {
            public PacketType packetType;
            public TimeSpan sendTime;
            public long index;
            public List<object> parsedPacketsList;
            public long number;

            public Packet(PacketType type, TimeSpan time, long index, List<object> parsedList, long number)
            { packetType = type; sendTime = time; this.index = index; parsedPacketsList = parsedList; this.number = number; }

            public static PacketType GetPacketTypeFromLine(string line)
            {
                foreach (string packetName in Enum.GetNames(typeof(PacketType)))
                {
                    if (line.Contains(packetName))
                    {
                        Regex packetnameRegex = new Regex(packetName + @"{1}\s+");
                        if (packetnameRegex.IsMatch(line))
                            return (PacketType)Enum.Parse(typeof(PacketType), packetnameRegex.Match(line).ToString());
                    }
                }

                return PacketType.UNKNOWN_PACKET;
            }

            public bool HasCreatureWithGuid(string guid)
            {
                if (parsedPacketsList.Count == 0)
                    return false;

                switch (packetType)
                {
                    case PacketType.SMSG_UPDATE_OBJECT:
                    {
                        if (parsedPacketsList.Cast<UpdateObjectPacket>().Any(updatePacket => updatePacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_ON_MONSTER_MOVE:
                    {
                        if (parsedPacketsList.Cast<MonsterMovePacket>().Any(movementPacket => movementPacket.creatureGuid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_SPELL_START:
                    {
                        if (parsedPacketsList.Cast<SpellPacket>().Any(spellPacket => spellPacket.casterGuid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_AURA_UPDATE:
                    {
                        if (parsedPacketsList.Cast<AuraUpdatePacket>().Any(auraPacket => auraPacket.unitGuid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_EMOTE:
                    {
                        if (parsedPacketsList.Cast<EmotePacket>().Any(emotePacket => emotePacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_ATTACK_STOP:
                    {
                        if (parsedPacketsList.Cast<AttackStopPacket>().Any(attackStopPacket => attackStopPacket.creatureGuid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_SET_AI_ANIM_KIT:
                    {
                        if (parsedPacketsList.Cast<SetAiAnimKitPacket>().Any(animKitPacket => animKitPacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketType.SMSG_PLAY_ONE_SHOT_ANIM_KIT:
                    {
                        if (parsedPacketsList.Cast<PlayOneShotAnimKitPacket>().Any(playOneShotAnimKitPacket => playOneShotAnimKitPacket.guid == guid))
                            return true;

                        break;
                    }
                    default:
                        return false;
                }

                return false;
            }

            public bool IsScriptPacket()
            {
                switch (packetType)
                {
                    case PacketType.SMSG_UPDATE_OBJECT:
                    case PacketType.SMSG_SPELL_START:
                        return true;
                    default:
                        return false;
                }
            }

            public static bool IsPlayerMovePacket(PacketType packetType)
            {
                return packetType == PacketType.CMSG_MOVE_START_FORWARD || packetType == PacketType.CMSG_MOVE_STOP || packetType == PacketType.CMSG_MOVE_HEARTBEAT;
            }

            public static bool IsQuestPacket(PacketType packetType)
            {
                return packetType == PacketType.CMSG_QUEST_GIVER_ACCEPT_QUEST || packetType == PacketType.SMSG_QUEST_GIVER_QUEST_COMPLETE || packetType == PacketType.SMSG_QUEST_UPDATE_ADD_CREDIT || packetType == PacketType.SMSG_QUEST_UPDATE_COMPLETE;
            }
        }

        [ProtoContract]
        public class SpellPacket
        {
            [ProtoMember(1)]
            public string casterGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint spellId
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan spellCastTime
            {
                get; set;
            }

            [ProtoMember(4)]
            public TimeSpan spellCastStartTime
            {
                get; set;
            }

            [ProtoMember(5)]
            public Position spellDestination
            {
                get; set;
            }

            [ProtoMember(6)]
            public PacketType type
            {
                get; set;
            }

            [ProtoMember(7)]
            public string targetGuid
            {
                get; set;
            }

            public SpellPacket() { }

            public SpellPacket(string guid, uint id, TimeSpan castTime, TimeSpan startTime, Position spellDest, PacketType type)
            { casterGuid = guid; spellId = id; spellCastTime = castTime; spellCastStartTime = startTime; spellDestination = spellDest; this.type = type; }

            public static uint GetSpellIdFromLine(string line)
            {
                Regex spellIdRegex = new Regex(@"SpellID:{1}\s*\d+");
                if (spellIdRegex.IsMatch(line))
                    return Convert.ToUInt32(spellIdRegex.Match(line).ToString().Replace("SpellID: ", ""));

                return 0;
            }

            public static TimeSpan GetCastTimeFromLine(string line)
            {
                Regex castTimeRegex = new Regex(@"CastTime:{1}\s*\d+");
                int.TryParse(castTimeRegex.Match(line).ToString().Replace("CastTime: ", ""), out int castTime);
                if (castTimeRegex.IsMatch(line) && castTime != 0)
                    return new TimeSpan(0, 0, 0, 0, castTime);

                return new TimeSpan();
            }

            public static Position GetSpellDestinationFromLine(string line)
            {
                Position destPosition = new Position();

                Regex xyzRegex = new Regex(@"Location:\s{1}X:{1}\s{1}");
                if (xyzRegex.IsMatch(line))
                {
                    string[] splittedLine = line.Split(' ');

                    destPosition.x = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                    destPosition.y = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                    destPosition.z = float.Parse(splittedLine[9], CultureInfo.InvariantCulture.NumberFormat);
                }

                return destPosition;
            }

            public static bool IsCreatureSpellCastLine(string line)
            {
                return line.Contains("CasterGUID: TypeName: Creature;") || line.Contains("CasterGUID: TypeName: Vehicle;");
            }

            public static bool IsPlayerSpellCastLine(string firstLine, string secondLine)
            {
                return firstLine.Contains("CasterGUID: TypeName: Player;") || secondLine.Contains("CasterUnit: TypeName: Player;");
            }

            public static SpellPacket ParseSpellPacket(string[] lines, long index, BuildVersions buildVersion, PacketType type, bool playerPacket = false)
            {
                SpellPacket spellPacket = new SpellPacket("", 0, new TimeSpan(), LineGetters.GetTimeSpanFromLine(lines[index]), new Position(), type);

                if (playerPacket)
                {
                    if (IsPlayerSpellCastLine(lines[index + 1], lines[index + 2]))
                    {
                        string casterGuid = "";

                        do
                        {
                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true) != "" && casterGuid == "")
                                casterGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true);

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, casterUnit: true) != "" && casterGuid == "")
                                casterGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, casterUnit: true);

                            if (GetSpellIdFromLine(lines[index]) != 0)
                                spellPacket.spellId = GetSpellIdFromLine(lines[index]);

                            if (GetCastTimeFromLine(lines[index]) != TimeSpan.Zero)
                                spellPacket.spellCastTime = GetCastTimeFromLine(lines[index]);

                            if (GetSpellDestinationFromLine(lines[index]).IsValid())
                                spellPacket.spellDestination = GetSpellDestinationFromLine(lines[index]);

                            index++;
                        }
                        while (lines[index] != "");

                        spellPacket.casterGuid = casterGuid;
                    }
                }
                else
                {
                    if (IsCreatureSpellCastLine(lines[index + 1]))
                    {
                        do
                        {
                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true) != "")
                                spellPacket.casterGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true);

                            if (GetSpellIdFromLine(lines[index]) != 0)
                                spellPacket.spellId = GetSpellIdFromLine(lines[index]);

                            if (GetCastTimeFromLine(lines[index]) != TimeSpan.Zero)
                                spellPacket.spellCastTime = GetCastTimeFromLine(lines[index]);

                            if (GetSpellDestinationFromLine(lines[index]).IsValid())
                                spellPacket.spellDestination = GetSpellDestinationFromLine(lines[index]);

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, targetUnit: true) != "")
                                spellPacket.targetGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, targetUnit: true);

                            index++;
                        }
                        while (lines[index] != "");
                    }
                }

                return spellPacket;
            }
        };

        [ProtoContract]
        public class ChatPacket
        {
            [ProtoMember(1)]
            public string creatureGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint creatureEntry
            {
                get; set;
            }

            [ProtoMember(3)]
            public string creatureText
            {
                get; set;
            }

            [ProtoMember(4)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public ChatPacket() { }

            public ChatPacket(string guid, uint entry, string text, TimeSpan time)
            { creatureGuid = guid; creatureEntry = entry; creatureText = text; packetSendTime = time; }

            public static bool IsCreatureText(string line)
            {
                if (line.Contains("SlashCmd: 12 (MonsterSay)"))
                    return true;

                return false;
            }

            public static string GetTextFromLine(string line)
            {
                if (line.Contains("Text:"))
                    return line.Replace("Text: ", "");

                return "";
            }

            public static ChatPacket ParseChatPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                ChatPacket chatPacket = new ChatPacket("", 0, "", LineGetters.GetTimeSpanFromLine(lines[index]));

                if (IsCreatureText(lines[index + 1]))
                {
                    do
                    {
                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion, senderGuid: true) != "")
                            chatPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, senderGuid: true);

                        if (GetTextFromLine(lines[index]) != "")
                            chatPacket.creatureText = GetTextFromLine(lines[index]);

                        index++;
                    }
                    while (lines[index] != "");

                    chatPacket.creatureEntry = CreatureScriptsCreator.GetCreatureEntryByGuid(chatPacket.creatureGuid);
                    if (chatPacket.creatureEntry == 0)
                    {
                        chatPacket.creatureEntry = ParsedFileAdvisor.GetCreatureEntryByGuid(chatPacket.creatureGuid);
                    }
                }

                return chatPacket;
            }
        }

        [ProtoContract]
        public class UpdateObjectPacket
        {
            [ProtoMember(1)]
            public ObjectTypes objectType
            {
                get; set;
            } = ObjectTypes.Unknown;

            [ProtoMember(2)]
            public PacketUpdateType updateType
            {
                get; set;
            } = PacketUpdateType.Unknown;

            [ProtoMember(3)]
            public uint entry
            {
                get; set;
            } = 0;

            [ProtoMember(4)]
            public string guid
            {
                get; set;
            } = "";

            [ProtoMember(5)]
            public string transportGuid
            {
                get; set;
            } = "";

            [ProtoMember(6)]
            public int currentHealth
            {
                get; set;
            } = -1;

            [ProtoMember(7)]
            public ulong maxHealth
            {
                get; set;
            } = 0;

            [ProtoMember(8)]
            public TimeSpan packetSendTime
            {
                get; set;
            } = new TimeSpan();

            [ProtoMember(9)]
            public Position spawnPosition
            {
                get; set;
            } = new Position();

            [ProtoMember(10)]
            public uint? mapId
            {
                get; set;
            } = null;

            [ProtoMember(11)]
            public List<Waypoint> waypoints { get; set; } = new List<Waypoint>();

            [ProtoMember(12)]
            public uint? emoteStateId
            {
                get; set;
            } = null;

            [ProtoMember(13)]
            public uint? sheatheState
            {
                get; set;
            } = null;

            [ProtoMember(14)]
            public uint? standState
            {
                get; set;
            } = null;

            [ProtoMember(15)]
            public bool hasDisableGravity
            {
                get; set;
            } = false;

            [ProtoMember(16)]
            public bool isCyclic
            {
                get; set;
            } = false;

            [ProtoMember(17)]
            public uint moveTime
            {
                get; set;
            } = 0;

            [ProtoMember(18)]
            public uint? unitFlags
            {
                get; set;
            } = null;

            [ProtoMember(19)]
            public MonsterMovePacket.JumpInfo jumpInfo
            {
                get; set;
            } = new MonsterMovePacket.JumpInfo();

            [ProtoMember(20)]
            public ConversationData conversationData
            {
                get; set;
            } = new ConversationData();

            [ProtoMember(21)]
            public Dictionary<uint, MonsterMovePacket.FilterKey> filterKeys { get; set; } = new Dictionary<uint, MonsterMovePacket.FilterKey>();

            [ProtoMember(22)]
            public List<VirtualItemData> virtualItems { get; set; } = new List<VirtualItemData>();

            [ProtoMember(23)]
            public List<QuestCompletedData> questCompletedData { get; set; } = new List<QuestCompletedData>();

            [ProtoMember(24)]
            public HoverData hoverData
            {
                get; set;
            } = new HoverData();

            [ProtoMember(25)]
            public List<string> destroyedObjectGuids
            {
                get; set;
            } = new List<string>();

            public UpdateObjectPacket() { }

            public UpdateObjectPacket(ObjectTypes objectType, uint entry, string guid, string transportGuid, string name, int curHealth, uint maxHealth, TimeSpan time, Position spawnPos, uint? mapId, List<Waypoint> waypoints, uint? emote, uint? sheatheState, uint? standState, bool hasDisableGravity, bool isCyclic, uint moveTime, uint? unitFlags, MonsterMovePacket.JumpInfo jumpInfo, ConversationData conversationData, Dictionary<uint, MonsterMovePacket.FilterKey> filterKeys, List<VirtualItemData> virtualItems, List<QuestCompletedData> questCompletedData, HoverData hoverData)
            { this.objectType = objectType; this.entry = entry; this.guid = guid; this.transportGuid = transportGuid; currentHealth = curHealth; this.maxHealth = maxHealth; packetSendTime = time; spawnPosition = spawnPos; this.mapId = mapId; this.waypoints = waypoints; emoteStateId = emote; this.sheatheState = sheatheState; this.standState = standState; this.hasDisableGravity = hasDisableGravity; this.isCyclic = isCyclic; this.moveTime = moveTime; this.unitFlags = unitFlags; this.jumpInfo = jumpInfo; this.conversationData = conversationData; this.filterKeys = filterKeys; this.virtualItems = virtualItems; this.questCompletedData = questCompletedData; this.hoverData = hoverData; }

            [ProtoContract]
            public class ConversationData
            {
                [ProtoMember(1)]
                public List<ConversationActorData> conversationActors
                {
                    get; set;
                } = new List<ConversationActorData>();

                [ProtoMember(2)]
                public List<ConversationLineData> conversationLines
                {
                    get; set;
                } = new List<ConversationLineData>();

                [ProtoContract]
                public class ConversationActorData
                {
                    [ProtoMember(1)]
                    public string ActorGuid
                    {
                        get; set;
                    } = "";

                    [ProtoMember(2)]
                    public uint? ActorIndex
                    {
                        get; set;
                    } = null;

                    public ConversationActorData() { }

                    public ConversationActorData(KeyValuePair<string, uint?> actorData)
                    {
                        ActorGuid = actorData.Key;
                        ActorIndex = actorData.Value;
                    }
                }

                [ProtoContract]
                public class ConversationLineData
                {
                    [ProtoMember(1)]
                    public uint ConversationLineId
                    {
                        get; set;
                    } = 0;

                    [ProtoMember(2)]
                    public uint? ActorIndex
                    {
                        get; set;
                    } = null;

                    public ConversationLineData() { }

                    public ConversationLineData(uint lineId, uint? actorIdx)
                    {
                        ConversationLineId = lineId;
                        ActorIndex = actorIdx;
                    }
                }

                public ConversationData() { }

                public ConversationData(List<ConversationActorData> conversationActors, List<ConversationLineData> conversationLines)
                { this.conversationActors = conversationActors; this.conversationLines = conversationLines; }
            }

            [ProtoContract]
            public class QuestCompletedData
            {
                [ProtoMember(1)]
                public int Index
                {
                    get; set;
                }

                [ProtoMember(2)]
                public ulong Flags
                {
                    get; set;
                }

                public QuestCompletedData() { }

                public QuestCompletedData(int index, ulong flags)
                {
                    Index = index;
                    Flags = flags;
                }
            }

            [ProtoContract]
            public class HoverData
            {
                [ProtoMember(1)]
                public bool Hover
                {
                    get; set;
                } = false;

                [ProtoMember(2)]
                public float HoverHeight
                {
                    get; set;
                } = 0.0f;

                public HoverData() { }

                public HoverData(bool hover, ulong hoverHeight)
                {
                    Hover = hover;
                    HoverHeight = hoverHeight;
                }
            }

            [ProtoContract]
            public class VirtualItemData
            {
                [ProtoMember(1)]
                public uint? ItemId
                {
                    get; set;
                } = null;

                [ProtoMember(2)]
                public uint? ItemIdx
                {
                    get; set;
                } = null;

                public VirtualItemData() { }

                public VirtualItemData(uint? itemId, uint? itemIdx)
                {
                    ItemId = itemId;
                    ItemIdx = itemIdx;
                }
            }

            [ProtoContract]
            public class CombatTimingsData
            {
                [ProtoMember(1)]
                public TimeSpan CombatStartTime
                {
                    get; set;
                } = new TimeSpan();

                [ProtoMember(2)]
                public TimeSpan CombatStopTime
                {
                    get; set;
                } = new TimeSpan();

                public CombatTimingsData()
                {
                }

                public CombatTimingsData(TimeSpan combatStartTime, TimeSpan combatStopTime)
                {
                    CombatStartTime = combatStartTime;
                    CombatStopTime = combatStopTime;
                }
            }

            public static bool IsLineValidForObjectParse(string line)
            {
                if (line == null)
                    return false;

                if (line == "")
                    return false;

                if (line.Contains("UpdateType: 1 (CreateObject1)") || line.Contains("UpdateType: CreateObject1"))
                    return false;

                if (line.Contains("UpdateType: 2 (CreateObject2)") || line.Contains("UpdateType: CreateObject2"))
                    return false;

                if (line.Contains("UpdateType: 0 (Values)") || line.Contains("UpdateType: Values"))
                    return false;

                if (line.Contains("DataSize"))
                    return false;

                return true;
            }

            public static uint GetEntryFromLine(string line)
            {
                Regex objectType = new Regex(@"ServerId:{1}\s+\d+;{1}\s+Entry:{1}\s+");
                Regex entryRegexField = new Regex(@"ServerId:{1}\s+\d+;{1}\s+Entry:{1}\s+\d+");

                if (entryRegexField.IsMatch(line))
                    return Convert.ToUInt32(entryRegexField.Match(line).ToString().Replace(objectType.Match(line).ToString(), ""));

                return 0;
            }

            public static int GetHealthFromLine(string line)
            {
                Regex healthRegex = new Regex(@"Health:{1}\s+\d+");
                if (healthRegex.IsMatch(line))
                    try
                    {
                        return Convert.ToInt32(healthRegex.Match(line).ToString().Replace("Health: ", ""));
                    }
                    catch
                    {
                        return -1;
                    }

                return -1;
            }

            public static ulong GetMaxHealthFromLine(string line)
            {
                Regex maxHealthRegex = new Regex(@"MaxHealth:{1}\s+\d+");
                if (maxHealthRegex.IsMatch(line))
                    return Convert.ToUInt64(maxHealthRegex.Match(line).ToString().Replace("MaxHealth: ", ""));

                return 0;
            }

            public static Position GetSpawnPositionFromLine(string xyzLine, string oriLine)
            {
                Position position = new Position();

                if (xyzLine.Contains("TransportPosition"))
                {
                    Regex xyzRegex = new Regex(@"TransportPosition:\s{1}X:{1}\s{1}");
                    if (xyzRegex.IsMatch(xyzLine))
                    {
                        string[] splittedLine = xyzLine.Split(' ');

                        position.x = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                        position.y = float.Parse(splittedLine[6], CultureInfo.InvariantCulture.NumberFormat);
                        position.z = float.Parse(splittedLine[8], CultureInfo.InvariantCulture.NumberFormat);
                        position.orientation = float.Parse(splittedLine[10], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    return position;
                }
                else if (xyzLine.Contains("Position:"))
                {
                    Regex xyzRegex = new Regex(@"Position:\s{1}X:{1}\s{1}");
                    if (xyzRegex.IsMatch(xyzLine))
                    {
                        string[] splittedLine = xyzLine.Split(' ');

                        position.x = float.Parse(splittedLine[3], CultureInfo.InvariantCulture.NumberFormat);
                        position.y = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                        position.z = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    Regex oriRegex = new Regex(@"Orientation:\s{1}");
                    if (oriRegex.IsMatch(oriLine))
                    {
                        string[] splittedLine = oriLine.Split(' ');

                        position.orientation = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    return position;
                }

                return position;
            }

            public static uint? GetMapIdFromLine(string line)
            {
                Regex mapRegex = new Regex(@"MapId:{1}\s{1}\d+");
                if (mapRegex.IsMatch(line))
                    return Convert.ToUInt32(mapRegex.Match(line).ToString().Replace("MapId: ", ""));

                return null;
            }

            public static bool GetDisableGravityFromLine(string line)
            {
                if (line.Contains("MovementFlags:") && line.Contains("DisableGravity"))
                    return true;

                return false;
            }

            public bool HasWaypoints()
            {
                return waypoints.Count != 0;
            }

            public static List<string> GetGuidsFromUpdatePacket(string[] lines, long index, BuildVersions build)
            {
                List<string> guidsList = new List<string>();

                do
                {
                    string guid = LineGetters.GetGuidFromLine(lines[index], build);
                    if (guid != "")
                    {
                        if (!guidsList.Contains(guid))
                        {
                            guidsList.Add(guid);
                        }
                    }

                    index++;
                }
                while (lines[index] != "");

                return guidsList;
            }

            public static uint? GetEmoteStateFromLine(string line)
            {
                Regex emoteRegex = new Regex(@"EmoteState:{1}\s{1}\w+");
                if (emoteRegex.IsMatch(line))
                    return Convert.ToUInt32(emoteRegex.Match(line).ToString().Replace("EmoteState: ", ""));

                return null;
            }

            public static uint? GetSheatheStateFromLine(string line)
            {
                Regex sheatheStateRegex = new Regex(@"SheatheState:{1}\s{1}\w+");
                if (sheatheStateRegex.IsMatch(line))
                    return Convert.ToUInt32(sheatheStateRegex.Match(line).ToString().Replace("SheatheState: ", ""));

                return null;
            }

            public static uint? GetStandStateFromLine(string line)
            {
                Regex standstateRegex = new Regex(@"StandState:{1}\s{1}\w+");
                if (standstateRegex.IsMatch(line))
                    return Convert.ToUInt32(standstateRegex.Match(line).ToString().Replace("StandState: ", ""));

                return null;
            }

            public static uint GetDurationFromLine(string line)
            {
                Regex durationRegexFirst = new Regex(@"] Duration:{1}\s{1}\w+");
                Regex durationRegexSecond = new Regex(@"\(MovementSpline\) Duration:{1}\s{1}\w+");

                if (durationRegexFirst.IsMatch(line))
                    return Convert.ToUInt32(durationRegexFirst.Match(line).ToString().Replace("] Duration: ", ""));
                else if (durationRegexSecond.IsMatch(line))
                    return Convert.ToUInt32(durationRegexSecond.Match(line).ToString().Replace("(MovementSpline) Duration: ", ""));

                return 0;
            }

            public static uint? GetUnitFlagsFromLine(string line)
            {
                Regex durationRegex = new Regex(@"\(UnitData\) Flags:{1}\s{1}\w+");
                if (durationRegex.IsMatch(line))
                    return Convert.ToUInt32(durationRegex.Match(line).ToString().Replace("(UnitData) Flags: ", ""));

                return null;
            }

            public static float GetJumpGravityFromLine(string line)
            {
                Regex jumpGravityRegex = new Regex(@"JumpGravity:{1}\s+.+");
                if (jumpGravityRegex.IsMatch(line))
                    return float.Parse((jumpGravityRegex.Match(line).ToString().Replace("JumpGravity: ", "")), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public static ObjectTypes GetObjectTypeFromLine(string line)
            {
                Regex objectTypeRegex = new Regex(@"TypeName:{1}\s{1}\w+");
                if (objectTypeRegex.IsMatch(line))
                {
                    switch (Convert.ToString(objectTypeRegex.Match(line).ToString().Replace("TypeName: ", "")))
                    {
                        case "Creature":
                            return ObjectTypes.Creature;
                        case "GameObject":
                            return ObjectTypes.GameObject;
                        case "Player":
                            return ObjectTypes.Player;
                        case "Conversation":
                            return ObjectTypes.Conversation;
                        default:
                            return ObjectTypes.Unknown;
                    }
                }

                return ObjectTypes.Unknown;
            }

            public static bool ObjectIsValidForParse(string line)
            {
                if (line.Contains("TypeName: Creature") || line.Contains("TypeName: Vehicle") || line.Contains("TypeName: Transport") || line.Contains("TypeName: Player"))
                    return true;

                return false;
            }

            public static uint GetConversationLineIdFromLine(string line)
            {
                Regex conversationLineIdRegex = new Regex(@"ConversationLineID:{1}\s{1}\w+");
                if (conversationLineIdRegex.IsMatch(line))
                    return Convert.ToUInt32(conversationLineIdRegex.Match(line).ToString().Replace("ConversationLineID: ", ""));

                return 0;
            }

            public static uint? GetActorIndexFromLine(string line)
            {
                Regex actorIndexRegex = new Regex(@"ActorIndex:{1}\s{1}\w+");
                if (actorIndexRegex.IsMatch(line))
                    return Convert.ToUInt32(actorIndexRegex.Match(line).ToString().Replace("ActorIndex: ", ""));

                return null;
            }

            public static KeyValuePair<string, uint?> GetActorDataFromLine(string line, BuildVersions buildVersion)
            {
                string actorGuid = LineGetters.GetGuidFromLine(line, buildVersion, conversationActorGuid: true);
                uint? actorIndex = null;

                Regex actorIndexRegex = new Regex(@"\(ConversationData\) \(Actors\){1}\s\[{1}\w+\]{1}");
                if (actorIndexRegex.IsMatch(line))
                {
                    actorIndex = Convert.ToUInt32(actorIndexRegex.Match(line).ToString().Replace("(ConversationData) (Actors) [", "").Replace("]", ""));
                }

                return new KeyValuePair<string, uint?>(actorGuid, actorIndex);
            }

            public static uint? GetFilterKeysCountFromLine(string line)
            {
                Regex filterKeysCountRegex = new Regex(@"FilterKeysCount:{1}\s{1}\w+");
                if (filterKeysCountRegex.IsMatch(line))
                    return Convert.ToUInt32(filterKeysCountRegex.Match(line).ToString().Replace("FilterKeysCount: ", ""));

                return null;
            }

            public static float GetFilterKeyFromLine(string line)
            {
                Regex inRegex = new Regex(@"In:{1}\s+.+");
                Regex outRegex = new Regex(@"Out:{1}\s+.+");
                if (inRegex.IsMatch(line))
                    return float.Parse(inRegex.Match(line).ToString().Replace("In: ", ""), CultureInfo.InvariantCulture.NumberFormat);
                else if (outRegex.IsMatch(line))
                    return float.Parse(outRegex.Match(line).ToString().Replace("Out: ", ""), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public static VirtualItemData GetItemDataFromLine(string line)
            {
                if (!line.Contains("(VirtualItems)"))
                    return null;

                VirtualItemData data = new VirtualItemData();

                Regex itemIdRegex = new Regex(@"ItemID:\s(\d+)");
                Match itemMatch = itemIdRegex.Match(line);
                if (!itemMatch.Success)
                    return null;

                data.ItemId = Convert.ToUInt32(itemMatch.Groups[1].Value);

                Regex itemIdxRegex = new Regex(@"\(VirtualItems\)\s\[(\d+)\]");
                Match idxMatch = itemIdxRegex.Match(line);

                if (idxMatch.Success)
                {
                    data.ItemIdx = Convert.ToUInt32(idxMatch.Groups[1].Value);
                }

                return data;
            }

            public static QuestCompletedData GetQuestCompletedFromLine(string line)
            {
                QuestCompletedData questCompletedData = new QuestCompletedData();
                questCompletedData.Index = -1;

                if (!line.Contains("(ActivePlayerData) (BitVectors) (Values) [9]"))
                    return questCompletedData;

                Regex indexRegex = new Regex(@"\(ActivePlayerData\) \(BitVectors\) \(Values\) \[9] \[{1}\w+");
                Regex flagsRegex = new Regex(@"Values:{1}\s{1}\w+");

                if (indexRegex.IsMatch(line))
                    questCompletedData.Index = Convert.ToInt32(indexRegex.Match(line).ToString().Replace("(ActivePlayerData) (BitVectors) (Values) [9] [", ""));

                if (flagsRegex.IsMatch(line))
                    questCompletedData.Flags = Convert.ToUInt64(flagsRegex.Match(line).ToString().Replace("Values: ", ""));

                return questCompletedData;
            }

            public static bool GetHoverFromLine(string line)
            {
                if (line.Contains("MovementFlags:") && line.Contains("Hover"))
                    return true;

                return false;
            }

            public static float GetHoverHeightFromLine(string line)
            {
                Regex jumpGravityRegex = new Regex(@"HoverHeight:{1}\s+.+");
                if (jumpGravityRegex.IsMatch(line))
                    return float.Parse((jumpGravityRegex.Match(line).ToString().Replace("HoverHeight: ", "")), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public static IEnumerable<UpdateObjectPacket> ParseObjectUpdatePacket(string[] lines, long index, BuildVersions buildVersion, long packetNumber)
            {
                TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                List<UpdateObjectPacket> updatePacketsList = new List<UpdateObjectPacket>();

                do
                {
                    if (((lines[index].Contains("UpdateType: 1 (CreateObject1)") || lines[index].Contains("UpdateType: CreateObject1")) || (lines[index].Contains("UpdateType: 2 (CreateObject2)") || lines[index].Contains("UpdateType: CreateObject2"))) && ObjectIsValidForParse(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, objectFieldGuid: true), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        updatePacket.updateType = PacketUpdateType.CreateObject;
                        Position tempPointPosition = new Position();
                        uint? filterKeysCount = null;

                        do
                        {
                            if (updatePacket.transportGuid == "")
                            {
                                tempUpdatePacket.transportGuid = LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, transportGuid: true);

                                if (tempUpdatePacket.transportGuid != "")
                                {
                                    updatePacket.transportGuid = tempUpdatePacket.transportGuid;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.objectType == ObjectTypes.Unknown)
                            {
                                tempUpdatePacket.objectType = GetObjectTypeFromLine(lines[index]);

                                if (tempUpdatePacket.objectType != ObjectTypes.Unknown)
                                {
                                    updatePacket.objectType = tempUpdatePacket.objectType;
                                    continue;
                                }
                            }

                            if (!tempPointPosition.IsValid())
                            {
                                tempPointPosition = MonsterMovePacket.GetPointPositionFromLine(lines[index]);

                                if (tempPointPosition.IsValid())
                                {
                                    uint pointId = 1;

                                    do
                                    {
                                        Waypoint wp = new Waypoint(MonsterMovePacket.GetPointPositionFromLine(lines[index]), 0.0f, 0, new Position(), 0, packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, updatePacket.hasDisableGravity ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                        wp.packetNumber = packetNumber;
                                        updatePacket.waypoints.Add(wp);
                                        pointId++;
                                        index++;
                                    }
                                    while (lines[index].Contains("Points:"));
                                }
                            }

                            if (updatePacket.mapId == null)
                            {
                                tempUpdatePacket.mapId = GetMapIdFromLine(lines[index]);

                                if (tempUpdatePacket.mapId != null)
                                {
                                    updatePacket.mapId = tempUpdatePacket.mapId;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.spawnPosition.IsValid())
                            {
                                tempUpdatePacket.spawnPosition = GetSpawnPositionFromLine(lines[index], lines[index + 1]);

                                if (tempUpdatePacket.spawnPosition.IsValid())
                                {
                                    updatePacket.spawnPosition = tempUpdatePacket.spawnPosition;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.entry == 0)
                            {
                                tempUpdatePacket.entry = GetEntryFromLine(lines[index]);

                                if (tempUpdatePacket.entry != 0)
                                {
                                    updatePacket.entry = tempUpdatePacket.entry;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.maxHealth == 0)
                            {
                                tempUpdatePacket.maxHealth = GetMaxHealthFromLine(lines[index]);

                                if (tempUpdatePacket.maxHealth != 0)
                                {
                                    updatePacket.maxHealth = tempUpdatePacket.maxHealth;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.hasDisableGravity)
                            {
                                tempUpdatePacket.hasDisableGravity = GetDisableGravityFromLine(lines[index]);

                                if (tempUpdatePacket.hasDisableGravity)
                                {
                                    updatePacket.hasDisableGravity = tempUpdatePacket.hasDisableGravity;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.isCyclic)
                            {
                                tempUpdatePacket.isCyclic = MonsterMovePacket.GetCyclicFromLine(lines[index]);
                                if (tempUpdatePacket.isCyclic)
                                {
                                    updatePacket.isCyclic = tempUpdatePacket.isCyclic;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.moveTime == 0 || updatePacket.jumpInfo.moveTime == 0)
                            {
                                tempUpdatePacket.moveTime = GetDurationFromLine(lines[index]);

                                if (tempUpdatePacket.moveTime != 0)
                                {
                                    updatePacket.moveTime = tempUpdatePacket.moveTime;
                                    updatePacket.jumpInfo.moveTime = tempUpdatePacket.moveTime;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.unitFlags == null)
                            {
                                tempUpdatePacket.unitFlags = GetUnitFlagsFromLine(lines[index]);

                                if (tempUpdatePacket.unitFlags != null)
                                {
                                    updatePacket.unitFlags = tempUpdatePacket.unitFlags;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.jumpInfo.jumpGravity == 0.0f)
                            {
                                tempUpdatePacket.jumpInfo.jumpGravity = GetJumpGravityFromLine(lines[index]);

                                if (tempUpdatePacket.jumpInfo.jumpGravity != 0.0f)
                                {
                                    updatePacket.jumpInfo.jumpGravity = tempUpdatePacket.jumpInfo.jumpGravity;
                                    updatePacket.jumpInfo.jumpPos = updatePacket.waypoints.Last().movePosition;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.hasDisableGravity)
                            {
                                tempUpdatePacket.hasDisableGravity = MonsterMovePacket.GetFlyingFromLine(lines[index]);

                                if (tempUpdatePacket.hasDisableGravity)
                                {
                                    updatePacket.hasDisableGravity = tempUpdatePacket.hasDisableGravity;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.isCyclic)
                            {
                                tempUpdatePacket.isCyclic = MonsterMovePacket.GetCyclicFromLine(lines[index]);
                                if (tempUpdatePacket.isCyclic)
                                {
                                    updatePacket.isCyclic = tempUpdatePacket.isCyclic;
                                    index++;
                                    continue;
                                }
                            }

                            if (filterKeysCount == null)
                            {
                                filterKeysCount = GetFilterKeysCountFromLine(lines[index]);
                                if (filterKeysCount != null)
                                {
                                    for (uint i = 0; i < filterKeysCount; i++)
                                    {
                                        MonsterMovePacket.FilterKey filterKey = new MonsterMovePacket.FilterKey();

                                        for (uint j = 0; j < 2; j++)
                                        {
                                            index++;

                                            if (j == 0)
                                            {
                                                filterKey.In = GetFilterKeyFromLine(lines[index]);
                                            }
                                            else
                                            {
                                                filterKey.Out = GetFilterKeyFromLine(lines[index]);
                                            }
                                        }

                                        updatePacket.filterKeys.Add(i, filterKey);
                                    }
                                }
                            }

                            if (updatePacket.virtualItems.Count != 3)
                            {
                                VirtualItemData itemData = GetItemDataFromLine(lines[index]);
                                if (itemData != null)
                                {
                                    updatePacket.virtualItems.Add(itemData);
                                    index++;
                                    continue;
                                }
                            }

                            QuestCompletedData questCompletedData = GetQuestCompletedFromLine(lines[index]);

                            if (questCompletedData.Index != -1)
                            {
                                updatePacket.questCompletedData.Add(questCompletedData);
                            }

                            if (!updatePacket.hoverData.Hover)
                            {
                                tempUpdatePacket.hoverData.Hover = GetHoverFromLine(lines[index]);

                                if (tempUpdatePacket.hoverData.Hover)
                                {
                                    updatePacket.hoverData.Hover = tempUpdatePacket.hoverData.Hover;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.hoverData.HoverHeight == 0.0f)
                            {
                                tempUpdatePacket.hoverData.HoverHeight = GetHoverHeightFromLine(lines[index]);

                                if (tempUpdatePacket.hoverData.HoverHeight != 0.0f)
                                {
                                    updatePacket.hoverData.HoverHeight = tempUpdatePacket.hoverData.HoverHeight;
                                    index++;
                                    continue;
                                }
                            }

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.objectType != ObjectTypes.Player && (updatePacket.entry == 0 || updatePacket.guid == ""))
                            continue;

                        if (Properties.Settings.Default.CombatMovement && updatePacket.unitFlags != null && ((UnitFlags)updatePacket.unitFlags & UnitFlags.AffectingCombat) != 0)
                        {
                            updatePacket.waypoints.Clear();
                        }
                        else if (updatePacket.waypoints.Count != 0)
                        {
                            float velocity = MonsterMovePacket.GetWaypointsVelocity(updatePacket.waypoints, updatePacket.isCyclic ? updatePacket.waypoints.First().movePosition : updatePacket.spawnPosition, updatePacket.moveTime);

                            if (velocity != 0.0f)
                            {
                                for (int i = 0; i < updatePacket.waypoints.Count; i++)
                                {
                                    updatePacket.waypoints[i].velocity = velocity;
                                    updatePacket.waypoints[i].moveTime = updatePacket.moveTime;
                                    updatePacket.waypoints[i].moveStartTime = updatePacket.packetSendTime;
                                    updatePacket.waypoints[i].startPosition = updatePacket.spawnPosition;
                                }

                                if (!updatePacket.hasDisableGravity && velocity != 0.0f)
                                {
                                    if (updatePacket.jumpInfo.IsValid())
                                    {
                                        updatePacket.waypoints.Clear();
                                    }
                                    else if (velocity >= 4.2)
                                    {
                                        for (int i = 0; i < updatePacket.waypoints.Count; i++)
                                        {
                                            updatePacket.waypoints[i].moveType = MoveType.MOVE_RUN;
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < updatePacket.waypoints.Count; i++)
                                        {
                                            updatePacket.waypoints[i].moveType = MoveType.MOVE_WALK;
                                        }
                                    }
                                }
                            }
                        }

                        if (!updatePacket.hoverData.Hover && updatePacket.hoverData.HoverHeight != 0.0f)
                        {
                            updatePacket.hoverData.HoverHeight = 0.0f;
                        }

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if ((lines[index].Contains("UpdateType: 0 (Values)") || lines[index].Contains("UpdateType: Values")) && ObjectIsValidForParse(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        updatePacket.updateType = PacketUpdateType.Values;

                        do
                        {
                            if (updatePacket.objectType == ObjectTypes.Unknown)
                            {
                                tempUpdatePacket.objectType = GetObjectTypeFromLine(lines[index]);

                                if (tempUpdatePacket.objectType != ObjectTypes.Unknown)
                                {
                                    updatePacket.objectType = tempUpdatePacket.objectType;
                                    continue;
                                }
                            }

                            if (updatePacket.maxHealth == 0)
                            {
                                tempUpdatePacket.maxHealth = GetMaxHealthFromLine(lines[index]);

                                if (tempUpdatePacket.maxHealth != 0)
                                {
                                    updatePacket.maxHealth = tempUpdatePacket.maxHealth;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.hasDisableGravity)
                            {
                                tempUpdatePacket.hasDisableGravity = GetDisableGravityFromLine(lines[index]);

                                if (tempUpdatePacket.hasDisableGravity)
                                {
                                    updatePacket.hasDisableGravity = tempUpdatePacket.hasDisableGravity;
                                    index++;
                                    continue;
                                }
                            }

                            if (!updatePacket.isCyclic)
                            {
                                tempUpdatePacket.isCyclic = MonsterMovePacket.GetCyclicFromLine(lines[index]);
                                if (tempUpdatePacket.isCyclic)
                                {
                                    updatePacket.isCyclic = tempUpdatePacket.isCyclic;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.unitFlags == null)
                            {
                                tempUpdatePacket.unitFlags = GetUnitFlagsFromLine(lines[index]);

                                if (tempUpdatePacket.unitFlags != null)
                                {
                                    updatePacket.unitFlags = tempUpdatePacket.unitFlags;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.emoteStateId == null)
                            {
                                tempUpdatePacket.emoteStateId = GetEmoteStateFromLine(lines[index]);

                                if (tempUpdatePacket.emoteStateId != null)
                                {
                                    updatePacket.emoteStateId = tempUpdatePacket.emoteStateId;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.sheatheState == null)
                            {
                                tempUpdatePacket.sheatheState = GetSheatheStateFromLine(lines[index]);

                                if (tempUpdatePacket.sheatheState != null)
                                {
                                    updatePacket.sheatheState = tempUpdatePacket.sheatheState;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.standState == null)
                            {
                                tempUpdatePacket.standState = GetStandStateFromLine(lines[index]);

                                if (tempUpdatePacket.standState != null)
                                {
                                    updatePacket.standState = tempUpdatePacket.standState;
                                    index++;
                                    continue;
                                }
                            }

                            if (updatePacket.currentHealth == -1)
                            {
                                tempUpdatePacket.currentHealth = GetHealthFromLine(lines[index]);

                                if (tempUpdatePacket.currentHealth == 0)
                                {
                                    updatePacket.currentHealth = tempUpdatePacket.currentHealth;
                                    index++;
                                    continue;
                                }
                            }

                            QuestCompletedData questCompletedData = GetQuestCompletedFromLine(lines[index]);

                            if (questCompletedData.Index != -1)
                            {
                                updatePacket.questCompletedData.Add(questCompletedData);
                            }

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.guid == "")
                            continue;

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if (lines[index].Contains("DestroyedObjCount"))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket
                        {
                            updateType = PacketUpdateType.Destroy,
                            packetSendTime = packetSendTime,
                        };

                        do
                        {
                            string guid = LineGetters.GetGuidFromLine(lines[index], buildVersion, destroyObjectGuid: true);

                            if (guid != "")
                            {
                                updatePacket.destroyedObjectGuids.Add(guid);
                            }

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.destroyedObjectGuids.Count == 0)
                            continue;

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if (((lines[index].Contains("UpdateType: 1 (CreateObject1)") || lines[index].Contains("UpdateType: CreateObject1")) || (lines[index].Contains("UpdateType: 2 (CreateObject2)") || lines[index].Contains("UpdateType: CreateObject2"))) && lines[index + 1].IsConversationLine())
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<VirtualItemData>(), new List<QuestCompletedData>(), new HoverData());
                        updatePacket.updateType = PacketUpdateType.CreateObject;

                        do
                        {
                            if (updatePacket.objectType == ObjectTypes.Unknown)
                            {
                                tempUpdatePacket.objectType = GetObjectTypeFromLine(lines[index]);

                                if (tempUpdatePacket.objectType != ObjectTypes.Unknown)
                                {
                                    updatePacket.objectType = tempUpdatePacket.objectType;
                                    continue;
                                }
                            }

                            if (updatePacket.entry == 0)
                            {
                                tempUpdatePacket.entry = GetEntryFromLine(lines[index]);

                                if (tempUpdatePacket.entry != 0)
                                {
                                    updatePacket.entry = tempUpdatePacket.entry;
                                    index++;
                                    continue;
                                }
                            }

                            if (GetConversationLineIdFromLine(lines[index]) != 0)
                            {
                                uint conversationLineId = GetConversationLineIdFromLine(lines[index]);

                                do
                                {
                                    if (GetActorIndexFromLine(lines[index]) != null)
                                    {
                                        updatePacket.conversationData.conversationLines.Add(new ConversationData.ConversationLineData(conversationLineId, GetActorIndexFromLine(lines[index])));
                                    }

                                    index++;
                                } while (!lines[index].Contains("ConversationLineID") && lines[index].Contains("Lines"));

                                index--;
                            }

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, conversationActorGuid: true) != "")
                            {
                                updatePacket.conversationData.conversationActors.Add(new ConversationData.ConversationActorData(GetActorDataFromLine(lines[index], buildVersion)));
                            }

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.entry == 0)
                            continue;

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }

                    index++;

                } while (lines[index] != "");

                return updatePacketsList;
            }
        }

        [ProtoContract]
        public class MonsterMovePacket
        {
            [ProtoMember(1)]
            public string creatureGuid
            {
                get; set;
            } = "";

            [ProtoMember(2)]
            public float creatureOrientation
            {
                get; set;
            } = 0.0f;

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            } = new TimeSpan();

            [ProtoMember(4)]
            public List<Waypoint> waypoints
            {
                get; set;
            } = new List<Waypoint>();

            [ProtoMember(5)]
            public uint moveTime
            {
                get; set;
            } = 0;

            [ProtoMember(6)]
            public Position startPos
            {
                get; set;
            } = new Position();

            [ProtoMember(7)]
            public JumpInfo jumpInfo
            {
                get; set;
            } = new JumpInfo();

            [ProtoContract]
            public class JumpInfo
            {
                [ProtoMember(1)]
                public uint moveTime
                {
                    get; set;
                } = 0;

                [ProtoMember(2)]
                public float jumpGravity
                {
                    get; set;
                } = 0.0f;

                [ProtoMember(3)]
                public Position jumpPos
                {
                    get; set;
                } = new Position();

                public JumpInfo() { }

                public JumpInfo(uint time, float gravity, Position positon)
                { moveTime = time; jumpGravity = gravity; jumpPos = positon; }

                public bool IsValid()
                {
                    return moveTime != 0 && jumpGravity != 0.0f && jumpPos.IsValid();
                }
            }

            [ProtoContract]
            public class FilterKey
            {
                [ProtoMember(1)]
                public float In
                {
                    get; set;
                } = 0.0f;

                [ProtoMember(2)]
                public float Out
                {
                    get; set;
                } = 0.0f;

                public FilterKey() { }
            };

            public MonsterMovePacket() { }

            public MonsterMovePacket(string guid, float orientation, TimeSpan time, List<Waypoint> waypoints, uint moveTime, Position pos, JumpInfo jump)
            { creatureGuid = guid; creatureOrientation = orientation; packetSendTime = time; this.waypoints = waypoints; this.moveTime = moveTime; startPos = pos; jumpInfo = jump; }

            public static float GetFaceDirectionFromLine(string line)
            {
                Regex facingRegex = new Regex(@"FaceDirection:{1}\s+\d+\.+\d+");
                if (facingRegex.IsMatch(line))
                    return float.Parse(facingRegex.Match(line).ToString().Replace("FaceDirection: ", ""), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public static bool ConsistsOfPoints(string pointLine, string nextLine)
            {
                if (pointLine.Contains("[0] Points: X:") && nextLine.Contains("[1] Points: X:"))
                    return true;

                return false;
            }

            public static Position GetPointPositionFromLine(string line)
            {
                Position pointPosition = new Position();

                Regex xyzRegex = new Regex(@"Points:{1}\s{1}X:{1}.+");
                if (xyzRegex.IsMatch(line))
                {
                    string[] splittedLine = xyzRegex.Match(line).ToString().Replace("Points: X: ", "").Split(' ');

                    pointPosition.x = float.Parse(splittedLine[0], CultureInfo.InvariantCulture.NumberFormat);
                    pointPosition.y = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    pointPosition.z = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                }

                return pointPosition;
            }

            public static Position GetWayPointPositionFromLine(string line)
            {
                Position wayPointPosition = new Position();

                Regex xyzRegex = new Regex(@"WayPoints:{1}\s{1}X:{1}.+");
                if (xyzRegex.IsMatch(line))
                {
                    string[] splittedLine = xyzRegex.Match(line).ToString().Replace("WayPoints: X: ", "").Split(' ');

                    wayPointPosition.x = float.Parse(splittedLine[0], CultureInfo.InvariantCulture.NumberFormat);
                    wayPointPosition.y = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    wayPointPosition.z = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                }

                return wayPointPosition;
            }

            public static uint GetMoveTimeFromLine(string line)
            {
                Regex moveTimeRegex = new Regex(@"MoveTime:{1}\s+\d+");
                if (moveTimeRegex.IsMatch(line))
                    return Convert.ToUInt32(moveTimeRegex.Match(line).ToString().Replace("MoveTime: ", ""));

                return 0;
            }

            public static Position GetStartPositionFromLine(string line)
            {
                Position startPosition = new Position();

                Regex xyzRegex = new Regex(@"Position:{1}\s{1}X:{1}.+");
                if (xyzRegex.IsMatch(line))
                {
                    string[] splittedLine = xyzRegex.Match(line).ToString().Replace("Position: X: ", "").Split(' ');

                    startPosition.x = float.Parse(splittedLine[0], CultureInfo.InvariantCulture.NumberFormat);
                    startPosition.y = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    startPosition.z = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                }

                return startPosition;
            }

            public static float GetJumpGravityFromLine(string line)
            {
                Regex jumpGravityRegex = new Regex(@"JumpGravity:{1}\s+.+");
                if (jumpGravityRegex.IsMatch(line))
                    return float.Parse((jumpGravityRegex.Match(line).ToString().Replace("JumpGravity: ", "")), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public bool HasOrientation()
            {
                return creatureOrientation != 0.0f;
            }

            public bool HasWaypoints()
            {
                return waypoints.Count >= 1;
            }

            public bool HasJump()
            {
                return jumpInfo.IsValid();
            }

            public static bool GetFlyingFromLine(string line)
            {
                if (line.Contains("Flags:"))
                {
                    if (line.Contains("Flying"))
                        return true;
                    else
                        return false;
                }

                return false;
            }

            public static bool GetCyclicFromLine(string line)
            {
                if (line.Contains("Flags:"))
                {
                    if (line.Contains("Cyclic"))
                        return true;
                    else
                        return false;
                }

                return false;
            }

            public static float GetWaypointsDistance(List<Waypoint> waypoints, Position startPosition)
            {
                if (waypoints.Count() == 0)
                    return 0.0f;

                float distance = startPosition.GetDistance(waypoints.First().movePosition);

                for (int i = 1; i < waypoints.Count(); i++)
                {
                    distance += waypoints[i - 1].movePosition.GetDistance(waypoints[i].movePosition);
                }

                return distance;
            }

            public static float GetWaypointsVelocity(List<Waypoint> waypoints, Position startPosition, float moveTime)
            {
                if (moveTime != 0 && waypoints.Count() != 0)
                {
                    return GetWaypointsDistance(waypoints, startPosition) / moveTime * 1000;
                }

                return 0.0f;
            }

            public static MonsterMovePacket ParseMovementPacket(string[] lines, long index, BuildVersions buildVersion, long packetNumber)
            {
                MonsterMovePacket movePacket = new MonsterMovePacket(LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, moverGuid: true), 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                MonsterMovePacket tempMovePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                bool tempFlying = false;
                bool tempCyclic = false;
                Position tempPointPosition = new Position();

                if (LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    Position lastPosition = new Position();
                    bool isFlying = false;
                    bool isCyclic = false;

                    do
                    {
                        if (!movePacket.startPos.IsValid())
                        {
                            tempMovePacket.startPos = GetStartPositionFromLine(lines[index]);

                            if (tempMovePacket.startPos.IsValid())
                            {
                                movePacket.startPos = tempMovePacket.startPos;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.moveTime == 0)
                        {
                            tempMovePacket.moveTime = GetMoveTimeFromLine(lines[index]);

                            if (tempMovePacket.moveTime != 0)
                            {
                                movePacket.moveTime = tempMovePacket.moveTime;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.creatureOrientation == 0.0f)
                        {
                            tempMovePacket.creatureOrientation = GetFaceDirectionFromLine(lines[index]);

                            if (tempMovePacket.creatureOrientation != 0.0f)
                            {
                                movePacket.creatureOrientation = tempMovePacket.creatureOrientation;
                                index++;
                                continue;
                            }
                        }

                        if (!isFlying)
                        {
                            tempFlying = GetFlyingFromLine(lines[index]);

                            if (tempFlying)
                            {
                                isFlying = tempFlying;
                                index++;
                                continue;
                            }
                        }

                        if (!isCyclic)
                        {
                            tempCyclic = GetCyclicFromLine(lines[index]);
                            if (tempCyclic)
                            {
                                isCyclic = tempCyclic;
                                index++;
                                continue;
                            }
                        }

                        if (!tempPointPosition.IsValid())
                        {
                            tempPointPosition = GetPointPositionFromLine(lines[index]);

                            if (tempPointPosition.IsValid())
                            {
                                if (ConsistsOfPoints(lines[index], lines[index + 1]))
                                {
                                    uint pointId = 1;

                                    do
                                    {
                                        Position point = GetPointPositionFromLine(lines[index]);

                                        if (point.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = packetNumber;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");
                                }
                                else
                                {
                                    lastPosition = tempPointPosition;

                                    uint pointId = 1;

                                    do
                                    {
                                        Position waypoint = GetWayPointPositionFromLine(lines[index]);

                                        if (waypoint.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = packetNumber;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                        }

                                        if (movePacket.jumpInfo.jumpGravity == 0.0f)
                                        {
                                            tempMovePacket.jumpInfo.jumpGravity = GetJumpGravityFromLine(lines[index]);

                                            if (tempMovePacket.jumpInfo.jumpGravity != 0.0f)
                                            {
                                                movePacket.jumpInfo.jumpGravity = tempMovePacket.jumpInfo.jumpGravity;
                                            }
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");

                                    index--;
                                }

                                if (lastPosition.IsValid())
                                {
                                    if (movePacket.jumpInfo.jumpGravity != 0.0f)
                                    {
                                        movePacket.jumpInfo.moveTime = movePacket.moveTime;
                                        movePacket.jumpInfo.jumpPos = lastPosition;
                                    }
                                    else
                                    {
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                        wp.packetNumber = packetNumber;
                                        movePacket.waypoints.Add(wp);
                                    }
                                }
                            }
                        }

                        index++;
                    }
                    while (lines[index] != "");

                    if (movePacket.creatureGuid == "")
                        return movePacket;

                    float velocity = GetWaypointsVelocity(movePacket.waypoints, isCyclic ? movePacket.waypoints.First().movePosition : movePacket.startPos, movePacket.moveTime);

                    if (velocity != 0.0f)
                    {
                        for (int i = 0; i < movePacket.waypoints.Count; i++)
                        {
                            movePacket.waypoints[i].velocity = velocity;
                        }

                        if (!isFlying)
                        {
                            if (velocity >= 4.2)
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_RUN;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_WALK;
                                }
                            }
                        }
                    }
                }

                return movePacket;
            }

            public static MonsterMovePacket ParseMovementPacket(string[] lines, long index, BuildVersions buildVersion, SortedDictionary<long, Packet> updateObjectPacketsDict, long packetNumber)
            {
                MonsterMovePacket movePacket = new MonsterMovePacket(LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, moverGuid: true), 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                MonsterMovePacket tempMovePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                bool tempFlying = false;
                bool tempCyclic = false;
                Position tempPointPosition = new Position();
                bool skipCombatMovement = Properties.Settings.Default.CombatMovement;

                if (LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    Position lastPosition = new Position();
                    bool isFlying = false;
                    bool isCyclic = false;

                    do
                    {
                        if (!movePacket.startPos.IsValid())
                        {
                            tempMovePacket.startPos = GetStartPositionFromLine(lines[index]);

                            if (tempMovePacket.startPos.IsValid())
                            {
                                movePacket.startPos = tempMovePacket.startPos;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.moveTime == 0)
                        {
                            tempMovePacket.moveTime = GetMoveTimeFromLine(lines[index]);

                            if (tempMovePacket.moveTime != 0)
                            {
                                movePacket.moveTime = tempMovePacket.moveTime;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.creatureOrientation == 0.0f)
                        {
                            tempMovePacket.creatureOrientation = GetFaceDirectionFromLine(lines[index]);

                            if (tempMovePacket.creatureOrientation != 0.0f)
                            {
                                movePacket.creatureOrientation = tempMovePacket.creatureOrientation;
                                index++;
                                continue;
                            }
                        }

                        if (!isFlying)
                        {
                            tempFlying = GetFlyingFromLine(lines[index]);

                            if (tempFlying)
                            {
                                isFlying = tempFlying;
                                index++;
                                continue;
                            }
                        }

                        if (!isCyclic)
                        {
                            tempCyclic = GetCyclicFromLine(lines[index]);
                            if (tempCyclic)
                            {
                                isCyclic = tempCyclic;
                                index++;
                                continue;
                            }
                        }

                        if (!tempPointPosition.IsValid())
                        {
                            tempPointPosition = GetPointPositionFromLine(lines[index]);

                            if (tempPointPosition.IsValid())
                            {
                                if (ConsistsOfPoints(lines[index], lines[index + 1]))
                                {
                                    uint pointId = 1;

                                    do
                                    {
                                        Position point = GetPointPositionFromLine(lines[index]);

                                        if (point.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = packetNumber;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");
                                }
                                else
                                {
                                    lastPosition = tempPointPosition;

                                    uint pointId = 1;

                                    do
                                    {
                                        Position waypoint = GetWayPointPositionFromLine(lines[index]);

                                        if (waypoint.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = packetNumber;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                            index++;
                                            continue;
                                        }

                                        if (movePacket.jumpInfo.jumpGravity == 0.0f)
                                        {
                                            tempMovePacket.jumpInfo.jumpGravity = GetJumpGravityFromLine(lines[index]);

                                            if (tempMovePacket.jumpInfo.jumpGravity != 0.0f)
                                            {
                                                movePacket.jumpInfo.jumpGravity = tempMovePacket.jumpInfo.jumpGravity;
                                            }
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");

                                    index--;
                                }

                                if (lastPosition.IsValid())
                                {
                                    if (movePacket.jumpInfo.jumpGravity != 0.0f)
                                    {
                                        movePacket.jumpInfo.moveTime = movePacket.moveTime;
                                        movePacket.jumpInfo.jumpPos = lastPosition;
                                    }
                                    else
                                    {
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                        wp.packetNumber = packetNumber;
                                        movePacket.waypoints.Add(wp);
                                    }
                                }
                            }
                        }

                        if (skipCombatMovement && (lines[index].Contains("FacingGUID: TypeName: Player; Full:") || lines[index].Contains("FacingGUID: TypeName: Creature; Full:") || lines[index].Contains("FacingGUID: TypeName: Vehicle; Full:")))
                            return tempMovePacket;

                        index++;
                    }
                    while (lines[index] != "");

                    if (movePacket.creatureGuid == "")
                        return movePacket;

                    float velocity = GetWaypointsVelocity(movePacket.waypoints, isCyclic ? movePacket.waypoints.First().movePosition : movePacket.startPos, movePacket.moveTime);

                    if (velocity != 0.0f)
                    {

                        for (int i = 0; i < movePacket.waypoints.Count; i++)
                        {
                            movePacket.waypoints[i].velocity = velocity;
                        }

                        if (!isFlying)
                        {
                            if (movePacket.jumpInfo.IsValid())
                            {
                                movePacket.waypoints.Clear();
                            }
                            else if (velocity >= 4.2)
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_RUN;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_WALK;
                                }
                            }
                        }
                    }
                }

                return movePacket;
            }

            public static MonsterMovePacket ParseMovementPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                MonsterMovePacket movePacket = new MonsterMovePacket(LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, moverGuid: true), 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                MonsterMovePacket tempMovePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());
                bool tempFlying = false;
                bool tempCyclic = false;
                Position tempPointPosition = new Position();

                if (LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    Position lastPosition = new Position();
                    bool isFlying = false;
                    bool isCyclic = false;

                    do
                    {
                        if (!movePacket.startPos.IsValid())
                        {
                            tempMovePacket.startPos = GetStartPositionFromLine(lines[index]);

                            if (tempMovePacket.startPos.IsValid())
                            {
                                movePacket.startPos = tempMovePacket.startPos;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.moveTime == 0)
                        {
                            tempMovePacket.moveTime = GetMoveTimeFromLine(lines[index]);

                            if (tempMovePacket.moveTime != 0)
                            {
                                movePacket.moveTime = tempMovePacket.moveTime;
                                index++;
                                continue;
                            }
                        }

                        if (movePacket.creatureOrientation == 0.0f)
                        {
                            tempMovePacket.creatureOrientation = GetFaceDirectionFromLine(lines[index]);

                            if (tempMovePacket.creatureOrientation != 0.0f)
                            {
                                movePacket.creatureOrientation = tempMovePacket.creatureOrientation;
                                index++;
                                continue;
                            }
                        }

                        if (!isFlying)
                        {
                            tempFlying = GetFlyingFromLine(lines[index]);

                            if (tempFlying)
                            {
                                isFlying = tempFlying;
                                index++;
                                continue;
                            }
                        }

                        if (!isCyclic)
                        {
                            tempCyclic = GetCyclicFromLine(lines[index]);
                            if (tempCyclic)
                            {
                                isCyclic = tempCyclic;
                                index++;
                                continue;
                            }
                        }

                        if (!tempPointPosition.IsValid())
                        {
                            tempPointPosition = GetPointPositionFromLine(lines[index]);

                            if (tempPointPosition.IsValid())
                            {
                                if (ConsistsOfPoints(lines[index], lines[index + 1]))
                                {
                                    uint pointId = 1;

                                    do
                                    {
                                        Position point = GetPointPositionFromLine(lines[index]);

                                        if (point.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = -1;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");
                                }
                                else
                                {
                                    lastPosition = tempPointPosition;

                                    uint pointId = 1;

                                    do
                                    {
                                        Position waypoint = GetWayPointPositionFromLine(lines[index]);

                                        if (waypoint.IsValid())
                                        {
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                            wp.packetNumber = -1;
                                            movePacket.waypoints.Add(wp);
                                            pointId++;
                                        }

                                        if (movePacket.jumpInfo.jumpGravity == 0.0f)
                                        {
                                            tempMovePacket.jumpInfo.jumpGravity = GetJumpGravityFromLine(lines[index]);

                                            if (tempMovePacket.jumpInfo.jumpGravity != 0.0f)
                                            {
                                                movePacket.jumpInfo.jumpGravity = tempMovePacket.jumpInfo.jumpGravity;
                                            }
                                        }

                                        index++;
                                    }
                                    while (lines[index] != "");

                                    index--;
                                }

                                if (lastPosition.IsValid())
                                {
                                    if (movePacket.jumpInfo.jumpGravity != 0.0f)
                                    {
                                        movePacket.jumpInfo.moveTime = movePacket.moveTime;
                                        movePacket.jumpInfo.jumpPos = lastPosition;
                                    }
                                    else
                                    {
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                                        wp.packetNumber = -1;
                                        movePacket.waypoints.Add(wp);
                                    }
                                }
                            }
                        }

                        index++;
                    }
                    while (lines[index] != "");

                    if (movePacket.creatureGuid == "")
                        return movePacket;

                    if (movePacket.waypoints.Count == 0 && movePacket.HasOrientation())
                    {
                        Waypoint wp = new Waypoint(lastPosition, movePacket.creatureOrientation, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying ? MoveType.MOVE_FLIGHT : MoveType.MOVE_UNKNOWN);
                        wp.packetNumber = -1;
                        movePacket.waypoints.Add(wp);
                    }

                    float velocity = GetWaypointsVelocity(movePacket.waypoints, isCyclic ? movePacket.waypoints.First().movePosition : movePacket.startPos, movePacket.moveTime);

                    if (velocity != 0.0f)
                    {

                        for (int i = 0; i < movePacket.waypoints.Count; i++)
                        {
                            movePacket.waypoints[i].velocity = velocity;
                        }

                        if (!isFlying)
                        {
                            if (movePacket.jumpInfo.IsValid())
                            {
                                movePacket.waypoints.Clear();
                            }
                            else if (velocity >= 4.2)
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_RUN;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < movePacket.waypoints.Count; i++)
                                {
                                    movePacket.waypoints[i].moveType = MoveType.MOVE_WALK;
                                }
                            }
                        }
                    }
                }

                return movePacket;
            }
        }

        [ProtoContract]
        public class AttackStopPacket
        {
            [ProtoMember(1)]
            public string creatureGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public bool nowDead
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public AttackStopPacket() { }

            public AttackStopPacket(string guid, bool dead, TimeSpan time)
            { creatureGuid = guid; nowDead = dead; packetSendTime = time; }

            public static bool GetNowDeadFromLine(string line)
            {
                Regex nowDeadRegex = new Regex(@"NowDead:{1}\s+\w+");
                if (nowDeadRegex.IsMatch(line))
                    return nowDeadRegex.Match(line).ToString().Replace("NowDead: ", "") == "True";

                return false;
            }

            public static AttackStopPacket ParseAttackStopPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                AttackStopPacket attackPacket = new AttackStopPacket("", false, LineGetters.GetTimeSpanFromLine(lines[index]));

                if (LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    do
                    {
                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion, attackerGuid: true) != "")
                            attackPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, attackerGuid: true);

                        if (GetNowDeadFromLine(lines[index]))
                            attackPacket.nowDead = GetNowDeadFromLine(lines[index]);

                        index++;
                    }
                    while (lines[index] != "");
                }

                return attackPacket;
            }
        }

        [ProtoContract]
        public class TimePacket
        {
            [ProtoMember(1)]
            public string hours
            {
                get; set;
            } = "";

            [ProtoMember(2)]
            public string minutes
            {
                get; set;
            } = "";

            [ProtoMember(3)]
            public string seconds
            {
                get; set;
            } = "";

            public TimePacket() { }
        }

        [ProtoContract]
        public class AIReactionPacket
        {
            [ProtoMember(1)]
            public string creatureGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint creatureEntry
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public AIReactionPacket() { }

            public AIReactionPacket(string guid, uint entry, TimeSpan time)
            { creatureGuid = guid; creatureEntry = entry; packetSendTime = time; }

            public static AIReactionPacket ParseAIReactionPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                AIReactionPacket reactionPacket = new AIReactionPacket("", 0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true) != "")
                    {
                        reactionPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true);
                    }

                    index++;
                }
                while (lines[index] != "");

                reactionPacket.creatureEntry = CreatureScriptsCreator.GetCreatureEntryByGuid(reactionPacket.creatureGuid);

                return reactionPacket;
            }
        }

        [ProtoContract]
        public class AuraUpdatePacket
        {
            [ProtoMember(1)]
            public string unitGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint? slot
            {
                get; set;
            }

            [ProtoMember(3)]
            public uint spellId
            {
                get; set;
            }

            [ProtoMember(4)]
            public bool? HasAura
            {
                get; set;
            }

            [ProtoMember(5)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public AuraUpdatePacket() { }

            public AuraUpdatePacket(string guid, uint? slot, uint spellId, bool? hasAUra, TimeSpan time)
            { unitGuid = guid; this.slot = slot; this.spellId = spellId; this.HasAura = hasAUra; packetSendTime = time; }

            public static uint? GetAuraSlotFromLine(string line)
            {
                Regex slotRegex = new Regex(@"Slot:{1}\s{1}\d+");
                if (slotRegex.IsMatch(line))
                    return Convert.ToUInt32(slotRegex.Match(line).ToString().Replace("Slot: ", ""));

                return null;
            }

            public static bool? GetHasAuraFromLine(string line)
            {
                Regex applyRegex = new Regex(@"HasAura:{1}\s{1}\w+");
                if (applyRegex.IsMatch(line))
                    return applyRegex.Match(line).ToString().Replace("HasAura: ", "") == "True";

                return null;
            }

            public static bool IsLineValidForAuraUpdateParsing(string line)
            {
                if (line == null)
                    return false;

                if (line == "")
                    return false;

                if (line.Contains("Slot:"))
                    return false;

                return true;
            }

            public bool IsValid()
            {
                return slot != null && HasAura != null && packetSendTime != TimeSpan.Zero;
            }

            public static string GetUnitGuidFromAuraUpdatePacket(string[] lines, long index, BuildVersions build)
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], build, unitGuid: true) != "")
                        return LineGetters.GetGuidFromLine(lines[index], build, unitGuid: true);

                    index++;
                }
                while (lines[index] != "");

                return "";
            }

            public static IEnumerable<AuraUpdatePacket> ParseAuraUpdatePacket(string[] lines, long index, BuildVersions buildVersion)
            {
                TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                List<AuraUpdatePacket> aurasList = new List<AuraUpdatePacket>();
                string guid = "";

                do
                {
                    if (lines[index].Contains("Slot:"))
                    {
                        AuraUpdatePacket auraUpdatePacket = new AuraUpdatePacket("", null, 0, null, packetSendTime);

                        do
                        {
                            if (GetAuraSlotFromLine(lines[index]) != null)
                                auraUpdatePacket.slot = GetAuraSlotFromLine(lines[index]);

                            if (GetHasAuraFromLine(lines[index]) != null)
                                auraUpdatePacket.HasAura = GetHasAuraFromLine(lines[index]);

                            if (SpellPacket.GetSpellIdFromLine(lines[index]) != 0)
                                auraUpdatePacket.spellId = SpellPacket.GetSpellIdFromLine(lines[index]);

                            if (guid == "" && LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true) != "")
                            {
                                guid = LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true);
                            }

                            index++;
                        }
                        while (IsLineValidForAuraUpdateParsing(lines[index]));

                        if (!auraUpdatePacket.IsValid())
                            continue;

                        aurasList.Add(auraUpdatePacket);

                        index--;
                    }

                    index++;
                }
                while (lines[index] != "");

                if (guid == "")
                {
                    aurasList.Clear();
                }
                else
                {
                    AuraUpdatePacket[] auraPackets = new AuraUpdatePacket[aurasList.Count];
                    auraPackets = aurasList.ToArray();

                    for (int i = 0; i < auraPackets.Length; i++)
                    {
                        auraPackets[i].unitGuid = guid;
                    }

                    aurasList = auraPackets.ToList();
                }

                return aurasList;
            }
        }

        [ProtoContract]
        public class EmotePacket
        {
            [ProtoMember(1)]
            public string guid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint emoteId
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public EmotePacket() { }

            public EmotePacket(string guid, uint emote, TimeSpan time)
            { this.guid = guid; emoteId = emote; packetSendTime = time; }

            public static string GetGuidFromLine(string line)
            {
                Regex guidRegex = new Regex(@"GUID: TypeName: Creature; Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("GUID: TypeName: Creature; Full: ", "");

                return "";
            }

            public static uint GetEmoteIdFromLine(string line)
            {
                Regex emoteRegex = new Regex(@"EmoteID:{1}\s{1}\d+");
                if (emoteRegex.IsMatch(line))
                    return Convert.ToUInt32(emoteRegex.Match(line).ToString().Replace("EmoteID: ", ""));

                return 0;
            }

            public static EmotePacket ParseEmotePacket(string[] lines, long index)
            {
                EmotePacket emotePacket = new EmotePacket("", 0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetGuidFromLine(lines[index]) != "")
                        emotePacket.guid = GetGuidFromLine(lines[index]);

                    if (GetEmoteIdFromLine(lines[index]) != 0)
                        emotePacket.emoteId = GetEmoteIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return emotePacket;
            }
        }

        [ProtoContract]
        public class SetAiAnimKitPacket
        {
            [ProtoMember(1)]
            public string guid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint? aiAnimKitId
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public SetAiAnimKitPacket() { }

            public SetAiAnimKitPacket(string guid, uint animKitId, TimeSpan time)
            { this.guid = guid; aiAnimKitId = animKitId; packetSendTime = time; }

            public static string GetGuidFromLine(string line)
            {
                Regex guidRegex = new Regex(@"Unit: TypeName: Creature; Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Unit: TypeName: Creature; Full: ", "");

                return "";
            }

            public static uint? GetAiAnimKitIdFromLine(string line)
            {
                Regex emoteRegex = new Regex(@"AiAnimKitId:{1}\s{1}\d+");
                if (emoteRegex.IsMatch(line))
                    return Convert.ToUInt32(emoteRegex.Match(line).ToString().Replace("AiAnimKitId: ", ""));

                return null;
            }

            public static SetAiAnimKitPacket ParseSetAiAnimKitPacket(string[] lines, long index)
            {
                SetAiAnimKitPacket animPacket = new SetAiAnimKitPacket("", 0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetGuidFromLine(lines[index]) != "")
                        animPacket.guid = GetGuidFromLine(lines[index]);

                    if (GetAiAnimKitIdFromLine(lines[index]) != null)
                        animPacket.aiAnimKitId = GetAiAnimKitIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return animPacket;
            }
        }

        [ProtoContract]
        public class PlayOneShotAnimKitPacket
        {
            [ProtoMember(1)]
            public string guid
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint? animKitId
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public PlayOneShotAnimKitPacket() { }

            public PlayOneShotAnimKitPacket(string guid, uint animKitId, TimeSpan time)
            {
                this.guid = guid;
                this.animKitId = animKitId;
                packetSendTime = time;
            }

            public static string GetGuidFromLine(string line)
            {
                Regex guidRegex = new Regex(@"Unit: TypeName: Creature; Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Unit: TypeName: Creature; Full: ", "");

                return "";
            }

            public static uint? GetAnimKitIdFromLine(string line)
            {
                Regex animKitRegex = new Regex(@"AnimKitID:{1}\s+\d+");
                if (animKitRegex.IsMatch(line))
                    return Convert.ToUInt32(animKitRegex.Match(line).ToString().Replace("AnimKitID: ", ""));

                return null;
            }

            public static PlayOneShotAnimKitPacket ParsePlayOneShotAnimKitPacket(string[] lines, long index)
            {
                PlayOneShotAnimKitPacket animPacket = new PlayOneShotAnimKitPacket("", 0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetGuidFromLine(lines[index]) != "")
                        animPacket.guid = GetGuidFromLine(lines[index]);

                    if (GetAnimKitIdFromLine(lines[index]) != null)
                        animPacket.animKitId = GetAnimKitIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return animPacket;
            }
        }

        [ProtoContract]
        public class QuestGiverAcceptQuestPacket
        {
            [ProtoMember(1)]
            public uint questId
            {
                get; set;
            }

            [ProtoMember(2)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public QuestGiverAcceptQuestPacket() { }

            public QuestGiverAcceptQuestPacket(uint questId, TimeSpan time)
            { this.questId = questId; packetSendTime = time; }

            public static QuestGiverAcceptQuestPacket ParseQuestGiverAcceptQuestPacket(string[] lines, long index)
            {
                QuestGiverAcceptQuestPacket questAcceptPacket = new QuestGiverAcceptQuestPacket(0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetQuestIdFromLine(lines[index]) != 0)
                        questAcceptPacket.questId = GetQuestIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return questAcceptPacket;
            }

            private static uint GetQuestIdFromLine(string line)
            {
                Regex questIdRegex = new Regex(@"QuestID:{1}\s{1}\d+");
                if (questIdRegex.IsMatch(line))
                    return Convert.ToUInt32(questIdRegex.Match(line).ToString().Replace("QuestID: ", ""));

                return 0;
            }
        }

        [ProtoContract]
        public class QuestGiverQuestCompletePacket
        {
            [ProtoMember(1)]
            public uint questId
            {
                get; set;
            }

            [ProtoMember(2)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public QuestGiverQuestCompletePacket() { }

            public QuestGiverQuestCompletePacket(uint questId, TimeSpan time)
            { this.questId = questId; packetSendTime = time; }

            public static QuestGiverQuestCompletePacket ParseQuestGiverQuestCompletePacket(string[] lines, long index)
            {
                QuestGiverQuestCompletePacket questCompletePacket = new QuestGiverQuestCompletePacket(0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetQuestIdFromLine(lines[index]) != 0)
                        questCompletePacket.questId = GetQuestIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return questCompletePacket;
            }

            private static uint GetQuestIdFromLine(string line)
            {
                Regex questIdRegex = new Regex(@"QuestId:{1}\s{1}\d+");
                if (questIdRegex.IsMatch(line))
                    return Convert.ToUInt32(questIdRegex.Match(line).ToString().Replace("QuestId: ", ""));

                return 0;
            }
        }

        [ProtoContract]
        public class PlayerMovePacket
        {
            [ProtoMember(1)]
            public string playerGuid
            {
                get; set;
            }

            [ProtoMember(2)]
            public Position position
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public PlayerMovePacket() { }

            public PlayerMovePacket(string playerGuid, Position position, TimeSpan time)
            { this.playerGuid = playerGuid; this.position = position; packetSendTime = time; }

            public static PlayerMovePacket ParsePlayerMovePacket(string[] lines, long index, BuildVersions buildVersion)
            {
                PlayerMovePacket playerMovePacket = new PlayerMovePacket("", new Position(), LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true) != "")
                        playerMovePacket.playerGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true);

                    if (GetPositionFromLine(lines[index]).IsValid())
                        playerMovePacket.position = GetPositionFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return playerMovePacket;
            }

            public static Position GetPositionFromLine(string line)
            {
                Position position = new Position();

                Regex xyzRegex = new Regex(@"Position:{1}\s{1}X:{1}.+");
                if (xyzRegex.IsMatch(line))
                {
                    string[] splittedLine = xyzRegex.Match(line).ToString().Replace("Position: X: ", "").Split(' ');

                    position.x = float.Parse(splittedLine[0], CultureInfo.InvariantCulture.NumberFormat);
                    position.y = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    position.z = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                }

                return position;
            }
        }

        [ProtoContract]
        public class QuestUpdateAddCreditPacket
        {
            [ProtoMember(1)]
            public uint questId
            {
                get; set;
            }

            [ProtoMember(2)]
            public uint objectId
            {
                get; set;
            }

            [ProtoMember(3)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public QuestUpdateAddCreditPacket() { }

            public QuestUpdateAddCreditPacket(uint questId, uint objectId, TimeSpan time)
            { this.questId = questId; this.objectId = objectId; packetSendTime = time; }

            public static QuestUpdateAddCreditPacket ParseQuestUpdateAddCreditPacket(string[] lines, long index)
            {
                QuestUpdateAddCreditPacket addCreditPacket = new QuestUpdateAddCreditPacket(0, 0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetQuestIdFromLine(lines[index]) != 0)
                        addCreditPacket.questId = GetQuestIdFromLine(lines[index]);

                    if (GetObjectIdFromLine(lines[index]) != 0)
                        addCreditPacket.objectId = GetObjectIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return addCreditPacket;
            }

            private static uint GetQuestIdFromLine(string line)
            {
                Regex questIdRegex = new Regex(@"QuestID:{1}\s{1}\d+");
                if (questIdRegex.IsMatch(line))
                    return Convert.ToUInt32(questIdRegex.Match(line).ToString().Replace("QuestID: ", ""));

                return 0;
            }

            private static uint GetObjectIdFromLine(string line)
            {
                Regex questIdRegex = new Regex(@"ObjectID:{1}\s{1}\d+");
                if (questIdRegex.IsMatch(line))
                    return Convert.ToUInt32(questIdRegex.Match(line).ToString().Replace("ObjectID: ", ""));

                return 0;
            }
        }

        [ProtoContract]
        public class QuestUpdateCompletePacket
        {
            [ProtoMember(1)]
            public uint questId
            {
                get; set;
            }

            [ProtoMember(2)]
            public TimeSpan packetSendTime
            {
                get; set;
            }

            public QuestUpdateCompletePacket() { }

            public QuestUpdateCompletePacket(uint questId, TimeSpan time)
            { this.questId = questId; packetSendTime = time; }

            public static QuestUpdateCompletePacket ParseQuestUpdateCompletePacket(string[] lines, long index)
            {
                QuestUpdateCompletePacket addCreditPacket = new QuestUpdateCompletePacket(0, LineGetters.GetTimeSpanFromLine(lines[index]));

                do
                {
                    if (GetQuestIdFromLine(lines[index]) != 0)
                        addCreditPacket.questId = GetQuestIdFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                return addCreditPacket;
            }

            private static uint GetQuestIdFromLine(string line)
            {
                Regex questIdRegex = new Regex(@"QuestID:{1}\s{1}\d+");
                if (questIdRegex.IsMatch(line))
                    return Convert.ToUInt32(questIdRegex.Match(line).ToString().Replace("QuestID: ", ""));

                return 0;
            }
        }
    }
}
