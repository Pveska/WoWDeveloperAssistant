using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WoWDeveloperAssistant
{
    public static class Packets
    {
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
            public string creatureText;
            public TimeSpan packetSendTime;

            public ChatPacket(string guid, string text, TimeSpan time)
            { creatureGuid = guid; creatureText = text; packetSendTime = time; }

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

            public UpdateObjectPacket(uint entry, string guid, string name, int curHealth, uint maxHealth, TimeSpan time)
            { creatureEntry = entry; creatureGuid = guid; creatureName = name; creatureCurrentHealth = curHealth; creatureMaxHealth = maxHealth; packetSendTime = time; }

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
                    return Convert.ToInt32(healthRegex.Match(line).ToString().Replace("UNIT_FIELD_HEALTH: ", ""));

                return -1;
            }

            public static uint GetMaxHealthFromLine(string line)
            {
                Regex maxHealthRegex = new Regex(@"UNIT_FIELD_MAXHEALTH:{1}\s+\d+");
                if (maxHealthRegex.IsMatch(line))
                    return Convert.ToUInt32(maxHealthRegex.Match(line).ToString().Replace("UNIT_FIELD_MAXHEALTH: ", ""));

                return 0;
            }
        }

        public struct MonsterMovePacket
        {
            public string creatureGuid;
            public float creatureOrientation;
            public TimeSpan packetSendTime;

            public MonsterMovePacket(string guid, float orientation, TimeSpan time)
            { creatureGuid = guid; creatureOrientation = orientation; packetSendTime = time; }

            public static float GetFaceDirectionFromLine(string line)
            {
                Regex facingRegex = new Regex(@"FaceDirection:{1}\s+\d+\.+\d+");
                if (facingRegex.IsMatch(line))
                    return float.Parse(facingRegex.Match(line).ToString().Replace("FaceDirection: ", ""), CultureInfo.InvariantCulture.NumberFormat);

                return 0.0f;
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
                Regex noewDeadRegex = new Regex(@"NowDead:{1}\s+\w+");
                if (noewDeadRegex.IsMatch(line))
                    return noewDeadRegex.Match(line).ToString().Replace("NowDead: ", "") == "True";

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
            public TimeSpan packetSendTime;

            public AIReactionPacket(string guid, TimeSpan time)
            { creatureGuid = guid; packetSendTime = time; }
        }

        public enum PacketTypes : byte
        {
            SMSG_UPDATE_OBJECT   = 1,
            SMSG_AI_REACTION     = 2,
            SMSG_SPELL_START     = 3,
            SMSG_CHAT            = 4,
            SMSG_ON_MONSTER_MOVE = 5,
            SMSG_ATTACK_STOP     = 6
        }

        public static void ParseObjectUpdatePacket(string[] lines, long index)
        {
            TimeSpan packetSendTime = LineGetters.GetTimeSpanFromLine(lines[index]);

            do
            {
                if ((lines[index].Contains("UpdateType: CreateObject1") || lines[index].Contains("UpdateType: CreateObject2")) && LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime);

                    do
                    {
                        if (UpdateObjectPacket.GetEntryFromLine(lines[index]) != 0)
                            updatePacket.creatureEntry = UpdateObjectPacket.GetEntryFromLine(lines[index]);

                        if (LineGetters.GetGuidFromLine(lines[index], true) != "")
                            updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], true);

                        if (UpdateObjectPacket.GetMaxHealthFromLine(lines[index]) != 0)
                            updatePacket.creatureMaxHealth = UpdateObjectPacket.GetMaxHealthFromLine(lines[index]);

                        index++;
                    }
                    while (UpdateObjectPacket.IsLineValidForObjectParse(lines[index]));

                    if (updatePacket.creatureEntry == 0 || updatePacket.creatureGuid == "")
                        continue;

                    updatePacket.creatureName = CreatureScriptsCreator.GetCreatureNameByEntry(updatePacket.creatureEntry);

                    lock (CreatureScriptsCreator.creaturesDict)
                    {
                        if (!CreatureScriptsCreator.creaturesDict.ContainsKey(updatePacket.creatureGuid))
                        {
                            CreatureScriptsCreator.creaturesDict.Add(updatePacket.creatureGuid, new Creature(updatePacket));
                        }
                        else
                        {
                            CreatureScriptsCreator.creaturesDict[updatePacket.creatureGuid].UpdateCreature(updatePacket);
                        }
                    }

                    --index;
                }
                else if (lines[index].Contains("UpdateType: Values") && LineGetters.IsCreatureLine(lines[index + 1]))
                {
                    UpdateObjectPacket updatePacket = new UpdateObjectPacket(0, "", "Unknown", -1, 0, packetSendTime);

                    do
                    {
                        if (LineGetters.GetGuidFromLine(lines[index]) != "")
                            updatePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index]);

                        if (UpdateObjectPacket.GetHealthFromLine(lines[index]) == 0)
                            updatePacket.creatureCurrentHealth = UpdateObjectPacket.GetHealthFromLine(lines[index]);

                        index++;
                    }
                    while (UpdateObjectPacket.IsLineValidForObjectParse(lines[index]));

                    if (updatePacket.creatureGuid == "")
                        continue;

                    lock (CreatureScriptsCreator.creaturesDict)
                    {
                        if (!CreatureScriptsCreator.creaturesDict.ContainsKey(updatePacket.creatureGuid))
                        {
                            CreatureScriptsCreator.creaturesDict.Add(updatePacket.creatureGuid, new Creature(updatePacket));
                        }
                        else
                        {
                            CreatureScriptsCreator.creaturesDict[updatePacket.creatureGuid].UpdateCreature(updatePacket);
                        }
                    }

                    --index;
                }

                index++;

            } while (lines[index] != "");
        }

        public static void ParseAIReactionPacket(string[] lines, long index)
        {
            AIReactionPacket reactionPacket = new AIReactionPacket("", LineGetters.GetTimeSpanFromLine(lines[index]));

            do
            {
                if (LineGetters.GetGuidFromLine(lines[index], false, true) != "")
                {
                    reactionPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], false, true);
                }

                index++;
            }
            while (lines[index] != "");

            if (reactionPacket.creatureGuid == "")
                return;

            lock (CreatureScriptsCreator.creaturesDict)
            {
                if (CreatureScriptsCreator.creaturesDict.ContainsKey(reactionPacket.creatureGuid))
                {
                    if (CreatureScriptsCreator.creaturesDict[reactionPacket.creatureGuid].combatStartTime == TimeSpan.Zero ||
                        CreatureScriptsCreator.creaturesDict[reactionPacket.creatureGuid].combatStartTime < reactionPacket.packetSendTime)
                    {
                        CreatureScriptsCreator.creaturesDict[reactionPacket.creatureGuid].combatStartTime = reactionPacket.packetSendTime;
                    }

                    CreatureScriptsCreator.creaturesDict[reactionPacket.creatureGuid].UpdateCombatSpells(reactionPacket);
                    CreatureScriptsCreator.creaturesDict[reactionPacket.creatureGuid].UpdateTexts(reactionPacket);
                }
            }
        }

        public static void ParseSpellStartPacket(string[] lines, long index)
        {
            SpellStartPacket spellPacket = new SpellStartPacket("", 0, new TimeSpan(), LineGetters.GetTimeSpanFromLine(lines[index]));

            if (SpellStartPacket.IsCreatureSpellCastLine(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], false, false, false, false, false, true) != "")
                        spellPacket.casterGuid = LineGetters.GetGuidFromLine(lines[index], false, false, false, false, false, true);

                    if (SpellStartPacket.GetSpellIdFromLine(lines[index]) != 0)
                        spellPacket.spellId = SpellStartPacket.GetSpellIdFromLine(lines[index]);

                    if (SpellStartPacket.GetCastTimeFromLine(lines[index]) != TimeSpan.Zero)
                        spellPacket.spellCastTime = SpellStartPacket.GetCastTimeFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                if (spellPacket.casterGuid == "")
                    return;

                lock (CreatureScriptsCreator.creaturesDict)
                {
                    if (CreatureScriptsCreator.creaturesDict.ContainsKey(spellPacket.casterGuid))
                    {
                        if (!CreatureScriptsCreator.creaturesDict[spellPacket.casterGuid].castedSpells.ContainsKey(spellPacket.spellId))
                            CreatureScriptsCreator.creaturesDict[spellPacket.casterGuid].castedSpells.Add(spellPacket.spellId, new Spell(spellPacket));
                        else
                            CreatureScriptsCreator.creaturesDict[spellPacket.casterGuid].UpdateSpells(spellPacket);
                    }
                }
            }
        }

        public static void ParseChatPacket(string[] lines, long index)
        {
            ChatPacket chatPacket = new ChatPacket("", "", LineGetters.GetTimeSpanFromLine(lines[index]));

            if (ChatPacket.IsCreatureText(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], false, false, true) != "")
                        chatPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], false, false, true);

                    if (ChatPacket.GetTextFromLine(lines[index]) != "")
                        chatPacket.creatureText = ChatPacket.GetTextFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");


                if (chatPacket.creatureGuid == "")
                    return;

                lock (CreatureScriptsCreator.creaturesDict)
                {
                    if (CreatureScriptsCreator.creaturesDict.ContainsKey(chatPacket.creatureGuid))
                    {
                        CreatureScriptsCreator.creaturesDict[chatPacket.creatureGuid].saidTexts.Add(new CreatureText(chatPacket));
                    }
                }
            }
        }

        public static void ParseMovementPacket(string[] lines, long index)
        {
            MonsterMovePacket movePacket = new MonsterMovePacket("", 0.0f, LineGetters.GetTimeSpanFromLine(lines[index]));

            if (LineGetters.IsCreatureLine(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], false, false, false, true) != "")
                        movePacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], false, false, false, true);

                    if (MonsterMovePacket.GetFaceDirectionFromLine(lines[index]) != 0.0f)
                        movePacket.creatureOrientation = MonsterMovePacket.GetFaceDirectionFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                if (movePacket.creatureGuid == "")
                    return;

                lock (CreatureScriptsCreator.creaturesDict)
                {
                    if (CreatureScriptsCreator.creaturesDict.ContainsKey(movePacket.creatureGuid))
                    {
                        CreatureScriptsCreator.creaturesDict[movePacket.creatureGuid].UpdateSpellsByMovementPacket(movePacket);
                    }
                }
            }
        }

        public static void ParseAttackStopkPacket(string[] lines, long index)
        {
            AttackStopPacket attackPacket = new AttackStopPacket("", false, LineGetters.GetTimeSpanFromLine(lines[index]));

            if (LineGetters.IsCreatureLine(lines[index + 1]))
            {
                do
                {
                    if (LineGetters.GetGuidFromLine(lines[index], false, false, false, false, true) != "")
                        attackPacket.creatureGuid = LineGetters.GetGuidFromLine(lines[index], false, false, false, false, true);

                    if (AttackStopPacket.GetNowDeadFromLine(lines[index]))
                        attackPacket.nowDead = AttackStopPacket.GetNowDeadFromLine(lines[index]);

                    index++;
                }
                while (lines[index] != "");

                if (attackPacket.creatureGuid == "")
                    return;

                lock (CreatureScriptsCreator.creaturesDict)
                {
                    if (CreatureScriptsCreator.creaturesDict.ContainsKey(attackPacket.creatureGuid))
                    {
                        CreatureScriptsCreator.creaturesDict[attackPacket.creatureGuid].UpdateSpellsByAttackStopPacket(attackPacket);

                        if (attackPacket.nowDead && CreatureScriptsCreator.creaturesDict[attackPacket.creatureGuid].deathTime == TimeSpan.Zero)
                        {
                            CreatureScriptsCreator.creaturesDict[attackPacket.creatureGuid].deathTime = attackPacket.packetSendTime;
                        }
                    }
                }
            }
        }
    }
}
