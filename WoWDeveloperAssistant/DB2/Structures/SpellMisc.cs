namespace DB2.Structures
{
    [Hotfix("SpellMisc")]
    public class SpellMisc
    {
        public int[] Attributes = new int[15];
        public byte DifficultyId;
        public ushort CastingTimeIndex;
        public ushort DurationIndex;
        public ushort PvPDurationIndex;
        public ushort RangeIndex;
        public byte SchoolMask;
        public float Speed;
        public float LaunchDelay;
        public float MinDuration;
        public int SpellIconFileDataId;
        public int ActiveIconFileDataId;
        public int ContentTuningId;
        public int ShowFutureSpellPlayerConditionId;
        public int SpellVisualScript;
        public int ActiveSpellVisualScript;
        public int SpellId;
    }
}
