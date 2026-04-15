using ProtoBuf;
using System;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Creature_Scripts_Creator
{
    [ProtoContract]
    public class CreatureText
    {
        [ProtoMember(1)]
        public string creatureGuid
        {
            get; set;
        }

        [ProtoMember(2)]
        public string creatureText
        {
            get; set;
        }

        [ProtoMember(3)]
        public bool isAggroText
        {
            get; set;
        }

        [ProtoMember(4)]
        public bool isDeathText
        {
            get; set;
        }

        [ProtoMember(5)]
        public TimeSpan sayTime
        {
            get; set;
        }

        public CreatureText() { }

        public CreatureText(Packets.ChatPacket chatPacket, bool isAggroText = false, bool isDeathText = false)
        {
            creatureGuid = chatPacket.creatureGuid;
            creatureText = chatPacket.creatureText;
            sayTime = chatPacket.packetSendTime;
            this.isAggroText = isAggroText;
            this.isDeathText = isDeathText;
        }
    }
}
