using System.Runtime.InteropServices;
namespace WoWDeveloperAssistant.Structures
{
    [DBFile("SpellMiscEntry")]

    public sealed class SpellMiscEntry
    {
        public byte DifficultyID;
        public short CastingTimeIndex;
        public short DurationIndex;
        public short RangeIndex;
        public byte SchoolMask;
        public float Speed;
        public float LaunchDelay;
        public float UnknownField;
        public int SpellIconFileDataID;
        public int ActiveIconFileDataID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public int[] Attributes;
    }
}
