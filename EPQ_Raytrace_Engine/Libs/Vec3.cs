using System;

namespace EPQ_Raytrace_Engine.Libs
{
    class Vec3
    {
        private static Random random = new Random();

        private float[] e = new float[3];

        public Vec3()
        {
        }

        public float this[int index]
        {
            get => e[index];
            set => e[index] = value;
        }

        public Vec3(Vec3 v)
        {
            e[0] = v.e[0];
            e[1] = v.e[1];
            e[2] = v.e[2];
        }

        public Vec3(float e0, float e1, float e2)
        {
            e[0] = e0;
            e[1] = e1;
            e[2] = e2;
        }

        public float[] getList()
        {
            return e;
        }

        public float x
        {
            get { return e[0]; }
            set { e[0] = value; }
        }

        public float y
        {
            get { return e[1]; }
            set { e[1] = value; }
        }

        public float z
        {
            get { return e[2]; }
            set { e[2] = value; }
        }

        public float r
        {
            get { return e[0]; }
            set { e[0] = value; }
        }

        public float g
        {
            get { return e[1]; }
            set { e[1] = value; }
        }

        public float b
        {
            get { return e[2]; }
            set { e[2] = value; }
        }

        public static Vec3 RandomInUnitDisk()
        {
            return (new Vec3(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                0
            ) * 2 - new Vec3(1, 1, 0)).Normalize();
        }

        public static Vec3 RandomInUnitSphere()
        {
            return (new Vec3(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble()
            ) * 2 - new Vec3(1, 1, 1)).Normalize();
            /*Vec3 p;
            do
            {
                p = new Vec3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) * 2 - new Vec3(1,1,1);
            } while (p.SquaredLength() >= 1.0);
            return p;*/
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vec3 operator /(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return (a.x == b.x && a.y == b.y && a.z == b.z);
        }

        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return (a.x != b.x || a.y != b.y || a.z != b.z);
        }

        public static Vec3 operator +(Vec3 a, float b)
        {
            return new Vec3(a.x + b, a.y + b, a.z + b);
        }

        public static Vec3 operator -(Vec3 a, float b)
        {
            return new Vec3(a.x - b, a.y - b, a.z - b);
        }

        public static Vec3 operator *(Vec3 a, float b)
        {
            return new Vec3(a.x * b, a.y * b, a.z * b);
        }

        public static Vec3 operator /(Vec3 a, float b)
        {
            return new Vec3(a.x / b, a.y / b, a.z / b);
        }

        public float Length()
        {
            return (float)Math.Sqrt(e[0] * e[0] + e[1] * e[1] + e[2] * e[2]);
        }

        public float SquaredLength()
        {
            return e[0] * e[0] + e[1] * e[1] + e[2] * e[2];
        }

        public float Dot(Vec3 a)
        {
            return a.x * x + a.y * y + a.z * z;
        }

        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public Vec3 Cross(Vec3 a)
        {
            return new Vec3(
                a.y * z - a.z * y,
                a.z * x - a.x * z,
                a.x * y - a.y * x);
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x);
        }

        public float magnitude
        {
            get { return (float)Math.Sqrt(sqr_magnitude); }
        }

        public double sqr_magnitude
        {
            get { return (x * x + y * y + z * z); }
        }

        public void Normalized()
        {
            e[0] /= magnitude;
            e[1] /= magnitude;
            e[2] /= magnitude;
        }

        public Vec3 Normalize()
        {
            return this / magnitude;
        }

        public Vec3 Pow(float exp)
        {
            x = (float)Math.Pow(x, exp);
            y = (float)Math.Pow(y, exp);
            z = (float)Math.Pow(z, exp);
            return this;
        }

        public Vec3 Min(float n)
        {
            x = Math.Min(x, n);
            y = Math.Min(y, n);
            z = Math.Min(z, n);
            return this;
        }

        public Vec3 Max(float n)
        {
            x = Math.Max(x, n);
            y = Math.Max(y, n);
            z = Math.Max(z, n);
            return this;
        }

        public Vec3 Zero()
        {
            x = 0;
            y = 0;
            z = 0;
            return this;
        }

        public static Vec3 EqualZero()
        {
            return new Vec3(0, 0, 0);
        }

        public Vec3 Set(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
            return this;
        }

        public static Vec3 unitVector(Vec3 a)
        {
            return a / a.Length();
        }

        public static Vec3 Random()
        {
            return new Vec3(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble()
            ).Normalize();
        }

        public static Vec3 Reflect(Vec3 v, Vec3 n)
        {
            return v - n * Vec3.Dot(v, n) * 2;
        }

        public static bool Refract(Vec3 v, Vec3 n, float niOverNt, ref Vec3 r)
        {
            Vec3 uv = v.Normalize();
            float dt = Vec3.Dot(uv, n);
            float discriminant = 1.0f - niOverNt * niOverNt * (1 - dt * dt);
            if (discriminant > 0)
            {
                r = (uv - n * dt) * niOverNt - n * (float)Math.Sqrt(discriminant);
                return true;
            }
            return false;
        }
    }
}