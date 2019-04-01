using System;
using System.Collections.Generic;
using System.Linq;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    public class Waypoint : ICloneable
    {
        public Position movePosition;
        public float orientation;
        public uint delay;
        public Position startPosition;
        public uint moveTime;
        public TimeSpan moveStartTime;
        public TimeSpan orientationSetTime;
        public List<WaypointScript> scripts;

        public Waypoint() {}

        public Waypoint(Position movePos, float ori, uint delay, Position startPos, uint moveTime, TimeSpan moveStartTime, TimeSpan oriTime, List<WaypointScript> scripts)
        { movePosition = movePos; orientation = ori; this.delay = delay; startPosition = startPos; this.moveTime = moveTime; this.moveStartTime = moveStartTime; orientationSetTime = oriTime; this.scripts = scripts; }

        public bool HasOrientation()
        {
            return orientation != 0.0f;
        }

        public bool HasScripts()
        {
            return scripts.Count != 0;
        }

        public uint GetScriptId()
        {
            uint scriptId = 0;

            if (scripts.Count != 0)
                scriptId = scripts.First().id;

            return scriptId;
        }

        public object Clone()
        {
            Waypoint waypoint = new Waypoint();
            waypoint.movePosition.x = movePosition.x;
            waypoint.movePosition.y = movePosition.y;
            waypoint.movePosition.z = movePosition.z;
            waypoint.movePosition.orientation = movePosition.orientation;
            waypoint.orientation = orientation;
            waypoint.delay = delay;
            waypoint.startPosition.x = startPosition.x;
            waypoint.startPosition.y = startPosition.y;
            waypoint.startPosition.z = startPosition.z;
            waypoint.startPosition.orientation = startPosition.orientation;
            waypoint.moveTime = moveTime;
            waypoint.moveStartTime = moveStartTime;
            waypoint.orientationSetTime = orientationSetTime;
            waypoint.scripts = new List<WaypointScript>();

            foreach (WaypointScript script in scripts)
            {
                waypoint.scripts.Add((WaypointScript)script.Clone());
            }

            return waypoint;
        }

        public void SetOrientation(float orientation)
        {
            this.orientation = orientation;
        }

        public void SetOrientationSetTime(TimeSpan time)
        {
            orientationSetTime = time;
        }

        public void SetDelay(uint delay)
        {
            this.delay = delay;
        }
    }
}
