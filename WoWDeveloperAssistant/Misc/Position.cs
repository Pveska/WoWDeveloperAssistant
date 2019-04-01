using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWDeveloperAssistant.Misc
{
    public struct Position
    {
        public float x;
        public float y;
        public float z;
        public float orientation;

        public Position(float x, float y, float z)
        { this.x = x; this.y = y; this.z = z; orientation = 0.0f; }

        public Position(float x, float y, float z, float o)
        { this.x = x; this.y = y; this.z = z; orientation = o; }

        public bool IsValid()
        {
            return x != 0.0f && y != 0.0f;
        }

        public double GetExactDist2dSq(Position mainPos, Position comparePos)
        {
            double dx = mainPos.x - comparePos.x; double dy = mainPos.y - comparePos.y;
            return dx * dx + dy * dy;
        }

        public float GetExactDist2d(Position comparePos)
        {
            return (float)Math.Sqrt(GetExactDist2dSq(this, comparePos));
        }

        public static Position operator -(Position firstPos, Position secondPos)
        {
            float x = firstPos.x - secondPos.x;
            float y = firstPos.y - secondPos.y;
            float z = firstPos.z - secondPos.z;

            return new Position(x, y, z);
        }

        public static bool operator ==(Position firstPos, Position secondPos)
        {
            return firstPos.x == secondPos.x && firstPos.y == secondPos.y && firstPos.z == secondPos.z && firstPos.orientation == secondPos.orientation;
        }

        public static bool operator !=(Position firstPos, Position secondPos)
        {
            return firstPos.x != secondPos.x || firstPos.y != secondPos.y || firstPos.z != secondPos.z || firstPos.orientation != secondPos.orientation;
        }
    }
}
