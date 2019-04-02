using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant
{
    public static class Packets
    {
        public struct Packet
        {
            public PacketTypes packetType;
            public TimeSpan sendTime;
            public long index;
            public List<object> parsedPacketsList;

            public Packet(PacketTypes type, TimeSpan time, long index, List<object> parsedList)
            { packetType = type; sendTime = time; this.index = index; parsedPacketsList = parsedList; }

            public static PacketTypes GetPacketTypeFromLine(string line)
            {
                PacketTypes packetType = PacketTypes.UNKNOWN_PACKET;

                if (line.Contains("SMSG_UPDATE_OBJECT"))
                    packetType = PacketTypes.SMSG_UPDATE_OBJECT;
                else if (line.Contains("SMSG_SPELL_START"))
                    packetType = PacketTypes.SMSG_SPELL_START;
                else if (line.Contains("SMSG_ON_MONSTER_MOVE"))
                    packetType = PacketTypes.SMSG_ON_MONSTER_MOVE;
                else if (line.Contains("SMSG_AURA_UPDATE"))
                    packetType = PacketTypes.SMSG_AURA_UPDATE;

                return packetType;
            }

            public bool HasCreatureWithGuid(string guid)
            {
                if (parsedPacketsList.Count == 0)
                    return false;

                switch (packetType)
                {
                    case PacketTypes.SMSG_UPDATE_OBJECT:
                    {
                        foreach (UpdateObjectPacket updatePacket in parsedPacketsList)
                        {
                            if (updatePacket.creatureGuid == guid)
                                return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_ON_MONSTER_MOVE:
                    {
                        foreach (MonsterMovePacket movementPacket in parsedPacketsList)
                        {
                            if (movementPacket.creatureGuid == guid)
                                return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_SPELL_START:
                    {
                        foreach (SpellStartPacket spellPacket in parsedPacketsList)
                        {
                            if (spellPacket.casterGuid == guid)
                                return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_AURA_UPDATE:
                    {
                        foreach (AuraUpdatePacket auraPacket in parsedPacketsList)
                        {
                            if (auraPacket.unitGuid == guid)
                                return true;
                        }

                        break;
                    }
                    default:
                        break;
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
        }

        public struct SpellStartPacket
        {
            public string casterGuid;
            public uint spellId;
            public TimeSpan spellCastTime;
            public TimeSpan spellCastStartTime;

            public SpellStartPacket(string guid, uint id, TimeSpan castTime, TimeSpan startTime)
            { casterGuid = guid; spellId = id; spellCastTime = castTime; spellCastStartTime = startTime; }

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
                if (castTimeRegex.IsMatch(line))
                    return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(castTimeRegex.Match(line).ToString().Replace("CastTime: ", "")));

                return new TimeSpan();
            }

            public static bool IsCreatureSpellCastLine(string line)
            {
                if (line.Contains("CasterGUID: Full:") &&
                    (line.Contains("Creature/0") || line.Contains("Vehicle/0")))
                    return true;

                return false;
            }
        };

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
        }

        public struct UpdateObjectPacket
        {
            public uint creatureEntry;
            public string creatureGuid;
            public string creatureName;
            public int creatureCurrentHealth;
            public uint creatureMaxHealth;
            public TimeSpan packetSendTime;
            public Position spawnPosition;
            public uint mapId;
            public List<Waypoint> waypoints;
            public uint? emoteStateId;
            public uint? sheatheState;

            public UpdateObjectPacket(uint entry, string guid, string name, int curHealth, uint maxHealth, TimeSpan time, Position spawnPos, uint mapId, List<Waypoint> waypoints, uint? emote, uint? sheatheState)
            { creatureEntry = entry; creatureGuid = guid; creatureName = name; creatureCurrentHealth = curHealth; creatureMaxHealth = maxHealth; packetSendTime = time; spawnPosition = spawnPos; this.mapId = mapId; this.waypoints = waypoints; emoteStateId = emote; this.sheatheState = sheatheState; }

            public static bool IsLineValidForObjectParse(string line)
            {
                if (line == null)
                    return false;

                if (line == "")
                    return false;

                if (line.Contains("UpdateType: CreateObject1"))
                    return false;

                if (line.Contains("UpdateType: CreateObject2"))
                    return false;

                if (line.Contains("UpdateType: Values"))
                    return false;

                return true;
            }

            public static uint GetEntryFromLine(string line)
            {
                Regex entryRegexField = new Regex(@"OBJECT_FIELD_ENTRY:{1}\s*\d+");
                if (entryRegexField.IsMatch(line))
                    return Convert.ToUInt32(entryRegexField.Match(line).ToString().Replace("OBJECT_FIELD_ENTRY: ", ""));
                else
                    return 0;
            }

            public static int GetHealthFromLine(string line)
            {
                Regex healthRegex = new Regex(@"UNIT_FIELD_HEALTH:{1}\s+\d+");
                if (healthRegex.IsMatch(line))
                    try
                    {
                        return Convert.ToInt32(healthRegex.Match(line).ToString().Replace("UNIT_FIELD_HEALTH: ", ""));
                    }
                    catch
                    {
                        return -1;
                    }

                return -1;
            }

            public static uint GetMaxHealthFromLine(string line)
            {
                Regex maxHealthRegex = new Regex(@"UNIT_FIELD_MAXHEALTH:{1}\s+\d+");
                if (maxHealthRegex.IsMatch(line))
                    return Convert.ToUInt32(maxHealthRegex.Match(line).ToString().Replace("UNIT_FIELD_MAXHEALTH: ", ""));

                return 0;
            }

            public static Position GetSpawnPositionFromLine(string xyzLine, string oriLine)
            {
                Position spawnPosition = new Position();

                Regex xyzRegex = new Regex(@"Position:\s{1}X:{1}\s{1}");
                if (xyzRegex.IsMatch(xyzLine))
                {
                    string[] splittedLine = xyzLine.Split(' ');

                    spawnPosition.x = float.Parse(splittedLine[3], CultureInfo.InvariantCulture.NumberFormat);
                    spawnPosition.y = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                    spawnPosition.z = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                }

                Regex oriRegex = new Regex(@"Orientation:\s{1}");
                if (oriRegex.IsMatch(oriLine))
                {
                    string[] splittedLine = oriLine.Split(' ');

                    spawnPosition.orientation = float.Parse(splittedLine[2], CultureInfo.InvariantCulture.NumberFormat);
                }

                return spawnPosition;
            }

            public static uint GetMapIdFromLine(string line)
            {
                string map = "";

                Regex mapRegex = new Regex(@"Map:{1}.+Entry:{1}");
                if (mapRegex.IsMatch(line.ToString()))
                    map = mapRegex.Match(line.ToString()).ToString().Replace("Map: ", "").Replace(" Entry:", "");

                foreach (var row in DBC.Map)
                {
                    if (map == row.Value.MapName)
                        return (uint)row.Key;
                }

                return 0;
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
                Regex emoteRegex = new Regex(@"UNIT_NPC_EMOTESTATE:{1}\s{1}\w + ");
                if (emoteRegex.IsMatch(line.ToString()))
                    return Convert.ToUInt32(emoteRegex.Match(line.ToString()).ToString().Replace("UNIT_NPC_EMOTESTATE: ", ""));

                return null;
            }

            public static uint? GetSheatheStateFromLine(string line)
            {
                Regex sheatheStateRegex = new Regex(@"SheatheState:{1}\s{1}\w + ");
                if (sheatheStateRegex.IsMatch(line.ToString()))
                    return Convert.ToUInt32(sheatheStateRegex.Match(line.ToString()).ToString().Replace("SheatheState: ", ""));

                return null;
            }
        }

        public struct MonsterMovePacket
        {
            public string creatureGuid;
            public float creatureOrientation;
            public TimeSpan packetSendTime;
            public List<Waypoint> waypoints;
            public uint moveTime;
            public Position startPos;

            public MonsterMovePacket(string guid, float orientation, TimeSpan time, List<Waypoint> waypoints, uint moveTime, Position pos)
            { creatureGuid = guid; creatureOrientation = orientation; packetSendTime = time; this.waypoints = waypoints; this.moveTime = moveTime; startPos = pos; }

            public static float GetFaceDirectionFromLine(string line)
            {
                Regex facingRegex = new Regex(@"FaceDirection:{1}\s+\d+\.+\d+");
                if (facingRegex.IsMatch(line))
                    return float.Parse(facingRegex.Match(line).ToString().Replace("FaceDirection: ", ""), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
            }

            public static bool ConsistsOfPoints(string pointLine, string nextLine)
            {
                if (pointLine.Contains("[0] Points: X:") && pointLine.Contains("[1] Points: X:"))
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

            public bool HasOrientation()
            {
                return creatureOrientation != 0.0f;
            }

            public bool HasWaypoints()
            {
                return waypoints.Count != 0;
            }
        }

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
        }

        public struct TimePacket
        {
            public string hours;
            public string minutes;
            public string seconds;
        }

        public struct AIReactionPacket
        {
            public string creatureGuid;
            public uint creatureEntry;
            public TimeSpan packetSendTime;

            public AIReactionPacket(string guid, uint entry, TimeSpan time)
            { creatureGuid = guid; creatureEntry = entry; packetSendTime = time; }
        }

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
                return unitGuid != "" && slot != null && HasAura != null && packetSendTime != TimeSpan.Zero;
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
        }

        public enum PacketTypes : byte
        {
            UNKNOWN_PACKET       = 0,
            SMSG_UPDATE_OBJECT   = 1,
            SMSG_AI_REACTION     = 2,
            SMSG_SPELL_START     = 3,
            SMSG_CHAT            = 4,
            SMSG_ON_MONSTER_MOVE = 5,
            SMSG_ATTACK_STOP     = 6,
            SMSG_AURA_UPDATE     = 7
        }

        public static List<UpdateObjectPacket> ParseObjectUpdatePacket(string[] lines, long index, BuildVersions buildVersion)
        {
            TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
            List<UpdateObjectPacket> updatePacketsList = new List<UpdateObjectPacket>();

            do
            {
                if ((lines[index].Contains("UpdateType: CreateObject1") || lines[index].Contains("UpdateType: CreateObject2")) && LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime, new Position(), 0, new List<Waypoint>(), null, null);

                    do
                    {
                        if (MonsterMovePacket.GetPointPositionFromLine(lines[index]).IsValid())
                        {
                            do
                            {
                                updatePacket.waypoints.Add(new Waypoint(MonsterMovePacket.GetPointPositionFromLine(lines[index]), 0.0f, 0, new Position(), 0, packetSendTime, new TimeSpan(), new List<WaypointScript>()));
                                index++;
                            }
                            while (lines[index].Contains("Points:"));
                        }

                        if (UpdateObjectPacket.GetMapIdFromLine(lines[index]) != 0)
                            updatePacket.mapId = UpdateObjectPacket.GetMapIdFromLine(lines[index]);

                        if (UpdateObjectPacket.GetSpawnPositionFromLine(lines[index], lines[index + 1]).IsValid())
                            updatePacket.spawnPosition = UpdateObjectPacket.GetSpawnPositionFromLine(lines[index], lines[index + 1]);

                        if (UpdateObjectPacket.GetEntryFromLine(lines[index]) != 0)
                            updatePacket.creatureEntry = UpdateObjectPacket.GetEntryFromLine(lines[index]);

                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion, objectFieldGuid: true) != "")
                            updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, objectFieldGuid: true);

                        if (UpdateObjectPacket.GetMaxHealthFromLine(lines[index]) != 0)
                            updatePacket.creatureMaxHealth = UpdateObjectPacket.GetMaxHealthFromLine(lines[index]);

                        index++;
                    }
                    while (UpdateObjectPacket.IsLineValidForObjectParse(lines[index]));

                    if (updatePacket.creatureEntry == 0 || updatePacket.creatureGuid == "")
                        continue;

                    updatePacket.creatureName = MainForm.GetCreatureNameByEntry(updatePacket.creatureEntry);

                    updatePacketsList.Add(updatePacket);

                    --index;
                }
                else if (lines[index].Contains("UpdateType: Values") && LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime, new Position(), 0, new List<Waypoint>(), null, null);

                    do
                    {
                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion) != "")
                            updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion);

                        if (UpdateObjectPacket.GetHealthFromLine(lines[index]) == 0)
                            updatePacket.creatureCurrentHealth = UpdateObjectPacket.GetHealthFromLine(lines[index]);

                        if (UpdateObjectPacket.GetEmoteStateFromLine(lines[index]) != null)
                            updatePacket.emoteStateId = UpdateObjectPacket.GetEmoteStateFromLine(lines[index]);

                        if (UpdateObjectPacket.GetSheatheStateFromLine(lines[index]) != null)
                            updatePacket.sheatheState = UpdateObjectPacket.GetSheatheStateFromLine(lines[index]);

                        index++;
                    }
                    while (UpdateObjectPacket.IsLineValidForObjectParse(lines[index]));

                    updatePacket.creatureName = MainForm.GetCreatureNameByEntry(updatePacket.creatureEntry);

                    if (updatePacket.creatureEntry == 0 || updatePacket.creatureGuid == "")
                        continue;

                    updatePacketsList.Add(updatePacket);

                    --index;
                }

                index++;

            } while (lines[index] != "");

            return updatePacketsList;
        }

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

        public static SpellStartPacket ParseSpellStartPacket(string[] lines, long index, BuildVersions buildVersion)
        {
            SpellStartPacket spellPacket = new SpellStartPacket("", 0, new TimeSpan(), LineGetters.GetTimeSpanFromLine(lines[index]));

            if (SpellStartPacket.IsCreatureSpellCastLine(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true) != "")
                        spellPacket.casterGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, casterGuid: true);

                    if (SpellStartPacket.GetSpellIdFromLine(lines[index]) != 0)
                        spellPacket.spellId = SpellStartPacket.GetSpellIdFromLine(lines[index]);

                    if (SpellStartPacket.GetCastTimeFromLine(lines[index]) != TimeSpan.Zero)
                        spellPacket.spellCastTime = SpellStartPacket.GetCastTimeFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");
            }

            return spellPacket;
        }

        public static ChatPacket ParseChatPacket(string[] lines, long index, BuildVersions buildVersion)
        {
            ChatPacket chatPacket = new ChatPacket("", 0, "", LineGetters.GetTimeSpanFromLine(lines[index]));

            if (ChatPacket.IsCreatureText(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], buildVersion, senderGuid: true) != "")
                        chatPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, senderGuid: true);

                    if (ChatPacket.GetTextFromLine(lines[index]) != "")
                        chatPacket.creatureText = ChatPacket.GetTextFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                chatPacket.creatureEntry = CreatureScriptsCreator.GetCreatureEntryByGuid(chatPacket.creatureGuid);
            }

            return chatPacket;
        }

        public static MonsterMovePacket ParseMovementPacket(string[] lines, long index, BuildVersions buildVersion)
        {
            MonsterMovePacket movePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position());

            if (LineGetters.IsCreatureLine(lines[index + 1]))
            {
                Position lastPosition = new Position();

                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true) != "")
                        movePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true);

                    if (MonsterMovePacket.GetStartPositionFromLine(lines[index]).IsValid())
                        movePacket.startPos = MonsterMovePacket.GetStartPositionFromLine(lines[index]);

                    if (MonsterMovePacket.GetMoveTimeFromLine(lines[index]) != 0)
                        movePacket.moveTime = MonsterMovePacket.GetMoveTimeFromLine(lines[index]);

                    if (MonsterMovePacket.GetFaceDirectionFromLine(lines[index]) != 0.0f)
                        movePacket.creatureOrientation = MonsterMovePacket.GetFaceDirectionFromLine(lines[index]);

                    if (MonsterMovePacket.GetPointPositionFromLine(lines[index]).IsValid())
                    {
                        if (MonsterMovePacket.ConsistsOfPoints(lines[index], lines[index + 1]))
                        {
                            do
                            {
                                if (MonsterMovePacket.GetPointPositionFromLine(lines[index]).IsValid())
                                    movePacket.waypoints.Add(new Waypoint(MonsterMovePacket.GetPointPositionFromLine(lines[index]), 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>()));

                                index++;
                            }
                            while (lines[index] != "");
                        }
                        else
                        {
                            if (MonsterMovePacket.GetPointPositionFromLine(lines[index]).IsValid())
                                lastPosition = MonsterMovePacket.GetPointPositionFromLine(lines[index]);

                            do
                            {
                                if (MonsterMovePacket.GetWayPointPositionFromLine(lines[index]).IsValid())
                                    movePacket.waypoints.Add(new Waypoint(MonsterMovePacket.GetWayPointPositionFromLine(lines[index]), 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>()));

                                index++;
                            }
                            while (lines[index] != "");
                        }

                        if (lastPosition.IsValid())
                        {
                            movePacket.waypoints.Add(new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>()));
                        }

                        break;
                    }

                    index++;
                }
                while (lines[index] != "");
            }

            return movePacket;
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

                    if (AttackStopPacket.GetNowDeadFromLine(lines[index]))
                        attackPacket.nowDead = AttackStopPacket.GetNowDeadFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");
            }

            return attackPacket;
        }

        public static List<AuraUpdatePacket> ParseAuraUpdatePacket(string[] lines, long index, BuildVersions buildVersion)
        {
            TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
            List<AuraUpdatePacket> aurasList = new List<AuraUpdatePacket>();

            do
            {
                if (lines[index].Contains("Slot:"))
                {
                    AuraUpdatePacket auraUpdatePacket = new AuraUpdatePacket("", null, 0, null, packetSendTime);

                    do
                    {
                        if (AuraUpdatePacket.GetAuraSlotFromLine(lines[index]) != null)
                            auraUpdatePacket.slot = AuraUpdatePacket.GetAuraSlotFromLine(lines[index]);

                        if (AuraUpdatePacket.GetHasAuraFromLine(lines[index]) != null)
                            auraUpdatePacket.HasAura = AuraUpdatePacket.GetHasAuraFromLine(lines[index]);

                        if (SpellStartPacket.GetSpellIdFromLine(lines[index]) != 0)
                            auraUpdatePacket.spellId = SpellStartPacket.GetSpellIdFromLine(lines[index]);

                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true) != "")
                            auraUpdatePacket.unitGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true);

                        index++;
                    }
                    while (AuraUpdatePacket.IsLineValidForAuraUpdateParsing(lines[index]));

                    if (!auraUpdatePacket.IsValid())
                        continue;

                    aurasList.Add(auraUpdatePacket);

                    index--;
                }

                index++;
            }
            while (lines[index] != "");

            return aurasList;
        }
    }
}
