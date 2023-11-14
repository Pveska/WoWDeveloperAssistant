using DB2.Structures;
using DB2Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DB2
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HotfixAttribute : Attribute
    {
        public HotfixAttribute(string pTableName)
        {
            TableName = pTableName;
        }

        public string TableName { get; set; }
    }

    public static class Db2
    {
        public static bool Loaded = false;

        public static MySqlStorage<Achievement>      Achievement { get; set; }
        public static MySqlStorage<AreaTable>        AreaTable { get; set; }
        public static MySqlStorage<ConversationLine> ConversationLine { get; set; }
        public static MySqlStorage<Criteria>         Criteria { get; set; }
        public static MySqlStorage<CriteriaTree>     CriteriaTree { get; set; }
        public static MySqlStorage<Map>              Map { get; set; }
        public static MySqlStorage<MapDifficulty>    MapDifficulty { get; set; }
        public static MySqlStorage<ModifierTree>     ModifierTree { get; set; }
        public static MySqlStorage<SpellEffect>      SpellEffect { get; set; }
        public static MySqlStorage<SpellMisc>        SpellMisc { get; set; }
        public static MySqlStorage<SpellName>        SpellName { get; set; }
        public static MySqlStorage<SpellRadius>      SpellRadius { get; set; }
        public static MySqlStorage<SpellRange>       SpellRange { get; set; }
        public static MySqlStorage<SpellDuration>    SpellDuration { get; set; }
        public static MySqlStorage<QuestV2>          QuestV2 { get; set; }


        public static readonly Dictionary<int, string> MapDifficultyStore = new Dictionary<int, string>();
        public static readonly Dictionary<Tuple<uint, uint>, SpellEffect> SpellEffectStore = new Dictionary<Tuple<uint, uint>, SpellEffect>();
        public static readonly Dictionary<int, int> SpellDurationStore = new Dictionary<int, int>();
        public static readonly Dictionary<int, int> QuestBitsStore = new Dictionary<int, int>();

        public static bool IsLoaded()
        {
            return Loaded;
        }

        public static void Load()
        {
            List<string> lFailedDb2 = new List<string>();

            Parallel.ForEach(typeof(Db2).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), db2 =>
            {
                if (!db2.PropertyType.IsGenericType ||
                    (db2.PropertyType.GetGenericTypeDefinition() != typeof(MySqlStorage<>) &&
                    db2.PropertyType.GetGenericTypeDefinition() != typeof(MySqlWorldStorage<>)))
                    return;

                var name = db2.Name;

                try
                {
                    db2.SetValue(db2.GetValue(null), Activator.CreateInstance(db2.PropertyType));
                }
                catch (DirectoryNotFoundException)
                {
                    lFailedDb2.Add(name + ".db2");
                }
            });

            if (lFailedDb2.Count != 0)
            {
                StreamWriter lErrorLog = new StreamWriter(new FileStream("error.log", FileMode.CreateNew));

                foreach (var db2 in lFailedDb2)
                    lErrorLog.WriteLine(db2);

                lErrorLog.Flush();
                lErrorLog.Close();
                return;
            }

            if (SpellEffect != null && SpellEffectStore.Count == 0)
            {
                foreach (var effect in SpellEffect)
                {
                    var tuple = Tuple.Create((uint)effect.Value.SpellId, (uint)effect.Value.EffectIndex);
                    SpellEffectStore[tuple] = effect.Value;
                }
            }

            if (MapDifficulty != null && MapDifficultyStore.Count == 0)
            {
                foreach (var mapDifficulty in MapDifficulty)
                {
                    if (MapDifficultyStore.ContainsKey(mapDifficulty.Value.MapID))
                        MapDifficultyStore[mapDifficulty.Value.MapID] = MapDifficultyStore[mapDifficulty.Value.MapID] + " " + mapDifficulty.Value.DifficultyID;
                    else
                        MapDifficultyStore.Add(mapDifficulty.Value.MapID, Convert.ToString(mapDifficulty.Value.DifficultyID));
                }
            }


            if (SpellMisc != null && SpellDuration != null && SpellDurationStore.Count == 0)
            {
                foreach (var spellMisc in SpellMisc.Where(x => x.Value.DurationIndex != 0))
                {
                    if (!SpellDurationStore.ContainsKey(spellMisc.Value.SpellId))
                    {
                        SpellDurationStore.Add(spellMisc.Value.SpellId, SpellDuration[spellMisc.Value.DurationIndex].MaxDuration != 2147483647 ? SpellDuration[spellMisc.Value.DurationIndex].MaxDuration : -1);
                    }
                }
            }

            if (QuestV2 != null && QuestBitsStore.Count == 0)
            {
                foreach (var quest in QuestV2)
                {
                    if (!QuestBitsStore.ContainsKey(quest.Value.UniqueBitFlag))
                    {
                        QuestBitsStore.Add(quest.Value.UniqueBitFlag, quest.Key);
                    }
                }
            }

            Loaded = true;
        }
    }
}
