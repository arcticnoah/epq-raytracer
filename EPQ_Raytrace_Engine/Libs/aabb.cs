using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class aabb
    {
        private Vec3 _min, _max;

        public aabb()
        {
        }

        public aabb(Vec3 a, Vec3 b)
        {
            _min = a;
            _max = b;
        }

        public Vec3 GetMin
        {
            get { return _min; }
            set { _min = value; }
        }

        public Vec3 GetMax
        {
            get { return _max; }
            set { _max = value; }
        }

        public static float ffmin(float a, float b)
        {
            return a < b ? a : b;
        }

        public static float ffmax(float a, float b)
        {
            return a > b ? a : b;
        }

        public bool Hit(Ray r, ref float tMin, ref float tMax)
        {
            for (int a = 0; a < 3; a++)
            {
                float invD = 1 / r.GetDirection.getList()[a];
                float t0 = (_min.getList()[a] - r.GetOrigin.getList()[a]) * invD;
                float t1 = (_max.getList()[a] - r.GetOrigin.getList()[a]) * invD;

                if (invD < 0)
                {
                    // Swap t0 and t1
                    float tmp = t0;
                    t0 = t1;
                    t1 = tmp;
                }

                tMin = t0 > tMin ? t0 : tMin;
                tMax = t1 < tMax ? t1 : tMax;

                if (tMax <= tMin)
                {
                    return false;
                }
            }
            return true;
        }

        public static aabb SurroundingBox(aabb box0, aabb box1)
        {
            Vec3 small = new Vec3(ffmin(box0.GetMin.x, box1.GetMin.x), ffmin(box0.GetMin.y, box1.GetMin.y), ffmin(box0.GetMin.z, box1.GetMin.z));
            Vec3 big = new Vec3(ffmax(box0.GetMin.x, box1.GetMin.x), ffmax(box0.GetMin.y, box1.GetMin.y), ffmax(box0.GetMin.z, box1.GetMin.z));
            return new aabb(small, big);
        }
    }
}
