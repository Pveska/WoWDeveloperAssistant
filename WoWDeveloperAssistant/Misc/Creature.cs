﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Waypoints_Creator;
using static WoWDeveloperAssistant.Packets;

namespace WoWDeveloperAssistant
{
    public class Creature
    {
        public string guid;
        public uint entry;
        public string name;
        public uint maxhealth;
        public TimeSpan combatStartTime;
        public TimeSpan deathTime;
        public Position spawnPosition;
        public uint mapId;
        public List<Waypoint> waypoints;
        public List<Aura> auras;
        public Dictionary<uint, Spell> castedSpells;

        public Creature(UpdateObjectPacket updatePacket)
        {
            guid = updatePacket.creatureGuid;
            entry = updatePacket.creatureEntry;
            name = updatePacket.creatureName;
            maxhealth = updatePacket.creatureMaxHealth;
            deathTime = updatePacket.creatureCurrentHealth == 0 ? updatePacket.packetSendTime : new TimeSpan();
            castedSpells = new Dictionary<uint, Spell>();
            combatStartTime = new TimeSpan();
            spawnPosition = updatePacket.spawnPosition;
            mapId = updatePacket.mapId;
            waypoints = updatePacket.waypoints;
            auras = new List<Aura>();
        }

        public void UpdateCreature(UpdateObjectPacket updatePacket)
        {
            if (guid == "" && updatePacket.creatureGuid != "")
                guid = updatePacket.creatureGuid;

            if (entry == 0 && updatePacket.creatureEntry != 0)
                entry = updatePacket.creatureEntry;

            if (name == "Unknown" && updatePacket.creatureName != "Unknown")
                name = updatePacket.creatureName;

            if (deathTime == TimeSpan.Zero && updatePacket.creatureCurrentHealth == 0)
                deathTime = updatePacket.packetSendTime;

            if (maxhealth == 0 && updatePacket.creatureMaxHealth != 0)
                maxhealth = updatePacket.creatureMaxHealth;

            if (spawnPosition != updatePacket.spawnPosition)
                spawnPosition = updatePacket.spawnPosition;

            if (mapId == 0 && updatePacket.mapId != 0)
                mapId = updatePacket.mapId;

            if (updatePacket.HasWaypoints())
            {
                foreach (Waypoint wp in updatePacket.waypoints)
                {
                    waypoints.Add(wp);
                }
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

        public bool HasCombatSpells()
        {
            foreach (Spell spell in castedSpells.Values)
            {
                if (spell.isCombatSpell)
                    return true;
            }

            return false;
        }

        public string GetLinkedId()
        {
            string linkedId = "";
            linkedId = Convert.ToString(Math.Round(spawnPosition.x / 0.25), CultureInfo.InvariantCulture) + " " + Convert.ToString(Math.Round(spawnPosition.y / 0.25), CultureInfo.InvariantCulture) + " " + Convert.ToString(Math.Round(spawnPosition.z / 0.25), CultureInfo.InvariantCulture) + " ";
            linkedId += Convert.ToString(entry) + " " + Convert.ToString(mapId) + " 0 1 0";
            linkedId = Utils.SHA1HashStringForUTF8String(linkedId).ToUpper();
            return linkedId;
        }

        public bool HasWaypoints()
        {
            return waypoints.Count > 1;
        }

        public void AddScriptsForWaypoints(List<WaypointScript> scriptsList, MonsterMovePacket firstMovePacket, MonsterMovePacket lastMovePacket)
        {
            Waypoint waypoint = waypoints.GetLastWaypointWithTime(firstMovePacket.packetSendTime);

            uint id = (entry * 100) + waypoints.GetPointsWithScriptsCount();
            uint guid = id + waypoints.GetScriptsCount() == id ? id + waypoints.GetScriptsCount() : id + waypoints.GetScriptsCount() - 1;

            foreach (WaypointScript script in scriptsList)
            {
                if (script.type == WaypointScript.ScriptType.SetOrientation && waypoint.HasOrientation())
                {
                    waypoint.orientation = 0.0f;
                }

                script.id = id;
                int tempDelay = (int)Math.Floor((script.scriptTime.TotalMilliseconds - (firstMovePacket.packetSendTime.TotalMilliseconds + firstMovePacket.moveTime)) / 1000);
                script.delay = tempDelay < 0 ? 0 : (uint)tempDelay;
                script.guid = guid;
                guid++;

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
                    if (auras[i].HasAura == true && auras[i].slot == auraPacket.slot)
                        spellId = auras[i].spellId;
                }
            }

            return spellId;
        }
    }
}