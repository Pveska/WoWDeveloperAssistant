namespace WoWDeveloperAssistant.Waypoints_Creator
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.checkBox_CreateVector = new System.Windows.Forms.CheckBox();
            this.checkBox_ParseWaypointScripts = new System.Windows.Forms.CheckBox();
            this.checkBox_DoNotAddCritterGuids = new System.Windows.Forms.CheckBox();
            this.checkBox_CheckExistedDataOnDb = new System.Windows.Forms.CheckBox();
            this.checkBox_SkipCombatMovement = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_CreateVector
            // 
            this.checkBox_CreateVector.AutoSize = true;
            this.checkBox_CreateVector.Checked = true;
            this.checkBox_CreateVector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_CreateVector.Location = new System.Drawing.Point(12, 12);
            this.checkBox_CreateVector.Name = "checkBox_CreateVector";
            this.checkBox_CreateVector.Size = new System.Drawing.Size(181, 24);
            this.checkBox_CreateVector.TabIndex = 0;
            this.checkBox_CreateVector.Text = "Create G3D Vector3";
            this.checkBox_CreateVector.UseVisualStyleBackColor = true;
            // 
            // checkBox_ParseWaypointScripts
            // 
            this.checkBox_ParseWaypointScripts.AutoSize = true;
            this.checkBox_ParseWaypointScripts.Checked = true;
            this.checkBox_ParseWaypointScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ParseWaypointScripts.Location = new System.Drawing.Point(13, 42);
            this.checkBox_ParseWaypointScripts.Name = "checkBox_ParseWaypointScripts";
            this.checkBox_ParseWaypointScripts.Size = new System.Drawing.Size(199, 24);
            this.checkBox_ParseWaypointScripts.TabIndex = 1;
            this.checkBox_ParseWaypointScripts.Text = "Parse Waypoint Scripts";
            this.checkBox_ParseWaypointScripts.UseVisualStyleBackColor = true;
            // 
            // checkBox_DoNotAddCritterGuids
            // 
            this.checkBox_DoNotAddCritterGuids.AutoSize = true;
            this.checkBox_DoNotAddCritterGuids.Checked = true;
            this.checkBox_DoNotAddCritterGuids.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DoNotAddCritterGuids.Location = new System.Drawing.Point(13, 72);
            this.checkBox_DoNotAddCritterGuids.Name = "checkBox_DoNotAddCritterGuids";
            this.checkBox_DoNotAddCritterGuids.Size = new System.Drawing.Size(200, 24);
            this.checkBox_DoNotAddCritterGuids.TabIndex = 24;
            this.checkBox_DoNotAddCritterGuids.Text = "Do not add critter guids";
            this.checkBox_DoNotAddCritterGuids.UseVisualStyleBackColor = true;
            // 
            // checkBox_CheckExistedDataOnDb
            // 
            this.checkBox_CheckExistedDataOnDb.AutoSize = true;
            this.checkBox_CheckExistedDataOnDb.Checked = true;
            this.checkBox_CheckExistedDataOnDb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_CheckExistedDataOnDb.Location = new System.Drawing.Point(13, 102);
            this.checkBox_CheckExistedDataOnDb.Name = "checkBox_CheckExistedDataOnDb";
            this.checkBox_CheckExistedDataOnDb.Size = new System.Drawing.Size(284, 24);
            this.checkBox_CheckExistedDataOnDb.TabIndex = 25;
            this.checkBox_CheckExistedDataOnDb.Text = "Check if creature already have data";
            this.checkBox_CheckExistedDataOnDb.UseVisualStyleBackColor = true;
            // 
            // checkBox_SkipCombatMovement
            // 
            this.checkBox_SkipCombatMovement.AutoSize = true;
            this.checkBox_SkipCombatMovement.Checked = true;
            this.checkBox_SkipCombatMovement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SkipCombatMovement.Location = new System.Drawing.Point(13, 132);
            this.checkBox_SkipCombatMovement.Name = "checkBox_SkipCombatMovement";
            this.checkBox_SkipCombatMovement.Size = new System.Drawing.Size(201, 24);
            this.checkBox_SkipCombatMovement.TabIndex = 26;
            this.checkBox_SkipCombatMovement.Text = "Skip combat movement";
            this.checkBox_SkipCombatMovement.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(300, 165);
            this.Controls.Add(this.checkBox_SkipCombatMovement);
            this.Controls.Add(this.checkBox_CheckExistedDataOnDb);
            this.Controls.Add(this.checkBox_DoNotAddCritterGuids);
            this.Controls.Add(this.checkBox_ParseWaypointScripts);
            this.Controls.Add(this.checkBox_CreateVector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_CreateVector;
        private System.Windows.Forms.CheckBox checkBox_ParseWaypointScripts;
        private System.Windows.Forms.CheckBox checkBox_DoNotAddCritterGuids;
        private System.Windows.Forms.CheckBox checkBox_CheckExistedDataOnDb;
        private System.Windows.Forms.CheckBox checkBox_SkipCombatMovement;
    }
}