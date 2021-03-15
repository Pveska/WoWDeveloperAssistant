namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("ModifierTree")]

    public sealed class ModifierTreeEntry
    {
        public uint ID;
        public uint Parent;
        public sbyte Operator;
        public byte Amount;
        public uint Type;
        public uint Asset;
        public int SecondaryAsset;
        public byte TertiaryAsset;
    }
}
