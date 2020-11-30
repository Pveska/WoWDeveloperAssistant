using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Misc
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

            public enum PacketTypes : byte
            {
                UNKNOWN_PACKET       = 0,
                SMSG_UPDATE_OBJECT   = 1,
                SMSG_AI_REACTION     = 2,
                SMSG_SPELL_START     = 3,
                SMSG_CHAT            = 4,
                SMSG_ON_MONSTER_MOVE = 5,
                SMSG_ATTACK_STOP     = 6,
                SMSG_AURA_UPDATE     = 7,
                SMSG_EMOTE           = 8,
                SMSG_SPELL_GO        = 9
            }

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
                else if (line.Contains("SMSG_EMOTE"))
                    packetType = PacketTypes.SMSG_EMOTE;

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
                        if (parsedPacketsList.Cast<UpdateObjectPacket>().Any(updatePacket => updatePacket.creatureGuid == guid))
                        {
                            return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_ON_MONSTER_MOVE:
                    {
                        if (parsedPacketsList.Cast<MonsterMovePacket>().Any(movementPacket => movementPacket.creatureGuid == guid))
                        {
                            return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_SPELL_START:
                    {
                        if (parsedPacketsList.Cast<SpellStartPacket>().Any(spellPacket => spellPacket.casterGuid == guid))
                        {
                            return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_AURA_UPDATE:
                    {
                        if (parsedPacketsList.Cast<AuraUpdatePacket>().Any(auraPacket => auraPacket.unitGuid == guid))
                        {
                            return true;
                        }

                        break;
                    }
                    case PacketTypes.SMSG_EMOTE:
                    {
                        if (parsedPacketsList.Cast<EmotePacket>().Any(emotePacket => emotePacket.guid == guid))
                        {
                            return true;
                        }

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
        }

        public struct SpellStartPacket
        {
            public string casterGuid;
            public uint spellId;
            public TimeSpan spellCastTime;
            public TimeSpan spellCastStartTime;
            public Position spellDestination;

            public SpellStartPacket(string guid, uint id, TimeSpan castTime, TimeSpan startTime, Position spellDest)
            { casterGuid = guid; spellId = id; spellCastTime = castTime; spellCastStartTime = startTime; spellDestination = spellDest; }

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
                int castTime = 0;
                Int32.TryParse(castTimeRegex.Match(line).ToString().Replace("CastTime: ", ""), out castTime);
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

            public static SpellStartPacket ParseSpellStartPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                SpellStartPacket spellPacket = new SpellStartPacket("", 0, new TimeSpan(), LineGetters.GetTimeSpanFromLine(lines[index]), new Position());

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

                return spellPacket;
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
                }

                return chatPacket;
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
            public uint? mapId;
            public List<Waypoint> waypoints;
            public uint? emoteStateId;
            public uint? sheatheState;
            public uint? standState;
            public bool hasDisableGravity;

            public UpdateObjectPacket(uint entry, string guid, string name, int curHealth, uint maxHealth, TimeSpan time, Position spawnPos, uint? mapId, List<Waypoint> waypoints, uint? emote, uint? sheatheState, uint? standState, bool hasDisableGravity)
            { creatureEntry = entry; creatureGuid = guid; creatureName = name; creatureCurrentHealth = curHealth; creatureMaxHealth = maxHealth; packetSendTime = time; spawnPosition = spawnPos; this.mapId = mapId; this.waypoints = waypoints; emoteStateId = emote; this.sheatheState = sheatheState; this.standState = standState; this.hasDisableGravity = hasDisableGravity; }

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
                Regex entryRegexField = new Regex(@"EntryID:{1}\s*\d+");
                if (entryRegexField.IsMatch(line))
                    return Convert.ToUInt32(entryRegexField.Match(line).ToString().Replace("EntryID: ", ""));
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

            public static Position GetSpawnPositionFromLine(string xyzLine, string oriLine)
            {
                Position spawnPosition = new Position();

                if (xyzLine.Contains("TransportPosition"))
                    return spawnPosition;

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

            public static uint? GetMapIdFromLine(string line)
            {
                Regex mapRegex = new Regex(@"MapId:{1}\s{1}\d+");
                if (mapRegex.IsMatch(line))
                    return Convert.ToUInt32(mapRegex.Match(line).ToString().Replace("MapId: ", ""));

                return null;
            }

            public static bool GetDisableGravityFromLine(string line)
            {
                if (line.Contains("MovementFlags:") && line.Contains("MOVEMENTFLAG_DISABLE_GRAVITY"))
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

            public static IEnumerable<UpdateObjectPacket> ParseObjectUpdatePacket(string[] lines, long index, BuildVersions buildVersion)
            {
                TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                List<UpdateObjectPacket> updatePacketsList = new List<UpdateObjectPacket>();

                do
                {
                    if ((lines[index].Contains("UpdateType: CreateObject1") || lines[index].Contains("UpdateType: CreateObject2")) && LineGetters.IsCreatureLine(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false);

                        do
                        {
                            if (MonsterMovePacket.GetPointPositionFromLine(lines[index]).IsValid())
                            {
                                uint pointId = 1;

                                do
                                {
                                    updatePacket.waypoints.Add(new Waypoint(MonsterMovePacket.GetPointPositionFromLine(lines[index]), 0.0f, 0, new Position(), 0, packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId));
                                    pointId++;
                                    index++;
                                }
                                while (lines[index].Contains("Points:"));
                            }

                            if (GetMapIdFromLine(lines[index]) != null)
                                updatePacket.mapId = GetMapIdFromLine(lines[index]);

                            if (GetSpawnPositionFromLine(lines[index], lines[index + 1]).IsValid())
                                updatePacket.spawnPosition = GetSpawnPositionFromLine(lines[index], lines[index + 1]);

                            if (GetEntryFromLine(lines[index]) != 0)
                                updatePacket.creatureEntry = GetEntryFromLine(lines[index]);

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, objectFieldGuid: true) != "")
                                updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, objectFieldGuid: true);

                            if (GetMaxHealthFromLine(lines[index]) != 0)
                                updatePacket.creatureMaxHealth = GetMaxHealthFromLine(lines[index]);

                            if (GetDisableGravityFromLine(lines[index]))
                                updatePacket.hasDisableGravity = true;

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        if (updatePacket.creatureEntry == 0 || updatePacket.creatureGuid == "")
                            continue;

                        updatePacket.creatureName = MainForm.GetCreatureNameByEntry(updatePacket.creatureEntry);

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }
                    else if (lines[index].Contains("UpdateType: Values") && LineGetters.IsCreatureLine(lines[index + 1]))
                    {
                        UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime, new Position(), null, new List<Waypoint>(), null, null, null, false);

                        do
                        {
                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion) != "")
                                updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion);

                            if (GetHealthFromLine(lines[index]) == 0)
                                updatePacket.creatureCurrentHealth = GetHealthFromLine(lines[index]);

                            if (GetEmoteStateFromLine(lines[index]) != null)
                                updatePacket.emoteStateId = GetEmoteStateFromLine(lines[index]);

                            if (GetSheatheStateFromLine(lines[index]) != null)
                                updatePacket.sheatheState = GetSheatheStateFromLine(lines[index]);

                            if (GetStandStateFromLine(lines[index]) != null)
                                updatePacket.standState = GetStandStateFromLine(lines[index]);

                            if (GetDisableGravityFromLine(lines[index]))
                                updatePacket.hasDisableGravity = true;

                            index++;
                        }
                        while (IsLineValidForObjectParse(lines[index]));

                        updatePacket.creatureName = MainForm.GetCreatureNameByEntry(updatePacket.creatureEntry);

                        if (updatePacket.creatureGuid == "")
                            continue;

                        updatePacketsList.Add(updatePacket);

                        --index;
                    }

                    index++;

                } while (lines[index] != "");

                return updatePacketsList;
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
            public JumpInfo jumpInfo;

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

            public static MonsterMovePacket ParseMovementPacket(string[] lines, long index, BuildVersions buildVersion)
            {
                MonsterMovePacket movePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]), new List<Waypoint>(), 0, new Position(), new JumpInfo());

                if (LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    Position lastPosition = new Position();

                    do
                    {
                        if (lines[index].Contains("FacingGUID: TypeName: Player; Full:"))
                        {
                            movePacket.creatureGuid = "";
                            break;
                        }

                        if (LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true) != "")
                            movePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, moverGuid: true);

                        if (GetStartPositionFromLine(lines[index]).IsValid())
                            movePacket.startPos = GetStartPositionFromLine(lines[index]);

                        if (GetMoveTimeFromLine(lines[index]) != 0)
                            movePacket.moveTime = GetMoveTimeFromLine(lines[index]);

                        if (GetFaceDirectionFromLine(lines[index]) != 0.0f)
                            movePacket.creatureOrientation = GetFaceDirectionFromLine(lines[index]);

                        if (GetPointPositionFromLine(lines[index]).IsValid())
                        {
                            if (ConsistsOfPoints(lines[index], lines[index + 1]))
                            {
                                uint pointId = 1;

                                do
                                {
                                    if (GetPointPositionFromLine(lines[index]).IsValid())
                                    {
                                        movePacket.waypoints.Add(new Waypoint(GetPointPositionFromLine(lines[index]), 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId));
                                        pointId++;
                                    }

                                    index++;
                                }
                                while (lines[index] != "");
                            }
                            else
                            {
                                if (GetPointPositionFromLine(lines[index]).IsValid())
                                    lastPosition = GetPointPositionFromLine(lines[index]);

                                uint pointId = 1;

                                do
                                {
                                    if (GetWayPointPositionFromLine(lines[index]).IsValid())
                                    {
                                        movePacket.waypoints.Add(new Waypoint(GetWayPointPositionFromLine(lines[index]), 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), pointId));
                                        pointId++;
                                    }

                                    if (GetJumpGravityFromLine(lines[index]) != 0.0f)
                                    {
                                        movePacket.jumpInfo.jumpGravity = GetJumpGravityFromLine(lines[index]);
                                    }

                                    index++;
                                }
                                while (lines[index] != "");
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
                                    movePacket.waypoints.Add(new Waypoint(lastPosition, 0.0f, 0, movePacket.startPos, movePacket.moveTime, movePacket.packetSendTime, new TimeSpan(), new List<WaypointScript>(), (uint)(movePacket.waypoints.Count + 1)));
                                }
                            }

                            break;
                        }

                        index++;
                    }
                    while (lines[index] != "");
                }

                return movePacket;
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

            public static IEnumerable<AuraUpdatePacket> ParseAuraUpdatePacket(string[] lines, long index, BuildVersions buildVersion)
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
                            if (GetAuraSlotFromLine(lines[index]) != null)
                                auraUpdatePacket.slot = GetAuraSlotFromLine(lines[index]);

                            if (GetHasAuraFromLine(lines[index]) != null)
                                auraUpdatePacket.HasAura = GetHasAuraFromLine(lines[index]);

                            if (SpellStartPacket.GetSpellIdFromLine(lines[index]) != 0)
                                auraUpdatePacket.spellId = SpellStartPacket.GetSpellIdFromLine(lines[index]);

                            if (LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true) != "")
                                auraUpdatePacket.unitGuid = LineGetters.GetGuidFromLine(lines[index], buildVersion, unitGuid: true);

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

                return aurasList;
            }
        }

        public struct EmotePacket
        {
            public string guid;
            public uint emoteId;
            public TimeSpan packetSendTime;

            public EmotePacket(string guid, uint emote, TimeSpan time)
            { this.guid = guid; emoteId = emote; packetSendTime = time; }

            public static string GetGuidFromLine(string line)
            {
                Regex guidRegex = new Regex(@"GUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("GUID: Full: ", "");

                return "";
            }

            public static uint GetEmoteIdFromLine(string line)
            {
                Regex emoteRegex = new Regex(@"Emote ID:{1}\s{1}\d+");
                if (emoteRegex.IsMatch(line))
                    return Convert.ToUInt32(emoteRegex.Match(line).ToString().Replace("Emote ID: ", ""));

                return 0;
            }

            public static EmotePacket ParseEmotePacket(string[] lines, long index, BuildVersions buildVersion)
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
    }
}
