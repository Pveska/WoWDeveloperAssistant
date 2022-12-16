namespace DB2.Structures
{
    [Hotfix("AreaTable")]
    public class AreaTable
    {
        public string ZoneName;
        public string AreaName;
        public ushort ContinentId;
        public ushort ParentAreaId;
        public short AreaBit;
        public byte SoundProviderPref;
        public byte SoundProviderPrefUnderwater;
        public ushort AmbienceId;
        public ushort UwAmbience;
        public ushort ZoneMusic;
        public ushort UwZoneMusic;
        public ushort IntroSound;
        public uint UwIntroSound;
        public byte FactionGroupMask;
        public float Ambient_multiplier;
        public byte MountFlags;
        public ushort PvpCombatWorldStateId;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsId;
        public uint ContentTuningId;
        public uint Flags;
        public uint Flags2;
        public ushort[] LiquidTypeId = new ushort[4];
    }
}
