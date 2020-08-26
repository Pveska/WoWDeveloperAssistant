using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Conditions_Creator
{
    public class ConditionsCreator
    {
        private MainForm mainForm;
        List<string> existedConditionsInDbList = new List<string>();
        List<string> createdConditionsList = new List<string>();

        public ConditionsCreator(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void ChangeTextBoxAccessibility(string sourceOrCondition, TextBox textBox)
        {
            bool isTextBoxAccessible = Conditions.textBoxAccessibilityDictionary[sourceOrCondition][textBox.Name];
            textBox.Enabled = isTextBoxAccessible;

            if (!isTextBoxAccessible)
            {
                textBox.Text = "";
            }
        }

        public void CreateCondition()
        {
            Conditions.Condition condition = new Conditions.Condition(mainForm);

            CheckIfConditionAlreadyExistedInDb();

            if (createdConditionsList.Count == 0)
            {
                string deleteConditionQuery = "DELETE FROM `conditions` WHERE `SourceTypeOrReferenceId` = " + condition.sourceType + " AND ";
                deleteConditionQuery += condition.sourceGroup != "0" ? "`SourceGroup` = " + condition.sourceGroup : "`SourceEntry` = " + condition.sourceEntry;
                deleteConditionQuery += ";";

                createdConditionsList.Add(deleteConditionQuery);
                createdConditionsList.Add("INSERT INTO `conditions` (`SourceTypeOrReferenceId`, `SourceGroup`, `SourceEntry`, `SourceId`, `ElseGroup`, `ConditionTypeOrReference`, `ConditionTarget`, `ConditionValue1`, `ConditionValue2`, `ConditionValue3`, `NegativeCondition`, `ScriptName`, `Comment`) VALUES");
            }

            if (condition.scriptName == "")
            {
                string conditionString = "(" + condition.sourceType + ", " + condition.sourceGroup + ", " + condition.sourceEntry + ", " + condition.sourceId + ", " + condition.elseGroup + ", " + condition.conditionType + ", " + condition.conditionTarget + ", " + condition.conditionValue1 + ", " + condition.conditionValue2 + ", " + condition.conditionValue3 + ", " + condition.negativeCondition + ", " + "\"\"" + ", " + GetCommentForCondition(condition) + ")";
                
                if (!createdConditionsList.Contains(conditionString))
                {
                    createdConditionsList.Add(conditionString);
                }
            }
            else
            {
                string conditionString = "(" + condition.sourceType + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + 0 + ", " + "\"" + condition.scriptName + "\"" + ", " + GetCommentForCondition(condition) + ")";

                if (!createdConditionsList.Contains(conditionString))
                {
                    createdConditionsList.Add(conditionString);
                }
            }

            UpdateOutputTexbox();
        }

        private void CheckIfConditionAlreadyExistedInDb()
        {
            uint sourceType = (uint)Enum.Parse(typeof(Conditions.ConditionSourceTypes), mainForm.comboBox_ConditionSourceType.SelectedItem.ToString());
            string sourceGroup = mainForm.textBox_SourceGroup.Text;
            string sourceEntry = mainForm.textBox_SourceEntry.Text;

            string checkConditionInDbQuery = "SELECT * FROM `conditions` WHERE `SourceTypeOrReferenceId` = " + sourceType + " AND ";
            checkConditionInDbQuery += sourceGroup != "" ? "`SourceGroup` = " + sourceGroup : "`SourceEntry` = " + sourceEntry;
            checkConditionInDbQuery += ";";

            var conditionsDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(checkConditionInDbQuery) : null;

            if (conditionsDs != null && conditionsDs.Tables["table"].Rows.Count != 0)
            {
                if (existedConditionsInDbList.Count == 0)
                {
                    existedConditionsInDbList.Add("Conditions that already existed in database with the same Source Type and Group or Entry:");
                }

                mainForm.textBox_ConditionsOutput.Text = "Conditions that already existed in database with the same Source Type and Group or Entry:";

                foreach (DataRow row in conditionsDs.Tables["table"].Rows)
                {
                    string conditionString = "";

                    for (uint i = 0; i < row.ItemArray.Length; i++)
                    {
                        if (i + 1 < row.ItemArray.Length)
                        {
                            if (row.ItemArray[i].ToString() != "")
                            {
                                conditionString += row.ItemArray[i].ToString() + ", ";
                            }
                            else
                            {
                                conditionString += "\"\"" + ", ";
                            }
                        }
                        else
                        {
                            if (row.ItemArray[i].ToString() != "")
                            {
                                conditionString += row.ItemArray[i].ToString() + ")";
                            }
                            else
                            {
                                conditionString += "\"\"" + ")";
                            }
                        }
                    }

                    if (!existedConditionsInDbList.Contains(conditionString))
                    {
                        existedConditionsInDbList.Add(conditionString);
                    }
                }
            }
        }

        private void UpdateOutputTexbox()
        {
            mainForm.textBox_ConditionsOutput.Clear();

            if (existedConditionsInDbList.Count != 0)
            {
                foreach (string condition in existedConditionsInDbList)
                {
                    mainForm.textBox_ConditionsOutput.Text += condition + "\r\n";
                }

                mainForm.textBox_ConditionsOutput.Text += "\r\n";
            }

            if (createdConditionsList.Count != 0)
            {
                for (int i = 0; i < createdConditionsList.Count; i++)
                {
                    if (i > 1)
                    {
                        if (i + 1 < createdConditionsList.Count)
                        {
                            mainForm.textBox_ConditionsOutput.Text += createdConditionsList[i] + "," + "\r\n";
                        }
                        else
                        {
                            mainForm.textBox_ConditionsOutput.Text += createdConditionsList[i] + ";";
                        }
                    }
                    else
                    {
                        mainForm.textBox_ConditionsOutput.Text += createdConditionsList[i] + "\r\n";
                    }
                }
            }
        }

        public void ClearConditions()
        {
            existedConditionsInDbList.Clear();
            createdConditionsList.Clear();
        }

        private string GetCommentForCondition(Conditions.Condition condition)
        {
            string comment = "\"";

            comment += Conditions.sourceTypeCommentsDictionary[(Conditions.ConditionSourceTypes)condition.sourceType];
            comment = comment.Replace("@sourceGroup", condition.sourceGroup).Replace("@sourceEntry", condition.sourceEntry);

            string targetLine = comment.Split(' ').Where(x => x.Contains("/")).FirstOrDefault();
            if (targetLine != null)
            {
                string target = targetLine.Split('/')[Convert.ToInt32(condition.conditionTarget)].ToString().Replace("@", "");
                comment = comment.Replace(targetLine, target);
            }

            if (condition.negativeCondition == "1")
            {
                comment += " not ";
            }
            else
            {
                comment += " ";
            }

            if (condition.scriptName == "")
            {
                comment += Conditions.conditionTypeCommentsDictionary[(Conditions.ConditionTypes)condition.conditionType];
                comment = comment.Replace("@conditionValue1", condition.conditionValue1).Replace("@conditionValue2", condition.conditionValue2).Replace("@conditionValue3", condition.conditionValue3);
            }
            else
            {
                comment += "satisfies script";
            }

            comment += "\"";

            return comment;
        }
    }
}
