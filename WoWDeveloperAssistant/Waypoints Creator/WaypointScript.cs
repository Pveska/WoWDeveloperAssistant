using System;
using System.Collections.Generic;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    [Serializable]
    public class WaypointScript : ICloneable
    {
        public enum ScriptType : byte
        {
            Talk           = 0,
            Emote          = 1,
            SetField       = 2,
            SetFlag        = 4,
            RemoveFlag     = 5,
            RemoveAura     = 14,
            CastSpell      = 15,
            SetOrientation = 30,
            SetAnimKit     = 100,
            Jump           = 101,
            Unknown        = 99
        };

        public uint id;
        public uint delay;
        public ScriptType type;
        public uint dataLong;
        public uint dataLongSecond;
        public uint dataInt;
        public float x;
        public float y;
        public float z;
        public float o;
        public uint guid;
        public TimeSpan scriptTime;

        WaypointScript(uint id, uint delay, ScriptType type, uint dataLong, uint dataLongSecond, uint dataInt, float x, float y, float z, float o, uint guid, TimeSpan time)
        { this.id = id; this.delay = delay; this.type = type; this.dataLong = dataLong; this.dataLongSecond = dataLongSecond; this.dataInt = dataInt; this.x = x; this.y = y; this.z = z; this.o = o; this.guid = guid; this.scriptTime = time; }

        public static List<WaypointScript> GetScriptsFromUpdatePacket(UpdateObjectPacket updatePacket)
        {
            List<WaypointScript> waypointScripts = new List<WaypointScript>();

            if (updatePacket.emoteStateId != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.Emote, (uint)updatePacket.emoteStateId, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            if (updatePacket.sheatheState != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.SetField, 160, (uint)updatePacket.sheatheState, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            if (updatePacket.standState != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.SetField, 117, (uint)updatePacket.standState, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            return waypointScripts;
        }

        public static WaypointScript GetScriptsFromSpellPacket(SpellStartPacket spellPacket)
        {
            return new WaypointScript(0, 0, ScriptType.CastSpell, spellPacket.spellId, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, spellPacket.spellCastStartTime);
        }

        public static WaypointScript GetScriptsFromMovementPacket(MonsterMovePacket movePacket)
        {
            if (movePacket.HasJump())
            {
                return new WaypointScript(0, 0, ScriptType.Jump, movePacket.jumpInfo.moveTime, 0, 0, movePacket.jumpInfo.jumpPos.x, movePacket.jumpInfo.jumpPos.y, movePacket.jumpInfo.jumpPos.z, movePacket.jumpInfo.jumpGravity, 0, movePacket.packetSendTime);
            }

            return new WaypointScript(0, 0, ScriptType.SetOrientation, 0, 0, 0, 0.0f, 0.0f, 0.0f, movePacket.creatureOrientation, 0, movePacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromAuraUpdatePacket(AuraUpdatePacket auraPacket, Creature creature)
        {
            return new WaypointScript(0, 0, ScriptType.RemoveAura, creature.GetSpellIdForAuraSlot(auraPacket), 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, auraPacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromEmotePacket(EmotePacket emotePacket)
        {
            return new WaypointScript(0, 0, ScriptType.Emote, emotePacket.emoteId, 0, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, emotePacket.packetSendTime);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetId(uint id)
        {
            this.id = id;
        }

        public void SetGuid(uint guid)
        {
            this.guid = guid;
        }

        public void SetDelay(uint delay)
        {
            this.delay = delay;
        }
    }
}
