using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class HitRecord
    {
        public float t, u, v;
        public Vec3 p;
        public Vec3 normal;
        public Material mat_ptr;

        public HitRecord()
        {
            p = new Vec3();
            normal = new Vec3();
        }

        public void Set(HitRecord src)
        {
            t = src.t;
            p = src.p;
            u = src.u;
            v = src.v;
            normal = src.normal;
            mat_ptr = src.mat_ptr;
        }
    }

    interface Hitable
    {
        bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec);

        bool BoundingBox(float t0, float t1, ref aabb box);
    }

    class FlipNormals : Hitable
    {
        private Hitable ptr;

        public FlipNormals(Hitable p)
        {
            ptr = p;
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            if (ptr.Hit(r, tMin, tMax, ref rec))
            {
                rec.normal = new Vec3() - rec.normal;
                return true;
            }
            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            return ptr.BoundingBox(t0, t1, ref box);
        }
    }
}
