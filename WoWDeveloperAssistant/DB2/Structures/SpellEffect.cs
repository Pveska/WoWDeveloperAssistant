namespace DB2.Structures
{
    [Hotfix("SpellEffect")]
    public class SpellEffect
    {
        public ushort EffectAura;
        public uint DifficultyId;
        public uint EffectIndex;
        public uint Effect;
        public float EffectAmplitude;
        public uint EffectAttributes;
        public uint EffectAuraPeriod;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public uint EffectChainTargets;
        public uint EffectItemType;
        public uint EffectMechanic;
        public float EffectPointsPerResource;
        public float EffectPosFacing;
        public float EffectRealPointsPerLevel;
        public uint EffectTriggerSpell;
        public float BonusCoefficientFromAP;
        public float PvpMultiplier;
        public float Coefficient;
        public float Variance;
        public float ResourceCoefficient;
        public float GroupSizeBasePointsCoefficient;
        public float EffectBasePoints;
        public int ScalingClass;
        public int[] EffectMiscValue = new int[2];
        public uint[] EffectRadiusIndex = new uint[2];
        public uint[] EffectSpellClassMask = new uint[4];
        public uint[] ImplicitTarget = new uint[2];
        public int SpellId;
    }
}
