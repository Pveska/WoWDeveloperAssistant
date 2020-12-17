using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class AreatriggerSplineCreator
    {
        public static void ParseSplinesForAreatrigger(string fileName, string customEntry)
        {
            var lines = File.ReadAllLines(fileName);

            string outputLine = "";

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].Contains("UpdateType: CreateObject2"))
                {
                    if (lines[i + 1].Contains("AreaTrigger") && 
                        LineGetters.GetAreatriggerEntryFromLine(lines[i + 1]) == customEntry)
                    {
                        outputLine += "DELETE FROM `areatrigger_move_splines` WHERE `move_curve_id` = " + customEntry + ";\n";
                        outputLine += "INSERT INTO `areatrigger_move_splines` (`move_curve_id`, `path_id`, `path_x`, `path_y`, `path_z`) VALUES\n";

                        Position summonPos = new Position(0.0f, 0.0f, 0.0f);
                        uint pathId = 0;
                        string spellId = "";

                        do
                        {
                            i++;

                            if (lines[i].Contains("StationaryPosition: X:"))
                            {
                                string[] splittedLine = lines[i].Split(' ');

                                summonPos.x = float.Parse(splittedLine[3], CultureInfo.InvariantCulture.NumberFormat);
                                summonPos.y = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                                summonPos.z = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                            }

                            if (lines[i].Contains("Points: X:") && lines[i].Contains("AreaTriggerUpdate"))
                            {
                                string[] splittedLine = lines[i].Split(' ');

                                float x = float.Parse(splittedLine[5], CultureInfo.InvariantCulture.NumberFormat);
                                float y = float.Parse(splittedLine[7], CultureInfo.InvariantCulture.NumberFormat);
                                float z = float.Parse(splittedLine[9], CultureInfo.InvariantCulture.NumberFormat);

                                var spline = new Position(x, y, z) - summonPos;

                                if (lines[i + 1].Contains("Points: X:"))
                                    outputLine += "(" + customEntry + ", " + pathId + ", " + spline.x.ToString().Replace(",", ".") + ", " + spline.y.ToString().Replace(",", ".") + ", " + spline.z.ToString().Replace(",", ".") + "),\n";
                                else
                                    outputLine += "(" + customEntry + ", " + pathId + ", " + spline.x.ToString().Replace(",", ".") + ", " + spline.y.ToString().Replace(",", ".") + ", " + spline.z.ToString().Replace(",", ".") + ");\n";

                                pathId++;
                            }

                            if (LineGetters.GetSpellIdFromLine(lines[i]) != "")
                                spellId = LineGetters.GetSpellIdFromLine(lines[i]);
                        }
                        while (Packets.UpdateObjectPacket.IsLineValidForObjectParse(lines[i]));

                        if (spellId != "")
                        {
                            outputLine += "-- SpellId: " + spellId + ";\n\n";
                        }

                        i--;
                    }
                }
            }

            Clipboard.SetText(outputLine);
            MessageBox.Show("Splines has been successfully parsed and copied on your clipboard!");
        }

        public static void OpenFileDialog(OpenFileDialog fileDialog)
        {
            fileDialog.Title = "Open File";
            fileDialog.Filter = "Txt File (*.txt)|*.txt";
            fileDialog.FileName = "";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowReadOnly = false;
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
        }
    }
}
