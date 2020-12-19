using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    abstract class Texture
    {
        abstract public Vec3 Value(float u, float v, Vec3 p);
    }

    class ConstantTexture : Texture
    {
        private Vec3 color;

        public ConstantTexture()
        {
        }

        public ConstantTexture(Vec3 c)
        {
            color = c;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            return color;
        }
    }

    class CheckerTexture : Texture
    {
        private Texture Odd;
        private Texture Even;
        private float scale;

        internal CheckerTexture(Texture p_texture1, Texture p_texture2, float s)
        {
            Odd = p_texture1;
            Even = p_texture2;
            scale = s;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            var sines = Math.Sin(scale * p.x) * Math.Sin(scale * p.y) * Math.Sin(scale * p.z);

            if (sines < 0)
            {
                return Odd.Value(u, v, p);
            }

            return Even.Value(u, v, p);
        }
    }

    class PerlinNoiseTexture : Texture
    {
        private Random rnd = new Random();
        private float scale;
        private Perlin Noise = new Perlin((int)new Random().NextDouble() * 1000);

        public PerlinNoiseTexture()
        {
        }

        public PerlinNoiseTexture(float s)
        {
            scale = s;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            return new Vec3(0.5f, 0.5f, 0.5f) * Noise.GetSmoothNoise(p * scale) + 0.5f;
        }
    }

    class TurbulentNoiseTexture : Texture
    {
        private Random rnd = new Random();
        private float scale;
        private Perlin Noise = new Perlin((int)new Random().NextDouble() * 1000);

        public TurbulentNoiseTexture()
        {
        }

        public TurbulentNoiseTexture(float s)
        {
            scale = s;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            return new Vec3(1, 1, 1) * Noise.GetTurbulentNoise(p * scale);
        }
    }

    class MarbleNoiseTexture : Texture
    {
        private Random rnd = new Random();
        private float scale;
        private Perlin Noise = new Perlin((int)new Random().NextDouble() * 1000);

        public MarbleNoiseTexture()
        {
        }

        public MarbleNoiseTexture(float s)
        {
            scale = s;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            return new Vec3(1, 1, 1) * 0.5f * (1 + (float)Math.Sin((p.z * scale) + 10 * Noise.GetTurbulentNoise(p * scale)));
        }
    }

    class ImageTexture : Texture
    {
        private uint[] data;
        private int nx, ny;
        private float scale = 1;

        public ImageTexture(uint[] pixels, int a, int b)
        {
            data = pixels;
            nx = a;
            ny = b;
        }

        public override Vec3 Value(float u, float v, Vec3 p)
        {
            int i = (int)(u * nx);
            int j = (int)((1 - v) * ny - 0.001f);
            if (i < 0) i = 0;
            if (j < 0) j = 0;
            if (i > nx - 1) i = nx - 1;
            if (j > ny - 1) j = ny - 1;
            uint argb = data[i + j * nx];
            return new Vec3(
                ((argb >> 16) & 255) / 255f,
                ((argb >> 8) & 255) / 255f,
                (argb & 255) / 255f
            );
        }
    }
}
