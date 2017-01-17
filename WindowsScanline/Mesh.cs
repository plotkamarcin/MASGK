// Mesh.cs

namespace WindowsScanline
{

    // Represents mesh of an object
    public class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Face[] Faces { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Mesh(string name, int verticesCount, int facesCount)
        {
            Vertices = new Vertex[verticesCount];
            Faces = new Face[facesCount];
            Name = name;
        }

        public Mesh()
        {
        }
        public struct Vertex
        {
            public Vector3 Normal;
            public Vector3 Coordinates;
            public Vector3 WorldCoordinates;
        }

        public void TranslateMesh(double x, double y, double z)
        {
            for(int i=0;i<Vertices.Length;i++)
            {
                Vertices[i].Coordinates.X += (float)x;
                Vertices[i].Coordinates.Y += (float)y;
                Vertices[i].Coordinates.Z += (float)z;
            }
        }
        public void ScaleMesh(double x, double y, double z)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Coordinates.X *= (float)x;
                Vertices[i].Coordinates.Y *= (float)y;
                Vertices[i].Coordinates.Z *= (float)z;
            }
        }
    }
}
