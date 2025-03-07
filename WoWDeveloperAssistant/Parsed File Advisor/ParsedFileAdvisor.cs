using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Packets.UpdateObjectPacket;
using static WoWDeveloperAssistant.Misc.Utils;
using HtmlAgilityPack;

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
        public List<QuestGiverQuestCompletePacket> questRewardPackets = new List<QuestGiverQuestCompletePacket>();
        public List<QuestUpdateCompletePacket> questCompletePackets = new List<QuestUpdateCompletePacket>();
        public List<PlayerMovePacket> playerMovePackets = new List<PlayerMovePacket>();
        public List<QuestUpdateAddCreditPacket> addCreditPackets = new List<QuestUpdateAddCreditPacket>();
        public Dictionary<string, List<UpdateObjectPacket>> playerCompletedQuestPackets = new Dictionary<string, List<UpdateObjectPacket>>();
        public List<InitWorldStatesPacket> initWorldStatesPackets = new List<InitWorldStatesPacket>();
        public List<UpdateWorldStatePacket> updateWorldStatePackets = new List<UpdateWorldStatePacket>();

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
                if (packetType != Packet.PacketTypes.UNKNOWN_PACKET && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
            });

            foreach (var value in packetIndexes)
            {
                if (value.Value == Packet.PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    Parallel.ForEach(ParseObjectUpdatePacket(lines, value.Key, buildVersion, 0), packet =>
                    {
                        if (packet.objectType == ObjectTypes.Conversation)
                        {
                            lock (conversationPackets)
                            {
                                UpdateObjectPacket existedUpdateObjectPacket = conversationPackets.FirstOrDefault(x => x.entry == packet.entry);
                                if (existedUpdateObjectPacket.entry == 0)
                                {
                                    conversationPackets.Add(packet);
                                }
                            }
                        }
                        else if (packet.questCompletedData.Count != 0)
                        {
                            lock (playerCompletedQuestPackets)
                            {
                                if (playerCompletedQuestPackets.ContainsKey(packet.guid))
                                {
                                    playerCompletedQuestPackets[packet.guid].Add(packet);
                                }
                                else
                                {
                                    playerCompletedQuestPackets.Add(packet.guid, new List<UpdateObjectPacket>() { packet });
                                }
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
                    Creature sender = creatures.FirstOrDefault(x => x.Value.guid == chatPacket.creatureGuid).Value;
                    if (sender == null)
                        return;

                    if (Math.Floor(sender.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                        Math.Floor(sender.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                        Math.Floor(sender.combatStartTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
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
                    else if (Math.Floor(sender.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) ||
                             Math.Floor(sender.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) + 1 ||
                             Math.Floor(sender.deathTime.TotalSeconds) == Math.Floor(chatPacket.packetSendTime.TotalSeconds) - 1)
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
                    QuestGiverQuestCompletePacket questRewardPacket = QuestGiverQuestCompletePacket.ParseQuestGiverQuestCompletePacket(lines, value.Key);
                    if (questRewardPacket.questId == 0)
                        return;

                    lock (questRewardPackets)
                    {
                        questRewardPackets.Add(questRewardPacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_QUEST_UPDATE_COMPLETE)
                {
                    QuestUpdateCompletePacket questCompletePacket = QuestUpdateCompletePacket.ParseQuestUpdateCompletePacket(lines, value.Key);
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

            addCreditPackets = addCreditPackets.OrderBy(x => x.packetSendTime).ToList();

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

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_INIT_WORLD_STATES)
                {
                    InitWorldStatesPacket initWorldStatesPacket = InitWorldStatesPacket.ParseInitWorldStatesPacket(lines, value.Key);
                    if (initWorldStatesPacket.worldStates.Count == 0)
                        return;

                    lock (initWorldStatesPackets)
                    {
                        initWorldStatesPackets.Add(initWorldStatesPacket);
                    }
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_UPDATE_WORLD_STATE)
                {
                    UpdateWorldStatePacket updateWorldStatePacket = UpdateWorldStatePacket.ParseUpdateWorldStatePacket(lines, value.Key);
                    if (updateWorldStatePacket.worldState.Key == 0)
                        return;

                    lock (updateWorldStatePackets)
                    {
                        updateWorldStatePackets.Add(updateWorldStatePacket);
                    }
                }
            });

            if (mainForm.checkBox_ParsedFileAdvisor_CreateDataFile.Checked)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (!multiSelect)
                {
                    using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_parsed_file_advisor_packets.dat"), FileMode.OpenOrCreate))
                    {
                        Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                        {
                            { 0,  creatures                   },
                            { 1,  conversationPackets         },
                            { 2,  creatureTexts               },
                            { 3,  spellPackets                },
                            { 4,  questAcceptPackets          },
                            { 5,  questCompletePackets        },
                            { 6,  questRewardPackets          },
                            { 7,  playerMovePackets           },
                            { 8,  addCreditPackets            },
                            { 9,  playerCompletedQuestPackets },
                            { 10, initWorldStatesPackets      },
                            { 11, updateWorldStatePackets     }
                        };

                        binaryFormatter.Serialize(fileStream, dictToSerialize);
                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "multi_selected_parsed_file_advisor_packets.dat"), FileMode.OpenOrCreate))
                    {
                        Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                        {
                            { 0, creatures                   },
                            { 1, conversationPackets         },
                            { 2, creatureTexts               },
                            { 3, spellPackets                },
                            { 4, questAcceptPackets          },
                            { 5, questCompletePackets        },
                            { 6, questRewardPackets          },
                            { 7, playerMovePackets           },
                            { 8, addCreditPackets            },
                            { 9, playerCompletedQuestPackets },
                            { 10, initWorldStatesPackets     },
                            { 11, updateWorldStatePackets    }
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
            List<QuestUpdateCompletePacket> questCompletePacketsFromSerialize = (List<QuestUpdateCompletePacket>)dictFromSerialize[5];
            List<QuestGiverQuestCompletePacket> questRewardPacketsFromSerialize = (List<QuestGiverQuestCompletePacket>)dictFromSerialize[6];
            List<PlayerMovePacket> playerMovePacketsFromSerialize = (List<PlayerMovePacket>)dictFromSerialize[7];
            List<QuestUpdateAddCreditPacket> addCreditPacketsFromSerialize = (List<QuestUpdateAddCreditPacket>)dictFromSerialize[8];
            Dictionary<string, List<UpdateObjectPacket>> playerCompletedQuestPacketsFromSerialize = (Dictionary<string, List<UpdateObjectPacket>>)dictFromSerialize[9];
            List<InitWorldStatesPacket> initWorldStatesPacketsFromSerialize = (List<InitWorldStatesPacket>)dictFromSerialize[10];
            List<UpdateWorldStatePacket> updateWorldStatePacketsFromSerialize = (List<UpdateWorldStatePacket>)dictFromSerialize[11];

            if (multiSelect)
            {
                creatures = creatures.Concat(creaturesFromSerialize.Where(x => !creatures.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                conversationPackets = conversationPackets.Concat(conversationPacketsFromSerialize.Where(x => !conversationPackets.Contains(x))).ToList();
                creatureTexts = creatureTexts.Concat(creatureTextsFromSerialize.Where(x => !creatureTexts.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                spellPackets = spellPackets.Concat(spellPacketsFromSerialize.Where(x => !spellPackets.Contains(x))).ToList();
                questAcceptPackets = questAcceptPackets.Concat(questAcceptPacketsFromSerialize.Where(x => !questAcceptPackets.Contains(x))).ToList();
                questCompletePackets = questCompletePackets.Concat(questCompletePacketsFromSerialize.Where(x => !questCompletePackets.Contains(x))).ToList();
                questRewardPackets = questRewardPackets.Concat(questRewardPacketsFromSerialize.Where(x => !questRewardPackets.Contains(x))).ToList();
                playerMovePackets = playerMovePackets.Concat(playerMovePacketsFromSerialize.Where(x => !playerMovePackets.Contains(x))).ToList();
                addCreditPackets = addCreditPackets.Concat(addCreditPacketsFromSerialize.Where(x => !addCreditPackets.Contains(x))).ToList();
                playerCompletedQuestPackets = playerCompletedQuestPackets.Concat(playerCompletedQuestPacketsFromSerialize.Where(x => !playerCompletedQuestPackets.Contains(x))).ToDictionary(x => x.Key, x => x.Value);
                initWorldStatesPackets = initWorldStatesPackets.Concat(initWorldStatesPacketsFromSerialize.Where(x => !initWorldStatesPackets.Contains(x))).ToList();
                updateWorldStatePackets = updateWorldStatePackets.Concat(updateWorldStatePacketsFromSerialize.Where(x => !updateWorldStatePackets.Contains(x))).ToList();
            }
            else
            {
                creatures = creaturesFromSerialize;
                conversationPackets = conversationPacketsFromSerialize;
                creatureTexts = creatureTextsFromSerialize;
                spellPackets = spellPacketsFromSerialize;
                questAcceptPackets = questAcceptPacketsFromSerialize;
                questCompletePackets = questCompletePacketsFromSerialize;
                questRewardPackets = questRewardPacketsFromSerialize;
                playerMovePackets = playerMovePacketsFromSerialize;
                addCreditPackets = addCreditPacketsFromSerialize;
                playerCompletedQuestPackets = playerCompletedQuestPacketsFromSerialize;
                initWorldStatesPackets = initWorldStatesPacketsFromSerialize;
                updateWorldStatePackets = updateWorldStatePacketsFromSerialize;
            }

            return true;
        }

        public void OpenFileDialog()
        {
            mainForm.openFileDialog.Title = "Open File";
            mainForm.openFileDialog.Filter = "Parsed sniff or data file (*.txt;*.dat)|*parsed.txt;*parsed_file_advisor_packets.dat";
            mainForm.openFileDialog.FilterIndex = 1;
            mainForm.openFileDialog.ShowReadOnly = false;
            mainForm.openFileDialog.Multiselect = true;
            mainForm.openFileDialog.CheckFileExists = true;
            mainForm.openFileDialog.FileName = " ";
        }
        public void ImportStarted()
        {
            mainForm.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = false;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "Loading File...";
            mainForm.Update();
        }

        public void ImportSuccessful()
        {
            mainForm.Cursor = System.Windows.Forms.Cursors.Default;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_AreaTriggerSplines.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_CreatureEquipmentId.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Enabled = true;
            mainForm.Update();
        }
        public void ImportFailed()
        {
            mainForm.Cursor = System.Windows.Forms.Cursors.Default;
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
                if (spellPacket.spellId != Convert.ToUInt32(spellId) || !spellPacket.spellDestination.IsValid())
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
            QuestUpdateCompletePacket completePacket = questCompletePackets.FirstOrDefault(x => x.questId == questId);
            QuestGiverQuestCompletePacket rewardPacket = questRewardPackets.FirstOrDefault(x => x.questId == questId);

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
                if (completePacket.packetSendTime >= creditPacket.packetSendTime && (completePacket.packetSendTime.TotalMilliseconds - creditPacket.packetSendTime.TotalMilliseconds) <= 1000)
                    continue;

                UpdateObjectPacket addCreditConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= creditPacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - creditPacket.packetSendTime.TotalMilliseconds) <= 2500);
                CreatureText creditCreatureText = GetCreatureTextBasedOnTargetTimeSpan(creditPacket.packetSendTime);

                if (addCreditConversationPacket.entry != 0)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    uint objectiveId = 0;
                    string description = "";

                    var objectiveIdDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery($"SELECT `ID`, `Description` FROM `quest_objectives` WHERE `QuestID` = {creditPacket.questId} AND `ObjectID` = {creditPacket.objectId};") : null;

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
                else if (creditCreatureText != null)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    uint objectiveId = 0;
                    string description = "";

                    var objectiveIdDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery($"SELECT `ID`, `Description` FROM `quest_objectives` WHERE `QuestID` = {creditPacket.questId} AND `ObjectID` = {creditPacket.objectId};") : null;

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

                    output += GetCreatureTextData(creditCreatureText);
                }
            }

            if (completePacket.questId != 0)
            {
                if (completePacket.packetSendTime >= acceptPacket.packetSendTime && (completePacket.packetSendTime.TotalMilliseconds - acceptPacket.packetSendTime.TotalMilliseconds) > 1000)
                {
                    UpdateObjectPacket completeConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= completePacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - completePacket.packetSendTime.TotalMilliseconds) <= 2500);
                    CreatureText completeCreatureText = GetCreatureTextBasedOnTargetTimeSpan(completePacket.packetSendTime);

                    if (completeConversationPacket.entry != 0)
                    {
                        output += $"- Conversation that goes after completing quest \"{MainForm.GetQuestNameById(completePacket.questId)}\" ({completePacket.questId}):\r\n";
                        output += $"Enum value: Quest{GetNormilizedQuestName(completePacket.questId)}CompletedConversation = {completeConversationPacket.entry}\r\n";
                        output += GetConversationData(completeConversationPacket);
                    }
                    else if (completeCreatureText != null)
                    {
                        output += $"- Creature text that goes after completing quest \"{MainForm.GetQuestNameById(completePacket.questId)}\" ({completePacket.questId}):\r\n";
                        output += GetCreatureTextData(completeCreatureText);
                    }
                }
            }

            if (rewardPacket.questId != 0)
            {
                UpdateObjectPacket completeConversationPacket = conversationPackets.FirstOrDefault(x => x.packetSendTime >= rewardPacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - rewardPacket.packetSendTime.TotalMilliseconds) <= 2500);
                CreatureText creatureText = GetCreatureTextBasedOnTargetTimeSpan(rewardPacket.packetSendTime);

                if (completeConversationPacket.entry != 0)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    output += $"- Conversation that goes after rewarding quest \"{MainForm.GetQuestNameById(rewardPacket.questId)}\" ({rewardPacket.questId}):\r\n";
                    output += $"Enum value: Quest{GetNormilizedQuestName(rewardPacket.questId)}RewardedConversation = {completeConversationPacket.entry}\r\n";
                    output += GetConversationData(completeConversationPacket);
                }
                else if (creatureText != null)
                {
                    output += $"- Creature text that goes after rewarding quest \"{MainForm.GetQuestNameById(rewardPacket.questId)}\" ({rewardPacket.questId}):\r\n";
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
                if (conversationPacket.conversationData.conversationActors.Count == 0)
                    continue;

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
            string output = "";
            List<KeyValuePair<string, double>> estimatedDistances = new List<KeyValuePair<string, double>>();
            bool thereWasActorWithWaypoints = false;

            if (conversationPacket.conversationData.conversationActors.Count == 0)
            {
                output = "estimated distance to trigger is unknown, because conversation doesn't have any creature actor";
                return output;
            }

            for (int i = 0; i < conversationPacket.conversationData.conversationActors.Count; i++)
            {
                Creature actor;

                if (creatures.ContainsKey(conversationPacket.conversationData.conversationActors[i].Key))
                {
                    actor = creatures[conversationPacket.conversationData.conversationActors[i].Key];
                }
                else
                    continue;

                if (!actor.HasWaypoints())
                {
                    estimatedDistances.Add(new KeyValuePair<string, double>(actor.name, Math.Round(movePacket.position.GetDistance(actor.spawnPosition))));
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
                        estimatedDistances.Add(new KeyValuePair<string, double>(actor.name, Math.Round(movePacket.position.GetDistance(lastWaypoint.movePosition))));
                    }
                    else if (lastWaypoint.startPosition.IsValid())
                    {
                        estimatedDistances.Add(new KeyValuePair<string, double>(actor.name, Math.Round(movePacket.position.GetDistance(lastWaypoint.startPosition))));
                    }
                }
            }

            if (estimatedDistances.Count > 1)
            {
                output = "estimated distances to trigger position: ";

                for (int i = 0; i < estimatedDistances.Count; i++)
                {
                    if (i + 1 < estimatedDistances.Count)
                    {
                        output += $"{estimatedDistances[i].Value}.0f ({estimatedDistances[i].Key}), ";
                    }
                    else
                    {
                        output += $"{estimatedDistances[i].Value}.0f ({estimatedDistances[i].Key})";
                    }
                }
            }
            else
            {
                output = $"estimated distance to trigger position: {estimatedDistances.FirstOrDefault().Value}.0f, ({estimatedDistances.FirstOrDefault().Key})";
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

                if (DB2.Db2.ConversationLine.ContainsKey(conversationLineId))
                {
                    DataSet broadcastTextDs = SQLModule.HotfixSelectQuery($"SELECT `Text`, `Text1` FROM `broadcasttext` WHERE `ROW_ID` = {DB2.Db2.ConversationLine[conversationLineId].BroadcastTextID}");
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
                    try
                    {
                        actor = creatures[conversationPacket.conversationData.conversationActors.FirstOrDefault(x => x.Value == conversationPacket.conversationData.conversationLines[i].Value).Key];
                    }
                    catch (System.Exception) { }

                    if (actor != null)
                    {
                        if (conversationLineText == "")
                        {
                            conversationLineText = "There is no text for this line";
                        }

                        output += $"[{i}] Actor: \"{actor.name}\" ({actor.entry}) - Line: \"{conversationLineText}\" ({conversationLineId})\r\n";
                    }
                    else
                    {
                        if (conversationLineText == "")
                        {
                            conversationLineText = "There is no text for this line";
                            output += $"[{i}] Actor: Is Player - Line: \"{conversationLineText}\" ({conversationLineId})\r\n";
                        }
                        else if (conversationLineText != "")
                        {
                            output += $"[{i}] Actor: Is Non WorldObject - Line: \"{conversationLineText}\" ({conversationLineId})\r\n";
                        }
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

        public void GetEquipmentIdForCreature()
        {
            string output = "";

            Dictionary<uint, List<KeyValuePair<byte, List<uint>>>> equipTemplates = new Dictionary<uint, List<KeyValuePair<byte, List<uint>>>>();

            DataSet creatureEquipTemplateDs = SQLModule.WorldSelectQuery($"SELECT `CreatureID`, `ID`, `ItemID1`, `ItemID2`, `ItemID3` FROM `creature_equip_template` WHERE `CreatureID` IN ({creatures.GetCreatureEntries()});");
            if (creatureEquipTemplateDs != null && creatureEquipTemplateDs.Tables["table"].Rows.Count != 0)
            {
                foreach (DataRow row in creatureEquipTemplateDs.Tables["table"].Rows)
                {
                    uint creatureId = Convert.ToUInt32(row[0]);
                    List<uint> itemsIds = new List<uint>();

                    for (int i = 2; i < 5; i++)
                    {
                        itemsIds.Add(Convert.ToUInt32(row[i]));
                    }

                    if (!equipTemplates.ContainsKey(creatureId))
                    {
                        equipTemplates.Add(creatureId, new List<KeyValuePair<byte, List<uint>>>() { new KeyValuePair<byte, List<uint>>(Convert.ToByte(row[1]), itemsIds) });
                    }
                    else
                    {
                        equipTemplates[creatureId].Add(new KeyValuePair<byte, List<uint>>(Convert.ToByte(row[1]), itemsIds));
                    }
                }
            }

            creatureEquipTemplateDs.Clear();

            if (mainForm.textBox_ParsedFileAdvisor_CreatureEquipmentId.Text == "")
            {
                foreach (Creature creature in creatures.Values)
                {
                    if (!equipTemplates.ContainsKey(creature.entry) || !IsCreatureExistOnDb(creature.guid) || equipTemplates[creature.entry].Count == 1)
                        continue;

                    foreach (var equip in equipTemplates[creature.entry])
                    {
                        bool equipFound = true;

                        for (int i = 0; i < 3; i++)
                        {
                            if (equip.Value[i] != creature.virtualItems[i])
                            {
                                equipFound = false;
                                break;
                            }
                        }

                        if (equipFound)
                        {
                            output += $"UPDATE `creature` SET `equipment_id` = {equip.Key} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                        }
                    }
                }
            }
            else
            {
                uint creatureEntry = Convert.ToUInt32(mainForm.textBox_ParsedFileAdvisor_CreatureEquipmentId.Text);

                if (creatures.Values.Count(x => x.entry == creatureEntry) == 0)
                {
                    mainForm.textBox_ParsedFileAdvisor_Output.Text = $"There is no creatures in sniff with entry {creatureEntry}!";
                    return;
                }

                if (!equipTemplates.ContainsKey(creatureEntry))
                {
                    mainForm.textBox_ParsedFileAdvisor_Output.Text = $"Creature with entry {creatureEntry} doesn't have any equip template!";
                    return;
                }

                List<KeyValuePair<byte, List<uint>>> equipTemplatesByEntry = equipTemplates[creatureEntry];
                if (equipTemplatesByEntry.Count == 1)
                {
                    mainForm.textBox_ParsedFileAdvisor_Output.Text = $"Creature with entry {creatureEntry} has only 1 equip template!";
                }
                else
                {
                    foreach (Creature creature in creatures.Values.Where(x => x.entry == creatureEntry))
                    {
                        foreach (var equip in equipTemplatesByEntry)
                        {
                            bool equipFound = true;

                            for (int i = 0; i < 3; i++)
                            {
                                if (equip.Value[i] != creature.virtualItems[i])
                                {
                                    equipFound = false;
                                    break;
                                }
                            }

                            if (equipFound)
                            {
                                output += $"UPDATE `creature` SET `equipment_id` = {equip.Key} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                            }
                        }
                    }
                }
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        public bool IsCreatureExistOnDb(string guid)
        {
            string linkedId = creatures[guid].GetLinkedId();

            string creatureQuery = "SELECT `linked_id` FROM `creature` WHERE `linked_id` = '" + linkedId + "';";
            var creatureDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(creatureQuery) : null;

            if (creatureDs != null && creatureDs.Tables["table"].Rows.Count > 0)
                return true;

            return false;
        }

        public void GetPlayerCompletedQuests()
        {
            if (mainForm.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Text == "" || !playerCompletedQuestPackets.ContainsKey(mainForm.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Text))
                return;

            string output = "";
            List<UpdateObjectPacket> updateObjectPackets = playerCompletedQuestPackets[mainForm.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Text].OrderBy(x => x.packetSendTime).ToList();
            Dictionary<TimeSpan, List<int>> convertedQuestData = new Dictionary<TimeSpan, List<int>>();

            foreach (UpdateObjectPacket packet in updateObjectPackets)
            {
                foreach (QuestCompletedData questData in packet.questCompletedData)
                {
                    List<int> questBits = new List<int>();

                    int l_FieldOffset = (questData.Index << 6) + 1;

                    for (int i = 1; i < 65; i++)
                    {
                        if ((questData.Flags & ((ulong)1 << (i - 1))) != 0)
                        {
                            questBits.Add(l_FieldOffset);
                        }

                        l_FieldOffset += 1;
                    }

                    foreach (var questBit in questBits)
                    {
                        if (!DB2.Db2.QuestBitsStore.ContainsKey(questBit))
                            continue;

                        if (!convertedQuestData.ContainsKey(packet.packetSendTime))
                        {
                            convertedQuestData.Add(packet.packetSendTime, new List<int> { DB2.Db2.QuestBitsStore[questBit] });
                        }
                        else
                        {
                            convertedQuestData[packet.packetSendTime].Add(DB2.Db2.QuestBitsStore[questBit]);
                        }
                    }
                }
            }

            for (int i = 1; i < convertedQuestData.Count; i++)
            {
                var exceptedQuests = convertedQuestData.ElementAt(i).Value.Except(convertedQuestData.ElementAt(i - 1).Value).ToList();

                if (exceptedQuests.Count != 0)
                {
                    output += $"Player has new completed quests after {convertedQuestData.ElementAt(i).Key.ToFormattedString()}:\r\n";

                    foreach (uint questId in exceptedQuests)
                    {
                        string questName = MainForm.GetQuestNameById(questId);
                        if (questName == "Unknown")
                        {
                            questName = "Server Side Quest";
                        }

                        output += $"QuestId: {questId} ({questName})\r\n";
                    }
                }
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        public void ShowWorldStates()
        {

        }

        public void ParseQuestgiverData()
        {
            if (mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Text == "")
                return;

            string output = "";

            HtmlDocument html = LoadHtml(mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Text);
            string name = html.DocumentNode.SelectSingleNode("//div[@class=\"text\"]//h1").InnerText;
            string questId = mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Text;
            string[] questRawData = html.DocumentNode.SelectSingleNode("//table[@class=\"infobox\"]").SelectSingleNode("//script[text()[contains(., 'Requires')]]").InnerText.Split(' ');

            List<string> questStarterIds = new List<string>();

            for (int i = 0; i < questRawData.Length; i++)
            {
                if (questRawData[i].Contains("Start"))
                {
                    string questStarterRow = "";

                    do
                    {
                        questStarterRow += questRawData[i] + " ";
                        i++;
                    } while (i < questRawData.Length - 1 && !questRawData[i].Contains("Start") && !questRawData[i].Contains("End"));

                    i--;

                    questStarterIds.Add(new Regex(@"\d+").Match(new Regex(@"Start:{1}.*npc={1}\d+").Match(questStarterRow).Value).Value);
                }
            }

            List<string> questEnderIds = new List<string>();

            for (int i = 0; i < questRawData.Length; i++)
            {
                if (questRawData[i].Contains("End"))
                {
                    string questEnderRow = "";

                    do
                    {
                        questEnderRow += questRawData[i] + " ";
                        i++;
                    } while (i < questRawData.Length - 1 && !questRawData[i].Contains("End"));

                    i--;

                    questEnderIds.Add(new Regex(@"\d+").Match(new Regex(@"End:{1}.*npc={1}\d+").Match(questEnderRow).Value).Value);
                }
            }

            output = $"-- {name} https://www.wowhead.com/quest={questId}\r\n";

            if (questStarterIds.Count > 1)
            {
                output += $"DELETE FROM `creature_queststarter` WHERE `id` IN (";

                for (int i = 0; i < questStarterIds.Count; i++)
                {
                    if (i + 1 < questStarterIds.Count)
                    {
                        output += questStarterIds[i] + ", ";
                    }
                    else
                    {
                        output += questStarterIds[i] + $") AND `quest` = {questId};\r\n";
                    }
                }

                output += "INSERT INTO `creature_queststarter` (`id`, `quest`) VALUES\r\n";

                for (int i = 0; i < questStarterIds.Count; i++)
                {
                    if (i + 1 < questStarterIds.Count)
                    {
                        output += $"({questStarterIds[i]}, {questId}),\r\n";
                    }
                    else
                    {
                        output += $"({questStarterIds[i]}, {questId});\r\n\r\n";
                    }
                }
            }
            else if (questStarterIds.Count == 1)
            {
                output += $"DELETE FROM `creature_queststarter` WHERE `id` = {questStarterIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                output += "INSERT INTO `creature_queststarter` (`id`, `quest`) VALUES\r\n";
                output += $"({questStarterIds.FirstOrDefault()}, {questId});\r\n\r\n";
            }

            if (questEnderIds.Count > 1)
            {
                output += $"DELETE FROM `creature_questender` WHERE `id` IN (";

                for (int i = 0; i < questEnderIds.Count; i++)
                {
                    if (i + 1 < questEnderIds.Count)
                    {
                        output += questEnderIds[i] + ", ";
                    }
                    else
                    {
                        output += questEnderIds[i] + $") AND `quest` = {questId};\r\n";
                    }
                }

                output += "INSERT INTO `creature_questender` (`id`, `quest`) VALUES\r\n";

                for (int i = 0; i < questEnderIds.Count; i++)
                {
                    if (i + 1 < questEnderIds.Count)
                    {
                        output += $"({questEnderIds[i]}, {questId}),\r\n";
                    }
                    else
                    {
                        output += $"({questEnderIds[i]}, {questId});\r\n\r\n";
                    }
                }
            }
            else if (questEnderIds.Count == 1)
            {
                output += $"DELETE FROM `creature_questender` WHERE `id` = {questEnderIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                output += "INSERT INTO `creature_questender` (`id`, `quest`) VALUES\r\n";
                output += $"({questEnderIds.FirstOrDefault()}, {questId});\r\n\r\n";
            }

            output += $"DELETE FROM `quest_template_addon` WHERE `ID` = {questId};\r\n";
            output += $"INSERT INTO `quest_template_addon` (`ID`, `PrevQuestId`, `NextQuestId`, `ExclusiveGroup`, `AllowableClasses`, `AllowableRaces`, `SourceSpellId`, `RequiredSkillId`, `RequiredSkillPoints`, `RequiredMinRepFaction`, `RequiredMaxRepFaction`, `RequiredMinRepValue`, `RequiredMaxRepValue`, `RewardMailTemplateId`, `RewardMailDelay`, `SpecialFlags`, `ResetType`, `OverrideFlags`, `OverrideFlagsEx`, `OverrideFlagsEx2`, `InProgressPhaseId`, `CompletedPhaseId`, `StartScript`, `CompleteScript`, `ScriptName`, `FromPatch`) VALUES\r\n";
            output += $"({questId}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0);";
            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        private HtmlDocument LoadHtml(string questId)
        {
            HtmlWeb web = new HtmlWeb();
            string url = $"https://www.wowhead.com/quest={questId}";
            HtmlDocument doc = web.Load(url);
            return doc;
        }
    }
}
