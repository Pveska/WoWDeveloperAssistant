namespace DB2.Structures
{
    [Hotfix("CriteriaTree")]
    public sealed class CriteriaTree
    {
        public string Description;
        public uint Parent;
        public uint Amount;
        public sbyte Operator;
        public uint CriteriaID;
        public int OrderIndex;
        public int Flags;
    }
}
