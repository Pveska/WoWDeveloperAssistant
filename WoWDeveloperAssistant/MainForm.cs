using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WoWDeveloperAssistant.Database_Advisor;
using WoWDeveloperAssistant.Waypoints_Creator;

namespace WoWDeveloperAssistant
{
    public partial class MainForm : Form
    {
        public bool importSuccessful = false;

        private CreatureScriptsCreator creatureScriptsCreator;
        private WaypointsCreator waypointsCreator;
        private static Dictionary<uint, string> creatureNamesDict;

        public MainForm()
        {
            InitializeComponent();

            creatureScriptsCreator = new CreatureScriptsCreator(this);
            waypointsCreator = new WaypointsCreator(this);

            if (Properties.Settings.Default.UsingDB == true)
            {
                creatureNamesDict = GetCreatureNamesFromDB();
            }
            else
            {
                checkBox_DatabaseConsidering.Enabled = false;
            }
        }

        private Dictionary<uint, string> GetCreatureNamesFromDB()
        {
            Dictionary<uint, string> namesDict = new Dictionary<uint, string>();

            DataSet creatureNameDs = new DataSet();
            string creatureNameQuery = "SELECT `entry`, `Name1` FROM `creature_template_wdb`;";
            creatureNameDs = (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery);

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    namesDict.Add((uint)row[0], row[1].ToString());
                }
            }

            return namesDict;
        }

        public static string GetCreatureNameByEntry(uint creatureEntry)
        {
            if (creatureNamesDict.ContainsKey(creatureEntry))
                return creatureNamesDict[creatureEntry];

            return "Unknown";
        }

        private bool IsTxtFileValidForParse(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                return true;
            }
            else
            {
                MessageBox.Show(fileName + " is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return false;
        }

        private void createSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Spells.Rows.Count > 0)
            {
                creatureScriptsCreator.FillSQLOutput();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_Spells.SelectedRows)
            {
                dataGridView_Spells.Rows.Remove(row);
            }
        }

        private void toolStripButton_ImportSniff_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                creatureScriptsCreator.ImportStarted();

                if (IsTxtFileValidForParse(openFileDialog.FileName) && 
                    creatureScriptsCreator.GetDataFromSniffFile(openFileDialog.FileName))
                {
                    creatureScriptsCreator.ImportSuccessful();
                }
                else
                {
                    toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                    toolStripButton_CSC_ImportSniff.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                return;
            }
        }

        private void toolStripButton_Search_Click(object sender, EventArgs e)
        {
            creatureScriptsCreator.FillListBoxWithGuids();
        }

        private void listBox_CreatureGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            creatureScriptsCreator.FillSpellsGrid();
        }

        private void textBox_CreatureFlags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreatureFlagsAdvisor.GetCreatureFlags(textBox_CreatureFlags.Text);
            }
        }

        private void textBox_QuestFlags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuestFlagsAdvisor.GetQuestFlags(textBox_QuestFlags.Text);
            }
        }

        private void button_ImportFile_Click(object sender, EventArgs e)
        {
            DoubleSpawnsRemover.OpenFileDialog(openFileDialog);

            this.Cursor = Cursors.WaitCursor;
            button_ImportFileForRemoving.Enabled = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoubleSpawnsRemover.RemoveDoubleSpawnsFromFile(openFileDialog.FileName, label_CreaturesRemoved, label_GameobjectsRemoved, checkBox_CreaturesRemover.Checked, checkBox_GameobjectsRemover.Checked, checkBox_DatabaseConsidering.Checked);
                button_ImportFileForRemoving.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            else
            {
                label_CreaturesRemoved.Text = "No creatures removed";
                label_GameobjectsRemoved.Text = "No gameobjects removed";
            }

            button_ImportFileForRemoving.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void checkBox_CreaturesRemover_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_CreaturesRemover.Checked)
            {
                button_ImportFileForRemoving.Enabled = true;
            }
            else if (!checkBox_CreaturesRemover.Checked && !checkBox_GameobjectsRemover.Checked)
            {
                button_ImportFileForRemoving.Enabled = false;
            }
        }

        private void checkBox_GameobjectsRemover_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_GameobjectsRemover.Checked)
            {
                button_ImportFileForRemoving.Enabled = true;
            }
            else if (!checkBox_GameobjectsRemover.Checked && !checkBox_CreaturesRemover.Checked)
            {
                button_ImportFileForRemoving.Enabled = false;
            }
        }

        private void textBoxAreatriggerSplines_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AreatriggerSplineCreator.OpenFileDialog(openFileDialog);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AreatriggerSplineCreator.ParseSplinesForAreatrigger(openFileDialog.FileName, textBoxAreatriggerSplines.Text);
                }
            }
        }

        private void toolStripButton_WCLoadSniff_Click(object sender, EventArgs e)
        {
            waypointsCreator.OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                waypointsCreator.ImportStarted();

                if (IsTxtFileValidForParse(openFileDialog.FileName) &&
                    waypointsCreator.GetDataFromSniffFile(openFileDialog.FileName))
                {
                    waypointsCreator.ImportSuccessful();
                }
                else
                {
                    toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                    toolStripButton_CSC_ImportSniff.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                return;
            }
        }

        private void toolStripButton_WCSettings_Click(object sender, EventArgs e)
        {
            Form settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void toolStripButton_WCSearch_Click(object sender, EventArgs e)
        {
            waypointsCreator.FillListBoxWithGuids();
        }

        private void listBox_WCCreatureGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            waypointsCreator.FillWaypointsGrid();
        }

        private void createSQLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            waypointsCreator.CreateSQL();
        }

        private void removeExcessPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveNearestPoints();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            waypointsCreator.CutFromGrid();
        }

        private void removeDuplicatePointsToolStripMenuItem_WC_Click(object sender, EventArgs e)
        {
            waypointsCreator.RemoveDuplicatePoints();
        }

        private void createReturnPathToolStripMenuItem_WC_Click(object sender, EventArgs e)
        {
            waypointsCreator.CreateReturnPath();
        }
    }
}
