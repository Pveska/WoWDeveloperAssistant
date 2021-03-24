using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Creature_Scripts_Creator
{
    [Serializable]
    public class Spell
    {
        public uint spellId;
        public TimeSpan spellCastTime;
        public bool hasConeType;
        public bool needConeDelay;
        public List<TimeSpan> spellStartCastTimes;
        public uint castTimes;
        public bool isCombatSpell;
        public string name;
        public CombatCastTimings combatCastTimings;
        public bool isDeathSpell;

        [Serializable]
        public struct CombatCastTimings
        {
            public TimeSpan minCastTime;
            public TimeSpan maxCastTime;
            public TimeSpan minRepeatTime;
            public TimeSpan maxRepeatTime;

            public CombatCastTimings(TimeSpan minCast, TimeSpan maxCast, TimeSpan minRepeat, TimeSpan maxRepeat)
            { minCastTime = minCast; maxCastTime = maxCast; minRepeatTime = minRepeat; maxRepeatTime = maxRepeat; }
        }

        public Spell(Packets.SpellStartPacket spellPacket)
        {
            spellId = spellPacket.spellId;
            spellCastTime = spellPacket.spellCastTime;
            hasConeType = SetConeTypeIfPossible();
            needConeDelay = false;
            spellStartCastTimes = new List<TimeSpan> { spellPacket.spellCastStartTime };
            castTimes = 1;
            isCombatSpell = false;
            isDeathSpell = false;
            name = GetSpellName(spellPacket.spellId);
        }

        public void SetConeDelayIfNeeded(Packets.MonsterMovePacket movePacket)
        {
            if (!hasConeType)
                return;

            if (spellCastTime == TimeSpan.Zero)
                return;

            if (movePacket.creatureOrientation == 0.0f)
                return;

            Parallel.ForEach(spellStartCastTimes, time =>
            {
                if (time == movePacket.packetSendTime)
                {
                    needConeDelay = true;
                    return;
                }
            });
        }

        public void SetConeDelayIfNeeded(Packets.AttackStopPacket attackPacket)
        {
            if (!hasConeType)
                return;

            if (spellCastTime == TimeSpan.Zero)
                return;

            if (attackPacket.nowDead)
                return;

            Parallel.ForEach(spellStartCastTimes, time =>
            {
                if (time == attackPacket.packetSendTime)
                {
                    needConeDelay = true;
                    return;
                }
            });
        }

        private bool SetConeTypeIfPossible()
        {
            for (uint i = 0; i < 32; i++)
            {
                var spellEffectTuple = Tuple.Create(spellId, i);

                if (DBC.DBC.SpellEffectStores.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DBC.DBC.SpellEffectStores[spellEffectTuple];

                    if ((spellEffect.ImplicitTarget[0] == 129 || spellEffect.ImplicitTarget[1] == 129) ||
                        (spellEffect.ImplicitTarget[0] == 130 || spellEffect.ImplicitTarget[1] == 130) ||
                        (spellEffect.ImplicitTarget[0] == 54 || spellEffect.ImplicitTarget[1] == 54) ||
                        (spellEffect.ImplicitTarget[0] == 110 || spellEffect.ImplicitTarget[1] == 110))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetSpellName(uint spellId)
        {
            if (DBC.DBC.SpellName.ContainsKey((int)spellId))
                return DBC.DBC.SpellName[(int)spellId].Name;

            return "Unknown";
        }

        public void UpdateIfNeeded(Packets.SpellStartPacket spellPacket)
        {
            if (spellId == spellPacket.spellId)
            {
                castTimes++;

                spellStartCastTimes.Add(spellPacket.spellCastStartTime);
            }
        }

        public void MarkSpellAsCombat(Packets.AIReactionPacket reactionPacket)
        {
            Parallel.ForEach(spellStartCastTimes, time =>
            {
                if (time >= reactionPacket.packetSendTime)
                {
                    isCombatSpell = true;
                    return;
                }
            });
        }

        public void RemoveNonCombatCastTimes(Creature creature)
        {
            Parallel.ForEach(spellStartCastTimes.ToList(), time =>
            {
                if (time < creature.combatStartTime && time != creature.combatStartTime)
                {
                    lock (spellStartCastTimes)
                    {
                        spellStartCastTimes.Remove(time);

                        if (spellStartCastTimes.Count == 0)
                            isCombatSpell = false;
                    }
                }
            });
        }

        public void CreateTimings(Creature currentCreature)
        {
            if (!isCombatSpell)
                return;

            CombatCastTimings castTimings = new CombatCastTimings(new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan());

            List<TimeSpan> maxCastTimesList = new List<TimeSpan>();
            List<TimeSpan> maxRepeatCastTimesList = new List<TimeSpan>();

            spellStartCastTimes = new List<TimeSpan>(from time in spellStartCastTimes orderby time.TotalSeconds ascending select time);

            Parallel.ForEach(CreatureScriptsCreator.creaturesDict, creature =>
            {
                if (creature.Value.entry == currentCreature.entry)
                {
                    if (creature.Value.castedSpells.ContainsKey(spellId))
                    {
                        if (creature.Value.castedSpells[spellId].isCombatSpell)
                        {
                            lock (maxCastTimesList)
                            {
                                maxCastTimesList.Add(Utils.GetMinTimeSpanFromList(creature.Value.castedSpells[spellId].spellStartCastTimes) - creature.Value.combatStartTime);
                            }
                        }
                    }
                }
            });

            castTimings.minCastTime = maxCastTimesList.Min();
            castTimings.maxCastTime = Utils.GetAverageTimeSpanFromList(maxCastTimesList) >= castTimings.minCastTime ? Utils.GetAverageTimeSpanFromList(maxCastTimesList) : castTimings.minCastTime;
            maxCastTimesList.Clear();

            if (spellStartCastTimes.Count == 1)
            {
                combatCastTimings = castTimings;
                return;
            }

            castTimings.minRepeatTime = spellStartCastTimes[1] - spellStartCastTimes[0];

            for(int i = 0; i < spellStartCastTimes.Count; i++)
            {
                if (i + 1 < spellStartCastTimes.Count)
                {
                    maxRepeatCastTimesList.Add(spellStartCastTimes[i + 1] - spellStartCastTimes[i]);
                }
            }

            castTimings.maxRepeatTime = Utils.GetAverageTimeSpanFromList(maxRepeatCastTimesList) >= castTimings.minRepeatTime ? Utils.GetAverageTimeSpanFromList(maxRepeatCastTimesList) : castTimings.minRepeatTime;

            combatCastTimings = castTimings;
        }

        public void MarkSpellAsDeath(Creature currentCreature)
        {
            Parallel.ForEach(spellStartCastTimes, time =>
            {
                if (currentCreature.deathTime != TimeSpan.Zero && time >= currentCreature.deathTime)
                {
                    isDeathSpell = true;
                    return;
                }
            });
        }

        public uint GetTargetType()
        {
            for (uint i = 0; i < 32; i++)
            {
                var spellEffectTuple = Tuple.Create(spellId, i);

                if (DBC.DBC.SpellEffectStores.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DBC.DBC.SpellEffectStores[spellEffectTuple];

                    if (IsSelfTargetType((uint)spellEffect.ImplicitTarget[0]) || IsSelfTargetType((uint)spellEffect.ImplicitTarget[1]))
                        return 1;
                    if (IsNonSelfTargetType((uint)spellEffect.ImplicitTarget[0]) || IsNonSelfTargetType((uint)spellEffect.ImplicitTarget[1]))
                        return 2;
                }
            }

            return 99;
        }

        private static bool IsSelfTargetType(uint targetType)
        {
            List<uint> selfTargetTypesList = new List<uint>()
            {
                1, 2, 7, 8, 15, 16, 18, 22, 24, 38,
                41, 42, 43, 44, 46, 47, 48, 49, 50,
                54, 64, 65, 66, 67, 68, 69, 70, 71,
                72, 73, 74, 75, 78, 79, 80, 81, 82,
                83, 84, 85, 86, 104
            };

            return selfTargetTypesList.Contains(targetType);
        }

        private static bool IsNonSelfTargetType(uint targetType)
        {
            List<uint> nonSelfTargetTypesList = new List<uint>()
            {
                6, 25, 53, 63, 87
            };

            return nonSelfTargetTypesList.Contains(targetType);
        }

        public bool ShouldBeCastedBeforeDeath()
        {
            for (uint i = 0; i < 32; i++)
            {
                var spellEffectTuple = Tuple.Create(spellId, i);

                if (DBC.DBC.SpellEffectStores.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DBC.DBC.SpellEffectStores[spellEffectTuple];

                    if (spellEffect.ImplicitTarget[0] == 123 || spellEffect.ImplicitTarget[1] == 123)
                        return true;
                }
            }

            return false;
        }
    }
}
