using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WindowsScanline
{
    public struct Color4
    {
       
        public static readonly Color4 Black = new Color4(0.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color4 White = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;


        public Color4(float red, float green, float blue, float alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public static Color4 operator *(float scale, Color4 value)
        {
            return new Color4(value.Red * scale, value.Green * scale, value.Blue * scale, value.Alpha * scale);
        }
        public static Color4 operator *(Color4 value, float scale)
        {
            return new Color4(value.Red * scale, value.Green * scale, value.Blue * scale, value.Alpha * scale);
        }
    }
}