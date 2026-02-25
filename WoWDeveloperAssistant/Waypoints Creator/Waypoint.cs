using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets.MonsterMovePacket;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    [ProtoContract]
    public class Waypoint : ICloneable
    {
        [ProtoMember(1)]
        public Position movePosition
        {
            get; set;
        } = new Position();

        [ProtoMember(2)]
        public float orientation
        {
            get; set;
        } = 0.0f;

        [ProtoMember(3)]
        public uint delay
        {
            get; set;
        } = 0;

        [ProtoMember(4)]
        public Position startPosition
        {
            get; set;
        } = new Position();

        [ProtoMember(5)]
        public uint moveTime
        {
            get; set;
        } = 0;

        [ProtoMember(6)]
        public TimeSpan moveStartTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(7)]
        public TimeSpan orientationSetTime
        {
            get; set;
        } = new TimeSpan();

        [ProtoMember(8)]
        public List<WaypointScript> scripts
        {
            get; set;
        } = new List<WaypointScript>();

        [ProtoMember(9)]
        public uint idFromParse
        {
            get; set;
        } = 0;

        [ProtoMember(10)]
        public MoveType moveType
        {
            get; set;
        } = MoveType.MOVE_UNKNOWN;

        [ProtoMember(11)]
        public float velocity
        {
            get; set;
        } = 0.0f;

        [ProtoMember(12)]
        public long packetNumber
        {
            get; set;
        } = 0;

        public Waypoint() { }

        public Waypoint(Position movePos, float ori, uint delay, Position startPos, uint moveTime, TimeSpan moveStartTime, TimeSpan oriTime, List<WaypointScript> scripts, uint id, MoveType moveType)
        { movePosition = movePos; orientation = ori; this.delay = delay; startPosition = startPos; this.moveTime = moveTime; this.moveStartTime = moveStartTime; orientationSetTime = oriTime; this.scripts = scripts; idFromParse = id; this.moveType = moveType; }

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
            waypoint.moveType = moveType;
            waypoint.packetNumber = packetNumber;
            waypoint.velocity = velocity;

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
