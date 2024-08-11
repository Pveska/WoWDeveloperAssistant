using System;

namespace DB2.Structures
{
    [Hotfix("Achievement")]
    public sealed class Achievement
    {
        public string Description;
        public string Title;
        public string Reward;
        public short InstanceID;
        public sbyte Faction;
        public int Supercedes;
        public short Category;
        public sbyte MinimumCriteria;
        public sbyte Points;
        public int Flags;
        public short UiOrder;
        public int IconFileID;
        public int RewardItemID;
        public uint CriteriaTree;
        public short SharesCriteria;
        public int CovenantID;
        public int HiddenBeforeDisplaySeason;
        public int LegacyAfterTimeEvent;
    }
}
