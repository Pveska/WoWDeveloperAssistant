using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using WoWDeveloperAssistant.Core_Script_Templates;
using System.Text.RegularExpressions;

namespace WoWDeveloperAssistant.Creature_Scripts_Creator
{
    public class CreatureScriptsCreator
    {
        private readonly MainForm mainForm;
        public static Dictionary<string, Creature> creaturesDict = new Dictionary<string, Creature>();
        public static Dictionary<uint, List<CreatureText>> creatureTextsDict = new Dictionary<uint, List<CreatureText>>();

        public CreatureScriptsCreator(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void FillSpellsGrid()
        {
            if (mainForm.listBox_CreatureScriptCreator_CreatureGuids.SelectedItem == null)
                return;

            Creature creature = creaturesDict[mainForm.listBox_CreatureScriptCreator_CreatureGuids.SelectedItem.ToString()];
            List<Spell> spellsList = new List<Spell>(from spell in creature.castedSpells.Values orderby spell.spellStartCastTimes.Count != 0 ? spell.spellStartCastTimes.Min() : new TimeSpan() ascending select spell);

            mainForm.dataGridView_CreatureScriptsCreator_Spells.Rows.Clear();

            if (mainForm.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Checked)
            {
                foreach (var spell in spellsList.Where(spell => spell.isCombatSpell))
                {
                    mainForm.dataGridView_CreatureScriptsCreator_Spells.Rows.Add(spell.spellId, spell.name, spell.spellStartCastTimes.Min().ToFormattedString(), spell.combatCastTimings.minCastTime.ToFormattedString(), spell.combatCastTimings.maxCastTime.ToFormattedString(), spell.combatCastTimings.minRepeatTime.ToFormattedString(), spell.combatCastTimings.maxRepeatTime.ToFormattedString(), spell.castTimes, spell);
                }
            }
            else
            {
                foreach (Spell spell in spellsList)
                {
                    mainForm.dataGridView_CreatureScriptsCreator_Spells.Rows.Add(spell.spellId, spell.name, spell.combatCastTimings.minCastTime.ToFormattedString(), 0, 0, 0, 0, spell.castTimes, spell);
                }
            }

            mainForm.dataGridView_CreatureScriptsCreator_Spells.Enabled = true;
        }

        public void FillListBoxWithGuids()
        {
            mainForm.listBox_CreatureScriptCreator_CreatureGuids.Items.Clear();
            mainForm.dataGridView_CreatureScriptsCreator_Spells.Rows.Clear();

            foreach (var creature in creaturesDict.Values.Where(creature => !mainForm.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Checked || creature.HasCombatSpells()).Where(creature => creature.castedSpells.Count != 0))
            {
                if (mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Text != "" && mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Text != "0")
                {
                    if (mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Text == creature.entry.ToString() ||
                        mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Text == creature.guid)
                    {
                        mainForm.listBox_CreatureScriptCreator_CreatureGuids.Items.Add(creature.guid);
                    }
                }
                else
                {
                    mainForm.listBox_CreatureScriptCreator_CreatureGuids.Items.Add(creature.guid);
                }
            }

            mainForm.listBox_CreatureScriptCreator_CreatureGuids.Refresh();
            mainForm.listBox_CreatureScriptCreator_CreatureGuids.Enabled = true;
        }

        public bool GetDataFromSniffFile(string fileName)
        {
            mainForm.SetCurrentStatus("Getting lines...");

            var lines = File.ReadAllLines(fileName);
            Dictionary<long, Packet.PacketTypes> packetIndexes = new Dictionary<long, Packet.PacketTypes>();

            BuildVersions buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            creaturesDict.Clear();

            mainForm.SetCurrentStatus("Searching for packet indexes in lines...");

            Parallel.For(0, lines.Length, index =>
            {
                if (lines[index].Contains("SMSG_UPDATE_OBJECT") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_UPDATE_OBJECT);
                }
                else if (lines[index].Contains("SMSG_AI_REACTION") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_AI_REACTION);
                }
                else if (lines[index].Contains("SMSG_SPELL_START") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_SPELL_START);
                }
                else if (lines[index].Contains("SMSG_CHAT") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_CHAT);
                }
                else if (lines[index].Contains("SMSG_ON_MONSTER_MOVE") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_ON_MONSTER_MOVE);
                }
                else if (lines[index].Contains("SMSG_ATTACK_STOP") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_ATTACK_STOP);
                }
            });

            mainForm.SetCurrentStatus("Parsing SMSG_UPDATE_OBJECT packets...");

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    Parallel.ForEach(UpdateObjectPacket.ParseObjectUpdatePacket(lines, value.Key, buildVersion).AsEnumerable(), packet =>
                    {
                        lock (creaturesDict)
                        {
                            if (!creaturesDict.ContainsKey(packet.creatureGuid))
                            {
                                creaturesDict.Add(packet.creatureGuid, new Creature(packet));
                            }
                            else
                            {
                                creaturesDict[packet.creatureGuid].UpdateCreature(packet);
                            }
                        }
                    });
                }
            });

            mainForm.SetCurrentStatus("Parsing SMSG_SPELL_START packets...");

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_SPELL_START)
                {
                    SpellStartPacket spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, value.Key, buildVersion);
                    if (spellPacket.spellId == 0)
                        return;

                    lock (creaturesDict)
                    {
                        if (creaturesDict.ContainsKey(spellPacket.casterGuid))
                        {
                            if (!creaturesDict[spellPacket.casterGuid].castedSpells.ContainsKey(spellPacket.spellId))
                                creaturesDict[spellPacket.casterGuid].castedSpells.Add(spellPacket.spellId, new Spell(spellPacket));
                            else
                                creaturesDict[spellPacket.casterGuid].UpdateSpells(spellPacket);
                        }
                    }
                }
            });

            mainForm.SetCurrentStatus("Parsing SMSG_AI_REACTION packets...");

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_AI_REACTION)
                {
                    AIReactionPacket reactionPacket = AIReactionPacket.ParseAIReactionPacket(lines, value.Key, buildVersion);
                    if (reactionPacket.creatureGuid == "")
                        return;

                    lock (creaturesDict)
                    {
                        if (creaturesDict.ContainsKey(reactionPacket.creatureGuid))
                        {
                            if (creaturesDict[reactionPacket.creatureGuid].combatStartTime == TimeSpan.Zero ||
                                creaturesDict[reactionPacket.creatureGuid].combatStartTime < reactionPacket.packetSendTime)
                            {
                                creaturesDict[reactionPacket.creatureGuid].combatStartTime = reactionPacket.packetSendTime;
                            }

                            creaturesDict[reactionPacket.creatureGuid].UpdateCombatSpells(reactionPacket);
                        }
                    }
                }
            });

            mainForm.SetCurrentStatus("Parsing SMSG_CHAT packets...");

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_CHAT)
                {
                    ChatPacket chatPacket = ChatPacket.ParseChatPacket(lines, value.Key, buildVersion);
                    if (chatPacket.creatureGuid == "")
                        return;

                    lock (creaturesDict)
                    {
                        Parallel.ForEach(creaturesDict, creature =>
                        {
                            if (creature.Value.entry == chatPacket.creatureEntry)
                            {
                                CreatureText text = new CreatureText(chatPacket, true);

                                if (Math.Floor(creature.Value.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                                Math.Floor(creature.Value.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                                Math.Floor(creature.Value.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
                                {
                                    lock (creatureTextsDict)
                                    {
                                        if (creatureTextsDict.ContainsKey(chatPacket.creatureEntry) && creatureTextsDict[chatPacket.creatureEntry].Count(x => x.creatureText == text.creatureText) == 0)
                                        {
                                            creatureTextsDict[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, true));
                                        }
                                        else if (!creatureTextsDict.ContainsKey(chatPacket.creatureEntry))
                                        {
                                            creatureTextsDict.Add(chatPacket.creatureEntry, new List<CreatureText>());
                                            creatureTextsDict[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, true));
                                        }
                                    }
                                }

                                if (Math.Floor(creature.Value.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                                Math.Floor(creature.Value.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                                Math.Floor(creature.Value.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
                                {
                                    lock (creatureTextsDict)
                                    {

                                        if (creatureTextsDict.ContainsKey(chatPacket.creatureEntry) && creatureTextsDict[chatPacket.creatureEntry].Count(x => x.creatureText == text.creatureText) == 0)
                                        {
                                            creatureTextsDict[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, false, true));
                                        }
                                        else if (!creatureTextsDict.ContainsKey(chatPacket.creatureEntry))
                                        {
                                            creatureTextsDict.Add(chatPacket.creatureEntry, new List<CreatureText>());
                                            creatureTextsDict[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, false, true));
                                        }
                                    }
                                }
                            }
                        });
                    }
                }
            });

            mainForm.SetCurrentStatus("Parsing SMSG_ON_MONSTER_MOVE and SMSG_ATTACK_STOP packets...");

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                switch (value.Value)
                {
                    case Packet.PacketTypes.SMSG_ON_MONSTER_MOVE:
                    {
                        MonsterMovePacket movePacket = MonsterMovePacket.ParseMovementPacket(lines, value.Key, buildVersion);
                        if (movePacket.creatureGuid == "")
                            return;

                        lock (creaturesDict)
                        {
                            if (creaturesDict.ContainsKey(movePacket.creatureGuid))
                            {
                                creaturesDict[movePacket.creatureGuid].UpdateSpellsByMovementPacket(movePacket);
                            }
                        }

                        break;
                    }
                    case Packet.PacketTypes.SMSG_ATTACK_STOP:
                    {
                        AttackStopPacket attackStopPacket = AttackStopPacket.ParseAttackStopkPacket(lines, value.Key, buildVersion);
                        if (attackStopPacket.creatureGuid == "")
                            return;

                        lock (creaturesDict)
                        {
                            if (creaturesDict.ContainsKey(attackStopPacket.creatureGuid))
                            {
                                creaturesDict[attackStopPacket.creatureGuid].UpdateSpellsByAttackStopPacket(attackStopPacket);

                                if (attackStopPacket.nowDead)
                                {
                                    creaturesDict[attackStopPacket.creatureGuid].deathTime = attackStopPacket.packetSendTime;
                                }
                            }
                        }

                        break;
                    }
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

            Parallel.ForEach(creaturesDict, creature =>
            {
                creature.Value.CreateDeathSpells();
            });

            if (mainForm.checkBox_CreatureScriptsCreator_CreateDataFile.Checked)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_script_packets.dat"), FileMode.OpenOrCreate))
                {
                    Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                    {
                        { 0, creaturesDict },
                        { 1, creatureTextsDict }
                    };

                    binaryFormatter.Serialize(fileStream, dictToSerialize);
                }
            }

            mainForm.SetCurrentStatus("");
            return true;
        }

        public bool GetPacketsFromDataFile(string fileName)
        {
            mainForm.toolStripStatusLabel_FileStatus.Text = "Current status: Getting packets from data file...";

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Dictionary<uint, object> dictFromSerialize = new Dictionary<uint, object>();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                dictFromSerialize = (Dictionary<uint, object>)binaryFormatter.Deserialize(fileStream);
            }

            creaturesDict = (Dictionary<string, Creature>)dictFromSerialize[0];
            creatureTextsDict = (Dictionary<uint, List<CreatureText>>)dictFromSerialize[1];

            return true;
        }

        public void FillSQLOutput()
        {
            Creature creature = creaturesDict[mainForm.listBox_CreatureScriptCreator_CreatureGuids.SelectedItem.ToString()];
            int i = 0;

            var SQLtext = "UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = " + creature.entry + ";\r\n";
            SQLtext += "DELETE FROM `smart_scripts` WHERE `entryorguid` = " + creature.entry + ";\r\n";
            SQLtext += "INSERT INTO `smart_scripts` (`entryorguid`, `source_type`, `id`, `link`, `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_difficulties`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_x`, `target_y`, `target_z`, `target_o`, `comment`) VALUES\r\n";

            if (IsCreatureHasAggroText(creature.entry))
            {
                SQLtext += "(" + creature.entry + ", 0, " + i + ", 0, 4, 0, 100, 0, '', 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - On aggro - Say line 0'),\r\n";
                i++;
            }

            if (IsCreatureHasDeathText(creature.entry))
            {
                SQLtext += "(" + creature.entry + ", 0, " + i + ", 0, 6, 0, 100, 0, '', 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - On death - Say line 1'),\r\n";
                i++;
            }

            for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++, i++)
            {
                Spell spell = (Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value;

                if (spell.isDeathSpell)
                {
                    if (spell.ShouldBeCastedBeforeDeath())
                    {
                        SQLtext += "(" + creature.entry + ", 0, " + i + ", 0, 82, 0, 100, 0, '', 0, 0, 0, 0, 11, " + spell.spellId + ", 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - Before death - Cast " + spell.name + "')";
                    }
                    else
                    {
                        SQLtext += "(" + creature.entry + ", 0, " + i + ", 0, 6, 0, 100, 0, '', 0, 0, 0, 0, 11, " + spell.spellId + ", 0, 0, 0, 0, 0, " + spell.GetTargetType() + ", 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - On death - Cast " + spell.name + "')";
                    }
                }
                else
                {
                    SQLtext += "(" + creature.entry + ", 0, " + i + ", 0, 0, 0, 100, 0, '', " + Math.Floor(spell.combatCastTimings.minCastTime.TotalSeconds) * 1000 + ", " + Math.Floor(spell.combatCastTimings.maxCastTime.TotalSeconds) * 1000 + ", " + Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) * 1000 + ", " + Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds) * 1000 + ", 11, " + spell.spellId + ", 0, " + (spell.needConeDelay ? (Math.Floor(spell.spellCastTime.TotalSeconds) + 1) * 1000 : 0) + ", 0, 0, 0, " + spell.GetTargetType() + ", 0, 0, 0, 0, 0, 0, 0, '" + creature.name + " - IC - Cast " + spell.name + "')";
                }

                if (l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount - 1)
                {
                    SQLtext += ",\r\n";
                }
                else
                {
                    SQLtext += ";\r\n";
                }
            }

            mainForm.textBox_SqlOutput.Text = SQLtext;
        }

        public void CreateCoreScript()
        {
            if (mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount == 0)
                return;

            Creature creature = creaturesDict[mainForm.listBox_CreatureScriptCreator_CreatureGuids.SelectedItem.ToString()];

            string scriptBody = "";
            string defaultName = "";
            string scriptName = "";

            string creatureNameQuery = $"SELECT `Name1` FROM `creature_template_wdb` WHERE `entry` = {creature.entry};";
            var creatureNameDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(creatureNameQuery) : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    defaultName = row[0].ToString();
                }
            }

            if (defaultName == "")
                return;

            scriptName = $"npc_{CreatureScriptTemplate.NormilizeScriptName(defaultName)}_{creature.entry}";
            scriptBody = $"/// {defaultName} - {creature.entry}" + "\r\n";
            scriptBody += $"struct {scriptName} : public ScriptedAI" + "\r\n";
            scriptBody += "{" + "\r\n";
            scriptBody += $"{AddSpacesCount(4)}explicit {scriptName}(Creature* p_Creature) : ScriptedAI(p_Creature) {{ }}";
            scriptBody += GetEnumsBody();
            scriptBody += GetHooksBody(creature);
            scriptBody += "\r\n" + "};" + "\r\n";

            Clipboard.SetText(scriptBody);
        }

        private string GetEnumsBody()
        {
            string body = "";

            body += $"\r\n\r\n{AddSpacesCount(4)}enum eSpells\r\n{AddSpacesCount(4)}{{";

            for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++)
            {
                Spell spell = (Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value;

                if (l == 0)
                {
                    body += $"\r\n{AddSpacesCount(8)}{NormilizeName(spell.name)} = {spell.spellId}";
                }
                else
                {
                    body += $",\r\n{AddSpacesCount(8)}{NormilizeName(spell.name)} = {spell.spellId}";
                }
            }

            body += $"\r\n{AddSpacesCount(4)}}};";

            bool hasCombatSpells = false;

            for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++)
            {
                if (!((Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value).isDeathSpell)
                {
                    hasCombatSpells = true;
                }
            }

            if (hasCombatSpells)
            {
                body += $"\r\n\r\n{AddSpacesCount(4)}enum eEvents\r\n{AddSpacesCount(4)}{{";

                for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++)
                {
                    Spell spell = (Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value;

                    if (!spell.isDeathSpell && l == 0)
                    {
                        body += $"\r\n{AddSpacesCount(8)}Cast{NormilizeName(spell.name)} = {l + 1}";
                    }
                    else if (!spell.isDeathSpell && l > 0)
                    {
                        body += $",\r\n{AddSpacesCount(8)}Cast{NormilizeName(spell.name)} = {l + 1}";
                    }
                }

                body += $"\r\n{AddSpacesCount(4)}}};";
            }

            return body;
        }

        private string GetHooksBody(Creature creature)
        {
            string body = "";

            /// Reset
            body += $"\r\n\r\n{AddSpacesCount(4)}void Reset() override";
            body += $"\r\n{AddSpacesCount(4)}{{\r\n{AddSpacesCount(8)}events.Reset();\r\n{AddSpacesCount(4)}}}";

            /// EnterCombat
            body += $"\r\n\r\n{AddSpacesCount(4)}void EnterCombat(Unit* /*p_Victim*/) override";
            body += $"\r\n{AddSpacesCount(4)}{{";

            if (IsCreatureHasAggroText(creature.entry))
            {
                body += $"\r\n{AddSpacesCount(8)}if (roll_chance_i({GetCreatureTexts(creature.entry).Count(x => x.isAggroText) * 10}))\r\n{AddSpacesCount(8)}{{\r\n{AddSpacesCount(12)}Talk(0);\r\n{AddSpacesCount(8)}}}";
            }

            for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++)
            {
                Spell spell = (Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value;

                if (!spell.isDeathSpell && l == 0)
                {
                    if (IsCreatureHasAggroText(creature.entry))
                    {
                        body += $"\r\n\r\n{AddSpacesCount(8)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, urand({Math.Floor(spell.combatCastTimings.minCastTime.TotalSeconds) * 1000}, {Math.Floor(spell.combatCastTimings.maxCastTime.TotalSeconds) * 1000}));";
                    }
                    else
                    {
                        body += $"\r\n{AddSpacesCount(8)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, urand({Math.Floor(spell.combatCastTimings.minCastTime.TotalSeconds) * 1000}, {Math.Floor(spell.combatCastTimings.maxCastTime.TotalSeconds) * 1000}));";
                    }
                }
                else if (!spell.isDeathSpell && l > 0)
                {
                    body += $"\r\n{AddSpacesCount(8)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, urand({Math.Floor(spell.combatCastTimings.minCastTime.TotalSeconds) * 1000}, {Math.Floor(spell.combatCastTimings.maxCastTime.TotalSeconds) * 1000}));";
                }
            }

            body += $"\r\n{AddSpacesCount(4)}}}";

            /// JustDied
            if (IsCreatureHasDeathText(creature.entry))
            {
                body += $"\r\n\r\n{AddSpacesCount(4)}void JustDied(Unit* /*p_Killer*/) override";
                body += $"\r\n{AddSpacesCount(4)}{{\r\n{AddSpacesCount(8)}if (roll_chance_i({GetCreatureTexts(creature.entry).Count(x => x.isDeathText) * 10}))\r\n{AddSpacesCount(8)}{{\r\n{AddSpacesCount(12)}Talk(1);\r\n{AddSpacesCount(8)}}}";
                body += $"\r\n{AddSpacesCount(4)}}}";
            }

            /// UpdateAI
            body += $"\r\n\r\n{AddSpacesCount(4)}void UpdateAI(uint32 const p_Diff) override";
            body += $"\r\n{AddSpacesCount(4)}{{\r\n{AddSpacesCount(8)}if (!UpdateVictim())\r\n{AddSpacesCount(12)}return;\r\n\r\n{AddSpacesCount(8)}events.Update(p_Diff);\r\n\r\n{AddSpacesCount(8)}if (me->HasUnitState(UNIT_STATE_CASTING))\r\n{AddSpacesCount(12)}return;";
            body += $"\r\n\r\n{AddSpacesCount(8)}switch (events.ExecuteEvent())\r\n{AddSpacesCount(8)}{{";

            for (int l = 0; l < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount; l++)
            {
                Spell spell = (Spell)mainForm.dataGridView_CreatureScriptsCreator_Spells[8, l].Value;

                if (!spell.isDeathSpell && l + 1 < mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount)
                {
                    body += $"\r\n{AddSpacesCount(12)}case eEvents::Cast{NormilizeName(spell.name)}:\r\n{AddSpacesCount(12)}{{\r\n{AddSpacesCount(16)}" + (spell.GetTargetType() == 1 ? "DoCast" : "DoCastVictim") + $"(eSpells::{NormilizeName(spell.name)});";

                    if (Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) == Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds))
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, {Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) * 1000});";
                    }
                    else if (Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) == 0 && Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds) == 0)
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, 0);";
                    }
                    else
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, urand({Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) * 1000}, {Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds) * 1000}));";
                    }

                    body += $"\r\n{AddSpacesCount(16)}break;";
                    body += $"\r\n{AddSpacesCount(12)}}}";
                }
                else if (!spell.isDeathSpell && l + 1 >= mainForm.dataGridView_CreatureScriptsCreator_Spells.RowCount)
                {
                    body += $"\r\n{AddSpacesCount(12)}case eEvents::Cast{NormilizeName(spell.name)}:\r\n{AddSpacesCount(12)}{{\r\n{AddSpacesCount(16)}" + (spell.GetTargetType() == 1 ? "DoCast" : "DoCastVictim") + $"(eSpells::{NormilizeName(spell.name)});";

                    if (Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) == Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds))
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, {Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) * 1000});";
                    }
                    else if (Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) == 0 && Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds) == 0)
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, 0);";
                    }
                    else
                    {
                        body += $"\r\n{AddSpacesCount(16)}events.ScheduleEvent(eEvents::Cast{NormilizeName(spell.name)}, urand({Math.Floor(spell.combatCastTimings.minRepeatTime.TotalSeconds) * 1000}, {Math.Floor(spell.combatCastTimings.maxRepeatTime.TotalSeconds) * 1000}));";
                    }

                    body += $"\r\n{AddSpacesCount(16)}break;";
                    body += $"\r\n{AddSpacesCount(12)}}}";
                    body += $"\r\n{AddSpacesCount(12)}default:\r\n{AddSpacesCount(16)}break;";
                }
            }

            body += $"\r\n{AddSpacesCount(8)}}}";
            body += $"\r\n\r\n{AddSpacesCount(8)}DoMeleeAttackIfReady();";
            body += $"\r\n{AddSpacesCount(4)}}}";

            return body;
        }

        private static string NormilizeName(string line)
        {
            Regex nonWordRegex = new Regex(@"\W+");
            string normilizedString = line;

            normilizedString = normilizedString.Replace(" ", "");

            foreach (char character in normilizedString)
            {
                if (character == ' ')
                    continue;

                if (nonWordRegex.IsMatch(character.ToString()))
                {
                    normilizedString = normilizedString.Replace(nonWordRegex.Match(character.ToString()).ToString(), "");
                }
            }

            return normilizedString;
        }

        public static uint GetCreatureEntryByGuid(string creatureGuid)
        {
            if (creaturesDict.ContainsKey(creatureGuid))
                return creaturesDict[creatureGuid].entry;

            return 0;
        }

        private static bool IsCreatureHasAggroText(uint creatureEntry)
        {
            if (creatureTextsDict.ContainsKey(creatureEntry))
            {
                return creatureTextsDict[creatureEntry].Any(text => text.isAggroText);
            }

            return false;
        }

        private static bool IsCreatureHasDeathText(uint creatureEntry)
        {
            if (creatureTextsDict.ContainsKey(creatureEntry))
            {
                return creatureTextsDict[creatureEntry].Any(text => text.isDeathText);
            }

            return false;
        }

        private static List<CreatureText> GetCreatureTexts(uint creatureEntry)
        {
            if (creatureTextsDict.ContainsKey(creatureEntry))
            {
                return creatureTextsDict[creatureEntry];
            }

            return null;
        }

        public void OpenFileDialog()
        {
            mainForm.openFileDialog.Title = "Open File";
            mainForm.openFileDialog.Filter = "Parsed Sniff or Data File (*.txt;*.dat)|*.txt;*.dat";
            mainForm.openFileDialog.FilterIndex = 1;
            mainForm.openFileDialog.ShowReadOnly = false;
            mainForm.openFileDialog.Multiselect = false;
            mainForm.openFileDialog.CheckFileExists = true;
        }

        public void ImportStarted()
        {
            mainForm.Cursor = Cursors.WaitCursor;
            mainForm.toolStripButton_CSC_ImportSniff.Enabled = false;
            mainForm.toolStripButton_CreatureScriptsCreator_Search.Enabled = false;
            mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Enabled = false;
            mainForm.listBox_CreatureScriptCreator_CreatureGuids.Enabled = false;
            mainForm.listBox_CreatureScriptCreator_CreatureGuids.Items.Clear();
            mainForm.listBox_CreatureScriptCreator_CreatureGuids.DataSource = null;
            mainForm.dataGridView_CreatureScriptsCreator_Spells.Enabled = false;
            mainForm.dataGridView_CreatureScriptsCreator_Spells.Rows.Clear();
            mainForm.toolStripStatusLabel_FileStatus.Text = "Loading File...";
        }

        public void ImportSuccessful()
        {
            mainForm.toolStripStatusLabel_CurrentAction.Text = "";
            mainForm.toolStripButton_CSC_ImportSniff.Enabled = true;
            mainForm.toolStripButton_CreatureScriptsCreator_Search.Enabled = true;
            mainForm.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Enabled = true;
            mainForm.toolStripStatusLabel_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.Cursor = Cursors.Default;
        }
    }
}
