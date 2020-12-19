using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class HitableList : Hitable
    {
        public List<Hitable> list;
        public int listSize;

        public HitableList()
        {
        }

        public HitableList(List<Hitable> l)
        {
            list = l;
            listSize = l.Count;
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            HitRecord temp_rec = new HitRecord();
            bool hit_anything = false;
            double closest_so_far = tMax;
            for (int i = 0; i < listSize; i++)
            {
                if (list[i].Hit(r, tMin, (float)closest_so_far, ref temp_rec))
                {
                    hit_anything = true;
                    closest_so_far = temp_rec.t;
                    rec.Set(temp_rec);
                }
            }
            return hit_anything;
        }

        public bool BoundingBox(float t0, float t1, ref aabb box)
        {
            if (listSize < 1)
            {
                return false;
            }

            aabb tempBox = new aabb();
            bool firstTrue = list[0].BoundingBox(t0, t1, ref tempBox);

            if (!firstTrue)
            {
                return false;
            }

            box = tempBox;

            for (int i = 1; i < listSize; i++)
            {
                if (list[i].BoundingBox(t0, t1, ref tempBox)) {
                    box = aabb.SurroundingBox(box, tempBox);
                } else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
