using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Utils;
using static WoWDeveloperAssistant.Packets;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    public class WaypointsCreator
    {
        private MainForm mainForm;
        private BuildVersions buildVersion;
        private Dictionary<string, Creature> creaturesDict = new Dictionary<string, Creature>();

        public WaypointsCreator(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public bool GetDataFromSniffFile(string fileName)
        {
            mainForm.SetCurrentStatus("Getting lines...");

            var lines = File.ReadAllLines(fileName);
            SortedDictionary<long, Packet> updateObjectPacketsDict = new SortedDictionary<long, Packet>();
            SortedDictionary<long, Packet> movementPacketsDict = new SortedDictionary<long, Packet>();
            SortedDictionary<long, Packet> spellPacketsDict = new SortedDictionary<long, Packet>();
            SortedDictionary<long, Packet> auraPacketsDict = new SortedDictionary<long, Packet>();

            buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            mainForm.SetCurrentStatus("Searching for packet indexes in lines...");

            Parallel.For(0, lines.Length, index =>
            {
                if (Packet.GetPacketTypeFromLine(lines[index]) == PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (updateObjectPacketsDict)
                        {
                            if (!updateObjectPacketsDict.ContainsKey(index))
                                updateObjectPacketsDict.Add(index, new Packet(PacketTypes.SMSG_UPDATE_OBJECT, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Packet.GetPacketTypeFromLine(lines[index]) == PacketTypes.SMSG_ON_MONSTER_MOVE)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (movementPacketsDict)
                        {
                            if (!movementPacketsDict.ContainsKey(index))
                                movementPacketsDict.Add(index, new Packet(PacketTypes.SMSG_ON_MONSTER_MOVE, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Properties.Settings.Default.Scripts && Packet.GetPacketTypeFromLine(lines[index]) == PacketTypes.SMSG_SPELL_START)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (spellPacketsDict)
                        {
                            if (!spellPacketsDict.ContainsKey(index))
                                spellPacketsDict.Add(index, new Packet(PacketTypes.SMSG_SPELL_START, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Properties.Settings.Default.Scripts && Packet.GetPacketTypeFromLine(lines[index]) == PacketTypes.SMSG_AURA_UPDATE)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (auraPacketsDict)
                        {
                            if (!auraPacketsDict.ContainsKey(index))
                                auraPacketsDict.Add(index, new Packet(PacketTypes.SMSG_AURA_UPDATE, sendTime, index, new List<object>()));
                        }
                    }
                }
            });

            creaturesDict.Clear();

            mainForm.SetCurrentStatus("Parsing SMSG_UPDATE_OBJECT packets...");

            Parallel.ForEach(updateObjectPacketsDict.Values.AsEnumerable(), packet =>
            {
                Parallel.ForEach(ParseObjectUpdatePacket(lines, packet.index, buildVersion).AsEnumerable(), updatePacket =>
                {
                    lock (updateObjectPacketsDict)
                    {
                        updateObjectPacketsDict.AddSourceFromUpdatePacket(updatePacket, packet.index);
                    }

                    lock (creaturesDict)
                    {
                        if (!creaturesDict.ContainsKey(updatePacket.creatureGuid))
                        {
                            creaturesDict.Add(updatePacket.creatureGuid, new Creature(updatePacket));
                        }
                        else
                        {
                            creaturesDict[updatePacket.creatureGuid].UpdateCreature(updatePacket);
                        }
                    }
                });
            });

            mainForm.SetCurrentStatus("Parsing SMSG_ON_MONSTER_MOVE packets...");

            foreach (var packet in movementPacketsDict.Values)
            {
                MonsterMovePacket movePacket = ParseMovementPacket(lines, packet.index, buildVersion);
                if (movePacket.creatureGuid == "" || (!movePacket.HasWaypoints() && !movePacket.HasOrientation()))
                    continue;

                movementPacketsDict.AddSourceFromMovementPacket(movePacket, packet.index);

                if (creaturesDict.ContainsKey(movePacket.creatureGuid))
                {
                    Creature creature = creaturesDict[movePacket.creatureGuid];

                    if (!creature.HasWaypoints() && movePacket.HasWaypoints())
                    {
                        creature.AddWaypointsFromMovementPacket(movePacket);
                    }
                    else if (creature.HasWaypoints() && movePacket.HasOrientation())
                    {
                        creature.waypoints.Last().SetOrientation(movePacket.creatureOrientation);
                        creature.waypoints.Last().SetOrientationSetTime(movePacket.packetSendTime);
                    }
                    else if (creature.HasWaypoints() && movePacket.HasWaypoints())
                    {
                        if (creature.waypoints.Last().HasOrientation())
                        {
                            creature.waypoints.Last().SetDelay((uint)((movePacket.packetSendTime - creature.waypoints.Last().orientationSetTime).TotalMilliseconds));
                        }

                        creature.AddWaypointsFromMovementPacket(movePacket);
                    }
                }
            }

            if (Properties.Settings.Default.Scripts)
            {
                mainForm.SetCurrentStatus("Parsing SMSG_SPELL_START packets...");

                Parallel.ForEach(spellPacketsDict.Values.AsEnumerable(), packet =>
                {
                    SpellStartPacket spellPacket = ParseSpellStartPacket(lines, packet.index, buildVersion);
                    if (spellPacket.spellId == 0)
                        return;

                    lock (spellPacketsDict)
                    {
                        spellPacketsDict.AddSourceFromSpellPacket(spellPacket, packet.index);
                    }
                });

                mainForm.SetCurrentStatus("Parsing SMSG_AURA_UPDATE packets...");

                Parallel.ForEach(auraPacketsDict.Values.AsEnumerable(), packet =>
                {
                    Parallel.ForEach(ParseAuraUpdatePacket(lines, packet.index, buildVersion).AsEnumerable(), auraPacket =>
                    {
                        lock (auraPacketsDict)
                        {
                            auraPacketsDict.AddSourceFromAuraUpdatePacket(auraPacket, packet.index);
                        }

                        lock (creaturesDict)
                        {
                            if (creaturesDict.ContainsKey(auraPacket.unitGuid))
                            {
                                Creature creature = creaturesDict[auraPacket.unitGuid];

                                creature.auras.Add(new Aura((uint)auraPacket.slot, (bool)auraPacket.HasAura, auraPacket.packetSendTime, auraPacket.spellId));
                            }
                        }
                    });
                });

                mainForm.SetCurrentStatus("Creating waypoint scripts for creatures...");

                Parallel.ForEach(creaturesDict.Values.AsEnumerable(), creature =>
                {
                    if (creature.HasWaypoints())
                    {
                        SortedDictionary<long, Packet> creaturePacketsDict = new SortedDictionary<long, Packet>();

                        foreach (Packet packet in updateObjectPacketsDict.Values)
                        {
                            if (packet.HasCreatureWithGuid(creature.guid))
                            {
                                creaturePacketsDict.Add(packet.index, packet);
                            }
                        }

                        foreach (Packet packet in movementPacketsDict.Values)
                        {
                            if (packet.HasCreatureWithGuid(creature.guid))
                            {
                                creaturePacketsDict.Add(packet.index, packet);
                            }
                        }

                        foreach (Packet packet in spellPacketsDict.Values)
                        {
                            if (packet.HasCreatureWithGuid(creature.guid))
                            {
                                creaturePacketsDict.Add(packet.index, packet);
                            }
                        }

                        foreach (Packet packet in auraPacketsDict.Values)
                        {
                            if (packet.HasCreatureWithGuid(creature.guid))
                            {
                                creaturePacketsDict.Add(packet.index, packet);
                            }
                        }

                        List<WaypointScript> scriptsList = new List<WaypointScript>();
                        MonsterMovePacket startMovePacket = new MonsterMovePacket();
                        bool scriptsParsingStarted = false;

                        foreach (Packet packet in creaturePacketsDict.Values)
                        {
                            switch (packet.packetType)
                            {
                                case PacketTypes.SMSG_ON_MONSTER_MOVE:
                                    {
                                        MonsterMovePacket movePacket = (MonsterMovePacket)packet.parsedPacketsList.First();
                                        if (movePacket.HasWaypoints() && !scriptsParsingStarted)
                                        {
                                            startMovePacket = movePacket;
                                            scriptsParsingStarted = true;
                                        }
                                        else if (movePacket.HasWaypoints() && scriptsParsingStarted)
                                        {
                                            if (scriptsList.Count != 0)
                                            {
                                                creature.AddScriptsForWaypoints(scriptsList, startMovePacket, movePacket);
                                                scriptsList.Clear();
                                            }

                                            startMovePacket = movePacket;
                                        }
                                        else if (movePacket.HasOrientation() && scriptsParsingStarted)
                                        {
                                            scriptsList.Add(WaypointScript.GetScriptsFromMovementPacket(movePacket));
                                        }

                                        break;
                                    }
                                case PacketTypes.SMSG_UPDATE_OBJECT:
                                    {
                                        if (scriptsParsingStarted && packet.parsedPacketsList.Count != 0)
                                        {
                                            if (packet.parsedPacketsList.GetUpdatePacketForCreatureWithGuid(creature.guid) != null)
                                            {
                                                UpdateObjectPacket updatePacket = (UpdateObjectPacket)packet.parsedPacketsList.GetUpdatePacketForCreatureWithGuid(creature.guid);

                                                List<WaypointScript> updateScriptsList = WaypointScript.GetScriptsFromUpdatePacket(updatePacket);
                                                if (updateScriptsList.Count != 0)
                                                {
                                                    foreach (WaypointScript script in updateScriptsList)
                                                    {
                                                        scriptsList.Add(script);
                                                    }
                                                }
                                            }
                                        }

                                        break;
                                    }
                                case PacketTypes.SMSG_SPELL_START:
                                    {
                                        if (scriptsParsingStarted)
                                        {
                                            SpellStartPacket spellPacket = (SpellStartPacket)packet.parsedPacketsList.First();
                                            scriptsList.Add(WaypointScript.GetScriptsFromSpellPacket(spellPacket));
                                        }

                                        break;
                                    }
                                case PacketTypes.SMSG_AURA_UPDATE:
                                    {
                                        if (scriptsParsingStarted)
                                        {
                                            AuraUpdatePacket auraPacket = (AuraUpdatePacket)packet.parsedPacketsList.First();
                                            if (auraPacket.HasAura == false)
                                            {
                                                scriptsList.Add(WaypointScript.GetScriptsFromAuraUpdatePacket(auraPacket, creature));
                                            }
                                        }

                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                });
            }

            mainForm.SetCurrentStatus("");
            return true;
        }

        public void FillListBoxWithGuids()
        {
            mainForm.listBox_WC_CreatureGuids.Items.Clear();
            mainForm.grid_WC_Waypoints.Rows.Clear();

            foreach (Creature creature in creaturesDict.Values)
            {
                if (!creature.HasWaypoints())
                    continue;

                if (mainForm.toolStripTextBox_WC_Entry.Text != "" && mainForm.toolStripTextBox_WC_Entry.Text != "0")
                {
                    if (mainForm.toolStripTextBox_WC_Entry.Text == creature.entry.ToString() ||
                        mainForm.toolStripTextBox_WC_Entry.Text == creature.guid)
                    {
                        mainForm.listBox_WC_CreatureGuids.Items.Add(creature.guid);
                    }
                }
                else
                {
                    mainForm.listBox_WC_CreatureGuids.Items.Add(creature.guid);
                }
            }

            mainForm.listBox_WC_CreatureGuids.Refresh();
            mainForm.listBox_WC_CreatureGuids.Enabled = true;
        }

        public void FillWaypointsGrid()
        {
            if (mainForm.listBox_WC_CreatureGuids.SelectedItem == null)
                return;

            Creature creature = creaturesDict[mainForm.listBox_WC_CreatureGuids.SelectedItem.ToString()];

            mainForm.grid_WC_Waypoints.Rows.Clear();

            uint index = 1;

            if (creature.waypoints.Count >= 1000)
                RemoveDuplicatePoints(creature.waypoints);

            foreach (Waypoint wp in creature.waypoints)
            {
                mainForm.grid_WC_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime, wp.delay, wp.HasScripts(), wp.Clone());
                index++;
            }

            GraphPath();

            mainForm.grid_WC_Waypoints.Enabled = true;
        }

        public void GraphPath()
        {
            Creature creature = creaturesDict[mainForm.listBox_WC_CreatureGuids.SelectedItem.ToString()];

            mainForm.chart_WC.BackColor = Color.White;
            mainForm.chart_WC.ChartAreas[0].BackColor = Color.White;
            mainForm.chart_WC.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            mainForm.chart_WC.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            mainForm.chart_WC.ChartAreas[0].AxisY.IsReversed = true;
            mainForm.chart_WC.Titles.Clear();
            mainForm.chart_WC.Titles.Add(creature.name + " Entry: " + creature.entry);
            mainForm.chart_WC.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            mainForm.chart_WC.Titles[0].ForeColor = Color.Blue;
            mainForm.chart_WC.Titles.Add("Linked Id: " + creature.GetLinkedId());
            mainForm.chart_WC.Titles[1].Font = new Font("Arial", 16, FontStyle.Bold);
            mainForm.chart_WC.Titles[1].ForeColor = Color.Blue;
            mainForm.chart_WC.Series.Clear();
            mainForm.chart_WC.Series.Add("Path");
            mainForm.chart_WC.Series["Path"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            mainForm.chart_WC.Series.Add("Line");
            mainForm.chart_WC.Series["Line"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            for (var i = 0; i < mainForm.grid_WC_Waypoints.RowCount; i++)
            {
                double posX = Convert.ToDouble(mainForm.grid_WC_Waypoints[1, i].Value);
                double posY = Convert.ToDouble(mainForm.grid_WC_Waypoints[2, i].Value);

                mainForm.chart_WC.Series["Path"].Points.AddXY(posX, posY);
                mainForm.chart_WC.Series["Path"].Points[i].Color = Color.Blue;
                mainForm.chart_WC.Series["Path"].Points[i].Label = Convert.ToString(i + 1);
                mainForm.chart_WC.Series["Line"].Points.AddXY(posX, posY);
                mainForm.chart_WC.Series["Line"].Points[i].Color = Color.Cyan;
            }
        }

        public void CutFromGrid()
        {
            foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.SelectedRows)
            {
                mainForm.grid_WC_Waypoints.Rows.Remove(row);
            }

            for (int i = 0; i < mainForm.grid_WC_Waypoints.Rows.Count; i++)
            {
                mainForm.grid_WC_Waypoints[0, i].Value = i + 1;
            }

            GraphPath();
        }

        public void CreateSQL()
        {
            Creature creature = creaturesDict[mainForm.listBox_WC_CreatureGuids.SelectedItem.ToString()];
            DataSet creatureAddonDs;
            string sqlQuery = "SELECT * FROM `creature_addon` WHERE `linked_id` = '" + creature.GetLinkedId() + "';";
            string creatureAddon = "";
            bool addonFound = false;
            creatureAddonDs = SQLModule.DatabaseSelectQuery(sqlQuery);

            if (creatureAddonDs != null && creatureAddonDs.Tables["table"].Rows.Count > 0)
            {
                creatureAddon = "UPDATE `creature_addon` SET `path_id` = @PATH WHERE `linked_id` = '" + creature.GetLinkedId() + "';" + "\r\n";
                addonFound = true;
            }
            else
            {
                creatureAddon = "('" + creature.GetLinkedId() + "', @PATH, 0, 0, 1, 0, 0, 0, 0, '', -1); " + "\r\n";
            }

            if (Properties.Settings.Default.Scripts)
            {
                List<Waypoint> waypoints = new List<Waypoint>();
                foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.Rows)
                {
                    Waypoint waypoint = (Waypoint)row.Cells[8].Value;
                    waypoints.Add(waypoint);
                }

                if (creature.waypoints.Count != waypoints.Count)
                {
                    waypoints.RecalculateIdsAndGuids(creature.entry);
                }
            }

            string SQLtext = "-- Pathing for " + creature.name + " Entry: " + creature.entry + "\r\n";
            SQLtext = SQLtext + "SET @GUID := (SELECT `guid` FROM `creature` WHERE `linked_id` = " + "'" + creature.GetLinkedId() + "'" + ");" + "\r\n";
            SQLtext = SQLtext + "SET @PATH := @GUID * 10;" + "\r\n";
            SQLtext = SQLtext + "UPDATE `creature` SET `spawndist` = 0, `MovementType` = 2 WHERE `linked_id` = '" + creature.GetLinkedId() + "'; " + "\r\n";

            if (addonFound)
            {
                SQLtext = SQLtext + creatureAddon;
            }
            else
            {
                SQLtext = SQLtext + "DELETE FROM `creature_addon` WHERE `linked_id` = '" + creature.GetLinkedId() + "';" + "\r\n";
                SQLtext = SQLtext + "INSERT INTO `creature_addon` (`linked_id`, `path_id`, `mount`, `bytes1`, `bytes2`, `emote`, `AiAnimKit`, `MovementAnimKit`, `MeleeAnimKit`, `auras`, `VerifiedBuild`) VALUES" + "\r\n";
                SQLtext = SQLtext + creatureAddon;
            }

            SQLtext = SQLtext + "DELETE FROM `waypoint_data` WHERE `id` = @PATH;" + "\r\n";
            SQLtext = SQLtext + "INSERT INTO `waypoint_data` (`id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`, `move_type`, `action`, `action_chance`, `speed`) VALUES" + "\r\n";

            for (int i = 0; i < mainForm.grid_WC_Waypoints.RowCount; i++)
            {
                Waypoint waypoint = (Waypoint)mainForm.grid_WC_Waypoints[8, i].Value;

                if (i < (mainForm.grid_WC_Waypoints.RowCount - 1))
                {
                    SQLtext = SQLtext + "(@PATH, " + (i + 1) + ", " + waypoint.movePosition.x.GetValueWithoutComma() + ", " + waypoint.movePosition.y.GetValueWithoutComma() + ", " + waypoint.movePosition.z.GetValueWithoutComma() + ", " + waypoint.orientation.GetValueWithoutComma() + ", " + waypoint.delay + ", 0" + ", " + waypoint.GetScriptId() + ", 100" + ", 0" + "),\r\n";
                }
                else
                {
                    SQLtext = SQLtext + "(@PATH, " + (i + 1) + ", " + waypoint.movePosition.x.GetValueWithoutComma() + ", " + waypoint.movePosition.y.GetValueWithoutComma() + ", " + waypoint.movePosition.z.GetValueWithoutComma() + ", " + waypoint.orientation.GetValueWithoutComma() + ", " + waypoint.delay + ", 0" + ", " + waypoint.GetScriptId() + ", 100" + ", 0" + ");\r\n";
                }
            }

            SQLtext = SQLtext + "-- " + creature.guid + " .go " + creature.spawnPosition.x.GetValueWithoutComma() + " " + creature.spawnPosition.y.GetValueWithoutComma() + " " + creature.spawnPosition.z.GetValueWithoutComma() + "\r\n";

            if (Properties.Settings.Default.Scripts)
            {
                List<Waypoint> waypoints = new List<Waypoint>();
                foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.Rows)
                {
                    Waypoint waypoint = (Waypoint)row.Cells[8].Value;
                    waypoints.Add(waypoint);
                }

                if (creature.waypoints.Count != waypoints.Count)
                {
                    waypoints.RecalculateIdsAndGuids(creature.entry);
                }

                SQLtext += "\r\n";
                SQLtext += "-- Waypoint scripts for " + creature.name + " Entry: " + creature.entry + "\r\n";
                SQLtext = SQLtext + "DELETE FROM `waypoint_scripts` WHERE `id` IN (" + waypoints.GetScriptIds() + ");\r\n";
                SQLtext = SQLtext + "INSERT INTO `waypoint_scripts` (`id`, `delay`, `command`, `datalong`, `datalong2`, `dataint`, `x`, `y`, `z`, `o`, `guid`) VALUES" + "\r\n";

                uint scriptsCount = waypoints.GetScriptsCount() - 1;

                for (int i = 0; i < mainForm.grid_WC_Waypoints.RowCount; i++)
                {
                    Waypoint waypoint = (Waypoint)mainForm.grid_WC_Waypoints[8, i].Value;

                    foreach (WaypointScript script in waypoint.scripts)
                    {
                        if (scriptsCount != 0)
                        {
                            SQLtext = SQLtext + "(" + script.id + ", " + script.delay + ", " + (uint)script.type + ", " + script.dataLong + ", " + script.dataLongSecond + ", " + script.dataInt + ", " + script.x.GetValueWithoutComma() + ", " + script.y.GetValueWithoutComma() + ", " + script.z.GetValueWithoutComma() + ", " + script.o.GetValueWithoutComma() + ", " + script.guid + "),\r\n";
                            scriptsCount--;
                        }
                        else
                        {
                            SQLtext = SQLtext + "(" + script.id + ", " + script.delay + ", " + (uint)script.type + ", " + script.dataLong + ", " + script.dataLongSecond + ", " + script.dataInt + ", " + script.x.GetValueWithoutComma() + ", " + script.y.GetValueWithoutComma() + ", " + script.z.GetValueWithoutComma() + ", " + script.o.GetValueWithoutComma() + ", " + script.guid + ");\r\n";
                        }
                    }
                }
            }

            if (Properties.Settings.Default.Vector)
            {
                SQLtext += "\r\n";
                SQLtext += "-- Vector3 for movement in core for " + creature.name + " Entry: " + creature.entry + "\r\n";
                SQLtext = SQLtext + "G3D::Vector3 const Path_XXX[" + mainForm.grid_WC_Waypoints.RowCount + "] =" + "\r\n";
                SQLtext = SQLtext + "{" + "\r\n";

                for (var i = 0; i < mainForm.grid_WC_Waypoints.RowCount; i++)
                {
                    Waypoint waypoint = (Waypoint)mainForm.grid_WC_Waypoints[8, i].Value;

                    if (i < (mainForm.grid_WC_Waypoints.RowCount - 1))
                    {
                        SQLtext = SQLtext + "{ " + waypoint.movePosition.x.GetValueWithoutComma() + "f, " + waypoint.movePosition.y.GetValueWithoutComma() + "f, " + waypoint.movePosition.z.GetValueWithoutComma() + "f },\r\n";
                    }
                    else
                    {
                        SQLtext = SQLtext + "{ " + waypoint.movePosition.x.GetValueWithoutComma() + "f, " + waypoint.movePosition.y.GetValueWithoutComma() + "f, " + waypoint.movePosition.z.GetValueWithoutComma() + "f };\r\n";
                    }
                }

                SQLtext = SQLtext + "};" + "\r\n";

                mainForm.textBox_SQLOutput.Text = SQLtext;
            }
        }

        public void RemoveNearestPoints()
        {
            bool canLoop = true;

            do
            {
                foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.Rows)
                {
                    Waypoint currentWaypoint = (Waypoint)row.Cells[8].Value;
                    Waypoint nextWaypoint;
                    try
                    {
                        nextWaypoint = (Waypoint)mainForm.grid_WC_Waypoints.Rows[row.Index + 1].Cells[8].Value;
                    }
                    catch
                    {
                        canLoop = false;
                        break;
                    }

                    if (currentWaypoint.movePosition.GetExactDist2d(nextWaypoint.movePosition) <= 5.0f &&
                        !nextWaypoint.HasOrientation())
                    {
                        mainForm.grid_WC_Waypoints.Rows.RemoveAt(row.Index + 1);
                        break;
                    }
                }
            }
            while (canLoop);

            for (int i = 0; i < mainForm.grid_WC_Waypoints.Rows.Count; i++)
            {
                mainForm.grid_WC_Waypoints[0, i].Value = i + 1;
            }

            GraphPath();
        }

        public void RemoveDuplicatePoints()
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            List<string> hashList = new List<string>();

            foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.Rows)
            {
                Waypoint waypoint = (Waypoint)row.Cells[8].Value;
                string hash = SHA1HashStringForUTF8String(Convert.ToString(Math.Round(float.Parse(row.Cells[1].Value.ToString()) / 0.25), CultureInfo.InvariantCulture) + " " + Convert.ToString(Math.Round(float.Parse(row.Cells[2].Value.ToString()) / 0.25), CultureInfo.InvariantCulture) + " " + Convert.ToString(Math.Round(float.Parse(row.Cells[3].Value.ToString()) / 0.25), CultureInfo.InvariantCulture));

                if (!hashList.Contains(hash) || waypoint.HasOrientation())
                {
                    hashList.Add(hash);
                    waypoints.Add(waypoint);
                }
            }

            mainForm.grid_WC_Waypoints.Rows.Clear();

            uint index = 1;

            foreach (Waypoint wp in waypoints)
            {
                mainForm.grid_WC_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime, wp.delay, wp.HasScripts(), wp);
                index++;
            }

            GraphPath();
        }

        public void RemoveDuplicatePoints(List<Waypoint> waypoints)
        {
            List<Waypoint> waypointList = new List<Waypoint>();
            List<string> hashList = new List<string>();

            foreach (Waypoint waypoint in waypoints)
            {
                string hash = SHA1HashStringForUTF8String(Convert.ToString(Math.Round(waypoint.movePosition.x / 0.25)) + " " + Convert.ToString(Math.Round(waypoint.movePosition.y / 0.25)) + " " + Convert.ToString(Math.Round(waypoint.movePosition.z / 0.25)));

                if (!hashList.Contains(hash) || waypoint.HasOrientation())
                {
                    hashList.Add(hash);
                    waypointList.Add(waypoint);
                }
            }

            waypoints.Clear();

            foreach (Waypoint wp in waypointList)
            {
                waypoints.Add(wp);
            }
        }

        public void CreateReturnPath()
        {
            List<Waypoint> waypoints = new List<Waypoint>();

            foreach (DataGridViewRow row in mainForm.grid_WC_Waypoints.Rows)
            {
                waypoints.Add((Waypoint)row.Cells[8].Value);
            }

            waypoints.Reverse();

            waypoints.RemoveAt(0);
            waypoints.RemoveAt(waypoints.Count - 1);

            int index = mainForm.grid_WC_Waypoints.Rows.Count + 1;

            foreach (Waypoint wp in waypoints)
            {
                mainForm.grid_WC_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime, wp.delay, wp.HasScripts(), wp);
                index++;
            }

            GraphPath();
        }

        public uint GetCreatureEntryByGuid(string creatureGuid)
        {
            if (creaturesDict.ContainsKey(creatureGuid))
                return creaturesDict[creatureGuid].entry;

            return 0;
        }

        public void OpenFileDialog()
        {
            mainForm.openFileDialog.Title = "Open File";
            mainForm.openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            mainForm.openFileDialog.FileName = "*.txt";
            mainForm.openFileDialog.FilterIndex = 1;
            mainForm.openFileDialog.ShowReadOnly = false;
            mainForm.openFileDialog.Multiselect = false;
            mainForm.openFileDialog.CheckFileExists = true;
        }

        public void ImportStarted()
        {
            mainForm.Cursor = Cursors.WaitCursor;
            mainForm.toolStripButton_WC_LoadSniff.Enabled = false;
            mainForm.toolStripButton_WC_Search.Enabled = false;
            mainForm.toolStripTextBox_WC_Entry.Enabled = false;
            mainForm.listBox_WC_CreatureGuids.Enabled = false;
            mainForm.listBox_WC_CreatureGuids.Items.Clear();
            mainForm.listBox_WC_CreatureGuids.DataSource = null;
            mainForm.grid_WC_Waypoints.Enabled = false;
            mainForm.grid_WC_Waypoints.Rows.Clear();
            mainForm.toolStripStatusLabel_FileStatus.Text = "Loading File...";
        }

        public void ImportSuccessful()
        {
            mainForm.toolStripStatusLabel_CurrentAction.Text = "";
            mainForm.toolStripButton_WC_LoadSniff.Enabled = true;
            mainForm.toolStripButton_WC_Search.Enabled = true;
            mainForm.toolStripTextBox_WC_Entry.Enabled = true;
            mainForm.toolStripStatusLabel_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.Cursor = Cursors.Default;
        }
    }
}
