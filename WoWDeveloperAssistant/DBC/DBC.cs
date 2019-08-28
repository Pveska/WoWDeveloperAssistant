using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DB2FileReaderLib.NET;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Structures;

namespace WoWDeveloperAssistant
{
    public static class DBC
    {
        public static Storage<SpellEffectEntry> SpellEffect { get; set; }
        public static Storage<SpellNameEntry> SpellName { get; set; }
        public static Storage<SpellMiscEntry> SpellMisc { get; set; }
        public static Storage<SpellCastTimesEntry> SpellCastTimes { get; set; }
        public static Storage<MapEntry> Map { get; set; }

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
            Parallel.ForEach(typeof(DBC).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), dbc =>
            {
                Type type = dbc.PropertyType.GetGenericArguments()[0];

                if (!type.IsClass)
                    return;

                var attr = type.GetCustomAttribute<DBFileAttribute>();
                if (attr == null)
                    return;

                var instanceType = typeof(Storage<>).MakeGenericType(type);
                var instance = Activator.CreateInstance(instanceType, $"{ GetPath(attr.FileName) }.db2");

                try
                {
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