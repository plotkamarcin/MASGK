
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WindowsScanline
{

    public struct Vector2 
    {

        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 UnitX = new Vector2(1.0f, 0.0f);
        public static readonly Vector2 UnitY = new Vector2(0.0f, 1.0f);
        public static readonly Vector2 One = new Vector2(1.0f, 1.0f);


        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public void Normalize()
        {
            float length = Length();
            if (!MathUtil.IsZero(length))
            {
                float inv = 1.0f / length;
                X *= inv;
                Y *= inv;
            }
        }

        public static void Dot(ref Vector2 left, ref Vector2 right, out float result)
        {
            result = (left.X * right.X) + (left.Y * right.Y);
        }

        public static float Dot(Vector2 left, Vector2 right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            result = value;
            result.Normalize();
        }

        public static Vector2 Normalize(Vector2 value)
        {
            value.Normalize();
            return value;
        }


        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }


        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator -(Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }

        public static Vector2 operator *(float scale, Vector2 value)
        {
            return new Vector2(value.X * scale, value.Y * scale);
        }

        public static Vector2 operator *(Vector2 value, float scale)
        {
            return new Vector2(value.X * scale, value.Y * scale);
        }

        public static Vector2 operator /(Vector2 value, float scale)
        {
            return new Vector2(value.X / scale, value.Y / scale);
        }

        public static Vector2 operator /(float scale, Vector2 value)
        {
            return new Vector2(scale / value.X, scale / value.Y);
        }

        public static Vector2 operator /(Vector2 value, Vector2 scale)
        {
            return new Vector2(value.X / scale.X, value.Y / scale.Y);
        }

        public static Vector2 operator +(Vector2 value, float scalar)
        {
            return new Vector2(value.X + scalar, value.Y + scalar);
        }

        public static Vector2 operator +(float scalar, Vector2 value)
        {
            return new Vector2(scalar + value.X, scalar + value.Y);
        }

        public static Vector2 operator -(Vector2 value, float scalar)
        {
            return new Vector2(value.X - scalar, value.Y - scalar);
        }

        public static Vector2 operator -(float scalar, Vector2 value)
        {
            return new Vector2(scalar - value.X, scalar - value.Y);
        }
    }
}
