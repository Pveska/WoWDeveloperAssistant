using System;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Misc
{
    [Serializable]
    public class GameObject
    {
        public string guid;
        public uint entry;

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
