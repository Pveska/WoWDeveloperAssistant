namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("SpellCastTimes")]

    public sealed class SpellCastTimesEntry
    {
        public uint ID;
        public int Base;
        public int Minimum;
    }
}
