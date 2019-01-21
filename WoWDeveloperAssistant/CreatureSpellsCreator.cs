using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoWDeveloperAssistant
{
    public class CreatureSpellsCreator
    {
        private MainForm mainForm;
        private DataTable guidsDataTable = new DataTable();
        private DataTable combatDataTable = new DataTable();
        private DataTable spellsDataTable = new DataTable();
        private DataTable textDataTable = new DataTable();
        private DataTable deathDataTable = new DataTable();
        private Dictionary<string, string> facingDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> stopAttackDictionary = new Dictionary<string, string>();

        public CreatureSpellsCreator(MainForm mainForm)
        {
            this.mainForm = mainForm;

            guidsDataTable.Columns.AddRange(new DataColumn[2] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("CreatureGuid", typeof(string)) });
            guidsDataTable.PrimaryKey = new DataColumn[] { guidsDataTable.Columns["CreatureGuid"] };

            combatDataTable.Columns.AddRange(new DataColumn[3] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("CreatureGuid", typeof(string)), new DataColumn("CombatStartTime", typeof(string)) });

            spellsDataTable.Columns.AddRange(new DataColumn[4] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("CreatureGuid", typeof(string)), new DataColumn("SpellId", typeof(string)), new DataColumn("CastTime", typeof(string)) });

            textDataTable.Columns.AddRange(new DataColumn[2] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("SayTime", typeof(string)) });

            deathDataTable.Columns.AddRange(new DataColumn[2] { new DataColumn("CreatureGuid", typeof(string)), new DataColumn("DeathTime", typeof(string)) });
        }

        struct SpellCastPacket
        {
            public string creature_entry;
            public string creature_guid;
            public string spell_id;
            public string cast_time;
            public string combat_start_time;
        };

        struct ChatPacket
        {
            public string creature_entry;
            public string say_time;
        }

        struct TimePacket
        {
            public string hours;
            public string minutes;
            public string seconds;
        }

        struct UpdatePacket
        {
            public string creature_entry;
            public string creature_guid;
            public string send_time;
        }

        struct MovePacket
        {
            public string creature_guid;
            public string orientation;
            public string send_time;
        }

        struct AttackStopPacket
        {
            public string creature_guid;
            public bool now_dead;
            public string send_time;
        }

        public void FillSpellsGrid()
        {
            DataTable defaultDataTable = spellsDataTable.Clone();
            DataTable antiDoublesDataTable = spellsDataTable.Clone();

            foreach (DataRow row in spellsDataTable.Rows)
            {
                if (row["CreatureGuid"].ToString() == mainForm.listBox_CreatureGuids.SelectedItem.ToString())
                    defaultDataTable.ImportRow(row);
            }

            foreach (DataRow row in defaultDataTable.Rows)
            {
                if (!GridContainsSpell(antiDoublesDataTable, row["SpellId"].ToString()))
                    antiDoublesDataTable.ImportRow(row);
            }

            string creatureEntry = GetCreatureEntryByGuid(mainForm.listBox_CreatureGuids.SelectedItem.ToString());

            mainForm.dataGridView_Spells.Rows.Clear();

            if (mainForm.checkBox_OnlyCombatSpells.Checked)
            {
                foreach (DataRow dataRow in antiDoublesDataTable.Rows)
                {
                    List<uint> startCastTimesList = new List<uint>();
                    List<uint> repeatCastTimesListForIter = new List<uint>();
                    List<uint> repeatCastTimesList = new List<uint>();

                    int spellId = Convert.ToInt32(dataRow["SpellId"].ToString());
                    string spellName = "Unknown";
                    string castTime = dataRow["CastTime"].ToString();
                    int castTimeInSec = GetCastStartTimeInSec(castTime);
                    uint castsCount = 0;
                    int combatStartTimeSec = GetCreatureCombatStartTime(mainForm.listBox_CreatureGuids.SelectedItem.ToString());
                    if (combatStartTimeSec == 0 || castTimeInSec - combatStartTimeSec < 0)
                        continue;

                    // Get min-max cast time for spell
                    foreach (string guid in mainForm.listBox_CreatureGuids.Items)
                    {
                        foreach (DataRow row in spellsDataTable.Rows)
                        {
                            if (row.Field<string>(0) == creatureEntry && row["CreatureGuid"].ToString() == guid && row["SpellId"].ToString() == spellId.ToString())
                            {
                                int combatStartTime = GetCreatureCombatStartTime(guid);
                                int castStartTimeSec = GetCastStartTimeInSec(row["CastTime"].ToString());

                                if (castStartTimeSec - combatStartTime >= 0)
                                {
                                    startCastTimesList.Add(Convert.ToUInt32((castStartTimeSec - combatStartTime) * 1000));
                                    break;
                                }
                            }
                        }
                    }

                    // Get min-max repeat cast time for spell
                    foreach (DataRow row in defaultDataTable.Rows)
                    {
                        if (row["SpellId"].ToString() == spellId.ToString())
                        {
                            int castStartTimeSec = GetCastStartTimeInSec(row["CastTime"].ToString());

                            if (castStartTimeSec - combatStartTimeSec >= 0)
                            {
                                repeatCastTimesListForIter.Add(Convert.ToUInt32(castStartTimeSec * 1000));
                            }
                        }
                    }

                    for (int i = repeatCastTimesListForIter.Count - 1; i >= 0; i--)
                    {
                        if (i - 1 >= 0)
                        {
                            repeatCastTimesList.Add(repeatCastTimesListForIter[i] - repeatCastTimesListForIter[i - 1]);
                        }
                    }

                    // Get cast count
                    foreach (DataRow row in defaultDataTable.Rows)
                    {
                        int castStartTimeSec = GetCastStartTimeInSec(row["CastTime"].ToString());

                        if (row["SpellId"].ToString() == spellId.ToString() && castStartTimeSec - combatStartTimeSec >= 0)
                            castsCount++;
                    }

                    if (DBC.SpellName.ContainsKey(spellId))
                    {
                        spellName = DBC.SpellName[spellId].Name;
                    }

                    mainForm.dataGridView_Spells.Rows.Add(spellId, spellName, castTime, startCastTimesList.Count != 0 ? startCastTimesList.Min() : 0, startCastTimesList.Count != 0 ? startCastTimesList.Max() : 0, repeatCastTimesList.Count != 0 ? repeatCastTimesList.Min() : 0, repeatCastTimesList.Count != 0 ? repeatCastTimesList.Max() : 0, castsCount);
                }
            }
            else
            {
                foreach (DataRow dataRow in antiDoublesDataTable.Rows)
                {
                    int spellId = Convert.ToInt32(dataRow[2]);
                    string spellName = "Unknown";
                    string castTime = dataRow[3].ToString();
                    uint castsCount = 0;

                    if (DBC.SpellName.ContainsKey(spellId))
                    {
                        spellName = DBC.SpellName[spellId].Name;
                    }

                    foreach (DataRow row in defaultDataTable.Rows)
                    {
                        if (row["SpellId"].ToString() == spellId.ToString())
                            castsCount++;
                    }

                    mainForm.dataGridView_Spells.Rows.Add(spellId, spellName, castTime, 0, 0, 0, 0, castsCount);
                }
            }

            mainForm.dataGridView_Spells.Enabled = true;
        }

        public void FillListBoxWithGuids()
        {
            mainForm.listBox_CreatureGuids.Items.Clear();
            mainForm.dataGridView_Spells.Rows.Clear();

            foreach (DataRow dataRow in guidsDataTable.Rows)
            {
                if (mainForm.checkBox_OnlyCombatSpells.Checked && !IsCreatureHasCombatSpells(dataRow["CreatureGuid"].ToString()))
                    continue;

                if (!IsCreatureHasAnySpell(dataRow["CreatureGuid"].ToString()))
                    continue;

                if (mainForm.toolStripTextBox_CreatureEntry.Text != "" && mainForm.toolStripTextBox_CreatureEntry.Text != "0")
                {
                    if (mainForm.toolStripTextBox_CreatureEntry.Text == dataRow["CreatureEntry"].ToString())
                    {
                        mainForm.listBox_CreatureGuids.Items.Add(dataRow["CreatureGuid"].ToString());
                    }
                }
                else
                {
                    mainForm.listBox_CreatureGuids.Items.Add(dataRow["CreatureGuid"].ToString());
                }
            }

            mainForm.listBox_CreatureGuids.Refresh();
            mainForm.listBox_CreatureGuids.Enabled = true;
        }

        public void LoadSniffFile(string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                guidsDataTable.Clear();
                combatDataTable.Clear();
                spellsDataTable.Clear();
                textDataTable.Clear();
                deathDataTable.Clear();
                facingDictionary.Clear();
                stopAttackDictionary.Clear();

                GetDataFromSniffFile(fileName);
                mainForm.importSuccessful = true;
            }
            else
            {
                MessageBox.Show(fileName + " is is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetDataFromSniffFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_UPDATE_OBJECT"))
                {
                    UpdatePacket packet;
                    packet.creature_entry = "";
                    packet.creature_guid = "";

                    do
                    {
                        i++;

                        if (lines[i].Contains("Object Guid: Full:"))
                        {
                            if (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0"))
                            {
                                packet.creature_entry = GetCreatureEntryFromLine(lines[i]); ;
                                if (packet.creature_entry == "")
                                {
                                    int itr = i;
                                    bool breakCycle = false;

                                    do
                                    {
                                        if (lines[itr].Contains("OBJECT_FIELD_ENTRY"))
                                        {
                                            packet.creature_entry = GetCreatureEntryFromLine(lines[itr], true);
                                            breakCycle = true;
                                        }

                                        itr++;
                                    }
                                    while (!breakCycle);
                                }

                                packet.creature_guid = GetGuidFromLine(lines[i]);
                            }
                        }
                    } while (lines[i] != "");

                    if (packet.creature_entry == "" || packet.creature_guid == "")
                        continue;

                    DataRow dataRow = guidsDataTable.NewRow();
                    dataRow[0] = packet.creature_entry;
                    dataRow[1] = packet.creature_guid;

                    if (!guidsDataTable.Rows.Contains(packet.creature_guid))
                        guidsDataTable.Rows.Add(dataRow);
                }
            }

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_AI_REACTION"))
                {
                    SpellCastPacket packet;
                    packet.creature_entry = "";
                    packet.creature_guid = "";
                    packet.combat_start_time = "";

                    string[] values = lines[i].Split(new char[] { ' ' });
                    string[] time = values[9].Split(new char[] { '.' });
                    packet.combat_start_time = time[0];

                    do
                    {
                        i++;

                        if (lines[i].Contains("UnitGUID: Full:"))
                        {
                            packet.creature_guid = GetGuidFromLine(lines[i]);
                            packet.creature_entry = GetCreatureEntryByGuid(packet.creature_guid);
                        }

                    } while (lines[i] != "");

                    if (packet.creature_entry == "" || packet.creature_guid == "")
                        continue;

                    DataRow dataRow = combatDataTable.NewRow();
                    dataRow[0] = packet.creature_entry;
                    dataRow[1] = packet.creature_guid;
                    dataRow[2] = packet.combat_start_time;
                    combatDataTable.Rows.Add(dataRow);
                }

                if (lines[i].Contains("SMSG_SPELL_START"))
                {
                    SpellCastPacket packet;
                    packet.creature_entry = "";
                    packet.creature_guid = "";
                    packet.spell_id = "";
                    packet.cast_time = "";

                    string[] values = lines[i].Split(new char[] { ' ' });
                    string[] time = values[9].Split(new char[] { '.' });
                    packet.cast_time = time[0];

                    do
                    {
                        i++;

                        if (lines[i].Contains("CasterGUID: Full:"))
                        {
                            if (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0"))
                            {
                                packet.creature_guid = GetGuidFromLine(lines[i]);
                                packet.creature_entry = GetCreatureEntryByGuid(packet.creature_guid);
                            }
                        }

                        if (lines[i].Contains("SpellID:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            packet.spell_id = packetline[2];
                        }

                    } while (lines[i] != "");

                    if (packet.creature_entry == "" || packet.creature_guid == "")
                        continue;

                    DataRow dataRow = spellsDataTable.NewRow();
                    dataRow[0] = packet.creature_entry;
                    dataRow[1] = packet.creature_guid;
                    dataRow[2] = packet.spell_id;
                    dataRow[3] = packet.cast_time;
                    spellsDataTable.Rows.Add(dataRow);
                }

                if (lines[i].Contains("SMSG_CHAT"))
                {
                    ChatPacket packet;
                    packet.creature_entry = "";
                    packet.say_time = GetPacketTimeFromStringInSeconds(lines[i]);

                    bool IsMonsterSay = false;

                    do
                    {
                        i++;

                        if (lines[i].Contains("SlashCmd: 12 (MonsterSay)"))
                        {
                            IsMonsterSay = true;
                        }

                        if (lines[i].Contains("SenderGUID: Full:"))
                        {
                            packet.creature_entry = GetCreatureEntryByGuid(GetGuidFromLine(lines[i]));
                        }

                    } while (lines[i] != "");

                    if (packet.creature_entry != "" && packet.say_time != "" && IsMonsterSay)
                    {
                        DataRow dataRow = textDataTable.NewRow();
                        dataRow[0] = packet.creature_entry;
                        dataRow[1] = packet.say_time;
                        textDataTable.Rows.Add(dataRow);
                    }
                }

                if (lines[i].Contains("SMSG_UPDATE_OBJECT"))
                {
                    UpdatePacket packet;
                    packet.creature_guid = "";
                    packet.send_time = GetPacketTimeFromStringInSeconds(lines[i]);

                    bool IsDead = false;

                    do
                    {
                        i++;

                        if (lines[i].Contains("Object Guid: Full:") &&
                            (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0")))
                        {
                            packet.creature_guid = GetGuidFromLine(lines[i]);
                        }

                        if (lines[i].Contains("UNIT_FIELD_HEALTH"))
                        {
                            string creatureHealth = GetHealthFromLine(lines[i]);

                            if (creatureHealth != "" && creatureHealth == "0")
                            {
                                IsDead = true;
                            }
                        }

                        if (packet.creature_guid != "" && packet.send_time != "" && IsDead)
                        {
                            DataRow dataRow = deathDataTable.NewRow();
                            dataRow[0] = packet.creature_guid;
                            dataRow[1] = packet.send_time;
                            deathDataTable.Rows.Add(dataRow);

                            packet.creature_guid = "";
                            IsDead = false;
                        }

                        if (lines[i].Contains("UpdateType: Values"))
                        {
                            packet.creature_guid = "";
                            IsDead = false;
                        }

                    } while (lines[i] != "");
                }

                if (lines[i].Contains("SMSG_ON_MONSTER_MOVE"))
                {
                    MovePacket packet;
                    packet.creature_guid = "";
                    packet.orientation = "";
                    packet.send_time = GetPacketTimeFromStringInSeconds(lines[i]);

                    do
                    {
                        i++;

                        if (lines[i].Contains("MoverGUID: Full:") &&
                            (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0")))
                        {
                            packet.creature_guid = GetGuidFromLine(lines[i]);
                        }

                        if (lines[i].Contains("FaceDirection:"))
                        {
                            packet.orientation = GetFaceDirectionFromLine(lines[i]);
                        }

                    } while (lines[i] != "");

                    if (packet.creature_guid != "" && packet.orientation != "" && packet.send_time != "")
                    {
                        if (!facingDictionary.ContainsKey(packet.creature_guid))
                        {
                            facingDictionary.Add(packet.creature_guid, packet.send_time);
                        }
                    }
                }

                if (lines[i].Contains("SMSG_ATTACK_STOP"))
                {
                    AttackStopPacket packet;
                    packet.creature_guid = "";
                    packet.now_dead = true;
                    packet.send_time = GetPacketTimeFromStringInSeconds(lines[i]);

                    do
                    {
                        i++;

                        if (lines[i].Contains("Attacker Guid: Full:") &&
                            (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0")))
                        {
                            packet.creature_guid = GetGuidFromLine(lines[i]);
                        }

                        if (lines[i].Contains("NowDead:"))
                        {
                            string[] splittedLine = lines[i].Split(':');

                            if (splittedLine[1] == " False")
                            {
                                packet.now_dead = false;
                            }
                        }

                    } while (lines[i] != "");

                    if (packet.creature_guid != "" && packet.now_dead == false && packet.send_time != "")
                    {
                        if (!stopAttackDictionary.ContainsKey(packet.creature_guid))
                        {
                            stopAttackDictionary.Add(packet.creature_guid, packet.send_time);
                        }
                    }
                }
            }
        }

        public void FillSQLOutput()
        {
            string SQLtext = "";
            string creatureGuid = mainForm.listBox_CreatureGuids.SelectedItem.ToString();
            string creatureEntry = GetCreatureEntryByGuid(mainForm.listBox_CreatureGuids.SelectedItem.ToString());
            int scriptsCount = mainForm.dataGridView_Spells.RowCount + IsCreatureHasAggroText(creatureGuid) + IsCreatureHasDeathText(creatureGuid);
            string creatureName = "Unknown";

            if (Properties.Settings.Default.UsingDB == true)
            {
                DataSet creatureNameDs = new DataSet();
                string creatureNameQuery = "SELECT `name1` FROM `creature_template_wdb` WHERE `entry` = " + creatureEntry + ";";
                creatureNameDs = (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery);

                if (creatureNameDs != null && creatureNameDs.Tables["table"].Rows.Count > 0)
                    creatureName = creatureNameDs.Tables["table"].Rows[0][0].ToString();
            }

            SQLtext = "UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = " + creatureEntry + ";\r\n";
            SQLtext = SQLtext + "DELETE FROM `smart_scripts` WHERE `entryorguid` = " + creatureEntry + ";\r\n";
            SQLtext = SQLtext + "INSERT INTO `smart_scripts` (`entryorguid`, `source_type`, `id`, `link`, `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_spawnMask`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_x`, `target_y`, `target_z`, `target_o`, `comment`) VALUES\r\n";

            for (var l = 0; l < scriptsCount; l++)
            {
                if (IsCreatureHasAggroText(creatureGuid) == 1 && l == 0)
                {
                    SQLtext = SQLtext + "(" + creatureEntry + ", 0, " + l + ", 0, 4, 0, 50, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creatureName + " - On aggro - Say line 0'),\r\n";
                    continue;
                }

                if (IsCreatureHasDeathText(creatureGuid) == 1 && l == (scriptsCount - 1))
                {
                    SQLtext = SQLtext + "(" + creatureEntry + ", 0, " + l + ", 0, 6, 0, 50, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creatureName + " - On death - Say line 1');\r\n";
                    continue;
                }

                string spellName = Convert.ToString(mainForm.dataGridView_Spells[1, l - IsCreatureHasAggroText(creatureGuid)].Value);
                int spellId = Convert.ToInt32(mainForm.dataGridView_Spells[0, l - IsCreatureHasAggroText(creatureGuid)].Value);
                string targetType = "";

                List<uint> effectIds = new List<uint>();

                foreach (var effectId in DBC.SpellEffect)
                {
                    if (effectId.Value.SpellID == spellId)
                        effectIds.Add((uint)effectId.Key);
                }

                if (effectIds.Count != 0)
                {
                    short targeType = 0;

                    foreach (var effectId in effectIds)
                    {
                        targeType = DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[0] > 0 ? DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[0] : DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[1];
                    }

                    if (IsSelfTargetType((uint)targeType))
                        targetType = "1";
                    else if (IsNonSelfTargetType((uint)targeType))
                        targetType = "2";
                    else
                        targetType = "99";
                }
                else
                {
                    targetType = "99";
                }

                SQLtext = SQLtext + "(" + creatureEntry + ", 0, " + l + ", 0, 0, 0, 100, 0, 0, " + Convert.ToString(mainForm.dataGridView_Spells[3, l - IsCreatureHasAggroText(creatureGuid)].Value) + ", " + Convert.ToString(mainForm.dataGridView_Spells[4, l - IsCreatureHasAggroText(creatureGuid)].Value) + ", " + Convert.ToString(mainForm.dataGridView_Spells[5, l - IsCreatureHasAggroText(creatureGuid)].Value) + ", " + Convert.ToString(mainForm.dataGridView_Spells[6, l - IsCreatureHasAggroText(creatureGuid)].Value) + ", 11, " + Convert.ToString(spellId) + ", 0, " + IsSetFacingNeededForSpell(spellId, creatureGuid) + ", 0, 0, 0, " + targetType + ", 0, 0, 0, 0, 0, 0, 0, '" + creatureName + " - IC - Cast " + spellName + "')";

                if (l < (scriptsCount - 1))
                {
                    SQLtext = SQLtext + ",\r\n";
                }
                else
                {
                    SQLtext = SQLtext + ";\r\n";
                }
            }

            mainForm.textBox_SQLOutput.Text = SQLtext;
        }

        private bool IsSelfTargetType(uint Target)
        {
            List<uint> selfTargetTypesList = new List<uint>()
            {
                1, 2, 7, 8, 15, 16, 18, 22, 24, 38,
                41, 42, 43, 44, 46, 47, 48, 49, 50,
                54, 64, 65, 66, 67, 68, 69, 70, 71,
                72, 73, 74, 75, 78, 79, 80, 81, 82,
                83, 84, 85, 86, 104
            };

            return (selfTargetTypesList.Contains(Target));
        }

        private bool IsNonSelfTargetType(uint Target)
        {
            List<uint> nonSelfTargetTypesList = new List<uint>()
            {
                6, 25, 53, 63, 87
            };

            return (nonSelfTargetTypesList.Contains(Target));
        }

        private string GetCreatureEntryFromLine(string line, bool FromObjectField = false)
        {
            if (FromObjectField)
            {
                Regex entryRegexField = new Regex(@"OBJECT_FIELD_ENTRY:{1}\s*\d+");
                if (entryRegexField.IsMatch(line.ToString()))
                    return entryRegexField.Match(line.ToString()).ToString().Replace("OBJECT_FIELD_ENTRY: ", "");
                else
                    return "";
            }

            Regex entryRegex = new Regex(@"Entry:{1}\s*\d+");
            if (entryRegex.IsMatch(line.ToString()))
                return entryRegex.Match(line.ToString()).ToString().Replace("Entry: ", "");

            return "";
        }

        private string GetGuidFromLine(string line)
        {
            Regex guidRegex = new Regex(@"Full:{1}\s*\w+");
            if (guidRegex.IsMatch(line.ToString()))
                return guidRegex.Match(line.ToString()).ToString().Replace("Full: ", "");

            return "";
        }

        private bool IsCreatureHasCombatSpells(string creatureGuid)
        {
            foreach (DataRow row in spellsDataTable.Rows)
            {
                if (row["CreatureGuid"].ToString() == creatureGuid)
                {
                    int castTimeInSec = GetCastStartTimeInSec(row["CastTime"].ToString());
                    int combatStartTimeSec = GetCreatureCombatStartTime(creatureGuid);

                    if (combatStartTimeSec != 0 && castTimeInSec - combatStartTimeSec > 0)
                        return true;
                }
            }

            return false;
        }

        private bool IsCreatureHasAnySpell(string creatureGuid)
        {
            foreach (DataRow row in spellsDataTable.Rows)
            {
                if (row["CreatureGuid"].ToString() == creatureGuid)
                    return true;
            }

            return false;
        }

        private static int GetCastStartTimeInSec(string castTime)
        {
            string[] castStartTimeStr = castTime.Split(new char[] { ':' });
            return (Convert.ToInt32(castStartTimeStr[0]) * 3600) + (Convert.ToInt32(castStartTimeStr[1]) * 60) + Convert.ToInt32(castStartTimeStr[2]);
        }

        private int GetCreatureCombatStartTime(string creatureGuid, bool firstMatch = false)
        {
            int combatStartTime = 0;
            List<string> combatList = new List<string>();

            foreach (DataRow dataRow in combatDataTable.Rows)
            {
                if (dataRow["CreatureGuid"].ToString() == creatureGuid)
                {
                    combatList.Add(dataRow["CombatStartTime"].ToString());
                }
            }

            if (combatList.Count != 0 && firstMatch)
            {
                string[] splittedLine = combatList[0].Split(new char[] { ':' });
                combatStartTime = (Convert.ToInt32(splittedLine[0]) * 3600) + (Convert.ToInt32(splittedLine[1]) * 60 + Convert.ToInt32(splittedLine[2]));
                return combatStartTime;
            }

            if (combatList.Count > 1 || combatList.Count == 0)
            {
                return combatStartTime;
            }

            string[] combatStartTimeString = combatList[0].Split(new char[] { ':' });
            combatStartTime = (Convert.ToInt32(combatStartTimeString[0]) * 3600) + (Convert.ToInt32(combatStartTimeString[1]) * 60) + Convert.ToInt32(combatStartTimeString[2]);
            return combatStartTime;
        }

        private static bool GridContainsSpell(DataTable dataTable, string spell)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["SpellId"].ToString() == spell)
                    return true;
            }

            return false;
        }

        private string GetCreatureEntryByGuid(string creatureGuid)
        {
            foreach (DataRow dataRow in guidsDataTable.Rows)
            {
                if (dataRow[1].ToString() == creatureGuid)
                {
                    return dataRow[0].ToString();
                }
            }

            return "";
        }

        private string GetPacketTimeFromStringInSeconds(string line)
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

        private int IsCreatureHasAggroText(string creatureEntry)
        {
            foreach (DataRow textRow in textDataTable.Rows)
            {
                foreach (DataRow combatRow in combatDataTable.Rows)
                {
                    int combatStartTime = GetCreatureCombatStartTime(combatRow["CreatureGuid"].ToString(), true);

                    if (combatStartTime == Convert.ToInt32(textRow["SayTime"].ToString()))
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }

        private int IsCreatureHasDeathText(string creatureEntry)
        {
            foreach (DataRow textRow in textDataTable.Rows)
            {
                foreach (DataRow deathRow in deathDataTable.Rows)
                {
                    int combatStartTime = GetCreatureCombatStartTime(deathRow["CreatureGuid"].ToString(), true);

                    if (combatStartTime == Convert.ToInt32(textRow["SayTime"].ToString()))
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }

        private string GetHealthFromLine(string line)
        {
            Regex healthRegex = new Regex(@"UNIT_FIELD_HEALTH:{1}\s+\d+");
            if (healthRegex.IsMatch(line.ToString()))
                return healthRegex.Match(line.ToString()).ToString().Replace("UNIT_FIELD_HEALTH: ", "");

            return "";
        }

        private string GetFaceDirectionFromLine(string line)
        {
            Regex facingRegex = new Regex(@"FaceDirection:{1}\s+\d+\.+\d+");
            if (facingRegex.IsMatch(line.ToString()))
                return facingRegex.Match(line.ToString()).ToString().Replace("FaceDirection: ", "");

            return "";
        }

        private string IsSetFacingNeededForSpell(int spellId, string creatureGuid)
        {
            string castTime = "";

            if (DBC.SpellMisc.ContainsKey(spellId))
            {
                if (DBC.SpellCastTimes.ContainsKey(DBC.SpellMisc[spellId].CastingTimeIndex))
                {
                    castTime = DBC.SpellCastTimes[DBC.SpellMisc[spellId].CastingTimeIndex].Minimum.ToString();
                }
            }

            bool isConeTypeSpell = false;

            for (int i = 0; i < 32; i++)
            {
                var spellEffectTuple = Tuple.Create(spellId, i);

                if (DBC.SpellEffectStores.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DBC.SpellEffectStores[spellEffectTuple];

                    if ((spellEffect.ImplicitTarget[0] == 129 || spellEffect.ImplicitTarget[1] == 129) ||
                        (spellEffect.ImplicitTarget[0] == 130 || spellEffect.ImplicitTarget[1] == 130) ||
                        (spellEffect.ImplicitTarget[0] == 54 || spellEffect.ImplicitTarget[1] == 54) ||
                        (spellEffect.ImplicitTarget[0] == 110 || spellEffect.ImplicitTarget[1] == 110))
                    {
                        isConeTypeSpell = true;
                    }
                }
            }

            if (facingDictionary.ContainsKey(creatureGuid) && stopAttackDictionary.ContainsKey(creatureGuid) && castTime != "" && isConeTypeSpell)
                return (Convert.ToUInt32(castTime) + 1000).ToString();

            return "0";
        }
    }
}
