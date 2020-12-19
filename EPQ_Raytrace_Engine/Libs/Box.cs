using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Box : Hitable
    {
        public Vec3 pMin, pMax;
        public HitableList ptrList;

        public Box()
        {
        }

        public Box(Vec3 p0, Vec3 p1, Material ptr)
        {
            pMin = p0;
            pMax = p1;

            List<Hitable> list = new List<Hitable>();

            list.Add(new xyRect(p0.x, p1.x, p0.y, p1.y, p1.z, ptr));
            list.Add(new FlipNormals(new xyRect(p0.x, p1.x, p0.y, p1.y, p0.z, ptr)));
            list.Add(new zxRect(p0.x, p1.x, p0.z, p1.z, p1.y, ptr));
            list.Add(new FlipNormals(new zxRect(p0.x, p1.x, p0.z, p1.z, p0.y, ptr)));
            list.Add(new yzRect(p0.y, p1.y, p0.z, p1.z, p1.x, ptr));
            list.Add(new FlipNormals(new yzRect(p0.y, p1.y, p0.z, p1.z, p0.x, ptr)));

            ptrList = new HitableList(list);
        }

        public bool Hit(Ray r, float t0, float t1, ref HitRecord rec)
        {
            return ptrList.Hit(r, t0, t1, ref rec);
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            box = new aabb(pMin, pMax);
            return true;
        }
    }
}
