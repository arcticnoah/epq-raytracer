using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    abstract class Material
    {
        public abstract bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s);

        public virtual Vec3 Emitted(float u, float v, Vec3 p)
        {
            return new Vec3(0, 0, 0);
        }
    }

    class Lambertian : Material
    {
        public Texture Albedo;

        public Lambertian(Texture a)
        {
            Albedo = a;
        }

        public override bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s)
        {
            Vec3 target = rec.p + rec.normal + Vec3.RandomInUnitSphere();
            s = new Ray(rec.p, target - rec.p, r.GetTime);
            a = Albedo.Value(rec.u, rec.v, rec.p);
            return true;
        }
    }

    class Metal : Material
    {
        public Texture Albedo;
        public float Fuzz;

        public Metal(Texture a, float f)
        {
            Albedo = a;
            if (f < 1 && f >= 0)
            {
                Fuzz = f;
            } else
            {
                Fuzz = 1;
            }
        }

        public override bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s)
        {
            Vec3 reflected = Vec3.Reflect(Vec3.unitVector(r.GetDirection), rec.normal);
            s = new Ray(rec.p, reflected + Vec3.RandomInUnitSphere() * Fuzz);
            a = Albedo.Value(rec.u, rec.v, rec.p);
            return (Vec3.Dot(s.GetDirection, rec.normal) > 0);
        }
    }

    class Dielectric : Material
    {
        public float refIdx;

        private Random rnd = new Random();

        public Dielectric(float r)
        {
            refIdx = r;
        }

        static float schlick(float cosine, float ref_idx)
        {
            float r0 = (1 - ref_idx) / (1 + ref_idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * (float)Math.Pow(1 - cosine, 5);
        }

        public override bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s)
        {
            Vec3 outwardNormal;
            Vec3 reflected = Vec3.Reflect(r.GetDirection, rec.normal);
            float niOverNt;
            a = new Vec3(1, 1, 1);
            //a.Set(1, 1, 1);
            Vec3 refracted = new Vec3();
            float reflectProb;
            float cosine;

            if (Vec3.Dot(r.GetDirection, rec.normal) > 0)
            {
                outwardNormal = rec.normal * -1;
                niOverNt = refIdx;
                cosine = refIdx * Vec3.Dot(r.GetDirection, rec.normal) / r.GetDirection.Length();
            } else
            {
                outwardNormal = rec.normal;
                niOverNt = 1.0f / refIdx;
                cosine = -Vec3.Dot(r.GetDirection, rec.normal) / r.GetDirection.Length();
            }

            if (Vec3.Refract(r.GetDirection, outwardNormal, niOverNt, ref refracted))
            {
                reflectProb = schlick(cosine, refIdx);
            } else
            {
                reflectProb = 1.0f;
            }

            if (rnd.NextDouble() < reflectProb)
            {
                s = new Ray(rec.p, reflected);
            } else
            {
                s = new Ray(rec.p, refracted);
            }
            return true;
        }
    }

    class DiffuseLight : Material
    {
        private Texture emit;
        private float intensity;

        public DiffuseLight(Texture a, float i)
        {
            emit = a;
            intensity = i;
        }

        public override bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s)
        {
            return false;
        }

        public override Vec3 Emitted(float u, float v, Vec3 p)
        {
            return emit.Value(u, v, p) * intensity;
        }
    }

    class Isotropic : Material
    {
        private Texture albedo;

        public Isotropic(Texture a)
        {
            albedo = a;
        }

        public override bool Scatter(Ray r, HitRecord rec, ref Vec3 a, ref Ray s)
        {
            s = new Ray(rec.p, Vec3.RandomInUnitSphere());
            a = albedo.Value(rec.u, rec.v, rec.p);
            return true;
        }
    }
}
