namespace DB2.Structures
{
    [Hotfix("Criteria")]
    public class Criteria
    {
        public short Type;
        public int Asset;
        public uint ModifierTreeId;
        public int StartEvent;
        public int StartAsset;
        public ushort StartTimer;
        public int FailEvent;
        public int FailAsset;
        public int Flags;
        public short EligibilityWorldStateID;
        public sbyte EligibilityWorldStateValue;
    }
}
