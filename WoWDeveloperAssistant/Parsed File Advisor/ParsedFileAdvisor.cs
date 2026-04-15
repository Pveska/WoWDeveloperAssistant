using HtmlAgilityPack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Packets.UpdateObjectPacket;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Parsed_File_Advisor
{
    public class ParsedFileAdvisor
    {
        private readonly MainForm mainForm;
        public static Dictionary<string, Creature> creatures = new Dictionary<string, Creature>();
        public List<UpdateObjectPacket> conversationPackets = new List<UpdateObjectPacket>();
        public Dictionary<uint, List<CreatureText>> creatureTexts = new Dictionary<uint, List<CreatureText>>();
        public List<SpellPacket> spellPackets = new List<SpellPacket>();
        public List<QuestGiverAcceptQuestPacket> questAcceptPackets = new List<QuestGiverAcceptQuestPacket>();
        public List<QuestUpdateCompletePacket> questCompletePackets = new List<QuestUpdateCompletePacket>();
        public List<QuestGiverQuestCompletePacket> questRewardPackets = new List<QuestGiverQuestCompletePacket>();
        public List<PlayerMovePacket> playerMovePackets = new List<PlayerMovePacket>();
        public List<QuestUpdateAddCreditPacket> addCreditPackets = new List<QuestUpdateAddCreditPacket>();
        public Dictionary<string, List<UpdateObjectPacket>> playerCompletedQuestPackets = new Dictionary<string, List<UpdateObjectPacket>>();
        public List<EmotePacket> emotePackets = new List<EmotePacket>();
        public Dictionary<string, List<UpdateObjectPacket>> creatureDestroyPackets = new Dictionary<string, List<UpdateObjectPacket>>();
        public Dictionary<string, List<UpdateObjectPacket>> emoteStatesPackets = new Dictionary<string, List<UpdateObjectPacket>>();
        public Dictionary<string, List<UpdateObjectPacket>> standStatesPackets = new Dictionary<string, List<UpdateObjectPacket>>();

        private void ClearContainers()
        {
            creatures.Clear();
            conversationPackets.Clear();
            creatureTexts.Clear();
            spellPackets.Clear();
            questAcceptPackets.Clear();
            questCompletePackets.Clear();
            questRewardPackets.Clear();
            playerMovePackets.Clear();
            addCreditPackets.Clear();
            playerCompletedQuestPackets.Clear();
            emotePackets.Clear();
            creatureDestroyPackets.Clear();
            emoteStatesPackets.Clear();
            standStatesPackets.Clear();
        }

        public ParsedFileAdvisor(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        [ProtoContract]
        public class ParsedFileData
        {
            [ProtoMember(1)]
            public Dictionary<string, Creature> Creatures
            {
                get; set;
            } = new Dictionary<string, Creature>();

            [ProtoMember(2)]
            public List<UpdateObjectPacket> ConversationPackets
            {
                get; set;
            } = new List<UpdateObjectPacket>();

            [ProtoMember(3)]
            public Dictionary<uint, List<CreatureText>> CreatureTexts
            {
                get; set;
            } = new Dictionary<uint, List<CreatureText>>();

            [ProtoMember(4)]
            public List<SpellPacket> SpellPackets
            {
                get; set;
            } = new List<SpellPacket>();

            [ProtoMember(5)]
            public List<QuestGiverAcceptQuestPacket> QuestAcceptPackets
            {
                get; set;
            } = new List<QuestGiverAcceptQuestPacket>();

            [ProtoMember(6)]
            public List<QuestUpdateCompletePacket> QuestCompletePackets
            {
                get; set;
            } = new List<QuestUpdateCompletePacket>();

            [ProtoMember(7)]
            public List<QuestGiverQuestCompletePacket> QuestRewardPackets
            {
                get; set;
            } = new List<QuestGiverQuestCompletePacket>();

            [ProtoMember(8)]
            public List<PlayerMovePacket> PlayerMovePackets
            {
                get; set;
            } = new List<PlayerMovePacket>();

            [ProtoMember(9)]
            public List<QuestUpdateAddCreditPacket> AddCreditPackets
            {
                get; set;
            } = new List<QuestUpdateAddCreditPacket>();

            [ProtoMember(10)]
            public Dictionary<string, List<UpdateObjectPacket>> PlayerCompletedQuestPackets
            {
                get; set;
            } = new Dictionary<string, List<UpdateObjectPacket>>();

            [ProtoMember(11)]
            public List<EmotePacket> EmotePackets
            {
                get; set;
            } = new List<EmotePacket>();

            [ProtoMember(12)]
            public Dictionary<string, List<UpdateObjectPacket>> CreatureDestroyPackets
            {
                get; set;
            } = new Dictionary<string, List<UpdateObjectPacket>>();

            [ProtoMember(13)]
            public Dictionary<string, List<UpdateObjectPacket>> EmoteStatePackets
            {
                get; set;
            } = new Dictionary<string, List<UpdateObjectPacket>>();

            [ProtoMember(14)]
            public Dictionary<string, List<UpdateObjectPacket>> StandStatePackets
            {
                get; set;
            } = new Dictionary<string, List<UpdateObjectPacket>>();
        }

        public bool GetDataFromFiles(string fileName)
        {
            if (fileName.Contains("txt") && GetDataFromTxtFile(fileName))
                return true;
            else if (fileName.Contains("proto") && GetDataFromBinFile(fileName))
                return true;

            return false;
        }

        public bool GetDataFromTxtFile(string fileName)
        {
            ClearContainers();
            var lines = File.ReadAllLines(fileName);
            Dictionary<long, PacketType> packetIndexes = new Dictionary<long, PacketType>();
            BuildVersions buildVersion = LineGetters.GetBuildVersion(lines);

            if (!IsTxtFileValidForParse(fileName, lines, buildVersion))
                return false;

            Parallel.For(0, lines.Length, index =>
            {
                PacketType packetType = Packet.GetPacketTypeFromLine(lines[index]);
                if (packetType != PacketType.UNKNOWN_PACKET && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, packetType);
                }
            });

            foreach (var value in packetIndexes)
            {
                if (value.Value == PacketType.SMSG_UPDATE_OBJECT)
                {
                    Parallel.ForEach(ParseObjectUpdatePacket(lines, value.Key, buildVersion, 0), packet =>
                    {
                        if (packet.objectType == ObjectTypes.Conversation)
                        {
                            lock (conversationPackets)
                            {
                                UpdateObjectPacket existedUpdateObjectPacket = conversationPackets.FirstOrDefault(x => x.entry == packet.entry);
                                if (existedUpdateObjectPacket == null)
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
                        else if (packet.objectType == ObjectTypes.Creature)
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

                            if (packet.updateType == PacketUpdateType.Values && (packet.emoteStateId != null || packet.standState != null))
                            {
                                UpdateObjectPacket updateObjectPacket = new UpdateObjectPacket
                                {
                                    guid = packet.guid,
                                    packetSendTime = packet.packetSendTime,
                                    emoteStateId = packet.emoteStateId,
                                    standState = packet.standState
                                };

                                if (packet.emoteStateId != null)
                                {
                                    lock (emoteStatesPackets)
                                    {
                                        if (!emoteStatesPackets.ContainsKey(packet.guid))
                                        {
                                            emoteStatesPackets.Add(packet.guid, new List<UpdateObjectPacket>() { updateObjectPacket });
                                        }
                                        else
                                        {
                                            emoteStatesPackets[packet.guid].Add(updateObjectPacket);
                                        }
                                    }
                                }

                                if (packet.standState != null)
                                {
                                    lock (standStatesPackets)
                                    {
                                        if (!standStatesPackets.ContainsKey(packet.guid))
                                        {
                                            standStatesPackets.Add(packet.guid, new List<UpdateObjectPacket>() { updateObjectPacket });
                                        }
                                        else
                                        {
                                            standStatesPackets[packet.guid].Add(updateObjectPacket);
                                        }
                                    }
                                }
                            }
                        }

                        if (packet.updateType == PacketUpdateType.Destroy)
                        {
                            lock (creatureDestroyPackets)
                            {
                                foreach (string guid in packet.destroyedObjectGuids)
                                {
                                    UpdateObjectPacket destroyPacket = new UpdateObjectPacket
                                    {
                                        guid = guid,
                                        packetSendTime = packet.packetSendTime
                                    };

                                    if (!creatureDestroyPackets.ContainsKey(guid))
                                    {
                                        creatureDestroyPackets.Add(guid, new List<UpdateObjectPacket>() { destroyPacket });
                                    }
                                    else
                                    {
                                        creatureDestroyPackets[guid].Add(destroyPacket);
                                    }
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
                if (value.Value == PacketType.SMSG_AI_REACTION)
                {
                    AIReactionPacket reactionPacket = AIReactionPacket.ParseAIReactionPacket(lines, value.Key, buildVersion);
                    if (reactionPacket.creatureGuid == "")
                        return;

                    lock (creatures)
                    {
                        if (creatures.ContainsKey(reactionPacket.creatureGuid))
                        {
                            if (creatures[reactionPacket.creatureGuid].combatTimings.Count(x => x.CombatStartTime == reactionPacket.packetSendTime) == 0)
                            {
                                creatures[reactionPacket.creatureGuid].combatTimings.Add(new CombatTimingsData(reactionPacket.packetSendTime, new TimeSpan()));
                            }
                        }
                    }
                }
            });

            Parallel.ForEach(packetIndexes, value =>
            {
                if (value.Value == PacketType.SMSG_ATTACK_STOP)
                {
                    AttackStopPacket attackStopPacket = AttackStopPacket.ParseAttackStopPacket(lines, value.Key, buildVersion);
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
                if (value.Value == PacketType.SMSG_SPELL_START || value.Value == PacketType.SMSG_SPELL_GO)
                {
                    SpellPacket spellPacket = SpellPacket.ParseSpellPacket(lines, value.Key, buildVersion, value.Value, true);
                    if (spellPacket.spellId == 0)
                    {
                        spellPacket = SpellPacket.ParseSpellPacket(lines, value.Key, buildVersion, value.Value, false);
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
                if (value.Value == PacketType.SMSG_CHAT)
                {
                    ChatPacket chatPacket = ChatPacket.ParseChatPacket(lines, value.Key, buildVersion);
                    if (chatPacket.creatureGuid == "")
                        return;

                    CreatureText text = new CreatureText(chatPacket);
                    Creature sender = creatures.FirstOrDefault(x => x.Value.guid == chatPacket.creatureGuid).Value;
                    if (sender == null)
                        return;

                    if (sender.combatTimings.IsCombatTimer(chatPacket.packetSendTime))
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
                    else if ((sender.deathTime - chatPacket.packetSendTime).Duration() <= TimeSpan.FromSeconds(1))
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
                if (value.Value == PacketType.CMSG_QUEST_GIVER_ACCEPT_QUEST)
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
                if (value.Value == PacketType.SMSG_QUEST_GIVER_QUEST_COMPLETE)
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
                if (value.Value == PacketType.SMSG_QUEST_UPDATE_COMPLETE)
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
                if (value.Value == PacketType.SMSG_QUEST_UPDATE_ADD_CREDIT)
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
                if (value.Value == PacketType.SMSG_ON_MONSTER_MOVE)
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
                if (value.Value == PacketType.SMSG_EMOTE)
                {
                    EmotePacket emotePacket = EmotePacket.ParseEmotePacket(lines, value.Key);
                    if (emotePacket.guid == "" || emotePacket.emoteId == 0)
                        return;

                    lock (emotePackets)
                    {
                        emotePackets.Add(emotePacket);
                    }
                }
            });

            if (mainForm.checkBox_ParsedFileAdvisor_CreateDataFile.Checked)
            {
                using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_parsed_file_advisor_packets.proto"), FileMode.OpenOrCreate))
                {
                    ParsedFileData data = new ParsedFileData
                    {
                        Creatures = creatures,
                        ConversationPackets = conversationPackets,
                        CreatureTexts = creatureTexts,
                        SpellPackets = spellPackets,
                        QuestAcceptPackets = questAcceptPackets,
                        QuestCompletePackets = questCompletePackets,
                        QuestRewardPackets = questRewardPackets,
                        PlayerMovePackets = playerMovePackets,
                        AddCreditPackets = addCreditPackets,
                        PlayerCompletedQuestPackets = playerCompletedQuestPackets,
                        EmotePackets = emotePackets,
                        CreatureDestroyPackets = creatureDestroyPackets,
                        EmoteStatePackets = emoteStatesPackets,
                        StandStatePackets = standStatesPackets
                    };

                    Serializer.Serialize(fileStream, data);
                }
            }

            return true;
        }

        public bool GetDataFromBinFile(string fileName)
        {
            ClearContainers();

            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "Current status: Getting packets from data file...";

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                ParsedFileData data = Serializer.Deserialize<ParsedFileData>(fileStream);
                creatures = data.Creatures;
                conversationPackets = data.ConversationPackets;
                creatureTexts = data.CreatureTexts;
                spellPackets = data.SpellPackets;
                questAcceptPackets = data.QuestAcceptPackets;
                questCompletePackets = data.QuestCompletePackets;
                questRewardPackets = data.QuestRewardPackets;
                playerMovePackets = data.PlayerMovePackets;
                addCreditPackets = data.AddCreditPackets;
                playerCompletedQuestPackets = data.PlayerCompletedQuestPackets;
                emotePackets = data.EmotePackets;
                creatureDestroyPackets = data.CreatureDestroyPackets;
                emoteStatesPackets = data.EmoteStatePackets;
                standStatesPackets = data.StandStatePackets;
            }

            return true;
        }

        public void OpenFileDialog()
        {
            mainForm.openFileDialog.Title = "Open File";
            mainForm.openFileDialog.Filter = "Parsed sniff or data file (*.txt;*.proto)|*parsed.txt;*parsed_file_advisor_packets.proto";
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
            mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_ParseRolePlayEvents.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_FindDoubleSpawns.Enabled = true;
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
            List<KeyValuePair<SpellPacket, uint>> spellPacketsListPair = new List<KeyValuePair<SpellPacket, uint>>();

            Parallel.ForEach(spellPackets, spellPacket =>
            {
                if (spellPacket.casterGuid != playerGuid)
                    return;

                lock (spellPackets)
                {
                    int spellIndex = spellPacketsListPair.FindIndex(x => x.Key.spellId == spellPacket.spellId);

                    if (spellIndex == -1)
                    {
                        spellPacketsListPair.Add(new KeyValuePair<SpellPacket, uint>(spellPacket, 1));
                    }
                    else if (spellPacketsListPair[spellIndex].Key.type == PacketType.SMSG_SPELL_GO && spellPacket.type == PacketType.SMSG_SPELL_START)
                    {
                        spellPacketsListPair.Add(new KeyValuePair<SpellPacket, uint>(spellPacket, 1));
                        spellPacketsListPair.RemoveAt(spellIndex);
                    }
                    else if ((spellPacketsListPair[spellIndex].Key.type == PacketType.SMSG_SPELL_START && spellPacket.type == PacketType.SMSG_SPELL_START) ||
                        (spellPacketsListPair[spellIndex].Key.type == PacketType.SMSG_SPELL_GO && spellPacket.type == PacketType.SMSG_SPELL_GO))
                    {

                        if (spellPacketsListPair[spellIndex].Key.spellCastStartTime >= spellPacket.spellCastStartTime)
                        {
                            spellPacketsListPair.Add(new KeyValuePair<SpellPacket, uint>(spellPacket, spellPacketsListPair[spellIndex].Value + 1));
                            spellPacketsListPair.RemoveAt(spellIndex);
                        }
                        else
                        {
                            spellPacketsListPair.Add(new KeyValuePair<SpellPacket, uint>(spellPacketsListPair[spellIndex].Key, spellPacketsListPair[spellIndex].Value + 1));
                            spellPacketsListPair.RemoveAt(spellIndex);
                        }
                    }
                }
            });

            spellPacketsListPair = spellPacketsListPair.Where(x => x.Key.casterGuid == playerGuid).OrderBy(x => x.Key.spellCastStartTime).ToList();

            output += "Spells casted by player with guid \"" + playerGuid + "\"" + "\r\n";

            foreach (var spell in spellPacketsListPair)
            {
                output += "Spell Id: " + spell.Key.spellId + " (" + GetSpellName(spell.Key.spellId) + "), Cast Time: " + spell.Key.spellCastStartTime.ToFormattedStringWithMilliseconds() + ", Casted times: " + spell.Value + "\r\n";
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

                if (creatures.ContainsKey(conversationPacket.conversationData.conversationActors[i].ActorGuid))
                {
                    actor = creatures[conversationPacket.conversationData.conversationActors[i].ActorGuid];
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
                int conversationLineId = (int)conversationPacket.conversationData.conversationLines[i].ConversationLineId;
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
                        actor = creatures[conversationPacket.conversationData.conversationActors.FirstOrDefault(x => x.ActorIndex == conversationPacket.conversationData.conversationLines[i].ActorIndex).ActorGuid];
                    }
                    catch (Exception) { }

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

        public void UpdateEquipmentForCreatures()
        {
            string output = "";

            Dictionary<uint, List<KeyValuePair<byte, List<VirtualItemData>>>> equipTemplates = new Dictionary<uint, List<KeyValuePair<byte, List<VirtualItemData>>>>();

            DataSet creatureEquipTemplateDs = SQLModule.WorldSelectQuery($"SELECT `CreatureID`, `ID`, `ItemID1`, `ItemID2`, `ItemID3` FROM `creature_equip_template` WHERE `CreatureID` IN ({creatures.GetCreatureEntries()});");
            if (creatureEquipTemplateDs != null && creatureEquipTemplateDs.Tables["table"].Rows.Count != 0)
            {
                foreach (DataRow row in creatureEquipTemplateDs.Tables["table"].Rows)
                {
                    uint creatureId = Convert.ToUInt32(row[0]);
                    List<VirtualItemData> items = new List<VirtualItemData>();

                    for (int i = 2; i < 5; i++)
                    {
                        items.Add(new VirtualItemData(Convert.ToUInt32(row[i]), (uint?)(i - 2)));
                    }

                    if (!equipTemplates.ContainsKey(creatureId))
                    {
                        equipTemplates.Add(creatureId, new List<KeyValuePair<byte, List<VirtualItemData>>>() { new KeyValuePair<byte, List<VirtualItemData>>(Convert.ToByte(row[1]), items) });
                    }
                    else
                    {
                        equipTemplates[creatureId].Add(new KeyValuePair<byte, List<VirtualItemData>>(Convert.ToByte(row[1]), items));
                    }
                }
            }

            creatureEquipTemplateDs.Clear();

            if (mainForm.textBox_ParsedFileAdvisor_CreatureEquipmentId.Text == "")
            {
                foreach (Creature creature in creatures.Values)
                {
                    if (!equipTemplates.ContainsKey(creature.entry) || !IsCreatureExistOnDb(creature.guid))
                        continue;

                    var equipTemplate = equipTemplates[creature.entry];

                    if (creature.virtualItems.Count(x => x.ItemId > 0) == 0 && equipTemplate.Any(x => x.Value.Any(v => v.ItemId != 0)))
                    {
                        output += $"UPDATE `creature` SET `equipment_id` = -1 WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                    }
                    else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count == 1)
                    {
                        output += $"UPDATE `creature` SET `equipment_id` = {equipTemplate.First().Key} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                    }
                    else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count > 1)
                    {
                        byte? foundKey = null;

                        foreach (var template in equipTemplate)
                        {
                            if (template.Value.Count != creature.virtualItems.Count)
                                continue;

                            bool match = template.Value
                                .Zip(creature.virtualItems, (t, c) =>
                                    t.ItemId == c.ItemId &&
                                    t.ItemIdx == c.ItemIdx)
                                .All(x => x);

                            if (match)
                            {
                                foundKey = template.Key;
                                break;
                            }
                        }

                        if (foundKey != null)
                        {
                            output += $"UPDATE `creature` SET `equipment_id` = {foundKey} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                        }
                    }
                    else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count == 0)
                    {
                        output += $"-- Creature with LinkedId: {creature.GetLinkedId()} (Name: {creature.name}) has items in sniff but not in DB!\r\n";
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

                var equipTemplate = equipTemplates[creatureEntry];

                foreach (Creature creature in creatures.Values.Where(x => x.entry == creatureEntry))
                {
                    foreach (var equip in equipTemplate)
                    {
                        if (creature.virtualItems.Count(x => x.ItemId > 0) == 0 && equipTemplate.Any(x => x.Value.Any(v => v.ItemId != 0)))
                        {
                            output += $"UPDATE `creature` SET `equipment_id` = -1 WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                        }
                        else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count == 1)
                        {
                            output += $"UPDATE `creature` SET `equipment_id` = {equipTemplate.First().Key} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                        }
                        else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count > 1)
                        {
                            byte? foundKey = null;

                            foreach (var template in equipTemplate)
                            {
                                if (template.Value.Count != creature.virtualItems.Count)
                                    continue;

                                bool match = template.Value
                                    .Zip(creature.virtualItems, (t, c) =>
                                        t.ItemId == c.ItemId &&
                                        t.ItemIdx == c.ItemIdx)
                                    .All(x => x);

                                if (match)
                                {
                                    foundKey = template.Key;
                                    break;
                                }
                            }

                            if (foundKey != null)
                            {
                                output += $"UPDATE `creature` SET `equipment_id` = {foundKey} WHERE `linked_id` = '{creature.GetLinkedId()}'; -- {creature.name}\r\n";
                            }
                        }
                        else if (creature.virtualItems.Count(x => x.ItemId > 0) != 0 && equipTemplate.Count == 0)
                        {
                            output += $"-- Creature with LinkedId: {creature.GetLinkedId()} (Name: {creature.name}) has items in sniff but not in DB!\r\n";
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

        public void ParseQuestgiverData()
        {
            if (mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Text == "")
                return;

            mainForm.textBox_ParsedFileAdvisor_Output.Text = "";
            string output = "";
            string[] questIds = mainForm.textBox_ParsedFileAdvisor_ParseQuestgiverData.Text.Split(',');

            foreach (string questId in questIds)
            {
                HtmlDocument html = LoadHtml(questId);
                string name = html.DocumentNode.SelectSingleNode("//div[@class=\"text\"]//h1").InnerText;
                string[] questRawData = html.DocumentNode.SelectSingleNode("//table[@class=\"infobox\"]").SelectSingleNode("//script[text()[contains(., 'Requires')]]").InnerText.Split(' ');
                bool questStarterIsGameObject = false;

                List<string> questStarterIds = new List<string>();

                for (int i = 0; i < questRawData.Length; i++)
                {
                    if (questRawData[i].Contains("Start:"))
                    {
                        string questStarterRow = "";

                        do
                        {
                            questStarterRow += questRawData[i] + " ";
                            i++;
                        } while (i < questRawData.Length - 1 && !questRawData[i].Contains("Start") && !questRawData[i].Contains("End"));

                        i--;

                        if (questStarterRow.Contains("npc"))
                        {
                            questStarterIds.Add(new Regex(@"\d+").Match(new Regex(@"Start:{1}.*npc={1}\d+").Match(questStarterRow).Value).Value);
                        }
                        else if (questStarterRow.Contains("object"))
                        {
                            questStarterIsGameObject = true;
                            questStarterIds.Add(new Regex(@"\d+").Match(new Regex(@"Start:{1}.*object={1}\d+").Match(questStarterRow).Value).Value);
                        }
                    }
                }

                List<string> questEnderIds = new List<string>();

                for (int i = 0; i < questRawData.Length; i++)
                {
                    if (questRawData[i].Contains("End:"))
                    {
                        string questEnderRow = "";

                        do
                        {
                            questEnderRow += questRawData[i] + " ";
                            i++;
                        } while (i < questRawData.Length - 1 && !questRawData[i].Contains("End"));

                        i--;

                        if (questEnderRow.Contains("npc"))
                        {
                            questEnderIds.Add(new Regex(@"\d+").Match(new Regex(@"End:{1}.*npc={1}\d+").Match(questEnderRow).Value).Value);
                        }
                        else if (questEnderRow.Contains("object"))
                        {
                            questStarterIsGameObject = true;
                            questEnderIds.Add(new Regex(@"\d+").Match(new Regex(@"End:{1}.*object={1}\d+").Match(questEnderRow).Value).Value);
                        }
                    }
                }

                output += $"-- {name} https://www.wowhead.com/quest={questId}\r\n";

                if (questStarterIds.Count > 1)
                {
                    if (questStarterIsGameObject)
                    {
                        output += $"DELETE FROM `gameobject_queststarter` WHERE `id` IN (";
                    }
                    else
                    {
                        output += $"DELETE FROM `creature_queststarter` WHERE `id` IN (";
                    }

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

                    if (questStarterIsGameObject)
                    {
                        output += "INSERT INTO `gameobject_queststarter` (`id`, `quest`) VALUES\r\n";
                    }
                    else
                    {
                        output += "INSERT INTO `creature_queststarter` (`id`, `quest`) VALUES\r\n";
                    }

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
                    if (questStarterIsGameObject)
                    {
                        output += $"DELETE FROM `gameobject_queststarter` WHERE `id` = {questStarterIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                        output += "INSERT INTO `gameobject_queststarter` (`id`, `quest`) VALUES\r\n";
                        output += $"({questStarterIds.FirstOrDefault()}, {questId});\r\n\r\n";
                    }
                    else
                    {
                        output += $"DELETE FROM `creature_queststarter` WHERE `id` = {questStarterIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                        output += "INSERT INTO `creature_queststarter` (`id`, `quest`) VALUES\r\n";
                        output += $"({questStarterIds.FirstOrDefault()}, {questId});\r\n\r\n";
                    }
                }

                if (questEnderIds.Count > 1)
                {
                    if (questStarterIsGameObject)
                    {
                        output += $"DELETE FROM `gameobject_questender` WHERE `id` IN (";
                    }
                    else
                    {
                        output += $"DELETE FROM `creature_questender` WHERE `id` IN (";
                    }

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

                    if (questStarterIsGameObject)
                    {
                        output += "INSERT INTO `gameobject_questender` (`id`, `quest`) VALUES\r\n";
                    }
                    else
                    {
                        output += "INSERT INTO `creature_questender` (`id`, `quest`) VALUES\r\n";
                    }

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
                    if (questStarterIsGameObject)
                    {
                        output += $"DELETE FROM `gameobject_questender` WHERE `id` = {questEnderIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                        output += "INSERT INTO `gameobject_questender` (`id`, `quest`) VALUES\r\n";
                        output += $"({questEnderIds.FirstOrDefault()}, {questId});\r\n\r\n";
                    }
                    else
                    {
                        output += $"DELETE FROM `creature_questender` WHERE `id` = {questEnderIds.FirstOrDefault()} AND `quest` = {questId};\r\n";
                        output += "INSERT INTO `creature_questender` (`id`, `quest`) VALUES\r\n";
                        output += $"({questEnderIds.FirstOrDefault()}, {questId});\r\n\r\n";
                    }
                }

                output += $"DELETE FROM `quest_template_addon` WHERE `ID` = {questId};\r\n";
                output += $"INSERT INTO `quest_template_addon` (`ID`, `PrevQuestId`, `NextQuestId`, `ExclusiveGroup`, `AllowableClasses`, `AllowableRaces`, `SourceSpellId`, `RequiredSkillId`, `RequiredSkillPoints`, `RequiredMinRepFaction`, `RequiredMaxRepFaction`, `RequiredMinRepValue`, `RequiredMaxRepValue`, `RewardMailTemplateId`, `RewardMailDelay`, `SpecialFlags`, `ResetType`, `OverrideFlags`, `OverrideFlagsEx`, `OverrideFlagsEx2`, `InProgressPhaseId`, `CompletedPhaseId`, `StartScript`, `CompleteScript`, `ScriptName`, `FromPatch`) VALUES\r\n";
                output += $"({questId}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0);";
                output += "\r\n\r\n";
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }

        private HtmlDocument LoadHtml(string questId)
        {
            HtmlWeb web = new HtmlWeb();
            string url = $"https://www.wowhead.com/quest={questId}";
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        private class RolePlayEvent
        {
            public uint? EmoteId;
            public uint? EmoteStateId;
            public uint? StandStateId;
            public uint? SpellId;
            public TimeSpan Time;
        }

        string GetEventKey(RolePlayEvent e)
        {
            string emote = e.EmoteId.HasValue ? e.EmoteId.Value.ToString() : "";
            string emoteState = e.EmoteStateId.HasValue ? e.EmoteStateId.Value.ToString() : "";
            string standState = e.StandStateId.HasValue ? e.StandStateId.Value.ToString() : "";
            string spell = e.SpellId.HasValue ? e.SpellId.Value.ToString() : "";

            List<string> parts = new List<string>();

            if (!string.IsNullOrEmpty(emote))
                parts.Add($"E:{emote}");

            if (!string.IsNullOrEmpty(emoteState))
                parts.Add($"S:{emoteState}");

            if (!string.IsNullOrEmpty(standState))
                parts.Add($"St:{standState}");

            if (!string.IsNullOrEmpty(spell))
                parts.Add($"Sp:{spell}");

            if (parts.Count == 0)
                return "EMPTY";

            return string.Join("|", parts);
        }

        public void ParseRolePlayEvents()
        {
            string mainOutput = "";
            string debugOutput = "";
            string linkedIdOrGuid = mainForm.textBox_ParsedFileAdvisor_ParseRolePlayEvents.Text;

            if (linkedIdOrGuid == "")
            {
                foreach (Creature creature in creatures.Values)
                {
                    if (!IsCreatureExistOnDb(creature.guid) || IsCreatureHasPathOrFormationOnDb(creature.linkedId) || IsCreatureHasRolePlayEvents(creature.linkedId) || IsCreatureHasConversationAurasInAddon(creature.linkedId))
                        continue;

                    List<RolePlayEvent> eventsList = new List<RolePlayEvent>();

                    string fakeDebug = "";
                    GetEventsForCreature(creature, ref eventsList, ref fakeDebug);
                    RemoveInvalidEvents(creature, ref eventsList, ref fakeDebug);
                    MergeClosestEvents(ref eventsList, ref fakeDebug);
                    string linked = creature.linkedId;

                    if (eventsList.Count == 0)
                        continue;

                    var pattern = GetPatternFromEvents(creature, eventsList);

                    if (pattern != "NO_PATTERN")
                    {
                        var parsed = ParsePattern(pattern);
                        var timings = CollectTimings(eventsList, parsed, creature);

                        if (timings.All(x => x.Count > 0))
                        {
                            if (mainOutput == "")
                            {
                                mainOutput += ("INSERT INTO `creature_role_play_events` (`LinkedId`, `Idx`, `EmoteIds`, `EmoteStateIds`, `StandStateIds`, `SpellIds`, `SpellTargetSearchRange`, `SpellTargetEntries`, `SpellCastFlags`, `MinDelay`, `MaxDelay`, `InitDelay`) VALUES\r\n");
                            }

                            var delays = BuildDelays(timings);
                            mainOutput += BuildSqlFromPattern(creature, parsed, delays, timings, false);
                        }
                    }
                    else
                    {
                        var distinctEvents = eventsList.GroupBy(e => new
                        {
                            EmoteId = e.EmoteId ?? 0,
                            EmoteStateId = e.EmoteStateId ?? 0,
                            SpellId = e.SpellId ?? 0
                        }).Select(g => g.First()).ToList();

                        debugOutput += $"Couldn't find any appropriate pattern for creature with LinkedId: {creature.linkedId} (Guid: {creature.guid}, Entry: {creature.entry}, Name: {creature.name}, Uniq events count: {distinctEvents.Count}).\r\n";
                    }
                }
            }
            else
            {
                Creature creature = null;

                if (!creatures.TryGetValue(linkedIdOrGuid, out creature))
                    creature = creatures.Values.FirstOrDefault(x => x.linkedId == linkedIdOrGuid);

                if (creature == null)
                    return;

                List<RolePlayEvent> eventsList = new List<RolePlayEvent>();

                GetEventsForCreature(creature, ref eventsList, ref debugOutput);
                RemoveInvalidEvents(creature, ref eventsList, ref debugOutput);
                MergeClosestEvents(ref eventsList, ref debugOutput);

                if (eventsList.Count == 0)
                {
                    mainOutput += "There is no events for this creature.\r\n";
                }
                else
                {
                    var pattern = GetPatternFromEvents(creature, eventsList);

                    if (pattern == "NO_PATTERN")
                    {
                        mainOutput += "Couldn't find any appropriate pattern for this creature.\r\n";
                    }
                    else
                    {
                        var parsed = ParsePattern(pattern);
                        var timings = CollectTimings(eventsList, parsed, creature);

                        if (timings.All(x => x.Count > 0))
                        {
                            mainOutput += ("INSERT INTO `creature_role_play_events` (`LinkedId`, `Idx`, `EmoteIds`, `EmoteStateIds`, `StandStateIds`, `SpellIds`, `SpellTargetSearchRange`, `SpellTargetEntries`, `SpellCastFlags`, `NextEventMinDelay`, `NextEventMaxDelay`, `InitDelay`) VALUES\r\n");
                            var delays = BuildDelays(timings);
                            mainOutput += BuildSqlFromPattern(creature, parsed, delays, timings, true);
                        }
                        else
                        {
                            mainOutput += "Timings was failed for this creature, so no SQL generated.\r\n";
                        }
                    }
                }
            }

            if (debugOutput != "")
            {
                if (mainOutput.Length > 0)
                    mainOutput += "\r\n";

                mainOutput += debugOutput;
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = mainOutput;
        }

        private List<Dictionary<string, List<int>>> ParsePattern(string pattern)
        {
            List<Dictionary<string, List<int>>> patterntList = new List<Dictionary<string, List<int>>>();

            try
            {
                if (pattern.Contains("->") && pattern.Contains("|"))
                {
                    foreach (var step in pattern.Split(new[] { "->" }, StringSplitOptions.None))
                    {
                        Dictionary<string, List<int>> actionsDict = new Dictionary<string, List<int>>();

                        if (step.Contains("|"))
                        {
                            foreach (string part in step.Split('|'))
                            {
                                string[] kv = part.Split(':');

                                if (!actionsDict.ContainsKey(kv[0]))
                                    actionsDict.Add(kv[0], new List<int> { int.Parse(kv[1]) });
                                else
                                    actionsDict[kv[0]].Add(int.Parse(kv[1]));
                            }
                        }
                        else
                        {
                            actionsDict.Add(step.Split(':')[0], new List<int> { int.Parse(step.Split(':')[1]) });
                        }

                        patterntList.Add(actionsDict);
                    }
                }
                else if (pattern.Contains("->") && !pattern.Contains("|"))
                {
                    Dictionary<string, List<int>> actionsDict = new Dictionary<string, List<int>>();

                    foreach (var step in pattern.Split(new[] { "->" }, StringSplitOptions.None))
                    {
                        patterntList.Add(new Dictionary<string, List<int>>() { { step.Split(':')[0], new List<int> { int.Parse(step.Split(':')[1]) } } });
                    }
                }
                else if (!pattern.Contains("->") && pattern.Contains("|"))
                {
                    Dictionary<string, List<int>> actionsDict = new Dictionary<string, List<int>>();

                    foreach (string part in pattern.Split('|'))
                    {
                        string[] kv = part.Split(':');

                        if (!actionsDict.ContainsKey(kv[0]))
                            actionsDict.Add(kv[0], new List<int> { int.Parse(kv[1]) });
                        else
                            actionsDict[kv[0]].Add(int.Parse(kv[1]));
                    }

                    patterntList.Add(actionsDict);
                }
                else
                    patterntList.Add(new Dictionary<string, List<int>>() { { pattern.Split(':')[0], new List<int> { int.Parse(pattern.Split(':')[1]) } } });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception \"{ex.Message}\" while parsing pattern \"{pattern}\"");
                return patterntList;
            }

            return patterntList;
        }

        private bool MatchStep(RolePlayEvent e, Dictionary<string, List<int>> step)
        {
            if (step.ContainsKey("E") && step["E"].Count(x => x == e.EmoteId) == 0)
                return false;

            if (step.ContainsKey("S") && step["S"].Count(x => x == e.EmoteStateId) == 0)
                return false;

            if (step.ContainsKey("Sp") && step["Sp"].Count(x => x == e.SpellId) == 0)
                return false;

            return true;
        }

        private List<List<double>> CollectTimings(List<RolePlayEvent> events, List<Dictionary<string, List<int>>> pattern, Creature creature)
        {
            List<List<double>> timings = new List<List<double>>(); 
            List<TimeSpan> destroyTimes = creatureDestroyPackets.ContainsKey(creature.guid) ? creatureDestroyPackets[creature.guid].Select(x => x.packetSendTime).OrderBy(x => x).ToList() : new List<TimeSpan>();

            for (int i = 0; i < pattern.Count; i++)
                timings.Add(new List<double>());

            for (int p = 0; p < pattern.Count; p++)
            {
                var patternCurrStep = pattern[p];
                var patternPrevStep = new Dictionary<string, List<int>>();
                if (p > 0)
                    patternPrevStep = pattern.ElementAt(p - 1);
                else
                    patternPrevStep = pattern.Last();

                for (int e = 1; e < events.Count; e++)
                {
                    if (MatchStep(events[e], patternCurrStep))
                    {
                        if (MatchStep(events[e - 1], patternPrevStep))
                        {
                            if ((destroyTimes != null && !destroyTimes.Any(x => x >= events[e - 1].Time && x < events[e].Time)))
                            {
                                timings[p].Add((events[e].Time - events[e - 1].Time).TotalMilliseconds);
                            }
                        }
                    }
                }
            }

            return timings;
        }

        private int RoundDelay(double value)
        {
            return (int)(Math.Round(value / 500.0) * 500);
        }

        private List<Tuple<int, int>> BuildDelays(List<List<double>> timings)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            foreach (List<double> list in timings)
            {
                if (list.Count == 0)
                {
                    result.Add(Tuple.Create(0, 0));
                    continue;
                }

                int min = RoundDelay(list.Min());
                int max = RoundDelay(list.Max());
                result.Add(Tuple.Create(min, max));
            }

            return result;
        }

        private string BuildSqlFromPattern(Creature creature, List<Dictionary<string, List<int>>> pattern, List<Tuple<int, int>> delays, List<List<double>> timings, bool singleNpc)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < pattern.Count; i++)
            {
                Dictionary<string, List<int>> step = pattern[i];
                string emote = step.ContainsKey("E") ? string.Join(" ", step["E"]) : "";
                string emoteState = step.ContainsKey("S") ? string.Join(" ", step["S"]) : "";
                string standState = step.ContainsKey("St") ? string.Join(" ", step["St"]) : "";
                string spell = step.ContainsKey("Sp") ? string.Join(" ", step["Sp"]) : "";
                Tuple<int, int> delay = delays[i];
                KeyValuePair<string, string> spellTargetsAndRange = GetSpellTargetsAndRange(spell, creature.guid);

                string line = $"('{creature.linkedId}', {i}, '{emote}', '{emoteState}', '{standState}', '{spell}', {spellTargetsAndRange.Value}, {spellTargetsAndRange.Key}, 0, {delay.Item1}, {(delay.Item1 == delay.Item2 ? 0 : delay.Item2)}, 0)";

                if (singleNpc)
                {
                    if (i < pattern.Count - 1)
                        line += ",";
                    else
                        line += ";";
                }
                else
                {
                    line += ",";
                }

                line += " -- " + creature.name + ", Timers Count: " + timings[i].Count;

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private KeyValuePair<string, string> GetSpellTargetsAndRange(string spell, string casterGuid)
        {
            if (spell == "")
                return new KeyValuePair<string, string>("''", "0");

            uint spellId = Convert.ToUInt32(spell);
            string spellRange = "0";
            string spellTargetEntries = "''";

            if (DB2.Db2.SpellRangeStore.ContainsKey(spellId))
            {
                spellRange = Convert.ToString(DB2.Db2.SpellRangeStore[spellId].Item2);
            }

            var spellTargetGuids = spellPackets.Where(x => x.spellId == spellId && x.casterGuid == casterGuid && x.targetGuid != null).Select(x => x.targetGuid).Distinct().ToList();
            if (spellTargetGuids.Count > 0)
            {
                spellTargetEntries = "'";

                foreach (string targetGuid in spellTargetGuids)
                {
                    if (creatures.ContainsKey(targetGuid) && !spellTargetEntries.Contains(Convert.ToString(creatures[targetGuid].entry)))
                    {
                        spellTargetEntries += spellTargetEntries == "'" ? Convert.ToString(creatures[targetGuid].entry) : ", " + Convert.ToString(creatures[targetGuid].entry);
                    }
                }

                spellTargetEntries += "'";
            }

            return new KeyValuePair<string, string>(spellTargetEntries, spellRange);
        }

        private async Task<string> GetAnswerFromAIAsync(string systemStr, string promptStr, CancellationToken cancellationToken = default)
        {
            var http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            var json = JsonSerializer.Serialize(new
            {
                model = "local-model",
                messages = new[]
                {
                    new { role = "system", content = systemStr },
                    new { role = "user", content = promptStr }
                },
                max_tokens = 50,
                temperature = 0.1f,
                top_p = 0.9f,
                top_k = 40,
                repeat_penalty = 1.1f,
                stop = new[] { "Explanation", "\nExplanation", " The sequence", " This shows" }
            });

            var response = await http.PostAsync(
                "http://localhost:8080/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json"),
                cancellationToken
            ).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return "";

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return result;
        }

        private string GetPatternFromAI(string sequence)
        {
            string system =
            @"You extract patterns from segmented sequences. Output ONLY the pattern or NO_PATTERN if not found any pattern.

            CRITICAL FORMATTING RULES:
            - NEVER use commas ',' in output
            - Use ONLY '->' for cycles (e.g., A->B->C or A->B|C->D)
            - Use ONLY '|' for sets (e.g., A|B|C or A|B->C|D)
            - NEVER add explanations in output, just output the pattern
            - Output ONLY 'NO_PATTERN' if no pattern found

            EXAMPLES OF CYCLE PATTERNS:
            Input: E:35, E:53 / E:35, E:53 / E:35, E:53
            Output: E:35->E:53

            Input: S:0 / S:418, S:173, S:0, S:418 / S:173, S:0 / S:418, S:0, S:418, S:173, S:0 / S:418, S:173 / S:0, S:418, S:173, S:0, S:418
            Output: S:0->S:418->S:173

            Input: S:173, S:0, S:173, S:0
            Output: S:173->S:0

            Input: E:35, E:53, E:7 / E:35, E:53, E:7
            Output: E:35->E:53->E:7
            
            EXAMPLES OF RANDOM PATTERNS:
            Input: E:11, E:21, E:25, E:273, E:5, E:5, E:11, E:25, E:5, E:273, E:21
            Explanation: Segment consist from random actions, so our pattern is a set of unique actions
            Output: E:11|E:21|E:25|E:273|E:5

            Input: E:274 / E:274, E:6, E:5, E:6, E:5, E:18, E:5, E:6, E:274, E:6, E:274, E:274, E:6, E:274, E:6, E:5, E:274, E:274, E:18, E:274, E:18
            Explanation: Second segment consist from random actions, so our pattern is a set of unique actions
            Output: E:274|E:6|E:5|E:18

            Input: E:11 / E:273, E:273, E:11, E:5 / E:4, E:25, E:21, E:11, E:25, E:5 / E:4, E:11, E:25 / E:25, E:21, E:5
            Explanation: Segments consist from random actions, so our pattern is a set of unique actions
            Output: E:11|E:273|E:5|E:4|E:25

            EXAMPLES OF NO_PATTERN:
            Input: E:35, E:53 / E:7, E:35, E:53 / E:7
            Output: NO_PATTERN

            Input: S:376 / S:384, S:384, E:435|S:0, E:435|S:376, S:384, E:435|S:0 / E:435|S:376, S:376, S:384, E:435|S:0, E:435|S:376, S:384
            Output: NO_PATTERN

            Input: S:0, S:418
            Explanation from user: Too little count of actions, so we can't really see the pattern here
            Output: NO_PATTERN

            Input: E:11 / E:5, E:21 / E:6, E:11 / Sp:1250028, E:6, E:6, E:1, E:1, E:1, E:273, E:5, E:6
            Explanation: Segments consist from random actions, but one of actions (Sp:1250028) doesn't repeat, so we can't really predict it's pattern
            Output: NO_PATTERN";

            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                var result = GetAnswerFromAIAsync(system, sequence, cts.Token);
                var doc = JsonDocument.Parse(result.Result);
                var content = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return content?.Trim() ?? "NO_PATTERN";
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("LLM request timed out after 30 seconds");
                return "NO_PATTERN";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LLM error: {ex.Message}");
                return "NO_PATTERN";
            }
        }

        private string GetPatternFromEvents(Creature creature, List<RolePlayEvent> events)
        {
            string sequence = "";
            List<List<string>> sequenceSegments = new List<List<string>>();
            List<TimeSpan> destroyTimes = creatureDestroyPackets.ContainsKey(creature.guid) ? creatureDestroyPackets[creature.guid].Select(x => x.packetSendTime).OrderBy(x => x).ToList() : new List<TimeSpan>();

            var distinctEvents = events.GroupBy(e => new
            {
                EmoteId = e.EmoteId ?? 0,
                EmoteStateId = e.EmoteStateId ?? 0,
                StandStateId = e.StandStateId ?? 0,
                SpellId = e.SpellId ?? 0
            }).Select(g => g.First()).ToList();

            if (events.Count == 1)
                return "NO_PATTERN";

            if (distinctEvents.Count() == 1)
            {
                if (IsEventContainsInvalidData(GetEventKey(distinctEvents.First()), creature.linkedId))
                    return "NO_PATTERN";
                else
                    return GetEventKey(events.First());
            }
            else
            {
                for (int e = 0; e < events.Count; e++)
                {
                    if (e == 0)
                        sequenceSegments.Add(new List<string> { GetEventKey(events[e]) });
                    else if (destroyTimes != null)
                    {
                        if (e < events.Count - 1 && destroyTimes.Any(x => x >= events[e].Time && x < events[e + 1].Time))
                        {
                            sequenceSegments.Add(new List<string> { GetEventKey(events[e]) });
                        }
                        else
                        {
                            sequenceSegments.Last().Add(GetEventKey(events[e]));
                        }
                    }
                    else
                    {
                        sequenceSegments.Last().Add(GetEventKey(events[e]));
                    }
                }

                foreach (var segment in sequenceSegments)
                {
                    if (sequence == "")
                        sequence = string.Join(", ", segment);
                    else
                        sequence += " / " + string.Join(", ", segment);
                }
            }

            return GetPatternFromAI(sequence);
        }

        private bool IsEventContainsInvalidData(string eventStr, string linkedId)
        {
            if (eventStr.StartsWith("S:") || eventStr.StartsWith("St:"))
                return true;

            if (eventStr.StartsWith("Sp:"))
            {
                string spellName = GetSpellName(Convert.ToUInt32(eventStr.Substring(3)));
                if (spellName.Contains("Zzz") || spellName.Contains("Sleep"))
                    return true;

                if (IsCreatureHasSpellInAddon(linkedId, eventStr.Substring(3)))
                    return true;
            }

            return false;
        }

        private void GetEventsForCreature(Creature creature, ref List<RolePlayEvent> events, ref string debugOutput)
        {
            if (emotePackets.Any(x => x.guid == creature.guid))
                events.AddRange(emotePackets.Where(x => x.guid == creature.guid).Select(x => new RolePlayEvent { EmoteId = x.emoteId, Time = x.packetSendTime }));

            if (emoteStatesPackets.ContainsKey(creature.guid))
                events.AddRange(emoteStatesPackets[creature.guid].Select(x => new RolePlayEvent { EmoteStateId = (uint)x.emoteStateId, Time = x.packetSendTime }));

            if (spellPackets.Any(x => x.casterGuid == creature.guid))
                events.AddRange(spellPackets.Where(x => x.casterGuid == creature.guid && !creature.combatTimings.IsCombatTimer(x.spellCastStartTime) && !IsSpellTriggeredByPeriodic((int)x.spellId) && x.type == PacketType.SMSG_SPELL_START).Select(x => new RolePlayEvent { SpellId = x.spellId, Time = x.spellCastStartTime }));

            if (standStatesPackets.ContainsKey(creature.guid))
                events.AddRange(standStatesPackets[creature.guid].Select(x => new RolePlayEvent { StandStateId = (uint)x.standState, Time = x.packetSendTime }));

            events = events.GroupBy(x => x.Time).Select(g => new RolePlayEvent
            {
                Time = g.Key,
                EmoteId = g.Where(x => x.EmoteId.HasValue).Select(x => x.EmoteId).FirstOrDefault(),
                EmoteStateId = g.Where(x => x.EmoteStateId.HasValue).Select(x => x.EmoteStateId).LastOrDefault(),
                StandStateId = g.Where(x => x.StandStateId.HasValue).Select(x => x.StandStateId).LastOrDefault(),
                SpellId = g.Where(x => x.SpellId.HasValue).Select(x => x.SpellId).FirstOrDefault()
            }).OrderBy(x => x.Time).ToList();

            if (events.Count > 0)
            {
                debugOutput += "Creature had following events:\r\n";

                for (int i = 0; i < events.Count; i++)
                {
                    RolePlayEvent rpEvent = events[i];

                    string emote = rpEvent.EmoteId.HasValue ? $"Emote: {rpEvent.EmoteId.Value}" : "";
                    string emoteState = rpEvent.EmoteStateId.HasValue ? $"Emote State: {rpEvent.EmoteStateId.Value}" : "";
                    string standState = rpEvent.StandStateId.HasValue ? $"Stand State: {rpEvent.StandStateId.Value}" : "";
                    string spell = rpEvent.SpellId.HasValue ? $"Spell: {rpEvent.SpellId.Value}" : "";
                    string key = string.Join(" ", new[] { emote, emoteState, standState, spell }.Where(x => x != ""));

                    string diffStr = "";

                    if (i < events.Count - 1)
                    {
                        double diff = (events[i + 1].Time - rpEvent.Time).TotalMilliseconds;
                        diffStr = diff.ToString("F0");
                    }

                    debugOutput += $"{rpEvent.Time.ToFormattedString()} | {key} | Time to next event: {diffStr}\r\n";
                }
            }
        }

        private void RemoveInvalidEvents(Creature creature, ref List<RolePlayEvent> events, ref string debugOutput)
        {
            List<TimeSpan> destroyTimes = creatureDestroyPackets.ContainsKey(creature.guid)? creatureDestroyPackets[creature.guid].Select(x => x.packetSendTime).OrderBy(x => x).ToList(): new List<TimeSpan>();
            if (destroyTimes.Count == 0)
                return;

            List<RolePlayEvent> validEvents = new List<RolePlayEvent>();
            StringBuilder removeDueDestroyLog = new StringBuilder();
            StringBuilder removeDueLowDelayLog = new StringBuilder();
            bool hasRemovals = false;

            for (int i = 0; i < events.Count; i++)
            {
                var current = events[i];

                TimeSpan prevDestroy = destroyTimes.LastOrDefault(dt => dt < current.Time);
                TimeSpan nextDestroy = destroyTimes.FirstOrDefault(dt => dt > current.Time);

                bool hasPrevDestroy = prevDestroy != default;
                bool hasNextDestroy = nextDestroy != default;

                bool isFirstEvent = i == 0;
                bool isLastEvent = i == events.Count - 1;

                bool remove = false;

                if (hasPrevDestroy && hasNextDestroy)
                {
                    bool hasOtherEventsBetween = events.Any(e =>
                        e != current &&
                        e.Time > prevDestroy &&
                        e.Time < nextDestroy);

                    if (!hasOtherEventsBetween)
                        remove = true;
                }

                else if (isFirstEvent && hasNextDestroy)
                {
                    bool hasOtherBeforeDestroy = events.Any(e =>
                        e != current &&
                        e.Time < nextDestroy);

                    if (!hasOtherBeforeDestroy)
                        remove = true;
                }

                else if (isLastEvent && hasPrevDestroy)
                {
                    bool hasOtherAfterDestroy = events.Any(e =>
                        e != current &&
                        e.Time > prevDestroy);

                    if (!hasOtherAfterDestroy)
                        remove = true;
                }

                if (remove)
                {
                    hasRemovals = true;

                    string emote = current.EmoteId.HasValue ? $"Emote: {current.EmoteId.Value}" : "";
                    string emoteState = current.EmoteStateId.HasValue ? $"Emote State: {current.EmoteStateId.Value}" : "";
                    string standState = current.StandStateId.HasValue ? $"Stand State: {current.StandStateId.Value}" : "";
                    string spell = current.SpellId.HasValue ? $"Spell: {current.SpellId.Value}" : "";
                    string key = string.Join(" ", new[] { emote, emoteState, standState, spell }.Where(x => x != ""));

                    removeDueDestroyLog.AppendLine($"{current.Time.ToFormattedString()} | {key}");
                }
                else
                {
                    validEvents.Add(current);
                }
            }

            for (int i = 0; i < events.Count; i++)
            {
                var prevEvent = i > 0 ? events[i - 1] : null;
                var currentEvent = events[i];

                if (prevEvent != null && (currentEvent.Time - prevEvent.Time).TotalMilliseconds <= 500 && prevEvent.EmoteId.HasValue && currentEvent.EmoteId.HasValue)
                {
                    hasRemovals = true;

                    string emote = currentEvent.EmoteId.HasValue ? $"Emote: {currentEvent.EmoteId.Value}" : "";
                    string emoteState = currentEvent.EmoteStateId.HasValue ? $"Emote State: {currentEvent.EmoteStateId.Value}" : "";
                    string standState = currentEvent.StandStateId.HasValue ? $"Stand State: {currentEvent.StandStateId.Value}" : "";
                    string spell = currentEvent.SpellId.HasValue ? $"Spell: {currentEvent.SpellId.Value}" : "";
                    string key = string.Join(" ", new[] { emote, emoteState, standState, spell }.Where(x => x != ""));

                    removeDueLowDelayLog.AppendLine($"{currentEvent.Time.ToFormattedString()} | {key}");
                    validEvents.Remove(currentEvent);
                }
            }

            if (hasRemovals)
            {
                if (debugOutput.Contains("Creature had following events:"))
                    debugOutput += "\r\n";

                if (removeDueDestroyLog.Length > 0)
                {
                    debugOutput += "Following events was removed due to destroy:\r\n";
                    debugOutput += removeDueDestroyLog.ToString();

                    if (removeDueLowDelayLog.Length > 0)
                    {
                        debugOutput += "\r\n";
                    }
                }

                if (removeDueLowDelayLog.Length > 0)
                {
                    debugOutput += "Following events was removed due to low delay:\r\n";
                    debugOutput += removeDueLowDelayLog.ToString();
                }
            }

            events = validEvents;
        }

        private void MergeClosestEvents(ref List<RolePlayEvent> events, ref string debugOutput)
        {
            List<RolePlayEvent> mergedEvents = new List<RolePlayEvent>();
            StringBuilder mergeLog = new StringBuilder();
            bool hasMerges = false;

            for (int i = 0; i < events.Count; ++i)
            {
                RolePlayEvent currentEvent = events[i];

                if (i < events.Count - 1)
                {
                    RolePlayEvent nextEvent = events[i + 1];

                    uint diff = (uint)Math.Ceiling((nextEvent.Time - currentEvent.Time).TotalMilliseconds);

                    if (((currentEvent.EmoteId.HasValue && (nextEvent.EmoteStateId.HasValue || nextEvent.SpellId.HasValue || nextEvent.StandStateId.HasValue)) ||
                        (currentEvent.EmoteStateId.HasValue && (nextEvent.EmoteId.HasValue || nextEvent.SpellId.HasValue || nextEvent.StandStateId.HasValue)) ||
                        (currentEvent.StandStateId.HasValue && (nextEvent.EmoteId.HasValue || nextEvent.SpellId.HasValue)) ||
                        (currentEvent.SpellId.HasValue && (nextEvent.EmoteId.HasValue || nextEvent.EmoteStateId.HasValue || nextEvent.StandStateId.HasValue)))
                        && diff < 200)
                    {
                        hasMerges = true;

                        string emote = nextEvent.EmoteId.HasValue ? $"Emote: {nextEvent.EmoteId.Value}" : "";
                        string emoteState = nextEvent.EmoteStateId.HasValue ? $"Emote State: {nextEvent.EmoteStateId.Value}" : "";
                        string standState = nextEvent.StandStateId.HasValue ? $"Stand State: {nextEvent.StandStateId.Value}" : "";
                        string spell = nextEvent.SpellId.HasValue ? $"Spell: {nextEvent.SpellId.Value}" : "";
                        string key = string.Join(" ", new[] { emote, emoteState, standState, spell }.Where(x => x != ""));

                        mergeLog.AppendLine($"{nextEvent.Time.ToFormattedString()} | {key}");

                        mergedEvents.Add(new RolePlayEvent
                        {
                            Time = currentEvent.Time,
                            EmoteId = currentEvent.EmoteId.HasValue ? currentEvent.EmoteId : nextEvent.EmoteId,
                            EmoteStateId = currentEvent.EmoteStateId.HasValue ? currentEvent.EmoteStateId : nextEvent.EmoteStateId,
                            StandStateId = currentEvent.StandStateId.HasValue ? currentEvent.StandStateId : nextEvent.StandStateId,
                            SpellId = currentEvent.SpellId.HasValue ? currentEvent.SpellId : nextEvent.SpellId,
                        });

                        i++;
                        continue;
                    }
                }

                mergedEvents.Add(currentEvent);
            }

            if (hasMerges)
            {
                if (debugOutput.Contains("Following events was removed due to destroy:"))
                    debugOutput += "\r\n";

                debugOutput += "Following events was removed due to merge:\r\n";
                debugOutput += mergeLog.ToString();
            }

            events = mergedEvents;
        }

        private bool IsCreatureHasPathOrFormationOnDb(string linkedId)
        {
            bool alreadyHaveWaypointsOrRelatedToFormation = false;

            string oldFormationSqlQuery = "SELECT `leaderLinkedId`, `memberLinkedId` FROM `creature_formations` WHERE `leaderLinkedId` = '" + linkedId + "' OR " + "`memberLinkedId` = '" + linkedId + "';";
            var oldCreatureFormationDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(oldFormationSqlQuery) : null;

            if (oldCreatureFormationDs != null && oldCreatureFormationDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in oldCreatureFormationDs.Tables["table"].Rows)
                {
                    if (Convert.ToString(row.ItemArray[0]) == linkedId ||
                        Convert.ToString(row.ItemArray[1]) == linkedId)
                    {
                        alreadyHaveWaypointsOrRelatedToFormation = true;
                        break;
                    }
                }
            }

            if (!alreadyHaveWaypointsOrRelatedToFormation)
            {
                string newFormationSqlQuery = "SELECT `LeaderLinkedId`, `MemberLinkedId` FROM `creature_group_members` WHERE `LeaderLinkedId` = '" + linkedId + "' OR " + "`MemberLinkedId` = '" + linkedId + "';";
                var newCreatureFormationDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(newFormationSqlQuery) : null;

                if (newCreatureFormationDs != null && newCreatureFormationDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in newCreatureFormationDs.Tables["table"].Rows)
                    {
                        if (Convert.ToString(row.ItemArray[0]) == linkedId ||
                            Convert.ToString(row.ItemArray[1]) == linkedId)
                        {
                            alreadyHaveWaypointsOrRelatedToFormation = true;
                            break;
                        }
                    }
                }
            }

            if (!alreadyHaveWaypointsOrRelatedToFormation)
            {
                string oldWaypointSqlQuery = "SELECT `path_id` FROM `creature_addon` WHERE `linked_id` = '" + linkedId + "';";
                var oldWaypointsDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(oldWaypointSqlQuery) : null;

                if (oldWaypointsDs != null && oldWaypointsDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in oldWaypointsDs.Tables["table"].Rows)
                    {
                        if (Convert.ToInt32(row.ItemArray[0]) > 0)
                        {
                            alreadyHaveWaypointsOrRelatedToFormation = true;
                            break;
                        }
                    }
                }
            }

            if (!alreadyHaveWaypointsOrRelatedToFormation)
            {
                string newWaypointSqlQuery = "SELECT `linked_id` FROM `waypoint_data` WHERE `linked_id` = '" + linkedId + "';";
                var newWaypointsDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(newWaypointSqlQuery) : null;

                if (newWaypointSqlQuery != null && newWaypointsDs.Tables["table"].Rows.Count > 0)
                {
                    alreadyHaveWaypointsOrRelatedToFormation = true;
                }
            }

            return alreadyHaveWaypointsOrRelatedToFormation;
        }

        private bool IsCreatureHasRolePlayEvents(string linkedId)
        {
            string randomEmotesSqlQuery = "SELECT `LinkedId` FROM `creature_role_play_events` WHERE `LinkedId` = '" + linkedId + "';";
            var randomEmotesDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(randomEmotesSqlQuery) : null;

            if (randomEmotesDs != null && randomEmotesDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in randomEmotesDs.Tables["table"].Rows)
                {
                    if (Convert.ToString(row.ItemArray[0]) != "")
                        return true;
                }
            }

            return false;
        }

        private bool IsCreatureHasConversationAurasInAddon(string linkedId)
        {
            string aurasSqlQuery = "SELECT `auras` FROM `creature_addon` WHERE `linked_id` = '" + linkedId + "';";
            var aurasDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(aurasSqlQuery) : null;

            if (aurasDs != null && aurasDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in aurasDs.Tables["table"].Rows)
                {
                    if (Convert.ToString(row.ItemArray[0]) != "")
                    {
                        string[] splittedLine = Convert.ToString(row.ItemArray[0]).Split(' ');

                        foreach (string auraId in splittedLine)
                        {
                            if (DB2.Db2.SpellName.ContainsKey(Convert.ToInt32(auraId)))
                            {
                                if (DB2.Db2.SpellName[Convert.ToInt32(auraId)].Name.Contains("Conversation"))
                                        return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool IsCreatureHasSpellInAddon(string linkedId, string spellId)
        {
            string aurasSqlQuery = "SELECT `auras` FROM `creature_addon` WHERE `linked_id` = '" + linkedId + "';";
            var aurasDs = Properties.Settings.Default.UsingDB ? SQLModule.WorldSelectQuery(aurasSqlQuery) : null;

            if (aurasDs != null && aurasDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in aurasDs.Tables["table"].Rows)
                {
                    if (Convert.ToString(row.ItemArray[0]) != "")
                        return Convert.ToString(row.ItemArray[0]).Split(' ').Any(x => x ==spellId);
                }
            }

            return false;
        }

        private bool IsSpellTriggeredByPeriodic(int triggerSpellId)
        {
            if (DB2.Db2.SpellTriggerStore.ContainsKey(triggerSpellId))
            {
                foreach (uint spellId in DB2.Db2.SpellTriggerStore[triggerSpellId])
                {
                    for (uint i = 0; i < 32; i++)
                    {
                        var spellEffectTuple = Tuple.Create(spellId, i);

                        if (DB2.Db2.SpellEffectStore.ContainsKey(spellEffectTuple))
                        {
                            var spellEffect = DB2.Db2.SpellEffectStore[spellEffectTuple];

                            if (spellEffect.EffectAura == 226 || spellEffect.EffectAura == 23 || spellEffect.EffectAura == 227)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public void FindDoubleSpawns()
        {
            if (mainForm.textBox_ParsedFileAdvisor_FindDoubleSpawns.Text == "")
                return;

            string creaturesSelectQuery = $"SELECT `linked_id`, `position_x`, `position_y`, `position_z` FROM `creature` WHERE `zoneid` = {mainForm.textBox_ParsedFileAdvisor_FindDoubleSpawns.Text};";
            var creaturesDs = SQLModule.WorldSelectQuery(creaturesSelectQuery);
            List<Creature> creaturesList = new List<Creature>();
            string output = "";

            if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                {
                    Creature creature = creatures.Values.FirstOrDefault(x => x.GetLinkedId() == Convert.ToString(row.ItemArray[0]));
                    if (creature == null)
                        continue;

                    creaturesList.Add(creature);
                }
            }

            creaturesDs.Clear();

            HashSet<Creature> duplicateCandidates = new HashSet<Creature>();

            for (int i = 0; i < creaturesList.Count; i++)
            {
                var creatureA = creaturesList[i];

                for (int j = i + 1; j < creaturesList.Count; j++)
                {
                    var creatureB = creaturesList[j];

                    if (creatureA.entry != creatureB.entry)
                        continue;

                    float distance = creatureA.spawnPosition.GetDistance(creatureB.spawnPosition);
                    if (distance > 5.0f)
                        continue;

                    TimeSpan timeDiff = (creatureA.lastUpdatePacketTime - creatureB.lastUpdatePacketTime).Duration();
                    if (timeDiff.TotalMinutes < 10)
                        continue;

                    duplicateCandidates.Add(creatureA);
                    duplicateCandidates.Add(creatureB);
                }
            }

            foreach (var creature in duplicateCandidates)
            {
                output += $"Entry: {creature.entry} | Name: {creature.name} | LinkedId: {creature.GetLinkedId()} | Position: .go {creature.spawnPosition.x} {creature.spawnPosition.y} {creature.spawnPosition.z} | Spawn time: {creature.lastUpdatePacketTime.ToString()}\r\n";
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }
    }
}
