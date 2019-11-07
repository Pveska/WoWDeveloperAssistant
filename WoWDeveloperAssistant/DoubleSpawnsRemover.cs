using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        public static void RemoveDoubleSpawnsFromFile(string fileName, Label labelCreatures, Label labelGameobjects, bool creaturesRemover, bool gameobjectsRemover, bool consideringDB)
        {
            StreamWriter outputFile = new StreamWriter(fileName + "_without_duplicates.sql");

            List<string> allowedCreatureLinkedIds = new List<string>();
            List<string> allowedGameobjectLinkedIds = new List<string>();

            var lines = File.ReadAllLines(fileName);
            List<string> outputLines = new List<string>();

            uint creatureRowsRemoved = 0;
            uint gameobjectRowsRemoved = 0;
            uint dbCreatureRowsRemoved = 0;
            uint dbGameobjectRowsRemoved = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                if (IsInsertLine(lines[i]))
                {
                    ObjectTypes type = GetObjectTypeFromLine(lines[i]);

                    do
                    {
                        i++;

                        if (IsCreatureAddonDeleteLine(lines[i]) || IsGameObjectAddonDeleteLine(lines[i]))
                        {
                            i--;
                            break;
                        }

                        if (lines[i].StartsWith("-- (") || lines[i] == "")
                            continue;

                        string linkedId = GetLinkedIdFromLine(lines[i]);
                        uint entry = GetEntryFromLine(lines[i]);
                        if (linkedId == "" || entry == 0)
                            continue;

                        switch (type)
                        {
                            case ObjectTypes.Creature:
                            {
                                if (creaturesRemover)
                                {
                                    if (!allowedCreatureLinkedIds.Contains(linkedId))
                                    {
                                        allowedCreatureLinkedIds.Add(linkedId);
                                    }
                                    else
                                    {
                                        creatureRowsRemoved++;
                                    }
                                }

                                break;
                            }
                            case ObjectTypes.GameObject:
                            {
                                if (gameobjectsRemover)
                                {
                                    if (!allowedGameobjectLinkedIds.Contains(linkedId))
                                    {
                                        allowedGameobjectLinkedIds.Add(linkedId);
                                    }
                                    else
                                    {
                                        gameobjectRowsRemoved++;
                                    }
                                }

                                break;
                            }
                        }
                    }
                    while (lines[i] != "" && GetLinkedIdFromLine(lines[i]) != "" && !IsCreatureAddonDeleteLine(lines[i]) && !IsGameObjectAddonDeleteLine(lines[i]));
                }
            }

            var creatureLinkedIds = new List<string>(allowedCreatureLinkedIds);
            var gameobjectLinkedIds = new List<string>(allowedGameobjectLinkedIds);

            if (consideringDB)
            {
                if (creaturesRemover)
                {
                    foreach (var linkedId in allowedCreatureLinkedIds)
                    {
                        string creaturesQuery = "SELECT `linked_id` FROM `creature` WHERE `linked_id` = '" + linkedId + "'";

                        var creaturesDs = SQLModule.DatabaseSelectQuery(creaturesQuery);

                        if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                            {
                                string dbLinkedId = row[0].ToString();

                                if (creatureLinkedIds.Contains(dbLinkedId))
                                {
                                    creatureLinkedIds.Remove(dbLinkedId);
                                    dbCreatureRowsRemoved++;
                                }
                            }
                        }
                    }
                }

                if (gameobjectsRemover)
                {
                    foreach (var linkedId in allowedGameobjectLinkedIds)
                    {
                        string gameobjectsQuery = "SELECT `linked_id` FROM `gameobject` WHERE `linked_id` = '" + linkedId + "'";

                        var gameobjectsDs = SQLModule.DatabaseSelectQuery(gameobjectsQuery);

                        if (gameobjectsDs != null && gameobjectsDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in gameobjectsDs.Tables["table"].Rows)
                            {
                                string dbLinkedId = row[0].ToString();

                                if (gameobjectLinkedIds.Contains(dbLinkedId))
                                {
                                    gameobjectLinkedIds.Remove(dbLinkedId);
                                    dbGameobjectRowsRemoved++;
                                }
                            }
                        }
                    }
                }
            }

            var creatureAddonLinkedIds = new List<string>(creatureLinkedIds);
            var gameobjectAddonLinkedIds = new List<string>(gameobjectLinkedIds);

            for (int i = 0; i < lines.Count(); i++)
            {
                if (IsDeleteLine(lines[i]))
                {
                    string deleteQuery = "";
                    uint linkedIdsCount = 0;

                    switch (GetObjectTypeFromLine(lines[i]))
                    {
                        case ObjectTypes.Creature:
                        {
                            if (creaturesRemover)
                            {
                                deleteQuery = "DELETE FROM `creature` WHERE `linked_id` IN (";

                                foreach (string linkedId in creatureLinkedIds)
                                {
                                    linkedIdsCount++;
                                    deleteQuery += "'" + linkedId + "'";

                                    if (creatureLinkedIds.Count > linkedIdsCount)
                                        deleteQuery += ", ";
                                    else
                                        deleteQuery += ");";
                                }

                                outputLines.Add(deleteQuery);
                            }

                            break;
                        }
                        case ObjectTypes.GameObject:
                        {
                            if (gameobjectsRemover)
                            {
                                deleteQuery = "DELETE FROM `gameobject` WHERE `linked_id` IN (";

                                foreach (string linkedId in gameobjectLinkedIds)
                                {
                                    linkedIdsCount++;
                                    deleteQuery += "'" + linkedId + "'";

                                    if (gameobjectLinkedIds.Count > linkedIdsCount)
                                        deleteQuery += ", ";
                                    else
                                        deleteQuery += ");";
                                }

                                outputLines.Add(deleteQuery);
                            }

                            break;
                        }
                    }
                }
                else if (IsInsertLine(lines[i]))
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        if (IsCreatureAddonDeleteLine(lines[i]) || IsGameObjectAddonDeleteLine(lines[i]))
                        {
                            i--;
                            break;
                        }

                        if (lines[i].StartsWith("-- (") || lines[i] == "")
                            continue;

                        string linkedId = GetLinkedIdFromLine(lines[i]);
                        uint entry = GetEntryFromLine(lines[i]);
                        if (linkedId == "" || entry == 0)
                            continue;

                        if (creaturesRemover)
                        {
                            if (creatureLinkedIds.Contains(linkedId))
                            {
                                outputLines.Add(lines[i]);
                                creatureLinkedIds.Remove(linkedId);
                                continue;
                            }
                        }

                        if (gameobjectsRemover)
                        {
                            if (gameobjectLinkedIds.Contains(linkedId))
                            {
                                outputLines.Add(lines[i]);
                                gameobjectLinkedIds.Remove(linkedId);
                                continue;
                            }
                        }
                    }
                    while (lines[i] != "" && GetLinkedIdFromLine(lines[i]) != "" && !IsCreatureAddonDeleteLine(lines[i]) && !IsGameObjectAddonDeleteLine(lines[i]));
                }
                else if (IsCreatureAddonDeleteLine(lines[i]) && creaturesRemover)
                {
                    string deleteQuery = "DELETE FROM `creature_addon` WHERE `linked_id` IN (";
                    uint linkedIdsCount = 0;

                    foreach (string linkedId in creatureAddonLinkedIds)
                    {
                        linkedIdsCount++;
                        deleteQuery += "'" + linkedId + "'";

                        if (creatureAddonLinkedIds.Count > linkedIdsCount)
                            deleteQuery += ", ";
                        else
                            deleteQuery += ");";
                    }

                    outputLines.Add(deleteQuery);
                }
                else if (IsCreatureAddonInsertLine(lines[i]) && creaturesRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        if (lines[i] == "")
                            continue;

                        string linkedId = GetLinkedIdFromLine(lines[i]);
                        if (linkedId == "")
                            continue;

                        if (creatureAddonLinkedIds.Contains(linkedId))
                        {
                            outputLines.Add(lines[i]);
                            creatureAddonLinkedIds.Remove(linkedId);
                            continue;
                        }
                    }
                    while (lines[i] != "" && GetLinkedIdFromLine(lines[i]) != "");
                }
                else if (IsGameObjectAddonDeleteLine(lines[i]) && gameobjectsRemover)
                {
                    string deleteQuery = "DELETE FROM `gameobject_addon` WHERE `linked_id` IN (";
                    uint linkedIdsCount = 0;

                    foreach (string linkedId in gameobjectAddonLinkedIds)
                    {
                        linkedIdsCount++;
                        deleteQuery += "'" + linkedId + "'";

                        if (gameobjectAddonLinkedIds.Count > linkedIdsCount)
                            deleteQuery += ", ";
                        else
                            deleteQuery += ");";
                    }

                    outputLines.Add(deleteQuery);
                }
                else if (IsGameObjectAddonInsertLine(lines[i]) && gameobjectsRemover)
                {
                    outputLines.Add(lines[i]);

                    do
                    {
                        i++;

                        if (lines[i] == "")
                            continue;

                        string linkedId = GetLinkedIdFromLine(lines[i]);
                        if (linkedId == "")
                            continue;

                        if (gameobjectAddonLinkedIds.Contains(linkedId))
                        {
                            outputLines.Add(lines[i]);
                            gameobjectAddonLinkedIds.Remove(linkedId);
                            continue;
                        }
                    }
                    while (lines[i] != "" && GetLinkedIdFromLine(lines[i]) != "");
                }
                else
                {
                    if (lines[i] != "")
                    {
                        outputLines.Add(lines[i]);
                    }
                }
            }

            if (creaturesRemover)
            {
                labelCreatures.Text = " Creatures was removed: " + (creatureRowsRemoved + dbCreatureRowsRemoved) + " (" + creatureRowsRemoved + " was found in current document, " + dbCreatureRowsRemoved + " was found in database)";
                labelCreatures.Show();
            }

            if (gameobjectsRemover)
            {
                labelGameobjects.Text = " Gameobjects was removed: " + (gameobjectRowsRemoved + dbGameobjectRowsRemoved) + " (" + gameobjectRowsRemoved + " was found in current document, " + dbGameobjectRowsRemoved + " was found in database)";
                labelGameobjects.Show();
            }

            foreach (string line in outputLines)
                outputFile.WriteLine(line);

            outputFile.Close();
        }

        private static string GetLinkedIdFromLine(string line)
        {
            Regex linkedIdRegex = new Regex(@"'+\S+'+");

            if (linkedIdRegex.IsMatch(line))
            {
                {
                    return linkedIdRegex.Match(line).ToString().Replace("'", "");
                }
            }

            return "";
        }

        private static uint GetEntryFromLine(string line)
        {
            var splittedLine = line.Split(',');

            return Convert.ToUInt32(splittedLine[1]);
        }

        private static bool IsDeleteLine(string line)
        {
            if (line.Contains("DELETE FROM `creature`") ||
                line.Contains("DELETE FROM `gameobject`"))
                return true;
            return false;
        }

        private static bool IsInsertLine(string line)
        {
            if (line.Contains("INSERT INTO `creature`") ||
                line.Contains("INSERT INTO `gameobject`"))
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
            fileDialog.Filter = "SQL File (*.sql)|*.sql";
            fileDialog.FileName = "";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowReadOnly = false;
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
        }
    }
}
