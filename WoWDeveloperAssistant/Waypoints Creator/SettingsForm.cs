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
            checkBox_CreateVector.Checked = Properties.Settings.Default.Vector;
            checkBox_ParseWaypointScripts.Checked = Properties.Settings.Default.Scripts;
            checkBox_DoNotAddCritterGuids.Checked = Properties.Settings.Default.Critters;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Vector = checkBox_CreateVector.Checked;
            Properties.Settings.Default.Scripts = checkBox_ParseWaypointScripts.Checked;
            Properties.Settings.Default.Critters = checkBox_DoNotAddCritterGuids.Checked;
        }
    }
}
