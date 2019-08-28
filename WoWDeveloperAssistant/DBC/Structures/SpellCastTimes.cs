namespace WoWDeveloperAssistant.Structures
{
    [DBFile("SpellCastTimes")]

    public sealed class SpellCastTimesEntry
    {
        public int Base;
        public short PerLevel;
        public int Minimum;
    }
}
