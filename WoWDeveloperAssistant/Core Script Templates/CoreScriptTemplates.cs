using System;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Core_Script_Templates
{
    public class CoreScriptTemplates
    {
        private MainForm mainForm;

        public CoreScriptTemplates(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        private enum ScriptTypes
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
            mainForm.listBox_CoreScriptTemplates_Hooks.Items.Clear();

            switch (GetScriptType(mainForm.comboBox_CoreScriptTemplates_ScriptType.SelectedIndex))
            {
                case ScriptTypes.Creature:
                {
                    foreach (var key in CreatureScriptTemplate.hooksDictionary.Keys)
                    {
                        mainForm.listBox_CoreScriptTemplates_Hooks.Items.Add(key);
                    }

                    break;
                }
                case ScriptTypes.Spell:
                {
                    foreach (var key in SpellScriptTemplate.hooksDictionary.Keys)
                    {
                        mainForm.listBox_CoreScriptTemplates_Hooks.Items.Add(key);
                    }

                    break;
                }
            }
        }

        public void FillTreeWithHookBodies()
        {
            int index = 0;
            TreeView treeView = mainForm.treeView_CoreScriptTemplates_HookBodies;
            treeView.Nodes.Clear();

            switch (GetScriptType(mainForm.comboBox_CoreScriptTemplates_ScriptType.SelectedIndex))
            {
                case ScriptTypes.Creature:
                {
                    foreach (var hook in mainForm.listBox_CoreScriptTemplates_Hooks.SelectedItems)
                    {
                        string hookName = hook.ToString();

                        if (!CreatureScriptTemplate.hookBodiesDictionary.ContainsKey(hookName))
                            continue;

                        treeView.Nodes.Add(new TreeNode(hookName));

                        foreach (var item in CreatureScriptTemplate.hookBodiesDictionary[hookName])
                        {
                            treeView.Nodes[index].Nodes.Add(item.Key);
                        }

                        index++;
                    }

                    treeView.ExpandAll();
                    break;
                }
                case ScriptTypes.Spell:
                {
                    foreach (var hook in mainForm.listBox_CoreScriptTemplates_Hooks.SelectedItems)
                    {
                        string hookName = hook.ToString();

                        if (!SpellScriptTemplate.hookBodiesDictionary.ContainsKey(hookName))
                            continue;

                        treeView.Nodes.Add(new TreeNode(hookName));

                        foreach (var item in SpellScriptTemplate.hookBodiesDictionary[hookName])
                        {
                            treeView.Nodes[index].Nodes.Add(item);
                        }

                        index++;
                    }

                    treeView.ExpandAll();
                    break;
                }
            }
        }

        public void CreateTemplate(bool preview)
        {
            if (string.IsNullOrWhiteSpace(mainForm.textBox_CoreScriptTemplates_ObjectId.Text))
                return;

            uint objectEntry = Convert.ToUInt32(mainForm.textBox_CoreScriptTemplates_ObjectId.Text);
            string previewText = preview ? "true" : "";

            switch (GetScriptType(mainForm.comboBox_CoreScriptTemplates_ScriptType.SelectedIndex))
            {
                case ScriptTypes.Creature:
                {
                    CreatureScriptTemplate.CreateTemplate(objectEntry, mainForm.listBox_CoreScriptTemplates_Hooks, mainForm.treeView_CoreScriptTemplates_HookBodies, ref previewText);
                    break;
                }
                case ScriptTypes.Spell:
                {
                    SpellScriptTemplate.CreateTemplate(objectEntry, mainForm.listBox_CoreScriptTemplates_Hooks, mainForm.treeView_CoreScriptTemplates_HookBodies, ref previewText);
                    break;
                }
            }

            this.mainForm.Core_script_creatore_preview.ResetText();
            this.mainForm.Core_script_creatore_preview.Text = previewText;
        }

        private static ScriptTypes GetScriptType(int selectedIndex)
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
