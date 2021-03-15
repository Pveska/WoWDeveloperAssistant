using System;

namespace WoWDeveloperAssistant.DBC.Structures
{
    public sealed class DBFileAttribute : Attribute
    {
        public string FileName { get;}

        public DBFileAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
