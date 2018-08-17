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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_CreatureSpellsCreator = new System.Windows.Forms.TabPage();
            this.toolStrip_CreatureSpellsCreator = new System.Windows.Forms.ToolStrip();
            this.tabPage_Output = new System.Windows.Forms.TabPage();
            this.statusStrip_LoadedFile = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTextBox_CreatureEntry = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_ImportSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_CreatureEntry = new System.Windows.Forms.ToolStripLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_CreatureSpellsCreator.SuspendLayout();
            this.toolStrip_CreatureSpellsCreator.SuspendLayout();
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
            this.tabControl.Size = new System.Drawing.Size(1590, 992);
            this.tabControl.TabIndex = 1;
            //
            // tabPage_CreatureSpellsCreator
            //
            this.tabPage_CreatureSpellsCreator.Controls.Add(this.toolStrip_CreatureSpellsCreator);
            this.tabPage_CreatureSpellsCreator.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CreatureSpellsCreator.Name = "tabPage_CreatureSpellsCreator";
            this.tabPage_CreatureSpellsCreator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CreatureSpellsCreator.Size = new System.Drawing.Size(1582, 959);
            this.tabPage_CreatureSpellsCreator.TabIndex = 0;
            this.tabPage_CreatureSpellsCreator.Text = "Creature Spells Creator";
            this.tabPage_CreatureSpellsCreator.UseVisualStyleBackColor = true;
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
            this.toolStrip_CreatureSpellsCreator.Size = new System.Drawing.Size(1576, 32);
            this.toolStrip_CreatureSpellsCreator.TabIndex = 1;
            this.toolStrip_CreatureSpellsCreator.Text = "toolStrip_CreatureSpellsCreator";
            //
            // tabPage_Output
            //
            this.tabPage_Output.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Output.Name = "tabPage_Output";
            this.tabPage_Output.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Output.Size = new System.Drawing.Size(1582, 959);
            this.tabPage_Output.TabIndex = 1;
            this.tabPage_Output.Text = "Text Output";
            this.tabPage_Output.UseVisualStyleBackColor = true;
            //
            // statusStrip_LoadedFile
            //
            this.statusStrip_LoadedFile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_LoadedFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileStatus});
            this.statusStrip_LoadedFile.Location = new System.Drawing.Point(0, 997);
            this.statusStrip_LoadedFile.Name = "statusStrip_LoadedFile";
            this.statusStrip_LoadedFile.Size = new System.Drawing.Size(1596, 30);
            this.statusStrip_LoadedFile.TabIndex = 2;
            this.statusStrip_LoadedFile.Text = "statusStrip";
            //
            // toolStripStatusLabel_FileStatus
            //
            this.toolStripStatusLabel_FileStatus.Name = "toolStripStatusLabel_FileStatus";
            this.toolStripStatusLabel_FileStatus.Size = new System.Drawing.Size(131, 25);
            this.toolStripStatusLabel_FileStatus.Text = "No File Loaded";
            //
            // toolStripTextBox_CreatureEntry
            //
            this.toolStripTextBox_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_CreatureEntry.Enabled = false;
            this.toolStripTextBox_CreatureEntry.MaxLength = 10;
            this.toolStripTextBox_CreatureEntry.Name = "toolStripTextBox_CreatureEntry";
            this.toolStripTextBox_CreatureEntry.Size = new System.Drawing.Size(100, 32);
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
            //
            // toolStripLabel_CreatureEntry
            //
            this.toolStripLabel_CreatureEntry.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CreatureEntry.Name = "toolStripLabel_CreatureEntry";
            this.toolStripLabel_CreatureEntry.Size = new System.Drawing.Size(127, 29);
            this.toolStripLabel_CreatureEntry.Text = "Creature Entry:";
            //
            // openFileDialog
            //
            this.openFileDialog.FileName = "openFileDialog";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1596, 1027);
            this.Controls.Add(this.statusStrip_LoadedFile);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wow Developer Assistance";
            this.tabControl.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.ResumeLayout(false);
            this.tabPage_CreatureSpellsCreator.PerformLayout();
            this.toolStrip_CreatureSpellsCreator.ResumeLayout(false);
            this.toolStrip_CreatureSpellsCreator.PerformLayout();
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
    }
}

