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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl_DoubleSpawnsRemover = new System.Windows.Forms.TabControl();
            this.tabPage_CreatureSpellsCreator = new System.Windows.Forms.TabPage();
            this.checkBox_OnlyCombatSpells = new System.Windows.Forms.CheckBox();
            this.dataGridView_Spells = new System.Windows.Forms.DataGridView();
            this.Spell_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spell_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cast_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Min_Cast_Start_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Max_Cast_Start_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Min_Cast_Repeat_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Max_Cast_Repeat_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Casts_Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.createSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_CreatureGuids = new System.Windows.Forms.ListBox();
            this.toolStrip_CreatureSpellsCreator = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_ImportSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_CreatureEntry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_CreatureEntry = new System.Windows.Forms.ToolStripLabel();
            this.tabPage_Output = new System.Windows.Forms.TabPage();
            this.textBox_SQLOutput = new System.Windows.Forms.TextBox();
            this.tabPage_DatabaseAdvisor = new System.Windows.Forms.TabPage();
            this.textBox_AreatriggerVerticesParser = new System.Windows.Forms.TextBox();
            this.label_AreatriggerVerticesParser = new System.Windows.Forms.Label();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_CreatureAddonImportFile = new System.Windows.Forms.Button();
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl_DoubleSpawnsRemover.SuspendLayout();
            this.tabPage_CreatureSpellsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip_CreatureSpellsCreator.SuspendLayout();
            this.tabPage_Output.SuspendLayout();
            this.tabPage_DatabaseAdvisor.SuspendLayout();
            this.tabPage_DoubleSpawnsRemover.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip_LoadedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_DoubleSpawnsRemover
            // 
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_CreatureSpellsCreator);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_Output);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_DatabaseAdvisor);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_DoubleSpawnsRemover);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage1);
            this.tabControl_DoubleSpawnsRemover.Location = new System.Drawing.Point(3, 3);
            this.tabControl_DoubleSpawnsRemover.Name = "tabControl_DoubleSpawnsRemover";
            this.tabControl_DoubleSpawnsRemover.SelectedIndex = 0;
            this.tabControl_DoubleSpawnsRemover.Size = new System.Drawing.Size(1773, 992);
            this.tabControl_DoubleSpawnsRemover.TabIndex = 1;
            // 
            // tabPage_CreatureSpellsCreator
            // 
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.checkBox_OnlyCombatSpells);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.dataGridView_Spells);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.listBox_CreatureGuids);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.toolStrip_CreatureSpellsCreator);
            this.tabPage_CreatureSpellsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CreatureSpellsCreator.Name = "tabPage_CreatureSpellsCreator";
            this.tabPage_CreatureSpellsCreator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CreatureSpellsCreator.Size = new System.Drawing.Size(1765, 959);
            this.tabPage_CreatureSpellsCreator.TabIndex = 0;
            this.tabPage_CreatureSpellsCreator.Text = "Creature Spells Creator";
            this.tabPage_CreatureSpellsCreator.UseVisualStyleBackColor = true;
            // 
            // checkBox_OnlyCombatSpells
            // 
            this.checkBox_OnlyCombatSpells.AutoSize = true;
            this.checkBox_OnlyCombatSpells.Checked = true;
            this.checkBox_OnlyCombatSpells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OnlyCombatSpells.Location = new System.Drawing.Point(1251, 9);
            this.checkBox_OnlyCombatSpells.Name = "checkBox_OnlyCombatSpells";
            this.checkBox_OnlyCombatSpells.Size = new System.Drawing.Size(173, 24);
            this.checkBox_OnlyCombatSpells.TabIndex = 4;
            this.checkBox_OnlyCombatSpells.Text = "Only Combat Spells";
            this.checkBox_OnlyCombatSpells.UseVisualStyleBackColor = true;
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
            this.Spell_Id,
            this.Spell_Name,
            this.Cast_Time,
            this.Min_Cast_Start_Time,
            this.Max_Cast_Start_Time,
            this.Min_Cast_Repeat_Time,
            this.Max_Cast_Repeat_Time,
            this.Casts_Count});
            this.dataGridView_Spells.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView_Spells.Enabled = false;
            this.dataGridView_Spells.Location = new System.Drawing.Point(380, 45);
            this.dataGridView_Spells.Name = "dataGridView_Spells";
            this.dataGridView_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Spells.RowTemplate.Height = 28;
            this.dataGridView_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Spells.Size = new System.Drawing.Size(1378, 905);
            this.dataGridView_Spells.TabIndex = 3;
            // 
            // Spell_Id
            // 
            this.Spell_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Spell_Id.DefaultCellStyle = dataGridViewCellStyle1;
            this.Spell_Id.HeaderText = "Spell_Id";
            this.Spell_Id.MaxInputLength = 10;
            this.Spell_Id.Name = "Spell_Id";
            this.Spell_Id.ReadOnly = true;
            this.Spell_Id.Width = 103;
            // 
            // Spell_Name
            // 
            this.Spell_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Spell_Name.DefaultCellStyle = dataGridViewCellStyle2;
            this.Spell_Name.HeaderText = "Spell_Name";
            this.Spell_Name.Name = "Spell_Name";
            this.Spell_Name.ReadOnly = true;
            this.Spell_Name.Width = 131;
            // 
            // Cast_Time
            // 
            this.Cast_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cast_Time.DefaultCellStyle = dataGridViewCellStyle3;
            this.Cast_Time.HeaderText = "Cast_Time";
            this.Cast_Time.Name = "Cast_Time";
            this.Cast_Time.ReadOnly = true;
            this.Cast_Time.Width = 121;
            // 
            // Min_Cast_Start_Time
            // 
            this.Min_Cast_Start_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Min_Cast_Start_Time.HeaderText = "Min_Cast_Start_Time";
            this.Min_Cast_Start_Time.Name = "Min_Cast_Start_Time";
            this.Min_Cast_Start_Time.ReadOnly = true;
            this.Min_Cast_Start_Time.Width = 199;
            // 
            // Max_Cast_Start_Time
            // 
            this.Max_Cast_Start_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Max_Cast_Start_Time.HeaderText = "Max_Cast_Start_Time";
            this.Max_Cast_Start_Time.Name = "Max_Cast_Start_Time";
            this.Max_Cast_Start_Time.ReadOnly = true;
            this.Max_Cast_Start_Time.Width = 203;
            // 
            // Min_Cast_Repeat_Time
            // 
            this.Min_Cast_Repeat_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Min_Cast_Repeat_Time.HeaderText = "Min_Cast_Repeat_Time";
            this.Min_Cast_Repeat_Time.Name = "Min_Cast_Repeat_Time";
            this.Min_Cast_Repeat_Time.ReadOnly = true;
            this.Min_Cast_Repeat_Time.Width = 217;
            // 
            // Max_Cast_Repeat_Time
            // 
            this.Max_Cast_Repeat_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Max_Cast_Repeat_Time.HeaderText = "Max_Cast_Repeat_Time";
            this.Max_Cast_Repeat_Time.Name = "Max_Cast_Repeat_Time";
            this.Max_Cast_Repeat_Time.ReadOnly = true;
            this.Max_Cast_Repeat_Time.Width = 221;
            // 
            // Casts_Count
            // 
            this.Casts_Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Casts_Count.DefaultCellStyle = dataGridViewCellStyle4;
            this.Casts_Count.HeaderText = "Casts_Count";
            this.Casts_Count.Name = "Casts_Count";
            this.Casts_Count.ReadOnly = true;
            this.Casts_Count.Width = 138;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.toolStripSeparator,
            this.createSQLToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(172, 70);
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
            this.listBox_CreatureGuids.Enabled = false;
            this.listBox_CreatureGuids.FormattingEnabled = true;
            this.listBox_CreatureGuids.ItemHeight = 20;
            this.listBox_CreatureGuids.Location = new System.Drawing.Point(6, 45);
            this.listBox_CreatureGuids.Name = "listBox_CreatureGuids";
            this.listBox_CreatureGuids.Size = new System.Drawing.Size(356, 904);
            this.listBox_CreatureGuids.TabIndex = 2;
            this.listBox_CreatureGuids.SelectedIndexChanged += new System.EventHandler(this.listBox_CreatureGuids_SelectedIndexChanged);
            // 
            // toolStrip_CreatureSpellsCreator
            // 
            this.toolStrip_CreatureSpellsCreator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_ImportSniff,
            this.toolStripButton_Search,
            this.toolStripTextBox_CreatureEntry,
            this.toolStripLabel_CreatureEntry});
            this.toolStrip_CreatureSpellsCreator.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_CreatureSpellsCreator.Name = "toolStrip_CreatureSpellsCreator";
            this.toolStrip_CreatureSpellsCreator.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip_CreatureSpellsCreator.Size = new System.Drawing.Size(1759, 32);
            this.toolStrip_CreatureSpellsCreator.TabIndex = 1;
            this.toolStrip_CreatureSpellsCreator.Text = "toolStrip_CreatureSpellsCreator";
            // 
            // toolStripButton_ImportSniff
            // 
            this.toolStripButton_ImportSniff.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Import;
            this.toolStripButton_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ImportSniff.Name = "toolStripButton_ImportSniff";
            this.toolStripButton_ImportSniff.Size = new System.Drawing.Size(136, 29);
            this.toolStripButton_ImportSniff.Text = "Import Sniff";
            this.toolStripButton_ImportSniff.Click += new System.EventHandler(this.toolStripButton_ImportSniff_Click);
            // 
            // toolStripButton_Search
            // 
            this.toolStripButton_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Search.Enabled = false;
            this.toolStripButton_Search.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Search;
            this.toolStripButton_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Search.Name = "toolStripButton_Search";
            this.toolStripButton_Search.Size = new System.Drawing.Size(92, 29);
            this.toolStripButton_Search.Text = "Search";
            this.toolStripButton_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripTextBox_CreatureEntry
            // 
            this.toolStripTextBox_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CreatureEntry.MaxLength = 10;
            this.toolStripTextBox_CreatureEntry.Name = "toolStripTextBox_CreatureEntry";
            this.toolStripTextBox_CreatureEntry.Size = new System.Drawing.Size(100, 32);
            // 
            // toolStripLabel_CreatureEntry
            // 
            this.toolStripLabel_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CreatureEntry.Name = "toolStripLabel_CreatureEntry";
            this.toolStripLabel_CreatureEntry.Size = new System.Drawing.Size(127, 29);
            this.toolStripLabel_CreatureEntry.Text = "Creature Entry:";
            // 
            // tabPage_Output
            // 
            this.tabPage_Output.Controls.Add(this.textBox_SQLOutput);
            this.tabPage_Output.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Output.Name = "tabPage_Output";
            this.tabPage_Output.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Output.Size = new System.Drawing.Size(1765, 959);
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
            this.textBox_SQLOutput.Size = new System.Drawing.Size(1759, 953);
            this.textBox_SQLOutput.TabIndex = 0;
            this.textBox_SQLOutput.WordWrap = false;
            // 
            // tabPage_DatabaseAdvisor
            // 
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_AreatriggerVerticesParser);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_AreatriggerVerticesParser);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DatabaseAdvisor.Name = "tabPage_DatabaseAdvisor";
            this.tabPage_DatabaseAdvisor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_DatabaseAdvisor.Size = new System.Drawing.Size(1765, 959);
            this.tabPage_DatabaseAdvisor.TabIndex = 2;
            this.tabPage_DatabaseAdvisor.Text = "Database Advisor";
            this.tabPage_DatabaseAdvisor.UseVisualStyleBackColor = true;
            // 
            // textBox_AreatriggerVerticesParser
            // 
            this.textBox_AreatriggerVerticesParser.Location = new System.Drawing.Point(1562, 28);
            this.textBox_AreatriggerVerticesParser.Name = "textBox_AreatriggerVerticesParser";
            this.textBox_AreatriggerVerticesParser.Size = new System.Drawing.Size(187, 26);
            this.textBox_AreatriggerVerticesParser.TabIndex = 5;
            this.textBox_AreatriggerVerticesParser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_AreatriggerVerticesParser_KeyDown);
            // 
            // label_AreatriggerVerticesParser
            // 
            this.label_AreatriggerVerticesParser.AutoSize = true;
            this.label_AreatriggerVerticesParser.Location = new System.Drawing.Point(1557, 3);
            this.label_AreatriggerVerticesParser.Name = "label_AreatriggerVerticesParser";
            this.label_AreatriggerVerticesParser.Size = new System.Drawing.Size(200, 20);
            this.label_AreatriggerVerticesParser.TabIndex = 4;
            this.label_AreatriggerVerticesParser.Text = "Areatrigger Vertices Parser";
            // 
            // textBox_QuestFlags
            // 
            this.textBox_QuestFlags.Location = new System.Drawing.Point(8, 83);
            this.textBox_QuestFlags.Name = "textBox_QuestFlags";
            this.textBox_QuestFlags.Size = new System.Drawing.Size(112, 26);
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
            this.tabPage_DoubleSpawnsRemover.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_DoubleSpawnsRemover.Size = new System.Drawing.Size(1765, 959);
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
            this.button_ImportFileForRemoving.Size = new System.Drawing.Size(232, 42);
            this.button_ImportFileForRemoving.TabIndex = 0;
            this.button_ImportFileForRemoving.Text = "Import File";
            this.button_ImportFileForRemoving.UseVisualStyleBackColor = true;
            this.button_ImportFileForRemoving.Click += new System.EventHandler(this.button_ImportFile_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_CreatureAddonImportFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1765, 959);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Creature Parser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_CreatureAddonImportFile
            // 
            this.button_CreatureAddonImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CreatureAddonImportFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_CreatureAddonImportFile.FlatAppearance.BorderSize = 5;
            this.button_CreatureAddonImportFile.Font = new System.Drawing.Font("Sitka Small", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreatureAddonImportFile.Location = new System.Drawing.Point(679, 6);
            this.button_CreatureAddonImportFile.Name = "button_CreatureAddonImportFile";
            this.button_CreatureAddonImportFile.Size = new System.Drawing.Size(232, 42);
            this.button_CreatureAddonImportFile.TabIndex = 1;
            this.button_CreatureAddonImportFile.Text = "Import File";
            this.button_CreatureAddonImportFile.UseVisualStyleBackColor = true;
            this.button_CreatureAddonImportFile.Click += new System.EventHandler(this.button_CreatureAddonImportFile_Click);
            // 
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 998);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(1776, 30);
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
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1776, 1028);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl_DoubleSpawnsRemover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wow Developer Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl_DoubleSpawnsRemover.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip_CreatureSpellsCreator.ResumeLayout(false);
            this.toolStrip_CreatureSpellsCreator.PerformLayout();
            this.tabPage_Output.ResumeLayout(false);
            this.tabPage_Output.PerformLayout();
            this.tabPage_DatabaseAdvisor.ResumeLayout(false);
            this.tabPage_DatabaseAdvisor.PerformLayout();
            this.tabPage_DoubleSpawnsRemover.ResumeLayout(false);
            this.tabPage_DoubleSpawnsRemover.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.statusStrip_LoadedFile.ResumeLayout(false);
            this.statusStrip_LoadedFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_DoubleSpawnsRemover;
        private System.Windows.Forms.TabPage tabPage_CreatureSpellsCreator;
        private System.Windows.Forms.ToolStrip toolStrip_CreatureSpellsCreator;
        private System.Windows.Forms.ToolStripButton toolStripButton_ImportSniff;
        private System.Windows.Forms.ToolStripButton toolStripButton_Search;
        private System.Windows.Forms.TabPage tabPage_Output;
        private System.Windows.Forms.StatusStrip statusStrip_LoadedFile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_FileStatus;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_CreatureEntry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_CreatureEntry;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridView dataGridView_Spells;
        private System.Windows.Forms.ListBox listBox_CreatureGuids;
        private System.Windows.Forms.CheckBox checkBox_OnlyCombatSpells;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spell_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spell_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cast_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Min_Cast_Start_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Max_Cast_Start_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Min_Cast_Repeat_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Max_Cast_Repeat_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Casts_Count;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem createSQLToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_SQLOutput;
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
        private System.Windows.Forms.TextBox textBox_AreatriggerVerticesParser;
        private System.Windows.Forms.Label label_AreatriggerVerticesParser;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_CreatureAddonImportFile;
    }
}

