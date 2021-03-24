using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WoWDeveloperAssistant.Core_Script_Templates;
using WoWDeveloperAssistant.Database_Advisor;
using WoWDeveloperAssistant.Waypoints_Creator;
using WoWDeveloperAssistant.Achievements;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Conditions_Creator;

namespace WoWDeveloperAssistant
{
    public partial class MainForm : Form
    {
        public bool importSuccessful = false;

        private CreatureScriptsCreator creatureScriptsCreator;
        private WaypointsCreator waypointsCreator;
        private CoreScriptTemplates coreScriptTemplate;
        private static Dictionary<uint, string> creatureNamesDict;
        private ConditionsCreator conditionsCreator;

        public MainForm()
        {
            InitializeComponent();

            creatureScriptsCreator = new CreatureScriptsCreator(this);
            waypointsCreator = new WaypointsCreator(this);
            coreScriptTemplate = new CoreScriptTemplates(this);
            conditionsCreator = new ConditionsCreator(this);

            creatureNamesDict = new Dictionary<uint, string>();

            if (Properties.Settings.Default.UsingDB)
            {
                creatureNamesDict = GetCreatureNamesFromDB();
            }
        }

        private Dictionary<uint, string> GetCreatureNamesFromDB()
        {
            Dictionary<uint, string> namesDict = new Dictionary<uint, string>();

            string creatureNameQuery = "SELECT `entry`, `Name1` FROM `creature_template_wdb`;";
            var creatureNameDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(creatureNameQuery) : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    namesDict.Add((uint)row[0], row[1].ToString());
                }
            }

