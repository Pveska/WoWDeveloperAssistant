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

            scriptName = "npc_" + defaultName.Replace(" ", "_").ToLower() + "_" + objectEntry;

            scriptBody = "/// " + defaultName + " - " + objectEntry + "\n";
            scriptBody += "class " + scriptName + " : public CreatureScript" + "\n";
            scriptBody += "{\n";
            scriptBody += Utils.AddSpacesCount(4) + "public:\n";
            scriptBody += Utils.AddSpacesCount(8) + scriptName + "()" + " : CreatureScript(\"" + scriptName + "\")" + " { }" + "\n\n";
            scriptBody += Utils.AddSpacesCount(8) + "struct " + scriptName + "AI" + " : public " + (IsVehicleScript(listBox) ? "VehicleAI" : "ScriptedAI") + "\n";
            scriptBody += Utils.AddSpacesCount(8) + "{\n";
            scriptBody += Utils.AddSpacesCount(12) + "explicit " + scriptName + "AI" + "(Creature* p_Creature) : " + (IsVehicleScript(listBox) ? "VehicleAI" : "ScriptedAI") + "(p_Creature) { }" + "\n";

            if (HasSummonedByHook(listBox))
            {
                scriptBody += "\n" + Utils.AddSpacesCount(12) + "ObjectGuid m_SummonerGuid;";
            }

            if (HasEvents(listBox))
            {
                scriptBody += "\n" + Utils.AddSpacesCount(12) + "EventMap m_Events;";
            }

            foreach (var item in listBox.SelectedItems)
            {
                if (Hooks.creatureHookDictionary.ContainsKey(item.ToString()))
                {
                    scriptBody += "\n\n" + Hooks.creatureHookDictionary[item.ToString()];
                }
            }

            scriptBody += "\n" + Utils.AddSpacesCount(8) + "};\n\n";
            scriptBody += Utils.AddSpacesCount(8) + "CreatureAI* GetAI(Creature* p_Creature) const override" + "\n";
            scriptBody += Utils.AddSpacesCount(8) + "{\n";
            scriptBody += Utils.AddSpacesCount(12) + "return new " + scriptName + "AI(p_Creature);" + "\n";
            scriptBody += Utils.AddSpacesCount(8) + "}\n";
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
