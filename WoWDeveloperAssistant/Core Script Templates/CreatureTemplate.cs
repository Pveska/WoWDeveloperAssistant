using System.Data;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Core_Script_Templates
{
    public static class CreatureTemplate
    {
        public static void CreateTemplate(uint objectEntry, ListBox listBox)
        {
            string scriptBody = "";
            string defaultName = "";
            string scriptName = "";

            DataSet creatureNameDs = new DataSet();
            string creatureNameQuery = "SELECT `Name1` FROM `creature_template_wdb` WHERE `entry` = " + objectEntry + ";";
            creatureNameDs = Properties.Settings.Default.UsingDB ? (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery) : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    defaultName = row[0].ToString();
                }
            }

            if (defaultName == "")
                return;

            scriptName = "npc_" + defaultName.Replace(" ", "_").ToLower().Replace("'", "") + "_" + objectEntry;

            scriptBody = "/// " + defaultName + " - " + objectEntry + "\r\n";
            scriptBody += "class " + scriptName + " : public CreatureScript" + "\r\n";
            scriptBody += "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(4) + "public:" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + scriptName + "()" + " : CreatureScript(\"" + scriptName + "\")" + " { }" + "\r\n\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "struct " + scriptName + "AI" + " : public " + (IsVehicleScript(listBox) ? "VehicleAI" : "ScriptedAI") + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(12) + "explicit " + scriptName + "AI" + "(Creature* p_Creature) : " + (IsVehicleScript(listBox) ? "VehicleAI" : "ScriptedAI") + "(p_Creature) { }" + "\r\n";

            uint variablesCount = 0;

            if (HasSummonedByHook(listBox))
            {
                scriptBody += "\r\n" + Utils.AddSpacesCount(12) + "ObjectGuid m_SummonerGuid;";
                variablesCount++;
            }

            if (HasEvents(listBox))
            {
                scriptBody += "\r\n" + Utils.AddSpacesCount(12) + "EventMap m_Events;";
                variablesCount++;
            }

            if (variablesCount != 0)
            {
                scriptBody += "\r\n";
            }

            bool firstHookCheck = false;

            foreach (var item in listBox.SelectedItems)
            {
                if (Hooks.creatureHookDictionary.ContainsKey(item.ToString()))
                {
                    if (firstHookCheck)
                    {
                        scriptBody += "\r\n\r\n" + Hooks.creatureHookDictionary[item.ToString()];
                    }
                    else
                    {
                        scriptBody += "\r\n" + Hooks.creatureHookDictionary[item.ToString()];
                        firstHookCheck = true;
                    }
                }
            }

            scriptBody += "\r\n" + Utils.AddSpacesCount(8) + "};" + "\r\n\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "CreatureAI* GetAI(Creature* p_Creature) const override" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(12) + "return new " + scriptName + "AI(p_Creature);" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "}" + "\r\n";
            scriptBody += "};";

            Clipboard.SetText(scriptBody);
            MessageBox.Show("Template has been successfully builded and copied on your clipboard!");
        }

        private static bool HasSummonedByHook(ListBox listBox)
        {
            foreach (var item in listBox.SelectedItems)
            {
                if (item.ToString() == "IsSummonedBy")
                    return true;
            }

            return false;
        }

        private static bool HasEvents(ListBox listBox)
        {
            uint matchesCount = 0;

            foreach (var item in listBox.SelectedItems)
            {
                if (item.ToString() == "EnterCombat" || item.ToString() == "UpdateAI")
                {
                    matchesCount++;
                }
            }

            return matchesCount == 2;
        }

        private static bool IsVehicleScript(ListBox listBox)
        {
            foreach (var item in listBox.SelectedItems)
            {
                if (item.ToString() == "PassengerBoarded")
                    return true;
            }

            return false;
        }
    }
}
