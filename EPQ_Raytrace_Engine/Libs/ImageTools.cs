using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class ImageTools
    {
        public static uint[] GetImagePixels24(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;
            uint[] pixels = new uint[w * h];
            for (int i = 0, y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    pixels[i++] = (uint)img.GetPixel(x, y).ToArgb();
                }
            }
            return pixels;
        }
    }
}
