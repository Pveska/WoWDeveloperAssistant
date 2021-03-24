using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DBFileReaderLib;
using WoWDeveloperAssistant.DBC.Structures;

namespace WoWDeveloperAssistant.DBC
{
    public static class DBC
    {
        private static bool loaded;
        public static Storage<AchievementEntry> Achievement { get; set; }
        public static Storage<CriteriaTreeEntry> CriteriaTree { get; set; }
        public static Storage<CriteriaEntry> Criteria { get; set; }
        public static Storage<ModifierTreeEntry> ModifierTree { get; set; }
        public static Storage<MapEntry> Map { get; set; }
        public static Storage<MapDifficultyEntry> MapDifficulty { get; set; }
        public static Storage<SpellEffectEntry> SpellEffect { get; set; }
        public static Storage<SpellNameEntry> SpellName { get; set; }

        private static string GetDBCPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "dbc", "enUS");
        }

        private static string GetDBCPath(string fileName)
        {
            return Path.Combine(GetDBCPath(), fileName);
        }

        public static bool IsLoaded()
        {
            return loaded;
        }

        public static void Load()
        {
            Parallel.ForEach(typeof(DBC).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), dbc =>
            {
                Type type = dbc.PropertyType.GetGenericArguments()[0];

                if (!type.IsClass)
                    return;

                var startTime = DateTime.Now;
                var attr = type.GetCustomAttribute<DBFileAttribute>();
                if (attr == null)
                    return;

                string pathName = GetDBCPath(attr.FileName) + ".db2";
                var instanceType = typeof(Storage<>).MakeGenericType(type);
                var countGetter = instanceType.GetProperty("Count").GetGetMethod();
                dynamic instance = Activator.CreateInstance(instanceType, pathName);
                var recordCount = (int)countGetter.Invoke(instance, new object[] { });

                try
                {
                    var db2Reader = new DBReader($"{ GetDBCPath(attr.FileName) }.db2");

                    dbc.SetValue(dbc.GetValue(null), instance);
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is ArgumentException)
                        throw new ArgumentException($"Failed to load {attr.FileName}.db2: {tie.InnerException.Message}");
                    throw;
                }
            });

            if (SpellEffect != null && SpellEffectStores.Count == 0)
            {
                foreach (var effect in SpellEffect)
                {
                    var tuple = Tuple.Create((uint)effect.Value.SpellID, (uint)effect.Value.EffectIndex);
                    SpellEffectStores[tuple] = effect.Value;
                }
            }

            if (MapDifficulty != null && MapDifficultyStores.Count == 0)
            {
                foreach (var mapDifficulty in MapDifficulty)
                {
                    if (MapDifficultyStores.ContainsKey(mapDifficulty.Value.MapID))
                        MapDifficultyStores[mapDifficulty.Value.MapID] = MapDifficultyStores[mapDifficulty.Value.MapID] + " " + mapDifficulty.Value.DifficultyID;
                    else
                        MapDifficultyStores.Add(mapDifficulty.Value.MapID, Convert.ToString(mapDifficulty.Value.DifficultyID));
                }
            }

            loaded = true;
        }

        public static readonly Dictionary<int, string> MapDifficultyStores = new Dictionary<int, string>();
        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
    }
}
