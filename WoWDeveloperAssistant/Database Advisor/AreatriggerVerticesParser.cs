using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class AreatriggerVerticesParser
    {
        struct Position
        {
            public double posX, posY, posZ;
        }

        public static void GetVerticesForGuidFromSniff(string areatriggerGuid, string fileName, TextBox textBox)
        {
            var lines = File.ReadAllLines(fileName);
            List<Position> verticesList = new List<Position>();
            string moveCurveID = "X";

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("Object Guid: Full:") && lines[i].Contains("AreaTrigger/0"))
                {
                    string[] splittedGuidLine = lines[i].Split(new char[] { ' ' });

                    string atGuid = splittedGuidLine[4];

                    if (atGuid == areatriggerGuid)
                    {
                        double posX = 0, posY = 0, posZ = 0;

                        do
                        {
                            i++;

                            if (lines[i].Contains("Stationary Position:"))
                            {
                                string[] splittedSpawnPosLine = lines[i].Split(new char[] { ' ' });

                                posX = Convert.ToDouble(splittedSpawnPosLine[4], NumberFormatInfo.InvariantInfo);
                                posY = Convert.ToDouble(splittedSpawnPosLine[6], NumberFormatInfo.InvariantInfo);
                                posZ = Convert.ToDouble(splittedSpawnPosLine[8], NumberFormatInfo.InvariantInfo);
                            }

                            if (lines[i].Contains("Points: X:"))
                            {
                                string[] splittedVerticesLine = lines[i].Split(new char[] { ' ' });
                                Position position;
                                position.posX = Math.Round(posX - Convert.ToDouble(splittedVerticesLine[4], NumberFormatInfo.InvariantInfo), 2);
                                position.posY = Math.Round(posY - Convert.ToDouble(splittedVerticesLine[6], NumberFormatInfo.InvariantInfo), 2);
                                position.posZ = Math.Round(posZ - Convert.ToDouble(splittedVerticesLine[8], NumberFormatInfo.InvariantInfo), 2);

                                verticesList.Add(position);
                            }

                            if (lines[i].Contains("MoveCurveID"))
                            {
                                string[] splittedMoveCurveLine = lines[i].Split(new char[] { ' ' });

                                moveCurveID = splittedMoveCurveLine[2];
                            }

                        } while (!lines[i].Contains("UpdateType: CreateObject2"));

                        break;
                    }
                }
            }

            int parsedCount = 0;
            string SQLtext = "DELETE FROM `areatrigger_move_splines` WHERE `move_curve_id` = " + moveCurveID + ";" + "\r\n";
            SQLtext = SQLtext + "INSERT INTO `areatrigger_move_splines` (`move_curve_id`, `path_id`, `path_x`, `path_y`, `path_z`) VALUES\r\n";

            foreach (var itr in verticesList)
            {
                SQLtext = SQLtext + "(" + moveCurveID + ", " + parsedCount + ", " + Convert.ToString(itr.posX).Replace(",", ".") + ", " + Convert.ToString(itr.posY).Replace(",", ".") + ", " + Convert.ToString(itr.posZ).Replace(",", ".") + ")";

                if (parsedCount < verticesList.Count - 1)
                {
                    SQLtext = SQLtext + ",\r\n";
                }
                else
                {
                    SQLtext = SQLtext + ";\r\n";
                }

                parsedCount++;
            }

            textBox.Text = SQLtext;
        }
    }
}
