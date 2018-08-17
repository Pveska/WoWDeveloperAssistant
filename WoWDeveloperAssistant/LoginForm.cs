using System;
using System.Windows.Forms;

namespace WoWDeveloperAssistant
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox_Host.Text = Properties.Settings.Default.Host;
            textBox_UserName.Text = Properties.Settings.Default.Username;
            textBox_Password.Text = Properties.Settings.Default.Password;
            textBox_DB.Text = Properties.Settings.Default.Database;
            textBox_Port.Text = Properties.Settings.Default.Port;
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            if (SQLModule.TryConnectToDB())
            {
                if (checkBox_SaveValues.Checked == true)
                {
                    Properties.Settings.Default.Host = textBox_Host.Text;
                    Properties.Settings.Default.Username = textBox_UserName.Text;
                    Properties.Settings.Default.Password = textBox_Password.Text;
                    Properties.Settings.Default.Database = textBox_DB.Text;
                    Properties.Settings.Default.Port = textBox_Port.Text;
                    Properties.Settings.Default.UsingDB = true;
                    Properties.Settings.Default.Save();
                }

                LoadMain();
            }
        }

        private void button_CancelLogin_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UsingDB = false;
            Properties.Settings.Default.Save();
            LoadMain();
        }

        private void LoadMain()
        {
            System.Windows.Forms.Form MainForm = new MainForm();
            MainForm.Show();
            this.Hide();
        }
    }
}
