namespace WoWDeveloperAssistant
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_CreatureScriptsCreator = new System.Windows.Forms.TabPage();
            this.checkBox_CreatureScriptsCreator_CreateDataFile = new System.Windows.Forms.CheckBox();
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells = new System.Windows.Forms.CheckBox();
            this.dataGridView_CreatureScriptsCreator_Spells = new System.Windows.Forms.DataGridView();
            this.SpellId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CastTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinCastStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxCastStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinCastRepeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxCastRepeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CastsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceSpell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_CreatureScriptsCreator = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.createSmartAISqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createLegionCombatAISqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCoreScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_CreatureScriptCreator_CreatureGuids = new System.Windows.Forms.ListBox();
            this.toolStrip_CreatureScriptsCreator = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_CSC_ImportSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CreatureScriptsCreator_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator_CSC = new System.Windows.Forms.ToolStripSeparator();
            this.tabPage_WaypointsCreator = new System.Windows.Forms.TabPage();
            this.checkBox_WaypointsCreator_CreateDataFile = new System.Windows.Forms.CheckBox();
            this.grid_WaypointsCreator_Waypoints = new System.Windows.Forms.DataGridView();
            this.gridColumn_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_Orientation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_WCTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_WCDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_HasScript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaypointSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_WaypointsCreator = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNearestPointsToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDuplicatePointsToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.createReturnPathToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.optimizeCirclePathToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.optimizeRegularPathToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.reversePointsOrderToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.setSelectedPointAsFirstToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_WC = new System.Windows.Forms.ToolStripSeparator();
            this.createSQLToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_WaypointsCreator_CreatureGuids = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_WaypointsCreator_Guids = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeGuidsBeforeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRandomMovementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart_WaypointsCreator_Path = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip_WaypointsCreator = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_WaypointsCreator_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_WaypointsCreator_Entry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_WaypointsCreator_Entry = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_WaypointsCreator_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_WaypointsCreator_LoadSniff = new System.Windows.Forms.ToolStripButton();
            this.tabPage_SqlOutput = new System.Windows.Forms.TabPage();
            this.textBox_SqlOutput = new System.Windows.Forms.TextBox();
            this.tabPage_DatabaseAdvisor = new System.Windows.Forms.TabPage();
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_FindWrongAurasInAddons = new System.Windows.Forms.Label();
            this.textBox_DatabaseAdvisor_FindPossibleFormations = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_FindPossibleFormations = new System.Windows.Forms.Label();
            this.textBox_DatabaseAdvisor_FindDoublePaths = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_FindDoublePaths = new System.Windows.Forms.Label();
            this.textBox_DatabaseAdvisor_Output = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_DatabaseAdvisor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createReturnPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalculatePointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAddonsFromSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalculateTextForGossipMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getPhaseDataForCreatures = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_DatabaseAdvisor_GossipMenuText = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_GossipMenuText = new System.Windows.Forms.Label();
            this.textBox_DatabaseAdvisor_QuestFlags = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_QuestFlags = new System.Windows.Forms.Label();
            this.textBox_DatabaseAdvisor_CreatureFlags = new System.Windows.Forms.TextBox();
            this.label_DatabaseAdvisor_CreatureFlags = new System.Windows.Forms.Label();
            this.tabPage_DoubleSpawnsRemover = new System.Windows.Forms.TabPage();
            this.label_DoubleSpawnsRemover_GameobjectsRemoved = new System.Windows.Forms.Label();
            this.checkBox_DoubleSpawnsRemover_Gameobjects = new System.Windows.Forms.CheckBox();
            this.checkBox_DoubleSpawnsRemover_Creatures = new System.Windows.Forms.CheckBox();
            this.label_DoubleSpawnsRemover_CreaturesRemoved = new System.Windows.Forms.Label();
            this.button_DoubleSpawnsRemover_ImportFile = new System.Windows.Forms.Button();
            this.tabPage_CoreScriptTemplates = new System.Windows.Forms.TabPage();
            this.Core_script_creatore_preview = new System.Windows.Forms.RichTextBox();
            this.treeView_CoreScriptTemplates_HookBodies = new System.Windows.Forms.TreeView();
            this.label_CoreScriptTemplates_ScriptType = new System.Windows.Forms.Label();
            this.comboBox_CoreScriptTemplates_ScriptType = new System.Windows.Forms.ComboBox();
            this.label_CoreScriptTemplates_ObjectId = new System.Windows.Forms.Label();
            this.textBox_CoreScriptTemplates_ObjectId = new System.Windows.Forms.TextBox();
            this.listBox_CoreScriptTemplates_Hooks = new System.Windows.Forms.ListBox();
            this.tabPage_Achievements = new System.Windows.Forms.TabPage();
            this.label_Achievements_ModifierTreeChildNodes = new System.Windows.Forms.Label();
            this.treeView_Achievements_ModifierTreeChildNodes = new System.Windows.Forms.TreeView();
            this.label_Achievements_ModifierTrees = new System.Windows.Forms.Label();
            this.treeView_Achievements_ModifierTrees = new System.Windows.Forms.TreeView();
            this.label_Achievements_Criterias = new System.Windows.Forms.Label();
            this.treeView_Achievements_Criterias = new System.Windows.Forms.TreeView();
            this.label_Achievements_CriteriaTreeAmount = new System.Windows.Forms.Label();
            this.label_Achievements_CriteriaTreeChildNodes = new System.Windows.Forms.Label();
            this.label_Achievement_CriteriaTreeOperator = new System.Windows.Forms.Label();
            this.label_Achievements_CriteriaTreeName = new System.Windows.Forms.Label();
            this.label_Achievements_CriteriaTreeId = new System.Windows.Forms.Label();
            this.label_Achievements_AchievementFlags = new System.Windows.Forms.Label();
            this.label_Achievements_AchievementFaction = new System.Windows.Forms.Label();
            this.treeView_Achievements_ChildNodes = new System.Windows.Forms.TreeView();
            this.label_Achievements_AchievementName = new System.Windows.Forms.Label();
            this.textBox_Achievements_AchievementId = new System.Windows.Forms.TextBox();
            this.label_Achievements_AchievementId = new System.Windows.Forms.Label();
            this.tabPage_ConditionsCreator = new System.Windows.Forms.TabPage();
            this.button_ConditionsCreator_ClearConditions = new System.Windows.Forms.Button();
            this.button_ConditionsCreator_AddCondition = new System.Windows.Forms.Button();
            this.textBox_ConditionsCreator_Output = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_ScriptName = new System.Windows.Forms.Label();
            this.label_ConditionsCreator_NegativeCondition = new System.Windows.Forms.Label();
            this.label_ConditionsCreator_ConditionValue3 = new System.Windows.Forms.Label();
            this.label_ConditionsCreator_ConditionValue2 = new System.Windows.Forms.Label();
            this.label_ConditionsCreator_ConditionValue1 = new System.Windows.Forms.Label();
            this.textBox_ConditionsCreator_ScriptName = new System.Windows.Forms.TextBox();
            this.textBox_ConditionsCreator_NegativeCondition = new System.Windows.Forms.TextBox();
            this.textBox_ConditionsCreator_ConditionValue3 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionsCreator_ConditionValue2 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionsCreator_ConditionValue1 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionsCreator_ConditionTarget = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_ConditionTarget = new System.Windows.Forms.Label();
            this.label_ConditionsCreator_ConditionType = new System.Windows.Forms.Label();
            this.comboBox_ConditionsCreator_ConditionType = new System.Windows.Forms.ComboBox();
            this.textBox_ConditionsCreator_ElseGroup = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_ElseGroup = new System.Windows.Forms.Label();
            this.textBox_ConditionsCreator_SourceId = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_SourceId = new System.Windows.Forms.Label();
            this.textBox_ConditionsCreator_SourceEntry = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_SourceEntry = new System.Windows.Forms.Label();
            this.textBox_ConditionsCreator_SourceGroup = new System.Windows.Forms.TextBox();
            this.label_ConditionsCreator_ConditionSourceGroup = new System.Windows.Forms.Label();
            this.comboBox_ConditionsCreator_ConditionSourceType = new System.Windows.Forms.ComboBox();
            this.label_ConditionsCreator_ConditionSourceType = new System.Windows.Forms.Label();
            this.tabPage_ModifierTrees = new System.Windows.Forms.TabPage();
            this.label_ModifierTrees_TertiaryAsset = new System.Windows.Forms.Label();
            this.label_ModifierTrees_SecondaryAsset = new System.Windows.Forms.Label();
            this.label_ModifierTrees_Asset = new System.Windows.Forms.Label();
            this.label_ModifierTrees_Amount = new System.Windows.Forms.Label();
            this.label_ModifierTrees_Operator = new System.Windows.Forms.Label();
            this.label_ModifierTrees_Type = new System.Windows.Forms.Label();
            this.textBox_ModifierTrees_ModifierTreeId = new System.Windows.Forms.TextBox();
            this.label_ModifierTrees_ModifierTreeId = new System.Windows.Forms.Label();
            this.label_ModifierTrees_ModifierTrees = new System.Windows.Forms.Label();
            this.treeView_ModifierTrees_ModifierTrees = new System.Windows.Forms.TreeView();
            this.tabPage_ParsedFileAdvisor = new System.Windows.Forms.TabPage();
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_PlayerCompletedQuests = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_CreatureEquipmentId = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_LosConversationsOrTexts = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_SpellDestinations = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_SpellDestinations = new System.Windows.Forms.Label();
            this.checkBox_ParsedFileAdvisor_CreateDataFile = new System.Windows.Forms.CheckBox();
            this.toolStrip_ParsedFileAdvisor = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripButton_ParsedFileAdvisor_ImportSniff = new System.Windows.Forms.ToolStripButton();
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_PlayerCastedSpells = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines = new System.Windows.Forms.TextBox();
            this.label_ParsedFileAdvisor_AreaTriggerSplines = new System.Windows.Forms.Label();
            this.textBox_ParsedFileAdvisor_Output = new System.Windows.Forms.TextBox();
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_CurrentAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_CreatureScriptsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CreatureScriptsCreator_Spells)).BeginInit();
            this.contextMenuStrip_CreatureScriptsCreator.SuspendLayout();
            this.toolStrip_CreatureScriptsCreator.SuspendLayout();
            this.tabPage_WaypointsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_WaypointsCreator_Waypoints)).BeginInit();
            this.contextMenuStrip_WaypointsCreator.SuspendLayout();
            this.contextMenuStrip_WaypointsCreator_Guids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_WaypointsCreator_Path)).BeginInit();
            this.toolStrip_WaypointsCreator.SuspendLayout();
            this.tabPage_SqlOutput.SuspendLayout();
            this.tabPage_DatabaseAdvisor.SuspendLayout();
            this.contextMenuStrip_DatabaseAdvisor.SuspendLayout();
            this.tabPage_DoubleSpawnsRemover.SuspendLayout();
            this.tabPage_CoreScriptTemplates.SuspendLayout();
            this.tabPage_Achievements.SuspendLayout();
            this.tabPage_ConditionsCreator.SuspendLayout();
            this.tabPage_ModifierTrees.SuspendLayout();
            this.tabPage_ParsedFileAdvisor.SuspendLayout();
            this.toolStrip_ParsedFileAdvisor.SuspendLayout();
            this.statusStrip_LoadedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_CreatureScriptsCreator);
            this.tabControl.Controls.Add(this.tabPage_WaypointsCreator);
            this.tabControl.Controls.Add(this.tabPage_SqlOutput);
            this.tabControl.Controls.Add(this.tabPage_DatabaseAdvisor);
            this.tabControl.Controls.Add(this.tabPage_DoubleSpawnsRemover);
            this.tabControl.Controls.Add(this.tabPage_CoreScriptTemplates);
            this.tabControl.Controls.Add(this.tabPage_Achievements);
            this.tabControl.Controls.Add(this.tabPage_ConditionsCreator);
            this.tabControl.Controls.Add(this.tabPage_ModifierTrees);
            this.tabControl.Controls.Add(this.tabPage_ParsedFileAdvisor);
            this.tabControl.Location = new System.Drawing.Point(3, 2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(2048, 988);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage_CreatureScriptsCreator
            // 
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.checkBox_CreatureScriptsCreator_CreateDataFile);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.checkBox_CreatureScriptsCreator_OnlyCombatSpells);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.dataGridView_CreatureScriptsCreator_Spells);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.listBox_CreatureScriptCreator_CreatureGuids);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.toolStrip_CreatureScriptsCreator);
            this.tabPage_CreatureScriptsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CreatureScriptsCreator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_CreatureScriptsCreator.Name = "tabPage_CreatureScriptsCreator";
            this.tabPage_CreatureScriptsCreator.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_CreatureScriptsCreator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_CreatureScriptsCreator.TabIndex = 0;
            this.tabPage_CreatureScriptsCreator.Text = "Creature Scripts Creator";
            this.tabPage_CreatureScriptsCreator.UseVisualStyleBackColor = true;
            // 
            // checkBox_CreatureScriptsCreator_CreateDataFile
            // 
            this.checkBox_CreatureScriptsCreator_CreateDataFile.AutoSize = true;
            this.checkBox_CreatureScriptsCreator_CreateDataFile.BackColor = System.Drawing.Color.LightGray;
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Location = new System.Drawing.Point(1235, 8);
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Name = "checkBox_CreatureScriptsCreator_CreateDataFile";
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Size = new System.Drawing.Size(161, 29);
            this.checkBox_CreatureScriptsCreator_CreateDataFile.TabIndex = 5;
            this.checkBox_CreatureScriptsCreator_CreateDataFile.Text = "Create Data File";
            this.checkBox_CreatureScriptsCreator_CreateDataFile.UseVisualStyleBackColor = false;
            // 
            // checkBox_CreatureScriptsCreator_OnlyCombatSpells
            // 
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.AutoSize = true;
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.BackColor = System.Drawing.Color.LightGray;
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Checked = true;
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Location = new System.Drawing.Point(1402, 8);
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Name = "checkBox_CreatureScriptsCreator_OnlyCombatSpells";
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Size = new System.Drawing.Size(244, 29);
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.TabIndex = 4;
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.Text = "Show Only Combat Spells";
            this.checkBox_CreatureScriptsCreator_OnlyCombatSpells.UseVisualStyleBackColor = false;
            // 
            // dataGridView_CreatureScriptsCreator_Spells
            // 
            this.dataGridView_CreatureScriptsCreator_Spells.AllowUserToAddRows = false;
            this.dataGridView_CreatureScriptsCreator_Spells.AllowUserToDeleteRows = false;
            this.dataGridView_CreatureScriptsCreator_Spells.AllowUserToOrderColumns = true;
            this.dataGridView_CreatureScriptsCreator_Spells.AllowUserToResizeColumns = false;
            this.dataGridView_CreatureScriptsCreator_Spells.AllowUserToResizeRows = false;
            this.dataGridView_CreatureScriptsCreator_Spells.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_CreatureScriptsCreator_Spells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CreatureScriptsCreator_Spells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SpellId,
            this.SpellName,
            this.CastTime,
            this.MinCastStartTime,
            this.MaxCastStartTime,
            this.MinCastRepeatTime,
            this.MaxCastRepeatTime,
            this.CastsCount,
            this.SourceSpell});
            this.dataGridView_CreatureScriptsCreator_Spells.ContextMenuStrip = this.contextMenuStrip_CreatureScriptsCreator;
            this.dataGridView_CreatureScriptsCreator_Spells.Enabled = false;
            this.dataGridView_CreatureScriptsCreator_Spells.Location = new System.Drawing.Point(760, 49);
            this.dataGridView_CreatureScriptsCreator_Spells.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_CreatureScriptsCreator_Spells.Name = "dataGridView_CreatureScriptsCreator_Spells";
            this.dataGridView_CreatureScriptsCreator_Spells.RowHeadersWidth = 62;
            this.dataGridView_CreatureScriptsCreator_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_CreatureScriptsCreator_Spells.RowTemplate.Height = 28;
            this.dataGridView_CreatureScriptsCreator_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_CreatureScriptsCreator_Spells.Size = new System.Drawing.Size(1269, 886);
            this.dataGridView_CreatureScriptsCreator_Spells.TabIndex = 3;
            // 
            // SpellId
            // 
            this.SpellId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellId.DefaultCellStyle = dataGridViewCellStyle1;
            this.SpellId.HeaderText = "SpellId";
            this.SpellId.MaxInputLength = 10;
            this.SpellId.MinimumWidth = 8;
            this.SpellId.Name = "SpellId";
            this.SpellId.ReadOnly = true;
            this.SpellId.Width = 94;
            // 
            // SpellName
            // 
            this.SpellName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellName.DefaultCellStyle = dataGridViewCellStyle2;
            this.SpellName.HeaderText = "SpellName";
            this.SpellName.MaxInputLength = 50;
            this.SpellName.MinimumWidth = 8;
            this.SpellName.Name = "SpellName";
            this.SpellName.ReadOnly = true;
            this.SpellName.Width = 122;
            // 
            // CastTime
            // 
            this.CastTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.CastTime.HeaderText = "CastTime";
            this.CastTime.MaxInputLength = 10;
            this.CastTime.MinimumWidth = 8;
            this.CastTime.Name = "CastTime";
            this.CastTime.ReadOnly = true;
            this.CastTime.Width = 112;
            // 
            // MinCastStartTime
            // 
            this.MinCastStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastStartTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.MinCastStartTime.HeaderText = "MinCastStartTime";
            this.MinCastStartTime.MaxInputLength = 10;
            this.MinCastStartTime.MinimumWidth = 8;
            this.MinCastStartTime.Name = "MinCastStartTime";
            this.MinCastStartTime.ReadOnly = true;
            this.MinCastStartTime.Width = 172;
            // 
            // MaxCastStartTime
            // 
            this.MaxCastStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastStartTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.MaxCastStartTime.HeaderText = "MaxCastStartTime";
            this.MaxCastStartTime.MaxInputLength = 10;
            this.MaxCastStartTime.MinimumWidth = 8;
            this.MaxCastStartTime.Name = "MaxCastStartTime";
            this.MaxCastStartTime.ReadOnly = true;
            this.MaxCastStartTime.Width = 176;
            // 
            // MinCastRepeatTime
            // 
            this.MinCastRepeatTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle6;
            this.MinCastRepeatTime.HeaderText = "MinCastRepeatTime";
            this.MinCastRepeatTime.MaxInputLength = 10;
            this.MinCastRepeatTime.MinimumWidth = 8;
            this.MinCastRepeatTime.Name = "MinCastRepeatTime";
            this.MinCastRepeatTime.ReadOnly = true;
            this.MinCastRepeatTime.Width = 190;
            // 
            // MaxCastRepeatTime
            // 
            this.MaxCastRepeatTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.MaxCastRepeatTime.HeaderText = "MaxCastRepeatTime";
            this.MaxCastRepeatTime.MaxInputLength = 10;
            this.MaxCastRepeatTime.MinimumWidth = 8;
            this.MaxCastRepeatTime.Name = "MaxCastRepeatTime";
            this.MaxCastRepeatTime.ReadOnly = true;
            this.MaxCastRepeatTime.Width = 194;
            // 
            // CastsCount
            // 
            this.CastsCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastsCount.DefaultCellStyle = dataGridViewCellStyle8;
            this.CastsCount.HeaderText = "CastsCount";
            this.CastsCount.MaxInputLength = 4;
            this.CastsCount.MinimumWidth = 8;
            this.CastsCount.Name = "CastsCount";
            this.CastsCount.ReadOnly = true;
            this.CastsCount.Width = 129;
            // 
            // SourceSpell
            // 
            this.SourceSpell.HeaderText = "SourceSpell";
            this.SourceSpell.MinimumWidth = 8;
            this.SourceSpell.Name = "SourceSpell";
            this.SourceSpell.Visible = false;
            this.SourceSpell.Width = 150;
            // 
            // contextMenuStrip_CreatureScriptsCreator
            // 
            this.contextMenuStrip_CreatureScriptsCreator.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_CreatureScriptsCreator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.toolStripSeparator,
            this.createSmartAISqlToolStripMenuItem,
            this.createLegionCombatAISqlToolStripMenuItem,
            this.createCoreScriptToolStripMenuItem});
            this.contextMenuStrip_CreatureScriptsCreator.Name = "contextMenuStrip1";
            this.contextMenuStrip_CreatureScriptsCreator.Size = new System.Drawing.Size(311, 138);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(310, 32);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(307, 6);
            // 
            // createSmartAISqlToolStripMenuItem
            // 
            this.createSmartAISqlToolStripMenuItem.Name = "createSmartAISqlToolStripMenuItem";
            this.createSmartAISqlToolStripMenuItem.Size = new System.Drawing.Size(310, 32);
            this.createSmartAISqlToolStripMenuItem.Text = "Create SmartAI SQL";
            this.createSmartAISqlToolStripMenuItem.Click += new System.EventHandler(this.createSQLToolStripMenuItem_Click);
            // 
            // createLegionCombatAISqlToolStripMenuItem
            // 
            this.createLegionCombatAISqlToolStripMenuItem.Name = "createLegionCombatAISqlToolStripMenuItem";
            this.createLegionCombatAISqlToolStripMenuItem.Size = new System.Drawing.Size(310, 32);
            this.createLegionCombatAISqlToolStripMenuItem.Text = "Create LegionCombatAI SQL";
            this.createLegionCombatAISqlToolStripMenuItem.Click += new System.EventHandler(this.createLegionCombatAISqlToolStripMenuItem_Click);
            // 
            // createCoreScriptToolStripMenuItem
            // 
            this.createCoreScriptToolStripMenuItem.Name = "createCoreScriptToolStripMenuItem";
            this.createCoreScriptToolStripMenuItem.Size = new System.Drawing.Size(310, 32);
            this.createCoreScriptToolStripMenuItem.Text = "Create core script";
            this.createCoreScriptToolStripMenuItem.Click += new System.EventHandler(this.createCoreScriptToolStripMenuItem_Click);
            // 
            // listBox_CreatureScriptCreator_CreatureGuids
            // 
            this.listBox_CreatureScriptCreator_CreatureGuids.BackColor = System.Drawing.SystemColors.Control;
            this.listBox_CreatureScriptCreator_CreatureGuids.Enabled = false;
            this.listBox_CreatureScriptCreator_CreatureGuids.FormattingEnabled = true;
            this.listBox_CreatureScriptCreator_CreatureGuids.ItemHeight = 20;
            this.listBox_CreatureScriptCreator_CreatureGuids.Location = new System.Drawing.Point(8, 49);
            this.listBox_CreatureScriptCreator_CreatureGuids.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox_CreatureScriptCreator_CreatureGuids.Name = "listBox_CreatureScriptCreator_CreatureGuids";
            this.listBox_CreatureScriptCreator_CreatureGuids.Size = new System.Drawing.Size(726, 884);
            this.listBox_CreatureScriptCreator_CreatureGuids.TabIndex = 2;
            this.listBox_CreatureScriptCreator_CreatureGuids.SelectedIndexChanged += new System.EventHandler(this.listBox_CreatureGuids_SelectedIndexChanged);
            // 
            // toolStrip_CreatureScriptsCreator
            // 
            this.toolStrip_CreatureScriptsCreator.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip_CreatureScriptsCreator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_CSC_ImportSniff,
            this.toolStripButton_CreatureScriptsCreator_Search,
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry,
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry,
            this.toolStripSeparator_CSC});
            this.toolStrip_CreatureScriptsCreator.Location = new System.Drawing.Point(3, 2);
            this.toolStrip_CreatureScriptsCreator.Name = "toolStrip_CreatureScriptsCreator";
            this.toolStrip_CreatureScriptsCreator.Size = new System.Drawing.Size(2034, 34);
            this.toolStrip_CreatureScriptsCreator.TabIndex = 1;
            this.toolStrip_CreatureScriptsCreator.Text = "toolStrip_CreatureScriptsCreator";
            // 
            // toolStripButton_CSC_ImportSniff
            // 
            this.toolStripButton_CSC_ImportSniff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_CSC_ImportSniff.Image")));
            this.toolStripButton_CSC_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CSC_ImportSniff.Name = "toolStripButton_CSC_ImportSniff";
            this.toolStripButton_CSC_ImportSniff.Size = new System.Drawing.Size(128, 29);
            this.toolStripButton_CSC_ImportSniff.Text = "Import Sniff";
            this.toolStripButton_CSC_ImportSniff.Click += new System.EventHandler(this.toolStripButton_ImportSniff_Click);
            // 
            // toolStripButton_CreatureScriptsCreator_Search
            // 
            this.toolStripButton_CreatureScriptsCreator_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_CreatureScriptsCreator_Search.Enabled = false;
            this.toolStripButton_CreatureScriptsCreator_Search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_CreatureScriptsCreator_Search.Image")));
            this.toolStripButton_CreatureScriptsCreator_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CreatureScriptsCreator_Search.Name = "toolStripButton_CreatureScriptsCreator_Search";
            this.toolStripButton_CreatureScriptsCreator_Search.Size = new System.Drawing.Size(84, 29);
            this.toolStripButton_CreatureScriptsCreator_Search.Text = "Search";
            this.toolStripButton_CreatureScriptsCreator_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripTextBox_CreatureScriptsCreator_CreatureEntry
            // 
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.MaxLength = 40;
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Name = "toolStripTextBox_CreatureScriptsCreator_CreatureEntry";
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.Size = new System.Drawing.Size(100, 34);
            this.toolStripTextBox_CreatureScriptsCreator_CreatureEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_CSC_CreatureEntrySearch_Enter);
            // 
            // toolStripLabel_CreatureScriptsCreator_CreatureEntry
            // 
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry.Name = "toolStripLabel_CreatureScriptsCreator_CreatureEntry";
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry.Size = new System.Drawing.Size(184, 29);
            this.toolStripLabel_CreatureScriptsCreator_CreatureEntry.Text = "Creature EntryOrGuid:";
            // 
            // toolStripSeparator_CSC
            // 
            this.toolStripSeparator_CSC.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator_CSC.Name = "toolStripSeparator_CSC";
            this.toolStripSeparator_CSC.Size = new System.Drawing.Size(6, 34);
            // 
            // tabPage_WaypointsCreator
            // 
            this.tabPage_WaypointsCreator.Controls.Add(this.checkBox_WaypointsCreator_CreateDataFile);
            this.tabPage_WaypointsCreator.Controls.Add(this.grid_WaypointsCreator_Waypoints);
            this.tabPage_WaypointsCreator.Controls.Add(this.listBox_WaypointsCreator_CreatureGuids);
            this.tabPage_WaypointsCreator.Controls.Add(this.chart_WaypointsCreator_Path);
            this.tabPage_WaypointsCreator.Controls.Add(this.toolStrip_WaypointsCreator);
            this.tabPage_WaypointsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_WaypointsCreator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_WaypointsCreator.Name = "tabPage_WaypointsCreator";
            this.tabPage_WaypointsCreator.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_WaypointsCreator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_WaypointsCreator.TabIndex = 4;
            this.tabPage_WaypointsCreator.Text = "Waypoints Creator";
            this.tabPage_WaypointsCreator.UseVisualStyleBackColor = true;
            // 
            // checkBox_WaypointsCreator_CreateDataFile
            // 
            this.checkBox_WaypointsCreator_CreateDataFile.AutoSize = true;
            this.checkBox_WaypointsCreator_CreateDataFile.BackColor = System.Drawing.Color.LightGray;
            this.checkBox_WaypointsCreator_CreateDataFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_WaypointsCreator_CreateDataFile.Location = new System.Drawing.Point(1369, 8);
            this.checkBox_WaypointsCreator_CreateDataFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_WaypointsCreator_CreateDataFile.Name = "checkBox_WaypointsCreator_CreateDataFile";
            this.checkBox_WaypointsCreator_CreateDataFile.Size = new System.Drawing.Size(161, 29);
            this.checkBox_WaypointsCreator_CreateDataFile.TabIndex = 29;
            this.checkBox_WaypointsCreator_CreateDataFile.Text = "Create Data File";
            this.checkBox_WaypointsCreator_CreateDataFile.UseVisualStyleBackColor = false;
            // 
            // grid_WaypointsCreator_Waypoints
            // 
            this.grid_WaypointsCreator_Waypoints.AllowUserToAddRows = false;
            this.grid_WaypointsCreator_Waypoints.AllowUserToDeleteRows = false;
            this.grid_WaypointsCreator_Waypoints.AllowUserToResizeColumns = false;
            this.grid_WaypointsCreator_Waypoints.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.NullValue = null;
            this.grid_WaypointsCreator_Waypoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.grid_WaypointsCreator_Waypoints.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.NullValue = null;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_WaypointsCreator_Waypoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grid_WaypointsCreator_Waypoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_WaypointsCreator_Waypoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridColumn_Id,
            this.gridColumn_PosX,
            this.gridColumn_PosY,
            this.gridColumn_PosZ,
            this.gridColumn_Orientation,
            this.gridColumn_WCTime,
            this.gridColumn_WCDelay,
            this.gridColumn_HasScript,
            this.WaypointSource});
            this.grid_WaypointsCreator_Waypoints.ContextMenuStrip = this.contextMenuStrip_WaypointsCreator;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.NullValue = null;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_WaypointsCreator_Waypoints.DefaultCellStyle = dataGridViewCellStyle19;
            this.grid_WaypointsCreator_Waypoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid_WaypointsCreator_Waypoints.Enabled = false;
            this.grid_WaypointsCreator_Waypoints.Location = new System.Drawing.Point(1274, 49);
            this.grid_WaypointsCreator_Waypoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid_WaypointsCreator_Waypoints.Name = "grid_WaypointsCreator_Waypoints";
            this.grid_WaypointsCreator_Waypoints.RowHeadersWidth = 62;
            this.grid_WaypointsCreator_Waypoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_WaypointsCreator_Waypoints.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.grid_WaypointsCreator_Waypoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_WaypointsCreator_Waypoints.Size = new System.Drawing.Size(770, 886);
            this.grid_WaypointsCreator_Waypoints.TabIndex = 28;
            this.grid_WaypointsCreator_Waypoints.TabStop = false;
            this.grid_WaypointsCreator_Waypoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_WaypointsCreator_Waypoints_CellContentClick);
            // 
            // gridColumn_Id
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Id.DefaultCellStyle = dataGridViewCellStyle11;
            this.gridColumn_Id.HeaderText = "Id";
            this.gridColumn_Id.MinimumWidth = 8;
            this.gridColumn_Id.Name = "gridColumn_Id";
            this.gridColumn_Id.ReadOnly = true;
            this.gridColumn_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.gridColumn_Id.Width = 35;
            // 
            // gridColumn_PosX
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle12.NullValue = null;
            this.gridColumn_PosX.DefaultCellStyle = dataGridViewCellStyle12;
            this.gridColumn_PosX.HeaderText = "PosX";
            this.gridColumn_PosX.MinimumWidth = 8;
            this.gridColumn_PosX.Name = "gridColumn_PosX";
            this.gridColumn_PosX.ReadOnly = true;
            this.gridColumn_PosX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosX.Width = 60;
            // 
            // gridColumn_PosY
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle13.NullValue = null;
            this.gridColumn_PosY.DefaultCellStyle = dataGridViewCellStyle13;
            this.gridColumn_PosY.HeaderText = "PosY";
            this.gridColumn_PosY.MinimumWidth = 8;
            this.gridColumn_PosY.Name = "gridColumn_PosY";
            this.gridColumn_PosY.ReadOnly = true;
            this.gridColumn_PosY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosY.Width = 60;
            // 
            // gridColumn_PosZ
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_PosZ.DefaultCellStyle = dataGridViewCellStyle14;
            this.gridColumn_PosZ.HeaderText = "PosZ";
            this.gridColumn_PosZ.MinimumWidth = 8;
            this.gridColumn_PosZ.Name = "gridColumn_PosZ";
            this.gridColumn_PosZ.ReadOnly = true;
            this.gridColumn_PosZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosZ.Width = 60;
            // 
            // gridColumn_Orientation
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Orientation.DefaultCellStyle = dataGridViewCellStyle15;
            this.gridColumn_Orientation.HeaderText = "Orientation";
            this.gridColumn_Orientation.MinimumWidth = 8;
            this.gridColumn_Orientation.Name = "gridColumn_Orientation";
            this.gridColumn_Orientation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_Orientation.Width = 65;
            // 
            // gridColumn_WCTime
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCTime.DefaultCellStyle = dataGridViewCellStyle16;
            this.gridColumn_WCTime.HeaderText = "Time";
            this.gridColumn_WCTime.MinimumWidth = 8;
            this.gridColumn_WCTime.Name = "gridColumn_WCTime";
            this.gridColumn_WCTime.ReadOnly = true;
            this.gridColumn_WCTime.Width = 60;
            // 
            // gridColumn_WCDelay
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCDelay.DefaultCellStyle = dataGridViewCellStyle17;
            this.gridColumn_WCDelay.HeaderText = "Delay";
            this.gridColumn_WCDelay.MinimumWidth = 8;
            this.gridColumn_WCDelay.Name = "gridColumn_WCDelay";
            this.gridColumn_WCDelay.Width = 50;
            // 
            // gridColumn_HasScript
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_HasScript.DefaultCellStyle = dataGridViewCellStyle18;
            this.gridColumn_HasScript.HeaderText = "HasScript";
            this.gridColumn_HasScript.MinimumWidth = 8;
            this.gridColumn_HasScript.Name = "gridColumn_HasScript";
            this.gridColumn_HasScript.ReadOnly = true;
            this.gridColumn_HasScript.Width = 60;
            // 
            // WaypointSource
            // 
            this.WaypointSource.HeaderText = "WaypointSource";
            this.WaypointSource.MinimumWidth = 8;
            this.WaypointSource.Name = "WaypointSource";
            this.WaypointSource.Visible = false;
            this.WaypointSource.Width = 150;
            // 
            // contextMenuStrip_WaypointsCreator
            // 
            this.contextMenuStrip_WaypointsCreator.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_WaypointsCreator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem_WC,
            this.removeNearestPointsToolStripMenuItem_WC,
            this.removeDuplicatePointsToolStripMenuItem_WC,
            this.createReturnPathToolStripMenuItem_WC,
            this.optimizeCirclePathToolStripMenuItem_WC,
            this.optimizeRegularPathToolStripMenuItem_WC,
            this.reversePointsOrderToolStripMenuItem_WC,
            this.setSelectedPointAsFirstToolStripMenuItem_WC,
            this.toolStripSeparator_WC,
            this.createSQLToolStripMenuItem_WC});
            this.contextMenuStrip_WaypointsCreator.Name = "contextMenuStrip_WC";
            this.contextMenuStrip_WaypointsCreator.Size = new System.Drawing.Size(289, 298);
            // 
            // cutToolStripMenuItem_WC
            // 
            this.cutToolStripMenuItem_WC.Name = "cutToolStripMenuItem_WC";
            this.cutToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.cutToolStripMenuItem_WC.Text = "Cut";
            this.cutToolStripMenuItem_WC.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // removeNearestPointsToolStripMenuItem_WC
            // 
            this.removeNearestPointsToolStripMenuItem_WC.Name = "removeNearestPointsToolStripMenuItem_WC";
            this.removeNearestPointsToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.removeNearestPointsToolStripMenuItem_WC.Text = "Remove Nearest Points";
            this.removeNearestPointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeExcessPointsToolStripMenuItem_Click);
            // 
            // removeDuplicatePointsToolStripMenuItem_WC
            // 
            this.removeDuplicatePointsToolStripMenuItem_WC.Name = "removeDuplicatePointsToolStripMenuItem_WC";
            this.removeDuplicatePointsToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.removeDuplicatePointsToolStripMenuItem_WC.Text = "Remove Duplicate Points";
            this.removeDuplicatePointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeDuplicatePointsToolStripMenuItem_WC_Click);
            // 
            // createReturnPathToolStripMenuItem_WC
            // 
            this.createReturnPathToolStripMenuItem_WC.Name = "createReturnPathToolStripMenuItem_WC";
            this.createReturnPathToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.createReturnPathToolStripMenuItem_WC.Text = "Create Return Path";
            this.createReturnPathToolStripMenuItem_WC.Click += new System.EventHandler(this.createReturnPathToolStripMenuItem_WC_Click);
            // 
            // optimizeCirclePathToolStripMenuItem_WC
            // 
            this.optimizeCirclePathToolStripMenuItem_WC.Name = "optimizeCirclePathToolStripMenuItem_WC";
            this.optimizeCirclePathToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.optimizeCirclePathToolStripMenuItem_WC.Text = "Optimize Circle Path";
            this.optimizeCirclePathToolStripMenuItem_WC.Click += new System.EventHandler(this.optimizeCirclePathToolStripMenuItem_Click);
            // 
            // optimizeRegularPathToolStripMenuItem_WC
            // 
            this.optimizeRegularPathToolStripMenuItem_WC.Name = "optimizeRegularPathToolStripMenuItem_WC";
            this.optimizeRegularPathToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.optimizeRegularPathToolStripMenuItem_WC.Text = "Optimize Regular Path";
            this.optimizeRegularPathToolStripMenuItem_WC.Click += new System.EventHandler(this.optimizeRegularPathToolStripMenuItem_WC_Click);
            // 
            // reversePointsOrderToolStripMenuItem_WC
            // 
            this.reversePointsOrderToolStripMenuItem_WC.Name = "reversePointsOrderToolStripMenuItem_WC";
            this.reversePointsOrderToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.reversePointsOrderToolStripMenuItem_WC.Text = "Reverse Points Order";
            this.reversePointsOrderToolStripMenuItem_WC.Click += new System.EventHandler(this.reversePointsOrderToolStripMenuItem_Click);
            // 
            // setSelectedPointAsFirstToolStripMenuItem_WC
            // 
            this.setSelectedPointAsFirstToolStripMenuItem_WC.Name = "setSelectedPointAsFirstToolStripMenuItem_WC";
            this.setSelectedPointAsFirstToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.setSelectedPointAsFirstToolStripMenuItem_WC.Text = "Set Selected Point As First";
            this.setSelectedPointAsFirstToolStripMenuItem_WC.Click += new System.EventHandler(this.setSelectedPointAsFirstToolStripMenuItem_Click);
            // 
            // toolStripSeparator_WC
            // 
            this.toolStripSeparator_WC.Name = "toolStripSeparator_WC";
            this.toolStripSeparator_WC.Size = new System.Drawing.Size(285, 6);
            // 
            // createSQLToolStripMenuItem_WC
            // 
            this.createSQLToolStripMenuItem_WC.Name = "createSQLToolStripMenuItem_WC";
            this.createSQLToolStripMenuItem_WC.Size = new System.Drawing.Size(288, 32);
            this.createSQLToolStripMenuItem_WC.Text = "Create SQL";
            this.createSQLToolStripMenuItem_WC.Click += new System.EventHandler(this.createSQLToolStripMenuItem1_Click);
            // 
            // listBox_WaypointsCreator_CreatureGuids
            // 
            this.listBox_WaypointsCreator_CreatureGuids.BackColor = System.Drawing.SystemColors.Control;
            this.listBox_WaypointsCreator_CreatureGuids.ContextMenuStrip = this.contextMenuStrip_WaypointsCreator_Guids;
            this.listBox_WaypointsCreator_CreatureGuids.Enabled = false;
            this.listBox_WaypointsCreator_CreatureGuids.FormattingEnabled = true;
            this.listBox_WaypointsCreator_CreatureGuids.ItemHeight = 20;
            this.listBox_WaypointsCreator_CreatureGuids.Location = new System.Drawing.Point(906, 49);
            this.listBox_WaypointsCreator_CreatureGuids.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_WaypointsCreator_CreatureGuids.Name = "listBox_WaypointsCreator_CreatureGuids";
            this.listBox_WaypointsCreator_CreatureGuids.Size = new System.Drawing.Size(356, 884);
            this.listBox_WaypointsCreator_CreatureGuids.TabIndex = 27;
            this.listBox_WaypointsCreator_CreatureGuids.SelectedIndexChanged += new System.EventHandler(this.listBox_WCCreatureGuids_SelectedIndexChanged);
            // 
            // contextMenuStrip_WaypointsCreator_Guids
            // 
            this.contextMenuStrip_WaypointsCreator_Guids.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_WaypointsCreator_Guids.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeGuidsBeforeSelectedToolStripMenuItem,
            this.createRandomMovementsToolStripMenuItem});
            this.contextMenuStrip_WaypointsCreator_Guids.Name = "contextMenuStrip_WC_Guids";
            this.contextMenuStrip_WaypointsCreator_Guids.Size = new System.Drawing.Size(324, 68);
            // 
            // removeGuidsBeforeSelectedToolStripMenuItem
            // 
            this.removeGuidsBeforeSelectedToolStripMenuItem.Name = "removeGuidsBeforeSelectedToolStripMenuItem";
            this.removeGuidsBeforeSelectedToolStripMenuItem.Size = new System.Drawing.Size(323, 32);
            this.removeGuidsBeforeSelectedToolStripMenuItem.Text = "Remove guids before selected";
            this.removeGuidsBeforeSelectedToolStripMenuItem.Click += new System.EventHandler(this.removeGuidsBeforeSelectedToolStripMenuItem_Click);
            // 
            // createRandomMovementsToolStripMenuItem
            // 
            this.createRandomMovementsToolStripMenuItem.Name = "createRandomMovementsToolStripMenuItem";
            this.createRandomMovementsToolStripMenuItem.Size = new System.Drawing.Size(323, 32);
            this.createRandomMovementsToolStripMenuItem.Text = "Create random movements";
            this.createRandomMovementsToolStripMenuItem.Click += new System.EventHandler(this.createRandomMovementsToolStripMenuItem_Click);
            // 
            // chart_WaypointsCreator_Path
            // 
            this.chart_WaypointsCreator_Path.BorderlineWidth = 0;
            this.chart_WaypointsCreator_Path.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart_WaypointsCreator_Path.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart_WaypointsCreator_Path.BorderSkin.BorderWidth = 0;
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.AxisX.IsReversed = true;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisX2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LabelStyle.IsEndLabelVisible = false;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea1.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.CursorY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.Name = "ChartArea1";
            this.chart_WaypointsCreator_Path.ChartAreas.Add(chartArea1);
            this.chart_WaypointsCreator_Path.Enabled = false;
            legend1.Enabled = false;
            legend1.ForeColor = System.Drawing.Color.Transparent;
            legend1.HeaderSeparatorColor = System.Drawing.Color.Transparent;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            legend1.TitleForeColor = System.Drawing.Color.Transparent;
            legend1.TitleSeparatorColor = System.Drawing.Color.Transparent;
            this.chart_WaypointsCreator_Path.Legends.Add(legend1);
            this.chart_WaypointsCreator_Path.Location = new System.Drawing.Point(4, 49);
            this.chart_WaypointsCreator_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_WaypointsCreator_Path.Name = "chart_WaypointsCreator_Path";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Path";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart_WaypointsCreator_Path.Series.Add(series1);
            this.chart_WaypointsCreator_Path.Size = new System.Drawing.Size(894, 888);
            this.chart_WaypointsCreator_Path.TabIndex = 26;
            this.chart_WaypointsCreator_Path.Text = "Waypoints";
            title1.DockedToChartArea = "ChartArea1";
            title1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Path";
            title1.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            this.chart_WaypointsCreator_Path.Titles.Add(title1);
            // 
            // toolStrip_WaypointsCreator
            // 
            this.toolStrip_WaypointsCreator.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip_WaypointsCreator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_WaypointsCreator_Search,
            this.toolStripTextBox_WaypointsCreator_Entry,
            this.toolStripLabel_WaypointsCreator_Entry,
            this.toolStripSeparator1,
            this.toolStripButton_WaypointsCreator_Settings,
            this.toolStripButton_WaypointsCreator_LoadSniff});
            this.toolStrip_WaypointsCreator.Location = new System.Drawing.Point(3, 2);
            this.toolStrip_WaypointsCreator.Name = "toolStrip_WaypointsCreator";
            this.toolStrip_WaypointsCreator.Size = new System.Drawing.Size(2034, 34);
            this.toolStrip_WaypointsCreator.TabIndex = 24;
            this.toolStrip_WaypointsCreator.Text = "toolStrip_WaypointsCreator";
            // 
            // toolStripButton_WaypointsCreator_Search
            // 
            this.toolStripButton_WaypointsCreator_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_WaypointsCreator_Search.Enabled = false;
            this.toolStripButton_WaypointsCreator_Search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WaypointsCreator_Search.Image")));
            this.toolStripButton_WaypointsCreator_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WaypointsCreator_Search.Name = "toolStripButton_WaypointsCreator_Search";
            this.toolStripButton_WaypointsCreator_Search.Size = new System.Drawing.Size(84, 29);
            this.toolStripButton_WaypointsCreator_Search.Text = "Search";
            this.toolStripButton_WaypointsCreator_Search.ToolTipText = "Fill listbox with guids of\r\nselected entry or all entries.";
            this.toolStripButton_WaypointsCreator_Search.Click += new System.EventHandler(this.toolStripButton_WCSearch_Click);
            // 
            // toolStripTextBox_WaypointsCreator_Entry
            // 
            this.toolStripTextBox_WaypointsCreator_Entry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_WaypointsCreator_Entry.Enabled = false;
            this.toolStripTextBox_WaypointsCreator_Entry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_WaypointsCreator_Entry.MaxLength = 40;
            this.toolStripTextBox_WaypointsCreator_Entry.Name = "toolStripTextBox_WaypointsCreator_Entry";
            this.toolStripTextBox_WaypointsCreator_Entry.Size = new System.Drawing.Size(103, 34);
            this.toolStripTextBox_WaypointsCreator_Entry.Tag = "";
            this.toolStripTextBox_WaypointsCreator_Entry.ToolTipText = "Input entry of creature or leave\r\nblank to fill listbox will all in sniff.";
            this.toolStripTextBox_WaypointsCreator_Entry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_WCSearch_Enter);
            // 
            // toolStripLabel_WaypointsCreator_Entry
            // 
            this.toolStripLabel_WaypointsCreator_Entry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_WaypointsCreator_Entry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel_WaypointsCreator_Entry.Name = "toolStripLabel_WaypointsCreator_Entry";
            this.toolStripLabel_WaypointsCreator_Entry.Size = new System.Drawing.Size(184, 29);
            this.toolStripLabel_WaypointsCreator_Entry.Text = "Creature EntryOrGuid:";
            this.toolStripLabel_WaypointsCreator_Entry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripButton_WaypointsCreator_Settings
            // 
            this.toolStripButton_WaypointsCreator_Settings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_WaypointsCreator_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WaypointsCreator_Settings.Image")));
            this.toolStripButton_WaypointsCreator_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WaypointsCreator_Settings.Name = "toolStripButton_WaypointsCreator_Settings";
            this.toolStripButton_WaypointsCreator_Settings.Size = new System.Drawing.Size(96, 29);
            this.toolStripButton_WaypointsCreator_Settings.Text = "Settings";
            this.toolStripButton_WaypointsCreator_Settings.ToolTipText = "Setup chart and output SQL.";
            this.toolStripButton_WaypointsCreator_Settings.Click += new System.EventHandler(this.toolStripButton_WCSettings_Click);
            // 
            // toolStripButton_WaypointsCreator_LoadSniff
            // 
            this.toolStripButton_WaypointsCreator_LoadSniff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WaypointsCreator_LoadSniff.Image")));
            this.toolStripButton_WaypointsCreator_LoadSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WaypointsCreator_LoadSniff.Name = "toolStripButton_WaypointsCreator_LoadSniff";
            this.toolStripButton_WaypointsCreator_LoadSniff.Size = new System.Drawing.Size(128, 29);
            this.toolStripButton_WaypointsCreator_LoadSniff.Text = "Import Sniff";
            this.toolStripButton_WaypointsCreator_LoadSniff.ToolTipText = "Import a parsed wpp sniff file.";
            this.toolStripButton_WaypointsCreator_LoadSniff.Click += new System.EventHandler(this.toolStripButton_WCLoadSniff_Click);
            // 
            // tabPage_SqlOutput
            // 
            this.tabPage_SqlOutput.Controls.Add(this.textBox_SqlOutput);
            this.tabPage_SqlOutput.Location = new System.Drawing.Point(4, 29);
            this.tabPage_SqlOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_SqlOutput.Name = "tabPage_SqlOutput";
            this.tabPage_SqlOutput.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_SqlOutput.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_SqlOutput.TabIndex = 1;
            this.tabPage_SqlOutput.Text = "Sql Output";
            this.tabPage_SqlOutput.UseVisualStyleBackColor = true;
            // 
            // textBox_SqlOutput
            // 
            this.textBox_SqlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SqlOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBox_SqlOutput.Location = new System.Drawing.Point(3, 2);
            this.textBox_SqlOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_SqlOutput.Multiline = true;
            this.textBox_SqlOutput.Name = "textBox_SqlOutput";
            this.textBox_SqlOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_SqlOutput.Size = new System.Drawing.Size(2034, 951);
            this.textBox_SqlOutput.TabIndex = 0;
            this.textBox_SqlOutput.WordWrap = false;
            // 
            // tabPage_DatabaseAdvisor
            // 
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_FindWrongAurasInAddons);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_FindWrongAurasInAddons);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_FindPossibleFormations);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_FindPossibleFormations);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_FindDoublePaths);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_FindDoublePaths);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_Output);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_GossipMenuText);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_GossipMenuText);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_DatabaseAdvisor_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_DatabaseAdvisor_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DatabaseAdvisor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_DatabaseAdvisor.Name = "tabPage_DatabaseAdvisor";
            this.tabPage_DatabaseAdvisor.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_DatabaseAdvisor.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_DatabaseAdvisor.TabIndex = 2;
            this.tabPage_DatabaseAdvisor.Text = "Database Advisor";
            this.tabPage_DatabaseAdvisor.UseVisualStyleBackColor = true;
            // 
            // textBox_DatabaseAdvisor_FindWrongAurasInAddons
            // 
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.Location = new System.Drawing.Point(8, 342);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.Name = "textBox_DatabaseAdvisor_FindWrongAurasInAddons";
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.TabIndex = 18;
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_FindWrongAurasInAddons_Click);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_DatabaseAdvisor_FindWrongAurasInAddons_KeyDown);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_FindWrongAurasInAddons_MouseEnter);
            this.textBox_DatabaseAdvisor_FindWrongAurasInAddons.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_FindWrongAurasInAddons_MouseLeave);
            // 
            // label_DatabaseAdvisor_FindWrongAurasInAddons
            // 
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.AutoSize = true;
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.Location = new System.Drawing.Point(6, 320);
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.Name = "label_DatabaseAdvisor_FindWrongAurasInAddons";
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.Size = new System.Drawing.Size(204, 20);
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.TabIndex = 17;
            this.label_DatabaseAdvisor_FindWrongAurasInAddons.Text = "Find wrong auras in addons";
            // 
            // textBox_DatabaseAdvisor_FindPossibleFormations
            // 
            this.textBox_DatabaseAdvisor_FindPossibleFormations.Location = new System.Drawing.Point(8, 277);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.Name = "textBox_DatabaseAdvisor_FindPossibleFormations";
            this.textBox_DatabaseAdvisor_FindPossibleFormations.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.TabIndex = 16;
            this.textBox_DatabaseAdvisor_FindPossibleFormations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_FindPossibleFormations.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_FindPossibleFormations_Click);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_DatabaseAdvisor_FindPossibleFormations_KeyDown);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_FindPossibleFormations_MouseEnter);
            this.textBox_DatabaseAdvisor_FindPossibleFormations.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_FindPossibleFormations_MouseLeave);
            // 
            // label_DatabaseAdvisor_FindPossibleFormations
            // 
            this.label_DatabaseAdvisor_FindPossibleFormations.AutoSize = true;
            this.label_DatabaseAdvisor_FindPossibleFormations.Location = new System.Drawing.Point(6, 255);
            this.label_DatabaseAdvisor_FindPossibleFormations.Name = "label_DatabaseAdvisor_FindPossibleFormations";
            this.label_DatabaseAdvisor_FindPossibleFormations.Size = new System.Drawing.Size(181, 20);
            this.label_DatabaseAdvisor_FindPossibleFormations.TabIndex = 15;
            this.label_DatabaseAdvisor_FindPossibleFormations.Text = "Find possible formations";
            // 
            // textBox_DatabaseAdvisor_FindDoublePaths
            // 
            this.textBox_DatabaseAdvisor_FindDoublePaths.Location = new System.Drawing.Point(8, 212);
            this.textBox_DatabaseAdvisor_FindDoublePaths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_FindDoublePaths.Name = "textBox_DatabaseAdvisor_FindDoublePaths";
            this.textBox_DatabaseAdvisor_FindDoublePaths.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_FindDoublePaths.TabIndex = 14;
            this.textBox_DatabaseAdvisor_FindDoublePaths.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_FindDoublePaths.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_FindDoublePaths_Click);
            this.textBox_DatabaseAdvisor_FindDoublePaths.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_DatabaseAdvisor_FindDoublePaths_KeyDown);
            this.textBox_DatabaseAdvisor_FindDoublePaths.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_FindDoublePaths_MouseEnter);
            this.textBox_DatabaseAdvisor_FindDoublePaths.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_FindDoublePaths_MouseLeave);
            // 
            // label_DatabaseAdvisor_FindDoublePaths
            // 
            this.label_DatabaseAdvisor_FindDoublePaths.AutoSize = true;
            this.label_DatabaseAdvisor_FindDoublePaths.Location = new System.Drawing.Point(6, 190);
            this.label_DatabaseAdvisor_FindDoublePaths.Name = "label_DatabaseAdvisor_FindDoublePaths";
            this.label_DatabaseAdvisor_FindDoublePaths.Size = new System.Drawing.Size(136, 20);
            this.label_DatabaseAdvisor_FindDoublePaths.TabIndex = 13;
            this.label_DatabaseAdvisor_FindDoublePaths.Text = "Find double paths";
            // 
            // textBox_DatabaseAdvisor_Output
            // 
            this.textBox_DatabaseAdvisor_Output.ContextMenuStrip = this.contextMenuStrip_DatabaseAdvisor;
            this.textBox_DatabaseAdvisor_Output.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBox_DatabaseAdvisor_Output.Location = new System.Drawing.Point(216, 2);
            this.textBox_DatabaseAdvisor_Output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_Output.MaxLength = 1000000;
            this.textBox_DatabaseAdvisor_Output.Multiline = true;
            this.textBox_DatabaseAdvisor_Output.Name = "textBox_DatabaseAdvisor_Output";
            this.textBox_DatabaseAdvisor_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_DatabaseAdvisor_Output.Size = new System.Drawing.Size(1821, 939);
            this.textBox_DatabaseAdvisor_Output.TabIndex = 10;
            this.textBox_DatabaseAdvisor_Output.WordWrap = false;
            // 
            // contextMenuStrip_DatabaseAdvisor
            // 
            this.contextMenuStrip_DatabaseAdvisor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_DatabaseAdvisor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createReturnPathToolStripMenuItem,
            this.recalculatePointsToolStripMenuItem,
            this.getAddonsFromSqlToolStripMenuItem,
            this.recalculateTextForGossipMenuToolStripMenuItem,
            this.getPhaseDataForCreatures});
            this.contextMenuStrip_DatabaseAdvisor.Name = "contextMenuStrip_DatabaseAdvisor";
            this.contextMenuStrip_DatabaseAdvisor.Size = new System.Drawing.Size(341, 164);
            // 
            // createReturnPathToolStripMenuItem
            // 
            this.createReturnPathToolStripMenuItem.Name = "createReturnPathToolStripMenuItem";
            this.createReturnPathToolStripMenuItem.Size = new System.Drawing.Size(340, 32);
            this.createReturnPathToolStripMenuItem.Text = "Create return path";
            this.createReturnPathToolStripMenuItem.Click += new System.EventHandler(this.createReturnPathToolStripMenuItem_Click);
            // 
            // recalculatePointsToolStripMenuItem
            // 
            this.recalculatePointsToolStripMenuItem.Name = "recalculatePointsToolStripMenuItem";
            this.recalculatePointsToolStripMenuItem.Size = new System.Drawing.Size(340, 32);
            this.recalculatePointsToolStripMenuItem.Text = "Recalculate point ids";
            this.recalculatePointsToolStripMenuItem.Click += new System.EventHandler(this.recalculatePointsToolStripMenuItem_Click);
            // 
            // getAddonsFromSqlToolStripMenuItem
            // 
            this.getAddonsFromSqlToolStripMenuItem.Name = "getAddonsFromSqlToolStripMenuItem";
            this.getAddonsFromSqlToolStripMenuItem.Size = new System.Drawing.Size(340, 32);
            this.getAddonsFromSqlToolStripMenuItem.Text = "Get addons from SQL";
            this.getAddonsFromSqlToolStripMenuItem.Click += new System.EventHandler(this.getAddonsFromSqlToolStripMenuItem_Click);
            // 
            // recalculateTextForGossipMenuToolStripMenuItem
            // 
            this.recalculateTextForGossipMenuToolStripMenuItem.Name = "recalculateTextForGossipMenuToolStripMenuItem";
            this.recalculateTextForGossipMenuToolStripMenuItem.Size = new System.Drawing.Size(340, 32);
            this.recalculateTextForGossipMenuToolStripMenuItem.Text = "Recalculate text for gossip menu";
            this.recalculateTextForGossipMenuToolStripMenuItem.Click += new System.EventHandler(this.recalculateTextForGossipMenuToolStripMenuItem_Click);
            // 
            // getPhaseDataForCreatures
            // 
            this.getPhaseDataForCreatures.Name = "getPhaseDataForCreatures";
            this.getPhaseDataForCreatures.Size = new System.Drawing.Size(340, 32);
            this.getPhaseDataForCreatures.Text = "Get phase data for creatures";
            this.getPhaseDataForCreatures.Click += new System.EventHandler(this.getPhaseDataForCreatures_Click);
            // 
            // textBox_DatabaseAdvisor_GossipMenuText
            // 
            this.textBox_DatabaseAdvisor_GossipMenuText.Location = new System.Drawing.Point(8, 147);
            this.textBox_DatabaseAdvisor_GossipMenuText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_GossipMenuText.Name = "textBox_DatabaseAdvisor_GossipMenuText";
            this.textBox_DatabaseAdvisor_GossipMenuText.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_GossipMenuText.TabIndex = 9;
            this.textBox_DatabaseAdvisor_GossipMenuText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_GossipMenuText.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_GossipMenuText_Click);
            this.textBox_DatabaseAdvisor_GossipMenuText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_GossipMenuText_KeyUp);
            this.textBox_DatabaseAdvisor_GossipMenuText.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_GossipMenuText_MouseEnter);
            this.textBox_DatabaseAdvisor_GossipMenuText.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_GossipMenuText_MouseLeave);
            // 
            // label_DatabaseAdvisor_GossipMenuText
            // 
            this.label_DatabaseAdvisor_GossipMenuText.AutoSize = true;
            this.label_DatabaseAdvisor_GossipMenuText.Location = new System.Drawing.Point(4, 125);
            this.label_DatabaseAdvisor_GossipMenuText.Name = "label_DatabaseAdvisor_GossipMenuText";
            this.label_DatabaseAdvisor_GossipMenuText.Size = new System.Drawing.Size(147, 20);
            this.label_DatabaseAdvisor_GossipMenuText.TabIndex = 8;
            this.label_DatabaseAdvisor_GossipMenuText.Text = "Get texts for gossip";
            // 
            // textBox_DatabaseAdvisor_QuestFlags
            // 
            this.textBox_DatabaseAdvisor_QuestFlags.Location = new System.Drawing.Point(8, 85);
            this.textBox_DatabaseAdvisor_QuestFlags.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_QuestFlags.Name = "textBox_DatabaseAdvisor_QuestFlags";
            this.textBox_DatabaseAdvisor_QuestFlags.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_QuestFlags.TabIndex = 3;
            this.textBox_DatabaseAdvisor_QuestFlags.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_QuestFlags.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_QuestFlags_Click);
            this.textBox_DatabaseAdvisor_QuestFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_QuestFlags_KeyDown);
            this.textBox_DatabaseAdvisor_QuestFlags.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_QuestFlags_MouseEnter);
            this.textBox_DatabaseAdvisor_QuestFlags.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_QuestFlags_MouseLeave);
            // 
            // label_DatabaseAdvisor_QuestFlags
            // 
            this.label_DatabaseAdvisor_QuestFlags.AutoSize = true;
            this.label_DatabaseAdvisor_QuestFlags.Location = new System.Drawing.Point(6, 60);
            this.label_DatabaseAdvisor_QuestFlags.Name = "label_DatabaseAdvisor_QuestFlags";
            this.label_DatabaseAdvisor_QuestFlags.Size = new System.Drawing.Size(141, 20);
            this.label_DatabaseAdvisor_QuestFlags.TabIndex = 2;
            this.label_DatabaseAdvisor_QuestFlags.Text = "Get flags for quest";
            // 
            // textBox_DatabaseAdvisor_CreatureFlags
            // 
            this.textBox_DatabaseAdvisor_CreatureFlags.Location = new System.Drawing.Point(8, 28);
            this.textBox_DatabaseAdvisor_CreatureFlags.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_DatabaseAdvisor_CreatureFlags.Name = "textBox_DatabaseAdvisor_CreatureFlags";
            this.textBox_DatabaseAdvisor_CreatureFlags.Size = new System.Drawing.Size(162, 26);
            this.textBox_DatabaseAdvisor_CreatureFlags.TabIndex = 1;
            this.textBox_DatabaseAdvisor_CreatureFlags.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DatabaseAdvisor_CreatureFlags.Click += new System.EventHandler(this.textBox_DatabaseAdvisor_CreatureFlags_Click);
            this.textBox_DatabaseAdvisor_CreatureFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_CreatureFlags_KeyDown);
            this.textBox_DatabaseAdvisor_CreatureFlags.MouseEnter += new System.EventHandler(this.textBox_DatabaseAdvisor_CreatureFlags_MouseEnter);
            this.textBox_DatabaseAdvisor_CreatureFlags.MouseLeave += new System.EventHandler(this.textBox_DatabaseAdvisor_CreatureFlags_MouseLeave);
            // 
            // label_DatabaseAdvisor_CreatureFlags
            // 
            this.label_DatabaseAdvisor_CreatureFlags.AutoSize = true;
            this.label_DatabaseAdvisor_CreatureFlags.Location = new System.Drawing.Point(4, 2);
            this.label_DatabaseAdvisor_CreatureFlags.Name = "label_DatabaseAdvisor_CreatureFlags";
            this.label_DatabaseAdvisor_CreatureFlags.Size = new System.Drawing.Size(160, 20);
            this.label_DatabaseAdvisor_CreatureFlags.TabIndex = 0;
            this.label_DatabaseAdvisor_CreatureFlags.Text = "Get flags for creature";
            // 
            // tabPage_DoubleSpawnsRemover
            // 
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.label_DoubleSpawnsRemover_GameobjectsRemoved);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.checkBox_DoubleSpawnsRemover_Gameobjects);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.checkBox_DoubleSpawnsRemover_Creatures);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.label_DoubleSpawnsRemover_CreaturesRemoved);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.button_DoubleSpawnsRemover_ImportFile);
            this.tabPage_DoubleSpawnsRemover.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DoubleSpawnsRemover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_DoubleSpawnsRemover.Name = "tabPage_DoubleSpawnsRemover";
            this.tabPage_DoubleSpawnsRemover.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_DoubleSpawnsRemover.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_DoubleSpawnsRemover.TabIndex = 3;
            this.tabPage_DoubleSpawnsRemover.Text = "Double-Spawns Remover";
            this.tabPage_DoubleSpawnsRemover.UseVisualStyleBackColor = true;
            // 
            // label_DoubleSpawnsRemover_GameobjectsRemoved
            // 
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.AutoSize = true;
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.Location = new System.Drawing.Point(494, 132);
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.Name = "label_DoubleSpawnsRemover_GameobjectsRemoved";
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.Size = new System.Drawing.Size(189, 20);
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.TabIndex = 4;
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.Text = "No gameobjects removed";
            this.label_DoubleSpawnsRemover_GameobjectsRemoved.Visible = false;
            // 
            // checkBox_DoubleSpawnsRemover_Gameobjects
            // 
            this.checkBox_DoubleSpawnsRemover_Gameobjects.AutoSize = true;
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Checked = true;
            this.checkBox_DoubleSpawnsRemover_Gameobjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Location = new System.Drawing.Point(802, 5);
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Name = "checkBox_DoubleSpawnsRemover_Gameobjects";
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Size = new System.Drawing.Size(130, 24);
            this.checkBox_DoubleSpawnsRemover_Gameobjects.TabIndex = 3;
            this.checkBox_DoubleSpawnsRemover_Gameobjects.Text = "Gameobjects";
            this.checkBox_DoubleSpawnsRemover_Gameobjects.UseVisualStyleBackColor = true;
            this.checkBox_DoubleSpawnsRemover_Gameobjects.CheckedChanged += new System.EventHandler(this.checkBox_GameobjectsRemover_CheckedChanged);
            // 
            // checkBox_DoubleSpawnsRemover_Creatures
            // 
            this.checkBox_DoubleSpawnsRemover_Creatures.AutoSize = true;
            this.checkBox_DoubleSpawnsRemover_Creatures.Checked = true;
            this.checkBox_DoubleSpawnsRemover_Creatures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DoubleSpawnsRemover_Creatures.Location = new System.Drawing.Point(700, 5);
            this.checkBox_DoubleSpawnsRemover_Creatures.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_DoubleSpawnsRemover_Creatures.Name = "checkBox_DoubleSpawnsRemover_Creatures";
            this.checkBox_DoubleSpawnsRemover_Creatures.Size = new System.Drawing.Size(105, 24);
            this.checkBox_DoubleSpawnsRemover_Creatures.TabIndex = 2;
            this.checkBox_DoubleSpawnsRemover_Creatures.Text = "Creatures";
            this.checkBox_DoubleSpawnsRemover_Creatures.UseVisualStyleBackColor = true;
            this.checkBox_DoubleSpawnsRemover_Creatures.CheckedChanged += new System.EventHandler(this.checkBox_CreaturesRemover_CheckedChanged);
            // 
            // label_DoubleSpawnsRemover_CreaturesRemoved
            // 
            this.label_DoubleSpawnsRemover_CreaturesRemoved.AutoSize = true;
            this.label_DoubleSpawnsRemover_CreaturesRemoved.Location = new System.Drawing.Point(494, 112);
            this.label_DoubleSpawnsRemover_CreaturesRemoved.Name = "label_DoubleSpawnsRemover_CreaturesRemoved";
            this.label_DoubleSpawnsRemover_CreaturesRemoved.Size = new System.Drawing.Size(165, 20);
            this.label_DoubleSpawnsRemover_CreaturesRemoved.TabIndex = 1;
            this.label_DoubleSpawnsRemover_CreaturesRemoved.Text = "No creatures removed";
            this.label_DoubleSpawnsRemover_CreaturesRemoved.Visible = false;
            // 
            // button_DoubleSpawnsRemover_ImportFile
            // 
            this.button_DoubleSpawnsRemover_ImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DoubleSpawnsRemover_ImportFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_DoubleSpawnsRemover_ImportFile.FlatAppearance.BorderSize = 5;
            this.button_DoubleSpawnsRemover_ImportFile.Font = new System.Drawing.Font("Sitka Small", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_DoubleSpawnsRemover_ImportFile.Location = new System.Drawing.Point(700, 29);
            this.button_DoubleSpawnsRemover_ImportFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_DoubleSpawnsRemover_ImportFile.Name = "button_DoubleSpawnsRemover_ImportFile";
            this.button_DoubleSpawnsRemover_ImportFile.Size = new System.Drawing.Size(220, 42);
            this.button_DoubleSpawnsRemover_ImportFile.TabIndex = 0;
            this.button_DoubleSpawnsRemover_ImportFile.Text = "Import File";
            this.button_DoubleSpawnsRemover_ImportFile.UseVisualStyleBackColor = true;
            this.button_DoubleSpawnsRemover_ImportFile.Click += new System.EventHandler(this.button_ImportFile_Click);
            // 
            // tabPage_CoreScriptTemplates
            // 
            this.tabPage_CoreScriptTemplates.Controls.Add(this.Core_script_creatore_preview);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.treeView_CoreScriptTemplates_HookBodies);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.label_CoreScriptTemplates_ScriptType);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.comboBox_CoreScriptTemplates_ScriptType);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.label_CoreScriptTemplates_ObjectId);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.textBox_CoreScriptTemplates_ObjectId);
            this.tabPage_CoreScriptTemplates.Controls.Add(this.listBox_CoreScriptTemplates_Hooks);
            this.tabPage_CoreScriptTemplates.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CoreScriptTemplates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_CoreScriptTemplates.Name = "tabPage_CoreScriptTemplates";
            this.tabPage_CoreScriptTemplates.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_CoreScriptTemplates.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_CoreScriptTemplates.TabIndex = 5;
            this.tabPage_CoreScriptTemplates.Text = "Core Script Templates";
            this.tabPage_CoreScriptTemplates.UseVisualStyleBackColor = true;
            // 
            // Core_script_creatore_preview
            // 
            this.Core_script_creatore_preview.Location = new System.Drawing.Point(500, 49);
            this.Core_script_creatore_preview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Core_script_creatore_preview.Name = "Core_script_creatore_preview";
            this.Core_script_creatore_preview.ReadOnly = true;
            this.Core_script_creatore_preview.Size = new System.Drawing.Size(1532, 889);
            this.Core_script_creatore_preview.TabIndex = 7;
            this.Core_script_creatore_preview.Text = "";
            // 
            // treeView_CoreScriptTemplates_HookBodies
            // 
            this.treeView_CoreScriptTemplates_HookBodies.CheckBoxes = true;
            this.treeView_CoreScriptTemplates_HookBodies.Location = new System.Drawing.Point(242, 49);
            this.treeView_CoreScriptTemplates_HookBodies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_CoreScriptTemplates_HookBodies.Name = "treeView_CoreScriptTemplates_HookBodies";
            this.treeView_CoreScriptTemplates_HookBodies.Size = new System.Drawing.Size(250, 884);
            this.treeView_CoreScriptTemplates_HookBodies.TabIndex = 6;
            this.treeView_CoreScriptTemplates_HookBodies.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_CoreScriptTemplates_HookBodies_AfterCheck);
            // 
            // label_CoreScriptTemplates_ScriptType
            // 
            this.label_CoreScriptTemplates_ScriptType.AutoSize = true;
            this.label_CoreScriptTemplates_ScriptType.Location = new System.Drawing.Point(6, 12);
            this.label_CoreScriptTemplates_ScriptType.Name = "label_CoreScriptTemplates_ScriptType";
            this.label_CoreScriptTemplates_ScriptType.Size = new System.Drawing.Size(92, 20);
            this.label_CoreScriptTemplates_ScriptType.TabIndex = 5;
            this.label_CoreScriptTemplates_ScriptType.Text = "Script Type:";
            // 
            // comboBox_CoreScriptTemplates_ScriptType
            // 
            this.comboBox_CoreScriptTemplates_ScriptType.Items.AddRange(new object[] {
            "Creature",
            "GameObject",
            "AreaTrigger",
            "Spell",
            "PlayerScript"});
            this.comboBox_CoreScriptTemplates_ScriptType.Location = new System.Drawing.Point(104, 9);
            this.comboBox_CoreScriptTemplates_ScriptType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_CoreScriptTemplates_ScriptType.Name = "comboBox_CoreScriptTemplates_ScriptType";
            this.comboBox_CoreScriptTemplates_ScriptType.Size = new System.Drawing.Size(121, 28);
            this.comboBox_CoreScriptTemplates_ScriptType.TabIndex = 1;
            this.comboBox_CoreScriptTemplates_ScriptType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // label_CoreScriptTemplates_ObjectId
            // 
            this.label_CoreScriptTemplates_ObjectId.AutoSize = true;
            this.label_CoreScriptTemplates_ObjectId.Location = new System.Drawing.Point(262, 12);
            this.label_CoreScriptTemplates_ObjectId.Name = "label_CoreScriptTemplates_ObjectId";
            this.label_CoreScriptTemplates_ObjectId.Size = new System.Drawing.Size(120, 20);
            this.label_CoreScriptTemplates_ObjectId.TabIndex = 4;
            this.label_CoreScriptTemplates_ObjectId.Text = "Enter Object Id:";
            // 
            // textBox_CoreScriptTemplates_ObjectId
            // 
            this.textBox_CoreScriptTemplates_ObjectId.Enabled = false;
            this.textBox_CoreScriptTemplates_ObjectId.Location = new System.Drawing.Point(388, 9);
            this.textBox_CoreScriptTemplates_ObjectId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_CoreScriptTemplates_ObjectId.MaxLength = 6;
            this.textBox_CoreScriptTemplates_ObjectId.Name = "textBox_CoreScriptTemplates_ObjectId";
            this.textBox_CoreScriptTemplates_ObjectId.Size = new System.Drawing.Size(70, 26);
            this.textBox_CoreScriptTemplates_ObjectId.TabIndex = 2;
            this.textBox_CoreScriptTemplates_ObjectId.TextChanged += new System.EventHandler(this.textBox_CoreScriptTemplates_ObjectId_TextChanged);
            this.textBox_CoreScriptTemplates_ObjectId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_CoreScriptTemplates_Enter);
            // 
            // listBox_CoreScriptTemplates_Hooks
            // 
            this.listBox_CoreScriptTemplates_Hooks.ItemHeight = 20;
            this.listBox_CoreScriptTemplates_Hooks.Location = new System.Drawing.Point(8, 49);
            this.listBox_CoreScriptTemplates_Hooks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox_CoreScriptTemplates_Hooks.Name = "listBox_CoreScriptTemplates_Hooks";
            this.listBox_CoreScriptTemplates_Hooks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_CoreScriptTemplates_Hooks.Size = new System.Drawing.Size(217, 884);
            this.listBox_CoreScriptTemplates_Hooks.TabIndex = 0;
            this.listBox_CoreScriptTemplates_Hooks.SelectedIndexChanged += new System.EventHandler(this.ListBox_CoreScriptTemplates_SelectedIndexChanged);
            // 
            // tabPage_Achievements
            // 
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_ModifierTreeChildNodes);
            this.tabPage_Achievements.Controls.Add(this.treeView_Achievements_ModifierTreeChildNodes);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_ModifierTrees);
            this.tabPage_Achievements.Controls.Add(this.treeView_Achievements_ModifierTrees);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_Criterias);
            this.tabPage_Achievements.Controls.Add(this.treeView_Achievements_Criterias);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeAmount);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeChildNodes);
            this.tabPage_Achievements.Controls.Add(this.label_Achievement_CriteriaTreeOperator);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeName);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeId);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_AchievementFlags);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_AchievementFaction);
            this.tabPage_Achievements.Controls.Add(this.treeView_Achievements_ChildNodes);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_AchievementName);
            this.tabPage_Achievements.Controls.Add(this.textBox_Achievements_AchievementId);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_AchievementId);
            this.tabPage_Achievements.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Achievements.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_Achievements.Name = "tabPage_Achievements";
            this.tabPage_Achievements.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_Achievements.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_Achievements.TabIndex = 6;
            this.tabPage_Achievements.Text = "Achievements";
            this.tabPage_Achievements.UseVisualStyleBackColor = true;
            // 
            // label_Achievements_ModifierTreeChildNodes
            // 
            this.label_Achievements_ModifierTreeChildNodes.AutoSize = true;
            this.label_Achievements_ModifierTreeChildNodes.Location = new System.Drawing.Point(1676, 225);
            this.label_Achievements_ModifierTreeChildNodes.Name = "label_Achievements_ModifierTreeChildNodes";
            this.label_Achievements_ModifierTreeChildNodes.Size = new System.Drawing.Size(194, 20);
            this.label_Achievements_ModifierTreeChildNodes.TabIndex = 16;
            this.label_Achievements_ModifierTreeChildNodes.Text = "Modifier Tree Child Nodes:";
            // 
            // treeView_Achievements_ModifierTreeChildNodes
            // 
            this.treeView_Achievements_ModifierTreeChildNodes.Location = new System.Drawing.Point(1522, 251);
            this.treeView_Achievements_ModifierTreeChildNodes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Achievements_ModifierTreeChildNodes.Name = "treeView_Achievements_ModifierTreeChildNodes";
            this.treeView_Achievements_ModifierTreeChildNodes.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_ModifierTreeChildNodes.TabIndex = 15;
            // 
            // label_Achievements_ModifierTrees
            // 
            this.label_Achievements_ModifierTrees.AutoSize = true;
            this.label_Achievements_ModifierTrees.Location = new System.Drawing.Point(1210, 226);
            this.label_Achievements_ModifierTrees.Name = "label_Achievements_ModifierTrees";
            this.label_Achievements_ModifierTrees.Size = new System.Drawing.Size(113, 20);
            this.label_Achievements_ModifierTrees.TabIndex = 14;
            this.label_Achievements_ModifierTrees.Text = "Modifier Trees:";
            // 
            // treeView_Achievements_ModifierTrees
            // 
            this.treeView_Achievements_ModifierTrees.Location = new System.Drawing.Point(1017, 251);
            this.treeView_Achievements_ModifierTrees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Achievements_ModifierTrees.Name = "treeView_Achievements_ModifierTrees";
            this.treeView_Achievements_ModifierTrees.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_ModifierTrees.TabIndex = 13;
            this.treeView_Achievements_ModifierTrees.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ModifierTrees_AfterCollapse);
            this.treeView_Achievements_ModifierTrees.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ModifierTrees_AfterExpand);
            // 
            // label_Achievements_Criterias
            // 
            this.label_Achievements_Criterias.AutoSize = true;
            this.label_Achievements_Criterias.Location = new System.Drawing.Point(726, 225);
            this.label_Achievements_Criterias.Name = "label_Achievements_Criterias";
            this.label_Achievements_Criterias.Size = new System.Drawing.Size(71, 20);
            this.label_Achievements_Criterias.TabIndex = 12;
            this.label_Achievements_Criterias.Text = "Criterias:";
            // 
            // treeView_Achievements_Criterias
            // 
            this.treeView_Achievements_Criterias.Location = new System.Drawing.Point(512, 251);
            this.treeView_Achievements_Criterias.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Achievements_Criterias.Name = "treeView_Achievements_Criterias";
            this.treeView_Achievements_Criterias.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_Criterias.TabIndex = 11;
            this.treeView_Achievements_Criterias.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_Criterias_AfterCollapse);
            this.treeView_Achievements_Criterias.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_Criterias_AfterExpand);
            // 
            // label_Achievements_CriteriaTreeAmount
            // 
            this.label_Achievements_CriteriaTreeAmount.AutoSize = true;
            this.label_Achievements_CriteriaTreeAmount.Location = new System.Drawing.Point(6, 169);
            this.label_Achievements_CriteriaTreeAmount.Name = "label_Achievements_CriteriaTreeAmount";
            this.label_Achievements_CriteriaTreeAmount.Size = new System.Drawing.Size(159, 20);
            this.label_Achievements_CriteriaTreeAmount.TabIndex = 10;
            this.label_Achievements_CriteriaTreeAmount.Text = "CriteriaTree Amount: ";
            // 
            // label_Achievements_CriteriaTreeChildNodes
            // 
            this.label_Achievements_CriteriaTreeChildNodes.AutoSize = true;
            this.label_Achievements_CriteriaTreeChildNodes.Location = new System.Drawing.Point(160, 228);
            this.label_Achievements_CriteriaTreeChildNodes.Name = "label_Achievements_CriteriaTreeChildNodes";
            this.label_Achievements_CriteriaTreeChildNodes.Size = new System.Drawing.Size(188, 20);
            this.label_Achievements_CriteriaTreeChildNodes.TabIndex = 9;
            this.label_Achievements_CriteriaTreeChildNodes.Text = "Criteria Tree Child Nodes:";
            // 
            // label_Achievement_CriteriaTreeOperator
            // 
            this.label_Achievement_CriteriaTreeOperator.AutoSize = true;
            this.label_Achievement_CriteriaTreeOperator.Location = new System.Drawing.Point(6, 195);
            this.label_Achievement_CriteriaTreeOperator.Name = "label_Achievement_CriteriaTreeOperator";
            this.label_Achievement_CriteriaTreeOperator.Size = new System.Drawing.Size(166, 20);
            this.label_Achievement_CriteriaTreeOperator.TabIndex = 8;
            this.label_Achievement_CriteriaTreeOperator.Text = "CriteriaTree Operator: ";
            // 
            // label_Achievements_CriteriaTreeName
            // 
            this.label_Achievements_CriteriaTreeName.AutoSize = true;
            this.label_Achievements_CriteriaTreeName.Location = new System.Drawing.Point(4, 145);
            this.label_Achievements_CriteriaTreeName.Name = "label_Achievements_CriteriaTreeName";
            this.label_Achievements_CriteriaTreeName.Size = new System.Drawing.Size(145, 20);
            this.label_Achievements_CriteriaTreeName.TabIndex = 7;
            this.label_Achievements_CriteriaTreeName.Text = "CriteriaTree Name: ";
            // 
            // label_Achievements_CriteriaTreeId
            // 
            this.label_Achievements_CriteriaTreeId.AutoSize = true;
            this.label_Achievements_CriteriaTreeId.Location = new System.Drawing.Point(6, 120);
            this.label_Achievements_CriteriaTreeId.Name = "label_Achievements_CriteriaTreeId";
            this.label_Achievements_CriteriaTreeId.Size = new System.Drawing.Size(117, 20);
            this.label_Achievements_CriteriaTreeId.TabIndex = 6;
            this.label_Achievements_CriteriaTreeId.Text = "CriteriaTree Id: ";
            // 
            // label_Achievements_AchievementFlags
            // 
            this.label_Achievements_AchievementFlags.AutoSize = true;
            this.label_Achievements_AchievementFlags.Location = new System.Drawing.Point(6, 85);
            this.label_Achievements_AchievementFlags.Name = "label_Achievements_AchievementFlags";
            this.label_Achievements_AchievementFlags.Size = new System.Drawing.Size(152, 20);
            this.label_Achievements_AchievementFlags.TabIndex = 5;
            this.label_Achievements_AchievementFlags.Text = "Achievement Flags: ";
            // 
            // label_Achievements_AchievementFaction
            // 
            this.label_Achievements_AchievementFaction.AutoSize = true;
            this.label_Achievements_AchievementFaction.Location = new System.Drawing.Point(4, 60);
            this.label_Achievements_AchievementFaction.Name = "label_Achievements_AchievementFaction";
            this.label_Achievements_AchievementFaction.Size = new System.Drawing.Size(166, 20);
            this.label_Achievements_AchievementFaction.TabIndex = 4;
            this.label_Achievements_AchievementFaction.Text = "Achievement Faction: ";
            // 
            // treeView_Achievements_ChildNodes
            // 
            this.treeView_Achievements_ChildNodes.Location = new System.Drawing.Point(4, 251);
            this.treeView_Achievements_ChildNodes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Achievements_ChildNodes.Name = "treeView_Achievements_ChildNodes";
            this.treeView_Achievements_ChildNodes.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_ChildNodes.TabIndex = 3;
            this.treeView_Achievements_ChildNodes.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ChildNodes_AfterCollapse);
            this.treeView_Achievements_ChildNodes.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ChildNodes_AfterExpand);
            // 
            // label_Achievements_AchievementName
            // 
            this.label_Achievements_AchievementName.AutoSize = true;
            this.label_Achievements_AchievementName.Location = new System.Drawing.Point(4, 35);
            this.label_Achievements_AchievementName.Name = "label_Achievements_AchievementName";
            this.label_Achievements_AchievementName.Size = new System.Drawing.Size(155, 20);
            this.label_Achievements_AchievementName.TabIndex = 2;
            this.label_Achievements_AchievementName.Text = "Achievement Name: ";
            // 
            // textBox_Achievements_AchievementId
            // 
            this.textBox_Achievements_AchievementId.Location = new System.Drawing.Point(130, 2);
            this.textBox_Achievements_AchievementId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Achievements_AchievementId.Name = "textBox_Achievements_AchievementId";
            this.textBox_Achievements_AchievementId.Size = new System.Drawing.Size(100, 26);
            this.textBox_Achievements_AchievementId.TabIndex = 1;
            this.textBox_Achievements_AchievementId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxAchievements_Id_KeyUp);
            // 
            // label_Achievements_AchievementId
            // 
            this.label_Achievements_AchievementId.AutoSize = true;
            this.label_Achievements_AchievementId.Location = new System.Drawing.Point(4, 8);
            this.label_Achievements_AchievementId.Name = "label_Achievements_AchievementId";
            this.label_Achievements_AchievementId.Size = new System.Drawing.Size(123, 20);
            this.label_Achievements_AchievementId.TabIndex = 0;
            this.label_Achievements_AchievementId.Text = "Achievement Id:";
            // 
            // tabPage_ConditionsCreator
            // 
            this.tabPage_ConditionsCreator.Controls.Add(this.button_ConditionsCreator_ClearConditions);
            this.tabPage_ConditionsCreator.Controls.Add(this.button_ConditionsCreator_AddCondition);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_Output);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ScriptName);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_NegativeCondition);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionValue3);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionValue2);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionValue1);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ScriptName);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_NegativeCondition);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ConditionValue3);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ConditionValue2);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ConditionValue1);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ConditionTarget);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionTarget);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionType);
            this.tabPage_ConditionsCreator.Controls.Add(this.comboBox_ConditionsCreator_ConditionType);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_ElseGroup);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ElseGroup);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_SourceId);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_SourceId);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_SourceEntry);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_SourceEntry);
            this.tabPage_ConditionsCreator.Controls.Add(this.textBox_ConditionsCreator_SourceGroup);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionSourceGroup);
            this.tabPage_ConditionsCreator.Controls.Add(this.comboBox_ConditionsCreator_ConditionSourceType);
            this.tabPage_ConditionsCreator.Controls.Add(this.label_ConditionsCreator_ConditionSourceType);
            this.tabPage_ConditionsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_ConditionsCreator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_ConditionsCreator.Name = "tabPage_ConditionsCreator";
            this.tabPage_ConditionsCreator.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_ConditionsCreator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_ConditionsCreator.TabIndex = 7;
            this.tabPage_ConditionsCreator.Text = "Conditions Creator";
            this.tabPage_ConditionsCreator.UseVisualStyleBackColor = true;
            // 
            // button_ConditionsCreator_ClearConditions
            // 
            this.button_ConditionsCreator_ClearConditions.Enabled = false;
            this.button_ConditionsCreator_ClearConditions.Location = new System.Drawing.Point(344, 409);
            this.button_ConditionsCreator_ClearConditions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_ConditionsCreator_ClearConditions.Name = "button_ConditionsCreator_ClearConditions";
            this.button_ConditionsCreator_ClearConditions.Size = new System.Drawing.Size(138, 31);
            this.button_ConditionsCreator_ClearConditions.TabIndex = 26;
            this.button_ConditionsCreator_ClearConditions.Text = "Clear Conditions";
            this.button_ConditionsCreator_ClearConditions.UseVisualStyleBackColor = true;
            this.button_ConditionsCreator_ClearConditions.Click += new System.EventHandler(this.button_ClearConditions_Click);
            // 
            // button_ConditionsCreator_AddCondition
            // 
            this.button_ConditionsCreator_AddCondition.Enabled = false;
            this.button_ConditionsCreator_AddCondition.Location = new System.Drawing.Point(12, 409);
            this.button_ConditionsCreator_AddCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_ConditionsCreator_AddCondition.Name = "button_ConditionsCreator_AddCondition";
            this.button_ConditionsCreator_AddCondition.Size = new System.Drawing.Size(126, 31);
            this.button_ConditionsCreator_AddCondition.TabIndex = 25;
            this.button_ConditionsCreator_AddCondition.Text = "Add Condition";
            this.button_ConditionsCreator_AddCondition.UseVisualStyleBackColor = true;
            this.button_ConditionsCreator_AddCondition.Click += new System.EventHandler(this.button_AddCondition_Click);
            // 
            // textBox_ConditionsCreator_Output
            // 
            this.textBox_ConditionsCreator_Output.Enabled = false;
            this.textBox_ConditionsCreator_Output.Location = new System.Drawing.Point(976, 6);
            this.textBox_ConditionsCreator_Output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_Output.Multiline = true;
            this.textBox_ConditionsCreator_Output.Name = "textBox_ConditionsCreator_Output";
            this.textBox_ConditionsCreator_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ConditionsCreator_Output.Size = new System.Drawing.Size(1057, 936);
            this.textBox_ConditionsCreator_Output.TabIndex = 24;
            // 
            // label_ConditionsCreator_ScriptName
            // 
            this.label_ConditionsCreator_ScriptName.AutoSize = true;
            this.label_ConditionsCreator_ScriptName.Location = new System.Drawing.Point(8, 366);
            this.label_ConditionsCreator_ScriptName.Name = "label_ConditionsCreator_ScriptName";
            this.label_ConditionsCreator_ScriptName.Size = new System.Drawing.Size(96, 20);
            this.label_ConditionsCreator_ScriptName.TabIndex = 23;
            this.label_ConditionsCreator_ScriptName.Text = "ScriptName:";
            // 
            // label_ConditionsCreator_NegativeCondition
            // 
            this.label_ConditionsCreator_NegativeCondition.AutoSize = true;
            this.label_ConditionsCreator_NegativeCondition.Location = new System.Drawing.Point(8, 334);
            this.label_ConditionsCreator_NegativeCondition.Name = "label_ConditionsCreator_NegativeCondition";
            this.label_ConditionsCreator_NegativeCondition.Size = new System.Drawing.Size(146, 20);
            this.label_ConditionsCreator_NegativeCondition.TabIndex = 22;
            this.label_ConditionsCreator_NegativeCondition.Text = "Negative Condition:";
            // 
            // label_ConditionsCreator_ConditionValue3
            // 
            this.label_ConditionsCreator_ConditionValue3.AutoSize = true;
            this.label_ConditionsCreator_ConditionValue3.Location = new System.Drawing.Point(8, 302);
            this.label_ConditionsCreator_ConditionValue3.Name = "label_ConditionsCreator_ConditionValue3";
            this.label_ConditionsCreator_ConditionValue3.Size = new System.Drawing.Size(138, 20);
            this.label_ConditionsCreator_ConditionValue3.TabIndex = 21;
            this.label_ConditionsCreator_ConditionValue3.Text = "Condition Value 3:";
            // 
            // label_ConditionsCreator_ConditionValue2
            // 
            this.label_ConditionsCreator_ConditionValue2.AutoSize = true;
            this.label_ConditionsCreator_ConditionValue2.Location = new System.Drawing.Point(8, 269);
            this.label_ConditionsCreator_ConditionValue2.Name = "label_ConditionsCreator_ConditionValue2";
            this.label_ConditionsCreator_ConditionValue2.Size = new System.Drawing.Size(138, 20);
            this.label_ConditionsCreator_ConditionValue2.TabIndex = 20;
            this.label_ConditionsCreator_ConditionValue2.Text = "Condition Value 2:";
            // 
            // label_ConditionsCreator_ConditionValue1
            // 
            this.label_ConditionsCreator_ConditionValue1.AutoSize = true;
            this.label_ConditionsCreator_ConditionValue1.Location = new System.Drawing.Point(8, 238);
            this.label_ConditionsCreator_ConditionValue1.Name = "label_ConditionsCreator_ConditionValue1";
            this.label_ConditionsCreator_ConditionValue1.Size = new System.Drawing.Size(138, 20);
            this.label_ConditionsCreator_ConditionValue1.TabIndex = 19;
            this.label_ConditionsCreator_ConditionValue1.Text = "Condition Value 1:";
            // 
            // textBox_ConditionsCreator_ScriptName
            // 
            this.textBox_ConditionsCreator_ScriptName.Enabled = false;
            this.textBox_ConditionsCreator_ScriptName.Location = new System.Drawing.Point(160, 362);
            this.textBox_ConditionsCreator_ScriptName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ScriptName.MaxLength = 50;
            this.textBox_ConditionsCreator_ScriptName.Name = "textBox_ConditionsCreator_ScriptName";
            this.textBox_ConditionsCreator_ScriptName.Size = new System.Drawing.Size(320, 26);
            this.textBox_ConditionsCreator_ScriptName.TabIndex = 18;
            // 
            // textBox_ConditionsCreator_NegativeCondition
            // 
            this.textBox_ConditionsCreator_NegativeCondition.Enabled = false;
            this.textBox_ConditionsCreator_NegativeCondition.Location = new System.Drawing.Point(160, 331);
            this.textBox_ConditionsCreator_NegativeCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_NegativeCondition.MaxLength = 1;
            this.textBox_ConditionsCreator_NegativeCondition.Name = "textBox_ConditionsCreator_NegativeCondition";
            this.textBox_ConditionsCreator_NegativeCondition.Size = new System.Drawing.Size(30, 26);
            this.textBox_ConditionsCreator_NegativeCondition.TabIndex = 17;
            this.textBox_ConditionsCreator_NegativeCondition.Text = "0";
            this.textBox_ConditionsCreator_NegativeCondition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_ConditionsCreator_ConditionValue3
            // 
            this.textBox_ConditionsCreator_ConditionValue3.Enabled = false;
            this.textBox_ConditionsCreator_ConditionValue3.Location = new System.Drawing.Point(160, 298);
            this.textBox_ConditionsCreator_ConditionValue3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ConditionValue3.MaxLength = 6;
            this.textBox_ConditionsCreator_ConditionValue3.Name = "textBox_ConditionsCreator_ConditionValue3";
            this.textBox_ConditionsCreator_ConditionValue3.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionsCreator_ConditionValue3.TabIndex = 16;
            // 
            // textBox_ConditionsCreator_ConditionValue2
            // 
            this.textBox_ConditionsCreator_ConditionValue2.Enabled = false;
            this.textBox_ConditionsCreator_ConditionValue2.Location = new System.Drawing.Point(160, 268);
            this.textBox_ConditionsCreator_ConditionValue2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ConditionValue2.MaxLength = 6;
            this.textBox_ConditionsCreator_ConditionValue2.Name = "textBox_ConditionsCreator_ConditionValue2";
            this.textBox_ConditionsCreator_ConditionValue2.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionsCreator_ConditionValue2.TabIndex = 15;
            // 
            // textBox_ConditionsCreator_ConditionValue1
            // 
            this.textBox_ConditionsCreator_ConditionValue1.Enabled = false;
            this.textBox_ConditionsCreator_ConditionValue1.Location = new System.Drawing.Point(160, 235);
            this.textBox_ConditionsCreator_ConditionValue1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ConditionValue1.MaxLength = 6;
            this.textBox_ConditionsCreator_ConditionValue1.Name = "textBox_ConditionsCreator_ConditionValue1";
            this.textBox_ConditionsCreator_ConditionValue1.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionsCreator_ConditionValue1.TabIndex = 14;
            // 
            // textBox_ConditionsCreator_ConditionTarget
            // 
            this.textBox_ConditionsCreator_ConditionTarget.Enabled = false;
            this.textBox_ConditionsCreator_ConditionTarget.Location = new System.Drawing.Point(160, 202);
            this.textBox_ConditionsCreator_ConditionTarget.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ConditionTarget.MaxLength = 1;
            this.textBox_ConditionsCreator_ConditionTarget.Name = "textBox_ConditionsCreator_ConditionTarget";
            this.textBox_ConditionsCreator_ConditionTarget.Size = new System.Drawing.Size(30, 26);
            this.textBox_ConditionsCreator_ConditionTarget.TabIndex = 13;
            this.textBox_ConditionsCreator_ConditionTarget.Text = "0";
            this.textBox_ConditionsCreator_ConditionTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ConditionsCreator_ConditionTarget
            // 
            this.label_ConditionsCreator_ConditionTarget.AutoSize = true;
            this.label_ConditionsCreator_ConditionTarget.Location = new System.Drawing.Point(8, 206);
            this.label_ConditionsCreator_ConditionTarget.Name = "label_ConditionsCreator_ConditionTarget";
            this.label_ConditionsCreator_ConditionTarget.Size = new System.Drawing.Size(130, 20);
            this.label_ConditionsCreator_ConditionTarget.TabIndex = 12;
            this.label_ConditionsCreator_ConditionTarget.Text = "Condition Target:";
            // 
            // label_ConditionsCreator_ConditionType
            // 
            this.label_ConditionsCreator_ConditionType.AutoSize = true;
            this.label_ConditionsCreator_ConditionType.Location = new System.Drawing.Point(8, 172);
            this.label_ConditionsCreator_ConditionType.Name = "label_ConditionsCreator_ConditionType";
            this.label_ConditionsCreator_ConditionType.Size = new System.Drawing.Size(118, 20);
            this.label_ConditionsCreator_ConditionType.TabIndex = 11;
            this.label_ConditionsCreator_ConditionType.Text = "Condition Type:";
            // 
            // comboBox_ConditionsCreator_ConditionType
            // 
            this.comboBox_ConditionsCreator_ConditionType.Enabled = false;
            this.comboBox_ConditionsCreator_ConditionType.FormattingEnabled = true;
            this.comboBox_ConditionsCreator_ConditionType.Location = new System.Drawing.Point(160, 169);
            this.comboBox_ConditionsCreator_ConditionType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_ConditionsCreator_ConditionType.Name = "comboBox_ConditionsCreator_ConditionType";
            this.comboBox_ConditionsCreator_ConditionType.Size = new System.Drawing.Size(320, 28);
            this.comboBox_ConditionsCreator_ConditionType.TabIndex = 10;
            this.comboBox_ConditionsCreator_ConditionType.DropDown += new System.EventHandler(this.comboBox_ConditionType_DropDown);
            this.comboBox_ConditionsCreator_ConditionType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ConditionType_SelectedIndexChanged);
            // 
            // textBox_ConditionsCreator_ElseGroup
            // 
            this.textBox_ConditionsCreator_ElseGroup.Enabled = false;
            this.textBox_ConditionsCreator_ElseGroup.Location = new System.Drawing.Point(160, 138);
            this.textBox_ConditionsCreator_ElseGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_ElseGroup.MaxLength = 2;
            this.textBox_ConditionsCreator_ElseGroup.Name = "textBox_ConditionsCreator_ElseGroup";
            this.textBox_ConditionsCreator_ElseGroup.Size = new System.Drawing.Size(50, 26);
            this.textBox_ConditionsCreator_ElseGroup.TabIndex = 9;
            this.textBox_ConditionsCreator_ElseGroup.Text = "0";
            this.textBox_ConditionsCreator_ElseGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ConditionsCreator_ElseGroup
            // 
            this.label_ConditionsCreator_ElseGroup.AutoSize = true;
            this.label_ConditionsCreator_ElseGroup.Location = new System.Drawing.Point(8, 142);
            this.label_ConditionsCreator_ElseGroup.Name = "label_ConditionsCreator_ElseGroup";
            this.label_ConditionsCreator_ElseGroup.Size = new System.Drawing.Size(93, 20);
            this.label_ConditionsCreator_ElseGroup.TabIndex = 8;
            this.label_ConditionsCreator_ElseGroup.Text = "Else Group:";
            // 
            // textBox_ConditionsCreator_SourceId
            // 
            this.textBox_ConditionsCreator_SourceId.Enabled = false;
            this.textBox_ConditionsCreator_SourceId.Location = new System.Drawing.Point(160, 106);
            this.textBox_ConditionsCreator_SourceId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_SourceId.MaxLength = 1;
            this.textBox_ConditionsCreator_SourceId.Name = "textBox_ConditionsCreator_SourceId";
            this.textBox_ConditionsCreator_SourceId.Size = new System.Drawing.Size(30, 26);
            this.textBox_ConditionsCreator_SourceId.TabIndex = 7;
            this.textBox_ConditionsCreator_SourceId.Text = "0";
            this.textBox_ConditionsCreator_SourceId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ConditionsCreator_SourceId
            // 
            this.label_ConditionsCreator_SourceId.AutoSize = true;
            this.label_ConditionsCreator_SourceId.Location = new System.Drawing.Point(8, 109);
            this.label_ConditionsCreator_SourceId.Name = "label_ConditionsCreator_SourceId";
            this.label_ConditionsCreator_SourceId.Size = new System.Drawing.Size(82, 20);
            this.label_ConditionsCreator_SourceId.TabIndex = 6;
            this.label_ConditionsCreator_SourceId.Text = "Source Id:";
            // 
            // textBox_ConditionsCreator_SourceEntry
            // 
            this.textBox_ConditionsCreator_SourceEntry.Enabled = false;
            this.textBox_ConditionsCreator_SourceEntry.Location = new System.Drawing.Point(160, 74);
            this.textBox_ConditionsCreator_SourceEntry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_SourceEntry.MaxLength = 6;
            this.textBox_ConditionsCreator_SourceEntry.Name = "textBox_ConditionsCreator_SourceEntry";
            this.textBox_ConditionsCreator_SourceEntry.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionsCreator_SourceEntry.TabIndex = 5;
            // 
            // label_ConditionsCreator_SourceEntry
            // 
            this.label_ConditionsCreator_SourceEntry.AutoSize = true;
            this.label_ConditionsCreator_SourceEntry.Location = new System.Drawing.Point(8, 78);
            this.label_ConditionsCreator_SourceEntry.Name = "label_ConditionsCreator_SourceEntry";
            this.label_ConditionsCreator_SourceEntry.Size = new System.Drawing.Size(105, 20);
            this.label_ConditionsCreator_SourceEntry.TabIndex = 4;
            this.label_ConditionsCreator_SourceEntry.Text = "Source Entry:";
            // 
            // textBox_ConditionsCreator_SourceGroup
            // 
            this.textBox_ConditionsCreator_SourceGroup.Enabled = false;
            this.textBox_ConditionsCreator_SourceGroup.Location = new System.Drawing.Point(160, 42);
            this.textBox_ConditionsCreator_SourceGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConditionsCreator_SourceGroup.MaxLength = 6;
            this.textBox_ConditionsCreator_SourceGroup.Name = "textBox_ConditionsCreator_SourceGroup";
            this.textBox_ConditionsCreator_SourceGroup.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionsCreator_SourceGroup.TabIndex = 3;
            // 
            // label_ConditionsCreator_ConditionSourceGroup
            // 
            this.label_ConditionsCreator_ConditionSourceGroup.AutoSize = true;
            this.label_ConditionsCreator_ConditionSourceGroup.Location = new System.Drawing.Point(8, 45);
            this.label_ConditionsCreator_ConditionSourceGroup.Name = "label_ConditionsCreator_ConditionSourceGroup";
            this.label_ConditionsCreator_ConditionSourceGroup.Size = new System.Drawing.Size(113, 20);
            this.label_ConditionsCreator_ConditionSourceGroup.TabIndex = 2;
            this.label_ConditionsCreator_ConditionSourceGroup.Text = "Source Group:";
            // 
            // comboBox_ConditionsCreator_ConditionSourceType
            // 
            this.comboBox_ConditionsCreator_ConditionSourceType.FormattingEnabled = true;
            this.comboBox_ConditionsCreator_ConditionSourceType.Location = new System.Drawing.Point(160, 6);
            this.comboBox_ConditionsCreator_ConditionSourceType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_ConditionsCreator_ConditionSourceType.Name = "comboBox_ConditionsCreator_ConditionSourceType";
            this.comboBox_ConditionsCreator_ConditionSourceType.Size = new System.Drawing.Size(320, 28);
            this.comboBox_ConditionsCreator_ConditionSourceType.TabIndex = 1;
            this.comboBox_ConditionsCreator_ConditionSourceType.DropDown += new System.EventHandler(this.comboBox_ConditionSourceType_DropDown);
            this.comboBox_ConditionsCreator_ConditionSourceType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ConditionSourceType_SelectedIndexChanged);
            // 
            // label_ConditionsCreator_ConditionSourceType
            // 
            this.label_ConditionsCreator_ConditionSourceType.AutoSize = true;
            this.label_ConditionsCreator_ConditionSourceType.Location = new System.Drawing.Point(8, 9);
            this.label_ConditionsCreator_ConditionSourceType.Name = "label_ConditionsCreator_ConditionSourceType";
            this.label_ConditionsCreator_ConditionSourceType.Size = new System.Drawing.Size(102, 20);
            this.label_ConditionsCreator_ConditionSourceType.TabIndex = 0;
            this.label_ConditionsCreator_ConditionSourceType.Text = "Source Type:";
            // 
            // tabPage_ModifierTrees
            // 
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_TertiaryAsset);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_SecondaryAsset);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_Asset);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_Amount);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_Operator);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_Type);
            this.tabPage_ModifierTrees.Controls.Add(this.textBox_ModifierTrees_ModifierTreeId);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_ModifierTreeId);
            this.tabPage_ModifierTrees.Controls.Add(this.label_ModifierTrees_ModifierTrees);
            this.tabPage_ModifierTrees.Controls.Add(this.treeView_ModifierTrees_ModifierTrees);
            this.tabPage_ModifierTrees.Location = new System.Drawing.Point(4, 29);
            this.tabPage_ModifierTrees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_ModifierTrees.Name = "tabPage_ModifierTrees";
            this.tabPage_ModifierTrees.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_ModifierTrees.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_ModifierTrees.TabIndex = 8;
            this.tabPage_ModifierTrees.Text = "Modifier Trees";
            this.tabPage_ModifierTrees.UseVisualStyleBackColor = true;
            // 
            // label_ModifierTrees_TertiaryAsset
            // 
            this.label_ModifierTrees_TertiaryAsset.AutoSize = true;
            this.label_ModifierTrees_TertiaryAsset.Location = new System.Drawing.Point(6, 142);
            this.label_ModifierTrees_TertiaryAsset.Name = "label_ModifierTrees_TertiaryAsset";
            this.label_ModifierTrees_TertiaryAsset.Size = new System.Drawing.Size(110, 20);
            this.label_ModifierTrees_TertiaryAsset.TabIndex = 25;
            this.label_ModifierTrees_TertiaryAsset.Text = "Tertiary Asset:";
            // 
            // label_ModifierTrees_SecondaryAsset
            // 
            this.label_ModifierTrees_SecondaryAsset.AutoSize = true;
            this.label_ModifierTrees_SecondaryAsset.Location = new System.Drawing.Point(6, 122);
            this.label_ModifierTrees_SecondaryAsset.Name = "label_ModifierTrees_SecondaryAsset";
            this.label_ModifierTrees_SecondaryAsset.Size = new System.Drawing.Size(134, 20);
            this.label_ModifierTrees_SecondaryAsset.TabIndex = 24;
            this.label_ModifierTrees_SecondaryAsset.Text = "Secondary Asset:";
            // 
            // label_ModifierTrees_Asset
            // 
            this.label_ModifierTrees_Asset.AutoSize = true;
            this.label_ModifierTrees_Asset.Location = new System.Drawing.Point(6, 102);
            this.label_ModifierTrees_Asset.Name = "label_ModifierTrees_Asset";
            this.label_ModifierTrees_Asset.Size = new System.Drawing.Size(54, 20);
            this.label_ModifierTrees_Asset.TabIndex = 23;
            this.label_ModifierTrees_Asset.Text = "Asset:";
            // 
            // label_ModifierTrees_Amount
            // 
            this.label_ModifierTrees_Amount.AutoSize = true;
            this.label_ModifierTrees_Amount.Location = new System.Drawing.Point(6, 62);
            this.label_ModifierTrees_Amount.Name = "label_ModifierTrees_Amount";
            this.label_ModifierTrees_Amount.Size = new System.Drawing.Size(69, 20);
            this.label_ModifierTrees_Amount.TabIndex = 22;
            this.label_ModifierTrees_Amount.Text = "Amount:";
            // 
            // label_ModifierTrees_Operator
            // 
            this.label_ModifierTrees_Operator.AutoSize = true;
            this.label_ModifierTrees_Operator.Location = new System.Drawing.Point(6, 82);
            this.label_ModifierTrees_Operator.Name = "label_ModifierTrees_Operator";
            this.label_ModifierTrees_Operator.Size = new System.Drawing.Size(76, 20);
            this.label_ModifierTrees_Operator.TabIndex = 21;
            this.label_ModifierTrees_Operator.Text = "Operator:";
            // 
            // label_ModifierTrees_Type
            // 
            this.label_ModifierTrees_Type.AutoSize = true;
            this.label_ModifierTrees_Type.Location = new System.Drawing.Point(6, 42);
            this.label_ModifierTrees_Type.Name = "label_ModifierTrees_Type";
            this.label_ModifierTrees_Type.Size = new System.Drawing.Size(47, 20);
            this.label_ModifierTrees_Type.TabIndex = 20;
            this.label_ModifierTrees_Type.Text = "Type:";
            // 
            // textBox_ModifierTrees_ModifierTreeId
            // 
            this.textBox_ModifierTrees_ModifierTreeId.Location = new System.Drawing.Point(135, 10);
            this.textBox_ModifierTrees_ModifierTreeId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ModifierTrees_ModifierTreeId.Name = "textBox_ModifierTrees_ModifierTreeId";
            this.textBox_ModifierTrees_ModifierTreeId.Size = new System.Drawing.Size(100, 26);
            this.textBox_ModifierTrees_ModifierTreeId.TabIndex = 19;
            this.textBox_ModifierTrees_ModifierTreeId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_ModifierTrees_ModifierTreeId_KeyUp);
            // 
            // label_ModifierTrees_ModifierTreeId
            // 
            this.label_ModifierTrees_ModifierTreeId.AutoSize = true;
            this.label_ModifierTrees_ModifierTreeId.Location = new System.Drawing.Point(6, 12);
            this.label_ModifierTrees_ModifierTreeId.Name = "label_ModifierTrees_ModifierTreeId";
            this.label_ModifierTrees_ModifierTreeId.Size = new System.Drawing.Size(123, 20);
            this.label_ModifierTrees_ModifierTreeId.TabIndex = 18;
            this.label_ModifierTrees_ModifierTreeId.Text = "Modifier Tree Id:";
            // 
            // label_ModifierTrees_ModifierTrees
            // 
            this.label_ModifierTrees_ModifierTrees.AutoSize = true;
            this.label_ModifierTrees_ModifierTrees.Location = new System.Drawing.Point(957, 132);
            this.label_ModifierTrees_ModifierTrees.Name = "label_ModifierTrees_ModifierTrees";
            this.label_ModifierTrees_ModifierTrees.Size = new System.Drawing.Size(0, 20);
            this.label_ModifierTrees_ModifierTrees.TabIndex = 15;
            // 
            // treeView_ModifierTrees_ModifierTrees
            // 
            this.treeView_ModifierTrees_ModifierTrees.Location = new System.Drawing.Point(3, 165);
            this.treeView_ModifierTrees_ModifierTrees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_ModifierTrees_ModifierTrees.Name = "treeView_ModifierTrees_ModifierTrees";
            this.treeView_ModifierTrees_ModifierTrees.Size = new System.Drawing.Size(2028, 766);
            this.treeView_ModifierTrees_ModifierTrees.TabIndex = 14;
            // 
            // tabPage_ParsedFileAdvisor
            // 
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_PlayerCompletedQuests);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_PlayerCompletedQuests);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_CreatureEquipmentId);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_CreatureEquipmentId);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_LosConversationsOrTexts);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_LosConversationsOrTexts);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_QuestConversationsOrTexts);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_SpellDestinations);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_SpellDestinations);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.checkBox_ParsedFileAdvisor_CreateDataFile);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.toolStrip_ParsedFileAdvisor);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_PlayerCastedSpells);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_PlayerCastedSpells);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_AreaTriggerSplines);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.label_ParsedFileAdvisor_AreaTriggerSplines);
            this.tabPage_ParsedFileAdvisor.Controls.Add(this.textBox_ParsedFileAdvisor_Output);
            this.tabPage_ParsedFileAdvisor.Location = new System.Drawing.Point(4, 29);
            this.tabPage_ParsedFileAdvisor.Name = "tabPage_ParsedFileAdvisor";
            this.tabPage_ParsedFileAdvisor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ParsedFileAdvisor.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_ParsedFileAdvisor.TabIndex = 9;
            this.tabPage_ParsedFileAdvisor.Text = "Parsed File Advisor";
            this.tabPage_ParsedFileAdvisor.UseVisualStyleBackColor = true;
            // 
            // textBox_ParsedFileAdvisor_PlayerCompletedQuests
            // 
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Enabled = false;
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Location = new System.Drawing.Point(21, 448);
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Name = "textBox_ParsedFileAdvisor_PlayerCompletedQuests";
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.Size = new System.Drawing.Size(140, 26);
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.TabIndex = 40;
            this.textBox_ParsedFileAdvisor_PlayerCompletedQuests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_PlayerCompletedQuests_KeyDown);
            // 
            // label_ParsedFileAdvisor_PlayerCompletedQuests
            // 
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.AutoSize = true;
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.Location = new System.Drawing.Point(6, 426);
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.Name = "label_ParsedFileAdvisor_PlayerCompletedQuests";
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.Size = new System.Drawing.Size(188, 20);
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.TabIndex = 39;
            this.label_ParsedFileAdvisor_PlayerCompletedQuests.Text = "Player Completed Quests";
            // 
            // textBox_ParsedFileAdvisor_CreatureEquipmentId
            // 
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.Enabled = false;
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.Location = new System.Drawing.Point(21, 387);
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.Name = "textBox_ParsedFileAdvisor_CreatureEquipmentId";
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.Size = new System.Drawing.Size(140, 26);
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.TabIndex = 38;
            this.textBox_ParsedFileAdvisor_CreatureEquipmentId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_CreatureEquipmentId_KeyDown);
            // 
            // label_ParsedFileAdvisor_CreatureEquipmentId
            // 
            this.label_ParsedFileAdvisor_CreatureEquipmentId.AutoSize = true;
            this.label_ParsedFileAdvisor_CreatureEquipmentId.Location = new System.Drawing.Point(6, 365);
            this.label_ParsedFileAdvisor_CreatureEquipmentId.Name = "label_ParsedFileAdvisor_CreatureEquipmentId";
            this.label_ParsedFileAdvisor_CreatureEquipmentId.Size = new System.Drawing.Size(170, 20);
            this.label_ParsedFileAdvisor_CreatureEquipmentId.TabIndex = 37;
            this.label_ParsedFileAdvisor_CreatureEquipmentId.Text = "Creature Equipment Id";
            // 
            // textBox_ParsedFileAdvisor_LosConversationsOrTexts
            // 
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Enabled = false;
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Location = new System.Drawing.Point(21, 326);
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Name = "textBox_ParsedFileAdvisor_LosConversationsOrTexts";
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.Size = new System.Drawing.Size(140, 26);
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.TabIndex = 36;
            this.textBox_ParsedFileAdvisor_LosConversationsOrTexts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_LosConversationsOrTexts_KeyDown);
            // 
            // label_ParsedFileAdvisor_LosConversationsOrTexts
            // 
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.AutoSize = true;
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.Location = new System.Drawing.Point(6, 304);
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.Name = "label_ParsedFileAdvisor_LosConversationsOrTexts";
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.Size = new System.Drawing.Size(182, 20);
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.TabIndex = 35;
            this.label_ParsedFileAdvisor_LosConversationsOrTexts.Text = "Los Conversations/Texts";
            // 
            // textBox_ParsedFileAdvisor_QuestConversationsOrTexts
            // 
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Enabled = false;
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Location = new System.Drawing.Point(21, 265);
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Name = "textBox_ParsedFileAdvisor_QuestConversationsOrTexts";
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.Size = new System.Drawing.Size(140, 26);
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.TabIndex = 34;
            this.textBox_ParsedFileAdvisor_QuestConversationsOrTexts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_QuestConversations_KeyDown);
            // 
            // label_ParsedFileAdvisor_QuestConversationsOrTexts
            // 
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.AutoSize = true;
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.Location = new System.Drawing.Point(2, 243);
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.Name = "label_ParsedFileAdvisor_QuestConversationsOrTexts";
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.Size = new System.Drawing.Size(199, 20);
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.TabIndex = 33;
            this.label_ParsedFileAdvisor_QuestConversationsOrTexts.Text = "Quest Conversations/Texts";
            // 
            // textBox_ParsedFileAdvisor_SpellDestinations
            // 
            this.textBox_ParsedFileAdvisor_SpellDestinations.Enabled = false;
            this.textBox_ParsedFileAdvisor_SpellDestinations.Location = new System.Drawing.Point(21, 199);
            this.textBox_ParsedFileAdvisor_SpellDestinations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_SpellDestinations.Name = "textBox_ParsedFileAdvisor_SpellDestinations";
            this.textBox_ParsedFileAdvisor_SpellDestinations.Size = new System.Drawing.Size(140, 26);
            this.textBox_ParsedFileAdvisor_SpellDestinations.TabIndex = 32;
            this.textBox_ParsedFileAdvisor_SpellDestinations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_SpellDestinations_KeyDown);
            // 
            // label_ParsedFileAdvisor_SpellDestinations
            // 
            this.label_ParsedFileAdvisor_SpellDestinations.AutoSize = true;
            this.label_ParsedFileAdvisor_SpellDestinations.Location = new System.Drawing.Point(17, 177);
            this.label_ParsedFileAdvisor_SpellDestinations.Name = "label_ParsedFileAdvisor_SpellDestinations";
            this.label_ParsedFileAdvisor_SpellDestinations.Size = new System.Drawing.Size(137, 20);
            this.label_ParsedFileAdvisor_SpellDestinations.TabIndex = 31;
            this.label_ParsedFileAdvisor_SpellDestinations.Text = "Spell Destinations";
            // 
            // checkBox_ParsedFileAdvisor_CreateDataFile
            // 
            this.checkBox_ParsedFileAdvisor_CreateDataFile.AutoSize = true;
            this.checkBox_ParsedFileAdvisor_CreateDataFile.BackColor = System.Drawing.Color.LightGray;
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Location = new System.Drawing.Point(1732, 10);
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Name = "checkBox_ParsedFileAdvisor_CreateDataFile";
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Size = new System.Drawing.Size(161, 29);
            this.checkBox_ParsedFileAdvisor_CreateDataFile.TabIndex = 30;
            this.checkBox_ParsedFileAdvisor_CreateDataFile.Text = "Create Data File";
            this.checkBox_ParsedFileAdvisor_CreateDataFile.UseVisualStyleBackColor = false;
            // 
            // toolStrip_ParsedFileAdvisor
            // 
            this.toolStrip_ParsedFileAdvisor.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip_ParsedFileAdvisor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus,
            this.toolStripButton_ParsedFileAdvisor_ImportSniff});
            this.toolStrip_ParsedFileAdvisor.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_ParsedFileAdvisor.Name = "toolStrip_ParsedFileAdvisor";
            this.toolStrip_ParsedFileAdvisor.Size = new System.Drawing.Size(2034, 38);
            this.toolStrip_ParsedFileAdvisor.TabIndex = 16;
            this.toolStrip_ParsedFileAdvisor.Text = "toolStrip1";
            // 
            // toolStripStatusLabel_ParsedFileAdvisor_FileStatus
            // 
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.AutoToolTip = true;
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Name = "toolStripStatusLabel_ParsedFileAdvisor_FileStatus";
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Size = new System.Drawing.Size(131, 31);
            this.toolStripStatusLabel_ParsedFileAdvisor_FileStatus.Text = "No File Loaded";
            // 
            // toolStripButton_ParsedFileAdvisor_ImportSniff
            // 
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_ParsedFileAdvisor_ImportSniff.Image")));
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Name = "toolStripButton_ParsedFileAdvisor_ImportSniff";
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Size = new System.Drawing.Size(128, 33);
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Text = "Import Sniff";
            this.toolStripButton_ParsedFileAdvisor_ImportSniff.Click += new System.EventHandler(this.toolStripButton_ParsedFileAdvisor_ImportSniff_Click);
            // 
            // textBox_ParsedFileAdvisor_PlayerCastedSpells
            // 
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.Enabled = false;
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.Location = new System.Drawing.Point(21, 134);
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.Name = "textBox_ParsedFileAdvisor_PlayerCastedSpells";
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.Size = new System.Drawing.Size(139, 26);
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.TabIndex = 15;
            this.textBox_ParsedFileAdvisor_PlayerCastedSpells.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ParsedFileAdvisor_PlayerCastedSpells_KeyDown);
            // 
            // label_ParsedFileAdvisor_PlayerCastedSpells
            // 
            this.label_ParsedFileAdvisor_PlayerCastedSpells.AutoSize = true;
            this.label_ParsedFileAdvisor_PlayerCastedSpells.Location = new System.Drawing.Point(17, 112);
            this.label_ParsedFileAdvisor_PlayerCastedSpells.Name = "label_ParsedFileAdvisor_PlayerCastedSpells";
            this.label_ParsedFileAdvisor_PlayerCastedSpells.Size = new System.Drawing.Size(154, 20);
            this.label_ParsedFileAdvisor_PlayerCastedSpells.TabIndex = 14;
            this.label_ParsedFileAdvisor_PlayerCastedSpells.Text = "Player Casted Spells";
            // 
            // textBox_ParsedFileAdvisor_AreaTriggerSplines
            // 
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.Enabled = false;
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.Location = new System.Drawing.Point(21, 74);
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.Name = "textBox_ParsedFileAdvisor_AreaTriggerSplines";
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.Size = new System.Drawing.Size(139, 26);
            this.textBox_ParsedFileAdvisor_AreaTriggerSplines.TabIndex = 13;
            // 
            // label_ParsedFileAdvisor_AreaTriggerSplines
            // 
            this.label_ParsedFileAdvisor_AreaTriggerSplines.AutoSize = true;
            this.label_ParsedFileAdvisor_AreaTriggerSplines.Location = new System.Drawing.Point(17, 52);
            this.label_ParsedFileAdvisor_AreaTriggerSplines.Name = "label_ParsedFileAdvisor_AreaTriggerSplines";
            this.label_ParsedFileAdvisor_AreaTriggerSplines.Size = new System.Drawing.Size(144, 20);
            this.label_ParsedFileAdvisor_AreaTriggerSplines.TabIndex = 12;
            this.label_ParsedFileAdvisor_AreaTriggerSplines.Text = "Areatrigger Splines";
            // 
            // textBox_ParsedFileAdvisor_Output
            // 
            this.textBox_ParsedFileAdvisor_Output.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBox_ParsedFileAdvisor_Output.Location = new System.Drawing.Point(207, 41);
            this.textBox_ParsedFileAdvisor_Output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParsedFileAdvisor_Output.MaxLength = 1000000;
            this.textBox_ParsedFileAdvisor_Output.Multiline = true;
            this.textBox_ParsedFileAdvisor_Output.Name = "textBox_ParsedFileAdvisor_Output";
            this.textBox_ParsedFileAdvisor_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ParsedFileAdvisor_Output.Size = new System.Drawing.Size(1830, 903);
            this.textBox_ParsedFileAdvisor_Output.TabIndex = 11;
            this.textBox_ParsedFileAdvisor_Output.WordWrap = false;
            // 
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.BackColor = System.Drawing.Color.LightGray;
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus,
            this.toolStripStatusLabel_CurrentAction});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 977);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(2061, 32);
            this.statusStrip_LoadedFile.TabIndex = 2;
            this.statusStrip_LoadedFile.Text = "statusStrip";
            // 
            // toolStripStatusLabel_FileStatus
            // 
            this.toolStripStatusLabel_FileStatus.Name = "toolStripStatusLabel_FileStatus";
            this.toolStripStatusLabel_FileStatus.Size = new System.Drawing.Size(131, 25);
            this.toolStripStatusLabel_FileStatus.Text = "No File Loaded";
            // 
            // toolStripStatusLabel_CurrentAction
            // 
            this.toolStripStatusLabel_CurrentAction.Name = "toolStripStatusLabel_CurrentAction";
            this.toolStripStatusLabel_CurrentAction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel_CurrentAction.Size = new System.Drawing.Size(0, 25);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2061, 1009);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Wow Developer Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage_CreatureScriptsCreator.ResumeLayout(false);
            this.tabPage_CreatureScriptsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CreatureScriptsCreator_Spells)).EndInit();
            this.contextMenuStrip_CreatureScriptsCreator.ResumeLayout(false);
            this.toolStrip_CreatureScriptsCreator.ResumeLayout(false);
            this.toolStrip_CreatureScriptsCreator.PerformLayout();
            this.tabPage_WaypointsCreator.ResumeLayout(false);
            this.tabPage_WaypointsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_WaypointsCreator_Waypoints)).EndInit();
            this.contextMenuStrip_WaypointsCreator.ResumeLayout(false);
            this.contextMenuStrip_WaypointsCreator_Guids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_WaypointsCreator_Path)).EndInit();
            this.toolStrip_WaypointsCreator.ResumeLayout(false);
            this.toolStrip_WaypointsCreator.PerformLayout();
            this.tabPage_SqlOutput.ResumeLayout(false);
            this.tabPage_SqlOutput.PerformLayout();
            this.tabPage_DatabaseAdvisor.ResumeLayout(false);
            this.tabPage_DatabaseAdvisor.PerformLayout();
            this.contextMenuStrip_DatabaseAdvisor.ResumeLayout(false);
            this.tabPage_DoubleSpawnsRemover.ResumeLayout(false);
            this.tabPage_DoubleSpawnsRemover.PerformLayout();
            this.tabPage_CoreScriptTemplates.ResumeLayout(false);
            this.tabPage_CoreScriptTemplates.PerformLayout();
            this.tabPage_Achievements.ResumeLayout(false);
            this.tabPage_Achievements.PerformLayout();
            this.tabPage_ConditionsCreator.ResumeLayout(false);
            this.tabPage_ConditionsCreator.PerformLayout();
            this.tabPage_ModifierTrees.ResumeLayout(false);
            this.tabPage_ModifierTrees.PerformLayout();
            this.tabPage_ParsedFileAdvisor.ResumeLayout(false);
            this.tabPage_ParsedFileAdvisor.PerformLayout();
            this.toolStrip_ParsedFileAdvisor.ResumeLayout(false);
            this.toolStrip_ParsedFileAdvisor.PerformLayout();
            this.statusStrip_LoadedFile.ResumeLayout(false);
            this.statusStrip_LoadedFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_CreatureScriptsCreator;
        private System.Windows.Forms.ToolStrip toolStrip_CreatureScriptsCreator;
        public System.Windows.Forms.ToolStripButton toolStripButton_CSC_ImportSniff;
        public System.Windows.Forms.ToolStripButton toolStripButton_CreatureScriptsCreator_Search;
        private System.Windows.Forms.TabPage tabPage_SqlOutput;
        private System.Windows.Forms.StatusStrip statusStrip_LoadedFile;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_FileStatus;
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox_CreatureScriptsCreator_CreatureEntry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_CreatureScriptsCreator_CreatureEntry;
        public System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.DataGridView dataGridView_CreatureScriptsCreator_Spells;
        public System.Windows.Forms.ListBox listBox_CreatureScriptCreator_CreatureGuids;
        public System.Windows.Forms.CheckBox checkBox_CreatureScriptsCreator_OnlyCombatSpells;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_CreatureScriptsCreator;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem createSmartAISqlToolStripMenuItem;
        public System.Windows.Forms.TextBox textBox_SqlOutput;
        private System.Windows.Forms.TabPage tabPage_DatabaseAdvisor;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_CreatureFlags;
        private System.Windows.Forms.Label label_DatabaseAdvisor_CreatureFlags;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_QuestFlags;
        private System.Windows.Forms.Label label_DatabaseAdvisor_QuestFlags;
        private System.Windows.Forms.TabPage tabPage_DoubleSpawnsRemover;
        private System.Windows.Forms.Label label_DoubleSpawnsRemover_CreaturesRemoved;
        private System.Windows.Forms.Button button_DoubleSpawnsRemover_ImportFile;
        private System.Windows.Forms.CheckBox checkBox_DoubleSpawnsRemover_Gameobjects;
        private System.Windows.Forms.CheckBox checkBox_DoubleSpawnsRemover_Creatures;
        private System.Windows.Forms.Label label_DoubleSpawnsRemover_GameobjectsRemoved;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpellId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CastTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinCastStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxCastStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinCastRepeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxCastRepeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CastsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceSpell;
        private System.Windows.Forms.TabPage tabPage_WaypointsCreator;
        internal System.Windows.Forms.DataGridView grid_WaypointsCreator_Waypoints;
        public System.Windows.Forms.ListBox listBox_WaypointsCreator_CreatureGuids;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart_WaypointsCreator_Path;
        private System.Windows.Forms.ToolStrip toolStrip_WaypointsCreator;
        public System.Windows.Forms.ToolStripButton toolStripButton_WaypointsCreator_Search;
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox_WaypointsCreator_Entry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_WaypointsCreator_Entry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_WaypointsCreator_Settings;
        public System.Windows.Forms.ToolStripButton toolStripButton_WaypointsCreator_LoadSniff;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WaypointsCreator;
        private System.Windows.Forms.ToolStripMenuItem createSQLToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripMenuItem removeNearestPointsToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_WC;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_PosX;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_PosY;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_PosZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_Orientation;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_WCTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_WCDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn_HasScript;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaypointSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_CSC;
        private System.Windows.Forms.ToolStripMenuItem removeDuplicatePointsToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripMenuItem createReturnPathToolStripMenuItem_WC;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_CurrentAction;
        private System.Windows.Forms.TabPage tabPage_CoreScriptTemplates;
        public System.Windows.Forms.ListBox listBox_CoreScriptTemplates_Hooks;
        public System.Windows.Forms.ComboBox comboBox_CoreScriptTemplates_ScriptType;
        public System.Windows.Forms.TextBox textBox_CoreScriptTemplates_ObjectId;
        private System.Windows.Forms.Label label_CoreScriptTemplates_ScriptType;
        private System.Windows.Forms.Label label_CoreScriptTemplates_ObjectId;
        public System.Windows.Forms.TreeView treeView_CoreScriptTemplates_HookBodies;
        public System.Windows.Forms.TabPage tabPage_Achievements;
        public System.Windows.Forms.TextBox textBox_Achievements_AchievementId;
        public System.Windows.Forms.Label label_Achievements_AchievementId;
        public System.Windows.Forms.TreeView treeView_Achievements_ChildNodes;
        public System.Windows.Forms.Label label_Achievements_AchievementName;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeChildNodes;
        public System.Windows.Forms.Label label_Achievement_CriteriaTreeOperator;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeName;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeId;
        public System.Windows.Forms.Label label_Achievements_AchievementFlags;
        public System.Windows.Forms.Label label_Achievements_AchievementFaction;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeAmount;
        public System.Windows.Forms.TreeView treeView_Achievements_Criterias;
        private System.Windows.Forms.Label label_Achievements_Criterias;
        private System.Windows.Forms.Label label_Achievements_ModifierTrees;
        public System.Windows.Forms.TreeView treeView_Achievements_ModifierTrees;
        private System.Windows.Forms.Label label_Achievements_ModifierTreeChildNodes;
        public System.Windows.Forms.TreeView treeView_Achievements_ModifierTreeChildNodes;
        private System.Windows.Forms.TabPage tabPage_ConditionsCreator;
        public System.Windows.Forms.ComboBox comboBox_ConditionsCreator_ConditionSourceType;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionSourceType;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionType;
        public System.Windows.Forms.ComboBox comboBox_ConditionsCreator_ConditionType;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ElseGroup;
        private System.Windows.Forms.Label label_ConditionsCreator_ElseGroup;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_SourceId;
        private System.Windows.Forms.Label label_ConditionsCreator_SourceId;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_SourceEntry;
        private System.Windows.Forms.Label label_ConditionsCreator_SourceEntry;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_SourceGroup;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionSourceGroup;
        private System.Windows.Forms.Label label_ConditionsCreator_ScriptName;
        private System.Windows.Forms.Label label_ConditionsCreator_NegativeCondition;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionValue3;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionValue2;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionValue1;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ScriptName;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_NegativeCondition;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ConditionValue3;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ConditionValue2;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ConditionValue1;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_ConditionTarget;
        private System.Windows.Forms.Label label_ConditionsCreator_ConditionTarget;
        public System.Windows.Forms.TextBox textBox_ConditionsCreator_Output;
        private System.Windows.Forms.Button button_ConditionsCreator_AddCondition;
        private System.Windows.Forms.Button button_ConditionsCreator_ClearConditions;
        public System.Windows.Forms.TextBox textBox_DatabaseAdvisor_Output;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_GossipMenuText;
        private System.Windows.Forms.Label label_DatabaseAdvisor_GossipMenuText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WaypointsCreator_Guids;
        private System.Windows.Forms.ToolStripMenuItem removeGuidsBeforeSelectedToolStripMenuItem;
        public System.Windows.Forms.CheckBox checkBox_CreatureScriptsCreator_CreateDataFile;
        public System.Windows.Forms.CheckBox checkBox_WaypointsCreator_CreateDataFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_DatabaseAdvisor;
        private System.Windows.Forms.ToolStripMenuItem createReturnPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recalculatePointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRandomMovementsToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_FindDoublePaths;
        private System.Windows.Forms.Label label_DatabaseAdvisor_FindDoublePaths;
        private System.Windows.Forms.ToolStripMenuItem getAddonsFromSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCoreScriptToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage_ModifierTrees;
        public System.Windows.Forms.TextBox textBox_ModifierTrees_ModifierTreeId;
        public System.Windows.Forms.Label label_ModifierTrees_ModifierTreeId;
        private System.Windows.Forms.Label label_ModifierTrees_ModifierTrees;
        public System.Windows.Forms.TreeView treeView_ModifierTrees_ModifierTrees;
        public System.Windows.Forms.Label label_ModifierTrees_Operator;
        public System.Windows.Forms.Label label_ModifierTrees_Type;
        public System.Windows.Forms.Label label_ModifierTrees_Amount;
        public System.Windows.Forms.Label label_ModifierTrees_TertiaryAsset;
        public System.Windows.Forms.Label label_ModifierTrees_SecondaryAsset;
        public System.Windows.Forms.Label label_ModifierTrees_Asset;
        private System.Windows.Forms.ToolStripMenuItem createLegionCombatAISqlToolStripMenuItem;
        public System.Windows.Forms.RichTextBox Core_script_creatore_preview;
        private System.Windows.Forms.TabPage tabPage_ParsedFileAdvisor;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_Output;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_PlayerCastedSpells;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_AreaTriggerSplines;
        private System.Windows.Forms.ToolStrip toolStrip_ParsedFileAdvisor;
        public System.Windows.Forms.ToolStripButton toolStripButton_ParsedFileAdvisor_ImportSniff;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ParsedFileAdvisor_FileStatus;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_PlayerCastedSpells;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_AreaTriggerSplines;
        public System.Windows.Forms.CheckBox checkBox_ParsedFileAdvisor_CreateDataFile;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_SpellDestinations;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_SpellDestinations;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_QuestConversationsOrTexts;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_QuestConversationsOrTexts;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_LosConversationsOrTexts;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_LosConversationsOrTexts;
        private System.Windows.Forms.ToolStripMenuItem recalculateTextForGossipMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPhaseDataForCreatures;
        private System.Windows.Forms.ToolStripMenuItem optimizeCirclePathToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripMenuItem optimizeRegularPathToolStripMenuItem_WC;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_FindPossibleFormations;
        private System.Windows.Forms.Label label_DatabaseAdvisor_FindPossibleFormations;
        private System.Windows.Forms.ToolStripMenuItem reversePointsOrderToolStripMenuItem_WC;
        private System.Windows.Forms.ToolStripMenuItem setSelectedPointAsFirstToolStripMenuItem_WC;
        private System.Windows.Forms.TextBox textBox_DatabaseAdvisor_FindWrongAurasInAddons;
        private System.Windows.Forms.Label label_DatabaseAdvisor_FindWrongAurasInAddons;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_CreatureEquipmentId;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_CreatureEquipmentId;
        public System.Windows.Forms.TextBox textBox_ParsedFileAdvisor_PlayerCompletedQuests;
        private System.Windows.Forms.Label label_ParsedFileAdvisor_PlayerCompletedQuests;
    }
}

