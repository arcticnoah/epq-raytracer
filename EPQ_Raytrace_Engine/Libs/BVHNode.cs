using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class BVHNode : Hitable
    {
        private Hitable left, right;
        private aabb box;
        private Random rnd = new Random();

        public BVHNode()
        {
        }

        public int boxXCompare(Hitable a, Hitable b)
        {
            aabb boxLeft = new aabb();
            aabb boxRight = new aabb();

            if (!a.BoundingBox(0, 0, ref boxLeft) || !b.BoundingBox(0, 0, ref boxRight))
            {
                Console.WriteLine("No bounding box in BVH node constructor");
            }

            if (boxLeft.GetMin.x - boxRight.GetMin.x < 0)
            {
                return -1;
            }
            return 1;
        }

        public int boxYCompare(Hitable a, Hitable b)
        {
            aabb boxLeft = new aabb();
            aabb boxRight = new aabb();

            if (!a.BoundingBox(0, 0, ref boxLeft) || !b.BoundingBox(0, 0, ref boxRight))
            {
                Console.WriteLine("No bounding box in BVH node constructor");
            }

            if (boxLeft.GetMin.y - boxRight.GetMin.y < 0)
            {
                return -1;
            }
            return 1;
        }

        public int boxZCompare(Hitable a, Hitable b)
        {
            aabb boxLeft = new aabb();
            aabb boxRight = new aabb();

            if (!a.BoundingBox(0, 0, ref boxLeft) || !b.BoundingBox(0, 0, ref boxRight))
            {
                Console.WriteLine("No bounding box in BVH node constructor");
            }

            if (boxLeft.GetMin.z - boxRight.GetMin.z < 0)
            {
                return -1;
            }
            return 1;
        }

        public BVHNode(Hitable[] h, int n, float time0, float time1)
        {
            int Compare(Hitable a, Hitable b, int i)
            {
                aabb l = new aabb();
                aabb r = new aabb();
                if (!a.BoundingBox(0, 0, ref l) || !b.BoundingBox(0, 0, ref r)) throw new Exception("NULL");
                return l.GetMin[i] - l.GetMin[i] < 0 ? -1 : 1;
            }

            Hitable[] SplitArray(Hitable[] Source, int StartIndex, int EndIndex)
            {
                Hitable[] result = new Hitable[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }

            var pl = h.ToList();
            var method = (int)(3 * rnd.NextDouble());
            pl.Sort((a, b) => Compare(a, b, method));
            h = pl.ToArray();
            //Console.ReadLine();
            switch (n)
            {
                case 1:
                    left = right = h[0];
                    break;
                case 2:
                    left = h[0];
                    right = h[1];
                    break;
                default:
                    left = new BVHNode(SplitArray(h, 0, n / 2 - 1), n / 2, time0, time1);
                    right = new BVHNode(SplitArray(h, n / 2, n - 1), n - n / 2, time0, time1);
                    break;
            }

            aabb boxLeft = new aabb();
            aabb boxRight = new aabb();

            if (!left.BoundingBox(time0, time1, ref boxLeft) || !right.BoundingBox(time0, time1, ref boxRight))
            {
                Console.WriteLine("No bounding box in BVH node constructor");
            }

            box = aabb.SurroundingBox(boxLeft, boxRight);
        }

        public bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
        {
            if (box.Hit(r, ref tMin, ref tMax))
            {
                HitRecord leftRec = new HitRecord();
                HitRecord rightRec = new HitRecord();
                bool hitLeft = left.Hit(r, tMin, tMax, ref leftRec);
                bool hitRight = right.Hit(r, tMin, tMax, ref rightRec);

                if (hitLeft && hitRight)
                {
                    if (leftRec.t < rightRec.t)
                    {
                        rec = leftRec;
                    } else
                    {
                        rec = rightRec;
                    }
                    return true;
                } else if (hitLeft)
                {
                    rec = leftRec;
                    return true;
                } else if (hitRight)
                {
                    rec = rightRec;
                    return true;
                } else
                return false;
            }
            return false;
        }

        public bool BoundingBox(float t0, float t1, ref aabb b)
        {
            b = box;
            return true;
        }
    }
}
