namespace WoWDeveloperAssistant.DBC.Structures
{
    public sealed class CriteriaEntry
    {
        public uint ID;
        public short Type;
        public int Asset;
        public uint ModifierTreeID;
        public byte StartEvent;
        public int StartAsset;
        public ushort StartTimer;
        public byte FailEvent;
        public int FailAsset;
        public byte Flags;
        public short EligibilityWorldStateID;
        public sbyte EligibilityWorldStateValue;
    }
}