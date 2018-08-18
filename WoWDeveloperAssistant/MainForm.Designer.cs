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
            this.tabControl = new System.Windows.Forms.TabControl();
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
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_CreatureSpellsCreator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip_CreatureSpellsCreator.SuspendLayout();
            this.tabPage_Output.SuspendLayout();
            this.statusStrip_LoadedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_CreatureSpellsCreator);
            this.tabControl.Controls.Add(this.tabPage_Output);
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1773, 992);
            this.tabControl.TabIndex = 1;
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
            this.dataGridView_Spells.Location = new System.Drawing.Point(380, 44);
            this.dataGridView_Spells.Name = "dataGridView_Spells";
            this.dataGridView_Spells.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Spells.RowTemplate.Height = 28;
            this.dataGridView_Spells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Spells.Size = new System.Drawing.Size(1379, 904);
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
            this.listBox_CreatureGuids.Location = new System.Drawing.Point(6, 44);
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
            // statusStrip_LoadedFile
            // 
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 997);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
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
            this.ClientSize = new System.Drawing.Size(1776, 1027);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wow Developer Assistance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spells)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip_CreatureSpellsCreator.ResumeLayout(false);
            this.toolStrip_CreatureSpellsCreator.PerformLayout();
            this.tabPage_Output.ResumeLayout(false);
            this.tabPage_Output.PerformLayout();
            this.statusStrip_LoadedFile.ResumeLayout(false);
            this.statusStrip_LoadedFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
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
    }
}

