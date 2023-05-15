using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Creature_Scripts_Creator;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Misc
{
    [Serializable]
    public class Creature
    {
        public string guid;
        public uint entry;
        public string name;
        public uint maxhealth;
        public TimeSpan combatStartTime;
        public TimeSpan deathTime;
        public Position spawnPosition;
        public uint? mapId;
        public List<Waypoint> waypoints;
        public List<Aura> auras;
        public Dictionary<uint, Spell> castedSpells;
        public TimeSpan lastUpdatePacketTime;
        public bool hasDisableGravity;
        public bool isCyclic;
        public string transportGuid;
        public Dictionary<uint, MonsterMovePacket.FilterKey> filterKeys;
        public List<uint> virtualItems;

        public Creature() { }

        public Creature(UpdateObjectPacket updatePacket)
        {
            guid = updatePacket.guid;
            entry = updatePacket.entry;
            maxhealth = updatePacket.maxHealth;
            deathTime = updatePacket.currentHealth == 0 ? updatePacket.packetSendTime : new TimeSpan();
            castedSpells = new Dictionary<uint, Spell>();
            combatStartTime = new TimeSpan();
            spawnPosition = updatePacket.spawnPosition;
            mapId = updatePacket.mapId;
            waypoints = updatePacket.waypoints;
            auras = new List<Aura>();
            lastUpdatePacketTime = updatePacket.packetSendTime;
            hasDisableGravity = updatePacket.hasDisableGravity;
            isCyclic = updatePacket.isCyclic;
            transportGuid = updatePacket.transportGuid;
            filterKeys = updatePacket.filterKeys;
            virtualItems = updatePacket.virtualItems;
        }

        public void UpdateCreature(UpdateObjectPacket updatePacket)
        {
            if (guid == "" && updatePacket.guid != "")
                guid = updatePacket.guid;

            if (entry == 0 && updatePacket.entry != 0)
                entry = updatePacket.entry;

            if (deathTime == TimeSpan.Zero && updatePacket.currentHealth == 0)
                deathTime = updatePacket.packetSendTime;

            if (maxhealth == 0 && updatePacket.maxHealth != 0)
                maxhealth = updatePacket.maxHealth;

            if (lastUpdatePacketTime > updatePacket.packetSendTime && updatePacket.spawnPosition.IsValid())
            {
                spawnPosition = updatePacket.spawnPosition;
                lastUpdatePacketTime = updatePacket.packetSendTime;
            }

            if (mapId == null && updatePacket.mapId != null)
                mapId = updatePacket.mapId;

            if (updatePacket.HasWaypoints())
            {
                foreach (Waypoint wp in updatePacket.waypoints)
                {
                    waypoints.Add(wp);
                }
            }

            if (!hasDisableGravity && updatePacket.hasDisableGravity)
                hasDisableGravity = updatePacket.hasDisableGravity;

            if (!isCyclic && updatePacket.isCyclic)
                isCyclic = updatePacket.isCyclic;

            if (transportGuid == "" && updatePacket.transportGuid != "")
                transportGuid = updatePacket.transportGuid;

            if (filterKeys.Count == 0)
            {
                filterKeys = updatePacket.filterKeys;
            }

            if (virtualItems.Count == 0)
            {
                virtualItems = updatePacket.virtualItems;
            }
        }

        public void UpdateSpells(SpellStartPacket spellPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.UpdateIfNeeded(spellPacket);
            });
        }

        public void UpdateSpellsByMovementPacket(MonsterMovePacket movePacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.SetConeDelayIfNeeded(movePacket);
            });
        }

        public void UpdateSpellsByAttackStopPacket(AttackStopPacket attackPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.SetConeDelayIfNeeded(attackPacket);
            });
        }

        public void UpdateCombatSpells(AIReactionPacket reactionPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.MarkSpellAsCombat(reactionPacket);
            });
        }

        public void RemoveNonCombatCastTimes()
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.RemoveNonCombatCastTimes(this);
            });
        }

        public void CreateCombatCastTimings()
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.CreateTimings(this);
            });
        }

        public void CreateDeathSpells()
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.MarkSpellAsDeath(this);
            });
        }

        public bool HasCombatSpells()
        {
            return castedSpells.Values.Any(spell => spell.isCombatSpell);
        }

        public string GetLinkedId()
        {
            var linkedId = Convert.ToString(Math.Round(spawnPosition.x / 0.25)) + " " + Convert.ToString(Math.Round(spawnPosition.y / 0.25)) + " " + Convert.ToString(Math.Round(spawnPosition.z / 0.25)) + " ";
            linkedId += Convert.ToString(entry) + " " + Convert.ToString(mapId) + " 0 1 " + GetSpawnDifficulties();
            return Utils.SHA1HashStringForUTF8String(linkedId).ToUpper();
        }

        public bool HasWaypoints()
        {
            return waypoints.Count >= 1;
        }

        public void AddScriptsForWaypoints(List<WaypointScript> scriptsList, MonsterMovePacket firstMovePacket, MonsterMovePacket lastMovePacket)
        {
            Waypoint waypoint = waypoints.GetLastWaypointWithTime(firstMovePacket.packetSendTime);

            uint id = (entry * 100) + waypoints.GetPointsWithScriptsCount();
            uint _guid = id + waypoints.GetScriptsCount() == id ? id + waypoints.GetScriptsCount() : id + waypoints.GetScriptsCount() - 1;

            foreach (WaypointScript script in scriptsList)
            {
                int tempDelay = (int)Math.Floor((script.scriptTime.TotalMilliseconds - (firstMovePacket.packetSendTime.TotalMilliseconds + firstMovePacket.moveTime)) / 1000);

                if (script.type == WaypointScript.ScriptType.SetOrientation && tempDelay <= 1)
                {
                    waypoint.SetOrientation(script.o);
                    continue;
                }

                script.SetId(id);
                script.SetDelay(tempDelay < 0 ? 0 : (uint)tempDelay);
                script.SetGuid(_guid);
                _guid++;

                waypoint.scripts.Add(script);
            }

            waypoint.delay = (uint)(lastMovePacket.packetSendTime.TotalMilliseconds - scriptsList.First().scriptTime.TotalMilliseconds);
        }

        public uint GetSpellIdForAuraSlot(AuraUpdatePacket auraPacket)
        {
            uint spellId = 0;
            int auraIndex = 0;

            auras = new List<Aura>(from aura in auras orderby aura.time select aura);

            for (int i = 0; i < auras.Count; i++)
            {
                if (auras[i].HasAura == false && auras[i].time == auraPacket.packetSendTime && auras[i].slot == auraPacket.slot)
                {
                    auraIndex = i;
                    break;
                }
            }

            if (auraIndex != 0)
            {
                for (int i = auraIndex; i >= 0; i--)
                {
                    if (auras[i].HasAura && auras[i].slot == auraPacket.slot)
                        spellId = auras[i].spellId;
                }
            }

            return spellId;
        }

        public void AddWaypointsFromMovementPacket(MonsterMovePacket movePacket)
        {
            foreach (Waypoint wp in movePacket.waypoints)
            {
                waypoints.Add(wp);
            }

            SortWaypoints();
        }

        public void SortWaypoints()
        {
            if (waypoints.Where(x => x.packetNumber == -1).Count() != 0)
            {
                waypoints = new List<Waypoint>(from waypoint in waypoints orderby waypoint.idFromParse orderby waypoint.moveStartTime select waypoint);
            }
            else
            {
                waypoints = new List<Waypoint>(from waypoint in waypoints orderby waypoint.idFromParse orderby waypoint.packetNumber select waypoint);
            }
        }

        public string GetSpawnDifficulties()
        {
            if (MapIsAzeriteExpeditions((uint)mapId))
                return "38 39 40 45";

            if (DB2.Db2.MapDifficultyStore.ContainsKey((int)mapId))
                return DB2.Db2.MapDifficultyStore[(int)mapId];

            if (MapIsContinent((uint)mapId))
                return "0";

            return "1 2";
        }

        public bool MapIsAzeriteExpeditions(uint mapId)
        {
            switch (mapId)
            {
                case 2124: // Crestfall 8.2
                case 1907: // Snowblossom Village 8.1
                case 1814: // Havenswood 8.1
                case 1879: // Jorundall 8.1
                case 1897: // The Molten Cay
                case 1813: // Un'gol Ruins
                case 1892: // The Rotting Mire
                case 1883: // The Whispering Reef
                case 1882: // Verdant Wilds
                case 1893: // The Dread Chain
                case 1898: // Skittering Hollow
                    return true;
                default:
                    return false;
            }
        }

        private static bool MapIsContinent(uint mapId)
        {
            switch (mapId)
            {
                case 0: // Eastern Kingdoms
                case 1: // Kalimdor
                case 13: // Art Team Map
                case 35: // <unused>StormwindPrison
                case 37: // Azshara Crater
                case 369: // Deeprun Tram
                case 449: // Alliance PVP Barracks
                case 450: // Horde PVP Barracks
                case 451: // Development Land
                case 530: // Outland
                case 571: // Northrend
                case 606: // QA and DVD
                case 609: // Ebon Hold
                case 638: // Gilneas
                case 646: // Deepholm
                case 648: // LostIsles
                case 654: // Gilneas2
                case 655: // GilneasPhase1
                case 656: // GilneasPhase2
                case 659: // Lost Isles Volcano Eruption
                case 660: // Deephome Ceiling
                case 661: // Lost Isles Town in a Box
                case 718: // Trasnport: The Mighty Wind (Icecrown Citadel Raid)
                case 719: // Mount Hyjal Phase 1
                case 723: // Stormwind
                case 730: // Maelstrom Zone
                case 731: // Stonetalon Bomb
                case 732: // Tol Barad
                case 736: // Twilight Highlands Dragonmaw Phase
                case 738: // Ship to Vashj'ir (Orgrimmar -> Vashj'ir)
                case 739: // Vashj'ir Sub - Horde
                case 740: // Vashj'ir Sub - Alliance
                case 742: // Vashj'ir Sub - Horde - Circling Abyssal Maw
                case 743: // Vashj'ir Sub - Alliance circling Abyssal Maw
                case 746: // Uldum Phase Oasis
                case 751: // Redridge - Orc Bomb
                case 752: // Redridge - Bridge Phase One
                case 753: // Redridge - Bridge Phase Two
                case 759: // Uldum Phased Entrance
                case 760: // Twilight Highlands Phased Entrance
                case 762: // Twilight Highlands Zeppelin 1
                case 763: // Twilight Highlands Zeppelin 2
                case 764: // Uldum - Phase Wrecked Camp
                case 765: // Krazzworks Attack Zeppelin
                case 860: // The Wandering Isle
                case 861: // Molten Front
                case 870: // Pandaria
                case 971: // Jade Forest Alliance Hub Phase
                case 972: // Jade Forest Battlefield Phase
                case 974: // Darkmoon Faire
                case 975: // Turtle Ship Phase 01
                case 976: // Turtle Ship Phase 02
                case 1019: // Ruins of Theramore
                case 1043: // Brawl'gar Arena
                case 1060: // Level Design Land - Dev Only
                case 1061: // Horde Beach Daily Area
                case 1062: // Alliance Beach Daily Area
                case 1064: // Mogu Island Daily Area
                case 1066: // Stormwind Gunship Pandaria Start Area
                case 1074: // Orgrimmar Gunship Pandaria Start
                case 1075: // Theramore's Fall Phase
                case 1076: // Jade Forest Horde Starting Area
                case 1101: // Defense Of The Ale House BG
                case 1107: // Dreadscar Rift
                case 1116: // Draenor
                case 1120: // Thunder King Horde Hub
                case 1121: // Thunder Island Alliance Hub
                case 1128: // Mogu Island Events - Horde Base
                case 1129: // Mogu Island Events - Alliance Base
                case 1166: // Small Battleground A
                case 1169: // Small Battleground B
                case 1170: // Small Battleground C
                case 1179: // Warcraft Heroes
                case 1190: // Blasted Lands Phase
                case 1220: // Broken Isles
                case 1264: // Propland - Dev Only
                case 1265: // Tanaan Jungle Intro
                case 1270: // Development Land 3
                case 1307: // Tanaan Jungle Intro - Forge Phase
                case 1310: // Expansion 5 QA Model Map
                case 1462: // Illidans Rock
                case 1463: // Helhiem Exterior Area
                case 1464: // Tanaan Jungle
                case 1465: // Tanaan Jungle - No Hubs Phase
                case 1468: // Warden Prison DH Quests
                case 1469: // The Heart of Azeroth
                case 1479: // Skyhold
                case 1481: // Mardum
                case 1502: // Dalaran Underbelly
                case 1509: // Bloodtotem Cavern - Fel Phase
                case 1510: // Bloodtotem Cavern - Tauren Phase
                case 1512: // Netherlight Temple
                case 1513: // Hall of the Guardian
                case 1514: // The Wandering Isle
                case 1515: // Huln's War
                case 1519: // The Fel Hammer
                case 1540: // Emerald Dreamway
                case 1547: // Artifact - Combat - Acquisition Ship
                case 1602: // Artifact - The Vortex Pinnacle - Shaman Order Hall
                case 1604: // Artifact - Order Campaign - Portal World Niskara
                case 1605: // Firelands_Artifact
                case 1608: // Hyjal War of the Ancients Quest
                case 1618: // Death Knight Campaign - Scarlet Monastery
                case 1622: // Pandemonium
                case 1647: // Vol'jin's Funeral Pyre
                case 1670: // Broken Shore (Delete)
                case 1711: // Alliance Submarine (7.1.5 Boat Holiday)
                case 1918: // Kalimdor Darkshore Phase
                case 1817: // Silithus: The Wound
                case 1643: // Kultiras
                case 1642: // Zandalar
                case 1718: // Nazjatar
                case 2241: // New Uldum
                case 2222: // Shadowlands
                    return true;
                default:
                    return false;
            }
        }

        public bool IsCritter()
        {
            if (!Properties.Settings.Default.UsingDB)
                return false;

            var creatureTemplateWdbDs = SQLModule.WorldSelectQuery("SELECT `Type` FROM `creature_template_wdb` WHERE `entry` = " + entry + ";");

            if (creatureTemplateWdbDs != null && creatureTemplateWdbDs.Tables["table"].Rows.Count > 0)
            {
                return creatureTemplateWdbDs.Tables["table"].Rows[0].ItemArray[0].ToString() == "8";
            }

            return false;
        }
    }
}
