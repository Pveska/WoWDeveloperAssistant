using System;

namespace DB2.Structures
{
    [Hotfix("Map")]
    public class Map
    {
        public string Directory;
        public string MapName;
        public string MapDescription0;
        public string MapDescription1;
        public string PvpShortDescription;
        public string PvpLongDescription;
        public float Corpse0;
        public float Corpse1;
        public byte MapType;
        public sbyte InstanceType;
        public byte ExpansionId;
        public ushort AreaTableId;
        public short LoadingScreenId;
        public short TimeOfDayOverride;
        public short ParentMapId;
        public short CosmeticParentMapId;
        public byte TimeOffset;
        public float MinimapIconScale;
        public short CorpseMapId;
        public byte MaxPlayers;
        public short WindSettingsId;
        public int ZmpFileDataId;
        public int WdtFileDataId;
        public int NavigationMaxDistance;
        public int PreloadFileDataId;
        public int Flags;
        public int Flags2;
        public int Flags3;
    }
}
