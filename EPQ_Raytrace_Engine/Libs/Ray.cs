using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Ray
    {
        private Vec3 Origin;
        private Vec3 Direction;
        private float _time;

        public Ray()
        {
        }

        public Ray(Ray r)
        {
            Origin = r.Origin;
            Direction = r.Direction;
        }

        public Ray(Vec3 a, Vec3 b, float ti = 0)
        {
            Origin = a;
            Direction = b;
            _time = ti;
        }

        public Vec3 GetOrigin
        {
            get { return Origin; }
            set { Origin = value; }
        }

        public Vec3 GetDirection
        {
            get { return Direction; }
            set { Direction = value; }
        }

        public float GetTime
        {
            get { return _time; }
            set { _time = value; }
        }

        public Vec3 PointAtParameter(float t)
        {
            return Origin + Direction * t;
        }
    }
}
