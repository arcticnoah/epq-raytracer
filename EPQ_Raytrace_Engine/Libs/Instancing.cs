using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Translate : Hitable
    {
        private Hitable ptr;
        private Vec3 displacement;

        public Translate(Hitable p, Vec3 d)
        {
            ptr = p;
            displacement = d;
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            Ray movedR = new Ray(r.GetOrigin - displacement, r.GetDirection, r.GetTime);

            if (ptr.Hit(movedR, tMin, tMax, ref rec))
            {
                rec.p += displacement;
                return true;
            }

            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            if (ptr.BoundingBox(t0, t1, ref box))
            {
                box = new aabb(box.GetMin + displacement, box.GetMax + displacement);
                return true;
            }

            return false;
        }
    }

    class RotateY : Hitable
    {
        private Hitable ptr;
        private float sinTheta, cosTheta;
        private aabb bbox;
        private bool hasBox;

        public RotateY(Hitable p, float angle)
        {
            ptr = p;
            float radians = (float)(Math.PI / 180) * angle;
            sinTheta = (float)Math.Sin(radians);
            cosTheta = (float)Math.Cos(radians);

            hasBox = ptr.BoundingBox(0, 1, ref bbox);
            Vec3 min = new Vec3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vec3 max = new Vec3( - float.MaxValue, - float.MaxValue, - float.MaxValue);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        float x = i * bbox.GetMax.x + (1 - i) * bbox.GetMin.x;
                        float y = i * bbox.GetMax.y + (1 - j) * bbox.GetMin.y;
                        float z = i * bbox.GetMax.z + (1 - k) * bbox.GetMin.z;

                        float newX = cosTheta * x + sinTheta * z;
                        float newZ = -sinTheta * x + cosTheta * z;

                        Vec3 tester = new Vec3(newX, y, newZ);

                        for (int c = 0; c < 3; c++)
                        {
                            if (tester[c] > max[c])
                            {
                                max[c] = tester[c];
                            }

                            if (tester[c] < min[c])
                            {
                                min[c] = tester[c];
                            }
                        }
                    }
                }
            }

            bbox = new aabb(min, max);
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            Vec3 origin = r.GetOrigin;
            Vec3 direction = r.GetDirection;

            origin[0] = cosTheta * r.GetOrigin[0] - sinTheta * r.GetOrigin[2];
            origin[2] = sinTheta * r.GetOrigin[0] + cosTheta * r.GetOrigin[2];

            direction[0] = cosTheta * r.GetDirection[0] - sinTheta * r.GetDirection[2];
            direction[2] = sinTheta * r.GetDirection[0] + cosTheta * r.GetDirection[2];

            Ray rotatedR = new Ray(origin, direction, r.GetTime);

            if (ptr.Hit(rotatedR, tMin, tMax, ref rec))
            {
                Vec3 p = rec.p;
                Vec3 normal = rec.normal;

                p[0] = cosTheta * p[0] + sinTheta * p[2];
                p[2] = -sinTheta * p[0] + cosTheta * p[2];

                normal[0] = cosTheta * normal[0] + sinTheta * normal[2];
                normal[2] = -sinTheta * normal[0] + cosTheta * normal[2];

                rec.p = p;
                rec.normal = normal;
                return true;
            }

            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = bbox;
            return hasBox;
        }
    }
}
