namespace WoWDeveloperAssistant
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label_Host = new System.Windows.Forms.Label();
            this.textBox_Host = new System.Windows.Forms.TextBox();
            this.label_UserName = new System.Windows.Forms.Label();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.label_Password = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.label_DB = new System.Windows.Forms.Label();
            this.textBox_DB = new System.Windows.Forms.TextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.checkBox_SaveValues = new System.Windows.Forms.CheckBox();
            this.button_Login = new System.Windows.Forms.Button();
            this.button_CancelLogin = new System.Windows.Forms.Button();
            this.label_HotfixDatabase = new System.Windows.Forms.Label();
            this.textBox_HotfixDatabase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Host
            // 
            this.label_Host.AutoSize = true;
            this.label_Host.Location = new System.Drawing.Point(119, 6);
            this.label_Host.Name = "label_Host";
            this.label_Host.Size = new System.Drawing.Size(43, 20);
            this.label_Host.TabIndex = 0;
            this.label_Host.Text = "Host";
            this.label_Host.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Host
            // 
            this.textBox_Host.Location = new System.Drawing.Point(45, 32);
            this.textBox_Host.Name = "textBox_Host";
            this.textBox_Host.Size = new System.Drawing.Size(190, 26);
            this.textBox_Host.TabIndex = 1;
            this.textBox_Host.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_UserName
            // 
            this.label_UserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(99, 65);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(83, 20);
            this.label_UserName.TabIndex = 2;
            this.label_UserName.Text = "Username";
            this.label_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Location = new System.Drawing.Point(45, 92);
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(190, 26);
            this.textBox_UserName.TabIndex = 3;
            this.textBox_UserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_Password
            // 
            this.label_Password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(101, 123);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(78, 20);
            this.label_Password.TabIndex = 4;
            this.label_Password.Text = "Password";
            this.label_Password.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(45, 148);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(190, 26);
            this.textBox_Password.TabIndex = 5;
            this.textBox_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_DB
            // 
            this.label_DB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_DB.AutoSize = true;
            this.label_DB.Location = new System.Drawing.Point(78, 179);
            this.label_DB.Name = "label_DB";
            this.label_DB.Size = new System.Drawing.Size(124, 20);
            this.label_DB.TabIndex = 6;
            this.label_DB.Text = "World Database";
            this.label_DB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_DB
            // 
            this.textBox_DB.Location = new System.Drawing.Point(45, 204);
            this.textBox_DB.Name = "textBox_DB";
            this.textBox_DB.Size = new System.Drawing.Size(190, 26);
            this.textBox_DB.TabIndex = 7;
            this.textBox_DB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_Port
            // 
            this.label_Port.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(121, 297);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(38, 20);
            this.label_Port.TabIndex = 8;
            this.label_Port.Text = "Port";
            this.label_Port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(45, 324);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(190, 26);
            this.textBox_Port.TabIndex = 9;
            this.textBox_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_SaveValues
            // 
            this.checkBox_SaveValues.AutoSize = true;
            this.checkBox_SaveValues.Checked = true;
            this.checkBox_SaveValues.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SaveValues.Location = new System.Drawing.Point(72, 358);
            this.checkBox_SaveValues.Name = "checkBox_SaveValues";
            this.checkBox_SaveValues.Size = new System.Drawing.Size(124, 24);
            this.checkBox_SaveValues.TabIndex = 10;
            this.checkBox_SaveValues.Text = "Save Values";
            this.checkBox_SaveValues.UseVisualStyleBackColor = true;
            // 
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(12, 388);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(110, 40);
            this.button_Login.TabIndex = 11;
            this.button_Login.Text = "Login";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // button_CancelLogin
            // 
            this.button_CancelLogin.Location = new System.Drawing.Point(152, 388);
            this.button_CancelLogin.Name = "button_CancelLogin";
            this.button_CancelLogin.Size = new System.Drawing.Size(110, 40);
            this.button_CancelLogin.TabIndex = 12;
            this.button_CancelLogin.Text = "Cancel";
            this.button_CancelLogin.UseVisualStyleBackColor = true;
            this.button_CancelLogin.Click += new System.EventHandler(this.button_CancelLogin_Click);
            // 
            // label_HotfixDatabase
            // 
            this.label_HotfixDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_HotfixDatabase.AutoSize = true;
            this.label_HotfixDatabase.Location = new System.Drawing.Point(78, 237);
            this.label_HotfixDatabase.Name = "label_HotfixDatabase";
            this.label_HotfixDatabase.Size = new System.Drawing.Size(124, 20);
            this.label_HotfixDatabase.TabIndex = 13;
            this.label_HotfixDatabase.Text = "Hotfix Database";
            this.label_HotfixDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_HotfixDatabase
            // 
            this.textBox_HotfixDatabase.Location = new System.Drawing.Point(45, 264);
            this.textBox_HotfixDatabase.Name = "textBox_HotfixDatabase";
            this.textBox_HotfixDatabase.Size = new System.Drawing.Size(190, 26);
            this.textBox_HotfixDatabase.TabIndex = 14;
            this.textBox_HotfixDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(274, 440);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_HotfixDatabase);
            this.Controls.Add(this.label_HotfixDatabase);
            this.Controls.Add(this.button_CancelLogin);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.checkBox_SaveValues);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.textBox_DB);
            this.Controls.Add(this.label_DB);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.textBox_UserName);
            this.Controls.Add(this.label_UserName);
            this.Controls.Add(this.textBox_Host);
            this.Controls.Add(this.label_Host);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to DB";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Host;
        private System.Windows.Forms.TextBox textBox_Host;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label_DB;
        private System.Windows.Forms.TextBox textBox_DB;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.CheckBox checkBox_SaveValues;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Button button_CancelLogin;
        private System.Windows.Forms.Label label_HotfixDatabase;
        private System.Windows.Forms.TextBox textBox_HotfixDatabase;
    }
}