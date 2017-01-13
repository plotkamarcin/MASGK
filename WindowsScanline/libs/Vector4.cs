using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WindowsScanline
{

    public struct Vector4 
    {
       
        public static readonly Vector4 Zero = new Vector4();
        public static readonly Vector4 UnitX = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        public static readonly Vector4 UnitY = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
        public static readonly Vector4 UnitZ = new Vector4(0.0f, 0.0f, 1.0f, 0.0f);
        public static readonly Vector4 UnitW = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

        public static readonly Vector4 One = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }
        public float LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

 
        public void Normalize()
        {
            float length = Length();
            if (!MathUtil.IsZero(length))
            {
                float inverse = 1.0f / length;
                X *= inverse;
                Y *= inverse;
                Z *= inverse;
                W *= inverse;
            }
        }

 
        public static void Normalize(ref Vector4 value, out Vector4 result)
        {
            Vector4 temp = value;
            result = temp;
            result.Normalize();
        }
        public static Vector4 Normalize(Vector4 value)
        {
            value.Normalize();
            return value;
        }

    }
}