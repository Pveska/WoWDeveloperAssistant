namespace DB2.Structures
{
    [Hotfix("SpellRange")]
    public sealed class SpellRange
    {
        public string DisplayName;
        public string DisplayNameShort;
        public int Flags;
        public float[] RangeMin = new float[2];
        public float[] RangeMax = new float[2];
    }
}
