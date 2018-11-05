using System.IO;
using System.Reflection;
using CascStorageLib;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Structures;

namespace WoWDeveloperAssistant
{
    public static class DBC
    {
        public static Storage<SpellEntryLegion> SpellLegion = new Storage<SpellEntryLegion>(GetPath("SpellLegion.db2"));
        public static Storage<SpellEffectEntryLegion> SpellEffectlegion = new Storage<SpellEffectEntryLegion>(GetPath("SpellEffectLegion.db2"));
        public static Storage<SpellEffectEntryBfa> SpellEffectBfa = new Storage<SpellEffectEntryBfa>(GetPath("SpellEffectBfa.db2"));
        public static Storage<SpellNameEntryBfa> SpellNameBfa = new Storage<SpellNameEntryBfa>(GetPath("SpellNameBfa.db2"));

        private static string GetPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Settings.DBCPath, Settings.DBCLocale);
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(GetPath(), fileName);
        }
    }
}