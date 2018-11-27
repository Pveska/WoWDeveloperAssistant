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
        public static void RemoveDoubleSpawnsFromFile(string fileName, Label labelCreatures, Label labelGameobjects, bool creaturesRemover, bool gameobjectsRemover, bool consideringDB)
        {
            List<string> databaseCreatureLinkedIds = new List<string>();
            List<string> databaseGameobjectLinkedIds = new List<string>();
            List<string> databaseCreatureEntries = new List<string>();
            List<string> databaseGameobjectEntries = new List<string>();
            StreamWriter outputFile = new StreamWriter(fileName + "_without_doubles.sql");
            List<string> creaturesLinkedIds = new List<string>();
            List<string> gameobjectsLinkedIds = new List<string>();
            var lines = File.ReadAllLines(fileName);
            List<string> outputLines = new List<string>();
            uint creatureRowsRemoved = 0;
            uint gameobjectRowsRemoved = 0;
            uint dbCreatureRowsRemoved = 0;
            uint dbGameobjectRowsRemoved = 0;

            if (consideringDB)
            {
                for (int i = 1; i < lines.Count(); i++)
                {
                    if (creaturesRemover)
                    {
                        if (lines[i].Contains("(@CGUID+"))
                        {
                            var splittedLine = lines[i].Split(',');

                            string creatureEntry = splittedLine[2].Split(' ')[1];

                            if (!databaseCreatureEntries.Contains(creatureEntry))
                            {
                                databaseCreatureEntries.Add(creatureEntry);
                            }
                        }
                    }

                    if (gameobjectsRemover)
                    {
                        if (lines[i].Contains("(@OGUID+"))
                        {
                            var splittedLine = lines[i].Split(',');

                            string gameobjectEntry = splittedLine[1].Split(' ')[1];

                            if (!databaseGameobjectEntries.Contains(gameobjectEntry))
                            {
                                databaseGameobjectEntries.Add(gameobjectEntry);
                            }
                        }
                    }
                }

                if (creaturesRemover)
                {
                    foreach (var entry in databaseCreatureEntries)
                    {
                        string creaturesQuery = "SELECT `id`, `map`, `spawnMask`, `phaseMask`, `phaseID`, `position_x`, `position_y`, `position_z`  FROM `creature` WHERE `id` = " + entry + ";";

                        DataSet creaturesDs = new DataSet();
                        creaturesDs = (DataSet)SQLModule.DatabaseSelectQuery(creaturesQuery);

                        if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                            {
                                string linkedId = "";

                                string creatureEntry = row[0].ToString();
                                string map = row[1].ToString();
                                double posX = Convert.ToDouble(row[5].ToString());
                                double posY = Convert.ToDouble(row[6].ToString());
                                double posZ = Convert.ToDouble(row[7].ToString());

                                linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                                linkedId += creatureEntry + " " + map + " " + 0 + " " + 1 + " " + 1;
                                linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                                databaseCreatureLinkedIds.Add(linkedId);
                            }
                        }
                    }
                }

                if (gameobjectsRemover)
                {
                    foreach (var entry in databaseGameobjectEntries)
                    {
                        string gameobjectsQuery = "SELECT `id`, `map`, `spawnMask`, `phaseMask`, `phaseID`, `position_x`, `position_y`, `position_z`  FROM `gameobject` WHERE `id` = " + entry + ";";

                        DataSet gameobjectsDs = new DataSet();
                        gameobjectsDs = (DataSet)SQLModule.DatabaseSelectQuery(gameobjectsQuery);

                        if (gameobjectsDs != null && gameobjectsDs.Tables["table"].Rows.Count > 0)
                        {
                            foreach (DataRow row in gameobjectsDs.Tables["table"].Rows)
                            {
                                string linkedId = "";

                                string gameobjectEntry = row[0].ToString();
                                string map = row[1].ToString();
                                double posX = Convert.ToDouble(row[5].ToString());
                                double posY = Convert.ToDouble(row[6].ToString());
                                double posZ = Convert.ToDouble(row[7].ToString());

                                linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                                linkedId += gameobjectEntry + " " + map + " " + 0 + " " + 1 + " " + 1;
                                linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                                databaseGameobjectLinkedIds.Add(linkedId);
                            }
                        }
                    }
                }
            }

            for (int i = 1; i < lines.Count(); i++)
            {
                if (creaturesRemover)
                {
                    if (lines[i].Contains("(@CGUID+"))
                    {
                        var splittedLine = lines[i].Split(',');

                        string linkedId = splittedLine[1].Split(' ')[1].Split('\'')[1];

                        if (!creaturesLinkedIds.Contains(linkedId))
                        {
                            creaturesLinkedIds.Add(linkedId);

                            if (!databaseCreatureLinkedIds.Contains(linkedId))
                            {
                                outputLines.Add(lines[i]);
                                continue;
                            }
                            else
                            {
                                dbCreatureRowsRemoved++;
                                continue;
                            }
                        }
                        else
                        {
                            creatureRowsRemoved++;
                            continue;
                        }
                    }
                }

                if (gameobjectsRemover)
                {
                    if (lines[i].Contains("(@OGUID+"))
                    {
                        string linkedId = "";

                        var splittedLine = lines[i].Split(',');

                        if (splittedLine.Count() == 5 || splittedLine.Count() == 6)
                            continue;

                        string gameobjectEntry = splittedLine[1].Split(' ')[1];
                        string map = splittedLine[2].Split(' ')[1];
                        double posX = Convert.ToDouble(splittedLine[6].Split(' ')[1], NumberFormatInfo.InvariantInfo);
                        double posY = Convert.ToDouble(splittedLine[7].Split(' ')[1], NumberFormatInfo.InvariantInfo);
                        double posZ = Convert.ToDouble(splittedLine[8].Split(' ')[1], NumberFormatInfo.InvariantInfo);

                        linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                        linkedId += gameobjectEntry + " " + map + " 0 1 1";
                        linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                        if (!gameobjectsLinkedIds.Contains(linkedId))
                        {
                            gameobjectsLinkedIds.Add(linkedId);

                            if (!databaseGameobjectLinkedIds.Contains(linkedId))
                            {
                                outputLines.Add(lines[i]);
                                continue;
                            }
                            else
                            {
                                dbGameobjectRowsRemoved++;
                                continue;
                            }
                        }
                        else
                        {
                            gameobjectRowsRemoved++;
                            continue;
                        }
                    }
                }

                outputLines.Add(lines[i]);
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

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
