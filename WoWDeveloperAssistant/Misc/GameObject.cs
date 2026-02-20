using ProtoBuf;
using System;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Misc
{
    [ProtoContract]
    public class GameObject
    {
        [ProtoMember(1)]
        public string guid
        {
            get; set;
        }

        [ProtoMember(2)]
        public uint entry
        {
            get; set;
        }

        public GameObject(UpdateObjectPacket updatePacket)
        {
            guid = updatePacket.guid;
            entry = updatePacket.entry;
        }

        public void UpdateGameObject(UpdateObjectPacket updatePacket)
        {
            if (guid == "" && updatePacket.guid != "")
                guid = updatePacket.guid;

            if (entry == 0 && updatePacket.entry != 0)
                entry = updatePacket.entry;
        }
    }
}
