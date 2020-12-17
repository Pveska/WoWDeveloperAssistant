using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Database_Advisor
{
    class SpellDestinationsParser
    {
        public static void ParseSpellDestinations(string fileName, string spellId)
        {
            var lines = File.ReadAllLines(fileName);

            string outputLine = "";
            Dictionary<long, Packet.PacketTypes> packetIndexes = new Dictionary<long, Packet.PacketTypes>();
            List<Position> allDestPositions = new List<Position>();
            List<Position> uniqDestPositions = new List<Position>();

            BuildVersions buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            Parallel.For(0, lines.Length, index =>
            {
                if (lines[index].Contains("SMSG_SPELL_START") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_SPELL_START);
                }
                else if (lines[index].Contains("SMSG_SPELL_GO") && !packetIndexes.ContainsKey(index))
                {
                    lock (packetIndexes)
                        packetIndexes.Add(index, Packet.PacketTypes.SMSG_SPELL_GO);
                }
            });

            Parallel.ForEach(packetIndexes.AsEnumerable(), value =>
            {
                if (value.Value == Packet.PacketTypes.SMSG_SPELL_START || value.Value == Packet.PacketTypes.SMSG_SPELL_GO)
                {
                    SpellStartPacket spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, value.Key, buildVersion);
                    if (spellPacket.spellId == 0 || spellPacket.spellId != Convert.ToUInt32(spellId))
                        return;

                    if (spellPacket.spellDestination.IsValid())
                    {
                        lock (allDestPositions)
                            allDestPositions.Add(spellPacket.spellDestination);

                        lock (uniqDestPositions)
                        {
                            if (!uniqDestPositions.Contains(spellPacket.spellDestination) && !CheckIfNearPointExist(spellPacket.spellDestination, uniqDestPositions))
                            {
                                uniqDestPositions.Add(spellPacket.spellDestination);
                            }
                        }
                    }
                }
            });

            outputLine += "Spell destinations for spell " + spellId + "\n\n";

            outputLine += "All positions count " + allDestPositions.Count + "\n";

            foreach(Position pos in allDestPositions)
            {
                outputLine += "{ " + pos.x.ToString().Replace(",", ".") + "f, " + pos.y.ToString().Replace(",", ".") + "f, " + pos.z.ToString().Replace(",", ".") + "f },\n";
            }

            outputLine += "Unique positions count " + allDestPositions.Count + "\n";

            foreach (Position pos in uniqDestPositions)
            {
                outputLine += "{ " + pos.x.ToString().Replace(",", ".") + "f, " + pos.y.ToString().Replace(",", ".") + "f, " + pos.z.ToString().Replace(",", ".") + "f },\n";
            }

            Clipboard.SetText(outputLine);
            MessageBox.Show("Spell destinations has been successfully parsed and copied on your clipboard!");
        }

        private static bool CheckIfNearPointExist(Position point, List<Position> points)
        {
            foreach (Position pos in points)
            {
                if (pos.GetExactDist2d(point) <= 5.0f)
                    return true;
            }

            return false;
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
