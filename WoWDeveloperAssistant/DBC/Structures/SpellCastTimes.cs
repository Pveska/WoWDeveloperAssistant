using System.Runtime.InteropServices;
namespace WoWDeveloperAssistant.Structures
{
    [DBFile("SpellCastTimesEntry")]

    public sealed class SpellCastTimesEntry
    {
        public int Base;
        public short PerLevel;
        public int Minimum;
    }
}
