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
        Mesh mesh = new Mesh("Cube", 8);
        Mesh grid = new Mesh("Grid", 28);
        Camera mera = new Camera();

        
        
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
            mesh.Vertices[3] = new Vector3(-1, -1, -1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, 1);
            mesh.Vertices[7] = new Vector3(1, -1, -1);

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


            mera.Position = new Vector3(0, 0, 10.0f);
            mera.Target = Vector3.Zero;

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
            // Doing the various matrix operations
            device.Render(mera, mesh);
            device.Render(mera, grid);
            // Flushing the back buffer into the front buffer
            device.Present();
            
            
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
