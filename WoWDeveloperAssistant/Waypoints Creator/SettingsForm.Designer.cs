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
            this.checkBox_WC_Vector = new System.Windows.Forms.CheckBox();
            this.checkBox_WC_Scripts = new System.Windows.Forms.CheckBox();
            this.button_WC_Ok = new System.Windows.Forms.Button();
            this.button_WC_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_WC_Vector
            // 
            this.checkBox_WC_Vector.AutoSize = true;
            this.checkBox_WC_Vector.Checked = true;
            this.checkBox_WC_Vector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_WC_Vector.Location = new System.Drawing.Point(12, 12);
            this.checkBox_WC_Vector.Name = "checkBox_WC_Vector";
            this.checkBox_WC_Vector.Size = new System.Drawing.Size(129, 24);
            this.checkBox_WC_Vector.TabIndex = 0;
            this.checkBox_WC_Vector.Text = "G3D Vector3";
            this.checkBox_WC_Vector.UseVisualStyleBackColor = true;
            // 
            // checkBox_WC_Scripts
            // 
            this.checkBox_WC_Scripts.AutoSize = true;
            this.checkBox_WC_Scripts.Checked = true;
            this.checkBox_WC_Scripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_WC_Scripts.Location = new System.Drawing.Point(147, 12);
            this.checkBox_WC_Scripts.Name = "checkBox_WC_Scripts";
            this.checkBox_WC_Scripts.Size = new System.Drawing.Size(154, 24);
            this.checkBox_WC_Scripts.TabIndex = 1;
            this.checkBox_WC_Scripts.Text = "Waypoint Scripts";
            this.checkBox_WC_Scripts.UseVisualStyleBackColor = true;
            // 
            // button_WC_Ok
            // 
            this.button_WC_Ok.Location = new System.Drawing.Point(12, 60);
            this.button_WC_Ok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_WC_Ok.Name = "button_WC_Ok";
            this.button_WC_Ok.Size = new System.Drawing.Size(123, 37);
            this.button_WC_Ok.TabIndex = 22;
            this.button_WC_Ok.Text = "OK";
            this.button_WC_Ok.UseVisualStyleBackColor = true;
            this.button_WC_Ok.Click += new System.EventHandler(this.button_WC_Ok_Click);
            // 
            // button_WC_Cancel
            // 
            this.button_WC_Cancel.Location = new System.Drawing.Point(165, 60);
            this.button_WC_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_WC_Cancel.Name = "button_WC_Cancel";
            this.button_WC_Cancel.Size = new System.Drawing.Size(123, 35);
            this.button_WC_Cancel.TabIndex = 23;
            this.button_WC_Cancel.Text = "Cancel";
            this.button_WC_Cancel.UseVisualStyleBackColor = true;
            this.button_WC_Cancel.Click += new System.EventHandler(this.button_WC_Cancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(304, 101);
            this.ControlBox = false;
            this.Controls.Add(this.button_WC_Cancel);
            this.Controls.Add(this.button_WC_Ok);
            this.Controls.Add(this.checkBox_WC_Scripts);
            this.Controls.Add(this.checkBox_WC_Vector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_WC_Vector;
        private System.Windows.Forms.CheckBox checkBox_WC_Scripts;
        internal System.Windows.Forms.Button button_WC_Ok;
        internal System.Windows.Forms.Button button_WC_Cancel;
    }
}