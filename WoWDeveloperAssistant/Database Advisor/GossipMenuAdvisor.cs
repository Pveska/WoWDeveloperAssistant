using System.Data;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class GossipMenuAdvisor
    {
        public static string GetTextForGossipMenu(string menuId)
        {
            string output = "";

            DataSet broadCastTextIds = SQLModule.WorldSelectQuery("SELECT `BroadcastTextId` FROM `gossip_menu` WHERE `entry` = " + menuId + ";");
            if (broadCastTextIds == null || broadCastTextIds.Tables["table"].Rows.Count == 0)
            {
                MessageBox.Show("There is no gossip menu with this Id in your database!");
                return output;
            }

            if (broadCastTextIds.Tables["table"].Rows.Count > 1)
            {
                foreach (DataRow broadCastTextRow in broadCastTextIds.Tables["table"].Rows)
                {
                    output += "BroadCastText Id: " + broadCastTextRow[0].ToString() + "\r\n";

                    DataSet broadcastTextDs = SQLModule.HotfixSelectQuery("SELECT `Text`, `Text1` FROM `broadcasttext` WHERE `ROW_ID` = " + broadCastTextRow[0].ToString() + ";");
                    if (broadcastTextDs == null || broadcastTextDs.Tables["table"].Rows.Count == 0)
                    {
                        output += "There is no broadcast text with this BroadcastId in your database!";
                        continue;
                    }

                    foreach (string stringRow in broadcastTextDs.Tables["table"].Rows[0].ItemArray)
                    {
                        if (stringRow != "")
                        {
                            output += "- " + stringRow + "\r\n";
                        }
                    }
                }
            }
            else
            {
                output += "BroadCastText Id: " + broadCastTextIds.Tables["table"].Rows[0][0].ToString() + "\r\n";

                DataSet broadcastTextDs = SQLModule.HotfixSelectQuery("SELECT `Text`, `Text1` FROM `broadcasttext` WHERE `ROW_ID` = " + broadCastTextIds.Tables["table"].Rows[0][0].ToString() + ";");
                if (broadcastTextDs == null || broadcastTextDs.Tables["table"].Rows.Count == 0)
                {
                    output += "There is no broadcast text with this BroadcastId in your database!";
                }
                else
                {
                    foreach (string stringRow in broadcastTextDs.Tables["table"].Rows[0].ItemArray)
                    {
                        if (stringRow != "")
                        {
                            output += "- " + stringRow + "\r\n";
                        }
                    }
                }
            }

            return output;
        }
    }
}
