using System;
using System.Collections.Generic;
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
        public static void RemoveDoubleSpawnsFromFile(string fileName, Label labelCreatures, Label labelGameobjects, bool creaturesRemover, bool gameobjectsRemover)
        {

            StreamWriter outputFile = new StreamWriter(fileName + "_without_doubles.sql");
            Regex linkedIdRegex = new Regex(@"'+\S*'+");
            Dictionary<string, string> creaturesLinkedIdsDictionary = new Dictionary<string, string>();
            Dictionary<string, string> gameobjectsLinkedIdsDictionary = new Dictionary<string, string>();
            var lines = File.ReadAllLines(fileName);
            List<string> outputLines = new List<string>();
            uint creatureRowsRemoved = 0;
            uint gameobjectRowsRemoved = 0;

            for (int i = 1; i < lines.Count(); i++)
            {
                if (creaturesRemover)
                {
                    if (lines[i].Contains("(@CGUID+"))
                    {
                        if (linkedIdRegex.IsMatch(lines[i]))
                        {
                            string linkedId = linkedIdRegex.Match(lines[i]).ToString();

                            if (!creaturesLinkedIdsDictionary.ContainsKey(linkedId))
                            {
                                creaturesLinkedIdsDictionary[linkedId] = lines[i];
                                outputLines.Add(lines[i]);
                            }
                            else
                            {
                                creatureRowsRemoved++;
                                continue;
                            }
                        }
                    }
                }
                if (gameobjectsRemover)
                {
                    if (lines[i].Contains("(@OGUID+"))
                    {
                        string linkedId = "";

                        var splirredLine = lines[i].Split(',');

                        string gameobjectEntry = splirredLine[1].Split(' ')[1];
                        string map = splirredLine[2].Split(' ')[1];
                        double posX = Convert.ToDouble(splirredLine[7].Split(' ')[1], NumberFormatInfo.InvariantInfo);
                        double posY = Convert.ToDouble(splirredLine[8].Split(' ')[1], NumberFormatInfo.InvariantInfo);
                        double posZ = Convert.ToDouble(splirredLine[9].Split(' ')[1], NumberFormatInfo.InvariantInfo);

                        linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                        linkedId += gameobjectEntry + " " + map + " 0 1 1";
                        linkedId = SHA1HashStringForUTF8String(linkedId).ToUpper();

                        if (!gameobjectsLinkedIdsDictionary.ContainsKey(linkedId))
                        {
                            gameobjectsLinkedIdsDictionary[linkedId] = lines[i];
                            outputLines.Add(lines[i]);
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
                labelCreatures.Text = " Creatures was removed: " + creatureRowsRemoved;
                labelCreatures.Show();
            }

            if (gameobjectsRemover)
            {
                labelGameobjects.Text = " Gameobjects was removed: " + gameobjectRowsRemoved;
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
    }
}
