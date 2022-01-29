using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Parsed_File_Advisor
{
    public class ParsedFileAdvisor
    {
        private readonly MainForm mainForm;
        public Dictionary<string, Creature> creaturesDict = new Dictionary<string, Creature>();
        public List<UpdateObjectPacket> conversationPackets = new List<UpdateObjectPacket>();
        public Dictionary<uint, List<CreatureText>> creatureTextsDict = new Dictionary<uint, List<CreatureText>>();
        public List<SpellStartPacket> spellPacketsList = new List<SpellStartPacket>();
        public List<QuestGiverAcceptQuestPacket> questAcceptPackets = new List<QuestGiverAcceptQuestPacket>();
        public List<QuestGiverQuestCompletePacket> questCompletePackets = new List<QuestGiverQuestCompletePacket>();

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
                creaturesDict.Clear();
            }

            Parallel.For(0, lines.Length, index =>
            {
                Packet.PacketTypes packetType = Packet.GetPacketTypeFromLine(lines[index]);

                if (packetType == Packet.PacketTypes.SMSG_UPDATE_OBJECT && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_UPDATE_OBJECT);
                }
                else if (packetType == Packet.PacketTypes.SMSG_SPELL_START && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_SPELL_START);
                }
                else if (packetType == Packet.PacketTypes.SMSG_SPELL_GO && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_SPELL_GO);
                }
                else if (packetType == Packet.PacketTypes.SMSG_CHAT && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_CHAT);
                }
                else if (packetType == Packet.PacketTypes.CMSG_QUEST_GIVER_ACCEPT_QUEST && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.CMSG_QUEST_GIVER_ACCEPT_QUEST);
                }
                else if (packetType == Packet.PacketTypes.SMSG_QUEST_GIVER_QUEST_COMPLETE && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_QUEST_GIVER_QUEST_COMPLETE);
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
                            lock (creaturesDict)
                            {
                                if (!creaturesDict.ContainsKey(packet.guid))
                                {
                                    creaturesDict.Add(packet.guid, new Creature(packet));
                                }
                                else
                                {
                                    creaturesDict[packet.guid].UpdateCreature(packet);
                                }
                            }
                        }
                    });
                }
            }

            conversationPackets = conversationPackets.OrderBy(x => x.packetSendTime).ToList();

            Parallel.ForEach(creaturesDict.Values, creature =>
            {
                creature.name = MainForm.GetCreatureNameByEntry(creature.entry);
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

                    lock (spellPacketsList)
                    {
                        spellPacketsList.Add(spellPacket);
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

            if (mainForm.checkBox_ParsedFileAdvisor_CreateDataFile.Checked)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (!multiSelect)
                {
                    using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_parsed_filed_advisor_packets.dat"), FileMode.OpenOrCreate))
                    {
                        Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>
                        {
                            { 0, creaturesDict },
                            { 1, creatureTextsDict }
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
                            { 0, creaturesDict },
                            { 1, creatureTextsDict }
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

            Dictionary<string, Creature> creatureDictFromSerialize = (Dictionary<string, Creature>)dictFromSerialize[0];
            Dictionary<uint, List<CreatureText>> creatureTextsDictFromSerialize = (Dictionary<uint, List<CreatureText>>)dictFromSerialize[1];

            if (multiSelect)
            {
                creaturesDict = creaturesDict.Concat(creatureDictFromSerialize.Where(x => !creaturesDict.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                creatureTextsDict = creatureTextsDict.Concat(creatureTextsDictFromSerialize.Where(x => !creatureTextsDict.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
            }
            else
            {
                creaturesDict = creatureDictFromSerialize;
                creatureTextsDict = creatureTextsDictFromSerialize;
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
        }

        public void ImportSuccessful()
        {
            mainForm.Cursor = Cursors.Default;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_AreaTriggerSplines.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_SpellDestinations.Enabled = true;
            mainForm.textBox_ParsedFileAdvisor_QuestConversations.Enabled = true;
        }
        public void ImportFailed()
        {
            mainForm.Cursor = Cursors.Default;
            mainForm.toolStripButton_ParsedFileAdvisor_ImportSniff.Enabled = true;
            mainForm.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "No File Loaded";
        }

        public void ParsePlayerCastedSpells()
        {
            if (mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Text == "")
                return;

            string playerGuid = mainForm.textBox_ParsedFileAdvisor_PlayerCastedSpells.Text;
            string output = "";
            List<KeyValuePair<SpellStartPacket, uint>> spellPacketsListPair = new List<KeyValuePair<SpellStartPacket, uint>>();

            Parallel.ForEach(spellPacketsList, spellPacket =>
            {
                if (spellPacket.casterGuid != playerGuid)
                    return;

                lock (spellPacketsList)
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

            Parallel.ForEach(spellPacketsList, spellPacket =>
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

        public void ParseQuestAcceptAndRewardConversations()
        {
            if (mainForm.textBox_ParsedFileAdvisor_QuestConversations.Text == "" || mainForm.textBox_ParsedFileAdvisor_QuestConversations.Text == "0")
                return;

            string output = "";
            QuestGiverAcceptQuestPacket acceptPacket = questAcceptPackets.SingleOrDefault(x => x.questId == Convert.ToUInt32(mainForm.textBox_ParsedFileAdvisor_QuestConversations.Text));
            QuestGiverQuestCompletePacket completePacket = questCompletePackets.SingleOrDefault(x => x.questId == Convert.ToUInt32(mainForm.textBox_ParsedFileAdvisor_QuestConversations.Text));

            if (acceptPacket.questId != 0)
            {
                UpdateObjectPacket acceptConversationPacket = conversationPackets.SingleOrDefault(x => x.packetSendTime >= acceptPacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - acceptPacket.packetSendTime.TotalMilliseconds) <= 1000);
                if (acceptConversationPacket.entry != 0)
                {
                    output += $"Conversation that goes after accepting quest {acceptPacket.questId} - {MainForm.GetQuestNameById(acceptPacket.questId)}:\r\n";
                    output += $"Conversation Id: {acceptConversationPacket.entry}\r\n";

                    for (int i = 0; i < acceptConversationPacket.conversationData.conversationLines.Count; i++)
                    {
                        int conversationLineId = (int)acceptConversationPacket.conversationData.conversationLines[i].Key;
                        string conversationLineText = "";
                        Creature actor = null;

                        if (creaturesDict.ContainsKey(acceptConversationPacket.conversationData.conversationActors[(int)acceptConversationPacket.conversationData.conversationLines[i].Value]))
                        {
                            actor = creaturesDict[acceptConversationPacket.conversationData.conversationActors[(int)acceptConversationPacket.conversationData.conversationLines[i].Value]];
                        }

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
                            else
                            {
                                conversationLineText = "There is no text for this line, probably actor is player";
                            }
                        }

                        if (actor != null)
                        {
                            output += $"[{i}]Actor: {actor.name} (Entry: {actor.entry}) - Line: {conversationLineText} (Id: {conversationLineId})\r\n";
                        }
                        else
                        {
                            output += $"[{i}]Actor is player\r\n";
                        }
                    }
                }
            }

            if (completePacket.questId != 0)
            {
                UpdateObjectPacket completeConversationPacket = conversationPackets.SingleOrDefault(x => x.packetSendTime >= completePacket.packetSendTime && (x.packetSendTime.TotalMilliseconds - completePacket.packetSendTime.TotalMilliseconds) <= 1000);
                if (completeConversationPacket.entry != 0)
                {
                    if (output != "")
                    {
                        output += "\r\n";
                    }

                    output += $"Conversation that goes after accepting quest {acceptPacket.questId} - {MainForm.GetQuestNameById(acceptPacket.questId)}:\r\n";
                    output += $"Conversation Id: {completeConversationPacket.entry}\r\n";

                    for (int i = 0; i < completeConversationPacket.conversationData.conversationLines.Count; i++)
                    {
                        int conversationLineId = (int)completeConversationPacket.conversationData.conversationLines[i].Key;
                        string conversationLineText = "";
                        Creature actor = null;

                        if (creaturesDict.ContainsKey(completeConversationPacket.conversationData.conversationActors[(int)completeConversationPacket.conversationData.conversationLines[i].Value]))
                        {
                            actor = creaturesDict[completeConversationPacket.conversationData.conversationActors[(int)completeConversationPacket.conversationData.conversationLines[i].Value]];
                        }

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
                            output += $"[{i}]Actor: {actor.name} - {actor.entry} - Line: {conversationLineText} : {conversationLineId}\r\n";
                        }
                        else
                        {
                            output += $"[{i}]Actor is player\r\n";
                        }
                    }
                }
            }

            mainForm.textBox_ParsedFileAdvisor_Output.Text = output;
        }
    }
}
