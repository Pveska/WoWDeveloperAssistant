using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class DoublePathsFinder
    {
        public static void FindDoublePaths(TextBox textBox, string zoneId)
        {
            string output = "";

            Dictionary<string, uint> creatureAddonDictionary = new Dictionary<string, uint>();

            if (Properties.Settings.Default.UsingDB)
            {
                DataSet creatureAddonDs = SQLModule.DatabaseSelectQuery($"SELECT `linked_id`, `path_id` FROM `creature_addon` WHERE `path_id` > 0 AND `linked_id` IN (SELECT `linked_id` FROM `creature` WHERE `linked_id` != 0 AND `zoneId` = {zoneId});");

                if (creatureAddonDs != null && creatureAddonDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in creatureAddonDs.Tables["table"].Rows)
                    {
                        if (!creatureAddonDictionary.ContainsKey((string)row.ItemArray[0]))
                        {
                            creatureAddonDictionary.Add((string)row.ItemArray[0], (uint)row.ItemArray[1]);
                        }
                    }
                }
            }
            else
            {
                textBox.Text = "Can't find any double path for this zone!";
                return;
            }

            Dictionary<uint, List<Position>> pathDictionary = new Dictionary<uint, List<Position>>();

            string pahtIds = "";

            for (int i = 0; i < creatureAddonDictionary.Values.Count; i++)
            {
                if (i + 1 < creatureAddonDictionary.Values.Count)
                {
                    pahtIds += creatureAddonDictionary.Values.ElementAt(i) + ", ";
                }
                else
                {
                    pahtIds += creatureAddonDictionary.Values.ElementAt(i);
                }
            }

            DataSet waypointDataDs = SQLModule.DatabaseSelectQuery($"SELECT `id`, `position_x`, `position_y`, `position_z` FROM `waypoint_data` WHERE `id` IN ({pahtIds});");

            pahtIds = "";

            if (waypointDataDs != null && waypointDataDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in waypointDataDs.Tables["table"].Rows)
                {
                    if (!pathDictionary.ContainsKey((uint)row.ItemArray[0]))
                    {
                        pathDictionary.Add((uint)row.ItemArray[0], new List<Position>() { new Position((float)row.ItemArray[1], (float)row.ItemArray[2], (float)row.ItemArray[3]) });
                    }
                    else
                    {
                        pathDictionary[(uint)row.ItemArray[0]].Add(new Position((float)row.ItemArray[1], (float)row.ItemArray[2], (float)row.ItemArray[3]));
                    }
                }
            }

            waypointDataDs.Clear();

            string linkedIds = "";

            for (int i = 0; i < creatureAddonDictionary.Keys.Count; i++)
            {
                if (i + 1 < creatureAddonDictionary.Keys.Count)
                {
                    linkedIds += $"'{creatureAddonDictionary.Keys.ElementAt(i)}', ";
                }
                else
                {
                    linkedIds += $"'{creatureAddonDictionary.Keys.ElementAt(i)}'";
                }
            }

            DataSet creatureDatasDs = SQLModule.DatabaseSelectQuery($"SELECT `linked_id`, `id`, `map`, `phaseId`, `phaseMask` FROM `creature` WHERE `linked_id` IN ({linkedIds});");

            Dictionary<string, object[]> creatureDatasDict = new Dictionary<string, object[]>();

            foreach (DataRow row in creatureDatasDs.Tables["table"].Rows)
            {
                creatureDatasDict.Add(row.ItemArray[0].ToString(), row.ItemArray);
            }

            creatureDatasDs.Clear();

            linkedIds = "";

            List<string> linkedIdsWithDoublePaths = new List<string>();
            uint doublePathsCount = 0;

            Parallel.ForEach(pathDictionary.AsEnumerable(), originalPath =>
            {
                foreach (var computePath in pathDictionary)
                {
                    if (originalPath.Key != computePath.Key)
                    {
                        string originalLinkedId = creatureAddonDictionary.First(x => x.Value == originalPath.Key).Key;
                        string computeLinkedId = creatureAddonDictionary.First(x => x.Value == computePath.Key).Key;

                        if (!IsCreaturesWithinLos(originalLinkedId, computeLinkedId, creatureDatasDict))
                            continue;

                        uint matchesCount = 0;

                        foreach (Position movePos in computePath.Value)
                        {
                            if (originalPath.Value.Count(x => x == movePos) != 0)
                            {
                                matchesCount++;
                            }
                        }

                        lock (linkedIdsWithDoublePaths)
                        {
                            if ((originalPath.Value.Count == matchesCount || matchesCount > 0) && (!linkedIdsWithDoublePaths.Contains(originalLinkedId) ||
                                !linkedIdsWithDoublePaths.Contains(computeLinkedId)))
                            {
                                doublePathsCount++;
                                linkedIdsWithDoublePaths.Add(originalLinkedId);
                                linkedIdsWithDoublePaths.Add(computeLinkedId);

                                if (originalPath.Value.Count == matchesCount)
                                {
                                    output += $"Creature with linked id: {originalLinkedId} ({GetCreatureData(originalLinkedId, creatureDatasDict)},  {GetMapData(originalLinkedId, creatureDatasDict)}) have same path as creature with linked id: {computeLinkedId} ({GetCreatureData(computeLinkedId, creatureDatasDict)},  {GetMapData(computeLinkedId, creatureDatasDict)}) ({matchesCount} familiar waypoints)" + "\r\n";
                                }
                                else
                                {
                                    output += $"Creature with linked id: {originalLinkedId} ({GetCreatureData(originalLinkedId, creatureDatasDict)},  {GetMapData(originalLinkedId, creatureDatasDict)}) probably have same path as creature with linked id: {computeLinkedId} ({GetCreatureData(computeLinkedId, creatureDatasDict)},  {GetMapData(computeLinkedId, creatureDatasDict)}) ({matchesCount} familiar waypoints)" + "\r\n";
                                }
                            }
                        }
                    }
                }
            });

            if (linkedIdsWithDoublePaths.Count > 0)
            {
                output += "\r\n";
            }

            if (doublePathsCount > 0)
            {
                output += $"Total count of double paths: {doublePathsCount}" + "\r\n";
            }
            else
            {
                output += "Can't find any double path for this zone!";
            }

            textBox.Text = output;
        }

        private static string GetCreatureData(string linkedId, Dictionary<string, object[]> creatureDatas)
        {
            string creatureData = "";

            try
            {
                uint creatureEntry = (uint)creatureDatas[linkedId][1];
                string creatureName = (string)SQLModule.DatabaseSelectQuery($"SELECT `Name1` FROM `creature_template_wdb` WHERE `entry` = {creatureEntry}").Tables["table"].Rows[0].ItemArray[0];
                return creatureData += $"Entry: {creatureEntry}, Name: {creatureName}";
            }
            catch
            {
                return creatureData += "Can't get creature data!";
            }
        }

        private static string GetMapData(string linkedId, Dictionary<string, object[]> creatureDatas)
        {
            string mapData = "";

            try
            {
                int mapId = Convert.ToInt32(creatureDatas[linkedId][2]);
                string mapName = "";

                if (!DBC.DBC.IsLoaded())
                {
                    DBC.DBC.Load();
                }

                mapName = DBC.DBC.Map[mapId].MapName;

                return mapData += $"MapId: {mapId}, MapName: {mapName}";
            }
            catch
            {
                return mapData += "Can't get map data!";
            }
        }

        private static bool IsCreaturesWithinLos(string originalLinkedId, string computeLinkedId, Dictionary<string, object[]> creatureDatas)
        {
            object[] originalCreatureData = creatureDatas[originalLinkedId];
            object[] computeCreatureData = creatureDatas[computeLinkedId];

            // Map
            if (originalCreatureData[2].ToString() != computeCreatureData[2].ToString())
                return false;

            // PhaseId
            if (originalCreatureData[3].ToString() != computeCreatureData[3].ToString())
                return false;

            // PhaseMask
            if (originalCreatureData[4].ToString() != computeCreatureData[4].ToString())
                return false;

            return true;
        }
    }
}
