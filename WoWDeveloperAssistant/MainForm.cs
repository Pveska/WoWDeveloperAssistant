using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WoWDeveloperAssistant.Core_Script_Templates;
using WoWDeveloperAssistant.Database_Advisor;
using WoWDeveloperAssistant.Waypoints_Creator;
using WoWDeveloperAssistant.Achievements;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Conditions_Creator;
using WoWDeveloperAssistant.Parsed_File_Advisor;

namespace WoWDeveloperAssistant
{
    public partial class MainForm : Form
    {
        public bool importSuccessful = false;

        private readonly CreatureScriptsCreator creatureScriptsCreator;
        private readonly WaypointsCreator waypointsCreator;
        private readonly CoreScriptTemplates coreScriptTemplate;
        private static Dictionary<uint, string> creatureNamesDict;
        private static Dictionary<uint, string> questNamesDict;
        private readonly ConditionsCreator conditionsCreator;
        private readonly ParsedFileAdvisor parsedFileAdvisor;

        public MainForm()
        {
            InitializeComponent();

            creatureScriptsCreator = new CreatureScriptsCreator(this);
            waypointsCreator = new WaypointsCreator(this);
            coreScriptTemplate = new CoreScriptTemplates(this);
            conditionsCreator = new ConditionsCreator(this);
            parsedFileAdvisor = new ParsedFileAdvisor(this);

            creatureNamesDict = new Dictionary<uint, string>();
            questNamesDict = new Dictionary<uint, string>();

            if (Properties.Settings.Default.UsingDB)
            {
                creatureNamesDict = Misc.Utils.GetCreatureNamesFromDB();
                questNamesDict = Misc.Utils.GetQuestNamesFromDB();
            }
        }

        public static string GetCreatureNameByEntry(uint creatureEntry)
        {
            if (creatureNamesDict.ContainsKey(creatureEntry))
                return creatureNamesDict[creatureEntry];

            return "Unknown";
        }

        public static string GetQuestNameById(uint questId)
        {
            if (questNamesDict.ContainsKey(questId))
                return questNamesDict[questId];

            return "Unknown";
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

                if (!DB2.Db2.IsLoaded())
                {
                    SetCurrentStatus("Loading DBC...");
                    DB2.Db2.Load();
                }

                if (creatureScriptsCreator.GetDataFromFiles(openFileDialog.FileNames) != 0)
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

            }
        }

        private void toolStripButton_WCLoadSniff_Click(object sender, EventArgs e)
        {
            waypointsCreator.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                waypointsCreator.ImportStarted();

                if (!DB2.Db2.IsLoaded())
                {
                    SetCurrentStatus("Loading DBC...");
                    DB2.Db2.Load();
                }

                if (waypointsCreator.GetDataFromFiles(openFileDialog.FileNames) != 0)
                {
                    waypointsCreator.ImportSuccessful(false);
                }
                else
                {
                    toolStripStatusLabel_CurrentAction.Text = "";
                    toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                    toolStripButton_CSC_ImportSniff.Enabled = true;
                    Cursor = Cursors.Default;
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
            if (Properties.Settings.Default.CheckPathOnDb)
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
            if (ModifierKeys == Keys.Control)
            {
                waypointsCreator.CreateReturnPath(true);
            }
            else
            {
                waypointsCreator.CreateReturnPath();
            }
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
            coreScriptTemplate.CreateTemplate(true);
        }

        private void treeView_CoreScriptTemplates_HookBodies_AfterCheck(object sender, TreeViewEventArgs e)
        {
            coreScriptTemplate.CreateTemplate(true);
        }

        private void TextBox_CoreScriptTemplates_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_CoreScriptTemplates_ObjectId.Text == "" || textBox_CoreScriptTemplates_ObjectId.Text == "0")
                return;

