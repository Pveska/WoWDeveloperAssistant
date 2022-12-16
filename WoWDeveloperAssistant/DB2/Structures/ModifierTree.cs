namespace DB2.Structures
{
    [Hotfix("ModifierTree")]
    public sealed class ModifierTree
    {
        public int Parent;
        public sbyte Operator;
        public sbyte Amount;
        public int Type;
        public int Asset;
        public int SecondaryAsset;
        public int TertiaryAsset;
    }
}
