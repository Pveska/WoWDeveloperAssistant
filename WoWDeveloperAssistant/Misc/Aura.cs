using ProtoBuf;
using System;

namespace WoWDeveloperAssistant.Misc
{
    [ProtoContract]
    public class Aura
    {
        [ProtoMember(1)]
        public uint slot
        {
            get; set;
        } = 0;

        [ProtoMember(2)]
        public bool hasAura
        {
            get; set;
        } = false;

        [ProtoMember(3)]
        public TimeSpan time
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(4)]
        public uint spellId
        {
            get; set;
        } = 0;

        public Aura() { }
        public Aura(uint slot, bool HasAura, TimeSpan time, uint spellId)
        { this.slot = slot; this.hasAura = HasAura; this.time = time; this.spellId = spellId; }
    }
}
