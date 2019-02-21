using System;

namespace WoWDeveloperAssistant
{
    public class CreatureText
    {
        public string creatureText;
        public bool isAggroText;
        public bool isDeadText;
        public bool isHealthPctText;
        public TimeSpan sayTime;

        public CreatureText(Packets.ChatPacket chatPacket)
        {
            creatureText = chatPacket.creatureText;
            sayTime = chatPacket.packetSendTime;
            isAggroText = false;
            isDeadText = false;
            isHealthPctText = false;
        }
    }
}
