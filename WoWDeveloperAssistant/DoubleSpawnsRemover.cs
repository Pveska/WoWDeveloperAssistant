using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant
{
    public static class DoubleSpawnsRemover
    {
        private enum ObjectTypes
        {
            Unknown    = 0,
            Creature   = 1,
            GameObject = 2
        };

        private enum LinkedIdType
        {
            Creature        = 1,
            CreatureAddon   = 2,
            GameObject      = 3,
            GameObjectAddon = 4
        }

        public static void RemoveDoubleSpawnsFromFile(string fileName, Label labelCreatures, Label labelGameobjects, bool creaturesRemover, bool gameobjectsRemover, ToolStripStatusLabel toolStripStatusLabel, MainForm mainForm)
        {
            StreamWriter outputFile = new StreamWriter(fileName + "_without_duplicates.sql");

            List<uint> creatureEntries = new List<uint>();
            List<uint> gameobjectEntries = new List<uint>();

            Dictionary<string, List<DataRow>> creaturesDataRowDictionary = new Dictionary<string, List<DataRow>>();
            Dictionary<string, List<DataRow>> gameobjectDataRowDictionary = new Dictionary<string, List<DataRow>>();

            DataRowCollection creaturesDataRowCollection;
            DataRowCollection gameobjectsDataRowCollection;

            var lines = File.ReadAllLines(fileName);
            List<string> outputLines = new List<string>();

            List<string> creatureAllowedLinkedIds = new List<string>();
            List<string> gameobjectAllowedLinkedIds = new List<string>();

            List<string> creatureAddonAllowedLinkedIds = new List<string>();
            List<string> gameobjectAddonAllowedLinkedIds = new List<string>();

            uint creaturesRemovedUsingLinkedIdCount = 0;
            uint creaturesRemovedUsingPositionCompareCount = 0;
            uint creatureAddonsRemoved = 0;

            uint gameobjectsRemovedUsingLinkedIdCount = 0;
            uint gameobjectsRemovedUsingPositionCompareCount = 0;
            uint gameobjectAddonsRemoved = 0;

            int linesCount = lines.Count();

            for (int i = 0; i < linesCount; i++)
            {
                if (IsCreatureInsertLine(lines[i]) && creaturesRemover)
                {
                    do
                    {
                        i++;

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsCreatureInsertLine(lines[i]))
                            continue;

                        if (IsCreatureAddonDeleteLine(lines[i]))
                            break;

                        uint entry = LineGetters.GetEntryFromLine(lines[i]);
                        if (entry == 0 || creatureEntries.Contains(entry))
                            continue;

                        creatureEntries.Add(entry);
                    }
                    while (!IsCreatureAddonDeleteLine(lines[i]));
                }
                else if (IsGameObjectInsertLine(lines[i]) && gameobjectsRemover)
                {
                    do
                    {
                        i++;

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsGameObjectInsertLine(lines[i]))
                            continue;

                        if (IsGameObjectAddonDeleteLine(lines[i]))
                            break;

                        uint entry = LineGetters.GetEntryFromLine(lines[i]);
                        if (entry == 0 || gameobjectEntries.Contains(entry))
                            continue;

                        gameobjectEntries.Add(entry);
                    }
                    while (!IsGameObjectAddonDeleteLine(lines[i]));
                }
            }

            if (creatureEntries.Count != 0)
            {
                creaturesDataRowCollection = GetDataRowCollectionFromQuery("SELECT `linked_id`, `id`, `position_x`, `position_y`, `position_z`, `map` FROM `creature` WHERE `id` IN (" + GetStringFromList(creatureEntries) + ")");

                foreach (DataRow row in creaturesDataRowCollection)
                {
                    if (!creaturesDataRowDictionary.ContainsKey(row[1].ToString()))
                    {
                        creaturesDataRowDictionary.Add(row[1].ToString(), new List<DataRow>());
                        creaturesDataRowDictionary[row[1].ToString()].Add(row);
                    }
                    else
                    {
                        creaturesDataRowDictionary[row[1].ToString()].Add(row);
                    }
                }
            }

            if (gameobjectEntries.Count != 0)
            {
                gameobjectsDataRowCollection = GetDataRowCollectionFromQuery("SELECT `linked_id`, `id`, `position_x`, `position_y`, `position_z`, `map` FROM `gameobject` WHERE `id` IN (" + GetStringFromList(gameobjectEntries) + ")");

                foreach (DataRow row in gameobjectsDataRowCollection)
                {
                    if (!gameobjectDataRowDictionary.ContainsKey(row[1].ToString()))
                    {
                        gameobjectDataRowDictionary.Add(row[1].ToString(), new List<DataRow>());
                        gameobjectDataRowDictionary[row[1].ToString()].Add(row);
                    }
                    else
                    {
                        gameobjectDataRowDictionary[row[1].ToString()].Add(row);
                    }
                }
            }

            for (int i = 0; i < linesCount; i++)
            {
                toolStripStatusLabel.Text = "Current line: " + i + "/ " + linesCount;
                mainForm.Update();

                if (IsCreatureInsertLine(lines[i]) && creaturesRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        toolStripStatusLabel.Text = "Current line: " + i + "/ " + linesCount;
                        mainForm.Update();

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsCreatureInsertLine(lines[i]))
                            continue;

                        if (IsCreatureAddonDeleteLine(lines[i]))
                            break;

                        string linkedId = LineGetters.GetLinkedIdFromLine(lines[i]);
                        uint entry = LineGetters.GetEntryFromLine(lines[i]);
                        Position spawnPos = GetPositionFromLine(lines[i]);
                        uint map = LineGetters.GetMapIdFromLine(lines[i]);
                        if (linkedId == "" || entry == 0 || !spawnPos.IsValid())
                            continue;

                        if (!creaturesDataRowDictionary.ContainsKey(entry.ToString()))
                        {
                            creatureAllowedLinkedIds.Add(linkedId);
                            outputLines.Add(lines[i]);
                            continue;
                        }

                        bool creatureFound = false;

                        foreach (DataRow row in creaturesDataRowDictionary[entry.ToString()])
                        {
                            if (Convert.ToUInt32(row[5]) == map)
                            {
                                if (linkedId == row[0].ToString())
                                {
                                    creaturesRemovedUsingLinkedIdCount++;
                                    creatureFound = true;
                                    break;
                                }

                                if (CoorsIsEqual(spawnPos.x, GetRoundedValueFromStringCoor(row[2].ToString())) &&
                                    CoorsIsEqual(spawnPos.y, GetRoundedValueFromStringCoor(row[3].ToString())) &&
                                    CoorsIsEqual(spawnPos.z, GetRoundedValueFromStringCoor(row[4].ToString())))
                                {
                                    creaturesRemovedUsingPositionCompareCount++;
                                    creatureFound = true;
                                    break;
                                }
                            }
                        }

                        if (!creatureFound)
                        {
                            creatureAllowedLinkedIds.Add(linkedId);
                            outputLines.Add(lines[i]);
                            continue;
                        }
                        else
                            continue;
                    }
                    while (!IsCreatureAddonDeleteLine(lines[i]));

                    i--;

                    if (creatureAllowedLinkedIds.Count > 0)
                    {
                        outputLines.Add("");
                    }
                }
                else if (IsCreatureAddonInsertLine(lines[i]) && creaturesRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        toolStripStatusLabel.Text = "Current line: " + i + "/ " + linesCount;
                        mainForm.Update();

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsCreatureAddonInsertLine(lines[i]))
                            continue;

                        if (IsGameObjectDeleteLine(lines[i]))
                            break;

                        string linkedId = LineGetters.GetLinkedIdFromLine(lines[i]);
                        if (linkedId == "")
                            continue;

                        if (!creatureAllowedLinkedIds.Contains(linkedId))
                        {
                            creatureAddonsRemoved++;
                            continue;
                        }

                        creatureAddonAllowedLinkedIds.Add(linkedId);
                        outputLines.Add(lines[i]);
                    }
                    while (!IsGameObjectDeleteLine(lines[i]));

                    i--;

                    if (creatureAddonAllowedLinkedIds.Count > 0)
                    {
                        outputLines.Add("");
                    }
                }
                else if (IsGameObjectInsertLine(lines[i]) && gameobjectsRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        toolStripStatusLabel.Text = "Current line: " + i + "/ " + linesCount;
                        mainForm.Update();

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsGameObjectInsertLine(lines[i]))
                            continue;

                        if (IsGameObjectAddonDeleteLine(lines[i]))
                            break;

                        string linkedId = LineGetters.GetLinkedIdFromLine(lines[i]);
                        uint entry = LineGetters.GetEntryFromLine(lines[i]);
                        Position spawnPos = GetPositionFromLine(lines[i]);
                        uint map = LineGetters.GetMapIdFromLine(lines[i]);
                        if (linkedId == "" || entry == 0 || !spawnPos.IsValid())
                            continue;

                        if (!gameobjectDataRowDictionary.ContainsKey(entry.ToString()))
                        {
                            gameobjectAllowedLinkedIds.Add(linkedId);
                            outputLines.Add(lines[i]);
                            continue;
                        }

                        bool gameobjectFound = false;

                        foreach (DataRow row in gameobjectDataRowDictionary[entry.ToString()])
                        {
                            if (Convert.ToUInt32(row[5]) == map)
                            {
                                if (linkedId == row[0].ToString())
                                {
                                    gameobjectsRemovedUsingLinkedIdCount++;
                                    gameobjectFound = true;
                                    break;
                                }

                                if (CoorsIsEqual(spawnPos.x, GetRoundedValueFromStringCoor(row[2].ToString())) &&
                                    CoorsIsEqual(spawnPos.y, GetRoundedValueFromStringCoor(row[3].ToString())) &&
                                    CoorsIsEqual(spawnPos.z, GetRoundedValueFromStringCoor(row[4].ToString())))
                                {
                                    gameobjectsRemovedUsingPositionCompareCount++;
                                    gameobjectFound = true;
                                    break;
                                }
                            }
                        }

                        if (!gameobjectFound)
                        {
                            gameobjectAllowedLinkedIds.Add(linkedId);
                            outputLines.Add(lines[i]);
                            continue;
                        }
                        else
                            continue;
                    }
                    while (!IsGameObjectAddonDeleteLine(lines[i]));

                    i--;

                    if (gameobjectAllowedLinkedIds.Count > 0)
                    {
                        outputLines.Add("");
                    }
                }
                else if (IsGameObjectAddonInsertLine(lines[i]) && gameobjectsRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        toolStripStatusLabel.Text = "Current line: " + i + "/ " + linesCount;
                        mainForm.Update();

                        if (lines[i].StartsWith("-- (") || lines[i] == "" || IsGameObjectAddonInsertLine(lines[i]))
                            continue;

                        if (!IsGameObjectAddonLine(lines[i]))
                            break;

                        string linkedId = LineGetters.GetLinkedIdFromLine(lines[i]);
                        if (linkedId == "")
                            continue;

                        if (!gameobjectAllowedLinkedIds.Contains(linkedId))
                        {
                            gameobjectAddonsRemoved++;
                            continue;
                        }

                        gameobjectAddonAllowedLinkedIds.Add(linkedId);
                        outputLines.Add(lines[i]);
                    }
                    while (IsGameObjectAddonLine(lines[i]));

                    i--;

                    if (gameobjectAddonAllowedLinkedIds.Count > 0)
                    {
                        outputLines.Add("");
                    }
                }
                else
                {
                    if (lines[i] == "")
                    {
                        if (outputLines.Count > 1 && outputLines[outputLines.Count - 1] != "")
                        {
                            outputLines.Add(lines[i]);
                        }
                    }
                    else
                    {
                        outputLines.Add(lines[i]);
                    }
                }
            }

            if (creatureAllowedLinkedIds.Count != 0)
            {
                outputLines[outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `creature`")))] = $"DELETE FROM `creature` WHERE `linked_id` IN ({GetStringFromList(creatureAllowedLinkedIds, true)});";
            }
            else
            {
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `creature`"))));
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("INSERT INTO `creature`"))));
            }

            if (creatureAddonAllowedLinkedIds.Count != 0)
            {
                outputLines[outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `creature_addon`")))] = $"DELETE FROM `creature_addon` WHERE `linked_id` IN ({GetStringFromList(creatureAddonAllowedLinkedIds, true)});";
            }
            else
            {
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `creature_addon`"))));
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("INSERT INTO `creature_addon`"))));
            }

            if (gameobjectAllowedLinkedIds.Count != 0)
            {
                outputLines[outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `gameobject`")))] = $"DELETE FROM `gameobject` WHERE `linked_id` IN ({GetStringFromList(gameobjectAllowedLinkedIds, true)});";
            }
            else
            {
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `gameobject`"))));
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("INSERT INTO `gameobject`"))));
            }

            if (gameobjectAddonAllowedLinkedIds.Count != 0)
            {
                outputLines[outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `gameobject_addon`")))] = $"DELETE FROM `gameobject_addon` WHERE `linked_id` IN ({GetStringFromList(gameobjectAddonAllowedLinkedIds, true)});";
            }
            else
            {
                outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("DELETE FROM `gameobject_addon`"))));

                if (outputLines.Contains("INSERT INTO `gameobject_addon`"))
                {
                    outputLines.RemoveAt(outputLines.IndexOf(outputLines.First(x => x.Contains("INSERT INTO `gameobject_addon`"))));
                }
            }

            labelCreatures.Text = " Creatures removed using LinkedId: " + creaturesRemovedUsingLinkedIdCount + ", using PositionCompare: " + creaturesRemovedUsingPositionCompareCount + ", Total removed count: " + (creaturesRemovedUsingLinkedIdCount + creaturesRemovedUsingPositionCompareCount) + ", Addons removed: " + creatureAddonsRemoved;
            labelCreatures.Show();

            labelGameobjects.Text = " GameObjects removed using LinkedId: " + gameobjectsRemovedUsingLinkedIdCount + ", using PositionCompare: " + gameobjectsRemovedUsingPositionCompareCount + ", Total removed count: " + (gameobjectsRemovedUsingLinkedIdCount + gameobjectsRemovedUsingPositionCompareCount) + ", Addons removed: " + gameobjectAddonsRemoved;
            labelGameobjects.Show();

            foreach (string line in outputLines)
                outputFile.WriteLine(line);

            outputFile.Close();
        }


        private static string GetStringFromList(List<string> list, bool linkedIdList = false)
        {
            string stringFromList = "";

            for (int i = 0; i < list.Count(); i++)
            {
                if (i + 1 < list.Count())
                {
                    stringFromList += linkedIdList ? "'" + list[i] + "', " : list[i] + ", ";
                }
                else
                {
                    stringFromList += linkedIdList ? "'" + list[i] + "'" : list[i];
                }
            }

            return stringFromList;
        }

        private static string GetStringFromList(List<uint> list, bool linkedIdList = false)
        {
            string stringFromList = "";

            for (int i = 0; i < list.Count(); i++)
            {
                if (i + 1 < list.Count())
                {
                    stringFromList += linkedIdList ? "'" + list[i].ToString() + "', " : list[i].ToString() + ", ";
                }
                else
                {
                    stringFromList += linkedIdList ? "'" + list[i].ToString() + "'" : list[i].ToString();
                }
            }

            return stringFromList;
        }

        private static bool IsGameObjectAddonLine(string line)
        {
            if (line.Contains("spell_target_position") || line.Contains("creature_model_info") || line.Contains("creature_template_addon"))
                return false;
            else
                return true;
        }

        private static bool CoorsIsEqual(float coorA, float coorB)
        {
            if (coorA == coorB || coorA + 1.0f == coorB || coorA - 1.0f == coorB ||
                coorA == coorB + 1.0f || coorA == coorB - 1.0f ||
                coorA + 1.0f == coorB + 1.0f || coorA - 1.0f == coorB - 1.0f)
            {
                return true;
            }
            else
                return false;
        }

        private static float GetRoundedValueFromStringCoor(string coor)
        {
            return float.Parse(Math.Round(float.Parse(coor.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat)).ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }

        private static Position GetPositionFromLine(string line)
        {
            Regex creatureLineRegex = new Regex(@".+, '0', 1, 0, 0, 0, {1}");
            Regex gameobjectLineRegex = new Regex(@".+, '0', 1, 0, {1}");

            if (creatureLineRegex.IsMatch(line))
            {
                string[] splittedLine = line.Replace(creatureLineRegex.Match(line).ToString(), "").Split(',');
                return new Position(GetRoundedValueFromStringCoor(splittedLine[0]), GetRoundedValueFromStringCoor(splittedLine[1]), GetRoundedValueFromStringCoor(splittedLine[2]));
            }
            else if (gameobjectLineRegex.IsMatch(line))
            {
                string[] splittedLine = line.Replace(gameobjectLineRegex.Match(line).ToString(), "").Split(',');
                return new Position(GetRoundedValueFromStringCoor(splittedLine[0]), GetRoundedValueFromStringCoor(splittedLine[1]), GetRoundedValueFromStringCoor(splittedLine[2]));
            }
            else
                return new Position();

        }

        private static DataRowCollection GetDataRowCollectionFromQuery (string query)
        {
            DataSet dataSet = SQLModule.WorldSelectQuery(query);

            if (dataSet != null && dataSet.Tables["table"].Rows.Count > 0)
            {
                return dataSet.Tables["table"].Rows;
            }
            else
                return null;
        }

        private static bool IsGameObjectDeleteLine(string line)
        {
            if (line.Contains("DELETE FROM `gameobject`"))
                return true;
            return false;
        }
        private static bool IsCreatureDeleteLine(string line)
        {
            if (line.Contains("DELETE FROM `creature`"))
                return true;
            return false;
        }

        private static bool IsGameObjectInsertLine(string line)
        {
            if (line.Contains("INSERT INTO `gameobject`"))
                return true;
            return false;
        }

        private static bool IsCreatureInsertLine(string line)
        {
            if (line.Contains("INSERT INTO `creature`"))
                return true;
            return false;
        }

        private static ObjectTypes GetObjectTypeFromLine(string line)
        {
            if (line.Contains("INSERT INTO `creature`") || line.Contains("DELETE FROM `creature`"))
                return ObjectTypes.Creature;
            if (line.Contains("INSERT INTO `gameobject`") || line.Contains("DELETE FROM `gameobject`"))
                return ObjectTypes.GameObject;
            return ObjectTypes.Unknown;
        }

        private static bool IsCreatureAddonDeleteLine(string line)
        {
            if (line.Contains("DELETE FROM `creature_addon`"))
                return true;

            return false;
        }

        private static bool IsCreatureAddonInsertLine(string line)
        {
            if (line.Contains("INSERT INTO `creature_addon`"))
                return true;

            return false;
        }

        private static bool IsGameObjectAddonDeleteLine(string line)
        {
            if (line.Contains("DELETE FROM `gameobject_addon`"))
                return true;

            return false;
        }

        private static bool IsGameObjectAddonInsertLine(string line)
        {
            if (line.Contains("INSERT INTO `gameobject_addon`"))
                return true;

            return false;
        }

        public static void OpenFileDialog(OpenFileDialog fileDialog)
        {
            fileDialog.Title = "Open File";
            fileDialog.Filter = "Parsed SQL file with spawns (*.sql)|*.sql";
            fileDialog.FileName = "";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowReadOnly = false;
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.FileName = " ";
        }
    }
}
