using ProtoBuf;
using System;

namespace WoWDeveloperAssistant.Misc
{
    [ProtoContract]
    public struct Aura
    {
        [ProtoMember(1)]
        public uint slot
        {
            get; set;
        }

        [ProtoMember(2)]
        public bool HasAura
        {
            get; set;
        }

        [ProtoMember(3)]
        public TimeSpan time
        {
            get; set;
        }

        [ProtoMember(4)]
        public uint spellId
        {
            get; set;
        }

        public Aura(uint slot, bool HasAura, TimeSpan time, uint spellId)
        { this.slot = slot; this.HasAura = HasAura; this.time = time; this.spellId = spellId; }
    }
}
