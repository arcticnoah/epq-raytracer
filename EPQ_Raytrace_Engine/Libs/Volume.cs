using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class ConstantMedium : Hitable
    {
        private Hitable boundary;
        private float density;
        private Material phaseFunction;
        private Random rnd = new Random();

        public ConstantMedium(Hitable b, float d, Texture a)
        {
            boundary = b;
            density = d;
            phaseFunction = new Isotropic(a);
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            HitRecord rec1 = new HitRecord();
            HitRecord rec2 = new HitRecord();

            if (boundary.Hit(r, -float.MaxValue, float.MaxValue, ref rec1))
            {
                if (boundary.Hit(r, rec1.t + 0.0001f, float.MaxValue, ref rec2))
                {
                    if (rec1.t < tMin) rec1.t = tMin;

                    if (rec2.t > tMax) rec2.t = tMax;

                    if (rec1.t >= rec2.t) return false;

                    if (rec1.t < 0) rec1.t = 0;

                    float distanceInsideBoundary = (rec2.t - rec1.t) * r.GetDirection.Length();
                    float hitDistance = -(1 / density) * (float)Math.Log(rnd.NextDouble());

                    if (hitDistance < distanceInsideBoundary)
                    {
                        rec.t = rec1.t + hitDistance / r.GetDirection.Length();
                        rec.p = r.PointAtParameter(rec.t);

                        rec.normal = new Vec3(1, 0, 0);
                        rec.mat_ptr = phaseFunction;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            return true;
        }
    }
}
