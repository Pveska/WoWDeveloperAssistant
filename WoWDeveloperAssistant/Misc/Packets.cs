using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Parsed_File_Advisor;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Database_Advisor.CreatureFlagsAdvisor;
using static WoWDeveloperAssistant.Misc.Packets.Packet;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Misc
{
    public static class Packets
    {
        [Serializable]
        public struct Packet
        {
            public PacketTypes packetType;
            public TimeSpan sendTime;
            public long index;
            public List<object> parsedPacketsList;
            public long number;

            public Packet(PacketTypes type, TimeSpan time, long index, List<object> parsedList, long number)
            { packetType = type; sendTime = time; this.index = index; parsedPacketsList = parsedList; this.number = number; }

            public enum PacketTypes : byte
            {
                UNKNOWN_PACKET,
                SMSG_UPDATE_OBJECT,
                SMSG_AI_REACTION,
                SMSG_SPELL_START,
                SMSG_CHAT,
                SMSG_ON_MONSTER_MOVE,
                SMSG_ATTACK_STOP,
                SMSG_AURA_UPDATE,
                SMSG_EMOTE,
                SMSG_SPELL_GO,
                SMSG_SET_AI_ANIM_KIT,
                CMSG_QUEST_GIVER_ACCEPT_QUEST,
                SMSG_QUEST_GIVER_QUEST_COMPLETE,
                CMSG_MOVE_START_FORWARD,
                CMSG_MOVE_STOP,
                CMSG_MOVE_HEARTBEAT,
                SMSG_QUEST_UPDATE_ADD_CREDIT,
                SMSG_QUEST_UPDATE_COMPLETE,
                SMSG_PLAY_ONE_SHOT_ANIM_KIT
            }

            public static PacketTypes GetPacketTypeFromLine(string line)
            {
                foreach (string packetName in Enum.GetNames(typeof(PacketTypes)))
                {
                    if (line.Contains(packetName))
                    {
                        Regex packetnameRegex = new Regex(packetName + @"{1}\s+");
                        if (packetnameRegex.IsMatch(line))
                            return (PacketTypes)Enum.Parse(typeof(PacketTypes), packetnameRegex.Match(line).ToString());
                    }
                }

                return PacketTypes.UNKNOWN_PACKET;
            }

            public bool HasCreatureWithGuid(string guid)
            {
                if (parsedPacketsList.Count == 0)
                    return false;

                switch (packetType)
                {
                    case PacketTypes.SMSG_UPDATE_OBJECT:
                    {
                        if (parsedPacketsList.Cast<UpdateObjectPacket>().Any(updatePacket => updatePacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_ON_MONSTER_MOVE:
                    {
                        if (parsedPacketsList.Cast<MonsterMovePacket>().Any(movementPacket => movementPacket.creatureGuid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_SPELL_START:
                    {
                        if (parsedPacketsList.Cast<SpellStartPacket>().Any(spellPacket => spellPacket.casterGuid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_AURA_UPDATE:
                    {
                        if (parsedPacketsList.Cast<AuraUpdatePacket>().Any(auraPacket => auraPacket.unitGuid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_EMOTE:
                    {
                        if (parsedPacketsList.Cast<EmotePacket>().Any(emotePacket => emotePacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_ATTACK_STOP:
                    {
                        if (parsedPacketsList.Cast<AttackStopPacket>().Any(attackStopPacket => attackStopPacket.creatureGuid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_SET_AI_ANIM_KIT:
                    {
                        if (parsedPacketsList.Cast<SetAiAnimKitPacket>().Any(animKitPacket => animKitPacket.guid == guid))
                            return true;

                        break;
                    }
                    case PacketTypes.SMSG_PLAY_ONE_SHOT_ANIM_KIT:
                    {
                        if (parsedPacketsList.Cast<PlayOneShotAnimKitPacket>().Any(playOneShotAnimKitPacket => playOneShotAnimKitPacket.guid == guid))
                            return true;

                        break;
                    }
                }

                return false;
            }

            public bool IsScriptPacket()
            {
                switch (packetType)
                {
                    case PacketTypes.SMSG_UPDATE_OBJECT:
                    case PacketTypes.SMSG_SPELL_START:
                        return true;
                    default:
                        return false;
                }
            }

            public static bool IsPlayerMovePacket(PacketTypes packetType)
            {
                return packetType == PacketTypes.CMSG_MOVE_START_FORWARD || packetType == PacketTypes.CMSG_MOVE_STOP || packetType == PacketTypes.CMSG_MOVE_HEARTBEAT;
            }

            public static bool IsQuestPacket(PacketTypes packetType)
            {
                return packetType == PacketTypes.CMSG_QUEST_GIVER_ACCEPT_QUEST || packetType == PacketTypes.SMSG_QUEST_GIVER_QUEST_COMPLETE || packetType == PacketTypes.SMSG_QUEST_UPDATE_ADD_CREDIT || packetType == PacketTypes.SMSG_QUEST_UPDATE_COMPLETE;
            }
        }

        [Serializable]
        public struct SpellStartPacket
        {
            public string casterGuid;
            public uint spellId;
            public TimeSpan spellCastTime;
            public TimeSpan spellCastStartTime;
            public Position spellDestination;
            public PacketTypes type;

            public SpellStartPacket(string guid, uint id, TimeSpan castTime, TimeSpan startTime, Position spellDest, PacketTypes type)
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

            public static SpellStartPacket ParseSpellStartPacket(string[] lines, long index, BuildVersions buildVersion, PacketTypes type, bool playerPacket = false)
            {
                SpellStartPacket spellPacket = new SpellStartPacket("", 0, new TimeSpan(), LineGetters.GetTimeSpanFromLine(lines[index]), new Position(), type);

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

                            index++;
                        }
                        while (lines[index] != "");
                    }
                }

                return spellPacket;
            }
        };

        [Serializable]
        public struct ChatPacket
        {
            public string creatureGuid;
            public uint creatureEntry;
            public string creatureText;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct UpdateObjectPacket
        {
            public ObjectTypes objectType;
            public uint entry;
            public string guid;
            public string transportGuid;
            public int currentHealth;
            public uint maxHealth;
            public TimeSpan packetSendTime;
            public Position spawnPosition;
            public uint? mapId;
            public List<Waypoint> waypoints;
            public uint? emoteStateId;
            public uint? sheatheState;
            public uint? standState;
            public bool hasDisableGravity;
            public bool isCyclic;
            public uint moveTime;
            public uint? unitFlags;
            public MonsterMovePacket.JumpInfo jumpInfo;
            public ConversationData conversationData;
            public Dictionary<uint, MonsterMovePacket.FilterKey> filterKeys;
            public List<uint> virtualItems;

            public UpdateObjectPacket(ObjectTypes objectType, uint entry, string guid, string transportGuid, string name, int curHealth, uint maxHealth, TimeSpan time, Position spawnPos, uint? mapId, List<Waypoint> waypoints, uint? emote, uint? sheatheState, uint? standState, bool hasDisableGravity, bool isCyclic, uint moveTime, uint? unitFlags, MonsterMovePacket.JumpInfo jumpInfo, ConversationData conversationData, Dictionary<uint, MonsterMovePacket.FilterKey> filterKeys, List<uint> virtualItems)
            { this.objectType = objectType; this.entry = entry; this.guid = guid; this.transportGuid = transportGuid; currentHealth = curHealth; this.maxHealth = maxHealth; packetSendTime = time; spawnPosition = spawnPos; this.mapId = mapId; this.waypoints = waypoints; emoteStateId = emote; this.sheatheState = sheatheState; this.standState = standState; this.hasDisableGravity = hasDisableGravity; this.isCyclic = isCyclic; this.moveTime = moveTime; this.unitFlags = unitFlags; this.jumpInfo = jumpInfo; this.conversationData = conversationData; this.filterKeys = filterKeys; this.virtualItems = virtualItems; }

            public enum ObjectTypes
            {
                Unit         = 5,
                GameObject   = 8,
                Conversation = 13
            }

            [Serializable]
            public struct ConversationData
            {
                public List<KeyValuePair<string, uint>> conversationActors;
                public List<KeyValuePair<uint, uint?>> conversationLines;

                public ConversationData(List<KeyValuePair<string, uint>> conversationActors, List<KeyValuePair<uint, uint?>> conversationLines)
                { this.conversationActors = conversationActors; this.conversationLines = conversationLines; }
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

            public static uint GetMaxHealthFromLine(string line)
            {
                Regex maxHealthRegex = new Regex(@"MaxHealth:{1}\s+\d+");
                if (maxHealthRegex.IsMatch(line))
                    return Convert.ToUInt32(maxHealthRegex.Match(line).ToString().Replace("MaxHealth: ", ""));

                return 0;
            }

            public static bool GetSpawnPositionFromLine(ref Position spawnPos, string xyznLine, string oriLine)
            {
                if (xyznLine.Contains("TransportPosition"))
                {
                    Regex xyzRegex = new Regex(@"TransportPosition:\s{1}X:{1}\s{1}");
                    if (xyzRegex.IsMatch(xyznLine))
                    {
                        string[] splittedLine = xyznLine.Split(' ');

                        spawnPos.x = float.Parse(splittedLine[4], CultureInfo.InvariantCulture.NumberFormat);
                        spawnPos.y = float.Parse(splittedLine[6], CultureInfo.InvariantCulture.NumberFormat);
                        spawnPos.z = float.Parse(splittedLine[8], CultureInfo.InvariantCulture.NumberFormat);
                        spawnPos.orientation = float.Parse(splittedLine[10], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    return true;
                }
                else if (!spawnPos.IsValid() && xyznLine.Contains("Position:"))
                {
                    Regex xyzRegex = new Regex(@"Position:\s{1}X:{1}\s{1}");
                    if (xyzRegex.IsMatch(xyznLine))
                    {
                        string[] splittedLine = xyznLine.Split(' ');

                        spawnPos.x = float.Parse(splittedLine[3], CultureInfo.InvariantCulture.NumberFormat);
                        spawnPos.y = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                        spawnPos.z = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    Regex oriRegex = new Regex(@"Orientation:\s{1}");
                    if (oriRegex.IsMatch(oriLine))
                    {
                        string[] splittedLine = oriLine.Split(' ');

                        spawnPos.orientation = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                    }

                    return true;
                }

                return false;
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
                Regex durationRegex = new Regex(@"] Duration:{1}\s{1}\w+");
                if (durationRegex.IsMatch(line))
                    return Convert.ToUInt32(durationRegex.Match(line).ToString().Replace("] Duration: ", ""));

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

            public static uint GetObjectTypeFromLine(string line)
            {
                Regex objectTypeRegex = new Regex(@"ObjectType:{1}\s{1}\w+");
                if (objectTypeRegex.IsMatch(line))
                    return Convert.ToUInt32(objectTypeRegex.Match(line).ToString().Replace("ObjectType: ", ""));

                return 0;
            }

            public static bool ObjectIsValidForParse(string line)
            {
                if (line.Contains("Creature") || line.Contains("Vehicle") || line.Contains("Transport"))
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

            public static KeyValuePair<string, uint> GetActorDataFromLine(string line, BuildVersions buildVersion)
            {
                string actorGuid = LineGetters.GetGuidFromLine(line, buildVersion, conversationActorGuid: true);
                uint actorIndex = 0;

                Regex actorIndexRegex = new Regex(@"\(ConversationData\) \(Actors\){1}\s\[{1}\w+\]{1}");
                if (actorIndexRegex.IsMatch(line))
                {
                    actorIndex = Convert.ToUInt32(actorIndexRegex.Match(line).ToString().Replace("(ConversationData) (Actors) [", "").Replace("]", ""));
                }

                return new KeyValuePair<string, uint>(actorGuid, actorIndex);
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

            public static uint? GetItemIdFromLine(string line)
            {
                Regex itemIdRegex = new Regex(@"ItemID:{1}\s{1}\w+");
                if (itemIdRegex.IsMatch(line))
                    return Convert.ToUInt32(itemIdRegex.Match(line).ToString().Replace("ItemID: ", ""));

                return null;
            }

            public static IEnumerable<UpdateObjectPacket> ParseObjectUpdatePacket(string[] lines, long index, BuildVersions buildVersion, long packetNumber)
            {
                TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                List<UpdateObjectPacket> updatePacketsList = new List<UpdateObjectPacket>();

                do
                {
                    if (((lines[index].Contains("UpdateType: 1 (CreateObject1)") || lines[index].Contains("UpdateType: CreateObject1")) || (lines[index].Contains("UpdateType: 2 (CreateObject2)") || lines[index].Contains("UpdateType: CreateObject2"))) && ObjectIsValidForParse(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion, objectFieldGuid: true), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());
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

                            if (updatePacket.objectType == 0)
                            {
                                tempUpdatePacket.objectType = (ObjectTypes)GetObjectTypeFromLine(lines[index]);

                                if (tempUpdatePacket.objectType != 0)
                                {
                                    updatePacket.objectType = tempUpdatePacket.objectType;
                                    index++;
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
                                        Waypoint wp = new Waypoint(MonsterMovePacket.GetPointPositionFromLine(lines[index]), 0.0f, 0, new Position(), 0, packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, updatePacket.hasDisableGravity ? MonsterMovePacket.MoveType.MOVE_FLIGHT : MonsterMovePacket.MoveType.MOVE_MAX);
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

                            if (GetSpawnPositionFromLine(ref updatePacket.spawnPosition, lines[index], lines[index + 1]))
                            {
                                index++;
                                continue;
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
                                    updatePacket.maxHealth = tempUpdatePacket.entry;
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
                                uint? itemId = GetItemIdFromLine(lines[index]);
                                if (itemId != null)
                                {
                                    updatePacket.virtualItems.Add((uint)itemId);
                                    index++;
                                    continue;
                                }
                            }

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.entry == 0 || updatePacket.guid == "")
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
                                            updatePacket.waypoints[i].moveType = MonsterMovePacket.MoveType.MOVE_RUN;
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < updatePacket.waypoints.Count; i++)
                                        {
                                            updatePacket.waypoints[i].moveType = MonsterMovePacket.MoveType.MOVE_WALK;
                                        }
                                    }
                                }
                            }
                        }

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if ((lines[index].Contains("UpdateType: 0 (Values)") || lines[index].Contains("UpdateType: Values")) && ObjectIsValidForParse(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());

                        do
                        {
                            if (updatePacket.maxHealth == 0)
                            {
                                tempUpdatePacket.maxHealth = GetMaxHealthFromLine(lines[index]);

                                if (tempUpdatePacket.maxHealth != 0)
                                {
                                    updatePacket.maxHealth = tempUpdatePacket.entry;
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

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.guid == "")
                            continue;

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if (((lines[index].Contains("UpdateType: 1 (CreateObject1)") || lines[index].Contains("UpdateType: CreateObject1")) || (lines[index].Contains("UpdateType: 2 (CreateObject2)") || lines[index].Contains("UpdateType: CreateObject2"))) && lines[index + 1].IsConversationLine())
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, 0, LineGetters.GetGuidFromLine(lines[index + 1], buildVersion), "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(new List<KeyValuePair<string, uint>>(), new List<KeyValuePair<uint, uint?>>()), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());
                        UpdateObjectPacket tempUpdatePacket = new UpdateObjectPacket(0, 0, "", "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false, false, 0, null, new MonsterMovePacket.JumpInfo(), new ConversationData(), new Dictionary<uint, MonsterMovePacket.FilterKey>(), new List<uint>());

                        do
                        {
                            if (updatePacket.objectType == 0)
                            {
                                tempUpdatePacket.objectType = (ObjectTypes)GetObjectTypeFromLine(lines[index]);

                                if (tempUpdatePacket.objectType != 0)
                                {
                                    updatePacket.objectType = tempUpdatePacket.objectType;
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

                            if (GetConversationLineIdFromLine(lines[index]) != 0)
                            {
                                uint conversationLineId = GetConversationLineIdFromLine(lines[index]);

                                do
                                {
                                    if (GetActorIndexFromLine(lines[index]) != null)
                                    {
                                        updatePacket.conversationData.conversationLines.Add(new KeyValuePair<uint, uint?>(conversationLineId, GetActorIndexFromLine(lines[index])));
                                    }

                                    index++;
                                } while (!lines[index].Contains("ConversationLineID") && lines[index].Contains("Lines"));

                                index--;
                            }

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, conversationActorGuid: true) != "")
                            {
                                updatePacket.conversationData.conversationActors.Add(GetActorDataFromLine(lines[index], buildVersion));
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

        [Serializable]
        public struct MonsterMovePacket
        {
            public string creatureGuid;
            public float creatureOrientation;
            public TimeSpan packetSendTime;
            public List<Waypoint> waypoints;
            public uint moveTime;
            public Position startPos;
            public JumpInfo jumpInfo;

            public enum MoveType
            {
                MOVE_WALK   = 0,
                MOVE_RUN    = 1,
                MOVE_FLIGHT = 4,
                MOVE_MAX    = 5
            };

            [Serializable]
            public struct JumpInfo
            {
                public uint moveTime;
                public float jumpGravity;
                public Position jumpPos;

                public JumpInfo(uint time, float gravity, Position positon)
                { moveTime = time; jumpGravity = gravity; jumpPos = positon; }

                public bool IsValid()
                {
                    return moveTime != 0 && jumpGravity != 0.0f && jumpPos.IsValid();
                }
            }

            [Serializable]
            public struct FilterKey
            {
                public float In;
                public float Out;
            };

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

            public bool HasCombatMovement(SortedDictionary<long, Packet> updateObjectPacketsDict)
            {
                string guid = creatureGuid;
                TimeSpan sendTime = packetSendTime;

                List<UpdateObjectPacket> creatureUpdateObjectPackets = new List<UpdateObjectPacket>();

                foreach (Packet packet in updateObjectPacketsDict.Values.Where(x => x.HasCreatureWithGuid(guid) && x.sendTime.TotalSeconds < sendTime.TotalSeconds).OrderBy(x => x.sendTime.TotalSeconds))
                {
                    creatureUpdateObjectPackets.Add((UpdateObjectPacket)packet.parsedPacketsList.First(x => ((UpdateObjectPacket)x).guid == guid));
                }

                UpdateObjectPacket firstNonCombatPacketFromDescent = new UpdateObjectPacket();
                UpdateObjectPacket firstCombatPacketFromDescent = new UpdateObjectPacket();

                for (int i = creatureUpdateObjectPackets.Count() - 1; i >= 0; i--)
                {
                    if (creatureUpdateObjectPackets[i].unitFlags != null && ((UnitFlags)creatureUpdateObjectPackets[i].unitFlags & UnitFlags.AffectingCombat) == 0)
                    {
                        firstNonCombatPacketFromDescent = creatureUpdateObjectPackets[i];
                        break;
                    }
                }

                for (int i = creatureUpdateObjectPackets.Count() - 1; i >= 0; i--)
                {
                    if (creatureUpdateObjectPackets[i].unitFlags != null && ((UnitFlags)creatureUpdateObjectPackets[i].unitFlags & UnitFlags.AffectingCombat) != 0)
                    {
                        firstCombatPacketFromDescent = creatureUpdateObjectPackets[i];
                        break;
                    }
                }

                if (firstNonCombatPacketFromDescent.guid == "" && firstCombatPacketFromDescent.guid != "")
                    return true;
                else if (firstNonCombatPacketFromDescent.guid != "" && firstCombatPacketFromDescent.guid != "" && firstNonCombatPacketFromDescent.packetSendTime.TotalSeconds < firstCombatPacketFromDescent.packetSendTime.TotalSeconds)
                    return true;

                return false;
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
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                    if (skipCombatMovement && movePacket.creatureGuid != "" && movePacket.HasCombatMovement(updateObjectPacketsDict))
                        return tempMovePacket;

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
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                            Waypoint wp = new Waypoint(point, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                            Waypoint wp = new Waypoint(waypoint, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId, isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                                        Waypoint wp = new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
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
                        Waypoint wp = new Waypoint(lastPosition, movePacket.creatureOrientation, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1), isFlying == true ? MoveType.MOVE_FLIGHT : MoveType.MOVE_MAX);
                        wp.packetNumber = -1;
                        movePacket.waypoints.Add(wp);
                    }
                }

                return movePacket;
            }
        }

        [Serializable]
        public struct AttackStopPacket
        {
            public string creatureGuid;
            public bool nowDead;
            public TimeSpan packetSendTime;

            public AttackStopPacket(string guid, bool dead, TimeSpan time)
            { creatureGuid = guid; nowDead = dead; packetSendTime = time; }

            public static bool GetNowDeadFromLine(string line)
            {
                Regex nowDeadRegex = new Regex(@"NowDead:{1}\s+\w+");
                if (nowDeadRegex.IsMatch(line))
                    return nowDeadRegex.Match(line).ToString().Replace("NowDead: ", "") == "True";

                return false;
            }

            public static AttackStopPacket ParseAttackStopkPacket(string[] lines, long index, BuildVersions buildVersion)
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

        [Serializable]
        public struct TimePacket
        {
            public string hours;
            public string minutes;
            public string seconds;
        }

        [Serializable]
        public struct AIReactionPacket
        {
            public string creatureGuid;
            public uint creatureEntry;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct AuraUpdatePacket
        {
            public string unitGuid;
            public uint? slot;
            public uint spellId;
            public bool? HasAura;
            public TimeSpan packetSendTime;

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

                            if (SpellStartPacket.GetSpellIdFromLine(lines[index]) != 0)
                                auraUpdatePacket.spellId = SpellStartPacket.GetSpellIdFromLine(lines[index]);

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

        [Serializable]
        public struct EmotePacket
        {
            public string guid;
            public uint emoteId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct SetAiAnimKitPacket
        {
            public string guid;
            public uint? aiAnimKitId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public class PlayOneShotAnimKitPacket
        {
            public string guid;
            public uint? animKitId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct QuestGiverAcceptQuestPacket
        {
            public uint questId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct QuestGiverQuestCompletePacket
        {
            public uint questId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct PlayerMovePacket
        {
            public string playerGuid;
            public Position position;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct QuestUpdateAddCreditPacket
        {
            public uint questId;
            public uint objectId;
            public TimeSpan packetSendTime;

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

        [Serializable]
        public struct QuestUpdateCompletePacket
        {
            public uint questId;
            public TimeSpan packetSendTime;

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
