using System;

namespace WoWDeveloperAssistant.Misc
{
    [Serializable]
    public struct Aura
    {
        public uint slot;
        public bool HasAura;
        public TimeSpan time;
        public uint spellId;

        public Aura(uint slot, bool HasAura, TimeSpan time, uint spellId)
        { this.slot = slot; this.HasAura = HasAura; this.time = time; this.spellId = spellId; }
    }
}
