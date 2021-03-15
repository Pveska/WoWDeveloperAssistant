using DBFileReaderLib.Attributes;

namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("SpellName")]
    public sealed class SpellNameEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
    }
}
