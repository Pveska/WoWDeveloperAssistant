using System;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBox_WC_Vector.Checked = Properties.Settings.Default.Vector;
            checkBox_WC_Scripts.Checked = Properties.Settings.Default.Scripts;
        }

        private void button_WC_Ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Vector = checkBox_WC_Vector.Checked;
            Properties.Settings.Default.Scripts = checkBox_WC_Scripts.Checked;
            Close();
        }

        private void button_WC_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
