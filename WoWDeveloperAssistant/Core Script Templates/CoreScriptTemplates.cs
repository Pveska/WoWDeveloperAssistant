using System;

namespace WoWDeveloperAssistant.Core_Script_Templates
{
    public class CoreScriptTemplates
    {
        private MainForm mainForm;

        public CoreScriptTemplates(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        private enum ScriptTypes : int
        {
            Creature     = 0,
            GameObject   = 1,
            AreaTrigger  = 2,
            Spell        = 3,
            PlayerScript = 4,
            Unknown      = 5
        };

        public void FillBoxWithHooks()
        {
            mainForm.listBox_CoreScriptTemplates.Items.Clear();

            switch (GetScriptType(mainForm.comboBox_CoreScriptTemplates.SelectedIndex))
            {
                case ScriptTypes.Creature:
                {
                    foreach (var key in Hooks.creatureHookDictionary.Keys)
                    {
                        mainForm.listBox_CoreScriptTemplates.Items.Add(key);
                    }

                    break;
                }
            }
        }

        public void CreateTemplate()
        {
            uint objectEntry = Convert.ToUInt32(mainForm.textBox_CoreScriptTemplates.Text);

            switch (GetScriptType(mainForm.comboBox_CoreScriptTemplates.SelectedIndex))
            {
                case ScriptTypes.Creature:
                {
                    CreatureTemplate.CreateTemplate(objectEntry, mainForm.listBox_CoreScriptTemplates);
                    break;
                }
            }
        }

        private ScriptTypes GetScriptType(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return ScriptTypes.Creature;
                case 1:
                    return ScriptTypes.GameObject;
                case 2:
                    return ScriptTypes.AreaTrigger;
                case 3:
                    return ScriptTypes.Spell;
                case 4:
                    return ScriptTypes.PlayerScript;
                default:
                    return ScriptTypes.Unknown;
            }
        }
    }
}
