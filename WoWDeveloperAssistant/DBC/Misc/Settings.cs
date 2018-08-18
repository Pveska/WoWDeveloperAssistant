namespace WoWDeveloperAssistant.Misc
{
    public static class Settings
    {
        private static readonly Configuration Conf = new Configuration();

        public static readonly string DBCPath = Conf.GetString("DBCPath", "dbc");
        public static readonly string DBCLocale = Conf.GetString("DBCLocale", "enUS");
    }
}