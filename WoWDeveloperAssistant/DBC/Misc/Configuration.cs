using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace WoWDeveloperAssistant.DBC.Misc
{
    public class Configuration
    {
        private readonly KeyValueConfigurationCollection _settingsCollection;

        public Configuration()
        {
            _settingsCollection = GetConfiguration();
        }

        public Configuration(KeyValueConfigurationCollection configCollection)
        {
            _settingsCollection = configCollection;
        }

        private static KeyValueConfigurationCollection GetConfiguration()
        {
            var args = Environment.GetCommandLineArgs();
            var opts = new Dictionary<string, string>();
            string configFile = null;
            KeyValueConfigurationCollection settings = null;
            for (int i = 1; i < args.Length - 1; ++i)
            {
                string opt = args[i];
                if (!opt.StartsWith("--", StringComparison.CurrentCultureIgnoreCase))
                    break;

                // analyze options
                string optname = opt.Substring(2);
                switch (optname)
                {
                    case "ConfigFile":
                        configFile = args[i + 1];
                        break;
                    default:
                        opts.Add(optname, args[i + 1]);
                        break;
                }
                ++i;
            }
            // load different config file
            if (configFile != null)
            {
                string configPath = Path.Combine(Environment.CurrentDirectory, configFile);

                try
                {
                    // Get the mapped configuration file
                    var config = ConfigurationManager.OpenExeConfiguration(configPath);


                    settings = ((AppSettingsSection)config.GetSection("appSettings")).Settings;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not load config file {0}, reason: {1}", configPath, ex.Message);
                }
            }
            if (settings == null)
                settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

            // override config options with options from command line
            foreach (var pair in opts)
            {
                settings.Remove(pair.Key);
                settings.Add(pair.Key, pair.Value);
            }

            return settings;
        }

        public string GetString(string key, string defValue)
        {
            KeyValueConfigurationElement s = _settingsCollection[key];
            return s?.Value ?? defValue;
        }
    }
}