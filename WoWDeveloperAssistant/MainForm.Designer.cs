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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle76 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle77 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle78 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle87 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle88 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle79 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle80 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle81 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle82 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle85 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle86 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            this.textBoxAreatriggerSplines = new System.Windows.Forms.TextBox();
            this.label_AreatriggerSplines = new System.Windows.Forms.Label();
            this.textBox_QuestFlags = new System.Windows.Forms.TextBox();
            this.label_QuestFlags = new System.Windows.Forms.Label();
            this.textBox_CreatureFlags = new System.Windows.Forms.TextBox();
            this.label_CreatureFlags = new System.Windows.Forms.Label();
            this.tabPage_DoubleSpawnsRemover = new System.Windows.Forms.TabPage();
            this.checkBox_DatabaseConsidering = new System.Windows.Forms.CheckBox();
            this.label_GameobjectsRemoved = new System.Windows.Forms.Label();
            this.checkBox_GameobjectsRemover = new System.Windows.Forms.CheckBox();
            this.checkBox_CreaturesRemover = new System.Windows.Forms.CheckBox();
            this.label_CreaturesRemoved = new System.Windows.Forms.Label();
            this.button_ImportFileForRemoving = new System.Windows.Forms.Button();
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.dataGridView_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Spells.RowTemplate.Height = 28;
            this.dataGridView_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Spells.Size = new System.Drawing.Size(1269, 886);
            this.dataGridView_Spells.TabIndex = 3;
            // 
            // SpellId
            // 
            this.SpellId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellId.DefaultCellStyle = dataGridViewCellStyle45;
            this.SpellId.HeaderText = "SpellId";
            this.SpellId.MaxInputLength = 10;
            this.SpellId.Name = "SpellId";
            this.SpellId.ReadOnly = true;
            this.SpellId.Width = 94;
            // 
            // SpellName
            // 
            this.SpellName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SpellName.DefaultCellStyle = dataGridViewCellStyle46;
            this.SpellName.HeaderText = "SpellName";
            this.SpellName.MaxInputLength = 50;
            this.SpellName.Name = "SpellName";
            this.SpellName.ReadOnly = true;
            this.SpellName.Width = 122;
            // 
            // CastTime
            // 
            this.CastTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastTime.DefaultCellStyle = dataGridViewCellStyle55;
            this.CastTime.HeaderText = "CastTime";
            this.CastTime.MaxInputLength = 10;
            this.CastTime.Name = "CastTime";
            this.CastTime.ReadOnly = true;
            this.CastTime.Width = 112;
            // 
            // MinCastStartTime
            // 
            this.MinCastStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastStartTime.DefaultCellStyle = dataGridViewCellStyle56;
            this.MinCastStartTime.HeaderText = "MinCastStartTime";
            this.MinCastStartTime.MaxInputLength = 10;
            this.MinCastStartTime.Name = "MinCastStartTime";
            this.MinCastStartTime.ReadOnly = true;
            this.MinCastStartTime.Width = 172;
            // 
            // MaxCastStartTime
            // 
            this.MaxCastStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastStartTime.DefaultCellStyle = dataGridViewCellStyle73;
            this.MaxCastStartTime.HeaderText = "MaxCastStartTime";
            this.MaxCastStartTime.MaxInputLength = 10;
            this.MaxCastStartTime.Name = "MaxCastStartTime";
            this.MaxCastStartTime.ReadOnly = true;
            this.MaxCastStartTime.Width = 176;
            // 
            // MinCastRepeatTime
            // 
            this.MinCastRepeatTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MinCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle74;
            this.MinCastRepeatTime.HeaderText = "MinCastRepeatTime";
            this.MinCastRepeatTime.MaxInputLength = 10;
            this.MinCastRepeatTime.Name = "MinCastRepeatTime";
            this.MinCastRepeatTime.ReadOnly = true;
            this.MinCastRepeatTime.Width = 190;
            // 
            // MaxCastRepeatTime
            // 
            this.MaxCastRepeatTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MaxCastRepeatTime.DefaultCellStyle = dataGridViewCellStyle75;
            this.MaxCastRepeatTime.HeaderText = "MaxCastRepeatTime";
            this.MaxCastRepeatTime.MaxInputLength = 10;
            this.MaxCastRepeatTime.Name = "MaxCastRepeatTime";
            this.MaxCastRepeatTime.ReadOnly = true;
            this.MaxCastRepeatTime.Width = 194;
            // 
            // CastsCount
            // 
            this.CastsCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle76.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CastsCount.DefaultCellStyle = dataGridViewCellStyle76;
            this.CastsCount.HeaderText = "CastsCount";
            this.CastsCount.MaxInputLength = 4;
            this.CastsCount.Name = "CastsCount";
            this.CastsCount.ReadOnly = true;
            this.CastsCount.Width = 129;
            // 
            // SourceSpell
            // 
            this.SourceSpell.HeaderText = "SourceSpell";
            this.SourceSpell.Name = "SourceSpell";
            this.SourceSpell.Visible = false;
            // 
            // contextMenuStrip_CSC
            // 
            this.contextMenuStrip_CSC.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_CSC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.toolStripSeparator,
            this.createSQLToolStripMenuItem});
            this.contextMenuStrip_CSC.Name = "contextMenuStrip1";
            this.contextMenuStrip_CSC.Size = new System.Drawing.Size(172, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(171, 30);
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
            this.createSQLToolStripMenuItem.Size = new System.Drawing.Size(171, 30);
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
            this.toolStrip_CSC.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip_CSC.Size = new System.Drawing.Size(2034, 32);
            this.toolStrip_CSC.TabIndex = 1;
            this.toolStrip_CSC.Text = "toolStrip_CreatureScriptsCreator";
            // 
            // toolStripButton_CSC_ImportSniff
            // 
            this.toolStripButton_CSC_ImportSniff.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Import;
            this.toolStripButton_CSC_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CSC_ImportSniff.Name = "toolStripButton_CSC_ImportSniff";
            this.toolStripButton_CSC_ImportSniff.Size = new System.Drawing.Size(136, 29);
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
            this.toolStripButton_CSC_Search.Size = new System.Drawing.Size(92, 29);
            this.toolStripButton_CSC_Search.Text = "Search";
            this.toolStripButton_CSC_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripTextBox_CSC_CreatureEntry
            // 
            this.toolStripTextBox_CSC_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CSC_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CSC_CreatureEntry.MaxLength = 40;
            this.toolStripTextBox_CSC_CreatureEntry.Name = "toolStripTextBox_CSC_CreatureEntry";
            this.toolStripTextBox_CSC_CreatureEntry.Size = new System.Drawing.Size(100, 32);
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
            this.toolStripSeparator_CSC.Size = new System.Drawing.Size(6, 32);
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
            dataGridViewCellStyle77.NullValue = null;
            this.grid_WC_Waypoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle77;
            this.grid_WC_Waypoints.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle78.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle78.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle78.NullValue = null;
            dataGridViewCellStyle78.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle78.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle78.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_WC_Waypoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle78;
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
            dataGridViewCellStyle87.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle87.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle87.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle87.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle87.NullValue = null;
            dataGridViewCellStyle87.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle87.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle87.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_WC_Waypoints.DefaultCellStyle = dataGridViewCellStyle87;
            this.grid_WC_Waypoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid_WC_Waypoints.Enabled = false;
            this.grid_WC_Waypoints.Location = new System.Drawing.Point(1274, 49);
            this.grid_WC_Waypoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid_WC_Waypoints.Name = "grid_WC_Waypoints";
            this.grid_WC_Waypoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle88.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_WC_Waypoints.RowsDefaultCellStyle = dataGridViewCellStyle88;
            this.grid_WC_Waypoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_WC_Waypoints.Size = new System.Drawing.Size(758, 886);
            this.grid_WC_Waypoints.TabIndex = 28;
            this.grid_WC_Waypoints.TabStop = false;
            // 
            // gridColumn_Id
            // 
            dataGridViewCellStyle79.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Id.DefaultCellStyle = dataGridViewCellStyle79;
            this.gridColumn_Id.HeaderText = "Id";
            this.gridColumn_Id.Name = "gridColumn_Id";
            this.gridColumn_Id.ReadOnly = true;
            this.gridColumn_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.gridColumn_Id.Width = 35;
            // 
            // gridColumn_PosX
            // 
            dataGridViewCellStyle80.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle80.NullValue = null;
            this.gridColumn_PosX.DefaultCellStyle = dataGridViewCellStyle80;
            this.gridColumn_PosX.HeaderText = "PosX";
            this.gridColumn_PosX.Name = "gridColumn_PosX";
            this.gridColumn_PosX.ReadOnly = true;
            this.gridColumn_PosX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosX.Width = 60;
            // 
            // gridColumn_PosY
            // 
            dataGridViewCellStyle81.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle81.NullValue = null;
            this.gridColumn_PosY.DefaultCellStyle = dataGridViewCellStyle81;
            this.gridColumn_PosY.HeaderText = "PosY";
            this.gridColumn_PosY.Name = "gridColumn_PosY";
            this.gridColumn_PosY.ReadOnly = true;
            this.gridColumn_PosY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosY.Width = 60;
            // 
            // gridColumn_PosZ
            // 
            dataGridViewCellStyle82.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_PosZ.DefaultCellStyle = dataGridViewCellStyle82;
            this.gridColumn_PosZ.HeaderText = "PosZ";
            this.gridColumn_PosZ.Name = "gridColumn_PosZ";
            this.gridColumn_PosZ.ReadOnly = true;
            this.gridColumn_PosZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_PosZ.Width = 60;
            // 
            // gridColumn_Orientation
            // 
            dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_Orientation.DefaultCellStyle = dataGridViewCellStyle83;
            this.gridColumn_Orientation.HeaderText = "Orientation";
            this.gridColumn_Orientation.Name = "gridColumn_Orientation";
            this.gridColumn_Orientation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridColumn_Orientation.Width = 65;
            // 
            // gridColumn_WCTime
            // 
            dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCTime.DefaultCellStyle = dataGridViewCellStyle84;
            this.gridColumn_WCTime.HeaderText = "Time";
            this.gridColumn_WCTime.Name = "gridColumn_WCTime";
            this.gridColumn_WCTime.ReadOnly = true;
            this.gridColumn_WCTime.Width = 60;
            // 
            // gridColumn_WCDelay
            // 
            dataGridViewCellStyle85.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_WCDelay.DefaultCellStyle = dataGridViewCellStyle85;
            this.gridColumn_WCDelay.HeaderText = "Delay";
            this.gridColumn_WCDelay.Name = "gridColumn_WCDelay";
            this.gridColumn_WCDelay.Width = 50;
            // 
            // gridColumn_HasScript
            // 
            dataGridViewCellStyle86.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.gridColumn_HasScript.DefaultCellStyle = dataGridViewCellStyle86;
            this.gridColumn_HasScript.HeaderText = "HasScript";
            this.gridColumn_HasScript.Name = "gridColumn_HasScript";
            this.gridColumn_HasScript.ReadOnly = true;
            this.gridColumn_HasScript.Width = 60;
            // 
            // WaypointSource
            // 
            this.WaypointSource.HeaderText = "WaypointSource";
            this.WaypointSource.Name = "WaypointSource";
            this.WaypointSource.Visible = false;
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
            this.contextMenuStrip_WC.Size = new System.Drawing.Size(281, 160);
            // 
            // cutToolStripMenuItem_WC
            // 
            this.cutToolStripMenuItem_WC.Name = "cutToolStripMenuItem_WC";
            this.cutToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 30);
            this.cutToolStripMenuItem_WC.Text = "Cut";
            this.cutToolStripMenuItem_WC.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // removeNearestPointsToolStripMenuItem_WC
            // 
            this.removeNearestPointsToolStripMenuItem_WC.Name = "removeNearestPointsToolStripMenuItem_WC";
            this.removeNearestPointsToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 30);
            this.removeNearestPointsToolStripMenuItem_WC.Text = "Remove nearest points";
            this.removeNearestPointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeExcessPointsToolStripMenuItem_Click);
            // 
            // removeDuplicatePointsToolStripMenuItem_WC
            // 
            this.removeDuplicatePointsToolStripMenuItem_WC.Name = "removeDuplicatePointsToolStripMenuItem_WC";
            this.removeDuplicatePointsToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 30);
            this.removeDuplicatePointsToolStripMenuItem_WC.Text = "Remove duplicate points";
            this.removeDuplicatePointsToolStripMenuItem_WC.Click += new System.EventHandler(this.removeDuplicatePointsToolStripMenuItem_WC_Click);
            // 
            // createReturnPathToolStripMenuItem_WC
            // 
            this.createReturnPathToolStripMenuItem_WC.Name = "createReturnPathToolStripMenuItem_WC";
            this.createReturnPathToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 30);
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
            this.createSQLToolStripMenuItem_WC.Size = new System.Drawing.Size(280, 30);
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
            chartArea5.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea5.AxisX.IsReversed = true;
            chartArea5.AxisX.IsStartedFromZero = false;
            chartArea5.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MajorGrid.Enabled = false;
            chartArea5.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MajorTickMark.Enabled = false;
            chartArea5.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea5.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea5.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea5.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea5.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX2.MajorGrid.Enabled = false;
            chartArea5.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX2.MajorTickMark.Enabled = false;
            chartArea5.AxisX2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea5.AxisY.IsStartedFromZero = false;
            chartArea5.AxisY.LabelStyle.Enabled = false;
            chartArea5.AxisY.LabelStyle.IsEndLabelVisible = false;
            chartArea5.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.MajorGrid.Enabled = false;
            chartArea5.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.MajorTickMark.Enabled = false;
            chartArea5.AxisY.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea5.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea5.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea5.AxisY.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY2.MajorGrid.Enabled = false;
            chartArea5.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY2.MajorTickMark.Enabled = false;
            chartArea5.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.BorderColor = System.Drawing.Color.Transparent;
            chartArea5.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.CursorX.IsUserEnabled = true;
            chartArea5.CursorX.IsUserSelectionEnabled = true;
            chartArea5.CursorX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea5.CursorY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.CursorY.IsUserEnabled = true;
            chartArea5.CursorY.IsUserSelectionEnabled = true;
            chartArea5.CursorY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea5.Name = "ChartArea1";
            this.chart_WC.ChartAreas.Add(chartArea5);
            this.chart_WC.Enabled = false;
            legend5.Enabled = false;
            legend5.ForeColor = System.Drawing.Color.Transparent;
            legend5.HeaderSeparatorColor = System.Drawing.Color.Transparent;
            legend5.ItemColumnSeparatorColor = System.Drawing.Color.Transparent;
            legend5.Name = "Legend1";
            legend5.TitleForeColor = System.Drawing.Color.Transparent;
            legend5.TitleSeparatorColor = System.Drawing.Color.Transparent;
            this.chart_WC.Legends.Add(legend5);
            this.chart_WC.Location = new System.Drawing.Point(4, 49);
            this.chart_WC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_WC.Name = "chart_WC";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series5.Color = System.Drawing.Color.Red;
            series5.Legend = "Legend1";
            series5.Name = "Path";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart_WC.Series.Add(series5);
            this.chart_WC.Size = new System.Drawing.Size(894, 888);
            this.chart_WC.TabIndex = 26;
            this.chart_WC.Text = "Waypoints";
            title5.DockedToChartArea = "ChartArea1";
            title5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title5.Name = "Path";
            title5.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            this.chart_WC.Titles.Add(title5);
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
            this.toolStrip_WC.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip_WC.Size = new System.Drawing.Size(2034, 32);
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
            this.toolStripButton_WC_Search.Size = new System.Drawing.Size(92, 29);
            this.toolStripButton_WC_Search.Text = "Search";
            this.toolStripButton_WC_Search.ToolTipText = "Fill listbox with guids of\r\nselected entry or all entries.";
            this.toolStripButton_WC_Search.Click += new System.EventHandler(this.toolStripButton_WCSearch_Click);
            // 
            // toolStripTextBox_WC_Entry
            // 
            this.toolStripTextBox_WC_Entry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_WC_Entry.Enabled = false;
            this.toolStripTextBox_WC_Entry.MaxLength = 40;
            this.toolStripTextBox_WC_Entry.Name = "toolStripTextBox_WC_Entry";
            this.toolStripTextBox_WC_Entry.Size = new System.Drawing.Size(103, 32);
            this.toolStripTextBox_WC_Entry.Tag = "";
            this.toolStripTextBox_WC_Entry.ToolTipText = "Input entry of creature or leave\r\nblank to fill listbox will all in sniff.";
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
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
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
            this.tabPage_DoubleSpawnsRemover.Controls.Add(this.checkBox_DatabaseConsidering);
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
            // checkBox_DatabaseConsidering
            // 
            this.checkBox_DatabaseConsidering.AutoSize = true;
            this.checkBox_DatabaseConsidering.Checked = true;
            this.checkBox_DatabaseConsidering.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DatabaseConsidering.Location = new System.Drawing.Point(723, 78);
            this.checkBox_DatabaseConsidering.Name = "checkBox_DatabaseConsidering";
            this.checkBox_DatabaseConsidering.Size = new System.Drawing.Size(190, 24);
            this.checkBox_DatabaseConsidering.TabIndex = 5;
            this.checkBox_DatabaseConsidering.Text = "Considering database";
            this.checkBox_DatabaseConsidering.UseVisualStyleBackColor = true;
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
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.BackColor = System.Drawing.Color.LightGray;
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 979);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(2050, 30);
            this.statusStrip_LoadedFile.TabIndex = 2;
            this.statusStrip_LoadedFile.Text = "statusStrip";
            // 
            // toolStripStatusLabel_FileStatus
            // 
            this.toolStripStatusLabel_FileStatus.Name = "toolStripStatusLabel_FileStatus";
            this.toolStripStatusLabel_FileStatus.Size = new System.Drawing.Size(131, 25);
            this.toolStripStatusLabel_FileStatus.Text = "No File Loaded";
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
        private System.Windows.Forms.CheckBox checkBox_DatabaseConsidering;
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
    }
}

