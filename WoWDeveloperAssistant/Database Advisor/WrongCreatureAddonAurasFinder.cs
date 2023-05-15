using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class WrongCreatureAddonAurasFinder
    {
        public static void FindWrongAurasInCreatureAddons(TextBox textBox, string zoneId)
        {
            string output = "";

            Dictionary<string, string> creatureAddons = new Dictionary<string, string>();

            if (Properties.Settings.Default.UsingDB)
            {
                DataSet creatureAddonsDs = SQLModule.WorldSelectQuery($"SELECT `linked_id`, `auras` FROM `creature_addon` WHERE `linked_id` IN (SELECT `linked_id` FROM `creature` WHERE `zoneid` = {zoneId}) AND `auras` != '';");

                if (creatureAddonsDs != null && creatureAddonsDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in creatureAddonsDs.Tables["table"].Rows)
                    {
                        creatureAddons.Add((string)row.ItemArray[0], (string)row.ItemArray[1]);
                    }
                }
            }
            else
            {
                textBox.Text = "Can't find any creature addon in this zone!";
                return;
            }

            if (!DB2.Db2.IsLoaded())
            {
                DB2.Db2.Load();
            }

            foreach (var addon in creatureAddons)
            {
                string[] auras = addon.Value.Split(' ');

                try
                {
                    if (auras.Count(x => DB2.Db2.SpellDurationStore[Convert.ToInt32(x)] != -1) != 0)
                    {
                        string updateString = "UPDATE `creature_addon` SET `auras` = '";
                        string commentString = "-- Removed auras: ";

                        foreach (string auraToAdd in auras.Where(x => DB2.Db2.SpellDurationStore[Convert.ToInt32(x)] == -1))
                        {
                            int auraId = Convert.ToInt32(auraToAdd);
                            updateString += $"{auraToAdd} ";
                        }

                        if (updateString != "UPDATE `creature_addon` SET `auras` = '")
                        {
                            updateString = updateString.Remove(updateString.Length - 1);
                        }

                        updateString += $"' WHERE `linked_id` = '{addon.Key}';";

                        foreach (string auraToRemove in auras.Where(x => DB2.Db2.SpellDurationStore[Convert.ToInt32(x)] != -1))
                        {
                            int auraId = Convert.ToInt32(auraToRemove);
                            commentString += $"{auraId} - ({DB2.Db2.SpellName[auraId].Name}), ";
                        }

                        commentString = commentString.Remove(commentString.Length - 2);
                        commentString += "\r\n";

                        updateString += $" {commentString}";
                        output += updateString;
                    }
                }
                catch (Exception) {}
            }

            textBox.Text = output;
        }
    }
}
