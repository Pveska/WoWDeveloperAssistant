using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Utils;
using static WoWDeveloperAssistant.Misc.Packets;
using System.Runtime.Serialization.Formatters.Binary;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    public class WaypointsCreator
    {
        private MainForm mainForm;
        private Dictionary<string, Creature> creaturesDict = new Dictionary<string, Creature>();
        private BuildVersions buildVersion;

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
            SortedDictionary<long, Packet> emotePacketsDict = new SortedDictionary<long, Packet>();
            SortedDictionary<long, Packet> attackStopPacketsDict = new SortedDictionary<long, Packet>();

            buildVersion = LineGetters.GetBuildVersion(lines);
            if (buildVersion == BuildVersions.BUILD_UNKNOWN)
            {
                MessageBox.Show(fileName + " has non-supported build.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            mainForm.SetCurrentStatus("Searching for packet indexes in lines...");

            Parallel.For(0, lines.Length, index =>
            {
                if (Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_UPDATE_OBJECT)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (updateObjectPacketsDict)
                        {
                            if (!updateObjectPacketsDict.ContainsKey(index))
                                updateObjectPacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_UPDATE_OBJECT, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_ON_MONSTER_MOVE)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (movementPacketsDict)
                        {
                            if (!movementPacketsDict.ContainsKey(index))
                                movementPacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_ON_MONSTER_MOVE, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Properties.Settings.Default.Scripts && Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_SPELL_START)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (spellPacketsDict)
                        {
                            if (!spellPacketsDict.ContainsKey(index))
                                spellPacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_SPELL_START, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Properties.Settings.Default.Scripts && Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_AURA_UPDATE)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (auraPacketsDict)
                        {
                            if (!auraPacketsDict.ContainsKey(index))
                                auraPacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_AURA_UPDATE, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Properties.Settings.Default.Scripts && Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_EMOTE)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (emotePacketsDict)
                        {
                            if (!emotePacketsDict.ContainsKey(index))
                                emotePacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_EMOTE, sendTime, index, new List<object>()));
                        }
                    }
                }
                else if (Packet.GetPacketTypeFromLine(lines[index]) == Packet.PacketTypes.SMSG_ATTACK_STOP)
                {
                    TimeSpan sendTime = LineGetters.GetTimeSpanFromLine(lines[index]);
                    if (sendTime != TimeSpan.Zero)
                    {
                        lock (attackStopPacketsDict)
                        {
                            if (!attackStopPacketsDict.ContainsKey(index))
                                attackStopPacketsDict.Add(index, new Packet(Packet.PacketTypes.SMSG_ATTACK_STOP, sendTime, index, new List<object>()));
                        }
                    }
                }
            });

            creaturesDict.Clear();

            mainForm.SetCurrentStatus("Parsing SMSG_UPDATE_OBJECT packets...");

            Parallel.ForEach(updateObjectPacketsDict.Values.AsEnumerable(), packet =>
            {
                Parallel.ForEach(UpdateObjectPacket.ParseObjectUpdatePacket(lines, packet.index, buildVersion).AsEnumerable(), updatePacket =>
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

            Parallel.ForEach(movementPacketsDict.Values.AsEnumerable(), packet =>
            {
                MonsterMovePacket movePacket = MonsterMovePacket.ParseMovementPacket(lines, packet.index, buildVersion, updateObjectPacketsDict);
                if (movePacket.creatureGuid != "" && (movePacket.HasWaypoints() || movePacket.HasOrientation() || movePacket.HasJump()))
                {
                    lock (movementPacketsDict)
                    {
                        movementPacketsDict.AddSourceFromMovementPacket(movePacket, packet.index);
                    }

                    lock (creaturesDict)
                    {
                        if (creaturesDict.ContainsKey(movePacket.creatureGuid))
                        {
                            Creature creature = creaturesDict[movePacket.creatureGuid];

                            if (!creature.HasWaypoints() && movePacket.HasWaypoints())
                            {
                                creature.AddWaypointsFromMovementPacket(movePacket);
                            }
                            else if (creature.HasWaypoints() && movePacket.HasOrientation() && !movePacket.HasWaypoints())
                            {
                                creature.SortWaypoints();
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
                }
            });

            if (Properties.Settings.Default.CombatMovement)
            {
                mainForm.SetCurrentStatus("Parsing SMSG_ATTACK_STOP packets...");

                Parallel.ForEach(attackStopPacketsDict.Values.AsEnumerable(), packet =>
                {
                    AttackStopPacket attackStopPacket = AttackStopPacket.ParseAttackStopkPacket(lines, packet.index, buildVersion);
                    if (attackStopPacket.creatureGuid == "")
                        return;

                    lock (attackStopPacketsDict)
                    {
                        attackStopPacketsDict.AddSourceFromAttackStopPacket(attackStopPacket, packet.index);
                    }
                });

                RemoveCombatMovementForCreatures(attackStopPacketsDict, updateObjectPacketsDict);
            }

            if (Properties.Settings.Default.Scripts)
            {
                mainForm.SetCurrentStatus("Parsing SMSG_SPELL_START packets...");

                Parallel.ForEach(spellPacketsDict.Values.AsEnumerable(), packet =>
                {
                    SpellStartPacket spellPacket = SpellStartPacket.ParseSpellStartPacket(lines, packet.index, buildVersion);
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
                    Parallel.ForEach(AuraUpdatePacket.ParseAuraUpdatePacket(lines, packet.index, buildVersion).AsEnumerable(), auraPacket =>
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

                mainForm.SetCurrentStatus("Parsing SMSG_EMOTE packets...");

                Parallel.ForEach(emotePacketsDict.Values.AsEnumerable(), packet =>
                {
                    EmotePacket emotePacket = EmotePacket.ParseEmotePacket(lines, packet.index, buildVersion);
                    if (emotePacket.guid == "" || emotePacket.emoteId == 0)
                        return;

                    lock (emotePacketsDict)
                    {
                        emotePacketsDict.AddSourceFromEmotePacket(emotePacket, packet.index);
                    }
                });

                mainForm.SetCurrentStatus("Creating waypoint scripts for creatures...");

                Parallel.ForEach(creaturesDict.Values.AsEnumerable(), creature =>
                {
                    if (creature.HasWaypoints())
                    {
                        SortedDictionary<long, Packet> creaturePacketsDict = new SortedDictionary<long, Packet>();

                        foreach (var packet in updateObjectPacketsDict.Values.Where(packet => packet.HasCreatureWithGuid(creature.guid)))
                        {
                            creaturePacketsDict.Add(packet.index, packet);
                        }

                        foreach (var packet in movementPacketsDict.Values.Where(packet => packet.HasCreatureWithGuid(creature.guid)))
                        {
                            creaturePacketsDict.Add(packet.index, packet);
                        }

                        foreach (var packet in spellPacketsDict.Values.Where(packet => packet.HasCreatureWithGuid(creature.guid)))
                        {
                            creaturePacketsDict.Add(packet.index, packet);
                        }

                        foreach (var packet in auraPacketsDict.Values.Where(packet => packet.HasCreatureWithGuid(creature.guid)))
                        {
                            creaturePacketsDict.Add(packet.index, packet);
                        }

                        foreach (var packet in emotePacketsDict.Values.Where(packet => packet.HasCreatureWithGuid(creature.guid)))
                        {
                            creaturePacketsDict.Add(packet.index, packet);
                        }

                        List<WaypointScript> scriptsList = new List<WaypointScript>();
                        MonsterMovePacket startMovePacket = new MonsterMovePacket();
                        bool scriptsParsingStarted = false;

                        foreach (Packet packet in creaturePacketsDict.Values)
                        {
                            switch (packet.packetType)
                            {
                                case Packet.PacketTypes.SMSG_ON_MONSTER_MOVE:
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
                                    else if ((movePacket.HasOrientation() || movePacket.HasJump()) && scriptsParsingStarted)
                                    {
                                        scriptsList.Add(WaypointScript.GetScriptsFromMovementPacket(movePacket));
                                    }

                                    break;
                                }
                                case Packet.PacketTypes.SMSG_UPDATE_OBJECT:
                                {
                                    if (scriptsParsingStarted && packet.parsedPacketsList.Count != 0)
                                    {
                                        if (packet.parsedPacketsList.GetUpdatePacketForCreatureWithGuid(creature.guid) != null)
                                        {
                                            UpdateObjectPacket updatePacket = (UpdateObjectPacket)packet.parsedPacketsList.GetUpdatePacketForCreatureWithGuid(creature.guid);

                                            List<WaypointScript> updateScriptsList = WaypointScript.GetScriptsFromUpdatePacket(updatePacket);
                                            if (updateScriptsList.Count != 0)
                                            {
                                                scriptsList.AddRange(updateScriptsList);
                                            }
                                        }
                                    }

                                    break;
                                }
                                case Packet.PacketTypes.SMSG_SPELL_START:
                                {
                                    if (scriptsParsingStarted)
                                    {
                                        SpellStartPacket spellPacket = (SpellStartPacket)packet.parsedPacketsList.First();
                                        scriptsList.Add(WaypointScript.GetScriptsFromSpellPacket(spellPacket));
                                    }

                                    break;
                                }
                                case Packet.PacketTypes.SMSG_AURA_UPDATE:
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
                                case Packet.PacketTypes.SMSG_EMOTE:
                                {
                                    if (scriptsParsingStarted)
                                    {
                                        EmotePacket emotePacket = (EmotePacket)packet.parsedPacketsList.First();
                                        scriptsList.Add(WaypointScript.GetScriptsFromEmotePacket(emotePacket));
                                    }

                                    break;
                                }
                            }
                        }
                    }
                });
            }

            if (mainForm.checkBox_WaypointsCreator_CreateDataFile.Checked)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                using (FileStream fileStream = new FileStream(fileName.Replace("_parsed.txt", "_waypoint_packets.dat"), FileMode.OpenOrCreate))
                {
                    Dictionary<uint, object> dictToSerialize = new Dictionary<uint, object>();

                    dictToSerialize.Add(0, creaturesDict);
                    dictToSerialize.Add(1, buildVersion);

                    binaryFormatter.Serialize(fileStream, dictToSerialize);
                }
            }

            mainForm.SetCurrentStatus("");
            return true;
        }

        public bool GetPacketsFromDataFile(string fileName)
        {
            mainForm.SetCurrentStatus("Current status: Getting packets from data file...");

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Dictionary<uint, object> dictFromSerialize = new Dictionary<uint, object>();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                dictFromSerialize = (Dictionary<uint, object>)binaryFormatter.Deserialize(fileStream);
            }

            creaturesDict = (Dictionary<string, Creature>)dictFromSerialize[0];
            buildVersion = (BuildVersions)dictFromSerialize[1];

            return true;
        }

        private void RemoveCombatMovementForCreatures(SortedDictionary<long, Packet> attackStopPackets, SortedDictionary<long, Packet> updateObjectPackets)
        {
            foreach (Creature creature in creaturesDict.Values)
            {
                if (creature.HasWaypoints())
                {
                    List<uint> attackStopPacketTimes = attackStopPackets.Where(x => x.Value.HasCreatureWithGuid(creature.guid)).Select(x => (uint)x.Value.sendTime.TotalSeconds).ToList();
                    if (attackStopPacketTimes.Count == 0)
                        return;

                    List<Waypoint> newWaypoints = new List<Waypoint>();

                    foreach (Waypoint waypoint in creature.waypoints)
                    {
                        if (!attackStopPacketTimes.Contains((uint)waypoint.moveStartTime.TotalSeconds))
                        {
                            newWaypoints.Add(waypoint);
                        }
                    }

                    if (creature.waypoints.Count != newWaypoints.Count)
                    {
                        creature.waypoints = newWaypoints;

                    }
                }
            }
        }

        public void FillListBoxWithGuids()
        {
            bool dataFoundOnCurrentList = false;

            if (mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Count != 0)
            {
                if (mainForm.toolStripTextBox_WaypointsCreator_Entry.Text != "" && mainForm.toolStripTextBox_WaypointsCreator_Entry.Text != "0")
                {
                    for (int i = 0; i < mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Count; i++)
                    {
                        if (mainForm.listBox_WaypointsCreator_CreatureGuids.Items[i].ToString() == mainForm.toolStripTextBox_WaypointsCreator_Entry.Text ||
                            creaturesDict[mainForm.listBox_WaypointsCreator_CreatureGuids.Items[i].ToString()].GetLinkedId() == mainForm.toolStripTextBox_WaypointsCreator_Entry.Text)
                        {
                            dataFoundOnCurrentList = true;
                            mainForm.listBox_WaypointsCreator_CreatureGuids.SetSelected(i, true);
                            break;
                        }
                    }
                }
            }

            if (!dataFoundOnCurrentList)
            {
                mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Clear();
                mainForm.grid_WaypointsCreator_Waypoints.Rows.Clear();

                foreach (Creature creature in creaturesDict.Values.OrderBy(x => x.lastUpdatePacketTime))
                {
                    if (!creature.HasWaypoints() || (Properties.Settings.Default.CheckDataOnDb && IsCreatureAlreadyHaveDataOnDb(creature.guid)) ||
                        (Properties.Settings.Default.Critters && creature.IsCritter()))
                        continue;

                    if (mainForm.toolStripTextBox_WaypointsCreator_Entry.Text != "" && mainForm.toolStripTextBox_WaypointsCreator_Entry.Text != "0")
                    {
                        if (mainForm.toolStripTextBox_WaypointsCreator_Entry.Text == creature.entry.ToString() ||
                            mainForm.toolStripTextBox_WaypointsCreator_Entry.Text == creature.guid ||
                            mainForm.toolStripTextBox_WaypointsCreator_Entry.Text == creature.GetLinkedId())
                        {
                            mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Add(creature.guid);
                        }
                    }
                    else
                    {
                        mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Add(creature.guid);
                    }
                }
            }

            mainForm.listBox_WaypointsCreator_CreatureGuids.Refresh();
            mainForm.listBox_WaypointsCreator_CreatureGuids.Enabled = true;
        }

        public void RemoveGuidsWithExistingDataFromListBox()
        {
            if (mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedIndex == -1)
            {
                mainForm.listBox_WaypointsCreator_CreatureGuids.SetSelected(0, true);
            }

            List<string> linkedIdsToRemove = GetExistedLinkedIdsFromListBox();
            List<object> listBoxOriginalItems = mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Cast<object>().ToList();
            string currentSelectedGuid = mainForm.listBox_WaypointsCreator_CreatureGuids.Items[mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedIndex].ToString();

            if (linkedIdsToRemove.Count != 0)
            {
                object[] items = mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Cast<object>().Where(x => !linkedIdsToRemove.Contains(creaturesDict[x.ToString()].GetLinkedId())).ToArray();
                bool guidFound = false;

                mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Clear();
                mainForm.listBox_WaypointsCreator_CreatureGuids.Items.AddRange(items);

                for (int i = 0; i < mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Count; i++)
                {
                    if (mainForm.listBox_WaypointsCreator_CreatureGuids.Items[i].ToString() == currentSelectedGuid)
                    {
                        guidFound = true;
                        mainForm.listBox_WaypointsCreator_CreatureGuids.SetSelected(i, true);
                        break;
                    }
                }

                if (!guidFound)
                {
                    for (int i = 0; i < listBoxOriginalItems.Count; i++)
                    {
                        if (listBoxOriginalItems[i].ToString() == currentSelectedGuid)
                        {
                            for (int j = i + 1; j < listBoxOriginalItems.Count; j++)
                            {
                                if (mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Cast<object>().FirstOrDefault(x => x.ToString() == listBoxOriginalItems[j].ToString()) != null)
                                {
                                    for (int l = 0; l < mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Count; l++)
                                    {
                                        if (mainForm.listBox_WaypointsCreator_CreatureGuids.Items[l].ToString() == listBoxOriginalItems[j].ToString())
                                        {
                                            guidFound = true;
                                            mainForm.listBox_WaypointsCreator_CreatureGuids.SetSelected(l, true);
                                            break;
                                        }
                                    }
                                }

                                if (guidFound)
                                    break;
                            }
                        }

                        if (guidFound)
                            break;
                    }
                }

                mainForm.listBox_WaypointsCreator_CreatureGuids.Refresh();
            }
        }

        public bool IsCreatureAlreadyHaveDataOnDb(string guid)
        {
            string linkedId = creaturesDict[guid].GetLinkedId();
            bool alreadyHaveWaypointsOrRelatedToFormation = false;

            string formationSqlQuery = "SELECT `leaderLinkedId`, `memberLinkedId` FROM `creature_formations` WHERE `leaderLinkedId` = '" + linkedId + "' OR " + "`memberLinkedId` = '" + linkedId + "';";
            var creatureFormationDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(formationSqlQuery) : null;

            if (creatureFormationDs != null && creatureFormationDs.Tables["table"].Rows.Count > 0)
            {
                foreach (DataRow row in creatureFormationDs.Tables["table"].Rows)
                {
                    if (Convert.ToString(row.ItemArray[0]) == linkedId ||
                        Convert.ToString(row.ItemArray[1]) == linkedId)
                    {
                        alreadyHaveWaypointsOrRelatedToFormation = true;
                        break;
                    }
                }
            }

            if (!alreadyHaveWaypointsOrRelatedToFormation)
            {
                string addonSqlQuery = "SELECT `path_id` FROM `creature_addon` WHERE `linked_id` = '" + linkedId + "';";
                var creatureAddonDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(addonSqlQuery) : null;

                if (creatureAddonDs != null && creatureAddonDs.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow row in creatureAddonDs.Tables["table"].Rows)
                    {
                        if (Convert.ToInt32(row.ItemArray[0]) > 0)
                        {
                            alreadyHaveWaypointsOrRelatedToFormation = true;
                            break;
                        }
                    }
                }
            }

            return alreadyHaveWaypointsOrRelatedToFormation;
        }

        private List<string> GetExistedLinkedIdsFromListBox()
        {
            List<string> foundLinkedIds = new List<string>();

            string linkedIds = GetLinkedIdsFromGuids();

            string formationSqlQuery = "SELECT `leaderLinkedId`, `memberLinkedId` FROM `creature_formations` WHERE `leaderLinkedId` IN (" + linkedIds + ") OR " + "`memberLinkedId` IN (" + linkedIds + ");";
            string addonSqlQuery = "SELECT `linked_id` FROM `creature_addon` WHERE `linked_id` IN (" + linkedIds + ") AND `path_id` != 0;";

            var creatureFormationsDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(formationSqlQuery) : null;
            var creatureAddonDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(addonSqlQuery) : null;

            if (creatureFormationsDs != null && creatureFormationsDs.Tables["table"].Rows.Count > 0)
            {
                Parallel.ForEach(creatureFormationsDs.Tables["table"].Rows.Cast<DataRow>().AsEnumerable(), row =>
                {
                    if (row.ItemArray[0].ToString() != "" && !foundLinkedIds.Contains(row.ItemArray[0].ToString()))
                    {
                        lock (foundLinkedIds)
                        {
                            foundLinkedIds.Add(row.ItemArray[0].ToString());
                        }
                    }

                    if (row.ItemArray[0].ToString() != "" && !foundLinkedIds.Contains(row.ItemArray[1].ToString()))
                    {
                        lock (foundLinkedIds)
                        {
                            foundLinkedIds.Add(row.ItemArray[1].ToString());
                        }
                    }
                });
            }

            if (creatureAddonDs != null && creatureAddonDs.Tables["table"].Rows.Count > 0)
            {
                Parallel.ForEach(creatureAddonDs.Tables["table"].Rows.Cast<DataRow>().AsEnumerable(), row =>
                {
                    if (row.ItemArray[0].ToString() != "" && !foundLinkedIds.Contains(row.ItemArray[0].ToString()))
                    {
                        lock (foundLinkedIds)
                        {
                            foundLinkedIds.Add(row.ItemArray[0].ToString());
                        }
                    }
                });
            }

            return foundLinkedIds;
        }

        private string GetLinkedIdsFromGuids()
        {
            string linkedIds = "";

            foreach (object item in mainForm.listBox_WaypointsCreator_CreatureGuids.Items)
            {
                linkedIds += "'" + creaturesDict[item.ToString()].GetLinkedId() + "', ";
            }

            return linkedIds.Remove(linkedIds.Length - 2);
        }

        public void RemoveGuidsBeforeSelectedOne()
        {
            if (mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedIndex == -1 || mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedIndex == 0)
                return;

            for (int i = mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedIndex - 1; i >= 0; i--)
            {
                mainForm.listBox_WaypointsCreator_CreatureGuids.Items.RemoveAt(i);
            }

            mainForm.listBox_WaypointsCreator_CreatureGuids.Refresh();
        }

        public void FillWaypointsGrid()
        {
            if (mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedItem == null)
                return;

            Creature creature = creaturesDict[mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedItem.ToString()];

            mainForm.grid_WaypointsCreator_Waypoints.Rows.Clear();

            uint index = 1;

            if (creature.waypoints.Count >= 1000)
                RemoveDuplicatePoints(creature.waypoints);

            foreach (Waypoint wp in creature.waypoints)
            {
                mainForm.grid_WaypointsCreator_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime.ToFormattedString(), wp.delay, wp.HasScripts(), wp.Clone());
                index++;
            }

            GraphPath();

            mainForm.grid_WaypointsCreator_Waypoints.Enabled = true;
        }

        public void GraphPath()
        {
            Creature creature = creaturesDict[mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedItem.ToString()];

            mainForm.chart_WaypointsCreator_Path.BackColor = Color.White;
            mainForm.chart_WaypointsCreator_Path.ChartAreas[0].BackColor = Color.White;
            mainForm.chart_WaypointsCreator_Path.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            mainForm.chart_WaypointsCreator_Path.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            mainForm.chart_WaypointsCreator_Path.ChartAreas[0].AxisY.IsReversed = true;
            mainForm.chart_WaypointsCreator_Path.Titles.Clear();
            mainForm.chart_WaypointsCreator_Path.Titles.Add(creature.name + " Entry: " + creature.entry);
            mainForm.chart_WaypointsCreator_Path.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            mainForm.chart_WaypointsCreator_Path.Titles[0].ForeColor = Color.Blue;
            mainForm.chart_WaypointsCreator_Path.Titles.Add("Linked Id: " + creature.GetLinkedId());
            mainForm.chart_WaypointsCreator_Path.Titles[1].Font = new Font("Arial", 16, FontStyle.Bold);
            mainForm.chart_WaypointsCreator_Path.Titles[1].ForeColor = Color.Blue;
            mainForm.chart_WaypointsCreator_Path.Series.Clear();
            mainForm.chart_WaypointsCreator_Path.Series.Add("Path");
            mainForm.chart_WaypointsCreator_Path.Series["Path"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            mainForm.chart_WaypointsCreator_Path.Series.Add("Line");
            mainForm.chart_WaypointsCreator_Path.Series["Line"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            for (var i = 0; i < mainForm.grid_WaypointsCreator_Waypoints.RowCount; i++)
            {
                double posX = Convert.ToDouble(mainForm.grid_WaypointsCreator_Waypoints[1, i].Value);
                double posY = Convert.ToDouble(mainForm.grid_WaypointsCreator_Waypoints[2, i].Value);

                mainForm.chart_WaypointsCreator_Path.Series["Path"].Points.AddXY(posX, posY);
                mainForm.chart_WaypointsCreator_Path.Series["Path"].Points[i].Color = Color.Blue;
                mainForm.chart_WaypointsCreator_Path.Series["Path"].Points[i].Label = Convert.ToString(i + 1);
                mainForm.chart_WaypointsCreator_Path.Series["Line"].Points.AddXY(posX, posY);
                mainForm.chart_WaypointsCreator_Path.Series["Line"].Points[i].Color = Color.Cyan;
            }
        }

        public void CutFromGrid()
        {
            foreach (DataGridViewRow row in mainForm.grid_WaypointsCreator_Waypoints.SelectedRows)
            {
                mainForm.grid_WaypointsCreator_Waypoints.Rows.Remove(row);
            }

            for (int i = 0; i < mainForm.grid_WaypointsCreator_Waypoints.Rows.Count; i++)
            {
                mainForm.grid_WaypointsCreator_Waypoints[0, i].Value = i + 1;
            }

            GraphPath();
        }

        public void CreateSQL()
        {
            Creature creature = creaturesDict[mainForm.listBox_WaypointsCreator_CreatureGuids.SelectedItem.ToString()];
            string sqlQuery = "SELECT * FROM `creature_addon` WHERE `linked_id` = '" + creature.GetLinkedId() + "';";
            string creatureAddon;
            bool addonFound = false;
            var creatureAddonDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(sqlQuery) : null;

            if (creatureAddonDs != null && creatureAddonDs.Tables["table"].Rows.Count > 0)
            {
                creatureAddon = "UPDATE `creature_addon` SET `path_id` = @PATH WHERE `linked_id` = '" + creature.GetLinkedId() + "';" + "\r\n";
                addonFound = true;
            }
            else
            {
                creatureAddon = "('" + creature.GetLinkedId() + "', @PATH, 0, 0, 1, 0, 0, 0, 0, '', -1); " + "\r\n";
            }

            List<Waypoint> waypoints = (from DataGridViewRow row in mainForm.grid_WaypointsCreator_Waypoints.Rows select (Waypoint) row.Cells[8].Value).ToList();

            if (Properties.Settings.Default.Scripts && waypoints.GetScriptsCount() != 0)
            {
                if (creature.waypoints.Count != waypoints.Count)
                {
                    waypoints.RecalculateIdsAndGuids(creature.entry);
                }
            }

            string SQLtext = "-- Pathing for " + creature.name + " Entry: " + creature.entry + "\r\n";
            SQLtext = SQLtext + "SET @GUID := (SELECT `guid` FROM `creature` WHERE `linked_id` = " + "'" + creature.GetLinkedId() + "'" + ");" + "\r\n";
            SQLtext = SQLtext + "SET @PATH := @GUID * 10;" + "\r\n";
            SQLtext = SQLtext + "UPDATE `creature` SET `spawndist` = 0, `MovementType` = 3 WHERE `linked_id` = '" + creature.GetLinkedId() + "'; " + "\r\n";

            if (addonFound)
            {
                SQLtext += creatureAddon;
            }
            else
            {
                SQLtext += "DELETE FROM `creature_addon` WHERE `linked_id` = '" + creature.GetLinkedId() + "';" + "\r\n";
                SQLtext += "INSERT INTO `creature_addon` (`linked_id`, `path_id`, `mount`, `bytes1`, `bytes2`, `emote`, `AiAnimKit`, `MovementAnimKit`, `MeleeAnimKit`, `auras`, `VerifiedBuild`) VALUES" + "\r\n";
                SQLtext += creatureAddon;
            }

            SQLtext = SQLtext + "DELETE FROM `waypoint_data` WHERE `id` = @PATH;" + "\r\n";
            SQLtext = SQLtext + "INSERT INTO `waypoint_data` (`id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`, `move_type`, `action`, `action_chance`, `speed`) VALUES" + "\r\n";

            for (int i = 0; i < waypoints.Count; i++)
            {
                Waypoint waypoint = waypoints[i];
                float orientation = waypoint.HasOrientation() ? waypoint.orientation : float.Parse(mainForm.grid_WaypointsCreator_Waypoints[4, i].Value.ToString());
                uint delay = waypoint.delay > 0 ? waypoint.delay : Convert.ToUInt32(mainForm.grid_WaypointsCreator_Waypoints[6, i].Value.ToString());

                if (i < (waypoints.Count - 1))
                {
                    SQLtext = SQLtext + "(@PATH, " + (i + 1) + ", " + waypoint.movePosition.x.GetValueWithoutComma() + ", " + waypoint.movePosition.y.GetValueWithoutComma() + ", " + waypoint.movePosition.z.GetValueWithoutComma() + ", " + orientation.GetValueWithoutComma() + ", " + delay + ", " + (uint)waypoint.moveType + ", " + waypoint.GetScriptId() + ", 100" + ", 0" + "),\r\n";
                }
                else
                {
                    SQLtext = SQLtext + "(@PATH, " + (i + 1) + ", " + waypoint.movePosition.x.GetValueWithoutComma() + ", " + waypoint.movePosition.y.GetValueWithoutComma() + ", " + waypoint.movePosition.z.GetValueWithoutComma() + ", " + orientation.GetValueWithoutComma() + ", " + delay + ", " + (uint)waypoint.moveType + ", " + waypoint.GetScriptId() + ", 100" + ", 0" + ");\r\n";
                }
            }

            SQLtext = SQLtext + "-- " + creature.guid + " .go " + creature.spawnPosition.x.GetValueWithoutComma() + " " + creature.spawnPosition.y.GetValueWithoutComma() + " " + creature.spawnPosition.z.GetValueWithoutComma() + "\r\n";

            if (Properties.Settings.Default.Scripts && creature.waypoints.GetScriptsCount() != 0)
            {
                if (creature.waypoints.Count != waypoints.Count)
                {
                    waypoints.RecalculateIdsAndGuids(creature.entry);
                }

                SQLtext += "\r\n";
                SQLtext += "-- Waypoint scripts for " + creature.name + " Entry: " + creature.entry + "\r\n";
                SQLtext = SQLtext + "DELETE FROM `waypoint_scripts` WHERE `id` IN (" + waypoints.GetScriptIds() + ");\r\n";
                SQLtext = SQLtext + "INSERT INTO `waypoint_scripts` (`id`, `delay`, `command`, `datalong`, `datalong2`, `dataint`, `x`, `y`, `z`, `o`, `guid`) VALUES" + "\r\n";

                uint scriptsCount = waypoints.GetScriptsCount() - 1;

                foreach (var script in waypoints.SelectMany(waypoint => waypoint.scripts))
                {
                    if (scriptsCount != 0)
                    {
                        SQLtext = SQLtext + "(" + script.id + ", " + script.delay + ", " + (uint)script.type + ", " + script.dataLong + ", " + script.dataLongSecond + ", " + script.dataInt + ", " + script.x.GetValueWithoutComma() + ", " + script.y.GetValueWithoutComma() + ", " + script.z.GetValueWithoutComma() + ", " + script.o.GetValueWithoutComma() + ", " + script.guid + ")," + " -- " + "Script Type: " + script.type + "\r\n";
                        scriptsCount--;
                    }
                    else
                    {
                        SQLtext = SQLtext + "(" + script.id + ", " + script.delay + ", " + (uint)script.type + ", " + script.dataLong + ", " + script.dataLongSecond + ", " + script.dataInt + ", " + script.x.GetValueWithoutComma() + ", " + script.y.GetValueWithoutComma() + ", " + script.z.GetValueWithoutComma() + ", " + script.o.GetValueWithoutComma() + ", " + script.guid + ");" + " -- " + "Script Type: " + script.type + "\r\n";
                    }
                }
            }

            if (Properties.Settings.Default.Vector)
            {
                SQLtext += "\r\n";
                SQLtext += "-- Vector3 for movement in core for " + creature.name + " Entry: " + creature.entry + "\r\n";
                SQLtext = SQLtext + "std::vector<G3D::Vector3> const g_Path" + creature.name + " =" + "\r\n";
                SQLtext = SQLtext + "{" + "\r\n";

                for (int i = 0; i < waypoints.Count; i++)
                {
                    Waypoint waypoint = waypoints[i];

                    if (i < (waypoints.Count - 1))
                    {
                        SQLtext = SQLtext + "{ " + waypoint.movePosition.x.GetValueWithoutComma() + "f, " + waypoint.movePosition.y.GetValueWithoutComma() + "f, " + waypoint.movePosition.z.GetValueWithoutComma() + "f },\r\n";
                    }
                    else
                    {
                        SQLtext = SQLtext + "{ " + waypoint.movePosition.x.GetValueWithoutComma() + "f, " + waypoint.movePosition.y.GetValueWithoutComma() + "f, " + waypoint.movePosition.z.GetValueWithoutComma() + "f }\r\n";
                    }
                }

                SQLtext = SQLtext + "};" + "\r\n";
            }

            mainForm.textBox_SqlOutput.Text = SQLtext;
        }

        public void RemoveNearestPoints()
        {
            bool canLoop = true;

            do
            {
                foreach (DataGridViewRow row in mainForm.grid_WaypointsCreator_Waypoints.Rows)
                {
                    Waypoint currentWaypoint = (Waypoint)row.Cells[8].Value;
                    Waypoint nextWaypoint;
                    try
                    {
                        nextWaypoint = (Waypoint)mainForm.grid_WaypointsCreator_Waypoints.Rows[row.Index + 1].Cells[8].Value;
                    }
                    catch
                    {
                        canLoop = false;
                        break;
                    }

                    if (currentWaypoint.movePosition.GetExactDist2d(nextWaypoint.movePosition) <= 5.0f &&
                        !nextWaypoint.HasOrientation() && !nextWaypoint.HasScripts())
                    {
                        mainForm.grid_WaypointsCreator_Waypoints.Rows.RemoveAt(row.Index + 1);
                        break;
                    }
                }
            }
            while (canLoop);

            for (int i = 0; i < mainForm.grid_WaypointsCreator_Waypoints.Rows.Count; i++)
            {
                mainForm.grid_WaypointsCreator_Waypoints[0, i].Value = i + 1;
            }

            GraphPath();
        }

        public void RemoveDuplicatePoints()
        {
            List<Waypoint> waypointsList = new List<Waypoint>();

            foreach (DataGridViewRow row in mainForm.grid_WaypointsCreator_Waypoints.Rows)
            {
                Waypoint waypoint = (Waypoint)row.Cells[8].Value;

                if (waypoint.HasOrientation() || waypoint.HasScripts())
                {
                    waypointsList.Add(waypoint);
                    continue;
                }

                bool waypointIsValid = waypointsList.All(compareWaypoint => !(waypoint.movePosition.GetExactDist2d(compareWaypoint.movePosition) <= 1.0f));

                if (waypointIsValid)
                {
                    waypointsList.Add(waypoint);
                }
            }

            mainForm.grid_WaypointsCreator_Waypoints.Rows.Clear();

            uint index = 1;

            foreach (Waypoint wp in waypointsList)
            {
                mainForm.grid_WaypointsCreator_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime.ToFormattedString(), wp.delay, wp.HasScripts(), wp);
                index++;
            }

            GraphPath();
        }

        public void RemoveDuplicatePoints(List<Waypoint> waypoints)
        {
            List<Waypoint> waypointsList = new List<Waypoint>();

            foreach (Waypoint waypoint in waypoints)
            {
                if (waypoint.HasOrientation() || waypoint.HasScripts())
                {
                    waypointsList.Add(waypoint);
                    continue;
                }

                bool waypointIsValid = waypointsList.All(compareWaypoint => !(waypoint.movePosition.GetExactDist2d(compareWaypoint.movePosition) <= 1.0f));

                if (waypointIsValid)
                {
                    waypointsList.Add(waypoint);
                }
            }

            waypoints.Clear();

            waypoints.AddRange(waypointsList);
        }

        public void CreateReturnPath()
        {
            List<Waypoint> waypoints = (from DataGridViewRow row in mainForm.grid_WaypointsCreator_Waypoints.Rows select (Waypoint) row.Cells[8].Value).ToList();

            waypoints.Reverse();

            waypoints.RemoveAt(0);
            waypoints.RemoveAt(waypoints.Count - 1);

            int index = mainForm.grid_WaypointsCreator_Waypoints.Rows.Count + 1;

            foreach (Waypoint wp in waypoints)
            {
                mainForm.grid_WaypointsCreator_Waypoints.Rows.Add(index, wp.movePosition.x, wp.movePosition.y, wp.movePosition.z, wp.orientation, wp.moveStartTime.ToFormattedString(), wp.delay, wp.HasScripts(), wp);
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
            mainForm.openFileDialog.Filter = "Parsed Sniff or Data File (*.txt;*.dat)|*.txt;*.dat";
            mainForm.openFileDialog.FilterIndex = 1;
            mainForm.openFileDialog.ShowReadOnly = false;
            mainForm.openFileDialog.Multiselect = false;
            mainForm.openFileDialog.CheckFileExists = true;
        }

        public void ImportStarted()
        {
            mainForm.Cursor = Cursors.WaitCursor;
            mainForm.toolStripButton_WaypointsCreator_LoadSniff.Enabled = false;
            mainForm.toolStripButton_WaypointsCreator_Search.Enabled = false;
            mainForm.toolStripTextBox_WaypointsCreator_Entry.Enabled = false;
            mainForm.listBox_WaypointsCreator_CreatureGuids.Enabled = false;
            mainForm.listBox_WaypointsCreator_CreatureGuids.Items.Clear();
            mainForm.listBox_WaypointsCreator_CreatureGuids.DataSource = null;
            mainForm.grid_WaypointsCreator_Waypoints.Enabled = false;
            mainForm.grid_WaypointsCreator_Waypoints.Rows.Clear();
            mainForm.toolStripStatusLabel_FileStatus.Text = "Loading File...";
        }

        public void ImportSuccessful()
        {
            mainForm.toolStripStatusLabel_CurrentAction.Text = "";
            mainForm.toolStripButton_WaypointsCreator_LoadSniff.Enabled = true;
            mainForm.toolStripButton_WaypointsCreator_Search.Enabled = true;
            mainForm.toolStripTextBox_WaypointsCreator_Entry.Enabled = true;
            mainForm.toolStripStatusLabel_FileStatus.Text = mainForm.openFileDialog.FileName + " is selected for input.";
            mainForm.Cursor = Cursors.Default;
        }
    }
}
