using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoWDeveloperAssistant
{
    public static class DoubleSpawnsRemover
    {
        private enum ObjectTypes
        {
            Creature   = 1,
            GameObject = 2
        };

        public static void RemoveDoubleSpawnsFromFile(string fileName, Label labelCreatures, Label labelGameobjects, bool creaturesRemover, bool gameobjectsRemover, bool consideringDB)
        {
            StreamWriter outputFile = new StreamWriter(fileName + "_without_duplicates.sql");

            List<string> allowedCreatureLinkedIds = new List<string>();
            List<string> allowedGameobjectLinkedIds = new List<string>();

            List<string> creatureAddonLinkedIds;
            List<string> gameobjectAddonLinkedIds;

            Dictionary<string, string> creatureEntries = new Dictionary<string, string>();
            Dictionary<string, string> gameobjectEntries = new Dictionary<string, string>();

            List<string> creaturesLinkedIds = new List<string>();
            List<string> gameobjectsLinkedIds = new List<string>();

            var lines = File.ReadAllLines(fileName);
            List<string> outputLines = new List<string>();

            uint creatureRowsRemoved = 0;
            uint gameobjectRowsRemoved = 0;
            uint dbCreatureRowsRemoved = 0;
            uint dbGameobjectRowsRemoved = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                if (IsSpawnLine(lines[i]))
                {
                    string linkedId = GetLinkedIdFromLine(lines[i]);
                    string entry = GetEntryFromLine(lines[i]);
                    string map = GetMapFromLine(lines[i]);
                    ObjectTypes type = GetStingObjectType(lines[i]);

                    if (map == "")
                        continue;

                    if (lines[i].StartsWith("-- ('"))
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

                                    if (!creatureEntries.ContainsKey(entry))
                                        creatureEntries.Add(entry, map);
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

                                    if (!gameobjectEntries.ContainsKey(entry))
                                        gameobjectEntries.Add(entry, map);
                                }
                                else
                                {
                                    gameobjectRowsRemoved++;
                                }
                            }

                            break;
                        }
                        default:
                            break;
                    }
                }
            }

            if (consideringDB)
            {
                if (creaturesRemover)
                {
                    foreach (var entry in creatureEntries)
                    {
                        string creaturesQuery = "SELECT `id`, `position_x`, `position_y`, `position_z`  FROM `creature` WHERE `id` = " + entry.Key + " AND `map` = " + entry.Value + ";";

                        DataSet creaturesDs = new DataSet();
                        creaturesDs = (DataSet)SQLModule.DatabaseSelectQuery(creaturesQuery);

                        if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                            {
                                string linkedId = "";

                                string creatureEntry = row[0].ToString();
                                string map = entry.Value;
                                double posX = Convert.ToDouble(row[1].ToString());
                                double posY = Convert.ToDouble(row[2].ToString());
                                double posZ = Convert.ToDouble(row[3].ToString());

                                linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                                linkedId += creatureEntry + " " + map + " 0 1 0";
                                linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                                if (allowedCreatureLinkedIds.Contains(linkedId))
                                {
                                    allowedCreatureLinkedIds.Remove(linkedId);
                                    dbCreatureRowsRemoved++;
                                }
                            }
                        }
                    }
                }

                if (gameobjectsRemover)
                {
                    foreach (var entry in gameobjectEntries)
                    {
                        string gameobjectsQuery = "SELECT `id`, `position_x`, `position_y`, `position_z`  FROM `gameobject` WHERE `id` = " + entry.Key + " AND `map` = " + entry.Value + ";";

                        DataSet gameobjectsDs = new DataSet();
                        gameobjectsDs = (DataSet)SQLModule.DatabaseSelectQuery(gameobjectsQuery);

                        if (gameobjectsDs != null && gameobjectsDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in gameobjectsDs.Tables["table"].Rows)
                            {
                                string linkedId = "";

                                string gameobjectEntry = row[0].ToString();
                                string map = entry.Value;
                                double posX = Convert.ToDouble(row[1].ToString());
                                double posY = Convert.ToDouble(row[2].ToString());
                                double posZ = Convert.ToDouble(row[3].ToString());

                                linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                                linkedId += gameobjectEntry + " " + map + " 0 1 0";
                                linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                                if (allowedGameobjectLinkedIds.Contains(linkedId))
                                {
                                    allowedGameobjectLinkedIds.Remove(linkedId);
                                    dbGameobjectRowsRemoved++;
                                }
                            }
                        }
                    }
                }
            }

            creatureAddonLinkedIds = new List<string>(allowedCreatureLinkedIds);
            gameobjectAddonLinkedIds = new List<string>(allowedGameobjectLinkedIds);

            for (int i = 0; i < lines.Count(); i++)
            {
                if (IsSpawnLine(lines[i]))
                {
                    string linkedId = GetLinkedIdFromLine(lines[i]);

                    if (creaturesRemover)
                    {
                        if (allowedCreatureLinkedIds.Contains(linkedId))
                        {
                            outputLines.Add(lines[i]);
                            allowedCreatureLinkedIds.Remove(linkedId);
                            continue;
                        }
                    }

                    if (gameobjectsRemover)
                    {
                        if (allowedGameobjectLinkedIds.Contains(linkedId))
                        {
                            outputLines.Add(lines[i]);
                            allowedGameobjectLinkedIds.Remove(linkedId);
                            continue;
                        }
                    }
                }
                else if (IsCreatureAddonLine(lines[i]) && creaturesRemover)
                {
                    string linkedId = GetLinkedIdFromLine(lines[i]);

                    if (creatureAddonLinkedIds.Contains(linkedId))
                    {
                        outputLines.Add(lines[i]);
                        creatureAddonLinkedIds.Remove(linkedId);
                        continue;
                    }
                }
                else if (IsGameObjectAddonLine(lines[i]) && gameobjectsRemover)
                {
                    string linkedId = GetLinkedIdFromLine(lines[i]);

                    if (gameobjectAddonLinkedIds.Contains(linkedId))
                    {
                        outputLines.Add(lines[i]);
                        gameobjectAddonLinkedIds.Remove(linkedId);
                        continue;
                    }
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

        private static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        private static string GetLinkedIdFromLine(string line, bool forced = false)
        {
            Regex linkedIdRegex;

            if (forced)
            {
                linkedIdRegex = new Regex(@"'+\S+'+");
            }
            else
            {
                linkedIdRegex = new Regex(@"\(+'+\S+'+");
            }

            if (linkedIdRegex.IsMatch(line))
            {
                if (forced)
                {
                    return linkedIdRegex.Match(line).ToString().Replace("'", "");
                }
                else
                {
                    return linkedIdRegex.Match(line).ToString().Replace("(", "").Replace("'", "");
                }
            }
            else
            {
                return "";
            }
        }

        private static string GetEntryFromLine(string line)
        {
            var splittedLine = line.Split(',');

            return splittedLine[1];
        }

        private static string GetMapFromLine(string line)
        {
            var splittedLine = line.Split(',');
            var splittedMap = splittedLine[2].Split(' ');

            try
            {
                Convert.ToUInt32(splittedMap[1]);
                return splittedMap[1];
            }
            catch
            {
                return "";
            }
        }

        private static bool IsSpawnLine(string line)
        {
            if (line.Contains("DELETE FROM `creature` WHERE `linked_id` IN") ||
                line.Contains("DELETE FROM `gameobject` WHERE `linked_id` IN"))
                return false;

            if (GetLinkedIdFromLine(line) == "")
                return false;

            if (GetEntryFromLine(line) == "")
                return false;

            if (GetStingObjectType(line) == 0)
                return false;

            return true;
        }

        private static ObjectTypes GetStingObjectType(string line)
        {
            if (line.Contains("Creature"))
            {
                return ObjectTypes.Creature;
            }
            else if (line.Contains("GameObject"))
            {
                return ObjectTypes.GameObject;
            }
            else
            {
                return 0;
            }
        }

        private static bool IsCreatureAddonLine(string line)
        {
            if (line.Contains("creature_addon") && GetLinkedIdFromLine(line) != "" &&
                !line.Contains("DELETE FROM `creature_addon` WHERE `linked_id` IN"))
                return true;

            return false;
        }

        private static bool IsGameObjectAddonLine(string line)
        {
            if (line.Contains("gameobject_addon") && GetLinkedIdFromLine(line) != "" &&
                !line.Contains("DELETE FROM `gameobject_addon` WHERE `linked_id` IN"))
                return true;

            return false;
        }
    }
}
