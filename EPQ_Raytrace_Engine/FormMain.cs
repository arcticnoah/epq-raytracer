using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPQ_Raytrace_Engine.Libs;
using System.Web.Script.Serialization;

namespace EPQ_Raytrace_Engine
{
    public partial class FormMain : Form
    {
        private bool doRenderPreview, doRenderFinalPreview, RenderPreviewBmpInUse, RenderFinalPreviewBmpInUse, doBook1Scene;
        private Bitmap RenderPreview, tmpRenderPreview, Render, tmpRender, noImageBitmap;
        private int RenderWidth, RenderHeight, RenderSamples;
        private Camera SceneCamera;
        private List<Hitable> SceneList;
        private SceneProperties ScenePropertiesList;
        private Hitable Scene;
        private Renderer RenderTools;
        private int samplesDone, progress = 0;
        private DateTime timeCounter;
        private string currentOpenFile = "";
        private string SceneName;
        private string timeFormat = "yyyyMMd_HH-mm-s";
        private OpenFileDialog openFileDialogMain;
        private SaveFileDialog saveFileDialogMain;

        public FormMain()
        {
            doRenderPreview = false;
            doRenderFinalPreview = false;
            RenderPreviewBmpInUse = false;
            RenderFinalPreviewBmpInUse = false;
            RenderPreview = new Bitmap(397, 397);

            // Setup the default image texture (Pink and black checker board)
            noImageBitmap = new Bitmap(2, 2);
            noImageBitmap.SetPixel(0, 0, Color.FromArgb(0, 0, 0));
            noImageBitmap.SetPixel(0, 1, Color.FromArgb(255, 0, 255));
            noImageBitmap.SetPixel(1, 0, Color.FromArgb(255, 0, 255));
            noImageBitmap.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            SceneList = new List<Hitable>();

            Scene = new HitableList(SceneList);
            RenderTools = new Renderer();

            openFileDialogMain = new OpenFileDialog()
            {
                Filter = "Scene files (*.scene)|*.scene",
                Title = "Open a scene file"
            };

            saveFileDialogMain = new SaveFileDialog()
            {
                Filter = "Scene files (*.scene)|*.scene",
                Title = "Save a scene file"
            };

            InitializeComponent();
        }

        private void createEmptyScene()
        {
            SceneList.Clear();
            RenderWidth = 512;
            RenderHeight = 512;
            RenderSamples = 64;
            SceneName = "Empty Scene";
            Render = new Bitmap(RenderWidth, RenderHeight);

            Invoke(new Action(() =>
            {
                RenderResolutionWidthTextBox.Text = RenderWidth.ToString();
                RenderResolutionHeightTextBox.Text = RenderHeight.ToString();
                RenderSamplesValueTextBox.Text = RenderSamples.ToString();
            }));

            ScenePropertiesList = new SceneProperties();
            SceneTreeView.Nodes.Add(SceneName + " [Scene]");
            SceneTreeView.Nodes[0].Nodes.Add("New Camera [Camera]");
            SceneTreeView.Nodes[0].Nodes.Add("Objects");
            SceneTreeView.Nodes[0].Nodes.Add("Materials");
            SceneTreeView.Nodes[0].Nodes.Add("Textures");

            ScenePropertiesList.EnableWorldLighting.value = true;
            ScenePropertiesList.MaxLuminance.value = 1f;

            doBook1Scene = false;
        }

        private void createCornellBoxScene()
        {
            SceneList.Clear();
            RenderWidth = 512;
            RenderHeight = 512;
            RenderSamples = 128;
            SceneName = "Cornell Box Scene";
            Render = new Bitmap(RenderWidth, RenderHeight);

            Invoke(new Action(() =>
            {
                RenderResolutionWidthTextBox.Text = RenderWidth.ToString();
                RenderResolutionHeightTextBox.Text = RenderHeight.ToString();
                RenderSamplesValueTextBox.Text = RenderSamples.ToString();
            }));

            ScenePropertiesList = new SceneProperties();
            SceneTreeView.Nodes.Add(SceneName + " [Scene]");

            SceneTreeView.Nodes[0].Nodes.Add("Main Camera [Camera]");

            ScenePropertiesList.Camera.position = new Vec3(278, 278, -800);
            ScenePropertiesList.Camera.focusPos = new Vec3(278, 278, 0);
            ScenePropertiesList.Camera.vUp = new Vec3(0, 1, 0);
            ScenePropertiesList.Camera.fov.value = 40;
            ScenePropertiesList.Camera.aspect.value = 1;
            ScenePropertiesList.Camera.focusAperture.value = 0;
            ScenePropertiesList.Camera.focusDistance.value = 10;
            ScenePropertiesList.Camera.time0.value = 0;
            ScenePropertiesList.Camera.time1.value = 1;

            SceneTreeView.Nodes[0].Nodes.Add("Objects");

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Floor Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("Floor Plane", 2, 0, 555, 0, 555, 0, 0, false));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Ceiling Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("Ceiling Plane", 2, 0, 555, 0, 555, 555, 0, true));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("White Wall Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("White Wall Plane", 0, 0, 555, 0, 555, 555, 0, true));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Green Wall Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("Green Wall Plane", 1, 0, 555, 0, 555, 555, 1, true));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Red Wall Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("Red Wall Plane", 1, 0, 555, 0, 555, 0, 2, false));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Ceiling Light Plane [Plane]");
            ScenePropertiesList.Objects.Add(new PlaneProperties("Ceiling Light Plane", 2, 213, 343, 227, 332, 554, 3, false));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Cube Box [Box]");
            ScenePropertiesList.Objects.Add(new BoxProperties("Cube Box", new Vec3(130, 0, 65), new Vec3(295, 165, 230), 0, false));

            SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("Cuboid Box [Box]");
            ScenePropertiesList.Objects.Add(new BoxProperties("Cuboid Box", new Vec3(265, 0, 295), new Vec3(430, 330, 460), 0, false));

            SceneTreeView.Nodes[0].Nodes.Add("Materials");

            SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("White [Lambertian]");
            ScenePropertiesList.Materials.Add(new LambertianProperties("White", 0));

            SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("Green [Lambertian]");
            ScenePropertiesList.Materials.Add(new LambertianProperties("Green", 1));

            SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("Red [Lambertian]");
            ScenePropertiesList.Materials.Add(new LambertianProperties("Red", 2));

            SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("Light [DiffuseLight]");
            ScenePropertiesList.Materials.Add(new DiffuseLightProperties("Light", 3, 15));

            SceneTreeView.Nodes[0].Nodes.Add("Textures");

            SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("White [ConstantTexture]");
            ScenePropertiesList.Textures.Add(new ConstantTextureProperties("White", new Vec3(0.73f, 0.73f, 0.73f)));

            SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("Green [ConstantTexture]");
            ScenePropertiesList.Textures.Add(new ConstantTextureProperties("Green", new Vec3(0.15f, 0.45f, 0.15f)));

            SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("Red [ConstantTexture]");
            ScenePropertiesList.Textures.Add(new ConstantTextureProperties("Red", new Vec3(0.65f, 0.05f, 0.05f)));

            SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("Light [ConstantTexture]");
            ScenePropertiesList.Textures.Add(new ConstantTextureProperties("Light", new Vec3(1, 1, 1)));

            ScenePropertiesList.EnableWorldLighting.value = false;
            ScenePropertiesList.MaxLuminance.value = 15f;