            return namesDict;
        }

        public static string GetCreatureNameByEntry(uint creatureEntry)
        {
            if (creatureNamesDict.ContainsKey(creatureEntry))
                return creatureNamesDict[creatureEntry];

            return "Unknown";
        }

        private static bool IsTxtFileValidForParse(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                return true;
            }

            MessageBox.Show(fileName + " is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            return false;
        }

        private void createSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_CreatureScriptsCreator_Spells.Rows.Count > 0)
            {
                creatureScriptsCreator.FillSQLOutput();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_CreatureScriptsCreator_Spells.SelectedRows)
            {
                dataGridView_CreatureScriptsCreator_Spells.Rows.Remove(row);
            }
        }

        private void toolStripButton_ImportSniff_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                creatureScriptsCreator.ImportStarted();

                if (!DBC.DBC.IsLoaded())
                {
                    SetCurrentStatus("Loading DBC...");
                    DBC.DBC.Load();
                }

                if (openFileDialog.FileName.Contains("txt"))
                {
                    if (IsTxtFileValidForParse(openFileDialog.FileName) &&
                    creatureScriptsCreator.GetDataFromSniffFile(openFileDialog.FileName))
                    {
                        creatureScriptsCreator.ImportSuccessful();
                    }
                    else
                    {
                        toolStripStatusLabel_CurrentAction.Text = "";
                        toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                        toolStripButton_CSC_ImportSniff.Enabled = true;
                        Cursor = Cursors.Default;
                    }
                }
                else if (openFileDialog.FileName.Contains("dat"))
                {
                    if (creatureScriptsCreator.GetPacketsFromDataFile(openFileDialog.FileName))
                    {
                        creatureScriptsCreator.ImportSuccessful();
                    }
                }
            }
        }

        private void toolStripButton_Search_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.FillListBoxWithGuids();
        }

        private void toolStripTextBox_CSC_CreatureEntrySearch_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            creatureScriptsCreator.FillListBoxWithGuids();
        }

        private void listBox_CreatureGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            creatureScriptsCreator.FillSpellsGrid();
        }

        private void textBox_CreatureFlags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreatureFlagsAdvisor.GetCreatureFlags(textBox_DatabaseAdvisor_CreatureFlags.Text);
            }
        }

        private void textBox_QuestFlags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuestFlagsAdvisor.GetQuestFlags(textBox_DatabaseAdvisor_QuestFlags.Text);
            }
        }

        private void button_ImportFile_Click(object sender, EventArgs e)
        {
            DoubleSpawnsRemover.OpenFileDialog(openFileDialog);

            this.Cursor = Cursors.WaitCursor;
            button_DoubleSpawnsRemover_ImportFile.Enabled = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoubleSpawnsRemover.RemoveDoubleSpawnsFromFile(openFileDialog.FileName, label_DoubleSpawnsRemover_CreaturesRemoved, label_DoubleSpawnsRemover_GameobjectsRemoved, checkBox_DoubleSpawnsRemover_Creatures.Checked, checkBox_DoubleSpawnsRemover_Gameobjects.Checked, toolStripStatusLabel_FileStatus, this);
                toolStripStatusLabel_FileStatus.Text =openFileDialog.FileName + " is selected for input.";
                button_DoubleSpawnsRemover_ImportFile.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            else
            {
                label_DoubleSpawnsRemover_CreaturesRemoved.Text = "No creatures removed";
                label_DoubleSpawnsRemover_GameobjectsRemoved.Text = "No gameobjects removed";
            }

            button_DoubleSpawnsRemover_ImportFile.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void checkBox_CreaturesRemover_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DoubleSpawnsRemover_Creatures.Checked)
            {
                button_DoubleSpawnsRemover_ImportFile.Enabled = true;
            }
            else if (!checkBox_DoubleSpawnsRemover_Creatures.Checked && !checkBox_DoubleSpawnsRemover_Gameobjects.Checked)
            {
                button_DoubleSpawnsRemover_ImportFile.Enabled = false;
            }
        }

        private void checkBox_GameobjectsRemover_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DoubleSpawnsRemover_Gameobjects.Checked)
            {
                button_DoubleSpawnsRemover_ImportFile.Enabled = true;
            }
            else if (!checkBox_DoubleSpawnsRemover_Gameobjects.Checked && !checkBox_DoubleSpawnsRemover_Creatures.Checked)
            {
                button_DoubleSpawnsRemover_ImportFile.Enabled = false;
            }
        }

        private void textBoxAreatriggerSplines_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AreatriggerSplineCreator.OpenFileDialog(openFileDialog);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AreatriggerSplineCreator.ParseSplinesForAreatrigger(openFileDialog.FileName, textBox_DatabaseAdvisor_AreatriggerSplines.Text);
                }
            }
        }

        private void toolStripButton_WCLoadSniff_Click(object sender, EventArgs e)
        {
            waypointsCreator.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                waypointsCreator.ImportStarted();

                if (!DBC.DBC.IsLoaded())
                {
                    SetCurrentStatus("Loading DBC...");
                    DBC.DBC.Load();
                }

                if (openFileDialog.FileName.Contains("txt"))
                {
                    if (IsTxtFileValidForParse(openFileDialog.FileName) &&
                    waypointsCreator.GetDataFromSniffFile(openFileDialog.FileName))
                    {
                        waypointsCreator.ImportSuccessful();
                    }
                    else
                    {
                        toolStripStatusLabel_CurrentAction.Text = "";
                        toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                        toolStripButton_CSC_ImportSniff.Enabled = true;
                        Cursor = Cursors.Default;
                    }
                }
                else if (openFileDialog.FileName.Contains("dat"))
                {
                    if (waypointsCreator.GetPacketsFromDataFile(openFileDialog.FileName))
                    {
                        waypointsCreator.ImportSuccessful();
                    }
                }
            }
        }

        private void toolStripButton_WCSettings_Click(object sender, EventArgs e)
        {
            Form settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void toolStripButton_WCSearch_Click(object sender, EventArgs e)
        {
            waypointsCreator.FillListBoxWithGuids();
        }

        private void toolStripTextBox_WCSearch_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            waypointsCreator.FillListBoxWithGuids();
        }

        private void listBox_WCCreatureGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CheckDataOnDb)
            {
                waypointsCreator.RemoveGuidsWithExistingDataFromListBox();
            }

            waypointsCreator.FillWaypointsGrid();
        }

        private void createSQLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            waypointsCreator.CreateSQL();
        }

        private void removeExcessPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveNearestPoints();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            waypointsCreator.CutFromGrid();
        }

        private void removeDuplicatePointsToolStripMenuItem_WC_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveDuplicatePoints();
        }

        private void createReturnPathToolStripMenuItem_WC_Click(object sender, EventArgs e)
        {
            waypointsCreator.CreateReturnPath();
        }

        public void SetCurrentStatus(string status)
        {
            toolStripStatusLabel_CurrentAction.Text = "Current Status: " + status;
            Update();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            coreScriptTemplate.FillBoxWithHooks();
        }

        private void ListBox_CoreScriptTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_CoreScriptTemplates_ObjectId.Enabled = true;
            coreScriptTemplate.FillTreeWithHookBodies();
        }

        private void TextBox_CoreScriptTemplates_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_CoreScriptTemplates_ObjectId.Text == "" || textBox_CoreScriptTemplates_ObjectId.Text == "0")
                return;

            coreScriptTemplate.CreateTemplate();
        }

        private void TextBoxAchievements_Id_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_Achievements_AchievementId.Text == "" || textBox_Achievements_AchievementId.Text == "0")
                return;

            treeView_Achievements_ChildNodes.Nodes.Clear();
            treeView_Achievements_Criterias.Nodes.Clear();
            treeView_Achievements_ModifierTrees.Nodes.Clear();
            treeView_Achievements_ModifierTreeChildNodes.Nodes.Clear();
            AchievementsHandler.ShowAchievementRequirements(this);
        }

        private void TreeView_Achievements_ChildNodes_AfterExpand(object sender, TreeViewEventArgs e)
        {
            uint expandedNodesCount = 0;

            foreach (TreeNode node in treeView_Achievements_ChildNodes.Nodes)
            {
                if (node.IsExpanded)
                {
                    expandedNodesCount++;
                }
            }

            treeView_Achievements_Criterias.Nodes.Clear();
            AchievementsHandler.FillTreeWithCriterias(Convert.ToUInt32(e.Node.Text), treeView_Achievements_Criterias, expandedNodesCount > 1);
        }

        private void TreeView_Achievements_ChildNodes_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            treeView_Achievements_Criterias.Nodes.Clear();
            treeView_Achievements_ModifierTrees.Nodes.Clear();
            treeView_Achievements_ModifierTreeChildNodes.Nodes.Clear();
        }

        private void TreeView_Achievements_Criterias_AfterExpand(object sender, TreeViewEventArgs e)
        {
            uint expandedNodesCount = 0;

            foreach (TreeNode node in treeView_Achievements_Criterias.Nodes)
            {
                if (node.IsExpanded)
                {
                    expandedNodesCount++;
                }
            }

            treeView_Achievements_ModifierTrees.Nodes.Clear();
            AchievementsHandler.FillTreeWithModifiers(Convert.ToUInt32(e.Node.Text), treeView_Achievements_ModifierTrees, expandedNodesCount > 1);
        }

        private void TreeView_Achievements_Criterias_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            treeView_Achievements_ModifierTrees.Nodes.Clear();
            treeView_Achievements_ModifierTreeChildNodes.Nodes.Clear();
        }

        private void TreeView_Achievements_ModifierTrees_AfterExpand(object sender, TreeViewEventArgs e)
        {
            treeView_Achievements_ModifierTreeChildNodes.Nodes.Clear();
            AchievementsHandler.FillTreeWithModifiersChildNodes(Convert.ToUInt32(e.Node.Text), treeView_Achievements_ModifierTreeChildNodes);
        }

        private void TreeView_Achievements_ModifierTrees_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            treeView_Achievements_ModifierTreeChildNodes.Nodes.Clear();
        }

        private void textBox_SpellDestinations_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_SpellDestinations.Text == "" || textBox_DatabaseAdvisor_SpellDestinations.Text == "0")
                return;

            SpellDestinationsParser.OpenFileDialog(openFileDialog);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SpellDestinationsParser.ParseSpellDestinations(openFileDialog.FileName, textBox_DatabaseAdvisor_SpellDestinations.Text);
            }
        }

        private void comboBox_ConditionSourceType_DropDown(object sender, EventArgs e)
        {
            if (comboBox_ConditionsCreator_ConditionSourceType.Items.Count == 0)
            {
                comboBox_ConditionsCreator_ConditionSourceType.Items.AddRange(Enum.GetNames(typeof(Conditions.ConditionSourceTypes)));
            }
        }

        private void comboBox_ConditionType_DropDown(object sender, EventArgs e)
        {
            if (comboBox_ConditionsCreator_ConditionType.Items.Count == 0)
            {
                comboBox_ConditionsCreator_ConditionType.Items.AddRange(Enum.GetNames(typeof(Conditions.ConditionTypes)));
            }
        }

        private void comboBox_ConditionSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            conditionsCreator.ClearConditions();
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionSourceType.SelectedItem.ToString(), textBox_ConditionsCreator_SourceGroup);
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionSourceType.SelectedItem.ToString(), textBox_ConditionsCreator_SourceEntry);
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionSourceType.SelectedItem.ToString(), textBox_ConditionsCreator_SourceId);

            if (comboBox_ConditionsCreator_ConditionType.SelectedItem != null)
            {
                conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionSourceType.SelectedItem.ToString(), textBox_ConditionsCreator_ConditionTarget);
            }

            textBox_ConditionsCreator_ElseGroup.Enabled = true;
            comboBox_ConditionsCreator_ConditionType.Enabled = true;
        }

        private void comboBox_ConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ConditionsCreator_ConditionSourceType.SelectedItem == null)
                return;

            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionType.SelectedItem.ToString(), textBox_ConditionsCreator_ConditionValue1);
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionType.SelectedItem.ToString(), textBox_ConditionsCreator_ConditionValue2);
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionType.SelectedItem.ToString(), textBox_ConditionsCreator_ConditionValue3);
            conditionsCreator.ChangeTextBoxAccessibility(comboBox_ConditionsCreator_ConditionSourceType.SelectedItem.ToString(), textBox_ConditionsCreator_ConditionTarget);
            textBox_ConditionsCreator_NegativeCondition.Enabled = true;
            textBox_ConditionsCreator_ScriptName.Enabled = true;
            button_ConditionsCreator_AddCondition.Enabled = true;
            button_ConditionsCreator_ClearConditions.Enabled = true;
        }

        private void button_AddCondition_Click(object sender, EventArgs e)
        {
            if (comboBox_ConditionsCreator_ConditionSourceType.SelectedItem == null || comboBox_ConditionsCreator_ConditionType.SelectedItem == null)
                return;

            conditionsCreator.CreateCondition();
            textBox_ConditionsCreator_Output.Enabled = true;
        }

        private void button_ClearConditions_Click(object sender, EventArgs e)
        {
            conditionsCreator.ClearConditions();
        }

        private void textBox_GossipMenuText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_GossipMenuText.Text == "" || textBox_DatabaseAdvisor_GossipMenuText.Text == "0")
                return;

            textBox_DatabaseAdvisor_Output.Text = GossipMenuAdvisor.GetTextForGossipMenu(textBox_DatabaseAdvisor_GossipMenuText.Text);
        }

        private void textBox_PlayerCastedSpells_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_PlayerCastedSpells.Text == "")
                return;

            PlayerCastedSpellsParser.OpenFileDialog(openFileDialog);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_DatabaseAdvisor_Output.Text = PlayerCastedSpellsParser.ParsePlayerCastedSpells(openFileDialog.FileName, textBox_DatabaseAdvisor_PlayerCastedSpells.Text);
            }
        }

        private void removeGuidsBeforeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveGuidsBeforeSelectedOne();
        }
    }
}
