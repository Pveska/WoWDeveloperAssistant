namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("SpellRange")]

    public sealed class SpellRangeEntry
    {
        public uint ID;
        public string DisplayName_Lang;
        public string DisplayNameShort_lang;
        public int Flags;
        public int[] RangeMin = new int[2];
        public int[] RangeMax = new int[2];
    }
}