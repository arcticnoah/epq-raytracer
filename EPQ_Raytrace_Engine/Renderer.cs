using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using EPQ_Raytrace_Engine.Libs;

namespace EPQ_Raytrace_Engine
{
    class Renderer
    {
        private int outputX, outputY;

        private Vec3 GetColor(Ray r, Hitable world, int d, bool worldLighting)
        {
            HitRecord rec = new HitRecord();

            if (world.Hit(r, 0.001f, float.MaxValue, ref rec))
            {
                Ray s = new Ray();
                Vec3 attenuation = new Vec3();
                Vec3 emitted = rec.mat_ptr.Emitted(rec.u, rec.v, rec.p);
                if (d > 0 && rec.mat_ptr.Scatter(r, rec, ref attenuation, ref s))
                {
                    return emitted + attenuation * GetColor(s, world, d - 1, worldLighting);
                }
                else
                {
                    return emitted;
                }
            }

            if (worldLighting == true)
            {
                // Lines below add fake atmosphere world lighting
                Vec3 normalizedDir = r.GetDirection.Normalize();
                float t = 0.5f * normalizedDir.y + 1.0f;
                Vec3 colorStart = new Vec3(1, 1, 1);
                Vec3 colorEnd = new Vec3(0.5f, 0.7f, 1);
                return colorStart * (1 - t) + colorEnd * t;
            }


            return new Vec3();
        }

        private float ReinhardFloat(float x, float max)
        {
            // x * (1 + x/(m ** 2)) /  (1 + x)
            return x * (1 + x / (float)Math.Pow(max,2)) / (1 + x);
        }

        private Vec3 ReinhardVec(Vec3 col, float max)
        {
            col.x = ReinhardFloat(col.x, max);
            col.y = ReinhardFloat(col.y, max);
            col.z = ReinhardFloat(col.z, max);
            return col;
        }

        private Vec3 RGBtoxxYtoRGB(Vec3 input, ref float maxLuminance)
        {
            float L = 0.2126f * input.r + 0.7152f * input.g + 0.0722f * input.b;
            float scale = 1;
            if (L != 0)
            {
                float nL = ToneMap(L, ref maxLuminance);
                scale = nL / L;
            }
            input.r *= scale;
            input.g *= scale;
            input.b *= scale;
            return input;
        }

        private float ToneMap(float input, ref float maxLuminance)
        {
            float output1, output2;
            output1 = ReinhardFloat(input, maxLuminance);
            output2 = (float)Math.Pow(output1, 1.0f / 2.2f);
            return output2;
        }

        private void ToneMap1DFloatArrayToBitmap(int nx, int ns, int currentY, Vec3[,] input, ref Bitmap output, ref float maxLuminance)
        {
            for (int i = 0; i < nx; i++)
            {
                Vec3 col = input[i, currentY];
                col /= ns;
                //col *= 3f;
                col = RGBtoxxYtoRGB(col, ref maxLuminance);
                col.Max(0);
                col.Min(1);
                col *= 255.99f;
                output.SetPixel(i, currentY, Color.FromArgb((int)col.x, (int)col.y, (int)col.z));
            }
        }

        private void ToneMap2DFloatArrayToBitmap(int nx, int ny, int ns, int currentY, Vec3[,] input, ref Bitmap output, ref float maxLuminance)
        {
            if (currentY == -1)
            {
                for (int j = 0; j < ny; j++)
                {
                    // Tonemap every row
                    ToneMap1DFloatArrayToBitmap(nx, ns, j, input, ref output, ref maxLuminance);
                }
            } else
            {
                // Tonemap single row
                ToneMap1DFloatArrayToBitmap(nx, ns, currentY, input, ref output, ref maxLuminance);
            }
        }

        private void ProgressiveResolutionPreview(int nx, int ny, Vec3[,] img, ref Bitmap bmp, Camera cam, Hitable world, ref int samplesDone, ref bool bmpInUse, ref float maxLuminance, ref bool worldLighting)
        {
            float u, v, scale;
            Ray r;
            Vec3 color = new Vec3(0, 0, 0);
            Random rnd = new Random();
            for (int t = 4; t >= 1; t--)
            {
                scale = 1 / (float)Math.Pow(2, t);
                int nnx = (int)(nx * scale);
                int nny = (int)(ny * scale);
                int nns = 0;
                Bitmap unscaled = new Bitmap(nnx, nny);
                for (int p = 1; p <= 8; p++)
                {
                    for (int j = 0; j < nny; j++)
                    {
                        for (int i = 0; i < nnx; i++)
                        {
                            color.Zero();
                            u = (float)(i + rnd.NextDouble()) / nnx;
                            v = 1.0f - (float)(j + rnd.NextDouble()) / nny;

                            r = cam.GetRay(u, v);

                            color = GetColor(r, world, 10, worldLighting);

                            img[i, j] += color;

                            samplesDone += 1;
                        }
                    }
                    nns += 1;
                }
                bmpInUse = true;
                ToneMap2DFloatArrayToBitmap(nnx, nny, nns, -1, img, ref unscaled, ref maxLuminance);
                Bitmap corrected = new Bitmap(unscaled, new Size(outputX, outputY));
                bmp = corrected;
                bmpInUse = false;
                for (int k = 0; k < nx; k++)
                {
                    for (int l = 0; l < ny; l++)
                    {
                        img[k, l] = new Vec3(0, 0, 0);
                    }
                }
            }
            samplesDone = 0;
        }

