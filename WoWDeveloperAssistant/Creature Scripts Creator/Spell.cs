using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Utils;

namespace WoWDeveloperAssistant.Creature_Scripts_Creator
{
    [ProtoContract]
    public class CombatCastTimings
    {
        [ProtoMember(1)]
        public TimeSpan minCastTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(2)]
        public TimeSpan maxCastTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(3)]
        public TimeSpan minRepeatTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(4)]
        public TimeSpan maxRepeatTime
        {
            get; set;
        } = new TimeSpan();

        public CombatCastTimings() { }

        public CombatCastTimings(TimeSpan minCast, TimeSpan maxCast, TimeSpan minRepeat, TimeSpan maxRepeat)
        {
            minCastTime = minCast;
            maxCastTime = maxCast;
            minRepeatTime = minRepeat;
            maxRepeatTime = maxRepeat;
        }
    }

    [ProtoContract]
    public class Spell
    {
        [ProtoMember(1)]
        public uint spellId
        {
            get; set;
        } = 0;

        [ProtoMember(2)]
        public TimeSpan spellCastTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(3)]
        public bool hasConeType
        {
            get; set;
        } = false;

        [ProtoMember(4)]
        public bool needConeDelay
        {
            get; set;
        } = false;

        [ProtoMember(5)]
        public List<TimeSpan> spellStartCastTimes
        {
            get; set;
        } = new List<TimeSpan>();

        [ProtoMember(6)]
        public uint castTimes
        {
            get; set;
        } = 0;

        [ProtoMember(7)]
        public bool isCombatSpell
        {
            get; set;
        } = false;

        [ProtoMember(8)]
        public string name
        {
            get; set;
        } = string.Empty;

        [ProtoMember(9)]
        public CombatCastTimings combatCastTimings
        {
            get; set;
        } = new CombatCastTimings();

        [ProtoMember(10)]
        public bool isDeathSpell
        {
            get; set;
        } = false;

        public Spell() { }

        public Spell(SpellPacket spellPacket)
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

        public void SetConeDelayIfNeeded(MonsterMovePacket movePacket)
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

        public void SetConeDelayIfNeeded(AttackStopPacket attackPacket)
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

                if (DB2.Db2.SpellEffectStore.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DB2.Db2.SpellEffectStore[spellEffectTuple];

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

        public void UpdateIfNeeded(SpellPacket spellPacket)
        {
            if (spellId == spellPacket.spellId)
            {
                castTimes++;
                spellStartCastTimes.Add(spellPacket.spellCastStartTime);
            }
        }

        public void MarkSpellAsCombat(Creature creature)
        {
            Parallel.ForEach(spellStartCastTimes, time =>
            {
                if (creature.combatTimings.IsCombatTimer(time, true))
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
                if (!creature.combatTimings.IsCombatTimer(time, true))
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

        public void CreateCombatTimings(Creature currentCreature)
        {
            if (!isCombatSpell)
                return;

            List<TimeSpan> castStartTimesList = new List<TimeSpan>();
            List<TimeSpan> castRepeatTimesList = new List<TimeSpan>();

            Parallel.ForEach(CreatureScriptsCreator.creaturesDict.Values.Where(x => x.entry == currentCreature.entry), creature =>
            {
                if (creature.castedSpells.TryGetValue(spellId, out Spell spellData) && spellData.isCombatSpell)
                {
                    var minCastTime = spellData.spellStartCastTimes.Min();
                    var combatTiming = creature.combatTimings?.FirstOrDefault(t => minCastTime >= t.CombatStartTime && minCastTime <= t.CombatStopTime);
                    castStartTimesList.Add(minCastTime - combatTiming.CombatStartTime);
                }
            });

            if (castStartTimesList.Count != 0)
            {
                combatCastTimings.minCastTime = castStartTimesList.Min();
                combatCastTimings.maxCastTime = GetAverageTimeSpanFromList(castStartTimesList) >= combatCastTimings.minCastTime ? GetAverageTimeSpanFromList(castStartTimesList) : combatCastTimings.minCastTime;
            }

            if (spellStartCastTimes.Count == 1)
                return;

            spellStartCastTimes = spellStartCastTimes.OrderBy(x => x.TotalMilliseconds).ToList();

            for (int i = 0; i < spellStartCastTimes.Count; i++)
            {
                if (i + 1 < spellStartCastTimes.Count)
                {
                    castRepeatTimesList.Add(spellStartCastTimes[i + 1] - spellStartCastTimes[i]);
                }
            }

            if (castRepeatTimesList.Count != 0)
            {
                combatCastTimings.minRepeatTime = castRepeatTimesList.Min();
                combatCastTimings.maxRepeatTime = GetAverageTimeSpanFromList(castRepeatTimesList) >= combatCastTimings.minRepeatTime ? GetAverageTimeSpanFromList(castRepeatTimesList) : combatCastTimings.minRepeatTime;
            }
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

                if (DB2.Db2.SpellEffectStore.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DB2.Db2.SpellEffectStore[spellEffectTuple];

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

                if (DB2.Db2.SpellEffectStore.ContainsKey(spellEffectTuple))
                {
                    var spellEffect = DB2.Db2.SpellEffectStore[spellEffectTuple];

                    if (spellEffect.ImplicitTarget[0] == 123 || spellEffect.ImplicitTarget[1] == 123)
                        return true;
                }
            }

            return false;
        }
    }
}
