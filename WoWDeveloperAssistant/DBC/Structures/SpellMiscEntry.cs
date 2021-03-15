namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("SpellMisc")]

    public sealed class SpellMiscEntry
    {
        public uint ID;
        public int[] Attributes = new int[15];
        public byte DifficultyID;
        public ushort CastingTimeIndex;
        public ushort DurationIndex;
        public ushort RangeIndex;
        public byte SchoolMask;
        public float Speed;
        public float LaunchDelay;
        public float MinDuration;
        public int SpellIconFileDataID;
        public int ActiveIconFileDataID;
        public int ContentTuningID;
        public int ShowFutureSpellPlayerConditionId;
        public int SpellVisualScript;
        public int ActiveSpellVisualScript;
        public int SpellID;
    }
}
