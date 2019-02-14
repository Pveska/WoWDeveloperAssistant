using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CascStorageLib;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Structures;

namespace WoWDeveloperAssistant
{
    public static class DBC
    {
        public static Storage<SpellEffectEntry> SpellEffect = new Storage<SpellEffectEntry>(GetPath("SpellEffect.db2"));
        public static Storage<SpellNameEntry> SpellName = new Storage<SpellNameEntry>(GetPath("SpellName.db2"));
        public static Storage<SpellMiscEntry> SpellMisc = new Storage<SpellMiscEntry>(GetPath("SpellMisc.db2"));
        public static Storage<SpellCastTimesEntry> SpellCastTimes = new Storage<SpellCastTimesEntry>(GetPath("SpellCastTimes.db2"));

        private static string GetPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Settings.DBCPath, Settings.DBCLocale);
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(GetPath(), fileName);
        }

        public static void Load()
        {
            if (SpellEffect != null && SpellEffectStores.Count == 0)
            {
                Parallel.ForEach(SpellEffect, effect =>
                {
                    var tuple = Tuple.Create((uint)effect.Value.SpellID, (uint)effect.Value.EffectIndex);

                    lock (SpellEffectStores)
                    {
                        SpellEffectStores[tuple] = effect.Value;
                    }
                });
            }
        }

        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
    }
}