            doBook1Scene = false;
        }

        private void createBook1Scene()
        {
            SceneList.Clear();
            RenderWidth = 512;
            RenderHeight = 512;
            RenderSamples = 64;
            SceneName = "Raytracing In One Weekend Book Cover Scene";
            Render = new Bitmap(RenderWidth, RenderHeight);

            Invoke(new Action(() =>
            {
                RenderResolutionWidthTextBox.Text = RenderWidth.ToString();
                RenderResolutionHeightTextBox.Text = RenderHeight.ToString();
                RenderSamplesValueTextBox.Text = RenderSamples.ToString();
            }));

            ScenePropertiesList = new SceneProperties();
            SceneTreeView.Nodes.Add(SceneName + " [Scene]");
            SceneTreeView.Nodes[0].Nodes.Add("Main Camera [Camera]");
            SceneTreeView.Nodes[0].Nodes.Add("Objects");
            SceneTreeView.Nodes[0].Nodes.Add("Materials");
            SceneTreeView.Nodes[0].Nodes.Add("Textures");

            ScenePropertiesList.Camera.position = new Vec3(13, 2, 3);
            ScenePropertiesList.Camera.focusPos = new Vec3(0, 0, 0);
            ScenePropertiesList.Camera.vUp = new Vec3(0, 1, 0);
            ScenePropertiesList.Camera.fov.value = 20;
            ScenePropertiesList.Camera.aspect.value = 1;
            ScenePropertiesList.Camera.focusAperture.value = 0.1f;
            ScenePropertiesList.Camera.focusDistance.value = 10;
            ScenePropertiesList.Camera.time0.value = 0;
            ScenePropertiesList.Camera.time1.value = 1;

            ScenePropertiesList.EnableWorldLighting.value = true;
            ScenePropertiesList.MaxLuminance.value = 1f;

            doBook1Scene = true;
        }

        private void convertScenePropertiesToScene()
        {
            SceneList.Clear();

            if (doBook1Scene == true)
            {
                SceneList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(new ConstantTexture(new Vec3(0.5f, 0.5f, 0.5f)))));

                Random rnd = new Random();

                for (int a = -11; a < 11; a++)
                {
                    for (int b = -11; b < 11; b++)
                    {
                        float choose_mat = (float)rnd.NextDouble();
                        Vec3 center = new Vec3(a + 0.9f * (float)rnd.NextDouble(), 0.2f, b + 0.9f * (float)rnd.NextDouble());

                        if ((center - new Vec3(4, 0.2f, 0)).Length() > 0.9f)
                        {
                            Material sphere_material;

                            if (choose_mat < 0.8)
                            {
                                // diffuse
                                Vec3 albedo = Vec3.Random() * Vec3.Random();
                                sphere_material = new Lambertian(new ConstantTexture(albedo));
                            }
                            else if (choose_mat < 0.95)
                            {
                                // metal
                                Vec3 albedo = Vec3.Random() * Vec3.Random();

                                albedo.r = Math.Min(Math.Max(albedo.r, 0.5f), 1f);
                                albedo.g = Math.Min(Math.Max(albedo.g, 0.5f), 1f);
                                albedo.b = Math.Min(Math.Max(albedo.b, 0.5f), 1f);

                                float fuzz = Math.Min(Math.Max((float)rnd.NextDouble(), 0f), 0.5f);
                                sphere_material = new Metal(new ConstantTexture(albedo), fuzz);
                            }
                            else
                            {
                                // glass
                                sphere_material = new Dielectric(1.5f);
                            }

                            SceneList.Add(new Sphere(center, 0.2f, sphere_material));
                        }
                    }
                }

                SceneList.Add(new Sphere(new Vec3(0, 1, 0), 1f, new Dielectric(1.5f)));

                SceneList.Add(new Sphere(new Vec3(-4, 1, 0), 1f, new Lambertian(new ConstantTexture(new Vec3(0.4f, 0.2f, 0.1f)))));

                SceneList.Add(new Sphere(new Vec3(4, 1, 0), 1f, new Metal(new ConstantTexture(new Vec3(0.7f, 0.6f, 0.5f)), 0f)));
            }

            List<Texture> SceneTextures = new List<Texture>();
            List<Material> SceneMaterials = new List<Material>();
            CameraProperties SceneCameraProperties = ScenePropertiesList.Camera;
            SceneCamera = new Camera(SceneCameraProperties.position, SceneCameraProperties.focusPos, SceneCameraProperties.vUp, SceneCameraProperties.fov.value, SceneCameraProperties.aspect.value, SceneCameraProperties.focusAperture.value, SceneCameraProperties.focusDistance.value, SceneCameraProperties.time0.value, SceneCameraProperties.time1.value);

            if (ScenePropertiesList.Textures.Count != 0)
            {
                for (int x = 0; x < ScenePropertiesList.Textures.Count; x++)
                {
                    if (ScenePropertiesList.Textures[x].GetType() == typeof(ConstantTextureProperties))
                    {
                        ConstantTextureProperties tmp = (ConstantTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new ConstantTexture(tmp.color));
                    }

                    else if (ScenePropertiesList.Textures[x].GetType() == typeof(CheckerTextureProperties))
                    {
                        CheckerTextureProperties tmp = (CheckerTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new CheckerTexture(SceneTextures[tmp.color1.value], SceneTextures[tmp.color2.value], tmp.scale.value));
                    }

                    else if (ScenePropertiesList.Textures[x].GetType() == typeof(PerlinNoiseTextureProperties))
                    {
                        PerlinNoiseTextureProperties tmp = (PerlinNoiseTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new PerlinNoiseTexture(tmp.scale.value));
                    }

                    else if (ScenePropertiesList.Textures[x].GetType() == typeof(TurbulentNoiseTextureProperties))
                    {
                        TurbulentNoiseTextureProperties tmp = (TurbulentNoiseTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new TurbulentNoiseTexture(tmp.scale.value));
                    }

                    else if (ScenePropertiesList.Textures[x].GetType() == typeof(MarbleNoiseTextureProperties))
                    {
                        MarbleNoiseTextureProperties tmp = (MarbleNoiseTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new MarbleNoiseTexture(tmp.scale.value));
                    }

                    else if (ScenePropertiesList.Textures[x].GetType() == typeof(ImageTextureProperties))
                    {
                        ImageTextureProperties tmp = (ImageTextureProperties)ScenePropertiesList.Textures[x];
                        SceneTextures.Add(new ImageTexture(tmp.image.value, tmp.nx.value, tmp.ny.value));
                    }
                }
            }

            if (ScenePropertiesList.Materials.Count != 0)
            {
                for (int x = 0; x < ScenePropertiesList.Materials.Count; x++)
                {
                    if (ScenePropertiesList.Materials[x].GetType() == typeof(LambertianProperties))
                    {
                        LambertianProperties tmp = (LambertianProperties)ScenePropertiesList.Materials[x];
                        SceneMaterials.Add(new Lambertian(SceneTextures[tmp.texture.value]));
                    }

                    else if (ScenePropertiesList.Materials[x].GetType() == typeof(MetalProperties))
                    {
                        MetalProperties tmp = (MetalProperties)ScenePropertiesList.Materials[x];
                        SceneMaterials.Add(new Metal(SceneTextures[tmp.texture.value], tmp.roughness.value));
                    }

                    else if (ScenePropertiesList.Materials[x].GetType() == typeof(DielectricProperties))
                    {
                        DielectricProperties tmp = (DielectricProperties)ScenePropertiesList.Materials[x];
                        SceneMaterials.Add(new Dielectric(tmp.refraction.value));
                    }

                    else if (ScenePropertiesList.Materials[x].GetType() == typeof(DiffuseLightProperties))
                    {
                        DiffuseLightProperties tmp = (DiffuseLightProperties)ScenePropertiesList.Materials[x];
                        SceneMaterials.Add(new DiffuseLight(SceneTextures[tmp.texture.value], tmp.intensity.value));
                    }
                }
            }

            if (ScenePropertiesList.Objects.Count != 0)
            {
                for (int x = 0; x < ScenePropertiesList.Objects.Count; x++)
                {
                    if (ScenePropertiesList.Objects[x].GetType() == typeof(SphereProperties))
                    {
                        SphereProperties tmp = (SphereProperties)ScenePropertiesList.Objects[x];
                        Hitable tmp2 = new Sphere(tmp.position, tmp.scale.value, SceneMaterials[tmp.material.value]);
                        if (tmp.invertNormal.value)
                        {
                            SceneList.Add(new FlipNormals(tmp2));
                        } else
                        {
                            SceneList.Add(tmp2);
                        }
                    }

                    else if (ScenePropertiesList.Objects[x].GetType() == typeof(PlaneProperties))
                    {
                        PlaneProperties tmp = (PlaneProperties)ScenePropertiesList.Objects[x];
                        Hitable tmp2;
                        if (tmp.direction.value == 1)
                        {
                            tmp2 = new yzRect(tmp.position1.value, tmp.position2.value, tmp.position3.value, tmp.position4.value, tmp.constant.value, SceneMaterials[tmp.material.value]);
                        } else if (tmp.direction.value == 2)
                        {
                            tmp2 = new zxRect(tmp.position1.value, tmp.position2.value, tmp.position3.value, tmp.position4.value, tmp.constant.value, SceneMaterials[tmp.material.value]);
                        } else
                        {
                            tmp2 = new xyRect(tmp.position1.value, tmp.position2.value, tmp.position3.value, tmp.position4.value, tmp.constant.value, SceneMaterials[tmp.material.value]);
                        }
                        if (tmp.invertNormal.value)
                        {
                            SceneList.Add(new FlipNormals(tmp2));
                        }
                        else
                        {
                            SceneList.Add(tmp2);
                        }
                    }

                    else if (ScenePropertiesList.Objects[x].GetType() == typeof(BoxProperties))
                    {
                        BoxProperties tmp = (BoxProperties)ScenePropertiesList.Objects[x];
                        Hitable tmp2 = new Box(tmp.point1, tmp.point2, SceneMaterials[tmp.material.value]);
                        if (tmp.invertNormal.value)
                        {
                            SceneList.Add(new FlipNormals(tmp2));
                        }
                        else
                        {
                            SceneList.Add(tmp2);
                        }
                    }
                }
            }
        }

        private async Task<Bitmap> cloneRenderPreview()
        {
            // TMP: No idea if this is even doing anything useful... it probably isn't
            return (Bitmap)((Image)RenderPreview.Clone());
        }
        
        private async Task<Bitmap> cloneRenderFinalPreview()
        {
            // TMP: No idea if this is even doing anything useful... it probably isn't
            return (Bitmap)((Image)Render.Clone());
        }

        private void runRenderPreview(ref bool run, bool final)
        {
            Graphics RenderFill = Graphics.FromImage(Render);
            RenderFill.Clear(Color.Black);
            Render = new Bitmap(RenderWidth, RenderHeight, RenderFill);

            Graphics RenderPreviewFill = Graphics.FromImage(RenderPreview);
            RenderPreviewFill.Clear(Color.Black);
            RenderPreview = new Bitmap(397, 397, RenderPreviewFill);

            // TODO: Allow stopping of render preview
            Action<object> StartRenderPreviewAction = (object StartRenderPreviewObject) =>
            {
                // TODO: Calculate brightest object in the scene and parse its brightness it via 'maxLuminance'
                RenderTools.Start(397, 397, 32, ref RenderPreview, SceneCamera, Scene, ref samplesDone, ref progress, ref RenderPreviewBmpInUse, ref ScenePropertiesList.MaxLuminance.value, ref doRenderPreview, ref ScenePropertiesList.EnableWorldLighting.value);
            };

            Action<object> StartRenderFinalPreviewAction = (object StartRenderPreviewObject) =>
            {
                // TODO: Calculate brightest object in the scene and parse its brightness it via 'maxLuminance'
                RenderTools.Start(RenderWidth, RenderHeight, RenderSamples, ref Render, SceneCamera, Scene, ref samplesDone, ref progress, ref RenderFinalPreviewBmpInUse, ref ScenePropertiesList.MaxLuminance.value, ref doRenderFinalPreview, ref ScenePropertiesList.EnableWorldLighting.value);
            };

            Action<object> UpdateUIRenderPreviewAction = async (object UpdateUIRenderPreviewObject) =>
            {
                // TODO: Clean this mess up (at least its working now)
                RenderPreviewPictureBox.Invoke(new Action(() =>
                {
                    RenderPreviewGroupBox.Text = "Render Preview: Currently Rendering";
                    MainToolStripCurrentTaskLabel.Text = "Current Task: Rendering Preview";
                }));

                progress = 0;

                while (true)
                {
                    Thread.Sleep(100);
                    if (progress == 100)
                    {
                        // End task
                        RenderPreviewPictureBox.Invoke(new Action(() =>
                        {
                            RenderPreviewGroupBox.Text = "Render Preview: Rendering Done";
                            MainToolStripCurrentTaskLabel.Text = "Current Task: Done Rendering Preview";
                            MainToolStripTaskLabel.Text = "Progress: Done";
                            doRenderPreview = false;
                            string fileName = "render_preview_" + DateTime.Now.ToString(timeFormat) + ".png";
                            RenderPreview.Save(fileName, ImageFormat.Png);
                        }));
                        break;
                    }
                    while (true)
                    {
                        if (!RenderPreviewBmpInUse)
                        {
                            RenderPreviewBmpInUse = true;
                            break;
                        }
                    }
                    try
                    {
                        tmpRenderPreview = await cloneRenderPreview();
                        RenderPreviewBmpInUse = false;
                        RenderPreviewPictureBox.Invoke(new Action(() =>
                        {
                            RenderPreviewPictureBox.Image = tmpRenderPreview;
                            if (progress == -1)
                            {
                                MainToolStripTaskProgressBar.Style = new ProgressBarStyle();
                                MainToolStripTaskLabel.Text = "";
                            }
                            else
                            {
                                MainToolStripTaskProgressBar.Value = progress;
                                MainToolStripTaskLabel.Text = "Progress: " + progress + "%";
                            }
                        }));
                    }
                    catch (Exception e)
                    {
                        // Tried to clone the image whilst it was in use, this at least doesn't crash the program now!
                    }
                }
            };

            Action<object> UpdateUIRenderFinalPreviewAction = async (object UpdateUIRenderFinalPreviewObject) =>
            {
                // TODO: Clean this mess up (at least its working now)
                RenderPreviewPictureBox.Invoke(new Action(() =>
                {
                    RenderPreviewGroupBox.Text = "Render: Currently Rendering";
                    MainToolStripCurrentTaskLabel.Text = "Current Task: Rendering";
                    MainToolStripTaskLabel.Text = "";
                    toolStripStatusLabel3.Text = "";
                }));

                progress = 0;

                while (true)
                {
                    Thread.Sleep(100);
                    if (progress == 100)
                    {
                        // End task
                        RenderPreviewPictureBox.Invoke(new Action(() =>
                        {
                            RenderPreviewGroupBox.Text = "Render: Rendering Done";
                            MainToolStripCurrentTaskLabel.Text = "Current Task: Done Rendering";
                            MainToolStripTaskLabel.Text = "Progress: Done";
                            startStopFullResRenderToolStripMenuItem.Text = "Start Render";
                            doRenderFinalPreview = false;
                            string fileName = "render_" + DateTime.Now.ToString(timeFormat) + ".png";
                            Render.Save(fileName, ImageFormat.Png);
                        }));
                        break;
                    }
                    while (true)
                    {
                        if (!RenderFinalPreviewBmpInUse)
                        {
                            RenderFinalPreviewBmpInUse = true;
                            break;
                        }
                    }
                    try
                    {
                        tmpRender = await cloneRenderFinalPreview();
                        RenderFinalPreviewBmpInUse = false;
                        RenderPreviewPictureBox.Invoke(new Action(() =>
                        {
                            RenderPreviewPictureBox.Image = tmpRender;
                            if (progress == -1)
                            {
                                MainToolStripTaskProgressBar.Style = new ProgressBarStyle();
                                MainToolStripTaskLabel.Text = "";
                            }
                            else
                            {
                                MainToolStripTaskProgressBar.Value = progress;
                                MainToolStripTaskLabel.Text = "Progress: " + progress + "%";
                            }
                        }));
                    }
                    catch (Exception e)
                    {
                        // Tried to clone the image whilst it was in use, this at least doesn't crash the program now!
                    }
                }
            };

            Action<object> UpdateElapsedTimeRenderPreviewAction = async (object UpdateElapsedTimeRenderPreviewObject) =>
            {
                timeCounter = new DateTime(); // Set counter start value
                bool loopState = true;

                while (loopState)
                {
                    // Update label
                    RenderPreviewPictureBox.Invoke(new Action(() =>
                    {
                        if (MainToolStripTaskLabel.Text == "Progress: Done" || !doRenderFinalPreview)
                        {
                            loopState = false;
                        } else
                        {
                            toolStripStatusLabel3.Text = "Time elapsed: " + timeCounter.ToString("HH:mm:ss");
                        }
                    }));
                    Thread.Sleep(1000);
                    // Add to time counter
                    timeCounter = timeCounter.AddSeconds(1);
                }
            };

            Task RenderD, UpdateImg;

            if (final == true)
            {
                RenderD = new Task(StartRenderFinalPreviewAction, "obj");
                UpdateImg = new Task(UpdateUIRenderFinalPreviewAction, "obj2");
            } else
            {
                RenderD = new Task(StartRenderPreviewAction, "obj");
                UpdateImg = new Task(UpdateUIRenderPreviewAction, "obj2");
            }

            Task TimerElapsed = new Task(UpdateElapsedTimeRenderPreviewAction, "obj3");

            RenderD.Start();
            // TODO: Make this not buggy (please)
            UpdateImg.Start();
            TimerElapsed.Start();
        }

        private void startStopFullResRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Render => Start/Stop Render
            if (!doRenderFinalPreview)
            {
                Invoke(new Action(() =>
                {
                    startStopFullResRenderToolStripMenuItem.Text = "Stop Render";
                }));
                // Convert scene properties into actual scene
                convertScenePropertiesToScene();
                Scene = new HitableList(SceneList);
                doRenderFinalPreview = true;
                runRenderPreview(ref doRenderFinalPreview, true);
            }
            else
            {
                // TODO: Stop Render
                Invoke(new Action(() =>
                {
                    startStopFullResRenderToolStripMenuItem.Text = "Start Render";
                    MainToolStripTaskLabel.Text = "Progress: Stopped";
                }));
                doRenderFinalPreview = false;
                string fileName = "render_" + DateTime.Now.ToString(timeFormat) + ".png";
                while (true)
                {
                    if (!RenderFinalPreviewBmpInUse)
                    {
                        RenderFinalPreviewBmpInUse = true;
                        break;
                    }
                }
                Render.Save(fileName, ImageFormat.Png);
                RenderFinalPreviewBmpInUse = false;
            }
        }

        private void startStopPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Render => Start/Stop Preview Render
            if (!doRenderPreview) {
                doRenderPreview = true;
                Invoke(new Action(() =>
                {
                    startStopPreviewToolStripMenuItem.Text = "Stop Render Preview";
                }));
                // Convert scene properties into actual scene
                convertScenePropertiesToScene();
                Scene = new HitableList(SceneList);
                
                runRenderPreview(ref doRenderPreview, false);
            } else
            {
                doRenderPreview = false;
                Invoke(new Action(() =>
                {
                    startStopPreviewToolStripMenuItem.Text = "Start Render Preview";
                    MainToolStripTaskLabel.Text = "Progress: Stopped";
                }));
                string fileName = "render_preview_" + DateTime.Now.ToString(timeFormat) + ".png";
                while (true)
                {
                    if (!RenderPreviewBmpInUse)
                    {
                        RenderPreviewBmpInUse = true;
                        break;
                    }
                }
                RenderPreview.Save(fileName, ImageFormat.Png);
                RenderPreviewBmpInUse = false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // UI Loaded
            // TODO: Initalise stuff

            // Allow scrolling in 'panel1' but only vertically
            panel1.AutoScroll = false;
            panel1.HorizontalScroll.Maximum = 0;
            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
            panel1.AutoScroll = true;

            createCornellBoxScene();
        }

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // File => New Scene Option
            // TODO: Check if unsaved changes have been made, if so, create dialog about it with save and don't save option, else clear scene to empty

            // Clear scene
            SceneTreeView.Nodes[0].Remove();
            createEmptyScene();
        }

        private void openSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // File => Open File Menu Option
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                /*try
                {*/
                    var filePath = openFileDialogMain.FileName;
                    /*using (Stream str = openFileDialogMain.OpenFile())
                    {
                        // TODO: Run JSON to scene function
                        Console.WriteLine(str);
                        // TODO: Update 'currentOpenFile' with path of currently open file

                        // TMP: Open file in notepad
                        //Process.Start("notepad.exe", filePath);
                    }*/
                    var json = System.IO.File.ReadAllText(filePath);
                    ScenePropertiesList = new JavaScriptSerializer().Deserialize<SceneProperties>(json);
                    Console.WriteLine("wow");
                /*}
                catch
                {
                    Console.WriteLine("Something went wrong?");
                }*/
            }
        }

        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // File => Save File Menu Option
            // If not saved yet, run 'saveAsSceneToolStripMenuItem_Click'
            if (currentOpenFile == "")
            {
                saveAsSceneToolStripMenuItem_Click(sender, e);
            }
            // Else, replace current scene with already 'open' file
            // TODO: Run scene to JSON function
            // TODO: Save JSON to file
        }

        private void saveAsSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // File => Save As File Menu Option
            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                // TODO: Run scene to JSON function
                // TODO: Save JSON to file
                var json = new JavaScriptSerializer().Serialize(ScenePropertiesList);
                // TODO: Update 'currentOpenFile' with path of currently open file

                // TMP: Test save file
                FileStream fs = (FileStream)saveFileDialogMain.OpenFile();
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                fs.Write(bytes, 0, json.Length);
                fs.Close();
            }
        }

        private void updateIndex(object sender, EventArgs e, int index, IntWrapper output)
        {
            output.value = index;
        }

        private void updateInt(object sender, EventArgs e, string text, IntWrapper output)
        {
            if (text != "")
            {
                // TODO: Allow negatives
                output.value = Int32.Parse(text);
            }
            else
            {
                output.value = 0;
            }
        }

        private void updateFloat(object sender, EventArgs e, string text, FloatWrapper output)
        {
            if (text != "")
            {
                // TODO: Allow negatives
                if (text[0] == '-')
                {
                    if (text.Length > 1)
                    {
                        output.value = 0 - float.Parse(text.Substring(1));
                    }
                } else
                {
                    output.value = float.Parse(text);
                }
            } else
            {
                output.value = 0;
            }
        }

        private void updateString(object sender, EventArgs e, string text, StringWrapper output)
        {
            output.value = text;
        }

        private void updateBool(object sender, EventArgs e, bool input, BoolWrapper output)
        {
            output.value = input;
        }

        private void updateVec3(object sender, EventArgs e, string text, Vec3 output, int pos)
        {
            if (text != "")
            {
                // TODO: Allow negatives
                output[pos] = float.Parse(text);
            } else
            {
                output[pos] = 0;
            }
        }

        private void SceneTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure this is the right button
            if (e.Button != MouseButtons.Right) return;

            // Select the node beneath the mouse
            TreeNode selectedNode = SceneTreeView.GetNodeAt(e.X, e.Y);

            // Check if a node was found
            if (selectedNode == null || selectedNode.Text.Contains("[Scene]") || selectedNode.Text.Contains("[Camera]") || selectedNode.Text == "Objects" || selectedNode.Text == "Materials" || selectedNode.Text == "Textures") return;
            
            // 'Select' the node and display the context menu
            SceneTreeView.SelectedNode = selectedNode;
            SceneTreeNodeContextMenuStrip.Show(SceneTreeView, new Point(e.X, e.Y));
        }

        private void SceneTreeNodeContextDeleteNode(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            SceneTreeView.SelectedNode.Remove();
        }

        private GroupBox createDropdownGroupBox(string boxName, Point pos, string[] elements, IntWrapper output)
        {
            GroupBox DropdownGroupBox = new GroupBox();
            DropdownGroupBox.Location = pos;
            DropdownGroupBox.Width = 274;
            DropdownGroupBox.Height = 50;
            DropdownGroupBox.Text = boxName;

            ComboBox ComboBoxInput = new ComboBox();
            ComboBoxInput.Location = new Point(10, 18);
            ComboBoxInput.Width = 254;
            ComboBoxInput.Height = 20;
            ComboBoxInput.TabIndex = output.value;

            if (elements.Length != 0)
            {
                ComboBoxInput.Text = "Selected: " + elements[output.value];
            } else
            {
                ComboBoxInput.Text = "Select " + boxName;
            }

            ComboBoxInput.Items.AddRange(elements);
            ComboBoxInput.DropDownClosed += (sender, e) =>
            {
                updateIndex(sender, e, ComboBoxInput.SelectedIndex, output);
            };

            DropdownGroupBox.Controls.Add(ComboBoxInput);

            return DropdownGroupBox;
        }

        private GroupBox createCheckboxGroupBox(string boxName, Point pos, BoolWrapper output)
        {
            GroupBox CheckBoxGroupBox = new GroupBox();
            CheckBoxGroupBox.Location = pos;
            CheckBoxGroupBox.Width = 274;
            CheckBoxGroupBox.Height = 50;
            CheckBoxGroupBox.Text = boxName;

            CheckBox CheckBoxInput = new CheckBox();
            CheckBoxInput.Location = new Point(10, 18);
            CheckBoxInput.Width = 254;
            CheckBoxInput.Height = 20;
            CheckBoxInput.Text = boxName;
            CheckBoxInput.Checked = output.value;
            CheckBoxInput.Click += (sender, e) =>
            {
                updateBool(sender, e, CheckBoxInput.Checked, output);
            };


            CheckBoxGroupBox.Controls.Add(CheckBoxInput);

            return CheckBoxGroupBox;
        }

        private GroupBox createSingleIntInputGroupBox(string boxName, Point pos, IntWrapper output)
        {
            GroupBox SingleInputGroupBox = new GroupBox();
            SingleInputGroupBox.Location = pos;
            SingleInputGroupBox.Width = 274;
            SingleInputGroupBox.Height = 50;
            SingleInputGroupBox.Text = boxName;

            TextBox TextBoxInput = new TextBox();
            TextBoxInput.Location = new Point(10, 18);
            TextBoxInput.Width = 254;
            TextBoxInput.Height = 20;
            TextBoxInput.Text = output.value.ToString();
            TextBoxInput.TextChanged += (sender, e) =>
            {
                updateInt(sender, e, TextBoxInput.Text, output);
            };

            SingleInputGroupBox.Controls.Add(TextBoxInput);

            return SingleInputGroupBox;
        }

        private GroupBox createSingleFloatInputGroupBox(string boxName, Point pos, FloatWrapper output)
        {
            GroupBox SingleInputGroupBox = new GroupBox();
            SingleInputGroupBox.Location = pos;
            SingleInputGroupBox.Width = 274;
            SingleInputGroupBox.Height = 50;
            SingleInputGroupBox.Text = boxName;

            TextBox TextBoxInput = new TextBox();
            TextBoxInput.Location = new Point(10, 18);
            TextBoxInput.Width = 254;
            TextBoxInput.Height = 20;
            TextBoxInput.Text = output.value.ToString();
            TextBoxInput.TextChanged += (sender, e) =>
            {
                updateFloat(sender, e, TextBoxInput.Text, output);
            };

            SingleInputGroupBox.Controls.Add(TextBoxInput);

            return SingleInputGroupBox;
        }

        private GroupBox createSingleStringInputGroupBox(string boxName, Point pos, StringWrapper output, TreeNode node)
        {
            GroupBox SingleInputGroupBox = new GroupBox();
            SingleInputGroupBox.Location = pos;
            SingleInputGroupBox.Width = 274;
            SingleInputGroupBox.Height = 50;
            SingleInputGroupBox.Text = boxName;

            TextBox TextBoxInput = new TextBox();
            TextBoxInput.Location = new Point(10, 18);
            TextBoxInput.Width = 254;
            TextBoxInput.Height = 20;
            TextBoxInput.Text = output.value.ToString();
            TextBoxInput.TextChanged += (sender, e) =>
            {
                updateString(sender, e, TextBoxInput.Text, output);
                // TODO: Add 'updateTreeView' with new name

                string[] elementCombo = node.Text.Split('[');
                string elementType = elementCombo[1].Trim(']');

                node.Text = output.value + " [" + elementType + "]";
                //updateTreeView();
            };

            SingleInputGroupBox.Controls.Add(TextBoxInput);

            return SingleInputGroupBox;
        }

        private GroupBox createFileInputGroupBox(string boxName, Point pos, UintWrapper output, IntWrapper nx, IntWrapper ny)
        {
            GroupBox SingleInputGroupBox = new GroupBox();
            SingleInputGroupBox.Location = pos;
            SingleInputGroupBox.Width = 274;
            SingleInputGroupBox.Height = 50;
            SingleInputGroupBox.Text = boxName;

            Button FileOpenButton = new Button();
            FileOpenButton.Location = new Point(10, 18);
            FileOpenButton.Width = 254;
            FileOpenButton.Height = 20;
            FileOpenButton.Text = "Open Image Texture";
            FileOpenButton.Image = Bitmap.FromHicon(SystemIcons.Question.Handle);
            FileOpenButton.Click += (sender, e) =>
            {
                OpenFileDialog imageOpenFileDialog = new OpenFileDialog()
                {
                    Filter = "Image files (*.png)|*.png",
                    Title = "Open an image file"
                };
                if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap tmpImgWoo = new Bitmap(imageOpenFileDialog.FileName);
                        output.value = ImageTools.GetImagePixels24(tmpImgWoo);
                        nx.value = tmpImgWoo.Width;
                        ny.value = tmpImgWoo.Height;
                    }
                    catch
                    {
                        Console.WriteLine("didn't work, yikes");
                    }
                }
            };

            SingleInputGroupBox.Controls.Add(FileOpenButton);

            return SingleInputGroupBox;
        }

        private GroupBox createTripleInputGroupBox(string boxName, Point pos, string input1Text, string input2Text, string input3Text, Vec3 output)
        {
            GroupBox TripleInputGroupBox = new GroupBox();
            TripleInputGroupBox.Location = pos;
            TripleInputGroupBox.Width = 274;
            TripleInputGroupBox.Height = 50;
            TripleInputGroupBox.Text = boxName;

            Label LabelInput1 = new Label();
            LabelInput1.Location = new Point(10, 21);
            LabelInput1.Width = 14;
            LabelInput1.Height = 13;
            LabelInput1.Text = input1Text;

            TextBox TextBoxInput1 = new TextBox();
            TextBoxInput1.Location = new Point(25, 18);
            TextBoxInput1.Width = 51;
            TextBoxInput1.Height = 20;
            TextBoxInput1.Text = output.x.ToString();
            TextBoxInput1.TextChanged += (sender, e) =>
            {
                updateVec3(sender, e, TextBoxInput1.Text, output, 0);
            };

            Label LabelInput2 = new Label();
            LabelInput2.Location = new Point(101, 21);
            LabelInput2.Width = 14;
            LabelInput2.Height = 13;
            LabelInput2.Text = input2Text;

            TextBox TextBoxInput2 = new TextBox();
            TextBoxInput2.Location = new Point(116, 18);
            TextBoxInput2.Width = 51;
            TextBoxInput2.Height = 20;
            TextBoxInput2.Text = output.y.ToString();
            TextBoxInput2.TextChanged += (sender, e) =>
            {
                updateVec3(sender, e, TextBoxInput2.Text, output, 1);
            };

            Label LabelInput3 = new Label();
            LabelInput3.Location = new Point(198, 21);
            LabelInput3.Width = 14;
            LabelInput3.Height = 13;
            LabelInput3.Text = input3Text;

            TextBox TextBoxInput3 = new TextBox();
            TextBoxInput3.Location = new Point(213, 18);
            TextBoxInput3.Width = 51;
            TextBoxInput3.Height = 20;
            TextBoxInput3.Text = output.z.ToString();
            TextBoxInput3.TextChanged += (sender, e) =>
            {
                updateVec3(sender, e, TextBoxInput3.Text, output, 2);
            };

            TripleInputGroupBox.Controls.Add(LabelInput1);
            TripleInputGroupBox.Controls.Add(TextBoxInput1);
            TripleInputGroupBox.Controls.Add(LabelInput2);
            TripleInputGroupBox.Controls.Add(TextBoxInput2);
            TripleInputGroupBox.Controls.Add(LabelInput3);
            TripleInputGroupBox.Controls.Add(TextBoxInput3);

            return TripleInputGroupBox;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Tree View => Node Selected
            TreeNode node = SceneTreeView.SelectedNode;
            if (node.Text != SceneName & node.Text != "Objects" & node.Text != "Materials" & node.Text != "Textures")
            {
                // Not catagory, update properties panel
                panel1.Controls.Clear();
                Invoke(new Action(() =>
                {
                    panel1.Text = "Properties : " + node.Text;

                    // Get element name and type
                    string[] elementCombo = node.Text.Split('[');
                    string elementName = elementCombo[0].Remove(elementCombo[0].Length - 1);
                    string elementType = elementCombo[1].Trim(']');

                    // Get elementProperties
                    if (elementType == "Camera")
                    {
                        GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ScenePropertiesList.Camera.name, SceneTreeView.SelectedNode);
                        GroupBox positionGroupBox = createTripleInputGroupBox("Position", new Point(0, 56), "X", "Y", "Z", ScenePropertiesList.Camera.position);
                        GroupBox focusPosGroupBox = createTripleInputGroupBox("Focus Position", new Point(0, 112), "X", "Y", "Z", ScenePropertiesList.Camera.focusPos);
                        GroupBox worldLightingGroupBox = createCheckboxGroupBox("World Lighting", new Point(0, 168), ScenePropertiesList.EnableWorldLighting);
                        GroupBox maxLuminanceGroupBox = createSingleFloatInputGroupBox("Max Luminance", new Point(0, 224), ScenePropertiesList.MaxLuminance);

                        panel1.Controls.Add(nameGroupBox);
                        panel1.Controls.Add(positionGroupBox);
                        panel1.Controls.Add(focusPosGroupBox);
                        panel1.Controls.Add(worldLightingGroupBox);
                        panel1.Controls.Add(maxLuminanceGroupBox);
                    } else if (elementType == "Sphere" || elementType == "Plane" || elementType == "Box") 
                    {
                        // Object
                        int index = -1;
                        string[] materialNameList = new string[ScenePropertiesList.Materials.Count];

                        for (int x = 0; x < ScenePropertiesList.Objects.Count; x++)
                        {
                            if (elementName == ScenePropertiesList.Objects[x].name.value)
                            {
                                index = x;
                            }
                        }

                        for (int x = 0; x < ScenePropertiesList.Materials.Count; x++)
                        {
                            materialNameList[x] = ScenePropertiesList.Materials[x].name.value;
                        }

                        if (ScenePropertiesList.Objects[index].GetType() == typeof(SphereProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((SphereProperties)ScenePropertiesList.Objects[index]).name, SceneTreeView.SelectedNode);
                            GroupBox positionGroupBox = createTripleInputGroupBox("Position", new Point(0, 56), "X", "Y", "Z", ((SphereProperties)ScenePropertiesList.Objects[index]).position);
                            GroupBox scaleGroupBox = createSingleFloatInputGroupBox("Radius", new Point(0, 112), ((SphereProperties)ScenePropertiesList.Objects[index]).scale);
                            GroupBox materialGroupBox = createDropdownGroupBox("Material", new Point(0, 168), materialNameList, ((SphereProperties)ScenePropertiesList.Objects[index]).material);
                            GroupBox invertNormalGroupBox = createCheckboxGroupBox("Invert Normal", new Point(0, 224), ((SphereProperties)ScenePropertiesList.Objects[index]).invertNormal);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(positionGroupBox);
                            panel1.Controls.Add(scaleGroupBox);
                            panel1.Controls.Add(materialGroupBox);
                            panel1.Controls.Add(invertNormalGroupBox);
                        }

                        else if (ScenePropertiesList.Objects[index].GetType() == typeof(PlaneProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((PlaneProperties)ScenePropertiesList.Objects[index]).name, SceneTreeView.SelectedNode);
                            // TODO: Make direction a drop down box
                            GroupBox directionGroupBox = createSingleIntInputGroupBox("Direction (0: XY, 1: YZ or 2: ZX)", new Point(0, 56), ((PlaneProperties)ScenePropertiesList.Objects[index]).direction);
                            GroupBox position1GroupBox = createSingleFloatInputGroupBox("Point 1 Axis 1", new Point(0, 112), ((PlaneProperties)ScenePropertiesList.Objects[index]).position1);
                            GroupBox position2GroupBox = createSingleFloatInputGroupBox("Point 1 Axis 2", new Point(0, 168), ((PlaneProperties)ScenePropertiesList.Objects[index]).position2);
                            GroupBox position3GroupBox = createSingleFloatInputGroupBox("Point 2 Axis 1", new Point(0, 224), ((PlaneProperties)ScenePropertiesList.Objects[index]).position3);
                            GroupBox position4GroupBox = createSingleFloatInputGroupBox("Point 2 Axis 2", new Point(0, 280), ((PlaneProperties)ScenePropertiesList.Objects[index]).position4);
                            GroupBox constantGroupBox = createSingleFloatInputGroupBox("Other Axis Offset", new Point(0, 336), ((PlaneProperties)ScenePropertiesList.Objects[index]).constant);
                            GroupBox materialGroupBox = createDropdownGroupBox("Material", new Point(0, 392), materialNameList, ((PlaneProperties)ScenePropertiesList.Objects[index]).material);
                            GroupBox invertNormalGroupBox = createCheckboxGroupBox("Invert Normal", new Point(0, 448), ((PlaneProperties)ScenePropertiesList.Objects[index]).invertNormal);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(directionGroupBox);
                            panel1.Controls.Add(position1GroupBox);
                            panel1.Controls.Add(position2GroupBox);
                            panel1.Controls.Add(position3GroupBox);
                            panel1.Controls.Add(position4GroupBox);
                            panel1.Controls.Add(constantGroupBox);
                            panel1.Controls.Add(materialGroupBox);
                            panel1.Controls.Add(invertNormalGroupBox);
                        }

                        else if (ScenePropertiesList.Objects[index].GetType() == typeof(BoxProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((BoxProperties)ScenePropertiesList.Objects[index]).name, SceneTreeView.SelectedNode);
                            GroupBox point1GroupBox = createTripleInputGroupBox("Point 1", new Point(0, 56), "X", "Y", "Z", ((BoxProperties)ScenePropertiesList.Objects[index]).point1);
                            GroupBox point2GroupBox = createTripleInputGroupBox("Point 2", new Point(0, 112), "X", "Y", "Z", ((BoxProperties)ScenePropertiesList.Objects[index]).point2);
                            GroupBox materialGroupBox = createDropdownGroupBox("Material", new Point(0, 168), materialNameList, ((BoxProperties)ScenePropertiesList.Objects[index]).material);
                            GroupBox invertNormalGroupBox = createCheckboxGroupBox("Invert Normal", new Point(0, 224), ((BoxProperties)ScenePropertiesList.Objects[index]).invertNormal);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(point1GroupBox);
                            panel1.Controls.Add(point2GroupBox);
                            panel1.Controls.Add(materialGroupBox);
                            panel1.Controls.Add(invertNormalGroupBox);
                        }
                    } else if (elementType == "Lambertian" || elementType == "Metal" || elementType == "Dielectric" || elementType == "DiffuseLight")
                    {
                        // Material
                        int index = -1;
                        string[] textureNameList = new string[ScenePropertiesList.Textures.Count];

                        for (int x = 0; x < ScenePropertiesList.Materials.Count; x++)
                        {
                            if (elementName == ScenePropertiesList.Materials[x].name.value)
                            {
                                index = x;
                            }
                        }

                        for (int x = 0; x < ScenePropertiesList.Textures.Count; x++)
                        {
                            textureNameList[x] = ScenePropertiesList.Textures[x].name.value;
                        }

                        if (ScenePropertiesList.Materials[index].GetType() == typeof(LambertianProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((LambertianProperties)ScenePropertiesList.Materials[index]).name, SceneTreeView.SelectedNode);
                            GroupBox textureGroupBox = createDropdownGroupBox("Texture", new Point(0, 56), textureNameList, ((LambertianProperties)ScenePropertiesList.Materials[index]).texture);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(textureGroupBox);
                        } else if (ScenePropertiesList.Materials[index].GetType() == typeof(MetalProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((MetalProperties)ScenePropertiesList.Materials[index]).name, SceneTreeView.SelectedNode);
                            GroupBox textureGroupBox = createDropdownGroupBox("Texture", new Point(0, 56), textureNameList, ((MetalProperties)ScenePropertiesList.Materials[index]).texture);
                            GroupBox roughnessGroupBox = createSingleFloatInputGroupBox("Intensity", new Point(0, 112), ((MetalProperties)ScenePropertiesList.Materials[index]).roughness);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(textureGroupBox);
                            panel1.Controls.Add(roughnessGroupBox);
                        } else if (ScenePropertiesList.Materials[index].GetType() == typeof(DielectricProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((DielectricProperties)ScenePropertiesList.Materials[index]).name, SceneTreeView.SelectedNode);
                            GroupBox refractionGroupBox = createSingleFloatInputGroupBox("Intensity", new Point(0, 56), ((DielectricProperties)ScenePropertiesList.Materials[index]).refraction);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(refractionGroupBox);
                        } else if (ScenePropertiesList.Materials[index].GetType() == typeof(DiffuseLightProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((DiffuseLightProperties)ScenePropertiesList.Materials[index]).name, SceneTreeView.SelectedNode);
                            GroupBox textureGroupBox = createDropdownGroupBox("Texture", new Point(0, 56), textureNameList, ((DiffuseLightProperties)ScenePropertiesList.Materials[index]).texture);
                            GroupBox intensityGroupBox = createSingleFloatInputGroupBox("Intensity", new Point(0, 112), ((DiffuseLightProperties)ScenePropertiesList.Materials[index]).intensity);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(textureGroupBox);
                            panel1.Controls.Add(intensityGroupBox);
                        }
                    } else if (elementType == "ConstantTexture" || elementType == "CheckerTexture" || elementType == "PerlinNoiseTexture" || elementType == "TurbulentNoiseTexture" || elementType == "MarbleNoiseTexture" || elementType == "ImageTexture")
                    {
                        // Texture
                        int index = -1;
                        string[] textureNameList = new string[ScenePropertiesList.Textures.Count];

                        for (int x = 0; x < ScenePropertiesList.Textures.Count; x++)
                        {
                            if (elementName == ScenePropertiesList.Textures[x].name.value)
                            {
                                index = x;
                            }
                        }

                        for (int x = 0; x < ScenePropertiesList.Textures.Count; x++)
                        {
                            textureNameList[x] = ScenePropertiesList.Textures[x].name.value;
                        }

                        if (ScenePropertiesList.Textures[index].GetType() == typeof(ConstantTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((ConstantTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox colorGroupBox = createTripleInputGroupBox("Color", new Point(0, 56), "R", "G", "B", ((ConstantTextureProperties)ScenePropertiesList.Textures[index]).color);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(colorGroupBox);
                        } else if (ScenePropertiesList.Textures[index].GetType() == typeof(CheckerTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((CheckerTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox color1GroupBox = createDropdownGroupBox("Texture 1", new Point(0, 56), textureNameList, ((CheckerTextureProperties)ScenePropertiesList.Textures[index]).color1);
                            GroupBox color2GroupBox = createDropdownGroupBox("Texture 2", new Point(0, 112), textureNameList, ((CheckerTextureProperties)ScenePropertiesList.Textures[index]).color2);
                            GroupBox scaleGroupBox = createSingleFloatInputGroupBox("Scale", new Point(0, 168), ((CheckerTextureProperties)ScenePropertiesList.Textures[index]).scale);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(color1GroupBox);
                            panel1.Controls.Add(color2GroupBox);
                            panel1.Controls.Add(scaleGroupBox);
                        } else if (ScenePropertiesList.Textures[index].GetType() == typeof(PerlinNoiseTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((PerlinNoiseTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox scaleGroupBox = createSingleFloatInputGroupBox("Scale", new Point(0, 56), ((PerlinNoiseTextureProperties)ScenePropertiesList.Textures[index]).scale);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(scaleGroupBox);
                        } else if (ScenePropertiesList.Textures[index].GetType() == typeof(TurbulentNoiseTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((TurbulentNoiseTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox scaleGroupBox = createSingleFloatInputGroupBox("Scale", new Point(0, 56), ((TurbulentNoiseTextureProperties)ScenePropertiesList.Textures[index]).scale);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(scaleGroupBox);
                        } else if (ScenePropertiesList.Textures[index].GetType() == typeof(MarbleNoiseTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((MarbleNoiseTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox scaleGroupBox = createSingleFloatInputGroupBox("Scale", new Point(0, 56), ((MarbleNoiseTextureProperties)ScenePropertiesList.Textures[index]).scale);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(scaleGroupBox);
                        } else if (ScenePropertiesList.Textures[index].GetType() == typeof(ImageTextureProperties))
                        {
                            GroupBox nameGroupBox = createSingleStringInputGroupBox("Name", new Point(0, 0), ((ImageTextureProperties)ScenePropertiesList.Textures[index]).name, SceneTreeView.SelectedNode);
                            GroupBox imageGroupBox = createFileInputGroupBox("Image", new Point(0, 56), ((ImageTextureProperties)ScenePropertiesList.Textures[index]).image, ((ImageTextureProperties)ScenePropertiesList.Textures[index]).nx, ((ImageTextureProperties)ScenePropertiesList.Textures[index]).ny);

                            panel1.Controls.Add(nameGroupBox);
                            panel1.Controls.Add(imageGroupBox);
                        }
                    }
                }));
            }
        }

        private void addToSceneList(string sceneElement)
        {
            if (sceneElement == "Sphere")
            {
                // TODO: Do this to all others
                int id = SceneTreeView.Nodes[0].Nodes[1].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("New Sphere " + id + " [Sphere]");
                ScenePropertiesList.Objects.Add(new SphereProperties("New Sphere " + id.ToString(), new Vec3(0, 0, 0), 1, 0, false));
            } else if (sceneElement == "Plane")
            {
                int id = SceneTreeView.Nodes[0].Nodes[1].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("New Plane " + id + " [Plane]");
                ScenePropertiesList.Objects.Add(new PlaneProperties("New Plane " + id.ToString(), 0, 0, 0, 1, 1, 0, 0, false));
            } else if (sceneElement == "Box")
            {
                int id = SceneTreeView.Nodes[0].Nodes[1].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[1].Nodes.Add("New Box " + id + " [Box]");
                ScenePropertiesList.Objects.Add(new BoxProperties("New Box " + id.ToString(), new Vec3(0, 0, 0), new Vec3(1, 1, 1), 0, false));
            } else if (sceneElement == "Lambertian")
            {
                int id = SceneTreeView.Nodes[0].Nodes[2].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("New Lambertian " + id + " [Lambertian]");
                ScenePropertiesList.Materials.Add(new LambertianProperties("New Lambertian " + id.ToString(), 0));
            } else if (sceneElement == "Metal")
            {
                int id = SceneTreeView.Nodes[0].Nodes[2].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("New Metal " + id + " [Metal]");
                ScenePropertiesList.Materials.Add(new MetalProperties("New Metal " + id.ToString(), 0, 0.5f));
            } else if (sceneElement == "Dielectric")
            {
                int id = SceneTreeView.Nodes[0].Nodes[2].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("New Dielectric " + id + " [Dielectric]");
                ScenePropertiesList.Materials.Add(new DielectricProperties("New Dielectric " + id.ToString(), 0.5f));
            } else if (sceneElement == "DiffuseLight")
            {
                int id = SceneTreeView.Nodes[0].Nodes[2].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[2].Nodes.Add("New DiffuseLight " + id + " [DiffuseLight]");
                ScenePropertiesList.Materials.Add(new DiffuseLightProperties("New DiffuseLight " + id.ToString(), 0, 1));
            } else if (sceneElement == "ConstantTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New ConstantTexture " + id + " [ConstantTexture]");
                ScenePropertiesList.Textures.Add(new ConstantTextureProperties("New ConstantTexture " + id.ToString(), new Vec3(1, 1, 1)));
            } else if (sceneElement == "CheckerTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New CheckerTexture " + id + " [CheckerTexture]");
                ScenePropertiesList.Textures.Add(new CheckerTextureProperties("New CheckerTexture " + id.ToString(), 0, 0, 1));
            } else if (sceneElement == "PerlinNoiseTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New PerlinNoiseTexture " + id + " [PerlinNoiseTexture]");
                ScenePropertiesList.Textures.Add(new PerlinNoiseTextureProperties("New PerlinNoiseTexture " + id.ToString(), 1));
            } else if (sceneElement == "TurbulentNoiseTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New TurbulentNoiseTexture " + id + " [TurbulentNoiseTexture]");
                ScenePropertiesList.Textures.Add(new TurbulentNoiseTextureProperties("New TurbulentNoiseTexture " + id.ToString(), 1));
            } else if (sceneElement == "MarbleNoiseTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New MarbleNoiseTexture " + id + " [MarbleNoiseTexture]");
                ScenePropertiesList.Textures.Add(new MarbleNoiseTextureProperties("New MarbleNoiseTexture " + id.ToString(), 1));
            } else if (sceneElement == "ImageTexture")
            {
                int id = SceneTreeView.Nodes[0].Nodes[3].Nodes.Count;
                SceneTreeView.Nodes[0].Nodes[3].Nodes.Add("New ImageTexture " + id + " [ImageTexture]");
                ScenePropertiesList.Textures.Add(new ImageTextureProperties("New ImageTexture " + id.ToString(), noImageBitmap));
            }
        }

        private void sphereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Object => Sphere Option
            addToSceneList("Sphere");
        }

        private void planeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Object => Plane Option
            addToSceneList("Plane");
        }

        private void boxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Object => Box Option
            addToSceneList("Box");
        }

        private void lambertianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Material => Lambertian Option
            addToSceneList("Lambertian");
        }

        private void metalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Material => Metal Option
            addToSceneList("Metal");
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newCornellBoxSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneTreeView.Nodes[0].Remove();
            createCornellBoxScene();
        }

        private void newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneTreeView.Nodes[0].Remove();
            createBook1Scene();
        }

        private void dielectricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Material => Dielectric Option
            addToSceneList("Dielectric");
        }

        private void diffuseLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Material => Diffuse Light Option
            addToSceneList("DiffuseLight");
        }

        private void constantTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Constant Texture Option
            addToSceneList("ConstantTexture");
        }

        private void checkerTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Checker Texture Option
            addToSceneList("CheckerTexture");
        }

        private void perlinNoiseTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Perlin Noise Texture Option
            addToSceneList("PerlinNoiseTexture");
        }

        private void turbulentNoiseTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Turbulent Noise Texture Option
            addToSceneList("TurbulentNoiseTexture");
        }

        private void marbleNoiseTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Marble Noise Texture Option
            addToSceneList("MarbleNoiseTexture");
        }

        private void imageTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add => Texture => Image Texture Option
            addToSceneList("ImageTexture");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Resolution => Width Text Field
            if (RenderResolutionWidthTextBox.Text != "")
            {
                RenderWidth = Int32.Parse(RenderResolutionWidthTextBox.Text);
                Render = new Bitmap(RenderWidth, RenderHeight);
            }
            else
            {
                RenderWidth = 1;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Resolution => Height Text Field
            if (RenderResolutionHeightTextBox.Text != "")
            {
                RenderHeight = Int32.Parse(RenderResolutionHeightTextBox.Text);
                Render = new Bitmap(RenderWidth, RenderHeight);
            }
            else
            {
                RenderHeight = 1;
            }
        }

        private void RenderSamplesValueTextBox_TextChanged(object sender, EventArgs e)
        {
            // Samples => Value Text Field
            if (RenderSamplesValueTextBox.Text != "")
            {
                RenderSamples = Int32.Parse(RenderSamplesValueTextBox.Text);
            } else
            {
                RenderSamples = 1;
            }
        }
    }
}
