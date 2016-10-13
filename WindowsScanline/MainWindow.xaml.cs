using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Device device;
        
        Mesh mesh = new Mesh("Cube", 8,12);
        Mesh grid = new Mesh("Grid", 28, 2);
        Mesh cylinder = new Mesh("Cone", 110, 216);
        Mesh cone = new Mesh("Cone", 122, 288);
        Camera mera = new Camera();

        ObjParser.Obj objparser = new ObjParser.Obj();
        


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Choose the back buffer resolution here
            WriteableBitmap bmp = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgra32, null);

            device = new Device(bmp);

            // Our Image XAML control
            frontBuffer.Source = (ImageSource) bmp;


            mesh.Vertices[0] = new Vector3(-1, 1, 1);
            mesh.Vertices[1] = new Vector3(1, 1, 1);
            mesh.Vertices[2] = new Vector3(-1, -1, 1);
            mesh.Vertices[3] = new Vector3(1, -1, 1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, -1);
            mesh.Vertices[7] = new Vector3(-1, -1, -1);

            mesh.Faces[0] = new Face { A = 0, B = 1, C = 2 };
            mesh.Faces[1] = new Face { A = 1, B = 2, C = 3 };
            mesh.Faces[2] = new Face { A = 1, B = 3, C = 6 };
            mesh.Faces[3] = new Face { A = 1, B = 5, C = 6 };
            mesh.Faces[4] = new Face { A = 0, B = 1, C = 4 };
            mesh.Faces[5] = new Face { A = 1, B = 4, C = 5 };

            mesh.Faces[6] = new Face { A = 2, B = 3, C = 7 };
            mesh.Faces[7] = new Face { A = 3, B = 6, C = 7 };
            mesh.Faces[8] = new Face { A = 0, B = 2, C = 7 };
            mesh.Faces[9] = new Face { A = 0, B = 4, C = 7 };
            mesh.Faces[10] = new Face { A = 4, B = 5, C = 6 };
            mesh.Faces[11] = new Face { A = 4, B = 6, C = 7 };

            grid.Vertices[0] = new Vector3(2, 3, 0);
            grid.Vertices[1] = new Vector3(2, 2, 0);
            grid.Vertices[2] = new Vector3(2, 1, 0);
            grid.Vertices[3] = new Vector3(2, 0, 0);
            grid.Vertices[4] = new Vector3(2, -1, 0);
            grid.Vertices[5] = new Vector3(2, -2, 0);
            grid.Vertices[6] = new Vector3(2, -3, 0);
            grid.Vertices[7] = new Vector3(1, 3, 0);
            grid.Vertices[8] = new Vector3(1, 2, 0);
            grid.Vertices[9] = new Vector3(1, 1, 0);
            grid.Vertices[10] = new Vector3(1, 0, 0);
            grid.Vertices[11] = new Vector3(1, -1, 0);
            grid.Vertices[12] = new Vector3(1, -2, 0);
            grid.Vertices[13] = new Vector3(1, -3, 0);
            grid.Vertices[14] = new Vector3(0, 3, 0);
            grid.Vertices[15] = new Vector3(0, 2, 0);
            grid.Vertices[16] = new Vector3(0, 1, 0);
            grid.Vertices[17] = new Vector3(0, 0, 0);
            grid.Vertices[18] = new Vector3(0, -1, 0);
            grid.Vertices[19] = new Vector3(0, -2, 0);
            grid.Vertices[20] = new Vector3(0, -3, 0);
            grid.Vertices[21] = new Vector3(-1, 3, 0);
            grid.Vertices[22] = new Vector3(-1, 2, 0);
            grid.Vertices[23] = new Vector3(-1, 1, 0);
            grid.Vertices[24] = new Vector3(-1, 0, 0);
            grid.Vertices[25] = new Vector3(-1, -1, 0);
            grid.Vertices[26] = new Vector3(-1, -2, 0);
            grid.Vertices[27] = new Vector3(-1, -3, 0);

            grid.Faces[0] = new Face { A = 0, B = 27, C = 6 };
            grid.Faces[1] = new Face { A = 27, B = 0, C = 21 };


            mera.Position = new Vector3(0, 0, 10.0f);
            mera.Target = Vector3.Zero;

            objparser.LoadObj("scene.obj");
            
            for(int i= 110; i < objparser.VertexList.Count; i++)
            {
                cone.Vertices[i - 110] = new Vector3((float)objparser.VertexList[i].X/10,(float) objparser.VertexList[i].Y/10, (float)objparser.VertexList[i].Z/10);
            }
            for (int i = 0; i < 110; i++)
            {
                cylinder.Vertices[i] = new Vector3((float)objparser.VertexList[i].X / 10, (float)objparser.VertexList[i].Y / 10, (float)objparser.VertexList[i].Z / 10);
            }

            // Registering to the XAML rendering loop
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            
        }

        // Rendering loop handler
        protected void CompositionTarget_Rendering(object sender, object e)
        {
           
            device.Clear(0, 0, 0, 255);

            // rotating slightly the cube during each frame rendered
            mesh.Rotation = new Vector3(mesh.Rotation.X + 0.001f, mesh.Rotation.Y + 0.001f, mesh.Rotation.Z);
            grid.Rotation = new Vector3(mesh.Rotation.X + 0.001f, mesh.Rotation.Y + 0.001f, mesh.Rotation.Z);
            cone.Rotation = new Vector3(mesh.Rotation.X + 0.001f, mesh.Rotation.Y + 0.001f, mesh.Rotation.Z);
            cylinder.Rotation = new Vector3(mesh.Rotation.X + 0.001f, mesh.Rotation.Y + 0.001f, mesh.Rotation.Z);
            // Doing the various matrix operations
            device.RenderWireframe(mera, mesh);
            device.RenderWireframe(mera, grid);
            device.RenderPoints(mera, cone);
            device.RenderPoints(mera, cylinder);
            // Flushing the back buffer into the front buffer
            device.Present();
            
            
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
