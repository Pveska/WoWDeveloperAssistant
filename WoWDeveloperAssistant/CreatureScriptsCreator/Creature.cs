using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Dictionary<uint, Spell> castedSpells;
        public List<CreatureText> saidTexts;

        public Creature(Packets.UpdateObjectPacket updatePacket)
        {
            guid = updatePacket.creatureGuid;
            entry = updatePacket.creatureEntry;
            name = updatePacket.creatureName;
            maxhealth = updatePacket.creatureMaxHealth;
            deathTime = updatePacket.creatureCurrentHealth == 0 ? updatePacket.packetSendTime : new TimeSpan();
            castedSpells = new Dictionary<uint, Spell>();
            combatStartTime = new TimeSpan();
            saidTexts = new List<CreatureText>();
        }

        public void UpdateCreature(Packets.UpdateObjectPacket updatePacket)
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
        }

        public void UpdateSpells(Packets.SpellStartPacket spellPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.UpdateIfNeeded(spellPacket);
            });
        }

        public void UpdateSpellsByMovementPacket(Packets.MonsterMovePacket movePacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.SetConeDelayIfNeeded(movePacket);
            });
        }

        public void UpdateSpellsByAttackStopPacket(Packets.AttackStopPacket attackPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.SetConeDelayIfNeeded(attackPacket);
            });
        }

        public void UpdateCombatSpells(Packets.AIReactionPacket reactionPacket)
        {
            Parallel.ForEach(castedSpells, spell =>
            {
                spell.Value.MarkSpellAsCombat(reactionPacket);
            });
        }

        public void UpdateTexts()
        {
            Parallel.ForEach(saidTexts, text =>
            {
                if (text.sayTime == deathTime)
                {
                    text.isDeadText = true;
                }
            });
        }

        public void UpdateTexts(Packets.AIReactionPacket reactionPacket)
        {
            Parallel.ForEach(saidTexts, text =>
            {
                if (text.sayTime == reactionPacket.packetSendTime)
                {
                    text.isAggroText = true;
                }
                else if (text.sayTime == deathTime)
                {
                    text.isDeadText = true;
                }
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

        public CreatureText GetAggroText()
        {
            foreach (CreatureText text in saidTexts)
            {
                if (text.isAggroText)
                    return text;
            }

            return null;
        }

        public CreatureText GetDeathText()
        {
            foreach (CreatureText text in saidTexts)
            {
                if (text.isDeadText)
                    return text;
            }

            return null;
        }

        public bool HasAnyText()
        {
            return saidTexts.Count != 0;
        }
    }
}
