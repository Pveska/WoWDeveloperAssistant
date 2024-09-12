using System;
using System.Collections.Generic;

namespace WoWDeveloperAssistant.Misc
{
    [Serializable]
    public struct Position
    {
        public bool Equals(Position other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && orientation.Equals(other.orientation);
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ orientation.GetHashCode();
                return hashCode;
            }
        }

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
        public float GetDistance(Position comparePos)
        {
            return (float)Math.Sqrt(Math.Pow((x - comparePos.x), 2) + Math.Pow((y - comparePos.y), 2) + Math.Pow((z - comparePos.z), 2));
        }

        public static double GetExactDist2dSq(Position mainPos, Position comparePos)
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

        private bool HasInArc(float arc, Position refPos)
        {
            if (refPos == this)
                return true;

            arc = NormalizeOrientation(arc);

            float angle = GetAngle(refPos);
            angle -= orientation;

            angle = NormalizeOrientation(angle);
            if (angle > 3.141592653589793f)
                angle -= 2.0f * 3.141592653589793f;

            float lborder = -1 * (arc / 2.0f);
            float rborder = (arc / 2.0f);
            return ((angle >= lborder) && (angle <= rborder));
        }

        public bool IsInFront(Position refPos, float arc = 3.141592653589793f)
        {
            return HasInArc(arc, refPos);
        }

        public bool IsInBack(Position refPos, float arc = 3.141592653589793f)
        {
            return !HasInArc(2 * 3.141592653589793f - arc, refPos);
        }

        private float NormalizeOrientation(float o)
        {
            if (o >= 0.0f && o < 6.2831864f)
                return o;

            if (o < 0)
            {
                float mod = -o;
                mod = (float)Math.IEEERemainder(mod, 2.0f * 3.141592653589793f);
                mod = -mod + 2.0f * 3.141592653589793f;
                return mod;
            }

            return (float)Math.IEEERemainder(o, 2.0f * 3.141592653589793f);
        }

        public float GetAngle(Position refPos)
        {
            float dx = refPos.x - x;
            float dy = refPos.y - y;

            float ang = (float)Math.Atan2(dy, dx);
            ang = (ang >= 0) ? ang : 2 * 3.141592653589793f + ang;
            return ang;
        }

        public Position SimplePosXYRelocationByAngle(float dist, float angle)
        {
            float x = this.x + dist * (float)Math.Cos(angle);
            float y = this.y + dist * (float)Math.Sin(angle);
            float z = this.z;
            return new Position(x, y, z);
        }

        public static float toRadians(float deg)
        {
            return deg * (float)3.1415926535898f / 180.0f;
        }

        public bool IsInPolygon(List<Position> polygon)
        {
            if (polygon.Count < 3)
                return false;

            int count = polygon.Count;
            bool ok = false;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                Position point = new Position(x, y, z);
                Position curr = polygon[i];
                Position prev = polygon[j];

                if (((curr.y > point.y) != (prev.y > point.y)) && (point.x < (prev.x - curr.x) * (point.y - curr.y) / (prev.y - curr.y) + curr.x))
                {
                    ok = !ok;
                }
            }

            return ok;
        }
    }
}