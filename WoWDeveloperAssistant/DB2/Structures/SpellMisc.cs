namespace DB2.Structures
{
    [Hotfix("SpellMisc")]
    public class SpellMisc
    {
        public uint[] Attributes = new uint[15];
        public byte DifficultyId;
        public ushort CastingTimeIndex;
        public ushort DurationIndex;
        public ushort RangeIndex;
        public byte SchoolMask;
        public float Speed;
        public float LaunchDelay;
        public float MinDuration;
        public uint SpellIconFileDataId;
        public uint ActiveIconFileDataId;
        public uint ContentTuningId;
        public uint PlayerConditionId;
        public uint SpellVisualScript;
        public uint Field_9_0_1_35679_014;
        public uint SpellId;
    }
}
