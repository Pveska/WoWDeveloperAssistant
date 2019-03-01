using System;
using System.Data;
using System.Windows.Forms;
using WoWDeveloperAssistant.Database_Advisor;

namespace WoWDeveloperAssistant
{
    public partial class MainForm : Form
    {
        public bool importSuccessful = false;

        private CreatureScriptsCreator CreatureScriptsCreator;

        public MainForm()
        {
            InitializeComponent();

            CreatureScriptsCreator = new CreatureScriptsCreator(this);

            if (Properties.Settings.Default.UsingDB != true)
            {
                checkBox_DatabaseConsidering.Enabled = false;
            }
        }

        private void createSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Spells.Rows.Count > 0)
            {
                CreatureScriptsCreator.FillSQLOutput();
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
            OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportStarted();

                CreatureScriptsCreator.LoadSniffFile(openFileDialog.FileName);

                if (importSuccessful)
                {
                    ImportSuccessful();
                }
                else
                {
                    toolStripStatusLabel_FileStatus.Text = "No File Loaded";
                    toolStripButton_ImportSniff.Enabled = true;
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
            CreatureScriptsCreator.FillListBoxWithGuids();
        }

        private void listBox_CreatureGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatureScriptsCreator.FillSpellsGrid();
        }

        private void ImportStarted()
        {
            this.Cursor = Cursors.WaitCursor;
            toolStripButton_ImportSniff.Enabled = false;
            toolStripButton_Search.Enabled = false;
            toolStripTextBox_CreatureEntry.Enabled = false;
            listBox_CreatureGuids.Enabled = false;
            listBox_CreatureGuids.Items.Clear();
            listBox_CreatureGuids.DataSource = null;
            dataGridView_Spells.Enabled = false;
            dataGridView_Spells.Rows.Clear();
            toolStripStatusLabel_FileStatus.Text = "Loading File...";
        }

        private void ImportSuccessful()
        {
            toolStripButton_ImportSniff.Enabled = true;
            toolStripButton_Search.Enabled = true;
            toolStripTextBox_CreatureEntry.Enabled = true;
            toolStripStatusLabel_FileStatus.Text = openFileDialog.FileName + " is selected for input.";
            this.Cursor = Cursors.Default;
        }

        private void OpenFileDialog()
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            openFileDialog.FileName = "*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
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
            OpenFileDialogForRemoving();

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

        private void OpenFileDialogForRemoving()
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "SQL File (*.sql)|*.sql";
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
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
                OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AreatriggerSplineCreator.ParseSplinesForAreatrigger(openFileDialog.FileName, textBoxAreatriggerSplines.Text);
                }
            }
        }
    }
}
