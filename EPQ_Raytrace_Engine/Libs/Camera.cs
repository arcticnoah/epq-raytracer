using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Camera
    {
        private Vec3 lower_left_corner;
        private Vec3 horizontal;
        private Vec3 vertical;
        private Vec3 origin;
        private float lensRadius;
        private float time0, time1;
        private Vec3 u, v, w;
        private Random rnd = new Random();

        public Camera(Vec3 lookFrom, Vec3 lookAt, Vec3 vUp, float vFov, float aspect, float aperture, float focusDist, float t0, float t1)
        {
            lensRadius = aperture / 2;
            time0 = t0;
            time1 = t1;
            float theta = vFov * (float)Math.PI / 180;
            float halfHeight = (float)Math.Tan(theta / 2);
            float halfWidth = aspect * halfHeight;
            origin = lookFrom;
            w = Vec3.unitVector(lookFrom - lookAt);
            u = Vec3.unitVector(Vec3.Cross(vUp, w));
            v = Vec3.Cross(w, u);
            lower_left_corner = origin - u * halfWidth * focusDist - v * halfHeight * focusDist - w * focusDist;
            horizontal = u * 2 * focusDist * halfWidth;
            vertical = v * 2 * focusDist * halfHeight;
        }

        public Ray GetRay(float s, float t)
        {
            Vec3 rd = Vec3.RandomInUnitDisk() * lensRadius;
            Vec3 offset = u * rd.x + v * rd.y;
            float time = time0 + (float)rnd.NextDouble() * (time1 - time0);
            return new Ray(origin + offset, lower_left_corner + horizontal * s + vertical * t - origin - offset, time);
        }
    }
}
