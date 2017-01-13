using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WindowsScanline
{
    public struct Vector3
    {

        public static readonly Vector3 Zero = new Vector3();
        public static readonly Vector3 UnitX = new Vector3(1.0f, 0.0f, 0.0f);
        public static readonly Vector3 UnitY = new Vector3(0.0f, 1.0f, 0.0f);
        public static readonly Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1.0f);
        public static readonly Vector3 One = new Vector3(1.0f, 1.0f, 1.0f);

        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(Vector2 value, float z)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
        }

 
        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

        public float LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

        public void Normalize()
        {
            float length = Length();
            if (!MathUtil.IsZero(length))
            {
                float inv = 1.0f / length;
                X *= inv;
                Y *= inv;
                Z *= inv;
            }
        }

 // basic math
        public static void Add(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
    
        public static void Subtract(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
 

        public static void Multiply(ref Vector3 value, float scale, out Vector3 result)
        {
            result = new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static void Multiply(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

      
        public static void Cross(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(
                (left.Y * right.Z) - (left.Z * right.Y),
                (left.Z * right.X) - (left.X * right.Z),
                (left.X * right.Y) - (left.Y * right.X));
        }
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            Vector3 result;
            Cross(ref left, ref right, out result);
            return result;
        }

   
        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            float z = value1.Z - value2.Z;

            result = (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public static float Distance(Vector3 value1, Vector3 value2)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            float z = value1.Z - value2.Z;

            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

  
        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            float z = value1.Z - value2.Z;

            result = (x * x) + (y * y) + (z * z);
        }
        public static float DistanceSquared(Vector3 value1, Vector3 value2)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            float z = value1.Z - value2.Z;

            return (x * x) + (y * y) + (z * z);
        }

 
        public static void Dot(ref Vector3 left, ref Vector3 right, out float result)
        {
            result = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }
        public static float Dot(Vector3 left, Vector3 right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }

        public static void Normalize(ref Vector3 value, out Vector3 result)
        {
            result = value;
            result.Normalize();
        }

        public static Vector3 Normalize(Vector3 value)
        {
            value.Normalize();
            return value;
        }


        public static void TransformCoordinate(ref Vector3 coordinate, ref Matrix transform, out Vector3 result)
        {
            Vector4 vector = new Vector4();
            vector.X = (coordinate.X * transform.M11) + (coordinate.Y * transform.M21) + (coordinate.Z * transform.M31) + transform.M41;
            vector.Y = (coordinate.X * transform.M12) + (coordinate.Y * transform.M22) + (coordinate.Z * transform.M32) + transform.M42;
            vector.Z = (coordinate.X * transform.M13) + (coordinate.Y * transform.M23) + (coordinate.Z * transform.M33) + transform.M43;
            vector.W = 1f / ((coordinate.X * transform.M14) + (coordinate.Y * transform.M24) + (coordinate.Z * transform.M34) + transform.M44);

            result = new Vector3(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W);
        }

        public static Vector3 TransformCoordinate(Vector3 coordinate, Matrix transform)
        {
            Vector3 result;
            TransformCoordinate(ref coordinate, ref transform, out result);
            return result;
        }


        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }


        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator -(Vector3 value)
        {
            return new Vector3(-value.X, -value.Y, -value.Z);
        }

        public static Vector3 operator *(float scale, Vector3 value)
        {
            return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static Vector3 operator *(Vector3 value, float scale)
        {
            return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static Vector3 operator /(Vector3 value, float scale)
        {
            return new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
        }

        public static Vector3 operator /(float scale, Vector3 value)
        {
            return new Vector3(scale / value.X, scale / value.Y, scale / value.Z);
        }


        public static Vector3 operator /(Vector3 value, Vector3 scale)
        {
            return new Vector3(value.X / scale.X, value.Y / scale.Y, value.Z / scale.Z);
        }


        public static Vector3 operator +(Vector3 value, float scalar)
        {
            return new Vector3(value.X + scalar, value.Y + scalar, value.Z + scalar);
        }

        public static Vector3 operator +(float scalar, Vector3 value)
        {
            return new Vector3(scalar + value.X, scalar + value.Y, scalar + value.Z);
        }

        public static Vector3 operator -(Vector3 value, float scalar)
        {
            return new Vector3(value.X - scalar, value.Y - scalar, value.Z - scalar);
        }

        public static Vector3 operator -(float scalar, Vector3 value)
        {
            return new Vector3(scalar - value.X, scalar - value.Y, scalar - value.Z);
        }
    }
}
