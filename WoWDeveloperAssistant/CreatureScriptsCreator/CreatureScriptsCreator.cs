using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant
{
    public class CreatureScriptsCreator
    {
        private MainForm mainForm;
        public static Dictionary<string, Creature> creaturesDict = new Dictionary<string, Creature>();
        private static Dictionary<uint, string> creatureNamesDict = new Dictionary<uint, string>();
        public static Dictionary<uint, List<CreatureText>> creatureTextsDict = new Dictionary<uint, List<CreatureText>>();
        public static BuildVersions buildVersion = BuildVersions.BUILD_UNKNOWN;

        public CreatureScriptsCreator(MainForm mainForm)
        {
            this.mainForm = mainForm;

            if (Properties.Settings.Default.UsingDB == true)
            {
                creatureNamesDict = GetCreatureNamesFromDB();
            }
        }

        public void FillSpellsGrid()
        {
            if (mainForm.listBox_CreatureGuids.SelectedItem == null)
                return;

            Creature creature = creaturesDict[mainForm.listBox_CreatureGuids.SelectedItem.ToString()];
            List<Spell> spellsList = new List<Spell>(from spell in creature.castedSpells.Values orderby spell.spellStartCastTimes.Count != 0 ? spell.spellStartCastTimes.Min() : new TimeSpan() ascending select spell);

            mainForm.dataGridView_Spells.Rows.Clear();

            if (mainForm.checkBox_OnlyCombatSpells.Checked)
            {
                foreach (Spell spell in spellsList)
                {
                    if (spell.isCombatSpell)
                    {
                        mainForm.dataGridView_Spells.Rows.Add(spell.spellId, spell.name, spell.spellStartCastTimes.Min().ToFormattedString(), spell.combatCastTimings.minCastTime.ToFormattedString(), spell.combatCastTimings.maxCastTime.ToFormattedString(), spell.combatCastTimings.minRepeatTime.ToFormattedString(), spell.combatCastTimings.maxRepeatTime.ToFormattedString(), spell.castTimes, spell);
                    }
                }
            }
            else
            {
                foreach (Spell spell in spellsList)
                {
                    mainForm.dataGridView_Spells.Rows.Add(spell.spellId, spell.name, spell.combatCastTimings.minCastTime.ToFormattedString(), 0, 0, 0, 0, spell.castTimes, spell);
                }
            }

            mainForm.dataGridView_Spells.Enabled = true;
        }

        public void FillListBoxWithGuids()
        {
            mainForm.listBox_CreatureGuids.Items.Clear();
            mainForm.dataGridView_Spells.Rows.Clear();

            foreach (Creature creature in creaturesDict.Values)
            {
                if (mainForm.checkBox_OnlyCombatSpells.Checked && !creature.HasCombatSpells())
                    continue;

                if (creature.castedSpells.Count == 0)
                    continue;

                if (mainForm.toolStripTextBox_CreatureEntry.Text != "" && mainForm.toolStripTextBox_CreatureEntry.Text != "0")
                {
                    if (mainForm.toolStripTextBox_CreatureEntry.Text == creature.entry.ToString())
                    {
                        mainForm.listBox_CreatureGuids.Items.Add(creature.guid);
                    }
                }
                else
                {
                    mainForm.listBox_CreatureGuids.Items.Add(creature.guid);
                }
            }

            mainForm.listBox_CreatureGuids.Refresh();
            mainForm.listBox_CreatureGuids.Enabled = true;
        }

        public void LoadSniffFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                creaturesDict.Clear();
                DBC.Load();
                if (GetDataFromSniffFile(fileName))
                    mainForm.importSuccessful = true;
            }
            else
            {
                MessageBox.Show(fileName + " is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool GetDataFromSniffFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            Dictionary<long, PacketTypes> packetIndexes = new Dictionary<long, PacketTypes>();

            buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == 0)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            Parallel.For(0, lines.Length, index =>
            {
                if (lines[index].Contains("SMSG_UPDATE_OBJECT") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_UPDATE_OBJECT);
                }
                else if (lines[index].Contains("SMSG_AI_REACTION") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_AI_REACTION);
                }
                else if (lines[index].Contains("SMSG_SPELL_START") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_SPELL_START);
                }
                else if (lines[index].Contains("SMSG_CHAT") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_CHAT);
                }
                else if (lines[index].Contains("SMSG_ON_MONSTER_MOVE") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_ON_MONSTER_MOVE);
                }
                else if (lines[index].Contains("SMSG_ATTACK_STOP") &&
                !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, PacketTypes.SMSG_ATTACK_STOP);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    ParseObjectUpdatePacket(lines, value.Key);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == PacketTypes.SMSG_SPELL_START)
                {
                    ParseSpellStartPacket(lines, value.Key);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == PacketTypes.SMSG_AI_REACTION)
                {
                    ParseAIReactionPacket(lines, value.Key);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == PacketTypes.SMSG_CHAT)
                {
                    ParseChatPacket(lines, value.Key);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == PacketTypes.SMSG_ON_MONSTER_MOVE)
                {
                    ParseMovementPacket(lines, value.Key);
                }
                else if (value.Value == PacketTypes.SMSG_ATTACK_STOP)
                {
                    ParseAttackStopkPacket(lines, value.Key);
                }
            });

            Parallel.ForEach(creaturesDict, creature =>
            {
                creature.Value.RemoveNonCombatCastTimes();
            });

            Parallel.ForEach(creaturesDict, creature =>
            {
                creature.Value.CreateCombatCastTimings();
            });

            return true;
        }

        public void FillSQLOutput()
        {
            string SQLtext = "";
            Creature creature = creaturesDict[mainForm.listBox_CreatureGuids.SelectedItem.ToString()];
            int i = 0;

            SQLtext = "UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = " + creature.entry + ";\r\n";
            SQLtext = SQLtext + "DELETE FROM `smart_scripts` WHERE `entryorguid` = " + creature.entry + ";\r\n";
            SQLtext = SQLtext + "INSERT INTO `smart_scripts` (`entryorguid`, `source_type`, `id`, `link`, `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_difficulties`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_x`, `target_y`, `target_z`, `target_o`, `comment`) VALUES\r\n";

            if (IsCreatureHasAggroText(creature.entry))
            {
                SQLtext = SQLtext + "(" + creature.entry + ", 0, " + i + ", 0, 4, 0, 100, 0, '', 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - On aggro - Say line 0'),\r\n";
                i++;
            }

            if (IsCreatureHasDeathText(creature.entry))
            {
                SQLtext = SQLtext + "(" + creature.entry + ", 0, " + i + ", 0, 6, 0, 100, 0, '', 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - On death - Say line 1'),\r\n";
                i++;
            }

            for (int l = 0; l < mainForm.dataGridView_Spells.RowCount; l++, i++)
            {
                Spell spell = (Spell) mainForm.dataGridView_Spells[8, l].Value;

                SQLtext = SQLtext + "(" + creature.entry + ", 0, " + i + ", 0, 0, 0, 100, 0, '', " + spell.combatCastTimings.minCastTime.TotalMilliseconds + ", " + spell.combatCastTimings.maxCastTime.TotalMilliseconds + ", " + spell.combatCastTimings.minRepeatTime.TotalMilliseconds + ", " + spell.combatCastTimings.maxRepeatTime.TotalMilliseconds + ", 11, " + spell.spellId + ", 0, " + (spell.needConeDelay ? spell.spellCastTime.TotalMilliseconds + 1000 : 0) + ", 0, 0, 0, " + spell.GetTargetType() + ", 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - IC - Cast " + spell.name + "')";

                if (l < mainForm.dataGridView_Spells.RowCount - 1)
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

        public static uint GetCreatureEntryByGuid(string creatureGuid)
        {
            if (creaturesDict.ContainsKey(creatureGuid))
                return creaturesDict[creatureGuid].entry;

            return 0;
        }

        private Dictionary<uint, string> GetCreatureNamesFromDB()
        {
            Dictionary<uint, string> namesDict = new Dictionary<uint, string>();

            DataSet creatureNameDs = new DataSet();
            string creatureNameQuery = "SELECT `entry`, `Name1` FROM `creature_template_wdb`;";
            creatureNameDs = (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery);

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    namesDict.Add((uint)row[0], row[1].ToString());
                }
            }

            return namesDict;
        }

        public static string GetCreatureNameByEntry(uint creatureEntry)
        {
            if (creatureNamesDict.ContainsKey(creatureEntry))
                return creatureNamesDict[creatureEntry];

            return "Unknown";
        }

        public static bool IsCreatureHasAggroText(uint creatureEntry)
        {
            if (creatureTextsDict.ContainsKey(creatureEntry))
            {
                foreach (CreatureText text in creatureTextsDict[creatureEntry])
                {
                    if (text.isAggroText)
                        return true;
                }
            }

            return false;
        }

        public static bool IsCreatureHasDeathText(uint creatureEntry)
        {
            if (creatureTextsDict.ContainsKey(creatureEntry))
            {
                foreach (CreatureText text in creatureTextsDict[creatureEntry])
                {
                    if (text.isDeathText)
                        return true;
                }
            }

            return false;
        }
    }
}
