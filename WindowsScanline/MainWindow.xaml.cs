using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WindowsScanline
{
    public partial class MainWindow : Window
    {
        
        private Device device;
        
        Camera camera = new Camera();
        Mesh[] meshes;
        ObjParser.Obj[] objects;
        ObjParser.Obj objparser = new ObjParser.Obj();
        WriteableBitmap bmp;
        Vector3 lightPosition =new Vector3(10, 10, 20);

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Choose the back buffer resolution here
            bmp = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgra32, null);

            device = new Device(bmp,lightPosition);

            // Our Image XAML control
            frontBuffer.Source = (ImageSource) bmp;

            //setup camera
            camera.Position = new Vector3(0, 0, 10.0f);
            camera.Target = Vector3.Zero;

            //Setup objects filenames
            string[] filenames = { "box.obj", "cylinder.obj","cone.obj", "sphere.obj", "teapot.obj","torus.obj", "tube.obj" };
            objects = new ObjParser.Obj[filenames.Length];
            meshes = new Mesh[filenames.Length];


            //Load from objs
            for (int i=0; i < filenames.Length; i++)
            {
                objects[i] = new ObjParser.Obj();
                objects[i].LoadObj(filenames[i]);
            }

            for(int i = 0; i < objects.Length; i++)
            {
                foreach(ObjParser.Types.Face f in objects[i].FaceList)
                {
                    f.VertexIndexList[0]--;
                    f.VertexIndexList[1]--;
                    f.VertexIndexList[2]--;
                    f.NormalsVertexIndexList[0]--;
                    f.NormalsVertexIndexList[1]--;
                    f.NormalsVertexIndexList[2]--;
                }
            }
            //Build the meshes
            for(int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = new Mesh(filenames[i], objects[i].VertexList.Count, objects[i].FaceList.Count);
                for(int j = 0; j < objects[i].VertexList.Count; j++)
                {
                    var x = (float)objects[i].VertexList[j].X ;
                    var y = (float)objects[i].VertexList[j].Y ;
                    var z = (float)objects[i].VertexList[j].Z ;
                    meshes[i].Vertices[j]=new Mesh.Vertex { Coordinates = new Vector3(x, y, z), Normal = new Vector3(0, 0, 0) };
                }
                for(int k = 0; k < objects[i].FaceList.Count; k++)
                {
                    meshes[i].Faces[k]= new Face { A = objects[i].FaceList[k].VertexIndexList[0], B = objects[i].FaceList[k].VertexIndexList[1], C = objects[i].FaceList[k].VertexIndexList[2], nA = objects[i].FaceList[k].NormalsVertexIndexList[0], nB = objects[i].FaceList[k].NormalsVertexIndexList[1], nC = objects[i].FaceList[k].NormalsVertexIndexList[2] };
                }
                foreach(Face f in meshes[i].Faces)
                {
                    meshes[i].Vertices[f.A].Normal.X = (float)objects[i].NormalsList[f.nA].X;
                    meshes[i].Vertices[f.A].Normal.Y = (float)objects[i].NormalsList[f.nA].Y;
                    meshes[i].Vertices[f.A].Normal.Z = (float)objects[i].NormalsList[f.nA].Z;
                }
            }

            //additional mesh modifications
            meshes[0].TranslateMesh(0, 1, 0);
            meshes[0].ScaleMesh(1, 1, 3);
            meshes[1].TranslateMesh(0, 1, 0);
            meshes[1].ScaleMesh(0.5, 0.5, 0.5);
            meshes[2].TranslateMesh(-1, 2, 0);
            meshes[2].ScaleMesh(0.5, 0.5, 0.5);
            meshes[3].TranslateMesh(3, 3, 0);
            meshes[3].ScaleMesh(0.5, 0.5, 0.5);
            meshes[4].TranslateMesh(-5, 2, 0);
            meshes[4].ScaleMesh(0.33, 0.33, 0.33);
            meshes[5].TranslateMesh(-3, -2, 0);
            meshes[5].ScaleMesh(0.5, 0.5, 0.5);
            meshes[6].TranslateMesh(1, -2, 0);
            meshes[6].ScaleMesh(0.5, 0.5, 0.5);

            // Registering to the XAML rendering loop
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            // Registering save image function
            this.KeyDown += SaveRenderedImage;
           
        }

        private void SaveRenderedImage(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.S)
            {
                using (FileStream stream5 = new FileStream("rrender.png", FileMode.Create))
                {
                    PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                    encoder5.Frames.Add(BitmapFrame.Create(bmp));
                    encoder5.Save(stream5);
                }
            }
        }

        // Rendering loop handler
        DateTime previousDate;
        protected void CompositionTarget_Rendering(object sender, object e)
        {
            // Fps
            var now = DateTime.Now;
            var currentFps = 1000.0 / (now - previousDate).TotalMilliseconds;
            previousDate = now;

            fpsBox.Text = string.Format("{0:0.00} fps", currentFps);
            //rendering loop
            device.Clear(0, 0, 0, 255);

            // rotating the scene during each frame rendered
            foreach (Mesh m in meshes)
            {
                m.Rotation = new Vector3(m.Rotation.X + 0.001f, m.Rotation.Y + 0.001f, m.Rotation.Z);
            }

            //MainWindow rendering routine
            foreach (Mesh m in meshes)
            {
                device.RenderTriangles(camera, m);
            }
            // Flushing the back buffer into the front buffer
            device.Present();
        
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
