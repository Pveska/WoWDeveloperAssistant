using System.IO;
using System.Reflection;
using CascStorageLib;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Structures;

namespace WoWDeveloperAssistant
{
    public static class DBC
    {
        public static Storage<SpellEntry> Spell = new Storage<SpellEntry>(GetPath("Spell.db2"));
        public static Storage<SpellEffectEntry> SpellEffect = new Storage<SpellEffectEntry>(GetPath("SpellEffect.db2"));

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