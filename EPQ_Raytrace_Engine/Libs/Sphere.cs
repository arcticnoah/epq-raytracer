using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Sphere : Hitable
    {
        private Vec3 Center;
        private float Radius;
        private Material Material;

        public Sphere()
        {
        }

        public Sphere(Vec3 cen, float r, Material m)
        {
            Center = cen;
            Radius = r;
            Material = m;
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            Vec3 oc = r.GetOrigin - Center;
            float a = Vec3.Dot(r.GetDirection, r.GetDirection);
            float b = Vec3.Dot(oc, r.GetDirection) * 2;
            float c = Vec3.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - 4 * a * c;
            if (discriminant > 0)
            {
                discriminant = (float)Math.Sqrt(discriminant);
                float temp = (-b - discriminant) / (2 * a);
                float temp2 = (-b + discriminant) / (2 * a);
                bool isT1 = temp < tMax && temp > tMin;
                bool isT2 = temp2 < tMax && temp2 > tMin;
                if (isT1 || isT2)
                {
                    rec.t = isT1 ? temp : temp2;
                    rec.p = r.PointAtParameter(rec.t);
                    rec.normal = (rec.p - Center) / Radius;
                    rec.mat_ptr = Material;
                    GetUV(rec.normal, ref rec);
                    return true;
                }
            }
            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = new aabb(Center - new Vec3(Radius, Radius, Radius), Center + new Vec3(Radius, Radius, Radius));
            return true;
        }

        public void GetUV(Vec3 p, ref HitRecord rec)
        {
            float phi = (float)Math.Atan2(p.z, p.x);
            float theta = (float)Math.Asin(p.y);
            rec.u = 1 - (phi + (float)Math.PI) / (2 * (float)Math.PI);
            rec.v = (theta + ((float)Math.PI / 2)) / (float)Math.PI;
        }
    }

    class MovingSphere : Hitable
    {
        private Vec3 Center0, Center1;
        private float Time0, Time1;
        private float Radius;
        private Material Material;

        public MovingSphere()
        {
        }

        public MovingSphere(Vec3 cen0, Vec3 cen1, float t0, float t1, float r, Material m)
        {
            Center0 = cen0;
            Center1 = cen1;
            Time0 = t0;
            Time1 = t1;
            Radius = r;
            Material = m;
        }

        public Vec3 Center(float time)
        {
            return Center0 + (Center1 - Center0) * ((time - Time0) / (Time1 - Time0));
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            Vec3 oc = r.GetOrigin - Center(r.GetTime);
            float a = Vec3.Dot(r.GetDirection, r.GetDirection);
            float b = Vec3.Dot(oc, r.GetDirection);
            float c = Vec3.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - a * c;
            if (discriminant > 0)
            {
                float temp = (-b - (float)Math.Sqrt(b * b - a * c)) / a;
                if (temp < tMax && temp > tMin)
                {
                    rec.t = temp;
                    rec.p = r.PointAtParameter(rec.t);
                    rec.normal = (rec.p - Center(r.GetTime) / Radius);
                    rec.mat_ptr = Material;
                    GetUV((rec.p - Center(r.GetTime)) / Radius, ref rec);
                    return true;
                }
                temp = (-b + (float)Math.Sqrt(b * b - a * c)) / a;
                if (temp < tMax && temp > tMin)
                {
                    rec.t = temp;
                    rec.p = r.PointAtParameter(rec.t);
                    rec.normal = (rec.p - Center(r.GetTime)) / Radius;
                    rec.mat_ptr = Material;
                    GetUV((rec.p - Center(r.GetTime)) / Radius, ref rec);
                    return true;
                }
            }
            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            aabb box0 = new aabb(Center0 - new Vec3(Radius, Radius, Radius), Center0 + new Vec3(Radius, Radius, Radius));
            aabb box1 = new aabb(Center1 - new Vec3(Radius, Radius, Radius), Center1 + new Vec3(Radius, Radius, Radius));
            box = aabb.SurroundingBox(box0, box1);
            return true;
        }

        public void GetUV(Vec3 p, ref HitRecord rec)
        {
            float phi = (float)Math.Atan2(p.z, p.x);
            float theta = (float)Math.Sin(p.y);
            rec.u = 1 - (phi + (float)Math.PI) / (2 * (float)Math.PI);
            rec.v = (theta + ((float)Math.PI / 2)) / (float)Math.PI;
        }
    }
}
