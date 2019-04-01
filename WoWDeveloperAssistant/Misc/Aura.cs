using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWDeveloperAssistant.Misc
{
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
