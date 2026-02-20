using System;

namespace DB2.Structures
{
    [Hotfix("SpellEffect")]
    public class SpellEffect
    {
        public short EffectAura;
        public short DifficultyId;
        public int EffectIndex;
        public int Effect;
        public float EffectAmplitude;
        public int EffectAttributes;
        public int EffectAuraPeriod;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public int EffectChainTargets;
        public int EffectItemType;
        public int EffectMechanic;
        public float EffectPointsPerResource;
        public float EffectPosFacing;
        public float EffectRealPointsPerLevel;
        public int EffectTriggerSpell;
        public float BonusCoefficientFromAP;
        public float PvpMultiplier;
        public float Coefficient;
        public float Variance;
        public float ResourceCoefficient;
        public float GroupSizeBasePointsCoefficient;
        public float EffectBasePoints;
        public int ScalingClass;
        public int Unk1200;
        public int[] EffectMiscValue = new int[2];
        public uint[] EffectRadiusIndex = new uint[2];
        public uint[] EffectSpellClassMask = new uint[4];
        public short[] ImplicitTarget = new short[2];
        public int SpellId;
    }
}
