using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Database_Advisor
{
    class PlayerCastedSpellsParser
    {
        public static string ParsePlayerCastedSpells(string fileName, string playerGuid)
        {
            if (!DBC.DBC.IsLoaded())
            {
                DBC.DBC.Load();
            }

            var lines = File.ReadAllLines(fileName);

            string outputLine = "";
            Dictionary<long, Packet.PacketTypes> packetIndexes = new Dictionary<long, Packet.PacketTypes>();
            List<SpellStartPacket> spellsList = new List<SpellStartPacket>();
            Dictionary<uint, uint> spellsCastedCount = new Dictionary<uint, uint>();

            BuildVersions buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return "";
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
                    SpellStartPacket spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, value.Key, buildVersion, true);
                    if (spellPacket.spellId == 0 || spellPacket.casterGuid != playerGuid)
                        return;

                    lock (spellsList)
                    {
                        int spellIndex = spellsList.FindIndex(x => x.spellId == spellPacket.spellId);

                        if (spellIndex == -1)
                        {
                            spellsList.Add(spellPacket);
                            spellsCastedCount.Add(spellPacket.spellId, 1);
                        }
                        else
                        {
                            if (spellsList[spellIndex].spellCastStartTime > spellPacket.spellCastStartTime)
                            {
                                spellsList.RemoveAt(spellIndex);
                                spellsList.Add(spellPacket);
                            }

                            spellsCastedCount[spellPacket.spellId] += 1;
                        }
                    }
                }
            });

            spellsList = spellsList.OrderBy(x => x.spellCastStartTime).ToList();

            outputLine += "Spells casted by player with guid \"" + playerGuid + "\"" + "\r\n";

            foreach (SpellStartPacket spell in spellsList)
            {
                outputLine += "Spell Id: " + spell.spellId + " (" + Spell.GetSpellName(spell.spellId) + "), Cast Time: " + spell.spellCastStartTime.ToFormattedStringWithMilliseconds() + ", Casted times: " + spellsCastedCount[spell.spellId] + "\r\n";
            }

            return outputLine;
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
