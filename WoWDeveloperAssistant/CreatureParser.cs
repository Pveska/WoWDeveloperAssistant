using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoWDeveloperAssistant
{
    public static class CreatureParser
    {
        public static void ParseCreatureAddon(string fileName)
        {
            DataTable creatureDt = new DataTable();
            creatureDt.Columns.AddRange(new DataColumn[7] { new DataColumn("LinkedId", typeof(string)), new DataColumn("Id", typeof(string)), new DataColumn("Map", typeof(string)),
                new DataColumn("PositionX", typeof(string)), new DataColumn("PositionY", typeof(string)), new DataColumn("PositionZ", typeof(string)), new DataColumn("Orientation", typeof(string))});
            creatureDt.PrimaryKey = new DataColumn[] { creatureDt.Columns["LinkedId"] };
            StreamWriter outputFile = new StreamWriter(fileName + "_creature.sql");
            var lines = File.ReadAllLines(fileName);
            string outputQueries = "";
            uint build = 0;

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("Detected build:"))
                {
                    string[] splittedLine = lines[i].Split(new char[] { ' ' });
                    string[] splittedBuildLine = splittedLine[3].Split(new char[] { '_' });

                    build = Convert.ToUInt16(splittedBuildLine[3]);
                }

                if (lines[i].Contains("Object Guid: Full:") && (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0")))
                {
                    string linkedId = "";
                    uint creatureEntry = 0;
                    uint mapId = 0;
                    double posX = 0;
                    double posY = 0;
                    double posZ = 0;
                    double orientation = 0;

                    string[] splittedStartLine = lines[i].Split(new char[] { ' ' });
                    creatureEntry = Convert.ToUInt32(splittedStartLine[10]);
                    mapId = Convert.ToUInt32(splittedStartLine[8]);

                    do
                    {
                        i++;

                        if (lines[i].Contains("Position:"))
                        {
                            string[] splittedLine = lines[i].Split(new char[] { ' ' });

                            posX = Convert.ToDouble(splittedLine[3], NumberFormatInfo.InvariantInfo);
                            posY = Convert.ToDouble(splittedLine[5], NumberFormatInfo.InvariantInfo);
                            posZ = Convert.ToDouble(splittedLine[7], NumberFormatInfo.InvariantInfo);
                        }
                        else if (lines[i].Contains("Orientation:"))
                        {
                            string[] splittedLine = lines[i].Split(new char[] { ' ' });

                            orientation = Convert.ToDouble(splittedLine[2], NumberFormatInfo.InvariantInfo);
                        }

                    } while (!lines[i].Contains("CreateObject1") && lines[i] != "");

                    linkedId = Convert.ToString(Math.Round(posX / 0.25)) + " " + Convert.ToString(Math.Round(posY / 0.25)) + " " + Convert.ToString(Math.Round(posZ / 0.25)) + " ";
                    linkedId += creatureEntry + " " + mapId + " 0 1 1";
                    linkedId = DoubleSpawnsRemover.SHA1HashStringForUTF8String(linkedId).ToUpper();

                    DataRow dataRow = creatureDt.NewRow();
                    dataRow[0] = linkedId;
                    dataRow[1] = creatureEntry;
                    dataRow[2] = mapId;
                    dataRow[3] = posX;
                    dataRow[4] = posY;
                    dataRow[5] = posZ;
                    dataRow[6] = orientation;

                    if (creatureDt.Rows.Find(dataRow[0].ToString()) == null)
                        creatureDt.Rows.Add(dataRow);
                }
            }

            outputQueries = "INSERT INTO `creature` (`linked_id`, `id`, `map`, `zoneId`, `areaId`, `spawnMask`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `spawndist`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `npcflag2`, `unit_flags`, `unit_flags2`, `unit_flags3`, `dynamicflags`, `VerifiedBuild`) VALUES" + "\n";

            foreach (DataRow row in creatureDt.Rows)
            {
                outputQueries += "(" + "'" + row[0].ToString() + "'" + ", " + row[1].ToString() + ", " + row[2].ToString() + ", " + "0, 0, 1, 0, 0" + ", " + row[3].ToString().Replace(",", ".") + ", " + row[4].ToString().Replace(",", ".") + ", " + row[5].ToString().Replace(",", ".") + ", " + row[6].ToString().Replace(",", ".") + ", " + "120, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " + build + ")," + "\n";
            }

            outputFile.WriteLine(outputQueries);
            outputFile.Close();
        }
    }
}
