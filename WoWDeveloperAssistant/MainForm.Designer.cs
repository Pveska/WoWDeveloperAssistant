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
            this.tabControl_DoubleSpawnsRemover.SuspendLayout();
            this.tabPage_CreatureSpellsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip_CreatureSpellsCreator.SuspendLayout();
            this.tabPage_Output.SuspendLayout();
            this.tabPage_DatabaseAdvisor.SuspendLayout();
            this.tabPage_DoubleSpawnsRemover.SuspendLayout();
            this.statusStrip_LoadedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_DoubleSpawnsRemover
            // 
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_CreatureSpellsCreator);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_Output);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_DatabaseAdvisor);
            this.tabControl_DoubleSpawnsRemover.Controls.Add(this.tabPage_DoubleSpawnsRemover);
            this.tabControl_DoubleSpawnsRemover.Location = new System.Drawing.Point(2, 2);
            this.tabControl_DoubleSpawnsRemover.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl_DoubleSpawnsRemover.Name = "tabControl_DoubleSpawnsRemover";
            this.tabControl_DoubleSpawnsRemover.SelectedIndex = 0;
            this.tabControl_DoubleSpawnsRemover.Size = new System.Drawing.Size(1182, 645);
            this.tabControl_DoubleSpawnsRemover.TabIndex = 1;
            // 
            // tabPage_CreatureSpellsCreator
            // 
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.checkBox_OnlyCombatSpells);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.dataGridView_Spells);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.listBox_CreatureGuids);
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.toolStrip_CreatureSpellsCreator);
            this.tabPage_CreatureSpellsCreator.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CreatureSpellsCreator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_CreatureSpellsCreator.Name = "tabPage_CreatureSpellsCreator";
            this.tabPage_CreatureSpellsCreator.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_CreatureSpellsCreator.Size = new System.Drawing.Size(1174, 619);
            this.tabPage_CreatureSpellsCreator.TabIndex = 0;
            this.tabPage_CreatureSpellsCreator.Text = "Creature Spells Creator";
            this.tabPage_CreatureSpellsCreator.UseVisualStyleBackColor = true;
            // 
            // checkBox_OnlyCombatSpells
            // 
            this.checkBox_OnlyCombatSpells.AutoSize = true;
            this.checkBox_OnlyCombatSpells.Checked = true;
            this.checkBox_OnlyCombatSpells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OnlyCombatSpells.Location = new System.Drawing.Point(834, 6);
            this.checkBox_OnlyCombatSpells.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_OnlyCombatSpells.Name = "checkBox_OnlyCombatSpells";
            this.checkBox_OnlyCombatSpells.Size = new System.Drawing.Size(117, 17);
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
            this.dataGridView_Spells.Location = new System.Drawing.Point(253, 29);
            this.dataGridView_Spells.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView_Spells.Name = "dataGridView_Spells";
            this.dataGridView_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Spells.RowTemplate.Height = 28;
            this.dataGridView_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Spells.Size = new System.Drawing.Size(919, 588);
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
            this.Spell_Id.Width = 70;
            // 
            // Spell_Name
            // 
            this.Spell_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Spell_Name.DefaultCellStyle = dataGridViewCellStyle2;
            this.Spell_Name.HeaderText = "Spell_Name";
            this.Spell_Name.Name = "Spell_Name";
            this.Spell_Name.ReadOnly = true;
            this.Spell_Name.Width = 89;
            // 
            // Cast_Time
            // 
            this.Cast_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cast_Time.DefaultCellStyle = dataGridViewCellStyle3;
            this.Cast_Time.HeaderText = "Cast_Time";
            this.Cast_Time.Name = "Cast_Time";
            this.Cast_Time.ReadOnly = true;
            this.Cast_Time.Width = 82;
            // 
            // Min_Cast_Start_Time
            // 
            this.Min_Cast_Start_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Min_Cast_Start_Time.HeaderText = "Min_Cast_Start_Time";
            this.Min_Cast_Start_Time.Name = "Min_Cast_Start_Time";
            this.Min_Cast_Start_Time.ReadOnly = true;
            this.Min_Cast_Start_Time.Width = 133;
            // 
            // Max_Cast_Start_Time
            // 
            this.Max_Cast_Start_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Max_Cast_Start_Time.HeaderText = "Max_Cast_Start_Time";
            this.Max_Cast_Start_Time.Name = "Max_Cast_Start_Time";
            this.Max_Cast_Start_Time.ReadOnly = true;
            this.Max_Cast_Start_Time.Width = 136;
            // 
            // Min_Cast_Repeat_Time
            // 
            this.Min_Cast_Repeat_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Min_Cast_Repeat_Time.HeaderText = "Min_Cast_Repeat_Time";
            this.Min_Cast_Repeat_Time.Name = "Min_Cast_Repeat_Time";
            this.Min_Cast_Repeat_Time.ReadOnly = true;
            this.Min_Cast_Repeat_Time.Width = 146;
            // 
            // Max_Cast_Repeat_Time
            // 
            this.Max_Cast_Repeat_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Max_Cast_Repeat_Time.HeaderText = "Max_Cast_Repeat_Time";
            this.Max_Cast_Repeat_Time.Name = "Max_Cast_Repeat_Time";
            this.Max_Cast_Repeat_Time.ReadOnly = true;
            this.Max_Cast_Repeat_Time.Width = 149;
            // 
            // Casts_Count
            // 
            this.Casts_Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Casts_Count.DefaultCellStyle = dataGridViewCellStyle4;
            this.Casts_Count.HeaderText = "Casts_Count";
            this.Casts_Count.Name = "Casts_Count";
            this.Casts_Count.ReadOnly = true;
            this.Casts_Count.Width = 92;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.toolStripSeparator,
            this.createSQLToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(133, 54);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(129, 6);
            // 
            // createSQLToolStripMenuItem
            // 
            this.createSQLToolStripMenuItem.Name = "createSQLToolStripMenuItem";
            this.createSQLToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.createSQLToolStripMenuItem.Text = "Create SQL";
            this.createSQLToolStripMenuItem.Click += new System.EventHandler(this.createSQLToolStripMenuItem_Click);
            // 
            // listBox_CreatureGuids
            // 
            this.listBox_CreatureGuids.Enabled = false;
            this.listBox_CreatureGuids.FormattingEnabled = true;
            this.listBox_CreatureGuids.Location = new System.Drawing.Point(4, 29);
            this.listBox_CreatureGuids.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox_CreatureGuids.Name = "listBox_CreatureGuids";
            this.listBox_CreatureGuids.Size = new System.Drawing.Size(239, 589);
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
            this.toolStrip_CreatureSpellsCreator.Location = new System.Drawing.Point(2, 2);
            this.toolStrip_CreatureSpellsCreator.Name = "toolStrip_CreatureSpellsCreator";
            this.toolStrip_CreatureSpellsCreator.Size = new System.Drawing.Size(1170, 25);
            this.toolStrip_CreatureSpellsCreator.TabIndex = 1;
            this.toolStrip_CreatureSpellsCreator.Text = "toolStrip_CreatureSpellsCreator";
            // 
            // toolStripButton_ImportSniff
            // 
            this.toolStripButton_ImportSniff.Image = global::WoWDeveloperAssistant.Properties.Resources.PIC_Import;
            this.toolStripButton_ImportSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ImportSniff.Name = "toolStripButton_ImportSniff";
            this.toolStripButton_ImportSniff.Size = new System.Drawing.Size(90, 22);
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
            this.toolStripButton_Search.Size = new System.Drawing.Size(62, 22);
            this.toolStripButton_Search.Text = "Search";
            this.toolStripButton_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripTextBox_CreatureEntry
            // 
            this.toolStripTextBox_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CreatureEntry.MaxLength = 10;
            this.toolStripTextBox_CreatureEntry.Name = "toolStripTextBox_CreatureEntry";
            this.toolStripTextBox_CreatureEntry.Size = new System.Drawing.Size(68, 25);
            // 
            // toolStripLabel_CreatureEntry
            // 
            this.toolStripLabel_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CreatureEntry.Name = "toolStripLabel_CreatureEntry";
            this.toolStripLabel_CreatureEntry.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel_CreatureEntry.Text = "Creature Entry:";
            // 
            // tabPage_Output
            // 
            this.tabPage_Output.Controls.Add(this.textBox_SQLOutput);
            this.tabPage_Output.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Output.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_Output.Name = "tabPage_Output";
            this.tabPage_Output.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_Output.Size = new System.Drawing.Size(1174, 619);
            this.tabPage_Output.TabIndex = 1;
            this.tabPage_Output.Text = "SQL Output";
            this.tabPage_Output.UseVisualStyleBackColor = true;
            // 
            // textBox_SQLOutput
            // 
            this.textBox_SQLOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SQLOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBox_SQLOutput.Location = new System.Drawing.Point(2, 2);
            this.textBox_SQLOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_SQLOutput.Multiline = true;
            this.textBox_SQLOutput.Name = "textBox_SQLOutput";
            this.textBox_SQLOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_SQLOutput.Size = new System.Drawing.Size(1170, 615);
            this.textBox_SQLOutput.TabIndex = 0;
            this.textBox_SQLOutput.WordWrap = false;
            // 
            // tabPage_DatabaseAdvisor
            // 
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_QuestFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.textBox_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Controls.Add(this.label_CreatureFlags);
            this.tabPage_DatabaseAdvisor.Location = new System.Drawing.Point(4, 22);
            this.tabPage_DatabaseAdvisor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_DatabaseAdvisor.Name = "tabPage_DatabaseAdvisor";
            this.tabPage_DatabaseAdvisor.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_DatabaseAdvisor.Size = new System.Drawing.Size(1174, 619);
            this.tabPage_DatabaseAdvisor.TabIndex = 2;
            this.tabPage_DatabaseAdvisor.Text = "Database Advisor";
            this.tabPage_DatabaseAdvisor.UseVisualStyleBackColor = true;
            // 
            // textBox_QuestFlags
            // 
            this.textBox_QuestFlags.Location = new System.Drawing.Point(5, 54);
            this.textBox_QuestFlags.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_QuestFlags.Name = "textBox_QuestFlags";
            this.textBox_QuestFlags.Size = new System.Drawing.Size(76, 20);
            this.textBox_QuestFlags.TabIndex = 3;
            this.textBox_QuestFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_QuestFlags_KeyDown);
            // 
            // label_QuestFlags
            // 
            this.label_QuestFlags.AutoSize = true;
            this.label_QuestFlags.Location = new System.Drawing.Point(4, 39);
            this.label_QuestFlags.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_QuestFlags.Name = "label_QuestFlags";
            this.label_QuestFlags.Size = new System.Drawing.Size(63, 13);
            this.label_QuestFlags.TabIndex = 2;
            this.label_QuestFlags.Text = "Quest Flags";
            // 
            // textBox_CreatureFlags
            // 
            this.textBox_CreatureFlags.Location = new System.Drawing.Point(5, 18);
            this.textBox_CreatureFlags.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_CreatureFlags.Name = "textBox_CreatureFlags";
            this.textBox_CreatureFlags.Size = new System.Drawing.Size(76, 20);
            this.textBox_CreatureFlags.TabIndex = 1;
            this.textBox_CreatureFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_CreatureFlags_KeyDown);
            // 
            // label_CreatureFlags
            // 
            this.label_CreatureFlags.AutoSize = true;
            this.label_CreatureFlags.Location = new System.Drawing.Point(3, 2);
            this.label_CreatureFlags.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CreatureFlags.Name = "label_CreatureFlags";
            this.label_CreatureFlags.Size = new System.Drawing.Size(75, 13);
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
            this.tabPage_DoubleSpawnsRemover.Location = new System.Drawing.Point(4, 22);
            this.tabPage_DoubleSpawnsRemover.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_DoubleSpawnsRemover.Name = "tabPage_DoubleSpawnsRemover";
            this.tabPage_DoubleSpawnsRemover.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_DoubleSpawnsRemover.Size = new System.Drawing.Size(1174, 619);
            this.tabPage_DoubleSpawnsRemover.TabIndex = 3;
            this.tabPage_DoubleSpawnsRemover.Text = "Double-Spawns Remover";
            this.tabPage_DoubleSpawnsRemover.UseVisualStyleBackColor = true;
            // 
            // checkBox_DatabaseConsidering
            // 
            this.checkBox_DatabaseConsidering.AutoSize = true;
            this.checkBox_DatabaseConsidering.Checked = true;
            this.checkBox_DatabaseConsidering.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DatabaseConsidering.Location = new System.Drawing.Point(482, 51);
            this.checkBox_DatabaseConsidering.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_DatabaseConsidering.Name = "checkBox_DatabaseConsidering";
            this.checkBox_DatabaseConsidering.Size = new System.Drawing.Size(128, 17);
            this.checkBox_DatabaseConsidering.TabIndex = 5;
            this.checkBox_DatabaseConsidering.Text = "Considering database";
            this.checkBox_DatabaseConsidering.UseVisualStyleBackColor = true;
            // 
            // label_GameobjectsRemoved
            // 
            this.label_GameobjectsRemoved.AutoSize = true;
            this.label_GameobjectsRemoved.Location = new System.Drawing.Point(329, 86);
            this.label_GameobjectsRemoved.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_GameobjectsRemoved.Name = "label_GameobjectsRemoved";
            this.label_GameobjectsRemoved.Size = new System.Drawing.Size(128, 13);
            this.label_GameobjectsRemoved.TabIndex = 4;
            this.label_GameobjectsRemoved.Text = "No gameobjects removed";
            this.label_GameobjectsRemoved.Visible = false;
            // 
            // checkBox_GameobjectsRemover
            // 
            this.checkBox_GameobjectsRemover.AutoSize = true;
            this.checkBox_GameobjectsRemover.Checked = true;
            this.checkBox_GameobjectsRemover.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_GameobjectsRemover.Location = new System.Drawing.Point(535, 3);
            this.checkBox_GameobjectsRemover.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_GameobjectsRemover.Name = "checkBox_GameobjectsRemover";
            this.checkBox_GameobjectsRemover.Size = new System.Drawing.Size(88, 17);
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
            this.checkBox_CreaturesRemover.Location = new System.Drawing.Point(467, 3);
            this.checkBox_CreaturesRemover.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_CreaturesRemover.Name = "checkBox_CreaturesRemover";
            this.checkBox_CreaturesRemover.Size = new System.Drawing.Size(71, 17);
            this.checkBox_CreaturesRemover.TabIndex = 2;
            this.checkBox_CreaturesRemover.Text = "Creatures";
            this.checkBox_CreaturesRemover.UseVisualStyleBackColor = true;
            this.checkBox_CreaturesRemover.CheckedChanged += new System.EventHandler(this.checkBox_CreaturesRemover_CheckedChanged);
            // 
            // label_CreaturesRemoved
            // 
            this.label_CreaturesRemoved.AutoSize = true;
            this.label_CreaturesRemoved.Location = new System.Drawing.Point(329, 73);
            this.label_CreaturesRemoved.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CreaturesRemoved.Name = "label_CreaturesRemoved";
            this.label_CreaturesRemoved.Size = new System.Drawing.Size(112, 13);
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
            this.button_ImportFileForRemoving.Location = new System.Drawing.Point(467, 19);
            this.button_ImportFileForRemoving.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_ImportFileForRemoving.Name = "button_ImportFileForRemoving";
            this.button_ImportFileForRemoving.Size = new System.Drawing.Size(155, 27);
            this.button_ImportFileForRemoving.TabIndex = 0;
            this.button_ImportFileForRemoving.Text = "Import File";
            this.button_ImportFileForRemoving.UseVisualStyleBackColor = true;
            this.button_ImportFileForRemoving.Click += new System.EventHandler(this.button_ImportFile_Click);
            // 
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 646);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip_LoadedFile.TabIndex = 2;
            this.statusStrip_LoadedFile.Text = "statusStrip";
            // 
            // toolStripStatusLabel_FileStatus
            // 
            this.toolStripStatusLabel_FileStatus.Name = "toolStripStatusLabel_FileStatus";
            this.toolStripStatusLabel_FileStatus.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel_FileStatus.Text = "No File Loaded";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 668);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl_DoubleSpawnsRemover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox_CreatureEntry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_CreatureEntry;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.DataGridView dataGridView_Spells;
        public System.Windows.Forms.ListBox listBox_CreatureGuids;
        public System.Windows.Forms.CheckBox checkBox_OnlyCombatSpells;
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
    }
}

