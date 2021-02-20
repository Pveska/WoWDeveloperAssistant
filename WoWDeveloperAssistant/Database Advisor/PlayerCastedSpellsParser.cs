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
            Dictionary<uint, TimeSpan> spellsDictionary = new Dictionary<uint, TimeSpan>();

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

                    lock (spellsDictionary)
                    {
                        if (!spellsDictionary.ContainsKey(spellPacket.spellId))
                        {
                            spellsDictionary.Add(spellPacket.spellId, spellPacket.spellCastStartTime);
                        }
                    }
                }
            });

            spellsDictionary = spellsDictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            outputLine += "Spells casted by player with guid \"" + playerGuid + "\"" + "\r\n";

            foreach (var spell in spellsDictionary)
            {
                outputLine += "Spell Id: " + spell.Key + " (" + Spell.GetSpellName(spell.Key) + "), Cast Time: " + spell.Value.ToString() + "\r\n";
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
