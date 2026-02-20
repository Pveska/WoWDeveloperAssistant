using System;

namespace DB2.Structures
{
    [Hotfix("MapDifficulty")]
    public class MapDifficulty
    {
        public string Message;
        public short DifficultyID;
        public int LockID;
        public byte ResetInterval;
        public int MaxPlayers;
        public byte ItemContext;
        public int ItemContextPickerID;
        public int Flags;
        public int ContentTuningID;
        public int WorldStateExpressionId;
        public int MapID;
    }
}