        public void Start(int nx, int ny, int ns, ref Bitmap bmp, Camera cam, Hitable world, ref int samplesDone, ref int progress, ref bool bmpInUse, ref float maxLuminance, ref bool rendering, ref bool worldLighting)
        {
            rendering = true;

            float u, v;
            outputX = nx;
            outputY = ny;
            Ray r;
            Vec3 color = new Vec3(0, 0, 0);

            Vec3[,] img = new Vec3[nx, ny];
            for (int k = 0; k < nx; k++)
            {
                for (int l = 0; l < ny; l++)
                {
                    img[k, l] = new Vec3(0, 0, 0);
                }
            }

            Random rnd = new Random();

            // If -1, its a preview render (cap samples at 64)
            // If -2, its a progressive render (no cap of samples)
            // Else, its a normal render with a sample cap (per pixel)
            if (ns == -1)
            {
                // Preview render
                ProgressiveResolutionPreview(nx, ny, img, ref bmp, cam, world, ref samplesDone, ref bmpInUse, ref maxLuminance, ref worldLighting);
                Bitmap scaled = bmp;
                for (int p = 1; p <= 32; p++)
                {
                    for (int j = 0 ; j < ny; j++)
                    {
                        if (!rendering)
                        {
                            break;
                        }
                        for (int i = 0; i < nx; i++)
                        {
                            if (!rendering)
                            {
                                break;
                            }
                            color.Zero();
                            u = (float)(i + rnd.NextDouble()) / nx;
                            v = 1.0f - (float)(j + rnd.NextDouble()) / ny;

                            r = cam.GetRay(u, v);

                            color = GetColor(r, world, 10, worldLighting);

                            img[i, j] += color;

                            samplesDone += 1;
                            progress = (int)Math.Floor(((samplesDone / (float)(nx * ny * 32)) * 100));
                        }
                    }
                    while (true)
                    {
                        if (!bmpInUse)
                        {
                            bmpInUse = true;
                            break;
                        }
                    }
                    ToneMap2DFloatArrayToBitmap(nx, ny, p, -1, img, ref scaled, ref maxLuminance);
                    bmp = scaled;
                    bmpInUse = false;
                    if (!rendering)
                    {
                        break;
                    }
                    Thread.Sleep(25);
                }
            }
            else if (ns == -2)
            {
                // Progressive render
                ProgressiveResolutionPreview(nx, ny, img, ref bmp, cam, world, ref samplesDone, ref bmpInUse, ref maxLuminance, ref worldLighting);
                int p = 1;
                progress = -1;
                Bitmap scaled = bmp;
                while (true)
                {
                    for (int j = 0; j < ny; j++)
                    {
                        if (!rendering)
                        {
                            break;
                        }
                        for (int i = 0; i < nx; i++)
                        {
                            if (!rendering)
                            {
                                break;
                            }
                            color.Zero();
                            u = (float)(i + rnd.NextDouble()) / nx;
                            v = 1.0f - (float)(j + rnd.NextDouble()) / ny;

                            r = cam.GetRay(u, v);

                            color = GetColor(r, world, 10, worldLighting);

                            img[i, j] += color;

                            samplesDone += 1;
                        }
                    }
                    while (true)
                    {
                        if (!bmpInUse)
                        {
                            bmpInUse = true;
                            break;
                        }
                    }
                    ToneMap2DFloatArrayToBitmap(nx, ny, p, -1, img, ref scaled, ref maxLuminance);
                    bmp = scaled;
                    bmpInUse = false;
                    if (!rendering)
                    {
                        break;
                    }
                    Thread.Sleep(25);
                    p += 1;
                }
            }
            else
            {
                // Standard capped render
                ProgressiveResolutionPreview(nx, ny, img, ref bmp, cam, world, ref samplesDone, ref bmpInUse, ref maxLuminance, ref worldLighting);
                Bitmap scaled = bmp;
                for (int j = 0; j < ny; j++)
                {
                    for (int i = 0; i < nx; i++)
                    {
                        if (!rendering)
                        {
                            break;
                        }
                        color.Zero();
                        for (int s = 0; s < ns; s++)
                        {
                            if (!rendering)
                            {
                                break;
                            }
                            u = (float)(i + rnd.NextDouble()) / nx;
                            v = 1.0f - (float)(j + rnd.NextDouble()) / ny;

                            r = cam.GetRay(u, v);

                            color += GetColor(r, world, 10, worldLighting);

                            samplesDone += 1;
                            progress = (int)Math.Floor(((samplesDone / (float)(nx * ny * ns)) * 100));
                        }

                        img[i, j].Set(color.x, color.y, color.z);
                    }
                    while (true)
                    {
                        if (!bmpInUse)
                        {
                            bmpInUse = true;
                            break;
                        }
                    }
                    ToneMap2DFloatArrayToBitmap(nx, ny, ns, j, img, ref scaled, ref maxLuminance);
                    bmp = scaled;
                    bmpInUse = false;
                    if (!rendering)
                    {
                        break;
                    }
                    Thread.Sleep(25);
                }
            }
        }
    }
}
