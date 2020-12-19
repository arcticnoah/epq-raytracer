using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class Perlin
    {
        private Vec3[] ranVec;
        private float[] ranFloat;
        private int[] px, py, pz;

        internal Perlin(int p)
        {
            ranVec = GenerateVec3Perlin(p);
            ranFloat = GenerateFloatPerlin(p);
            px = GeneratePerlinPermutations(p);
            py = GeneratePerlinPermutations(p + 1);
            pz = GeneratePerlinPermutations(p + 2);
        }

        public float GetNoise(Vec3 p)
        {
            int i = (int)(4 * p.x) & 255;
            int j = (int)(4 * p.y) & 255;
            int k = (int)(4 * p.z) & 255;

            return ranFloat[px[i] ^ py[j] ^ pz[k]];
        }

        public float GetSmoothNoise(Vec3 p)
        {
            float u = p.x - (float)Math.Floor(p.x);
            float v = p.y - (float)Math.Floor(p.y);
            float w = p.z - (float)Math.Floor(p.z);

            int i = (int)Math.Floor(p.x);
            int j = (int)Math.Floor(p.y);
            int k = (int)Math.Floor(p.z);

            Vec3[,,] c = new Vec3[2, 2, 2];

            for (int di = 0; di < 2; di++)
            {
                for (int dj = 0; dj < 2; dj++)
                {
                    for (int dk = 0; dk < 2; dk++)
                    {
                        c[di, dj, dk] = ranVec[px[(i + di) & 255] ^ py[(j + dj) & 255] ^ pz[(k + dk) & 255]];
                    }
                }
            }

            return PerlinInterpolate(c, u, v, w);
        }

        private static Vec3[] GenerateVec3Perlin(int p_seed)
        {
            var rng = new Random(p_seed);

            var p = new Vec3[256];
            for (var i = 0; i < 256; ++i)
            {
                p[i] = Vec3.unitVector(new Vec3(-1 + 2 * (float)rng.NextDouble(), -1 + 2 * (float)rng.NextDouble(), -1 + 2 * (float)rng.NextDouble()));
                // p[i] = Vec3.unitVector(new Vec3((float)rng.NextDouble(), (float)rng.NextDouble(), (float)rng.NextDouble()));
            }

            return p;
        }

        private static float[] GenerateFloatPerlin(int p_seed)
        {
            var rng = new Random(p_seed);

            float[] p = new float[256];
            for (int i = 0; i < 256; ++i)
            {
                p[i] = (float)rng.NextDouble();
            }

            return p;
        }

        private static void Permutate(ref int[] p_p, int p_n, int p_seed)
        {
            var rng = new Random(p_seed);

            for (var i = p_n - 1; i > 0; --i)
            {
                var target = (int)(rng.NextDouble() * (i + 1));
                var temp = p_p[i];
                p_p[i] = p_p[target];
                p_p[target] = temp;
            }
        }

        private static int[] GeneratePerlinPermutations(int p_seed)
        {
            var p = new int[256];
            for (var i = 0; i < 256; ++i)
            {
                p[i] = i;
            }
            Permutate(ref p, 256, p_seed);
            return p;
        }

        private static float TrilinearInterpolate(float[,,] p_c, float p_u, float p_v, float p_w)
        {
            float accumulator = 0;
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        accumulator += (i * p_u + (1 - i) * (1 - p_u)) *
                                       (j * p_v + (1 - j) * (1 - p_v)) *
                                       (k * p_w + (1 - k) * (1 - p_w)) * p_c[i, j, k];
                    }
                }
            }

            return accumulator;
        }

        private static float PerlinInterpolate(Vec3[,,] c, float u, float v, float w)
        {
            var uu = u * u * (3 - 2 * u);
            var vv = v * v * (3 - 2 * v);
            var ww = w * w * (3 - 2 * w);

            float accumulator = 0;

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        Vec3 vWeight = new Vec3(u - i, v - j, w - k);
                        accumulator += (i * uu + (1 - i) * (1 - uu)) *
                                       (j * vv + (1 - j) * (1 - vv)) *
                                       (k * ww + (1 - k) * (1 - ww)) * Vec3.Dot(c[i, j, k], vWeight);
                    }
                }
            }

            return accumulator;
        }

        internal float GetTurbulentNoise(Vec3 p_point, int p_depth = 7)
        {
            float accumulator = 0;
            Vec3 tempPoint = p_point;
            float weight = 1;
            for (var i = 0; i < p_depth; ++i)
            {
                accumulator += weight * GetSmoothNoise(tempPoint);
                weight *= 0.5f;
                tempPoint *= 2;
            }

            return Math.Abs(accumulator);
        }
    }
}
