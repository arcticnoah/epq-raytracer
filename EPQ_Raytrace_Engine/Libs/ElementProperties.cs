using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace EPQ_Raytrace_Engine.Libs
{
    class UintWrapper
    {
        public uint[] value;

        public UintWrapper()
        {
            value = new uint[0];
        }
    }

    class IntWrapper
    {
        public int value;

        public IntWrapper()
        {
            value = 0;
        }
    }

    class FloatWrapper
    {
        public float value;

        public FloatWrapper()
        {
            value = 0;
        }
    }

    class StringWrapper
    {
        public string value;

        public StringWrapper()
        {
            value = "";
        }
    }

    class BoolWrapper
    {
        public bool value;

        public BoolWrapper()
        {
            value = false;
        }
    }


    class SceneProperties
    {
        public CameraProperties Camera;
        public List<ObjectProperties> Objects = new List<ObjectProperties>();
        public List<MaterialProperties> Materials = new List<MaterialProperties>();
        public List<TextureProperties> Textures = new List<TextureProperties>();
        public BoolWrapper EnableWorldLighting = new BoolWrapper();
        public FloatWrapper MaxLuminance = new FloatWrapper();

        public SceneProperties()
        {
            Camera = new CameraProperties("Main Camera", new Vec3(0, 0, 0), new Vec3(0, 0, 0), new Vec3(0, 1, 0), 90f, 1f, 0f, 0f, 0f, 1f);
            EnableWorldLighting.value = true;
            MaxLuminance.value = 1f;
        }
    }

    class ElementProperties
    {
        public StringWrapper name = new StringWrapper();
    }

    class CameraProperties : ElementProperties
    {
        public Vec3 position, focusPos, vUp;
        public FloatWrapper fov = new FloatWrapper();
        public FloatWrapper aspect = new FloatWrapper();
        public FloatWrapper focusAperture = new FloatWrapper();
        public FloatWrapper focusDistance = new FloatWrapper();
        public FloatWrapper time0 = new FloatWrapper();
        public FloatWrapper time1 = new FloatWrapper();

        public CameraProperties(String n, Vec3 p, Vec3 fp, Vec3 v, float f, float a, float fa, float fd, float t0, float t1)
        {
            name.value = n;
            position = p;
            focusPos = fp;
            vUp = v;
            fov.value = f;
            aspect.value = a;
            focusAperture.value = fa;
            focusDistance.value = fd;
            time0.value = t0;
            time1.value = t1;
        }
    }

    class ObjectProperties : ElementProperties
    {
        public IntWrapper material = new IntWrapper(); // Index of 'Materials', for shared materials
        public BoolWrapper invertNormal = new BoolWrapper();
    }

    class SphereProperties : ObjectProperties
    {
        public Vec3 position;
        public FloatWrapper scale = new FloatWrapper();

        public SphereProperties(string n, Vec3 p, float s, int m, bool i)
        {
            name.value = n;
            position = p;
            scale.value = s;
            material.value = m;
            invertNormal.value = i;
        }
    }

    class PlaneProperties : ObjectProperties
    {
        public IntWrapper direction = new IntWrapper(); // 0: XY, 1: YZ, 2: ZX
        public FloatWrapper position1 = new FloatWrapper();
        public FloatWrapper position2 = new FloatWrapper();
        public FloatWrapper position3 = new FloatWrapper();
        public FloatWrapper position4 = new FloatWrapper();
        public FloatWrapper constant = new FloatWrapper();

        public PlaneProperties(string n, int d, float x1, float x2, float y1, float y2, float k, int m, bool i)
        {
            name.value = n;
            direction.value = d;
            position1.value = x1;
            position2.value = x2;
            position3.value = y1;
            position4.value = y2;
            constant.value = k;
            material.value = m;
            invertNormal.value = i;
        }
    }

    class BoxProperties : ObjectProperties
    {
        public Vec3 point1, point2;

        public BoxProperties(string n, Vec3 p1, Vec3 p2, int m, bool i)
        {
            name.value = n;
            point1 = p1;
            point2 = p2;
            material.value = m;
            invertNormal.value = i;
        }
    }

    class MaterialProperties : ElementProperties
    {

    }

    class LambertianProperties : MaterialProperties
    {
        public IntWrapper texture = new IntWrapper(); // Index of 'Textures', for shared textures

        public LambertianProperties(string n, int t)
        {
            name.value = n;
            texture.value = t;
        }
    }

    class MetalProperties : MaterialProperties
    {
        public IntWrapper texture = new IntWrapper(); // Index of 'Textures', for shared textures
        public FloatWrapper roughness = new FloatWrapper();

        public MetalProperties(string n, int t, float r)
        {
            name.value = n;
            texture.value = t;
            roughness.value = r;
        }
    }

    class DielectricProperties : MaterialProperties
    {
        public FloatWrapper refraction = new FloatWrapper();

        public DielectricProperties(string n, float r)
        {
            name.value = n;
            refraction.value = r;
        }
    }

    class DiffuseLightProperties : MaterialProperties
    {
        public IntWrapper texture = new IntWrapper(); // Index of 'Textures', for shared textures
        public FloatWrapper intensity = new FloatWrapper();

        public DiffuseLightProperties(string n, int t, float i)
        {
            name.value = n;
            texture.value = t;
            intensity.value = i;
        }
    }

    class TextureProperties : ElementProperties
    {

    }

    class ConstantTextureProperties : TextureProperties
    {
        public Vec3 color;

        public ConstantTextureProperties(string n, Vec3 c)
        {
            name.value = n;
            color = c;
        }
    }

    class CheckerTextureProperties : TextureProperties
    {
        public IntWrapper color1 = new IntWrapper(); // Index of 'Textures', for shared textures
        public IntWrapper color2 = new IntWrapper(); // Index of 'Textures', for shared textures
        public FloatWrapper scale = new FloatWrapper();

        public CheckerTextureProperties(string n, int c1, int c2, float s)
        {
            name.value = n;
            color1.value = c1;
            color2.value = c2;
            scale.value = s;
        }
    }

    class PerlinNoiseTextureProperties : TextureProperties
    {
        public FloatWrapper scale = new FloatWrapper();

        public PerlinNoiseTextureProperties(string n, float s)
        {
            name.value = n;
            scale.value = s;
        }
    }

    class TurbulentNoiseTextureProperties : TextureProperties
    {
        public FloatWrapper scale = new FloatWrapper();

        public TurbulentNoiseTextureProperties(string n, float s)
        {
            name.value = n;
            scale.value = s;
        }
    }

    class MarbleNoiseTextureProperties : TextureProperties
    {
        public FloatWrapper scale = new FloatWrapper();

        public MarbleNoiseTextureProperties(string n, float s)
        {
            name.value = n;
            scale.value = s;
        }
    }

    class ImageTextureProperties : TextureProperties
    {
        public UintWrapper image = new UintWrapper();
        public IntWrapper nx = new IntWrapper();
        public IntWrapper ny = new IntWrapper();

        public ImageTextureProperties(string n, Bitmap bmp)
        {
            name.value = n;
            image.value = ImageTools.GetImagePixels24(bmp);
            nx.value = bmp.Width;
            ny.value = bmp.Height;
        }
    }
}