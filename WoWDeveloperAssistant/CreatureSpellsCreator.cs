using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWDeveloperAssistant
{
    public static class CreatureSpellsCreator
    {
        struct Packet
        {
            public string creature_entry;
            public string creature_guid;
            public string spell_id;
            public string cast_time;
            public string combat_start_time;
        };

        private static int GetCastStartTimeInSec(string castTime)
        {
            string[] castStartTimeStr = castTime.Split(new char[] { ':' });
            return (Convert.ToInt32(castStartTimeStr[0]) * 3600) + (Convert.ToInt32(castStartTimeStr[1]) * 60) + Convert.ToInt32(castStartTimeStr[2]);
        }

        private static bool IsGuidValidForCombatParse(DataTable combatDataTable, string creatureGuid)
        {
            List<string> combatList = new List<string>();

            foreach (DataRow dataRow in combatDataTable.Rows)
            {
                if (dataRow[1].ToString() == creatureGuid)
                {
                    combatList.Add(dataRow[2].ToString());
                }
            }

            return combatList.Count == 1;
        }

        private static int GetCreatureCombatStartTime(DataTable combatDataTable, string creatureGuid)
        {
            int combatStartTime = 0;
            List<string> combatList = new List<string>();

            foreach (DataRow dataRow in combatDataTable.Rows)
            {
                if (dataRow[1].ToString() == creatureGuid)
                {
                    combatList.Add(dataRow[2].ToString());
                }
            }

            if (combatList.Count > 1 || combatList.Count == 0)
            {
                return combatStartTime;
            }

            string[] combatStartTimeString = combatList[0].Split(new char[] { ':' });
            combatStartTime = (Convert.ToInt32(combatStartTimeString[0]) * 3600) + (Convert.ToInt32(combatStartTimeString[1]) * 60) + Convert.ToInt32(combatStartTimeString[2]);
            return combatStartTime;
        }

        private static bool GridContainSpell(DataTable Dt, string spell)
        {
            foreach (DataRow row in Dt.Rows)
            {
                if (row.Field<string>(2) == spell)
                    return true;
            }

            return false;
        }

        private static string GetCreatureEntryByGuid(DataTable guidsDataTable, string creatureGuid)
        {
            foreach (DataRow dataRow in guidsDataTable.Rows)
            {
                if (dataRow[0].ToString() == creatureGuid)
                {
                    return dataRow[1].ToString();
                }
            }

            return "";
        }

        public static void FillSpellsGrid(DataTable guidsDataTable, DataTable combatDataTable, DataTable spellsDataTable, ListBox guidsListBox, DataGridView dataGrid, string creatureGuid, bool onlyCombatSpells)
        {
            DataTable defaultDataTable = spellsDataTable.Clone();
            DataTable antiDoublesDataTable = spellsDataTable.Clone();

            foreach (DataRow row in spellsDataTable.Rows)
            {
                if (row.Field<string>(1) == creatureGuid)
                    defaultDataTable.ImportRow(row);
            }

            foreach (DataRow row in defaultDataTable.Rows)
            {
                if (!GridContainSpell(antiDoublesDataTable, row.Field<string>(2)))
                    antiDoublesDataTable.ImportRow(row);
            }

            string creatureEntry = GetCreatureEntryByGuid(guidsDataTable, creatureGuid);

            dataGrid.Rows.Clear();

            if (onlyCombatSpells)
            {
                foreach (DataRow dataRow in antiDoublesDataTable.Rows)
                {
                    List<uint> startCastTimesList = new List<uint>();
                    List<uint> repeatCastTimesListForIter = new List<uint>();
                    List<uint> repeatCastTimesList = new List<uint>();

                    int spellId = Convert.ToInt32(dataRow[2]);
                    string spellName = "Unknown";
                    string castTime = dataRow[3].ToString();
                    int castTimeInSec = GetCastStartTimeInSec(castTime);
                    uint castsCount = 0;
                    int combatStartTimeSec = GetCreatureCombatStartTime(combatDataTable, creatureGuid);
                    if (combatStartTimeSec == 0 || castTimeInSec - combatStartTimeSec < 0)
                        continue;

                    // Get min-max cast time for spell
                    foreach (string guid in guidsListBox.Items)
                    {
                        foreach (DataRow row in spellsDataTable.Rows)
                        {
                            if (row.Field<string>(0) == creatureEntry && row.Field<string>(1) == guid && row.Field<string>(2) == spellId.ToString())
                            {
                                int combatStartTime = GetCreatureCombatStartTime(combatDataTable, guid);
                                int castStartTimeSec = GetCastStartTimeInSec(row.Field<string>(3));

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
                        if (row.Field<string>(2) == spellId.ToString())
                        {
                            int castStartTimeSec = GetCastStartTimeInSec(row.Field<string>(3));

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
                        int castStartTimeSec = GetCastStartTimeInSec(row.Field<string>(3));

                        if (row.Field<string>(2) == spellId.ToString() && castStartTimeSec - combatStartTimeSec >= 0)
                            castsCount++;
                    }

                    if (DBC.Spell.ContainsKey(spellId))
                    {
                        spellName = DBC.Spell[spellId].Name;
                    }

                    dataGrid.Rows.Add(spellId, spellName, castTime, startCastTimesList.Count != 0 ? startCastTimesList.Min() : 0, startCastTimesList.Count != 0 ? startCastTimesList.Max() : 0, repeatCastTimesList.Count != 0 ? repeatCastTimesList.Min() : 0, repeatCastTimesList.Count != 0 ? repeatCastTimesList.Max() : 0, castsCount);
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

                    if (DBC.Spell.ContainsKey(spellId))
                    {
                        spellName = DBC.Spell[spellId].Name;
                    }

                    foreach (DataRow row in defaultDataTable.Rows)
                    {
                        if (row.Field<string>(2) == spellId.ToString())
                            castsCount++;
                    }

                    dataGrid.Rows.Add(spellId, spellName, castTime, 0, 0, 0, 0, castsCount);
                }
            }

            dataGrid.Enabled = true;
        }

        public static void FillListBoxWithGuids(DataTable combatDataTable, DataTable guidsDataTable, ListBox listBox, string creatureEntry, bool onlyCombatSpells)
        {
            List<string> guidList = new List<string>();

            foreach (DataRow dataRow in guidsDataTable.Rows)
            {
                if (creatureEntry != "" && creatureEntry != "0")
                {
                    if (creatureEntry == dataRow["CreatureEntry"].ToString() && onlyCombatSpells &&
                        IsGuidValidForCombatParse(combatDataTable, dataRow["CreatureGuid"].ToString()))
                    {
                        guidList.Add(dataRow["CreatureGuid"].ToString());
                    }
                    else if (creatureEntry == dataRow["CreatureEntry"].ToString() && !onlyCombatSpells)
                    {
                        guidList.Add(dataRow["CreatureGuid"].ToString());
                    }
                }
                else
                {
                    if (onlyCombatSpells && IsGuidValidForCombatParse(combatDataTable, dataRow["CreatureGuid"].ToString()))
                    {
                        guidList.Add(dataRow["CreatureGuid"].ToString());
                    }
                    else if (!onlyCombatSpells)
                    {
                        guidList.Add(dataRow["CreatureGuid"].ToString());
                    }
                }
            }

            listBox.DataSource = guidList;
            listBox.Refresh();
            listBox.Enabled = true;
        }

        public static DataSet LoadSniffFile(string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                return GetDataFromSniffFile(fileName);
            }
            else
            {
                MessageBox.Show(fileName + " is is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return null;
            }
        }

        private static DataSet GetDataFromSniffFile(string fileName)
        {
            DataSet dataSet = new DataSet();
            DataTable combatDataTable = new DataTable();
            DataTable spellsDataTable = new DataTable();
            combatDataTable.Columns.AddRange(new DataColumn[3] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("CreatureGuid", typeof(string)), new DataColumn("CombatStartTime", typeof(string)) });
            spellsDataTable.Columns.AddRange(new DataColumn[4] { new DataColumn("CreatureEntry", typeof(string)), new DataColumn("CreatureGuid", typeof(string)), new DataColumn("SpellId", typeof(string)), new DataColumn("CastTime", typeof(string)) });

            var lines = File.ReadAllLines(fileName);

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_AI_REACTION"))
                {
                    Packet packet;
                    packet.creature_entry = "";
                    packet.creature_guid = "";
                    packet.spell_id = "";
                    packet.cast_time = "";
                    packet.combat_start_time = "";

                    string[] values = lines[i].Split(new char[] { ' ' });
                    string[] time = values[9].Split(new char[] { '.' });
                    packet.combat_start_time = time[0];

                    do
                    {
                        i++;

                        if (lines[i].Contains("UnitGUID: Full:"))
                        {
                            if (lines[i].Contains("Creature/0"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                packet.creature_entry = packetline[8];
                                packet.creature_guid = packetline[2];
                            }
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
                    Packet packet;
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
                            if (lines[i].Contains("Creature/0"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                packet.creature_entry = packetline[9];
                                packet.creature_guid = packetline[3];
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
            }

            dataSet.Tables.Add(combatDataTable);
            dataSet.Tables.Add(spellsDataTable);
            return dataSet;
        }

        public static void FillSQLOutput(DataTable guidsDataTable, DataGridView dataGrid, TextBox textBox, string creatureGuid)
        {
            string SQLtext = "";
            string creatureName = "";
            string creatureEntry = GetCreatureEntryByGuid(guidsDataTable, creatureGuid);
            DataSet creatureNameDs = new DataSet();
            string creatureNameQuery = "SELECT `name1` FROM `creature_template_wdb` WHERE `entry` = " + creatureEntry + ";";
            creatureNameDs = (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery);

            if (creatureNameDs != null && creatureNameDs.Tables["table1"].Rows.Count > 0)
                creatureName = creatureNameDs.Tables["table1"].Rows[0][0].ToString();

            SQLtext = "UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = " + creatureEntry + ";\r\n";
            SQLtext = SQLtext + "DELETE FROM `smart_scripts` WHERE `entryorguid` = " + creatureEntry + ";\r\n";
            SQLtext = SQLtext + "INSERT INTO `smart_scripts` (`entryorguid`, `source_type`, `id`, `link`, `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_spawnMask`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_x`, `target_y`, `target_z`, `target_o`, `comment`) VALUES\r\n";

            for (var l = 0; l < dataGrid.RowCount; l++)
            {
                string spellName = Convert.ToString(dataGrid[1, l].Value);
                int spellId = Convert.ToInt32(dataGrid[0, l].Value);
                string targetType = "";

                List<uint> effectIds = new List<uint>();

                foreach (var effectId in DBC.SpellEffect.Values)
                {
                    if (effectId.SpellID == spellId)
                        effectIds.Add(effectId.ID);
                }

                if (effectIds.Count != 0)
                {
                    uint targeType = 0;

                    foreach (var effectId in effectIds)
                    {
                        targeType = DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[0] > 0 ? DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[0] : DBC.SpellEffect[Convert.ToInt32(effectId)].ImplicitTarget[1];
                    }

                    if (IsSelfTargetType(targeType))
                        targetType = "1";
                    else if (IsNonSelfTargetType(targeType))
                        targetType = "2";
                    else
                        targetType = "99";
                }
                else
                {
                    targetType = "99";
                }

                SQLtext = SQLtext + "(" + creatureEntry + ", 0, " + l + ", 0, 0, 0, 100, 0, 0, " + Convert.ToString(dataGrid[4, l].Value) + ", " + Convert.ToString(dataGrid[5, l].Value) + ", " + Convert.ToString(dataGrid[6, l].Value) + ", " + Convert.ToString(dataGrid[7, l].Value) + ", 11, " + Convert.ToString(spellId) + ", 0, 0, 0, 0, 0, " + targetType + ", 0, 0, 0, 0, 0, 0, 0, '" + creatureName + " - IC - Cast " + spellName + "')";

                if (l < (dataGrid.RowCount - 1))
                {
                    SQLtext = SQLtext + ",\r\n";
                }
                else
                {
                    SQLtext = SQLtext + ";\r\n";
                }
            }

            textBox.Text = SQLtext;
        }

        private static bool IsSelfTargetType(uint Target)
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

        private static bool IsNonSelfTargetType(uint Target)
        {
            List<uint> nonSelfTargetTypesList = new List<uint>()
            {
                6, 25, 53, 63, 87
            };

            return (nonSelfTargetTypesList.Contains(Target));
        }
    }
}
