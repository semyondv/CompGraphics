using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTrace {
    public class PointXYZ {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PointXYZ() {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public PointXYZ(float _x, float _y, float _z) {
            X = _x;
            Y = _y;
            Z = _z;
        }

        public PointXYZ(PointXYZ p) {
            if (p == null)
                return;
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public float Distance() {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static PointXYZ operator -(PointXYZ p1, PointXYZ p2) {
            return new PointXYZ(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);

        }

        public static float ScalarMul(PointXYZ p1, PointXYZ p2) {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }

        public static PointXYZ normal(PointXYZ p) {
            float z = (float)Math.Sqrt((float)(p.X * p.X + p.Y * p.Y + p.Z * p.Z));
            if (z == 0)
                return new PointXYZ(p);
            return new PointXYZ(p.X / z, p.Y / z, p.Z / z);
        }

        public static PointXYZ operator +(PointXYZ p1, PointXYZ p2) {
            return new PointXYZ(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);

        }

        public static PointXYZ operator *(PointXYZ p1, PointXYZ p2) {
            return new PointXYZ(p1.Y * p2.Z - p1.Z * p2.Y, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
        }

        public static PointXYZ operator *(float t, PointXYZ p1) {
            return new PointXYZ(p1.X * t, p1.Y * t, p1.Z * t);
        }

        public static PointXYZ operator *(PointXYZ p1, float t) {
            return new PointXYZ(p1.X * t, p1.Y * t, p1.Z * t);
        }

        public static PointXYZ operator /(PointXYZ p1, float t) {
            return new PointXYZ(p1.X / t, p1.Y / t, p1.Z / t);
        }
    }
}
