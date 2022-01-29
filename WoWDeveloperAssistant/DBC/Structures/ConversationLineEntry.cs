namespace WoWDeveloperAssistant.DBC.Structures
{
    [DBFile("ConversationLine")]
    public sealed class ConversationLineEntry
    {
        public uint Id;
        public uint BroadcastTextId;
        public uint SpellVisualKitId;
        public int AdditionalDuration;
        public short NextConversationLineId;
        public short AnimKitId;
        public byte SpeechType;
        public byte StartAnimation;
        public byte EndAnimation;
    }
}