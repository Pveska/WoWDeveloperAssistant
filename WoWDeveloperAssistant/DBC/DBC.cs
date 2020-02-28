using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using DBFileReaderLib;
using WoWDeveloperAssistant.DBC.Misc;
using WoWDeveloperAssistant.DBC.Structures;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.DBC
{
    public static class DBC
    {
        private static bool loaded;
        public static Storage<SpellEffectEntry> SpellEffect { get; set; }
        public static Storage<SpellNameEntry> SpellName { get; set; }
        public static Storage<SpellMiscEntry> SpellMisc { get; set; }
        public static Storage<SpellCastTimesEntry> SpellCastTimes { get; set; }
        public static Storage<MapEntry> Map { get; set; }
        public static Storage<AchievementEntry> Achievement { get; set; }
        public static Storage<CriteriaTreeEntry> CriteriaTree { get; set; }
        public static Storage<CriteriaEntry> Criteria { get; set; }
        public static Storage<ModifierTreeEntry> ModifierTree { get; set; }
        public static Storage<MapDifficultyEntry> MapDifficulty { get; set; }

        private static string GetPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Settings.DBCPath, Settings.DBCLocale);
        }

        public static void Load()
        {
            if (!Directory.Exists(GetPath()))
                return;

            var dbReader = new DBReader(GetPath(), "SpellEffect.db2");
            SpellEffect = dbReader.GetRecords<SpellEffectEntry>();

            dbReader = new DBReader(GetPath(), "SpellName.db2");
            SpellName = dbReader.GetRecords<SpellNameEntry>();

            dbReader = new DBReader(GetPath(), "SpellMisc.db2");
            SpellMisc = dbReader.GetRecords<SpellMiscEntry>();

            dbReader = new DBReader(GetPath(), "SpellCastTimes.db2");
            SpellCastTimes = dbReader.GetRecords<SpellCastTimesEntry>();

            dbReader = new DBReader(GetPath(), "Map.db2");
            Map = dbReader.GetRecords<MapEntry>();

            dbReader = new DBReader(GetPath(), "Achievement.db2");
            Achievement = dbReader.GetRecords<AchievementEntry>();

            dbReader = new DBReader(GetPath(), "CriteriaTree.db2");
            CriteriaTree = dbReader.GetRecords<CriteriaTreeEntry>();

            dbReader = new DBReader(GetPath(), "Criteria.db2");
            Criteria = dbReader.GetRecords<CriteriaEntry>();

            dbReader = new DBReader(GetPath(), "ModifierTree.db2");
            ModifierTree = dbReader.GetRecords<ModifierTreeEntry>();

            dbReader = new DBReader(GetPath(), "MapDifficulty.db2");
            MapDifficulty = dbReader.GetRecords<MapDifficultyEntry>();

            if (SpellEffect != null && SpellEffectStores.Count == 0)
            {
                foreach (var effect in SpellEffect)
                {
                    var tuple = Tuple.Create((uint)effect.Value.SpellID, (uint)effect.Value.EffectIndex);
                    SpellEffectStores[tuple] = effect.Value;
                }
            }

            if (MapDifficulty != null && MapSpawnDifficultyStore.Count == 0)
            {
                foreach (var mapDifficulty in MapDifficulty)
                {
                    if (MapSpawnDifficultyStore.ContainsKey(mapDifficulty.Value.MapID))
                        MapSpawnDifficultyStore[mapDifficulty.Value.MapID] = MapSpawnDifficultyStore[mapDifficulty.Value.MapID] + " " + mapDifficulty.Value.DifficultyID;
                    else
                        MapSpawnDifficultyStore.Add(mapDifficulty.Value.MapID, Convert.ToString(mapDifficulty.Value.DifficultyID));
                }
            }

            loaded = true;
        }

        public static bool IsLoaded()
        {
            return loaded;
        }

        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
        public static readonly Dictionary<int, string> MapSpawnDifficultyStore = new Dictionary<int, string>();
    }
}