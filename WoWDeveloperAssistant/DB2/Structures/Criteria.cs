namespace DB2.Structures
{
    [Hotfix("Criteria")]
    public class Criteria
    {
        public short Type;
        public int Asset;
        public uint ModifierTreeId;
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
