using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class xyRect : Hitable
    {
        private float x0, x1, y0, y1, k;
        private Material mat;
        public xyRect()
        {
        }

        public xyRect(float _x0, float _x1, float _y0, float _y1, float _k, Material m)
        {
            x0 = _x0;
            x1 = _x1;
            y0 = _y0;
            y1 = _y1;
            k = _k;
            mat = m;
        }

        public bool Hit(Ray r, float t0, float t1, ref HitRecord rec)
        {
            float t = (k - r.GetOrigin.z) / r.GetDirection.z;
            if (t < t0 || t > t1) return false;
            float x = r.GetOrigin.x + t * r.GetDirection.x;
            float y = r.GetOrigin.y + t * r.GetDirection.y;
            if (x < x0 || x > x1 || y < y0 || y > y1) return false;
            rec.u = (x - x0) / (x1 - x0);
            rec.v = (y - y0) / (y1 - y0);
            rec.t = t;
            rec.mat_ptr = mat;
            rec.p = r.PointAtParameter(t);
            rec.normal = new Vec3(0, 0, 1);
            return true;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = new aabb(new Vec3(x0, y0, k - 0.0001f), new Vec3(x1, y1, k + 0.0001f));
            return true;
        }
    }

    class zxRect : Hitable
    {
        private float x0, x1, z0, z1, k;
        private Material mat;
        public zxRect()
        {
        }

        public zxRect(float _x0, float _x1, float _z0, float _z1, float _k, Material m)
        {
            x0 = _x0;
            x1 = _x1;
            z0 = _z0;
            z1 = _z1;
            k = _k;
            mat = m;
        }

        public bool Hit(Ray r, float t0, float t1, ref HitRecord rec)
        {
            float t = (k - r.GetOrigin.y) / r.GetDirection.y;
            if (t < t0 || t > t1) return false;
            float x = r.GetOrigin.x + t * r.GetDirection.x;
            float z = r.GetOrigin.z + t * r.GetDirection.z;
            if (x < x0 || x > x1 || z < z0 || z > z1) return false;
            rec.u = (x - x0) / (x1 - x0);
            rec.v = (z - z0) / (z1 - z0);
            rec.t = t;
            rec.mat_ptr = mat;
            rec.p = r.PointAtParameter(t);
            rec.normal = new Vec3(0, 1, 0);
            return true;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = new aabb(new Vec3(x0, k - 0.0001f, z1), new Vec3(x1, k + 0.0001f, z1));
            return true;
        }
    }

    class yzRect : Hitable
    {
        private float y0, y1, z0, z1, k;
        private Material mat;
        public yzRect()
        {
        }

        public yzRect(float _y0, float _y1, float _z0, float _z1, float _k, Material m)
        {
            y0 = _y0;
            y1 = _y1;
            z0 = _z0;
            z1 = _z1;
            k = _k;
            mat = m;
        }

        public bool Hit(Ray r, float t0, float t1, ref HitRecord rec)
        {
            float t = (k - r.GetOrigin.x) / r.GetDirection.x;
            if (t < t0 || t > t1) return false;
            float y = r.GetOrigin.y + t * r.GetDirection.y;
            float z = r.GetOrigin.z + t * r.GetDirection.z;
            if (y < y0 || y > y1 || z < z0 || z > z1) return false;
            rec.u = (y - y0) / (y1 - y0);
            rec.v = (z - z0) / (z1 - z0);
            rec.t = t;
            rec.mat_ptr = mat;
            rec.p = r.PointAtParameter(t);
            rec.normal = new Vec3(1, 0, 0);
            return true;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = new aabb(new Vec3(k - 0.0001f, y0, z0), new Vec3(k + 0.0001f, y1, z1));
            return true;
        }
    }
}
