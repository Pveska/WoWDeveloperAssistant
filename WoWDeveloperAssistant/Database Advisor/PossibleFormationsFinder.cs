using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class PossibleFormationsFinder
    {
        public static void FindPossibleFormations(TextBox textBox, string zoneId)
        {
            string output = "";

            Dictionary<string, uint> creatureWithPathLinkedIds = new Dictionary<string, uint>();
            Dictionary<uint, string> creatureNames = Utils.GetCreatureNamesFromDB();

            if (Properties.Settings.Default.UsingDB)
            {
                DataSet creaturesDs = SQLModule.DatabaseSelectQuery($"SELECT `linked_id`, `id` FROM `creature` WHERE `MovementType` IN (3, 4) AND `zoneId` = {zoneId};");

                if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                    {
                        creatureWithPathLinkedIds.Add((string)row.ItemArray[0], (uint)row.ItemArray[1]);
                    }
                }
            }
            else
            {
                textBox.Text = "Can't find any possible formation in this zone!";
                return;
            }

            foreach (var leaderItr in creatureWithPathLinkedIds)
            {
                List<Waypoint> leaderWaypoints = new List<Waypoint>();

                DataSet waypointsDs = SQLModule.DatabaseSelectQuery($"SELECT `point`, `position_x`, `position_y`, `position_z` FROM `waypoint_data` WHERE `linked_id` = '{leaderItr.Key}';");

                if (waypointsDs != null && waypointsDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in waypointsDs.Tables["table"].Rows)
                    {
                        leaderWaypoints.Add(new Waypoint() { idFromParse = (uint)row.ItemArray[0], movePosition = new Position((float)row.ItemArray[1], (float)row.ItemArray[2], (float)row.ItemArray[3]) });
                    }
                }

                leaderWaypoints = leaderWaypoints.OrderBy(x => x.idFromParse).ToList();
                List<Waypoint> modifiedWaypoints = new List<Waypoint>();

                for (int i = 0; i < leaderWaypoints.Count; i++)
                {
                    Waypoint currWaypoint = leaderWaypoints[i];

                    if (i == 0)
                    {
                        modifiedWaypoints.Add(currWaypoint);
                    }
                    else
                    {
                        currWaypoint.idFromParse = modifiedWaypoints.Last().idFromParse + 1;
                    }

                    if (i + 1 < leaderWaypoints.Count)
                    {
                        Waypoint nextWaypoint = leaderWaypoints[i + 1];
                        float angle = currWaypoint.movePosition.GetAngle(nextWaypoint.movePosition);
                        int pointsToAddCount = (int)Math.Round(currWaypoint.movePosition.GetDistance(nextWaypoint.movePosition)) - 1;

                        for (int j = 0; j < pointsToAddCount; j++)
                        {
                            modifiedWaypoints.Add(new Waypoint() { idFromParse = currWaypoint.idFromParse + (uint)j + 1, movePosition = currWaypoint.movePosition.SimplePosXYRelocationByAngle(j + 1.0f, angle) });
                        }
                    }
                }

                Dictionary<string, KeyValuePair<uint, Position>> possibleFormationMembers = new Dictionary<string, KeyValuePair<uint, Position>>();
                DataSet creaturesDs = SQLModule.DatabaseSelectQuery($"SELECT `linked_id`, `id`, `position_x`, `position_y`, `position_z` FROM `creature` WHERE `MovementType` = 0 AND `zoneId` = {zoneId} AND `id` = {leaderItr.Value};");

                if (creaturesDs != null && creaturesDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in creaturesDs.Tables["table"].Rows)
                    {
                        DataSet leadersDs = SQLModule.DatabaseSelectQuery($"SELECT * FROM `creature_group_leaders` WHERE `LeaderLinkedId` = '{(string)row.ItemArray[0]}';");
                        DataSet membersDs = SQLModule.DatabaseSelectQuery($"SELECT * FROM `creature_group_members` WHERE `MemberLinkedId` = '{(string)row.ItemArray[0]}';");
                        DataSet memberWaypointsDs = SQLModule.DatabaseSelectQuery($"SELECT * FROM `waypoint_data` WHERE `linked_id` = '{(string)row.ItemArray[0]}';");

                        if ((leadersDs == null || leadersDs.Tables["table"].Rows.Count == 0) && (membersDs == null || membersDs.Tables["table"].Rows.Count == 0) && (memberWaypointsDs == null || memberWaypointsDs.Tables["table"].Rows.Count == 0))
                        {
                            possibleFormationMembers.Add((string)row.ItemArray[0], new KeyValuePair<uint, Position>((uint)row.ItemArray[1], new Position((float)row.ItemArray[2], (float)row.ItemArray[3], (float)row.ItemArray[4])));
                        }
                    }
                }

                foreach(var memberItr in possibleFormationMembers)
                {
                    foreach(Waypoint waypoint in modifiedWaypoints)
                    {
                        if (memberItr.Value.Value.GetDistance(waypoint.movePosition) <= 5.0f)
                        {
                            if (!output.Contains(leaderItr.Key))
                            {
                                if (output != "")
                                {
                                    output += "\r\n";
                                }

                                output += $"Found possible memebers for leader with LinkedId: {leaderItr.Key} (Name: {creatureNames[leaderItr.Value]}, Entry: {leaderItr.Value}):\r\n";
                                output += $"Member LinkedId: {memberItr.Key} (Name: {creatureNames[memberItr.Value.Key]}, Entry: {memberItr.Value.Key})\r\n";
                            }
                            else
                            {
                                output += $"Member LinkedId: {memberItr.Key} (Name: {creatureNames[memberItr.Value.Key]}, Entry: {memberItr.Value.Key})\r\n";
                            }

                            break;
                        }
                    }
                }
            }

            textBox.Text = output;
        }
    }
}
