using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Parsed_File_Advisor
{
    public class ParsedFileAdvisor
    {
        private readonly MainForm mainForm;
        public static Dictionary<string, Creature> creatures = new Dictionary<string, Creature>();
        public List<UpdateObjectPacket> conversationPackets = new List<UpdateObjectPacket>();
        public Dictionary<uint, List<CreatureText>> creatureTexts = new Dictionary<uint, List<CreatureText>>();
        public List<SpellStartPacket> spellPackets = new List<SpellStartPacket>();
        public List<QuestGiverAcceptQuestPacket> questAcceptPackets = new List<QuestGiverAcceptQuestPacket>();
        public List<QuestGiverQuestCompletePacket> questCompletePackets = new List<QuestGiverQuestCompletePacket>();
        public List<PlayerMovePacket> playerMovePackets = new List<PlayerMovePacket>();
        public List<QuestUpdateAddCreditPacket> addCreditPackets = new List<QuestUpdateAddCreditPacket>();

        public ParsedFileAdvisor(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }
        public uint GetDataFromFiles(string[] fileNames)
        {
            uint successfullyParsedFilesCount = 0;

            foreach (string fileName in fileNames)
            {
                if (fileName.Contains("txt") && GetDataFromTxtFile(fileName, fileNames.Length > 1))
                {
                    successfullyParsedFilesCount++;
                }
                else if (fileName.Contains("dat") && GetDataFromBinFile(fileName, fileNames.Length > 1))
                {
                    successfullyParsedFilesCount++;
                }
            }

            return successfullyParsedFilesCount;
        }

        public bool GetDataFromTxtFile(string fileName, bool multiSelect)
        {
            var lines = File.ReadAllLines(fileName);
            Dictionary<long, Packet.PacketTypes> packetIndexes = new Dictionary<long, Packet.PacketTypes>();
            BuildVersions buildVersion = LineGetters.GetBuildVersion(lines);

            if (!IsTxtFileValidForParse(fileName, lines, buildVersion))
                return false;

            if (!multiSelect)
            {
                creatures.Clear();
            }

            Parallel.For(0, lines.Length, index =>
            {
                Packet.PacketTypes packetType = Packet.GetPacketTypeFromLine(lines[index]);

                if (packetType == Packet.PacketTypes.SMSG_UPDATE_OBJECT && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if ((packetType == Packet.PacketTypes.SMSG_SPELL_START || packetType == Packet.PacketTypes.SMSG_SPELL_GO) && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if (packetType == Packet.PacketTypes.SMSG_CHAT && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if (Packet.IsQuestPacket(packetType) && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if (Packet.IsPlayerMovePacket(packetType) && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if(packetType == Packet.PacketTypes.SMSG_ON_MONSTER_MOVE && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if(packetType == Packet.PacketTypes.SMSG_AI_REACTION && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
                else if(packetType == Packet.PacketTypes.SMSG_ATTACK_STOP && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
            });

            foreach (var value in packetIndexes)
            {
                if (value.Value == Packet.PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    Parallel.ForEach(UpdateObjectPacket.ParseObjectUpdatePacket(lines, value.Key, buildVersion, 0), packet =>
                    {
                        if (packet.objectType == UpdateObjectPacket.ObjectTypes.Conversation)
                        {
                            lock (conversationPackets)
                            {
                                conversationPackets.Add(packet);
                            }
                        }
                        else
                        {
                            lock (creatures)
                            {
                                if (!creatures.ContainsKey(packet.guid))
                                {
                                    creatures.Add(packet.guid, new Creature(packet));
                                }
                                else
                                {
                                    creatures[packet.guid].UpdateCreature(packet);
                                }
                            }
                        }
                    });
                }
            }

            conversationPackets = conversationPackets.OrderBy(x => x.packetSendTime).ToList();

            Parallel.ForEach(creatures.Values, creature =>
            {
                creature.name = MainForm.GetCreatureNameByEntry(creature.entry);
            });

            Parallel.ForEach(packetIndexes, value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_AI_REACTION)
                {
                    AIReactionPacket reactionPacket = AIReactionPacket.ParseAIReactionPacket(lines, value.Key, buildVersion);
                    if (reactionPacket.creatureGuid == "")
                        return;

                    lock (creatures)
                    {
                        if (creatures.ContainsKey(reactionPacket.creatureGuid))
                        {
                            if (creatures[reactionPacket.creatureGuid].combatStartTime == TimeSpan.Zero ||
                                creatures[reactionPacket.creatureGuid].combatStartTime < reactionPacket.packetSendTime)
                            {
                                creatures[reactionPacket.creatureGuid].combatStartTime = reactionPacket.packetSendTime;
                            }
                        }
                    }
                }
            });

            Parallel.ForEach(packetIndexes, value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_ATTACK_STOP)
                {
                    AttackStopPacket attackStopPacket = AttackStopPacket.ParseAttackStopkPacket(lines, value.Key, buildVersion);
                    if (attackStopPacket.creatureGuid == "")
                        return;

                    lock (creatures)
                    {
                        if (creatures.ContainsKey(attackStopPacket.creatureGuid))
                        {
                            if (attackStopPacket.nowDead)
                            {
                                creatures[attackStopPacket.creatureGuid].deathTime = attackStopPacket.packetSendTime;
                            }
                        }
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_SPELL_START || value.Value == Packet.PacketTypes.SMSG_SPELL_GO)
                {
                    SpellStartPacket spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, value.Key, buildVersion, value.Value, true);
                    if (spellPacket.spellId == 0)
                    {
                        spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, value.Key, buildVersion, value.Value, false);
                        if (spellPacket.spellId == 0)
                            return;
                    }

                    lock (spellPackets)
                    {
                        spellPackets.Add(spellPacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes, value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_CHAT)
                {
                    ChatPacket chatPacket = ChatPacket.ParseChatPacket(lines, value.Key, buildVersion);
                    if (chatPacket.creatureGuid == "")
                        return;

                    CreatureText text = new CreatureText(chatPacket);
                    Creature sedner = creatures.FirstOrDefault(x => x.Value.guid == chatPacket.creatureGuid).Value;

                    if (Math.Floor(sedner.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                        Math.Floor(sedner.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                        Math.Floor(sedner.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
                    {
                        lock (creatureTexts)
                        {
                            if (creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, true));
                            }
                            else if (!creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts.Add(chatPacket.creatureEntry, new List<CreatureText>());
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, true));
                            }
                        }
                    }
                    else if (Math.Floor(sedner.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                             Math.Floor(sedner.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                             Math.Floor(sedner.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
                    {
                        lock (creatureTexts)
                        {
                            if (creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, false, true));
                            }
                            else if (!creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts.Add(chatPacket.creatureEntry, new List<CreatureText>());
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket, false, true));
                            }
                        }
                    }
                    else
                    {
                        lock (creatureTexts)
                        {
                            if (creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket));
                            }
                            else if (!creatureTexts.ContainsKey(chatPacket.creatureEntry))
                            {
                                creatureTexts.Add(chatPacket.creatureEntry, new List<CreatureText>());
                                creatureTexts[chatPacket.creatureEntry].Add(new CreatureText(chatPacket));
                            }
                        }
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.CMSG_QUEST_GIVER_ACCEPT_QUEST)
                {
                    QuestGiverAcceptQuestPacket questAcceptPacket = QuestGiverAcceptQuestPacket.ParseQuestGiverAcceptQuestPacket(lines, value.Key);
                    if (questAcceptPacket.questId == 0)
                        return;

                    lock (questAcceptPackets)
                    {
                        questAcceptPackets.Add(questAcceptPacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_QUEST_GIVER_QUEST_COMPLETE)
                {
                    QuestGiverQuestCompletePacket questCompletePacket = QuestGiverQuestCompletePacket.ParseQuestGiverQuestCompletePacket(lines, value.Key);
                    if (questCompletePacket.questId == 0)
                        return;

                    lock (questCompletePackets)
                    {
                        questCompletePackets.Add(questCompletePacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (Packet.IsPlayerMovePacket(value.Value))
                {
                    PlayerMovePacket playerMovePacket = PlayerMovePacket.ParsePlayerMovePacket(lines, value.Key, buildVersion);
                    if (playerMovePacket.playerGuid == "" || !playerMovePacket.position.IsValid())
                        return;

                    lock (playerMovePackets)
                    {
                        playerMovePackets.Add(playerMovePacket);
                    }
                }
            });

            playerMovePackets = playerMovePackets.OrderBy(x => x.packetSendTime).ToList();

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_QUEST_UPDATE_ADD_CREDIT)
                {
                    QuestUpdateAddCreditPacket addCreditPacket = QuestUpdateAddCreditPacket.ParseQuestUpdateAddCreditPacket(lines, value.Key);
                    if (addCreditPacket.questId == 0 || addCreditPacket.objectId == 0)
                        return;

                    lock (addCreditPackets)
                    {
                        addCreditPackets.Add(addCreditPacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_ON_MONSTER_MOVE)
                {
                    MonsterMovePacket movePacket = MonsterMovePacket.ParseMovementPacket(lines, value.Key, buildVersion);
                    if (movePacket.creatureGuid != "")
                    {
                        lock (creatures)
                        {
                            if (creatures.ContainsKey(movePacket.creatureGuid))
                            {
                                creatures[movePacket.creatureGuid].AddWaypointsFromMovementPacket(movePacket);
                            }
                        }
                    }
                }
            });

            if (mainForm.checkBox_ParsedFileAdvisor_CreateDataFile.Checked)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (!multiSelect)
                {
                    using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_parsed_filed_advisor_packets.dat"), FileMode.OpenOrCreate))
                    {
                        Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                        {
                            { 0, creatures            },
                            { 1, conversationPackets  },
                            { 2, creatureTexts        },
                            { 3, spellPackets         },
                            { 4, questAcceptPackets   },
                            { 5, questCompletePackets },
                            { 6, playerMovePackets    },
                            { 7, addCreditPackets     }
                        };

                        binaryFormatter.Serialize(fileStream, dictToSerialize);
                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "multi_selected_parsed_filed_advisor_packets.dat"), FileMode.OpenOrCreate))
                    {
                        Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                        {
                            { 0, creatures            },
                            { 1, conversationPackets  },
                            { 2, creatureTexts        },
                            { 3, spellPackets         },
                            { 4, questAcceptPackets   },
                            { 5, questCompletePackets },
                            { 6, playerMovePackets    },
                            { 7, addCreditPackets     }
                        };

                        binaryFormatter.Serialize(fileStream, dictToSerialize);
                    }
                }
            }

            return true;
        }

        public bool GetDataFromBinFile(string fileName, bool multiSelect)
        {
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "Current status: Getting packets from data file...";

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Dictionary<uint, object> dictFromSerialize = new Dictionary<uint, object>();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                dictFromSerialize = (Dictionary<uint, object>)binaryFormatter.Deserialize(fileStream);
            }

            Dictionary<string, Creature> creaturesFromSerialize = (Dictionary<string, Creature>)dictFromSerialize[0];
            List<UpdateObjectPacket> conversationPacketsFromSerialize = (List<UpdateObjectPacket>)dictFromSerialize[1];
            Dictionary<uint, List<CreatureText>> creatureTextsFromSerialize = (Dictionary<uint, List<CreatureText>>)dictFromSerialize[2];
            List<SpellStartPacket> spellPacketsFromSerialize = (List<SpellStartPacket>)dictFromSerialize[3];
            List<QuestGiverAcceptQuestPacket> questAcceptPacketsFromSerialize = (List<QuestGiverAcceptQuestPacket>)dictFromSerialize[4];
            List<QuestGiverQuestCompletePacket> questCompletePacketsFromSerialize = (List<QuestGiverQuestCompletePacket>)dictFromSerialize[5];
            List<PlayerMovePacket> playerMovePacketsFromSerialize = (List<PlayerMovePacket>)dictFromSerialize[6];
            List<QuestUpdateAddCreditPacket> addCreditPacketsFromSerialize = (List<QuestUpdateAddCreditPacket>)dictFromSerialize[7];

            if (multiSelect)
            {
                creatures = creatures.Concat(creaturesFromSerialize.Where(x => !creatures.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                conversationPackets = conversationPackets.Concat(conversationPacketsFromSerialize.Where(x => !conversationPackets.Contains(x))).ToList();
                creatureTexts = creatureTexts.Concat(creatureTextsFromSerialize.Where(x => !creatureTexts.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                spellPackets = spellPackets.Concat(spellPacketsFromSerialize.Where(x => !spellPackets.Contains(x))).ToList();
                questAcceptPackets = questAcceptPackets.Concat(questAcceptPacketsFromSerialize.Where(x => !questAcceptPackets.Contains(x))).ToList();
                questCompletePackets = questCompletePackets.Concat(questCompletePacketsFromSerialize.Where(x => !questCompletePackets.Contains(x))).ToList();
                playerMovePackets = playerMovePackets.Concat(playerMovePacketsFromSerialize.Where(x => !playerMovePackets.Contains(x))).ToList();
                addCreditPackets = addCreditPackets.Concat(addCreditPacketsFromSerialize.Where(x => !addCreditPackets.Contains(x))).ToList();
            }
            else
            {
                creatures = creaturesFromSerialize;
                conversationPackets = conversationPacketsFromSerialize;
                creatureTexts = creatureTextsFromSerialize;
                spellPackets = spellPacketsFromSerialize;
                questAcceptPackets = questAcceptPacketsFromSerialize;
                questCompletePackets = questCompletePacketsFromSerialize;
                playerMovePackets = playerMovePacketsFromSerialize;
                addCreditPackets = addCreditPacketsFromSerialize;
            }

            return true;
        }

        public void OpenFileDialog()
        {
            mainForm.openFileDialog.Title = "Open File";
            mainForm.openFileDialog.Filter = "Parsed Sniff or Data File (*.txt;*.dat)|*.txt;*.dat";
            mainForm.openFileDialog.FilterIndex = 1;
            mainForm.openFileDialog.ShowReadOnly = false;
            mainForm.openFileDialog.Multiselect = true;
            mainForm.openFileDialog.CheckFileExists = true;
        }
        public void ImportStarted()
        {
            mainForm.Cursor = Cursors.WaitCursor;
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = false;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "Loading File...";
            mainForm.Update();
        }

        public void ImportSuccessful()
        {
            mainForm.Cursor = Cursors.Default;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_AreaTriggerSplines.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Enabled = true;
            mainForm.Update();
        }
        public void ImportFailed()
        {
            mainForm.Cursor = Cursors.Default;
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = true;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "No File Loaded";
            mainForm.Update();
        }

        public static uint GetCreatureEntryByGuid(string creatureGuid)
        {
            if (creatures.ContainsKey(creatureGuid))
                return creatures[creatureGuid].entry;

            return 0;
        }

        public void ParsePlayerCastedSpells()
        {
            if (mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Text == "")
                return;

            string playerGuid = mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Text;
            string output = "";
            List<KeyValuePair<SpellStartPacket, uint>> spellPacketsListPair = new List<KeyValuePair<SpellStartPacket, uint>>();

            Parallel.ForEach(spellPackets, spellPacket =>
            {
                if (spellPacket.casterGuid != playerGuid)
                    return;

                lock (spellPackets)
                {
                    int spellIndex = spellPacketsListPair.FindIndex(x => x.Key.spellId == spellPacket.spellId);

                    if (spellIndex == -1)
                    {
                        spellPacketsListPair.Add(new KeyValuePair<SpellStartPacket, uint>(spellPacket, 1));
                    }
                    else if (spellPacketsListPair[spellIndex].Key.type == Packet.PacketTypes.SMSG_SPELL_GO && spellPacket.type == Packet.PacketTypes.SMSG_SPELL_START)
                    {
                        spellPacketsListPair.Add(new KeyValuePair<SpellStartPacket, uint>(spellPacket, 1));
                        spellPacketsListPair.RemoveAt(spellIndex);
                    }
                    else if ((spellPacketsListPair[spellIndex].Key.type == Packet.PacketTypes.SMSG_SPELL_START && spellPacket.type == Packet.PacketTypes.SMSG_SPELL_START) ||
                        (spellPacketsListPair[spellIndex].Key.type == Packet.PacketTypes.SMSG_SPELL_GO && spellPacket.type == Packet.PacketTypes.SMSG_SPELL_GO))
                    {

                        if (spellPacketsListPair[spellIndex].Key.spellCastStartTime >= spellPacket.spellCastStartTime)
                        {
                            spellPacketsListPair.Add(new KeyValuePair<SpellStartPacket, uint>(spellPacket, spellPacketsListPair[spellIndex].Value + 1));
                            spellPacketsListPair.RemoveAt(spellIndex);
                        }
                        else
                        {
                            spellPacketsListPair.Add(new KeyValuePair<SpellStartPacket, uint>(spellPacketsListPair[spellIndex].Key, spellPacketsListPair[spellIndex].Value + 1));
                            spellPacketsListPair.RemoveAt(spellIndex);
                        }
                    }
                }
            });

            spellPacketsListPair = spellPacketsListPair.Where(x => x.Key.casterGuid == playerGuid).OrderBy(x => x.Key.spellCastStartTime).ToList();

            output += "Spells casted by player with guid \"" + playerGuid + "\"" + "\r\n";

            foreach (var spell in spellPacketsListPair)
            {
                output += "Spell Id: " + spell.Key.spellId + " (" + Spell.GetSpellName(spell.Key.spellId) + "), Cast Time: " + spell.Key.spellCastStartTime.ToFormattedStringWithMilliseconds() + ", Casted times: " + spell.Value + "\r\n";
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        public void ParseSpellDestinations()
        {
            if (mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Text == "" || mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Text == "0")
                return;

            string spellId = mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Text;
            string output = "";
            List<Position> allDestPositions = new List<Position>();
            List<Position> uniqDestPositions = new List<Position>();

            Parallel.ForEach(spellPackets, spellPacket =>
            {
                if (spellPacket.spellId != Convert.ToUInt32(spellId) || spellPacket.type != Packet.PacketTypes.SMSG_SPELL_START || !spellPacket.spellDestination.IsValid())
                    return;

                lock (allDestPositions)
                {
                    allDestPositions.Add(spellPacket.spellDestination);

                }
                lock (uniqDestPositions)
                {
                    if (!uniqDestPositions.Contains(spellPacket.spellDestination) && !CheckIfNearPointExist(spellPacket.spellDestination, uniqDestPositions))
                    {
                        uniqDestPositions.Add(spellPacket.spellDestination);
                    }
                }
            });

            output += "Spell destinations for spell " + spellId + "\n\n";

            output += "All positions count " + allDestPositions.Count + "\n";

            foreach (Position pos in allDestPositions)
            {
                output += "{ " + pos.x.ToString().Replace(",", ".") + "f, " + pos.y.ToString().Replace(",", ".") + "f, " + pos.z.ToString().Replace(",", ".") + "f },\n";
            }

            output += "Unique positions count " + allDestPositions.Count + "\n";

            foreach (Position pos in uniqDestPositions)
            {
                output += "{ " + pos.x.ToString().Replace(",", ".") + "f, " + pos.y.ToString().Replace(",", ".") + "f, " + pos.z.ToString().Replace(",", ".") + "f },\n";
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        private bool CheckIfNearPointExist(Position point, List<Position> points)
        {
            foreach (Position pos in points)
            {
                if (pos.GetExactDist2d(point) <= 5.0f)
                    return true;
            }

            return false;
        }

        public void ParseQuestConversations()
        {
            if (mainForm.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Text == "" || mainForm.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Text == "0")
                return;

            string output = "";
            uint questId = Convert.ToUInt32(mainForm.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Text);
            QuestGiverAcceptQuestPacket acceptPacket = questAcceptPackets.FirstOrDefault(x => x.questId == questId);
            QuestGiverQuestCompletePacket completePacket = questCompletePackets.FirstOrDefault(x => x.questId == questId);

            if (acceptPacket.questId != 0)
            {
                UpdateObjectPacket acceptConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= acceptPacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - acceptPacket.packetSendTime.TotalMilliseconds) <= 2500);
                CreatureText creatureText = GetCreatureTextBasedOnTargetTimeSpan(acceptPacket.packetSendTime);
                if (acceptConversationPacket.entry != 0)
                {
                    output += $"- Conversation that goes after accepting quest \"{MainForm.GetQuestNameById(acceptPacket.questId)}\" ({acceptPacket.questId}):\r\n";
                    output += $"Enum value: Quest{GetNormilizedQuestName(acceptPacket.questId)}AcceptedConversation = {acceptConversationPacket.entry}\r\n";
                    output += GetConversationData(acceptConversationPacket);
                }
                else if (creatureText != null)
                {
                    output += $"- Creature text that goes after accepting quest \"{MainForm.GetQuestNameById(acceptPacket.questId)}\" ({acceptPacket.questId}):\r\n";
                    output += GetCreatureTextData(creatureText);
                }
            }

            foreach (QuestUpdateAddCreditPacket creditPacket in addCreditPackets.Where(x => x.questId == questId))
            {
                UpdateObjectPacket addCreditConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= creditPacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - creditPacket.packetSendTime.TotalMilliseconds) <= 2500);
                CreatureText creatureText = GetCreatureTextBasedOnTargetTimeSpan(creditPacket.packetSendTime);
                if (addCreditConversationPacket.entry != 0)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    uint objectiveId = 0;
                    string description = "";

                    var objectiveIdDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery($"SELECT `ID`, `Description` FROM `quest_objectives` WHERE `QuestID` = {creditPacket.questId} AND `ObjectID` = {creditPacket.objectId};") : null;

                    if (objectiveIdDs != null)
                    {
                        foreach (DataRow row in objectiveIdDs.Tables["table"].Rows)
                        {
                            objectiveId = Convert.ToUInt32(row[0]);
                            description = row[1].ToString();
                        }
                    }

                    if (description != "")
                    {
                        output += $"- Conversation that goes after completing objective \"{description}\" ({objectiveId}):\r\n";
                    }
                    else
                    {
                        output += $"- Conversation that goes after completing objective {objectiveId}:\r\n";
                    }

                    output += GetConversationData(addCreditConversationPacket);
                }
                else if (creatureText != null)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    uint objectiveId = 0;
                    string description = "";

                    var objectiveIdDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery($"SELECT `ID`, `Description` FROM `quest_objectives` WHERE `QuestID` = {creditPacket.questId} AND `ObjectID` = {creditPacket.objectId};") : null;

                    if (objectiveIdDs != null)
                    {
                        foreach (DataRow row in objectiveIdDs.Tables["table"].Rows)
                        {
                            objectiveId = Convert.ToUInt32(row[0]);
                            description = row[1].ToString();
                        }
                    }

                    if (description != "")
                    {
                        output += $"- Creature text that goes after completing objective \"{description}\" ({objectiveId}):\r\n";
                    }
                    else
                    {
                        output += $"- Creature text that goes after completing objective {objectiveId}:\r\n";
                    }

                    output += GetCreatureTextData(creatureText);
                }
            }

            if (completePacket.questId != 0)
            {
                UpdateObjectPacket completeConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= completePacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - completePacket.packetSendTime.TotalMilliseconds) <= 2500);
                CreatureText creatureText = GetCreatureTextBasedOnTargetTimeSpan(completePacket.packetSendTime);
                if (completeConversationPacket.entry != 0)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    output += $"- Conversation that goes after rewarding quest \"{MainForm.GetQuestNameById(acceptPacket.questId)}\" ({acceptPacket.questId}):\r\n";
                    output += $"Enum value: Quest{GetNormilizedQuestName(acceptPacket.questId)}RewardedConversation = {completeConversationPacket.entry}\r\n";
                    output += GetConversationData(completeConversationPacket);
                }
                else if (creatureText != null)
                {
                    output += $"- Creature text that goes after rewarding quest \"{MainForm.GetQuestNameById(acceptPacket.questId)}\" ({acceptPacket.questId}):\r\n";
                    output += GetCreatureTextData(creatureText);
                }
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        public static string GetNormilizedQuestName(uint questId)
        {
            string questName = MainForm.GetQuestNameById(questId);
            if (questName == "Unknown")
                return questName;

            string[] splittedLine = questName.Split(' ');

            for (int i = 0; i < splittedLine.Length; i++)
            {
                if (!char.IsUpper(splittedLine[i][0]))
                {
                    splittedLine[i] = splittedLine[i].Replace($"{splittedLine[i][0]}", $"{splittedLine[i][0].ToString().ToUpper()}");
                }
            }

            questName = string.Concat(splittedLine);

            Regex nonWordRegex = new Regex(@"\W+");

            foreach (char character in questName)
            {
                if (nonWordRegex.IsMatch(character.ToString()))
                {
                    questName = questName.Replace(nonWordRegex.Match(character.ToString()).ToString(), "");
                }
            }

            return questName;
        }

        CreatureText GetCreatureTextBasedOnTargetTimeSpan(TimeSpan time)
        {
            CreatureText text = null;

            foreach (var itr in creatureTexts.Values)
            {
                text = itr.FirstOrDefault(x => x.sayTime >= time && (x.sayTime.TotalMilliseconds - time.TotalMilliseconds) <= 2500);
                if (text != null)
                    return text;
            }

            return text;
        }

        public void ParseLosConversationsOrTexts()
        {
            if (mainForm.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Text == "")
                return;

            string output = "";
            string playerGuid = mainForm.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Text;

            foreach (UpdateObjectPacket conversationPacket in conversationPackets)
            {
                foreach (PlayerMovePacket movePacket in playerMovePackets.Where(x => x.playerGuid == playerGuid).OrderByDescending(x => x.packetSendTime))
                {
                    if (conversationPacket.packetSendTime.TotalMilliseconds >= movePacket.packetSendTime.TotalMilliseconds && (conversationPacket.packetSendTime.TotalMilliseconds - movePacket.packetSendTime.TotalMilliseconds) <= 2500 && !IsQuestRelatedConversation(conversationPacket))
                    {
                        output += $"- Player triggered LOS conversation at {movePacket.packetSendTime.ToFormattedStringWithMilliseconds()}, {GetEstimatedDistanceToTriggerPosition(conversationPacket, movePacket)}\r\n";
                        output += GetConversationData(conversationPacket);
                        break;
                    }
                }
            }

            List<CreatureText> tempCreatureTexts = new List<CreatureText>();

            foreach (var itr in creatureTexts.Values)
            {
                tempCreatureTexts.AddRange(itr);
            }

            foreach (CreatureText creatureText in tempCreatureTexts.OrderBy(x => x.sayTime))
            {
                foreach (PlayerMovePacket movePacket in playerMovePackets.Where(x => x.playerGuid == playerGuid).OrderByDescending(x => x.packetSendTime))
                {
                    if (!creatureText.isAggroText && !creatureText.isDeathText && creatureText.sayTime.TotalMilliseconds >= movePacket.packetSendTime.TotalMilliseconds && (creatureText.sayTime.TotalMilliseconds - movePacket.packetSendTime.TotalMilliseconds) <= 2500 && !IsQuestRelatedCreatureText(creatureText))
                    {
                        output += $"- Player triggered LOS creature text at {movePacket.packetSendTime.ToFormattedStringWithMilliseconds()}, {GetEstimatedDistanceToTriggerPosition(movePacket, creatureText)}\r\n";
                        output += GetCreatureTextData(creatureText);
                        break;
                    }
                }
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        private bool IsQuestRelatedConversation(UpdateObjectPacket conversationPacket)
        {
            return questAcceptPackets.Where(x => conversationPacket.packetSendTime.TotalMilliseconds >= x.packetSendTime.TotalMilliseconds && (conversationPacket.packetSendTime.TotalMilliseconds - x.packetSendTime.TotalMilliseconds) <= 4000).Count() != 0 ||
                questCompletePackets.Where(x => conversationPacket.packetSendTime.TotalMilliseconds >= x.packetSendTime.TotalMilliseconds && (conversationPacket.packetSendTime.TotalMilliseconds - x.packetSendTime.TotalMilliseconds) <= 4000).Count() != 0;
        }

        private bool IsQuestRelatedCreatureText(CreatureText creatureText)
        {
            return questAcceptPackets.Where(x => creatureText.sayTime.TotalMilliseconds >= x.packetSendTime.TotalMilliseconds && (creatureText.sayTime.TotalMilliseconds - x.packetSendTime.TotalMilliseconds) <= 4000).Count() != 0 ||
                questCompletePackets.Where(x => creatureText.sayTime.TotalMilliseconds >= x.packetSendTime.TotalMilliseconds && (creatureText.sayTime.TotalMilliseconds - x.packetSendTime.TotalMilliseconds) <= 4000).Count() != 0;
        }

        private string GetEstimatedDistanceToTriggerPosition(UpdateObjectPacket conversationPacket, PlayerMovePacket movePacket)
        {
            string output = "estimated distance to trigger position is unknown";
            List<double> estimatedDistances = new List<double>();
            bool thereWasActorWithWaypoints = false;

            if (conversationPacket.conversationData.conversationActors.Count == 0)
            {
                output = "estimated distance to trigger is unknown, because conversation doesn't have any creature actor";
                return output;
            }

            for (int i = 0; i < conversationPacket.conversationData.conversationLines.Count; i++)
            {
                Creature actor;

                if (creatures.ContainsKey(conversationPacket.conversationData.conversationActors[(int)conversationPacket.conversationData.conversationLines[i].Value]))
                {
                    actor = creatures[conversationPacket.conversationData.conversationActors[(int)conversationPacket.conversationData.conversationLines[i].Value]];
                }
                else
                    return output;

                if (!actor.HasWaypoints())
                {
                    estimatedDistances.Add(Math.Round(movePacket.position.GetDistance(actor.spawnPosition)));
                }
                else
                {
                    thereWasActorWithWaypoints = true;

                    List<Waypoint> waypoints = actor.waypoints.Where(x => conversationPacket.packetSendTime >= x.moveStartTime && (conversationPacket.packetSendTime.TotalMilliseconds - x.moveStartTime.TotalMilliseconds) <= 2500).ToList();
                    if (waypoints.Count == 0)
                        continue;

                    Waypoint lastWaypoint = waypoints.OrderBy(x => x.moveStartTime).OrderBy(x => x.idFromParse).LastOrDefault();
                    if (lastWaypoint.movePosition.IsValid())
                    {
                        estimatedDistances.Add(Math.Round(movePacket.position.GetDistance(lastWaypoint.movePosition)));
                    }
                    else if (lastWaypoint.startPosition.IsValid())
                    {
                        estimatedDistances.Add(Math.Round(movePacket.position.GetDistance(lastWaypoint.startPosition)));
                    }
                }
            }

            estimatedDistances = estimatedDistances.Intersect(estimatedDistances).ToList();

            if (estimatedDistances.Count > 1)
            {
                output = "estimated distances to trigger position: ";

                for (int i = 0; i < estimatedDistances.Count; i++)
                {
                    if (i + 1 < estimatedDistances.Count)
                    {
                        output += $"{estimatedDistances[i]}.0f, ";
                    }
                    else
                    {
                        output += $"{estimatedDistances[i]}.0f";
                    }
                }
            }
            else
            {
                output = $"estimated distance to trigger position: {estimatedDistances.FirstOrDefault()}.0f";
            }

            if (thereWasActorWithWaypoints)
            {
                if (conversationPacket.conversationData.conversationActors.Count > 1)
                {
                    output += " (Some actors has waypoints)";
                }
                else
                {
                    output += " (Actor has waypoints)";
                }
            }

            return output;
        }

        private string GetEstimatedDistanceToTriggerPosition(PlayerMovePacket movePacket, CreatureText creatureText)
        {
            string output = "estimated distance to trigger position is unknown";
            Creature actor;
            bool thereWasActorWithWaypoints = false;
            double distance = 0.0f;

            if (creatures.ContainsKey(creatureText.creatureGuid))
            {
                actor = creatures[creatureText.creatureGuid];
            }
            else
                return output;

            if (!actor.HasWaypoints())
            {
                distance = Math.Round(movePacket.position.GetDistance(actor.spawnPosition));
            }
            else
            {
                thereWasActorWithWaypoints = true;

                List<Waypoint> waypoints = actor.waypoints.Where(x => creatureText.sayTime >= x.moveStartTime && (creatureText.sayTime.TotalMilliseconds - x.moveStartTime.TotalMilliseconds) <= 2500).ToList();
                if (waypoints.Count == 0)
                    return output;

                Waypoint lastWaypoint = waypoints.OrderBy(x => x.moveStartTime).OrderBy(x => x.idFromParse).LastOrDefault();
                if (lastWaypoint.movePosition.IsValid())
                {
                    distance = Math.Round(movePacket.position.GetDistance(lastWaypoint.movePosition));
                }
                else if (lastWaypoint.startPosition.IsValid())
                {
                    distance = Math.Round(movePacket.position.GetDistance(lastWaypoint.startPosition));
                }
            }

            output = $"estimated distance to trigger position: {distance}.0f";

            if (thereWasActorWithWaypoints)
            {
                output += " (Sender has waypoints)";
            }

            return output;
        }

        private string GetConversationData(UpdateObjectPacket conversationPacket)
        {
            string output = "";

            output += $"Conversation Id: {conversationPacket.entry}\r\n";

            for (int i = 0; i < conversationPacket.conversationData.conversationLines.Count; i++)
            {
                int conversationLineId = (int)conversationPacket.conversationData.conversationLines[i].Key;
                string conversationLineText = "";
                Creature actor = null;

                if (DBC.DBC.ConversationLine.ContainsKey(conversationLineId))
                {
                    DataSet broadcastTextDs = SQLModule.HotfixSelectQuery($"SELECT `Text`, `Text1` FROM `broadcasttext` WHERE `ROW_ID` = {DBC.DBC.ConversationLine[conversationLineId].BroadcastTextId}");
                    if (broadcastTextDs != null && broadcastTextDs.Tables["table"].Rows.Count != 0)
                    {
                        foreach (string stringRow in broadcastTextDs.Tables["table"].Rows[0].ItemArray)
                        {
                            if (stringRow != "")
                            {
                                conversationLineText = stringRow;
                            }
                        }
                    }
                }

                if (conversationPacket.conversationData.conversationActors.Count != 0)
                {
                    if (creatures.ContainsKey(conversationPacket.conversationData.conversationActors[(int)conversationPacket.conversationData.conversationLines[i].Value]))
                    {
                        actor = creatures[conversationPacket.conversationData.conversationActors[(int)conversationPacket.conversationData.conversationLines[i].Value]];
                    }

                    if (conversationLineText == "" && actor != null)
                    {
                        conversationLineText = "There is no text for this line";
                    }
                    else if (conversationLineText == "" && actor == null)
                    {
                        conversationLineText = "There is no text for this line, probably actor is player";
                    }

                    if (actor != null)
                    {
                        output += $"[{i}] Actor: \"{actor.name}\" ({actor.entry}) - Line: \"{conversationLineText}\" ({conversationLineId})\r\n";
                    }
                    else
                    {
                        output += $"[{i}] Actor is player\r\n";
                    }
                }
                else
                {
                    if (conversationLineText == "")
                    {
                        conversationLineText = "There is no text for this line";
                    }

                    output += $"[{i}] Line: \"{conversationLineText}\" ({conversationLineId})\r\n";
                }
            }

            output += "\r\n";

            return output;
        }

        private string GetCreatureTextData(CreatureText creatureText)
        {
            string output = "Text data is empty";
            Creature sender;

            if (creatures.ContainsKey(creatureText.creatureGuid))
            {
                sender = creatures[creatureText.creatureGuid];
            }
            else
                return output;

            output = $"Sender: \"{sender.name}\" ({sender.entry})\r\n";
            output += $"Text: \"{creatureText.creatureText}\"\r\n";
            output += "\r\n";

            return output;
        }
    }
}
