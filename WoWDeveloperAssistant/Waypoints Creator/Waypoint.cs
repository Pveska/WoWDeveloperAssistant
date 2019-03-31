using System;
using static WoWDeveloperAssistant.Packets;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    public class Waypoint
    {
        public Position movePosition;
        public float orientation;
        public uint delay;
        public Position startPosition;
        public uint moveTime;
        public TimeSpan moveStartTime;
        public TimeSpan orientationSetTime;

        public Waypoint()
        { movePosition = new Position(); orientation = 0.0f; delay = 0; startPosition = new Position(); moveTime = 0; moveStartTime = new TimeSpan(); orientationSetTime = new TimeSpan(); }

        public Waypoint(Position movePos, float ori, uint delay, Position startPos, uint moveTime, TimeSpan moveStartTime, TimeSpan oriTime)
        { movePosition = movePos; orientation = ori; this.delay = delay; startPosition = startPos; this.moveTime = moveTime; this.moveStartTime = moveStartTime; orientationSetTime = oriTime; }

        public bool HasOrientation()
        {
            return orientation != 0.0f;
        }
    }
}
