﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class PhaseDataAdvisor
    {
        private struct CreatureData
        {
            public string linkedId;
            public uint entry;
            public uint zoneId;
            public uint phaseId;
            public string name;
        };

        public static void GetPhaseDataForCreatures(TextBox textBox, string fileNameWithAddons)
        {
            if (textBox.Text == "")
                return;

            if (!DBC.DBC.IsLoaded())
            {
                DBC.DBC.Load();
            }

            string output = "";
            string[] textBoxTextSplitted = textBox.Text.Split('\n');
            Dictionary<string, CreatureData> creaturesData = new Dictionary<string, CreatureData>();
            Dictionary<uint, List<string>> phasesLinkedToCreatures = new Dictionary<uint, List<string>>();
            Dictionary<uint, List<uint>> zonesLinkedToPhases = new Dictionary<uint, List<uint>>();

            for (int i = 0; i < textBoxTextSplitted.Count(); i++)
            {
                if (textBoxTextSplitted[i].Length != 0 && LineGetters.GetLinkedIdFromLine(textBoxTextSplitted[i]) != "")
                {
                    CreatureData creatureData = GetCreatureDataFromLine(textBoxTextSplitted[i]);
                    creaturesData.Add(GetRowWithModifyiedPhaseIdFromLine(textBoxTextSplitted[i]), creatureData);

                    if (!phasesLinkedToCreatures.ContainsKey(creatureData.phaseId))
                    {
                        phasesLinkedToCreatures.Add(creatureData.phaseId, new List<string>() { creatureData.linkedId });
                    }
                    else
                    {
                        phasesLinkedToCreatures[creatureData.phaseId].Add(creatureData.linkedId);
                    }

                    if (!zonesLinkedToPhases.ContainsKey(creatureData.zoneId))
                    {
                        zonesLinkedToPhases.Add(creatureData.zoneId, new List<uint>() { creatureData.phaseId });
                    }
                    else
                    {
                        if (!zonesLinkedToPhases[creatureData.zoneId].Contains(creatureData.phaseId))
                        {
                            zonesLinkedToPhases[creatureData.zoneId].Add(creatureData.phaseId);
                        }
                    }
                }
            }

            output += $"DELETE FROM `creature_addon` WHERE `linked_id` IN (SELECT `linked_id` FROM `creature` WHERE `id` IN ({GetEntriesStringFromCreatureData(creaturesData)}) AND `phaseId` IN ({GetPhasesStringFromCreatureData(creaturesData)}));\r\n\r\n";
            output += $"DELETE FROM `creature` WHERE `id` IN ({GetEntriesStringFromCreatureData(creaturesData)}) AND `phaseId` IN ({GetPhasesStringFromCreatureData(creaturesData)}));\r\n";
            output += $"INSERT INTO `creature` (`linked_id`, `id`, `map`, `zoneId`, `areaId`, `difficulties`, `phaseMask`, `phaseId`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `spawndist`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `npcflag2`, `unit_flags`, `unit_flags2`, `unit_flags3`, `dynamicflags`, `VerifiedBuild`) VALUES\r\n";

            foreach (var creatureRow in creaturesData)
            {
                output += creatureRow.Key;
            }

            output += $"\r\n{AddonsHelper.GetAddonsFromSql(fileNameWithAddons, textBox)}\r\n\r\n";

            foreach (var linkedPhasesData in phasesLinkedToCreatures)
            {
                output += $"UPDATE `creature` SET `phaseMask` = 0, `phaseId` = {linkedPhasesData.Key} WHERE `linked_id` IN ({GetLinkedIdsStringFromList(linkedPhasesData.Value)});\r\n";
            }

            output += $"\r\nDELETE FROM `phase_definitions` WHERE `zoneId` IN ({GetZoneIdsStringFromDictionary(zonesLinkedToPhases)}) AND `entry` IN ({GetPhaseIdsStringFromDictionary(zonesLinkedToPhases)});\r\n";
            output += "INSERT INTO `phase_definitions` (`zoneId`, `entry`, `phasemask`, `phaseId`, `terrainswapmap`, `uiworldmapareaswap`, `flags`, `comment`) VALUES\r\n";
            output += GetPhaseDefinitionStringFromDictionary(zonesLinkedToPhases, creaturesData.Values.ToList());

            output += $"\r\nDELETE FROM `conditions` WHERE `SourceTypeOrReferenceId` = 26 AND `SourceGroup` IN ({GetZoneIdsStringFromDictionary(zonesLinkedToPhases)}) AND `SourceEntry` IN ({GetPhaseIdsStringFromDictionary(zonesLinkedToPhases)});\r\n";
            output += "INSERT INTO `phase_definitions` (`zoneId`, `entry`, `phasemask`, `phaseId`, `terrainswapmap`, `uiworldmapareaswap`, `flags`, `comment`) VALUES\r\n";
            output += GetPhaseConditionsStringFromDictionary(zonesLinkedToPhases);

            textBox.Text = output;
        }

        private static CreatureData GetCreatureDataFromLine(string line)
        {
            return new CreatureData
            {
                linkedId = LineGetters.GetLinkedIdFromLine(line),
                entry = LineGetters.GetEntryFromLine(line),
                zoneId = LineGetters.GetZoneIdFromLine(line),
                phaseId = LineGetters.GetPhaseIdFromLine(line),
                name = LineGetters.GetCreatureNameFromLine(line)
            };
        }

        private static string GetEntriesStringFromCreatureData(Dictionary<string, CreatureData> creatureData)
        {
            string output = "";

            for (int i = 0; i < creatureData.Count(); i++)
            {
                if (output.Contains(creatureData.ElementAt(i).Value.entry.ToString()))
                        continue;

                if (i + 1 < creatureData.Count())
                {
                    output += $"{creatureData.ElementAt(i).Value.entry}, ";
                }
                else
                {
                    output += creatureData.ElementAt(i).Value.entry;
                }
            }

            if (output.EndsWith(", "))
                return output.Remove(output.LastIndexOf(", "));
            else
                return output;
        }

        private static string GetPhasesStringFromCreatureData(Dictionary<string, CreatureData> creatureData)
        {
            string output = "";

            List<uint> phasesList = creatureData.Select(x => x.Value.phaseId).ToList();
            phasesList.Sort();

            for (int i = 0; i < phasesList.Count(); i++)
            {
                if (output.Contains($"{creatureData.ElementAt(i).Value.phaseId}, ") || output.Contains($", {creatureData.ElementAt(i).Value.phaseId}"))
                    continue;

                if (i + 1 < phasesList.Count())
                {
                    output += $"{creatureData.ElementAt(i).Value.phaseId}, ";
                }
                else
                {
                    output += creatureData.ElementAt(i).Value.phaseId;
                }
            }

            if (output.EndsWith(", "))
                return output.Remove(output.LastIndexOf(", "));
            else
                return output;
        }

        private static string GetRowWithModifyiedPhaseIdFromLine(string line)
        {
            Regex phaseIdRegex = new Regex(@" '.+'{1}, \w{1}, \w+");

            if (phaseIdRegex.IsMatch(line))
            {
                string[] splittedLine = phaseIdRegex.Match(line).ToString().Split(',');
                splittedLine[0] = $"{splittedLine[0]},";
                splittedLine[1] = $"{splittedLine[1]}, ";
                splittedLine[2] = "0";
                return line.Replace(phaseIdRegex.Match(line).ToString(), string.Concat(splittedLine));
            }

            return "";
        }

        private static string GetLinkedIdsStringFromList(List<string> list)
        {
            string output = "";

            for (int i = 0; i < list.Count(); i++)
            {
                if (i + 1 < list.Count())
                {
                    output += "'" + list[i] + "', ";
                }
                else
                {
                    output += "'" + list[i] + "'";
                }
            }

            return output;
        }

        private static string GetZoneIdsStringFromDictionary(Dictionary<uint, List<uint>> dict)
        {
            string output = "";

            for (int i = 0; i < dict.Count(); i++)
            {
                if (i + 1 < dict.Count())
                {
                    output += dict.ElementAt(i).Key + ", ";
                }
                else
                {
                    output += dict.ElementAt(i).Key;
                }
            }

            return output;
        }

        private static string GetPhaseIdsStringFromDictionary(Dictionary<uint, List<uint>> dict)
        {
            string output = "";

            for (int i = 0; i < dict.Values.Count(); i++)
            {
                if (i + 1 < dict.Values.Count())
                {
                    for (int j = 0; j < dict.Values.ElementAt(i).Count(); j++)
                    {
                        if (output.Contains($"{dict.Values.ElementAt(i).ElementAt(j)}, ") || output.Contains($", {dict.Values.ElementAt(i).ElementAt(j)}"))
                            continue;

                        output += dict.Values.ElementAt(i).ElementAt(j) + ", ";
                    }
                }
                else
                {
                    for (int j = 0; j < dict.Values.ElementAt(i).Count(); j++)
                    {
                        if (output.Contains($"{dict.Values.ElementAt(i).ElementAt(j)}, ") || output.Contains($", {dict.Values.ElementAt(i).ElementAt(j)}"))
                            continue;

                        if (j + 1 < dict.Values.ElementAt(i).Count())
                        {
                            output += dict.Values.ElementAt(i).ElementAt(j) + ", ";
                        }
                        else
                        {
                            output += dict.Values.ElementAt(i).ElementAt(j);
                        }
                    }
                }
            }

            if (output.EndsWith(", "))
                return output.Remove(output.LastIndexOf(", "));
            else
                return output;
        }

        private static string GetPhaseDefinitionStringFromDictionary(Dictionary<uint, List<uint>> dict, List<CreatureData> creaturesData)
        {
            string output = "";

            for (int i = 0; i < dict.Keys.Count(); i++)
            {
                if (i + 1 < dict.Keys.Count())
                {
                    for (int j = 0; j < dict.ElementAt(i).Value.Count; j++)
                    {
                        output += $"({dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 0, '{DBC.DBC.AreaTable[(int)dict.ElementAt(i).Key].AreaName} - Quest XXX - {GetCreatureNamesStringFromList(dict.ElementAt(i).Key, dict.Values.ElementAt(i).ElementAt(j), creaturesData)} visible'),\r\n";
                    }
                }
                else
                {
                    for (int j = 0; j < dict.Values.ElementAt(i).Count(); j++)
                    {
                        if (j + 1 < dict.Values.ElementAt(i).Count())
                        {
                            output += $"({dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 0, '{DBC.DBC.AreaTable[(int)dict.ElementAt(i).Key].AreaName} - Quest XXX - {GetCreatureNamesStringFromList(dict.ElementAt(i).Key, dict.Values.ElementAt(i).ElementAt(j), creaturesData)} visible'),\r\n";
                        }
                        else
                        {
                            output += $"({dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 0, '{DBC.DBC.AreaTable[(int)dict.ElementAt(i).Key].AreaName} - Quest XXX - {GetCreatureNamesStringFromList(dict.ElementAt(i).Key, dict.Values.ElementAt(i).ElementAt(j), creaturesData)} visible');\r\n";
                        }
                    }
                }
            }

            return output;
        }

        private static string GetCreatureNamesStringFromList(uint zoneId, uint phaseId, List<CreatureData> creaturesData)
        {
            string output = "";
            uint index = 0;
            List<string> names = creaturesData.Where(x => x.phaseId == phaseId && x.zoneId == zoneId).Select(x => x.name).ToList();

            foreach (string name in names)
            {
                if (index + 1 < names.Count())
                {
                    if (names.Count > 1 && index + 2 == names.Count())
                    {
                        output += $"{name} and ";
                    }
                    else
                    {
                        output += $"{name}, ";
                    }
                }
                else
                {
                    output += name;
                }

                index++;
            }

            return output;
        }

        private static string GetPhaseConditionsStringFromDictionary(Dictionary<uint, List<uint>> dict)
        {
            string output = "";

            for (int i = 0; i < dict.Keys.Count(); i++)
            {
                if (i + 1 < dict.Keys.Count())
                {
                    for (int j = 0; j < dict.ElementAt(i).Value.Count; j++)
                    {
                        output += $"(26, {dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 47, 0, 12345, 74, 0, 0, 0, 0, '', 'Phase {dict.Values.ElementAt(i).ElementAt(j)} in zone {dict.ElementAt(i).Key} if player XXX quest 12345'),\r\n";
                    }
                }
                else
                {
                    for (int j = 0; j < dict.Values.ElementAt(i).Count(); j++)
                    {
                        if (j + 1 < dict.Values.ElementAt(i).Count())
                        {
                            output += $"(26, {dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 47, 0, 12345, 74, 0, 0, 0, 0, '', 'Phase {dict.Values.ElementAt(i).ElementAt(j)} in zone {dict.ElementAt(i).Key} if player XXX quest 12345'),\r\n";
                        }
                        else
                        {
                            output += $"(26, {dict.ElementAt(i).Key}, {dict.Values.ElementAt(i).ElementAt(j)}, 0, 0, 47, 0, 12345, 74, 0, 0, 0, 0, '', 'Phase {dict.Values.ElementAt(i).ElementAt(j)} in zone {dict.ElementAt(i).Key} if player XXX quest 12345');\r\n";
                        }
                    }
                }
            }

            return output;
        }
    }
}