            coreScriptTemplate.CreateTemplate(false);
        }

        private void textBox_CoreScriptTemplates_ObjectId_TextChanged(object sender, EventArgs e)
        {
            coreScriptTemplate.CreateTemplate(true);
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

        private void textBox_ParsedFileAdvisor_SpellDestinations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.ParseSpellDestinations();
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

        private void textBox_ParsedFileAdvisor_PlayerCastedSpells_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.ParsePlayerCastedSpells();
        }

        private void removeGuidsBeforeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveGuidsBeforeSelectedOne();
        }

        private void createReturnPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaypointsHelper.CreateReturnPath(textBox_DatabaseAdvisor_Output);
        }

        private void recalculatePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaypointsHelper.RecalculatePointIds(textBox_DatabaseAdvisor_Output);
        }

        private void createRandomMovementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.AddRandomMovement();
        }

        private void textBox_DatabaseAdvisor_FindDoublePaths_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_FindDoublePaths.Text == "")
                return;

            DoublePathsFinder.FindDoublePaths(textBox_DatabaseAdvisor_Output, textBox_DatabaseAdvisor_FindDoublePaths.Text);
        }

        private void getAddonsFromSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddonsHelper.OpenFileDialog(openFileDialog);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddonsHelper.GetAddonsFromSql(openFileDialog.FileName, textBox_DatabaseAdvisor_Output);
            }
        }

        private void createCoreScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.CreateCoreScript();
        }

        private void textBox_ModifierTrees_ModifierTreeId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_ModifierTrees_ModifierTreeId.Text == "" || textBox_ModifierTrees_ModifierTreeId.Text == "0")
                return;

            treeView_ModifierTrees_ModifierTrees.Nodes.Clear();
            ModifierTreesHandler.ShowModifierTreeRequirements(this);
        }

        private void createLegionCombatAISqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.GenerateCombatAISQL();
        }

        private void toolStripButton_ParsedFileAdvisor_ImportSniff_Click(object sender, EventArgs e)
        {
            parsedFileAdvisor.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                parsedFileAdvisor.ImportStarted();

                if (!DB2.Db2.IsLoaded())
                {
                    DB2.Db2.Load();
                }

                if (parsedFileAdvisor.GetDataFromFiles(openFileDialog.FileNames) != 0)
                {
                    parsedFileAdvisor.ImportSuccessful();
                }
                else
                {
                    parsedFileAdvisor.ImportFailed();
                }
            }
        }

        private void textBox_ParsedFileAdvisor_QuestConversations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.ParseQuestConversations();
        }

        private void textBox_ParsedFileAdvisor_LosConversationsOrTexts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.ParseLosConversationsOrTexts();
        }

        private void recalculateTextForGossipMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NpcTextAdvisor.GetNpcTextForGossipMenu(textBox_DatabaseAdvisor_Output);
        }

        private void getPhaseDataForCreatures_Click(object sender, EventArgs e)
        {
            AddonsHelper.OpenFileDialog(openFileDialog);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PhaseDataAdvisor.GetPhaseDataForCreatures(textBox_DatabaseAdvisor_Output, openFileDialog.FileName);
            }
        }

        private void optimizeCirclePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.OptimizeCirclePath();
        }

        private void optimizeRegularPathToolStripMenuItem_WC_Click(object sender, EventArgs e)
        {
            waypointsCreator.OptimizeRegularPath();
        }

        private void textBox_DatabaseAdvisor_FindDoublePaths_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindDoublePaths.Text == "")
            {
                textBox_DatabaseAdvisor_FindDoublePaths.Text = "Enter Zone Id";
            }
        }

        private void textBox_DatabaseAdvisor_FindDoublePaths_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindDoublePaths.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindDoublePaths.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_FindDoublePaths_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindDoublePaths.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindDoublePaths.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_GossipMenuText_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_GossipMenuText.Text == "")
            {
                textBox_DatabaseAdvisor_GossipMenuText.Text = "Enter Gossip Menu Id";
            }
        }

        private void textBox_DatabaseAdvisor_GossipMenuText_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_GossipMenuText.Text == "Enter Gossip Menu Id")
            {
                textBox_DatabaseAdvisor_GossipMenuText.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_GossipMenuText_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_GossipMenuText.Text == "Enter Gossip Menu Id")
            {
                textBox_DatabaseAdvisor_GossipMenuText.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_QuestFlags_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_QuestFlags.Text == "")
            {
                textBox_DatabaseAdvisor_QuestFlags.Text = "Enter Quest Id";
            }
        }

        private void textBox_DatabaseAdvisor_QuestFlags_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_QuestFlags.Text == "Enter Quest Id")
            {
                textBox_DatabaseAdvisor_QuestFlags.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_QuestFlags_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_QuestFlags.Text == "Enter Quest Id")
            {
                textBox_DatabaseAdvisor_QuestFlags.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_CreatureFlags_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_CreatureFlags.Text == "")
            {
                textBox_DatabaseAdvisor_CreatureFlags.Text = "Enter Creature Entry";
            }
        }

        private void textBox_DatabaseAdvisor_CreatureFlags_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_CreatureFlags.Text == "Enter Creature Entry")
            {
                textBox_DatabaseAdvisor_CreatureFlags.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_CreatureFlags_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_CreatureFlags.Text == "Enter Creature Entry")
            {
                textBox_DatabaseAdvisor_CreatureFlags.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_FindPossibleFormations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_FindPossibleFormations.Text == "")
                return;

            PossibleFormationsFinder.FindPossibleFormations(textBox_DatabaseAdvisor_Output, textBox_DatabaseAdvisor_FindPossibleFormations.Text);
        }

        private void textBox_DatabaseAdvisor_FindPossibleFormations_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindPossibleFormations.Text == "")
            {
                textBox_DatabaseAdvisor_FindPossibleFormations.Text = "Enter Zone Id";
            }
        }

        private void textBox_DatabaseAdvisor_FindPossibleFormations_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindPossibleFormations.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindPossibleFormations.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_FindPossibleFormations_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindPossibleFormations.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindPossibleFormations.Text = "";
            }
        }

        private void reversePointsOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.ReversePointsOrder();
        }

        private void setSelectedPointAsFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.SetSelectedPointAsFirst();
        }

        private void textBox_DatabaseAdvisor_FindWrongAurasInAddons_Click(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text = "";
            }
        }

        private void textBox_DatabaseAdvisor_FindWrongAurasInAddons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text == "")
                return;

            WrongCreatureAddonAurasFinder.FindWrongAurasInCreatureAddons(textBox_DatabaseAdvisor_Output, textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text);
        }

        private void textBox_DatabaseAdvisor_FindWrongAurasInAddons_MouseEnter(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text == "")
            {
                textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text = "Enter Zone Id";
            }
        }

        private void textBox_DatabaseAdvisor_FindWrongAurasInAddons_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text == "Enter Zone Id")
            {
                textBox_DatabaseAdvisor_FindWrongAurasInAddons.Text = "";
            }
        }

        private void textBox_ParsedFileAdvisor_CreatureEquipmentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.GetEquipmentIdForCreature();
        }

        private void grid_WaypointsCreator_Waypoints_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ModifierKeys == Keys.Alt && grid_WaypointsCreator_Waypoints.SelectedRows.Count == 1)
            {
                Clipboard.SetText($".go {grid_WaypointsCreator_Waypoints.SelectedCells[1].Value} {grid_WaypointsCreator_Waypoints.SelectedCells[2].Value} {grid_WaypointsCreator_Waypoints.SelectedCells[3].Value}");
            }
        }

        private void textBox_ParsedFileAdvisor_PlayerCompletedQuests_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            parsedFileAdvisor.GetPlayerCompletedQuests();
        }
    }
}
