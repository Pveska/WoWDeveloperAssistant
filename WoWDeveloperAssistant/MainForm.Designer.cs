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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_CreatureScriptsCreator = new System.Windows.Forms.TabPage();
            this.checkBox_OnlyCombatSpells = new System.Windows.Forms.CheckBox();
            this.dataGridView_Spells = new System.Windows.Forms.DataGridView();
            this.SpellId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CastTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinCastStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxCastStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinCastRepeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxCastRepeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CastsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceSpell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_CSC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.createSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_CreatureGuids = new System.Windows.Forms.ListBox();
            this.toolStrip_CSC = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_CSC_ImportSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CSC_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_CSC_CreatureEntry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_CSC_CreatureEntry = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator_CSC = new System.Windows.Forms.ToolStripSeparator();
            this.tabPage_WaypointsCreator = new System.Windows.Forms.TabPage();
            this.grid_WC_Waypoints = new System.Windows.Forms.DataGridView();
            this.gridColumn_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_PosZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_Orientation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_WCTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_WCDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColumn_HasScript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaypointSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_WC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNearestPointsToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDuplicatePointsToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.createReturnPathToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_WC = new System.Windows.Forms.ToolStripSeparator();
            this.createSQLToolStripMenuItem_WC = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_WC_CreatureGuids = new System.Windows.Forms.ListBox();
            this.chart_WC = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip_WC = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_WC_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_WC_Entry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_WC_Entry = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_WC_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_WC_LoadSniff = new System.Windows.Forms.ToolStripButton();
            this.tabPage_Output = new System.Windows.Forms.TabPage();
            this.textBox_SQLOutput = new System.Windows.Forms.TextBox();
            this.tabPage_DatabaseAdvisor = new System.Windows.Forms.TabPage();
            this.textBox_SpellDestinations = new System.Windows.Forms.TextBox();
            this.label_SpellDestinations = new System.Windows.Forms.Label();
            this.textBoxAreatriggerSplines = new System.Windows.Forms.TextBox();
            this.label_AreatriggerSplines = new System.Windows.Forms.Label();
            this.textBox_QuestFlags = new System.Windows.Forms.TextBox();
            this.label_QuestFlags = new System.Windows.Forms.Label();
            this.textBox_CreatureFlags = new System.Windows.Forms.TextBox();
            this.label_CreatureFlags = new System.Windows.Forms.Label();
            this.tabPage_DoubleSpawnsRemover = new System.Windows.Forms.TabPage();
            this.label_GameobjectsRemoved = new System.Windows.Forms.Label();
            this.checkBox_GameobjectsRemover = new System.Windows.Forms.CheckBox();
            this.checkBox_CreaturesRemover = new System.Windows.Forms.CheckBox();
            this.label_CreaturesRemoved = new System.Windows.Forms.Label();
            this.button_ImportFileForRemoving = new System.Windows.Forms.Button();
            this.coreScriptTemplates = new System.Windows.Forms.TabPage();
            this.treeView_CoreScriptTemplates_HookBodies = new System.Windows.Forms.TreeView();
            this.label_CoreScriptTemplates_ScriptType = new System.Windows.Forms.Label();
            this.comboBox_CoreScriptTemplates_ScriptType = new System.Windows.Forms.ComboBox();
            this.label_CoreScriptTemplates_Entry = new System.Windows.Forms.Label();
            this.textBox_CoreScriptTemplates_Entry = new System.Windows.Forms.TextBox();
            this.listBox_CoreScriptTemplates_Hooks = new System.Windows.Forms.ListBox();
            this.tabPage_Achievements = new System.Windows.Forms.TabPage();
            this.label_Achievements_ModifierTreeChildNodes = new System.Windows.Forms.Label();
            this.treeView_Achievements_ModifierTreeChildNodes = new System.Windows.Forms.TreeView();
            this.label_Achievements_ModifierTrees = new System.Windows.Forms.Label();
            this.treeView_Achievements_ModifierTrees = new System.Windows.Forms.TreeView();
            this.label_Achievements_Criterias = new System.Windows.Forms.Label();
            this.treeView_Achievements_Criterias = new System.Windows.Forms.TreeView();
            this.label_Achievements_CriteriaTree_Amount = new System.Windows.Forms.Label();
            this.label_Achievements_CreteriaThreeChilds = new System.Windows.Forms.Label();
            this.label_Achievement_CriteriaTree_Operator = new System.Windows.Forms.Label();
            this.label_Achievements_CriteriaTreeName = new System.Windows.Forms.Label();
            this.label_Achievements_CriteriaTreeId = new System.Windows.Forms.Label();
            this.label_Achievements_Flags = new System.Windows.Forms.Label();
            this.label_Achievements_Faction = new System.Windows.Forms.Label();
            this.treeView_Achievements_ChildNodes = new System.Windows.Forms.TreeView();
            this.label_Achievement_Name = new System.Windows.Forms.Label();
            this.textBoxAchievements_Id = new System.Windows.Forms.TextBox();
            this.label_Achievements_Id = new System.Windows.Forms.Label();
            this.tabPage_Conditions_Creator = new System.Windows.Forms.TabPage();
            this.button_ClearConditions = new System.Windows.Forms.Button();
            this.button_AddCondition = new System.Windows.Forms.Button();
            this.textBox_ConditionsOutput = new System.Windows.Forms.TextBox();
            this.label_ScriptName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_ConditionValue3 = new System.Windows.Forms.Label();
            this.labelConditionValue2 = new System.Windows.Forms.Label();
            this.label_ConditionValue1 = new System.Windows.Forms.Label();
            this.textBox_ScriptName = new System.Windows.Forms.TextBox();
            this.textBox_NegativeCondition = new System.Windows.Forms.TextBox();
            this.textBox_ConditionValue3 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionValue2 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionValue1 = new System.Windows.Forms.TextBox();
            this.textBox_ConditionTarget = new System.Windows.Forms.TextBox();
            this.label_ConditionTarget = new System.Windows.Forms.Label();
            this.label_ConditionType = new System.Windows.Forms.Label();
            this.comboBox_ConditionType = new System.Windows.Forms.ComboBox();
            this.textBox_ElseGroup = new System.Windows.Forms.TextBox();
            this.label_ElseGroup = new System.Windows.Forms.Label();
            this.textBox_SourceId = new System.Windows.Forms.TextBox();
            this.label_SourceId = new System.Windows.Forms.Label();
            this.textBox_SourceEntry = new System.Windows.Forms.TextBox();
            this.label_SourceEntry = new System.Windows.Forms.Label();
            this.textBox_SourceGroup = new System.Windows.Forms.TextBox();
            this.label_ConditionSourceGroup = new System.Windows.Forms.Label();
            this.comboBox_ConditionSourceType = new System.Windows.Forms.ComboBox();
            this.label_ConditionSourceType = new System.Windows.Forms.Label();
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_CurrentAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_CreatureScriptsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).BeginInit();
            this.contextMenuStrip_CSC.SuspendLayout();
            this.toolStrip_CSC.SuspendLayout();
            this.tabPage_WaypointsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_WC_Waypoints)).BeginInit();
            this.contextMenuStrip_WC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_WC)).BeginInit();
            this.toolStrip_WC.SuspendLayout();
            this.tabPage_Output.SuspendLayout();
            this.tabPage_DatabaseAdvisor.SuspendLayout();
            this.tabPage_DoubleSpawnsRemover.SuspendLayout();
            this.coreScriptTemplates.SuspendLayout();
            this.tabPage_Achievements.SuspendLayout();
            this.tabPage_Conditions_Creator.SuspendLayout();
            this.statusStrip_LoadedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_CreatureScriptsCreator);
            this.tabControl.Controls.Add(this.tabPage_WaypointsCreator);
            this.tabControl.Controls.Add(this.tabPage_Output);
            this.tabControl.Controls.Add(this.tabPage_DatabaseAdvisor);
            this.tabControl.Controls.Add(this.tabPage_DoubleSpawnsRemover);
            this.tabControl.Controls.Add(this.coreScriptTemplates);
            this.tabControl.Controls.Add(this.tabPage_Achievements);
            this.tabControl.Controls.Add(this.tabPage_Conditions_Creator);
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(2048, 988);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage_CreatureScriptsCreator
            // 
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.checkBox_OnlyCombatSpells);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.dataGridView_Spells);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.listBox_CreatureGuids);
            this.tabPage_CreatureScriptsCreator.Controls.Add(this.toolStrip_CSC);
            this.tabPage_CreatureScriptsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CreatureScriptsCreator.Name = "tabPage_CreatureScriptsCreator";
            this.tabPage_CreatureScriptsCreator.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_CreatureScriptsCreator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_CreatureScriptsCreator.TabIndex = 0;
            this.tabPage_CreatureScriptsCreator.Text = "Creature Scripts Creator";
            this.tabPage_CreatureScriptsCreator.UseVisualStyleBackColor = true;
            // 
            // checkBox_OnlyCombatSpells
            // 
            this.checkBox_OnlyCombatSpells.AutoSize = true;
            this.checkBox_OnlyCombatSpells.BackColor = System.Drawing.Color.LightGray;
            this.checkBox_OnlyCombatSpells.Checked = true;
            this.checkBox_OnlyCombatSpells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OnlyCombatSpells.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_OnlyCombatSpells.Location = new System.Drawing.Point(1450, 8);
            this.checkBox_OnlyCombatSpells.Name = "checkBox_OnlyCombatSpells";
            this.checkBox_OnlyCombatSpells.Size = new System.Drawing.Size(195, 29);
            this.checkBox_OnlyCombatSpells.TabIndex = 4;
            this.checkBox_OnlyCombatSpells.Text = "Only Combat Spells";
            this.checkBox_OnlyCombatSpells.UseVisualStyleBackColor = false;
            // 
            // dataGridView_Spells
            // 
            this.dataGridView_Spells.AllowUserToAddRows = false;
            this.dataGridView_Spells.AllowUserToDeleteRows = false;
            this.dataGridView_Spells.AllowUserToOrderColumns = true;
            this.dataGridView_Spells.AllowUserToResizeColumns = false;
            this.dataGridView_Spells.AllowUserToResizeRows = false;
            this.dataGridView_Spells.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Spells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Spells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SpellId,
            this.SpellName,
            this.CastTime,
            this.MinCastStartTime,
            this.MaxCastStartTime,
            this.MinCastRepeatTime,
            this.MaxCastRepeatTime,
            this.CastsCount,
            this.SourceSpell});
            this.dataGridView_Spells.ContextMenuStrip = this.contextMenuStrip_CSC;
            this.dataGridView_Spells.Enabled = false;
            this.dataGridView_Spells.Location = new System.Drawing.Point(760, 49);
            this.dataGridView_Spells.Name = "dataGridView_Spells";
            this.dataGridView_Spells.RowHeadersWidth = 62;
            this.dataGridView_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Spells.RowTemplate.Height = 28;
            this.dataGridView_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Spells.Size = new System.Drawing.Size(1269, 886);
            this.dataGridView_Spells.TabIndex = 3;
            // 
            // SpellId
            // 
            this.SpellId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellId.DefaultCellStyle = dataGridViewCellStyle9;
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellName.DefaultCellStyle = dataGridViewCellStyle10;
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
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastTime.DefaultCellStyle = dataGridViewCellStyle19;
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
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastStartTime.DefaultCellStyle = dataGridViewCellStyle20;
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
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastStartTime.DefaultCellStyle = dataGridViewCellStyle33;
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
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle34;
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
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle35;
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
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastsCount.DefaultCellStyle = dataGridViewCellStyle36;
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
            // contextMenuStrip_CSC
            // 
            this.contextMenuStrip_CSC.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_CSC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.toolStripSeparator,
            this.createSQLToolStripMenuItem});
            this.contextMenuStrip_CSC.Name = "contextMenuStrip1";
            this.contextMenuStrip_CSC.Size = new System.Drawing.Size(172, 74);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(171, 32);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(168, 6);
            // 
            // createSQLToolStripMenuItem
            // 
            this.createSQLToolStripMenuItem.Name = "createSQLToolStripMenuItem";
            this.createSQLToolStripMenuItem.Size = new System.Drawing.Size(171, 32);
            this.createSQLToolStripMenuItem.Text = "Create SQL";
            this.createSQLToolStripMenuItem.Click += new System.EventHandler(this.createSQLToolStripMenuItem_Click);
            // 
            // listBox_CreatureGuids
            // 
            this.listBox_CreatureGuids.BackColor = System.Drawing.SystemColors.Control;
            this.listBox_CreatureGuids.Enabled = false;
            this.listBox_CreatureGuids.FormattingEnabled = true;
            this.listBox_CreatureGuids.ItemHeight = 20;
            this.listBox_CreatureGuids.Location = new System.Drawing.Point(8, 49);
            this.listBox_CreatureGuids.Name = "listBox_CreatureGuids";
            this.listBox_CreatureGuids.Size = new System.Drawing.Size(726, 884);
            this.listBox_CreatureGuids.TabIndex = 2;
            this.listBox_CreatureGuids.SelectedIndexChanged += new System.EventHandler(this.listBox_CreatureGuids_SelectedIndexChanged);
            // 
            // toolStrip_CSC
            // 
            this.toolStrip_CSC.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip_CSC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_CSC_ImportSniff,
            this.toolStripButton_CSC_Search,
            this.toolStripTextBox_CSC_CreatureEntry,
            this.toolStripLabel_CSC_CreatureEntry,
            this.toolStripSeparator_CSC});
            this.toolStrip_CSC.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_CSC.Name = "toolStrip_CSC";
            this.toolStrip_CSC.Size = new System.Drawing.Size(2034, 34);
            this.toolStrip_CSC.TabIndex = 1;
            this.toolStrip_CSC.Text = "toolStrip_CreatureScriptsCreator";
            // 
            // toolStripButton_CSC_ImportSniff
            // 
            this.toolStripButton_CSC_ImportSniff.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Import;
            this.toolStripButton_CSC_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CSC_ImportSniff.Name = "toolStripButton_CSC_ImportSniff";
            this.toolStripButton_CSC_ImportSniff.Size = new System.Drawing.Size(128, 29);
            this.toolStripButton_CSC_ImportSniff.Text = "Import Sniff";
            this.toolStripButton_CSC_ImportSniff.Click += new System.EventHandler(this.toolStripButton_ImportSniff_Click);
            // 
            // toolStripButton_CSC_Search
            // 
            this.toolStripButton_CSC_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_CSC_Search.Enabled = false;
            this.toolStripButton_CSC_Search.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Search;
            this.toolStripButton_CSC_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CSC_Search.Name = "toolStripButton_CSC_Search";
            this.toolStripButton_CSC_Search.Size = new System.Drawing.Size(84, 29);
            this.toolStripButton_CSC_Search.Text = "Search";
            this.toolStripButton_CSC_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripTextBox_CSC_CreatureEntry
            // 
            this.toolStripTextBox_CSC_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CSC_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CSC_CreatureEntry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_CSC_CreatureEntry.MaxLength = 40;
            this.toolStripTextBox_CSC_CreatureEntry.Name = "toolStripTextBox_CSC_CreatureEntry";
            this.toolStripTextBox_CSC_CreatureEntry.Size = new System.Drawing.Size(100, 34);
            this.toolStripTextBox_CSC_CreatureEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_CSC_CreatureEntrySearch_Enter);
            // 
            // toolStripLabel_CSC_CreatureEntry
            // 
            this.toolStripLabel_CSC_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CSC_CreatureEntry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel_CSC_CreatureEntry.Name = "toolStripLabel_CSC_CreatureEntry";
            this.toolStripLabel_CSC_CreatureEntry.Size = new System.Drawing.Size(184, 29);
            this.toolStripLabel_CSC_CreatureEntry.Text = "Creature EntryOrGuid:";
            // 
            // toolStripSeparator_CSC
            // 
            this.toolStripSeparator_CSC.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator_CSC.Name = "toolStripSeparator_CSC";
            this.toolStripSeparator_CSC.Size = new System.Drawing.Size(6, 34);
            // 
            // tabPage_WaypointsCreator
            // 
            this.tabPage_WaypointsCreator.Controls.Add(this.grid_WC_Waypoints);
            this.tabPage_WaypointsCreator.Controls.Add(this.listBox_WC_CreatureGuids);
            this.tabPage_WaypointsCreator.Controls.Add(this.chart_WC);
            this.tabPage_WaypointsCreator.Controls.Add(this.toolStrip_WC);
            this.tabPage_WaypointsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_WaypointsCreator.Name = "tabPage_WaypointsCreator";
            this.tabPage_WaypointsCreator.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_WaypointsCreator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_WaypointsCreator.TabIndex = 4;
            this.tabPage_WaypointsCreator.Text = "Waypoints Creator";
            this.tabPage_WaypointsCreator.UseVisualStyleBackColor = true;
            // 
            // grid_WC_Waypoints
            // 
            this.grid_WC_Waypoints.AllowUserToAddRows = false;
            this.grid_WC_Waypoints.AllowUserToDeleteRows = false;
            this.grid_WC_Waypoints.AllowUserToResizeColumns = false;
            this.grid_WC_Waypoints.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.NullValue = null;
            this.grid_WC_Waypoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.grid_WC_Waypoints.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.NullValue = null;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_WC_Waypoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.grid_WC_Waypoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_WC_Waypoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridColumn_Id,
            this.gridColumn_PosX,
            this.gridColumn_PosY,
            this.gridColumn_PosZ,
            this.gridColumn_Orientation,
            this.gridColumn_WCTime,
            this.gridColumn_WCDelay,
            this.gridColumn_HasScript,
            this.WaypointSource});
            this.grid_WC_Waypoints.ContextMenuStrip = this.contextMenuStrip_WC;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle31.NullValue = null;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_WC_Waypoints.DefaultCellStyle = dataGridViewCellStyle31;
            this.grid_WC_Waypoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid_WC_Waypoints.Enabled = false;
            this.grid_WC_Waypoints.Location = new System.Drawing.Point(1274, 49);
            this.grid_WC_Waypoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid_WC_Waypoints.Name = "grid_WC_Waypoints";
            this.grid_WC_Waypoints.RowHeadersWidth = 62;
            this.grid_WC_Waypoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_WC_Waypoints.RowsDefaultCellStyle = dataGridViewCellStyle32;
            this.grid_WC_Waypoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_WC_Waypoints.Size = new System.Drawing.Size(758, 886);
            this.grid_WC_Waypoints.TabIndex = 28;
            this.grid_WC_Waypoints.TabStop = false;
            // 
            // gridColumn_Id
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Id.DefaultCellStyle = dataGridViewCellStyle23;
            this.gridColumn_Id.HeaderText = "Id";
            this.gridColumn_Id.MinimumWidth = 8;
            this.gridColumn_Id.Name = "gridColumn_Id";
            this.gridColumn_Id.ReadOnly = true;
            this.gridColumn_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.gridColumn_Id.Width = 35;
            // 
            // gridColumn_PosX
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle24.NullValue = null;
            this.gridColumn_PosX.DefaultCellStyle = dataGridViewCellStyle24;
            this.gridColumn_PosX.HeaderText = "PosX";
            this.gridColumn_PosX.MinimumWidth = 8;
            this.gridColumn_PosX.Name = "gridColumn_PosX";
            this.gridColumn_PosX.ReadOnly = true;
            this.gridColumn_PosX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosX.Width = 60;
            // 
            // gridColumn_PosY
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle25.NullValue = null;
            this.gridColumn_PosY.DefaultCellStyle = dataGridViewCellStyle25;
            this.gridColumn_PosY.HeaderText = "PosY";
            this.gridColumn_PosY.MinimumWidth = 8;
            this.gridColumn_PosY.Name = "gridColumn_PosY";
            this.gridColumn_PosY.ReadOnly = true;
            this.gridColumn_PosY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosY.Width = 60;
            // 
            // gridColumn_PosZ
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_PosZ.DefaultCellStyle = dataGridViewCellStyle26;
            this.gridColumn_PosZ.HeaderText = "PosZ";
            this.gridColumn_PosZ.MinimumWidth = 8;
            this.gridColumn_PosZ.Name = "gridColumn_PosZ";
            this.gridColumn_PosZ.ReadOnly = true;
            this.gridColumn_PosZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosZ.Width = 60;
            // 
            // gridColumn_Orientation
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Orientation.DefaultCellStyle = dataGridViewCellStyle27;
            this.gridColumn_Orientation.HeaderText = "Orientation";
            this.gridColumn_Orientation.MinimumWidth = 8;
            this.gridColumn_Orientation.Name = "gridColumn_Orientation";
            this.gridColumn_Orientation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_Orientation.Width = 65;
            // 
            // gridColumn_WCTime
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCTime.DefaultCellStyle = dataGridViewCellStyle28;
            this.gridColumn_WCTime.HeaderText = "Time";
            this.gridColumn_WCTime.MinimumWidth = 8;
            this.gridColumn_WCTime.Name = "gridColumn_WCTime";
            this.gridColumn_WCTime.ReadOnly = true;
            this.gridColumn_WCTime.Width = 60;
            // 
            // gridColumn_WCDelay
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCDelay.DefaultCellStyle = dataGridViewCellStyle29;
            this.gridColumn_WCDelay.HeaderText = "Delay";
            this.gridColumn_WCDelay.MinimumWidth = 8;
            this.gridColumn_WCDelay.Name = "gridColumn_WCDelay";
            this.gridColumn_WCDelay.Width = 50;
            // 
            // gridColumn_HasScript
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_HasScript.DefaultCellStyle = dataGridViewCellStyle30;
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
            // contextMenuStrip_WC
            // 
            this.contextMenuStrip_WC.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_WC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem_WC,
            this.removeNearestPointsToolStripMenuItem_WC,
            this.removeDuplicatePointsToolStripMenuItem_WC,
            this.createReturnPathToolStripMenuItem_WC,
            this.toolStripSeparator_WC,
            this.createSQLToolStripMenuItem_WC});
            this.contextMenuStrip_WC.Name = "contextMenuStrip_WC";
            this.contextMenuStrip_WC.Size = new System.Drawing.Size(281, 170);
            // 
            // cutToolStripMenuItem_WC
            // 
            this.cutToolStripMenuItem_WC.Name = "cutToolStripMenuItem_WC";
            this.cutToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 32);
            this.cutToolStripMenuItem_WC.Text = "Cut";
            this.cutToolStripMenuItem_WC.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // removeNearestPointsToolStripMenuItem_WC
            // 
            this.removeNearestPointsToolStripMenuItem_WC.Name = "removeNearestPointsToolStripMenuItem_WC";
            this.removeNearestPointsToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 32);
            this.removeNearestPointsToolStripMenuItem_WC.Text = "Remove nearest points";
            this.removeNearestPointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeExcessPointsToolStripMenuItem_Click);
            // 
            // removeDuplicatePointsToolStripMenuItem_WC
            // 
            this.removeDuplicatePointsToolStripMenuItem_WC.Name = "removeDuplicatePointsToolStripMenuItem_WC";
            this.removeDuplicatePointsToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 32);
            this.removeDuplicatePointsToolStripMenuItem_WC.Text = "Remove duplicate points";
            this.removeDuplicatePointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeDuplicatePointsToolStripMenuItem_WC_Click);
            // 
            // createReturnPathToolStripMenuItem_WC
            // 
            this.createReturnPathToolStripMenuItem_WC.Name = "createReturnPathToolStripMenuItem_WC";
            this.createReturnPathToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 32);
            this.createReturnPathToolStripMenuItem_WC.Text = "Create return path";
            this.createReturnPathToolStripMenuItem_WC.Click += new System.EventHandler(this.createReturnPathToolStripMenuItem_WC_Click);
            // 
            // toolStripSeparator_WC
            // 
            this.toolStripSeparator_WC.Name = "toolStripSeparator_WC";
            this.toolStripSeparator_WC.Size = new System.Drawing.Size(277, 6);
            // 
            // createSQLToolStripMenuItem_WC
            // 
            this.createSQLToolStripMenuItem_WC.Name = "createSQLToolStripMenuItem_WC";
            this.createSQLToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 32);
            this.createSQLToolStripMenuItem_WC.Text = "Create SQL";
            this.createSQLToolStripMenuItem_WC.Click += new System.EventHandler(this.createSQLToolStripMenuItem1_Click);
            // 
            // listBox_WC_CreatureGuids
            // 
            this.listBox_WC_CreatureGuids.BackColor = System.Drawing.SystemColors.Control;
            this.listBox_WC_CreatureGuids.Enabled = false;
            this.listBox_WC_CreatureGuids.FormattingEnabled = true;
            this.listBox_WC_CreatureGuids.ItemHeight = 20;
            this.listBox_WC_CreatureGuids.Location = new System.Drawing.Point(906, 49);
            this.listBox_WC_CreatureGuids.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_WC_CreatureGuids.Name = "listBox_WC_CreatureGuids";
            this.listBox_WC_CreatureGuids.Size = new System.Drawing.Size(356, 884);
            this.listBox_WC_CreatureGuids.TabIndex = 27;
            this.listBox_WC_CreatureGuids.SelectedIndexChanged += new System.EventHandler(this.listBox_WCCreatureGuids_SelectedIndexChanged);
            // 
            // chart_WC
            // 
            this.chart_WC.BorderlineWidth = 0;
            this.chart_WC.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart_WC.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart_WC.BorderSkin.BorderWidth = 0;
            chartArea2.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea2.AxisX.IsReversed = true;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea2.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea2.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX2.MajorTickMark.Enabled = false;
            chartArea2.AxisX2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LabelStyle.IsEndLabelVisible = false;
            chartArea2.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea2.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.MajorTickMark.Enabled = false;
            chartArea2.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea2.BorderColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.CursorY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.CursorY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.Name = "ChartArea1";
            this.chart_WC.ChartAreas.Add(chartArea2);
            this.chart_WC.Enabled = false;
            legend2.Enabled = false;
            legend2.ForeColor = System.Drawing.Color.Transparent;
            legend2.HeaderSeparatorColor = System.Drawing.Color.Transparent;
            legend2.ItemColumnSeparatorColor = System.Drawing.Color.Transparent;
            legend2.Name = "Legend1";
            legend2.TitleForeColor = System.Drawing.Color.Transparent;
            legend2.TitleSeparatorColor = System.Drawing.Color.Transparent;
            this.chart_WC.Legends.Add(legend2);
            this.chart_WC.Location = new System.Drawing.Point(4, 49);
            this.chart_WC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_WC.Name = "chart_WC";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Path";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart_WC.Series.Add(series2);
            this.chart_WC.Size = new System.Drawing.Size(894, 888);
            this.chart_WC.TabIndex = 26;
            this.chart_WC.Text = "Waypoints";
            title2.DockedToChartArea = "ChartArea1";
            title2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Path";
            title2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            this.chart_WC.Titles.Add(title2);
            // 
            // toolStrip_WC
            // 
            this.toolStrip_WC.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip_WC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_WC_Search,
            this.toolStripTextBox_WC_Entry,
            this.toolStripLabel_WC_Entry,
            this.toolStripSeparator1,
            this.toolStripButton_WC_Settings,
            this.toolStripButton_WC_LoadSniff});
            this.toolStrip_WC.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_WC.Name = "toolStrip_WC";
            this.toolStrip_WC.Size = new System.Drawing.Size(2034, 34);
            this.toolStrip_WC.TabIndex = 24;
            this.toolStrip_WC.Text = "toolStrip_WC";
            // 
            // toolStripButton_WC_Search
            // 
            this.toolStripButton_WC_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_WC_Search.Enabled = false;
            this.toolStripButton_WC_Search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WC_Search.Image")));
            this.toolStripButton_WC_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WC_Search.Name = "toolStripButton_WC_Search";
            this.toolStripButton_WC_Search.Size = new System.Drawing.Size(84, 29);
            this.toolStripButton_WC_Search.Text = "Search";
            this.toolStripButton_WC_Search.ToolTipText = "Fill listbox with guids of\r\nselected entry or all entries.";
            this.toolStripButton_WC_Search.Click += new System.EventHandler(this.toolStripButton_WCSearch_Click);
            // 
            // toolStripTextBox_WC_Entry
            // 
            this.toolStripTextBox_WC_Entry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_WC_Entry.Enabled = false;
            this.toolStripTextBox_WC_Entry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_WC_Entry.MaxLength = 40;
            this.toolStripTextBox_WC_Entry.Name = "toolStripTextBox_WC_Entry";
            this.toolStripTextBox_WC_Entry.Size = new System.Drawing.Size(103, 34);
            this.toolStripTextBox_WC_Entry.Tag = "";
            this.toolStripTextBox_WC_Entry.ToolTipText = "Input entry of creature or leave\r\nblank to fill listbox will all in sniff.";
            this.toolStripTextBox_WC_Entry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_WCSearch_Enter);
            // 
            // toolStripLabel_WC_Entry
            // 
            this.toolStripLabel_WC_Entry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_WC_Entry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel_WC_Entry.Name = "toolStripLabel_WC_Entry";
            this.toolStripLabel_WC_Entry.Size = new System.Drawing.Size(184, 29);
            this.toolStripLabel_WC_Entry.Text = "Creature EntryOrGuid:";
            this.toolStripLabel_WC_Entry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripButton_WC_Settings
            // 
            this.toolStripButton_WC_Settings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_WC_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WC_Settings.Image")));
            this.toolStripButton_WC_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WC_Settings.Name = "toolStripButton_WC_Settings";
            this.toolStripButton_WC_Settings.Size = new System.Drawing.Size(104, 29);
            this.toolStripButton_WC_Settings.Text = "Settings";
            this.toolStripButton_WC_Settings.ToolTipText = "Setup chart and output SQL.";
            this.toolStripButton_WC_Settings.Click += new System.EventHandler(this.toolStripButton_WCSettings_Click);
            // 
            // toolStripButton_WC_LoadSniff
            // 
            this.toolStripButton_WC_LoadSniff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_WC_LoadSniff.Image")));
            this.toolStripButton_WC_LoadSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_WC_LoadSniff.Name = "toolStripButton_WC_LoadSniff";
            this.toolStripButton_WC_LoadSniff.Size = new System.Drawing.Size(136, 29);
            this.toolStripButton_WC_LoadSniff.Text = "Import Sniff";
            this.toolStripButton_WC_LoadSniff.ToolTipText = "Import a parsed wpp sniff file.";
            this.toolStripButton_WC_LoadSniff.Click += new System.EventHandler(this.toolStripButton_WCLoadSniff_Click);
            // 
            // tabPage_Output
            // 
            this.tabPage_Output.Controls.Add(this.textBox_SQLOutput);
            this.tabPage_Output.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Output.Name = "tabPage_Output";
            this.tabPage_Output.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_Output.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_Output.TabIndex = 1;
            this.tabPage_Output.Text = "SQL Output";
            this.tabPage_Output.UseVisualStyleBackColor = true;
            // 
            // textBox_SQLOutput
            // 
            this.textBox_SQLOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SQLOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBox_SQLOutput.Location = new System.Drawing.Point(3, 3);
            this.textBox_SQLOutput.Multiline = true;
            this.textBox_SQLOutput.Name = "textBox_SQLOutput";
            this.textBox_SQLOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_SQLOutput.Size = new System.Drawing.Size(2034, 949);
            this.textBox_SQLOutput.TabIndex = 0;
            this.textBox_SQLOutput.WordWrap = false;
            // 
            // tabPage_DatabaseAdvisor
            // 
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_SpellDestinations);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_SpellDestinations);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBoxAreatriggerSplines);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_AreatriggerSplines);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DatabaseAdvisor.Name = "tabPage_DatabaseAdvisor";
            this.tabPage_DatabaseAdvisor.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_DatabaseAdvisor.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_DatabaseAdvisor.TabIndex = 2;
            this.tabPage_DatabaseAdvisor.Text = "Database Advisor";
            this.tabPage_DatabaseAdvisor.UseVisualStyleBackColor = true;
            // 
            // textBox_SpellDestinations
            // 
            this.textBox_SpellDestinations.Location = new System.Drawing.Point(8, 198);
            this.textBox_SpellDestinations.Name = "textBox_SpellDestinations";
            this.textBox_SpellDestinations.Size = new System.Drawing.Size(140, 26);
            this.textBox_SpellDestinations.TabIndex = 7;
            this.textBox_SpellDestinations.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_SpellDestinations_KeyUp);
            // 
            // label_SpellDestinations
            // 
            this.label_SpellDestinations.AutoSize = true;
            this.label_SpellDestinations.Location = new System.Drawing.Point(6, 175);
            this.label_SpellDestinations.Name = "label_SpellDestinations";
            this.label_SpellDestinations.Size = new System.Drawing.Size(137, 20);
            this.label_SpellDestinations.TabIndex = 6;
            this.label_SpellDestinations.Text = "Spell Destinations";
            // 
            // textBoxAreatriggerSplines
            // 
            this.textBoxAreatriggerSplines.Location = new System.Drawing.Point(8, 142);
            this.textBoxAreatriggerSplines.Name = "textBoxAreatriggerSplines";
            this.textBoxAreatriggerSplines.Size = new System.Drawing.Size(139, 26);
            this.textBoxAreatriggerSplines.TabIndex = 5;
            this.textBoxAreatriggerSplines.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAreatriggerSplines_KeyDown);
            // 
            // label_AreatriggerSplines
            // 
            this.label_AreatriggerSplines.AutoSize = true;
            this.label_AreatriggerSplines.Location = new System.Drawing.Point(3, 117);
            this.label_AreatriggerSplines.Name = "label_AreatriggerSplines";
            this.label_AreatriggerSplines.Size = new System.Drawing.Size(144, 20);
            this.label_AreatriggerSplines.TabIndex = 4;
            this.label_AreatriggerSplines.Text = "Areatrigger Splines";
            // 
            // textBox_QuestFlags
            // 
            this.textBox_QuestFlags.Location = new System.Drawing.Point(8, 85);
            this.textBox_QuestFlags.Name = "textBox_QuestFlags";
            this.textBox_QuestFlags.Size = new System.Drawing.Size(92, 26);
            this.textBox_QuestFlags.TabIndex = 3;
            this.textBox_QuestFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_QuestFlags_KeyDown);
            // 
            // label_QuestFlags
            // 
            this.label_QuestFlags.AutoSize = true;
            this.label_QuestFlags.Location = new System.Drawing.Point(6, 60);
            this.label_QuestFlags.Name = "label_QuestFlags";
            this.label_QuestFlags.Size = new System.Drawing.Size(95, 20);
            this.label_QuestFlags.TabIndex = 2;
            this.label_QuestFlags.Text = "Quest Flags";
            // 
            // textBox_CreatureFlags
            // 
            this.textBox_CreatureFlags.Location = new System.Drawing.Point(8, 28);
            this.textBox_CreatureFlags.Name = "textBox_CreatureFlags";
            this.textBox_CreatureFlags.Size = new System.Drawing.Size(112, 26);
            this.textBox_CreatureFlags.TabIndex = 1;
            this.textBox_CreatureFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_CreatureFlags_KeyDown);
            // 
            // label_CreatureFlags
            // 
            this.label_CreatureFlags.AutoSize = true;
            this.label_CreatureFlags.Location = new System.Drawing.Point(4, 3);
            this.label_CreatureFlags.Name = "label_CreatureFlags";
            this.label_CreatureFlags.Size = new System.Drawing.Size(114, 20);
            this.label_CreatureFlags.TabIndex = 0;
            this.label_CreatureFlags.Text = "Creature Flags";
            // 
            // tabPage_DoubleSpawnsRemover
            // 
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.label_GameobjectsRemoved);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.checkBox_GameobjectsRemover);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.checkBox_CreaturesRemover);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.label_CreaturesRemoved);
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.button_ImportFileForRemoving);
            this.tabPage_DoubleSpawnsRemover.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DoubleSpawnsRemover.Name = "tabPage_DoubleSpawnsRemover";
            this.tabPage_DoubleSpawnsRemover.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_DoubleSpawnsRemover.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_DoubleSpawnsRemover.TabIndex = 3;
            this.tabPage_DoubleSpawnsRemover.Text = "Double-Spawns Remover";
            this.tabPage_DoubleSpawnsRemover.UseVisualStyleBackColor = true;
            // 
            // label_GameobjectsRemoved
            // 
            this.label_GameobjectsRemoved.AutoSize = true;
            this.label_GameobjectsRemoved.Location = new System.Drawing.Point(494, 132);
            this.label_GameobjectsRemoved.Name = "label_GameobjectsRemoved";
            this.label_GameobjectsRemoved.Size = new System.Drawing.Size(189, 20);
            this.label_GameobjectsRemoved.TabIndex = 4;
            this.label_GameobjectsRemoved.Text = "No gameobjects removed";
            this.label_GameobjectsRemoved.Visible = false;
            // 
            // checkBox_GameobjectsRemover
            // 
            this.checkBox_GameobjectsRemover.AutoSize = true;
            this.checkBox_GameobjectsRemover.Checked = true;
            this.checkBox_GameobjectsRemover.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_GameobjectsRemover.Location = new System.Drawing.Point(802, 5);
            this.checkBox_GameobjectsRemover.Name = "checkBox_GameobjectsRemover";
            this.checkBox_GameobjectsRemover.Size = new System.Drawing.Size(130, 24);
            this.checkBox_GameobjectsRemover.TabIndex = 3;
            this.checkBox_GameobjectsRemover.Text = "Gameobjects";
            this.checkBox_GameobjectsRemover.UseVisualStyleBackColor = true;
            this.checkBox_GameobjectsRemover.CheckedChanged += new System.EventHandler(this.checkBox_GameobjectsRemover_CheckedChanged);
            // 
            // checkBox_CreaturesRemover
            // 
            this.checkBox_CreaturesRemover.AutoSize = true;
            this.checkBox_CreaturesRemover.Checked = true;
            this.checkBox_CreaturesRemover.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_CreaturesRemover.Location = new System.Drawing.Point(700, 5);
            this.checkBox_CreaturesRemover.Name = "checkBox_CreaturesRemover";
            this.checkBox_CreaturesRemover.Size = new System.Drawing.Size(105, 24);
            this.checkBox_CreaturesRemover.TabIndex = 2;
            this.checkBox_CreaturesRemover.Text = "Creatures";
            this.checkBox_CreaturesRemover.UseVisualStyleBackColor = true;
            this.checkBox_CreaturesRemover.CheckedChanged += new System.EventHandler(this.checkBox_CreaturesRemover_CheckedChanged);
            // 
            // label_CreaturesRemoved
            // 
            this.label_CreaturesRemoved.AutoSize = true;
            this.label_CreaturesRemoved.Location = new System.Drawing.Point(494, 112);
            this.label_CreaturesRemoved.Name = "label_CreaturesRemoved";
            this.label_CreaturesRemoved.Size = new System.Drawing.Size(165, 20);
            this.label_CreaturesRemoved.TabIndex = 1;
            this.label_CreaturesRemoved.Text = "No creatures removed";
            this.label_CreaturesRemoved.Visible = false;
            // 
            // button_ImportFileForRemoving
            // 
            this.button_ImportFileForRemoving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ImportFileForRemoving.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_ImportFileForRemoving.FlatAppearance.BorderSize = 5;
            this.button_ImportFileForRemoving.Font = new System.Drawing.Font("Sitka Small", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ImportFileForRemoving.Location = new System.Drawing.Point(700, 29);
            this.button_ImportFileForRemoving.Name = "button_ImportFileForRemoving";
            this.button_ImportFileForRemoving.Size = new System.Drawing.Size(220, 42);
            this.button_ImportFileForRemoving.TabIndex = 0;
            this.button_ImportFileForRemoving.Text = "Import File";
            this.button_ImportFileForRemoving.UseVisualStyleBackColor = true;
            this.button_ImportFileForRemoving.Click += new System.EventHandler(this.button_ImportFile_Click);
            // 
            // coreScriptTemplates
            // 
            this.coreScriptTemplates.Controls.Add(this.treeView_CoreScriptTemplates_HookBodies);
            this.coreScriptTemplates.Controls.Add(this.label_CoreScriptTemplates_ScriptType);
            this.coreScriptTemplates.Controls.Add(this.comboBox_CoreScriptTemplates_ScriptType);
            this.coreScriptTemplates.Controls.Add(this.label_CoreScriptTemplates_Entry);
            this.coreScriptTemplates.Controls.Add(this.textBox_CoreScriptTemplates_Entry);
            this.coreScriptTemplates.Controls.Add(this.listBox_CoreScriptTemplates_Hooks);
            this.coreScriptTemplates.Location = new System.Drawing.Point(4, 29);
            this.coreScriptTemplates.Name = "coreScriptTemplates";
            this.coreScriptTemplates.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.coreScriptTemplates.Size = new System.Drawing.Size(2040, 955);
            this.coreScriptTemplates.TabIndex = 5;
            this.coreScriptTemplates.Text = "Core Script Templates";
            this.coreScriptTemplates.UseVisualStyleBackColor = true;
            // 
            // treeView_CoreScriptTemplates_HookBodies
            // 
            this.treeView_CoreScriptTemplates_HookBodies.CheckBoxes = true;
            this.treeView_CoreScriptTemplates_HookBodies.Location = new System.Drawing.Point(242, 49);
            this.treeView_CoreScriptTemplates_HookBodies.Name = "treeView_CoreScriptTemplates_HookBodies";
            this.treeView_CoreScriptTemplates_HookBodies.Size = new System.Drawing.Size(250, 884);
            this.treeView_CoreScriptTemplates_HookBodies.TabIndex = 6;
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
            this.comboBox_CoreScriptTemplates_ScriptType.Name = "comboBox_CoreScriptTemplates_ScriptType";
            this.comboBox_CoreScriptTemplates_ScriptType.Size = new System.Drawing.Size(121, 28);
            this.comboBox_CoreScriptTemplates_ScriptType.TabIndex = 1;
            this.comboBox_CoreScriptTemplates_ScriptType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // label_CoreScriptTemplates_Entry
            // 
            this.label_CoreScriptTemplates_Entry.AutoSize = true;
            this.label_CoreScriptTemplates_Entry.Location = new System.Drawing.Point(262, 12);
            this.label_CoreScriptTemplates_Entry.Name = "label_CoreScriptTemplates_Entry";
            this.label_CoreScriptTemplates_Entry.Size = new System.Drawing.Size(120, 20);
            this.label_CoreScriptTemplates_Entry.TabIndex = 4;
            this.label_CoreScriptTemplates_Entry.Text = "Enter Object Id:";
            // 
            // textBox_CoreScriptTemplates_Entry
            // 
            this.textBox_CoreScriptTemplates_Entry.Enabled = false;
            this.textBox_CoreScriptTemplates_Entry.Location = new System.Drawing.Point(388, 9);
            this.textBox_CoreScriptTemplates_Entry.MaxLength = 6;
            this.textBox_CoreScriptTemplates_Entry.Name = "textBox_CoreScriptTemplates_Entry";
            this.textBox_CoreScriptTemplates_Entry.Size = new System.Drawing.Size(70, 26);
            this.textBox_CoreScriptTemplates_Entry.TabIndex = 2;
            this.textBox_CoreScriptTemplates_Entry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_CoreScriptTemplates_Enter);
            // 
            // listBox_CoreScriptTemplates_Hooks
            // 
            this.listBox_CoreScriptTemplates_Hooks.ItemHeight = 20;
            this.listBox_CoreScriptTemplates_Hooks.Location = new System.Drawing.Point(8, 49);
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
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTree_Amount);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CreteriaThreeChilds);
            this.tabPage_Achievements.Controls.Add(this.label_Achievement_CriteriaTree_Operator);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeName);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_CriteriaTreeId);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_Flags);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_Faction);
            this.tabPage_Achievements.Controls.Add(this.treeView_Achievements_ChildNodes);
            this.tabPage_Achievements.Controls.Add(this.label_Achievement_Name);
            this.tabPage_Achievements.Controls.Add(this.textBoxAchievements_Id);
            this.tabPage_Achievements.Controls.Add(this.label_Achievements_Id);
            this.tabPage_Achievements.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Achievements.Name = "tabPage_Achievements";
            this.tabPage_Achievements.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
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
            this.treeView_Achievements_Criterias.Name = "treeView_Achievements_Criterias";
            this.treeView_Achievements_Criterias.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_Criterias.TabIndex = 11;
            this.treeView_Achievements_Criterias.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_Criterias_AfterCollapse);
            this.treeView_Achievements_Criterias.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_Criterias_AfterExpand);
            // 
            // label_Achievements_CriteriaTree_Amount
            // 
            this.label_Achievements_CriteriaTree_Amount.AutoSize = true;
            this.label_Achievements_CriteriaTree_Amount.Location = new System.Drawing.Point(6, 169);
            this.label_Achievements_CriteriaTree_Amount.Name = "label_Achievements_CriteriaTree_Amount";
            this.label_Achievements_CriteriaTree_Amount.Size = new System.Drawing.Size(159, 20);
            this.label_Achievements_CriteriaTree_Amount.TabIndex = 10;
            this.label_Achievements_CriteriaTree_Amount.Text = "CriteriaTree Amount: ";
            // 
            // label_Achievements_CreteriaThreeChilds
            // 
            this.label_Achievements_CreteriaThreeChilds.AutoSize = true;
            this.label_Achievements_CreteriaThreeChilds.Location = new System.Drawing.Point(160, 228);
            this.label_Achievements_CreteriaThreeChilds.Name = "label_Achievements_CreteriaThreeChilds";
            this.label_Achievements_CreteriaThreeChilds.Size = new System.Drawing.Size(188, 20);
            this.label_Achievements_CreteriaThreeChilds.TabIndex = 9;
            this.label_Achievements_CreteriaThreeChilds.Text = "Criteria Tree Child Nodes:";
            // 
            // label_Achievement_CriteriaTree_Operator
            // 
            this.label_Achievement_CriteriaTree_Operator.AutoSize = true;
            this.label_Achievement_CriteriaTree_Operator.Location = new System.Drawing.Point(6, 195);
            this.label_Achievement_CriteriaTree_Operator.Name = "label_Achievement_CriteriaTree_Operator";
            this.label_Achievement_CriteriaTree_Operator.Size = new System.Drawing.Size(166, 20);
            this.label_Achievement_CriteriaTree_Operator.TabIndex = 8;
            this.label_Achievement_CriteriaTree_Operator.Text = "CriteriaTree Operator: ";
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
            // label_Achievements_Flags
            // 
            this.label_Achievements_Flags.AutoSize = true;
            this.label_Achievements_Flags.Location = new System.Drawing.Point(6, 85);
            this.label_Achievements_Flags.Name = "label_Achievements_Flags";
            this.label_Achievements_Flags.Size = new System.Drawing.Size(152, 20);
            this.label_Achievements_Flags.TabIndex = 5;
            this.label_Achievements_Flags.Text = "Achievement Flags: ";
            // 
            // label_Achievements_Faction
            // 
            this.label_Achievements_Faction.AutoSize = true;
            this.label_Achievements_Faction.Location = new System.Drawing.Point(4, 60);
            this.label_Achievements_Faction.Name = "label_Achievements_Faction";
            this.label_Achievements_Faction.Size = new System.Drawing.Size(166, 20);
            this.label_Achievements_Faction.TabIndex = 4;
            this.label_Achievements_Faction.Text = "Achievement Faction: ";
            // 
            // treeView_Achievements_ChildNodes
            // 
            this.treeView_Achievements_ChildNodes.Location = new System.Drawing.Point(4, 251);
            this.treeView_Achievements_ChildNodes.Name = "treeView_Achievements_ChildNodes";
            this.treeView_Achievements_ChildNodes.Size = new System.Drawing.Size(500, 679);
            this.treeView_Achievements_ChildNodes.TabIndex = 3;
            this.treeView_Achievements_ChildNodes.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ChildNodes_AfterCollapse);
            this.treeView_Achievements_ChildNodes.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Achievements_ChildNodes_AfterExpand);
            // 
            // label_Achievement_Name
            // 
            this.label_Achievement_Name.AutoSize = true;
            this.label_Achievement_Name.Location = new System.Drawing.Point(4, 35);
            this.label_Achievement_Name.Name = "label_Achievement_Name";
            this.label_Achievement_Name.Size = new System.Drawing.Size(155, 20);
            this.label_Achievement_Name.TabIndex = 2;
            this.label_Achievement_Name.Text = "Achievement Name: ";
            // 
            // textBoxAchievements_Id
            // 
            this.textBoxAchievements_Id.Location = new System.Drawing.Point(130, 2);
            this.textBoxAchievements_Id.Name = "textBoxAchievements_Id";
            this.textBoxAchievements_Id.Size = new System.Drawing.Size(100, 26);
            this.textBoxAchievements_Id.TabIndex = 1;
            this.textBoxAchievements_Id.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxAchievements_Id_KeyUp);
            // 
            // label_Achievements_Id
            // 
            this.label_Achievements_Id.AutoSize = true;
            this.label_Achievements_Id.Location = new System.Drawing.Point(4, 8);
            this.label_Achievements_Id.Name = "label_Achievements_Id";
            this.label_Achievements_Id.Size = new System.Drawing.Size(123, 20);
            this.label_Achievements_Id.TabIndex = 0;
            this.label_Achievements_Id.Text = "Achievement Id:";
            // 
            // tabPage_Conditions_Creator
            // 
            this.tabPage_Conditions_Creator.Controls.Add(this.button_ClearConditions);
            this.tabPage_Conditions_Creator.Controls.Add(this.button_AddCondition);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ConditionsOutput);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ScriptName);
            this.tabPage_Conditions_Creator.Controls.Add(this.label3);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionValue3);
            this.tabPage_Conditions_Creator.Controls.Add(this.labelConditionValue2);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionValue1);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ScriptName);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_NegativeCondition);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ConditionValue3);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ConditionValue2);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ConditionValue1);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ConditionTarget);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionTarget);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionType);
            this.tabPage_Conditions_Creator.Controls.Add(this.comboBox_ConditionType);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_ElseGroup);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ElseGroup);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_SourceId);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_SourceId);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_SourceEntry);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_SourceEntry);
            this.tabPage_Conditions_Creator.Controls.Add(this.textBox_SourceGroup);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionSourceGroup);
            this.tabPage_Conditions_Creator.Controls.Add(this.comboBox_ConditionSourceType);
            this.tabPage_Conditions_Creator.Controls.Add(this.label_ConditionSourceType);
            this.tabPage_Conditions_Creator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Conditions_Creator.Name = "tabPage_Conditions_Creator";
            this.tabPage_Conditions_Creator.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage_Conditions_Creator.Size = new System.Drawing.Size(2040, 955);
            this.tabPage_Conditions_Creator.TabIndex = 7;
            this.tabPage_Conditions_Creator.Text = "Conditions Creator";
            this.tabPage_Conditions_Creator.UseVisualStyleBackColor = true;
            // 
            // button_ClearConditions
            // 
            this.button_ClearConditions.Enabled = false;
            this.button_ClearConditions.Location = new System.Drawing.Point(344, 409);
            this.button_ClearConditions.Name = "button_ClearConditions";
            this.button_ClearConditions.Size = new System.Drawing.Size(138, 31);
            this.button_ClearConditions.TabIndex = 26;
            this.button_ClearConditions.Text = "Clear Conditions";
            this.button_ClearConditions.UseVisualStyleBackColor = true;
            this.button_ClearConditions.Click += new System.EventHandler(this.button_ClearConditions_Click);
            // 
            // button_AddCondition
            // 
            this.button_AddCondition.Enabled = false;
            this.button_AddCondition.Location = new System.Drawing.Point(12, 409);
            this.button_AddCondition.Name = "button_AddCondition";
            this.button_AddCondition.Size = new System.Drawing.Size(126, 31);
            this.button_AddCondition.TabIndex = 25;
            this.button_AddCondition.Text = "Add Condition";
            this.button_AddCondition.UseVisualStyleBackColor = true;
            this.button_AddCondition.Click += new System.EventHandler(this.button_AddCondition_Click);
            // 
            // textBox_ConditionsOutput
            // 
            this.textBox_ConditionsOutput.Enabled = false;
            this.textBox_ConditionsOutput.Location = new System.Drawing.Point(976, 6);
            this.textBox_ConditionsOutput.Multiline = true;
            this.textBox_ConditionsOutput.Name = "textBox_ConditionsOutput";
            this.textBox_ConditionsOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ConditionsOutput.Size = new System.Drawing.Size(1057, 936);
            this.textBox_ConditionsOutput.TabIndex = 24;
            // 
            // label_ScriptName
            // 
            this.label_ScriptName.AutoSize = true;
            this.label_ScriptName.Location = new System.Drawing.Point(8, 366);
            this.label_ScriptName.Name = "label_ScriptName";
            this.label_ScriptName.Size = new System.Drawing.Size(96, 20);
            this.label_ScriptName.TabIndex = 23;
            this.label_ScriptName.Text = "ScriptName:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Negative Condition:";
            // 
            // label_ConditionValue3
            // 
            this.label_ConditionValue3.AutoSize = true;
            this.label_ConditionValue3.Location = new System.Drawing.Point(8, 302);
            this.label_ConditionValue3.Name = "label_ConditionValue3";
            this.label_ConditionValue3.Size = new System.Drawing.Size(138, 20);
            this.label_ConditionValue3.TabIndex = 21;
            this.label_ConditionValue3.Text = "Condition Value 3:";
            // 
            // labelConditionValue2
            // 
            this.labelConditionValue2.AutoSize = true;
            this.labelConditionValue2.Location = new System.Drawing.Point(8, 269);
            this.labelConditionValue2.Name = "labelConditionValue2";
            this.labelConditionValue2.Size = new System.Drawing.Size(138, 20);
            this.labelConditionValue2.TabIndex = 20;
            this.labelConditionValue2.Text = "Condition Value 2:";
            // 
            // label_ConditionValue1
            // 
            this.label_ConditionValue1.AutoSize = true;
            this.label_ConditionValue1.Location = new System.Drawing.Point(8, 238);
            this.label_ConditionValue1.Name = "label_ConditionValue1";
            this.label_ConditionValue1.Size = new System.Drawing.Size(138, 20);
            this.label_ConditionValue1.TabIndex = 19;
            this.label_ConditionValue1.Text = "Condition Value 1:";
            // 
            // textBox_ScriptName
            // 
            this.textBox_ScriptName.Enabled = false;
            this.textBox_ScriptName.Location = new System.Drawing.Point(160, 363);
            this.textBox_ScriptName.MaxLength = 50;
            this.textBox_ScriptName.Name = "textBox_ScriptName";
            this.textBox_ScriptName.Size = new System.Drawing.Size(320, 26);
            this.textBox_ScriptName.TabIndex = 18;
            // 
            // textBox_NegativeCondition
            // 
            this.textBox_NegativeCondition.Enabled = false;
            this.textBox_NegativeCondition.Location = new System.Drawing.Point(160, 331);
            this.textBox_NegativeCondition.MaxLength = 1;
            this.textBox_NegativeCondition.Name = "textBox_NegativeCondition";
            this.textBox_NegativeCondition.Size = new System.Drawing.Size(30, 26);
            this.textBox_NegativeCondition.TabIndex = 17;
            this.textBox_NegativeCondition.Text = "0";
            this.textBox_NegativeCondition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_ConditionValue3
            // 
            this.textBox_ConditionValue3.Enabled = false;
            this.textBox_ConditionValue3.Location = new System.Drawing.Point(160, 298);
            this.textBox_ConditionValue3.MaxLength = 6;
            this.textBox_ConditionValue3.Name = "textBox_ConditionValue3";
            this.textBox_ConditionValue3.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionValue3.TabIndex = 16;
            // 
            // textBox_ConditionValue2
            // 
            this.textBox_ConditionValue2.Enabled = false;
            this.textBox_ConditionValue2.Location = new System.Drawing.Point(160, 268);
            this.textBox_ConditionValue2.MaxLength = 6;
            this.textBox_ConditionValue2.Name = "textBox_ConditionValue2";
            this.textBox_ConditionValue2.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionValue2.TabIndex = 15;
            // 
            // textBox_ConditionValue1
            // 
            this.textBox_ConditionValue1.Enabled = false;
            this.textBox_ConditionValue1.Location = new System.Drawing.Point(160, 235);
            this.textBox_ConditionValue1.MaxLength = 6;
            this.textBox_ConditionValue1.Name = "textBox_ConditionValue1";
            this.textBox_ConditionValue1.Size = new System.Drawing.Size(100, 26);
            this.textBox_ConditionValue1.TabIndex = 14;
            // 
            // textBox_ConditionTarget
            // 
            this.textBox_ConditionTarget.Enabled = false;
            this.textBox_ConditionTarget.Location = new System.Drawing.Point(160, 203);
            this.textBox_ConditionTarget.MaxLength = 1;
            this.textBox_ConditionTarget.Name = "textBox_ConditionTarget";
            this.textBox_ConditionTarget.Size = new System.Drawing.Size(30, 26);
            this.textBox_ConditionTarget.TabIndex = 13;
            this.textBox_ConditionTarget.Text = "0";
            this.textBox_ConditionTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ConditionTarget
            // 
            this.label_ConditionTarget.AutoSize = true;
            this.label_ConditionTarget.Location = new System.Drawing.Point(8, 206);
            this.label_ConditionTarget.Name = "label_ConditionTarget";
            this.label_ConditionTarget.Size = new System.Drawing.Size(130, 20);
            this.label_ConditionTarget.TabIndex = 12;
            this.label_ConditionTarget.Text = "Condition Target:";
            // 
            // label_ConditionType
            // 
            this.label_ConditionType.AutoSize = true;
            this.label_ConditionType.Location = new System.Drawing.Point(8, 172);
            this.label_ConditionType.Name = "label_ConditionType";
            this.label_ConditionType.Size = new System.Drawing.Size(118, 20);
            this.label_ConditionType.TabIndex = 11;
            this.label_ConditionType.Text = "Condition Type:";
            // 
            // comboBox_ConditionType
            // 
            this.comboBox_ConditionType.Enabled = false;
            this.comboBox_ConditionType.FormattingEnabled = true;
            this.comboBox_ConditionType.Location = new System.Drawing.Point(160, 169);
            this.comboBox_ConditionType.Name = "comboBox_ConditionType";
            this.comboBox_ConditionType.Size = new System.Drawing.Size(320, 28);
            this.comboBox_ConditionType.TabIndex = 10;
            this.comboBox_ConditionType.DropDown += new System.EventHandler(this.comboBox_ConditionType_DropDown);
            this.comboBox_ConditionType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ConditionType_SelectedIndexChanged);
            // 
            // textBox_ElseGroup
            // 
            this.textBox_ElseGroup.Enabled = false;
            this.textBox_ElseGroup.Location = new System.Drawing.Point(160, 138);
            this.textBox_ElseGroup.MaxLength = 2;
            this.textBox_ElseGroup.Name = "textBox_ElseGroup";
            this.textBox_ElseGroup.Size = new System.Drawing.Size(50, 26);
            this.textBox_ElseGroup.TabIndex = 9;
            this.textBox_ElseGroup.Text = "0";
            this.textBox_ElseGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ElseGroup
            // 
            this.label_ElseGroup.AutoSize = true;
            this.label_ElseGroup.Location = new System.Drawing.Point(8, 142);
            this.label_ElseGroup.Name = "label_ElseGroup";
            this.label_ElseGroup.Size = new System.Drawing.Size(93, 20);
            this.label_ElseGroup.TabIndex = 8;
            this.label_ElseGroup.Text = "Else Group:";
            // 
            // textBox_SourceId
            // 
            this.textBox_SourceId.Enabled = false;
            this.textBox_SourceId.Location = new System.Drawing.Point(160, 106);
            this.textBox_SourceId.MaxLength = 1;
            this.textBox_SourceId.Name = "textBox_SourceId";
            this.textBox_SourceId.Size = new System.Drawing.Size(30, 26);
            this.textBox_SourceId.TabIndex = 7;
            this.textBox_SourceId.Text = "0";
            this.textBox_SourceId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_SourceId
            // 
            this.label_SourceId.AutoSize = true;
            this.label_SourceId.Location = new System.Drawing.Point(8, 109);
            this.label_SourceId.Name = "label_SourceId";
            this.label_SourceId.Size = new System.Drawing.Size(82, 20);
            this.label_SourceId.TabIndex = 6;
            this.label_SourceId.Text = "Source Id:";
            // 
            // textBox_SourceEntry
            // 
            this.textBox_SourceEntry.Enabled = false;
            this.textBox_SourceEntry.Location = new System.Drawing.Point(160, 74);
            this.textBox_SourceEntry.MaxLength = 6;
            this.textBox_SourceEntry.Name = "textBox_SourceEntry";
            this.textBox_SourceEntry.Size = new System.Drawing.Size(100, 26);
            this.textBox_SourceEntry.TabIndex = 5;
            // 
            // label_SourceEntry
            // 
            this.label_SourceEntry.AutoSize = true;
            this.label_SourceEntry.Location = new System.Drawing.Point(8, 77);
            this.label_SourceEntry.Name = "label_SourceEntry";
            this.label_SourceEntry.Size = new System.Drawing.Size(105, 20);
            this.label_SourceEntry.TabIndex = 4;
            this.label_SourceEntry.Text = "Source Entry:";
            // 
            // textBox_SourceGroup
            // 
            this.textBox_SourceGroup.Enabled = false;
            this.textBox_SourceGroup.Location = new System.Drawing.Point(160, 42);
            this.textBox_SourceGroup.MaxLength = 6;
            this.textBox_SourceGroup.Name = "textBox_SourceGroup";
            this.textBox_SourceGroup.Size = new System.Drawing.Size(100, 26);
            this.textBox_SourceGroup.TabIndex = 3;
            // 
            // label_ConditionSourceGroup
            // 
            this.label_ConditionSourceGroup.AutoSize = true;
            this.label_ConditionSourceGroup.Location = new System.Drawing.Point(8, 45);
            this.label_ConditionSourceGroup.Name = "label_ConditionSourceGroup";
            this.label_ConditionSourceGroup.Size = new System.Drawing.Size(113, 20);
            this.label_ConditionSourceGroup.TabIndex = 2;
            this.label_ConditionSourceGroup.Text = "Source Group:";
            // 
            // comboBox_ConditionSourceType
            // 
            this.comboBox_ConditionSourceType.FormattingEnabled = true;
            this.comboBox_ConditionSourceType.Location = new System.Drawing.Point(160, 6);
            this.comboBox_ConditionSourceType.Name = "comboBox_ConditionSourceType";
            this.comboBox_ConditionSourceType.Size = new System.Drawing.Size(320, 28);
            this.comboBox_ConditionSourceType.TabIndex = 1;
            this.comboBox_ConditionSourceType.DropDown += new System.EventHandler(this.comboBox_ConditionSourceType_DropDown);
            this.comboBox_ConditionSourceType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ConditionSourceType_SelectedIndexChanged);
            // 
            // label_ConditionSourceType
            // 
            this.label_ConditionSourceType.AutoSize = true;
            this.label_ConditionSourceType.Location = new System.Drawing.Point(8, 9);
            this.label_ConditionSourceType.Name = "label_ConditionSourceType";
            this.label_ConditionSourceType.Size = new System.Drawing.Size(102, 20);
            this.label_ConditionSourceType.TabIndex = 0;
            this.label_ConditionSourceType.Text = "Source Type:";
            // 
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.BackColor = System.Drawing.Color.LightGray;
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus,
            this.toolStripStatusLabel_CurrentAction});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 977);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(2050, 32);
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
            this.openFileDialog.FileName = "openFileDialog_WSC";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2050, 1009);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Wow Developer Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage_CreatureScriptsCreator.ResumeLayout(false);
            this.tabPage_CreatureScriptsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).EndInit();
            this.contextMenuStrip_CSC.ResumeLayout(false);
            this.toolStrip_CSC.ResumeLayout(false);
            this.toolStrip_CSC.PerformLayout();
            this.tabPage_WaypointsCreator.ResumeLayout(false);
            this.tabPage_WaypointsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_WC_Waypoints)).EndInit();
            this.contextMenuStrip_WC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_WC)).EndInit();
            this.toolStrip_WC.ResumeLayout(false);
            this.toolStrip_WC.PerformLayout();
            this.tabPage_Output.ResumeLayout(false);
            this.tabPage_Output.PerformLayout();
            this.tabPage_DatabaseAdvisor.ResumeLayout(false);
            this.tabPage_DatabaseAdvisor.PerformLayout();
            this.tabPage_DoubleSpawnsRemover.ResumeLayout(false);
            this.tabPage_DoubleSpawnsRemover.PerformLayout();
            this.coreScriptTemplates.ResumeLayout(false);
            this.coreScriptTemplates.PerformLayout();
            this.tabPage_Achievements.ResumeLayout(false);
            this.tabPage_Achievements.PerformLayout();
            this.tabPage_Conditions_Creator.ResumeLayout(false);
            this.tabPage_Conditions_Creator.PerformLayout();
            this.statusStrip_LoadedFile.ResumeLayout(false);
            this.statusStrip_LoadedFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_CreatureScriptsCreator;
        private System.Windows.Forms.ToolStrip toolStrip_CSC;
        public System.Windows.Forms.ToolStripButton toolStripButton_CSC_ImportSniff;
        public System.Windows.Forms.ToolStripButton toolStripButton_CSC_Search;
        private System.Windows.Forms.TabPage tabPage_Output;
        private System.Windows.Forms.StatusStrip statusStrip_LoadedFile;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_FileStatus;
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox_CSC_CreatureEntry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_CSC_CreatureEntry;
        public System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.DataGridView dataGridView_Spells;
        public System.Windows.Forms.ListBox listBox_CreatureGuids;
        public System.Windows.Forms.CheckBox checkBox_OnlyCombatSpells;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_CSC;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem createSQLToolStripMenuItem;
        public System.Windows.Forms.TextBox textBox_SQLOutput;
        private System.Windows.Forms.TabPage tabPage_DatabaseAdvisor;
        private System.Windows.Forms.TextBox textBox_CreatureFlags;
        private System.Windows.Forms.Label label_CreatureFlags;
        private System.Windows.Forms.TextBox textBox_QuestFlags;
        private System.Windows.Forms.Label label_QuestFlags;
        private System.Windows.Forms.TabPage tabPage_DoubleSpawnsRemover;
        private System.Windows.Forms.Label label_CreaturesRemoved;
        private System.Windows.Forms.Button button_ImportFileForRemoving;
        private System.Windows.Forms.CheckBox checkBox_GameobjectsRemover;
        private System.Windows.Forms.CheckBox checkBox_CreaturesRemover;
        private System.Windows.Forms.Label label_GameobjectsRemoved;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpellId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CastTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinCastStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxCastStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinCastRepeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxCastRepeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CastsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceSpell;
        private System.Windows.Forms.TextBox textBoxAreatriggerSplines;
        private System.Windows.Forms.Label label_AreatriggerSplines;
        private System.Windows.Forms.TabPage tabPage_WaypointsCreator;
        internal System.Windows.Forms.DataGridView grid_WC_Waypoints;
        public System.Windows.Forms.ListBox listBox_WC_CreatureGuids;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart_WC;
        private System.Windows.Forms.ToolStrip toolStrip_WC;
        public System.Windows.Forms.ToolStripButton toolStripButton_WC_Search;
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox_WC_Entry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_WC_Entry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_WC_Settings;
        public System.Windows.Forms.ToolStripButton toolStripButton_WC_LoadSniff;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WC;
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
        private System.Windows.Forms.TabPage coreScriptTemplates;
        public System.Windows.Forms.ListBox listBox_CoreScriptTemplates_Hooks;
        public System.Windows.Forms.ComboBox comboBox_CoreScriptTemplates_ScriptType;
        public System.Windows.Forms.TextBox textBox_CoreScriptTemplates_Entry;
        private System.Windows.Forms.Label label_CoreScriptTemplates_ScriptType;
        private System.Windows.Forms.Label label_CoreScriptTemplates_Entry;
        public System.Windows.Forms.TreeView treeView_CoreScriptTemplates_HookBodies;
        public System.Windows.Forms.TabPage tabPage_Achievements;
        public System.Windows.Forms.TextBox textBoxAchievements_Id;
        public System.Windows.Forms.Label label_Achievements_Id;
        public System.Windows.Forms.TreeView treeView_Achievements_ChildNodes;
        public System.Windows.Forms.Label label_Achievement_Name;
        public System.Windows.Forms.Label label_Achievements_CreteriaThreeChilds;
        public System.Windows.Forms.Label label_Achievement_CriteriaTree_Operator;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeName;
        public System.Windows.Forms.Label label_Achievements_CriteriaTreeId;
        public System.Windows.Forms.Label label_Achievements_Flags;
        public System.Windows.Forms.Label label_Achievements_Faction;
        public System.Windows.Forms.Label label_Achievements_CriteriaTree_Amount;
        public System.Windows.Forms.TreeView treeView_Achievements_Criterias;
        private System.Windows.Forms.Label label_Achievements_Criterias;
        private System.Windows.Forms.Label label_Achievements_ModifierTrees;
        public System.Windows.Forms.TreeView treeView_Achievements_ModifierTrees;
        private System.Windows.Forms.Label label_Achievements_ModifierTreeChildNodes;
        public System.Windows.Forms.TreeView treeView_Achievements_ModifierTreeChildNodes;
        private System.Windows.Forms.TextBox textBox_SpellDestinations;
        private System.Windows.Forms.Label label_SpellDestinations;
        private System.Windows.Forms.TabPage tabPage_Conditions_Creator;
        public System.Windows.Forms.ComboBox comboBox_ConditionSourceType;
        private System.Windows.Forms.Label label_ConditionSourceType;
        private System.Windows.Forms.Label label_ConditionType;
        public System.Windows.Forms.ComboBox comboBox_ConditionType;
        public System.Windows.Forms.TextBox textBox_ElseGroup;
        private System.Windows.Forms.Label label_ElseGroup;
        public System.Windows.Forms.TextBox textBox_SourceId;
        private System.Windows.Forms.Label label_SourceId;
        public System.Windows.Forms.TextBox textBox_SourceEntry;
        private System.Windows.Forms.Label label_SourceEntry;
        public System.Windows.Forms.TextBox textBox_SourceGroup;
        private System.Windows.Forms.Label label_ConditionSourceGroup;
        private System.Windows.Forms.Label label_ScriptName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_ConditionValue3;
        private System.Windows.Forms.Label labelConditionValue2;
        private System.Windows.Forms.Label label_ConditionValue1;
        public System.Windows.Forms.TextBox textBox_ScriptName;
        public System.Windows.Forms.TextBox textBox_NegativeCondition;
        public System.Windows.Forms.TextBox textBox_ConditionValue3;
        public System.Windows.Forms.TextBox textBox_ConditionValue2;
        public System.Windows.Forms.TextBox textBox_ConditionValue1;
        public System.Windows.Forms.TextBox textBox_ConditionTarget;
        private System.Windows.Forms.Label label_ConditionTarget;
        public System.Windows.Forms.TextBox textBox_ConditionsOutput;
        private System.Windows.Forms.Button button_AddCondition;
        private System.Windows.Forms.Button button_ClearConditions;
    }
}

