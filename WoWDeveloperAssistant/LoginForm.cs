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
            textBox_DB.Text = Properties.Settings.Default.WorldDatabase;
            textBox_Port.Text = Properties.Settings.Default.Port;
            textBox_HotfixDatabase.Text = Properties.Settings.Default.HotfixDatabase;
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            if (SQLModule.TryConnectToDB(textBox_Host.Text, textBox_Port.Text, textBox_UserName.Text, textBox_Password.Text, textBox_DB.Text))
            {
                if (checkBox_SaveValues.Checked == true)
                {
                    Properties.Settings.Default.Host = textBox_Host.Text;
                    Properties.Settings.Default.Username = textBox_UserName.Text;
                    Properties.Settings.Default.Password = textBox_Password.Text;
                    Properties.Settings.Default.WorldDatabase = textBox_DB.Text;
                    Properties.Settings.Default.Port = textBox_Port.Text;
                    Properties.Settings.Default.UsingDB = true;
                    Properties.Settings.Default.HotfixDatabase = textBox_HotfixDatabase.Text;
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
            Form MainForm = new MainForm();
            MainForm.Show();
            Hide();
        }
    }
}
