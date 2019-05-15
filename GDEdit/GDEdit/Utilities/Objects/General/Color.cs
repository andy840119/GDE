using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General
{
    /// <summary>Represents an ARGB color.</summary>
    public struct Color
    {
        private ColorValue r, g, b, a;

        /// <summary>The red value of the color.</summary>
        public float R
        {
            get => r;
            set => r = value;
        }
        /// <summary>The green value of the color.</summary>
        public float G
        {
            get => g;
            set => g = value;
        }
        /// <summary>The blue value of the color.</summary>
        public float B
        {
            get => b;
            set => b = value;
        }
        /// <summary>The alpha value of the color.</summary>
        public float A
        {
            get => a;
            set => a = value;
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> class.</summary>
        /// <param name="all">The value for all the color values (R, G, B, A).</param>
        public Color(float all) : this(all, all, all, all) { }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> class.</summary>
        /// <param name="red">The red value of the color.</param>
        /// <param name="green">The green value of the color.</param>
        /// <param name="blue">The blue value of the color.</param>
        /// <param name="alpha">The alpha value of the color.</param>
        public Color(float red, float green, float blue, float alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }

        public static bool operator ==(Color left, Color right) => left.r == right.r && left.g == right.g && left.b == right.b && left.a == right.a;
        public static bool operator !=(Color left, Color right) => left.r != right.r && left.g != right.g && left.b != right.b && left.a != right.a;

        private struct ColorValue
        {
            private float v;

            public float Value
            {
                get => v;
                set
                {
                    if (value < 0 || value > 1)
                        throw new ArgumentException("The color value cannot be outside the range [0, 1].");
                    v = value;
                }
            }

            public ColorValue(float value)
            {
                v = value;
                Value = v;
            }

            public static implicit operator float(ColorValue v) => v.v;
            public static implicit operator ColorValue(float f) => new ColorValue(f);

            public static bool operator ==(ColorValue left, ColorValue right) => left.v == right.v;
            public static bool operator !=(ColorValue left, ColorValue right) => left.v != right.v;
        }
    }
}
