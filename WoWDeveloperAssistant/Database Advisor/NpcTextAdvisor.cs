using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class NpcTextAdvisor
    {
        public static void GetNpcTextForGossipMenu(TextBox textBox)
        {
            if (textBox.Text == "" || !textBox.Text.Contains("gossip_menu"))
                return;

            string outputText = "";
            string gossipRows = "";
            string npcTextRowsMerged = "";
            string npcTextRowsSplitted = "";
            string[] textBoxText = textBox.Text.Split('\n');
            Dictionary<int, List<int>> gossipWithBroadcastTextsDict = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> gossipWithNpcTextsDict = new Dictionary<int, List<int>>();
            Dictionary<int, int> broadcastTextToNpcTextLinksDict = new Dictionary<int, int>();

            for (int i = 0; i < textBoxText.Count(); i++)
            {
                if (textBoxText[i].Contains("INSERT INTO `gossip_menu`"))
                {
                    i++;

                    do
                    {
                        if (!gossipWithBroadcastTextsDict.ContainsKey(GetEntryFromLine(textBoxText[i])))
                        {
                            gossipWithBroadcastTextsDict.Add(GetEntryFromLine(textBoxText[i]), new List<int>());
                            gossipWithBroadcastTextsDict[GetEntryFromLine(textBoxText[i])].Add(GetBroadcastTextIdFromLine(textBoxText[i]));
                        }
                        else
                        {
                            gossipWithBroadcastTextsDict[GetEntryFromLine(textBoxText[i])].Add(GetBroadcastTextIdFromLine(textBoxText[i]));
                        }

                        i++;
                    } while (textBoxText.Length > i && textBoxText[i] != "");
                }
            }

            for (int i = 0; i < gossipWithBroadcastTextsDict.Count; i++)
            {
                var gossip = gossipWithBroadcastTextsDict.ElementAt(i);
                gossipWithNpcTextsDict.Add(gossip.Key, new List<int>());

                for (int j = 0; j < gossip.Value.Count(); j++)
                {
                    int broadcastTextId = gossip.Value[j];
                    DataSet npcTexts = SQLModule.DatabaseSelectQuery($"SELECT `id` FROM `npc_text` WHERE `BroadcastTextID0` = {broadcastTextId} OR `BroadcastTextID1` = {broadcastTextId} OR `BroadcastTextID2` = {broadcastTextId} OR `BroadcastTextID3` = {broadcastTextId} OR `BroadcastTextID4` = {broadcastTextId} OR `BroadcastTextID5` = {broadcastTextId} OR `BroadcastTextID6` = {broadcastTextId} OR `BroadcastTextID7` = {broadcastTextId};");
                    if (npcTexts == null || npcTexts.Tables["table"].Rows.Count == 0)
                    {
                        gossipWithNpcTextsDict[gossip.Key].Add(broadcastTextId * 10);
                        broadcastTextToNpcTextLinksDict.Add(broadcastTextId, broadcastTextId * 10);
                    }
                    else
                    {
                        gossipWithNpcTextsDict[gossip.Key].Add(Convert.ToInt32(npcTexts.Tables["table"].Rows[0].ItemArray[0].ToString()));
                        broadcastTextToNpcTextLinksDict.Add(Convert.ToInt32(broadcastTextId), Convert.ToInt32(npcTexts.Tables["table"].Rows[0].ItemArray[0].ToString()));
                    }
                }
            }

            for (int i = 0; i < textBoxText.Length; i++)
            {
                if (textBoxText[i].StartsWith("("))
                {
                    string[] gossipRow = textBoxText[i].Split(',');

                    string broadcastTextId = gossipRow[1];

                    gossipRow[1] = broadcastTextToNpcTextLinksDict[Convert.ToInt32(gossipRow[1])].ToString();

                    if (!IsGossipMenuExistsInDb(gossipRow))
                    {
                        gossipRows += textBoxText[i].Replace($"{broadcastTextId}", $" {gossipRow[1]}");
                    }
                }

                if (i + 1 >= textBoxText.Length)
                {
                    gossipRows = gossipRows.Replace(gossipRows.Split('\r').Where(x => x != "").ToArray().Last(), gossipRows.Split('\r').Where(x => x != "").ToArray().Last().Replace("),", ");")) + "\r\n";
                }
            }

            outputText += "DELETE FROM `gossip_menu` WHERE " + GetDeleteLineForGossipMenu(gossipRows);
            outputText += "INSERT INTO `gossip_menu` (`entry`, `text_id`, `FriendshipFactionId`, `VerifiedBuild`) VALUES\r\n";

            foreach (var gossip in gossipRows)
            {
                outputText += gossip;
            }

            foreach (var gossip in gossipWithNpcTextsDict)
            {
                if (!broadcastTextToNpcTextLinksDict.ContainsKey(gossip.Value[0] / 10))
                    continue;

                string buildedNpcRow = $"({gossip.Value[0]}, ";

                for (int i = 0; i < 8; i++)
                {
                    if (gossip.Value.Count - i > 0)
                    {
                        buildedNpcRow += $"{broadcastTextToNpcTextLinksDict.FirstOrDefault(x => x.Value == gossip.Value[i]).Key}, 1, ";
                    }
                    else
                    {
                        buildedNpcRow += $"0, 0, ";
                    }
                }

                buildedNpcRow += ";";

                buildedNpcRow = buildedNpcRow.Replace("0, ;", "0, -1),") + $" -- Gossip Menu Id: {gossip.Key}\r\n";

                npcTextRowsMerged += buildedNpcRow;
            }

            if (npcTextRowsMerged != "")
            {
                npcTextRowsMerged = npcTextRowsMerged.Replace(npcTextRowsMerged.Split('\n').Where(x => x != "").ToArray().Last(), npcTextRowsMerged.Split('\n').Where(x => x != "").ToArray().Last().Replace("),", ");"));

                outputText += "DELETE FROM `npc_text` WHERE " + GetDeleteForNpcText(npcTextRowsMerged);
                outputText += "INSERT INTO `npc_text` (`ID`, `BroadcastTextID0`, `Probability0`, `BroadcastTextID1`, `Probability1`, `BroadcastTextID2`, `Probability2`, `BroadcastTextID3`, `Probability3`, `BroadcastTextID4`, `Probability4`, `BroadcastTextID5`, `Probability5`, `BroadcastTextID6`, `Probability6`, `BroadcastTextID7`, `Probability7`, `VerifiedBuild`) VALUES\r\n";

                foreach (var npcText in npcTextRowsMerged)
                {
                    outputText += npcText;
                }
            }

            foreach (var gossip in gossipWithNpcTextsDict)
            {
                foreach (var npcTextId in gossip.Value)
                {
                    if (!broadcastTextToNpcTextLinksDict.ContainsKey(npcTextId / 10))
                        continue;

                    string buildedNpcRow = $"({npcTextId}, {npcTextId / 10}, ";

                    for (int i = 0; i < 7; i++)
                    {
                        buildedNpcRow += $"0, 0, ";
                    }

                    buildedNpcRow += ";";

                    buildedNpcRow = buildedNpcRow.Replace("0, ;", "0, -1),") + $" -- Gossip Menu Id: {gossip.Key}\r\n";

                    npcTextRowsSplitted += buildedNpcRow;
                }
            }

            if (npcTextRowsSplitted != "")
            {
                npcTextRowsSplitted = npcTextRowsSplitted.Replace(npcTextRowsSplitted.Split('\n').Where(x => x != "").ToArray().Last(), npcTextRowsSplitted.Split('\n').Where(x => x != "").ToArray().Last().Replace("),", ");"));

                outputText += "\r\n";

                outputText += "DELETE FROM `npc_text` WHERE " + GetDeleteForNpcText(npcTextRowsSplitted);
                outputText += "INSERT INTO `npc_text` (`ID`, `BroadcastTextID0`, `Probability0`, `BroadcastTextID1`, `Probability1`, `BroadcastTextID2`, `Probability2`, `BroadcastTextID3`, `Probability3`, `BroadcastTextID4`, `Probability4`, `BroadcastTextID5`, `Probability5`, `BroadcastTextID6`, `Probability6`, `BroadcastTextID7`, `Probability7`, `VerifiedBuild`) VALUES\r\n";

                foreach (var npcText in npcTextRowsSplitted)
                {
                    outputText += npcText;
                }
            }

            textBox.Text = outputText;
        }

        private static int GetEntryFromLine(string line)
        {
            if (line.Contains("("))
                return Convert.ToInt32(line.Split(',')[0].Replace("(", ""));
            else
                return 0;
        }

        private static int GetBroadcastTextIdFromLine(string line)
        {
            if (line.Contains("("))
                return Convert.ToInt32(line.Split(',')[1].Replace("(", ""));
            else
                return 0;
        }

        private static string GetDeleteLineForGossipMenu(string gossipRows)
        {
            string output = "";

            foreach (string gossip in gossipRows.Split('\r'))
            {
                if (gossip == "\n" || gossip == "")
                    continue;

                string[] splittedGossip = gossip.Split(',');

                if (IsGossipMenuExistsInDb(splittedGossip))
                    continue;

                if (output.Length == 0)
                {
                    output += $"(`entry` = {splittedGossip[0].Replace("(", "")} AND `text_id` ={splittedGossip[1]})";
                }
                else
                {
                    output += $" OR (`entry` = {splittedGossip[0].Replace("(", "")} AND `text_id` ={splittedGossip[1]})";
                }
            }

            output += ";\r\n";

            return output;
        }

        private static bool IsGossipMenuExistsInDb(string[] gossipRow)
        {
            DataSet gossipMenu = SQLModule.DatabaseSelectQuery($"SELECT `entry` FROM `gossip_menu` WHERE `entry` = {gossipRow[0].Replace("(", "")} AND `text_id` = {gossipRow[1]};");
            if (gossipMenu == null || gossipMenu.Tables["table"].Rows.Count == 0)
                return false;

            return true;
        }

        private static string GetDeleteForNpcText(string npcTextRows)
        {
            string output = "";

            foreach (string npcText in npcTextRows.Split('\n'))
            {
                if (npcText == "")
                    continue;

                string[] splittedNpcText = npcText.Split(',');

                if (output.Length == 0)
                {
                    output += $"(`ID` = {splittedNpcText[0].Replace("(", "")} AND `BroadcastTextID0` = {splittedNpcText[1]})";
                }
                else
                {
                    output += $" OR (`ID` = {splittedNpcText[0].Replace("(", "")} AND `BroadcastTextID0` = {splittedNpcText[1]})";
                }
            }

            output += ";\r\n";

            return output;
        }
    }
}
