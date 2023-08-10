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
        public float AmbientMultiplier;
        public int MountFlags;
        public short PvpCombatWorldStateId;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsId;
        public int ContentTuningId;
        public int[] Flags = new int[2];
        public ushort[] LiquidTypeId = new ushort[4];
    }
}
