using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.General
{
    /// <summary>Represents the HSV adjustment in an object's color or a trigger's copied color HSV adjustment.</summary>
    public class HSVAdjustment
    {
        private H h;
        private SV s, v;
        private SVAdjustmentMode saturationMode;
        private SVAdjustmentMode brightnessMode;
        
        /// <summary>The hue of the HSV adjustment.</summary>
        public double Hue
        {
            get => h.Value;
            set => h.Value = (short)value;
        }
        /// <summary>The saturation of the HSV adjustment.</summary>
        public double Saturation
        {
            get => s.Value;
            set => s.Value = (float)value;
        }
        /// <summary>The brightness of the HSV adjustment.</summary>
        public double Brightness
        {
            get => v.Value;
            set => v.Value = (float)value;
        }
        /// <summary>The adjustment mode of the saturation of the HSV adjustment.</summary>
        public SVAdjustmentMode SaturationMode
        {
            get => saturationMode;
            set
            {
                s = SV.GetSVFromMode(value, s.Value);
                saturationMode = value;
            }
        }
        /// <summary>The adjustment mode of the brightness of the HSV adjustment.</summary>
        public SVAdjustmentMode BrightnessMode
        {
            get => brightnessMode;
            set
            {
                v = SV.GetSVFromMode(value, v.Value);
                brightnessMode = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="HSVAdjustment"/> class.</summary>
        /// <param name="hue">The hue of the HSV adjustment.</param>
        /// <param name="saturation">The saturation of the HSV adjustment.</param>
        /// <param name="brightness">The brightness of the HSV adjustment.</param>
        /// <param name="saturationMode">The adjustment mode of the saturation of the HSV adjustment.</param>
        /// <param name="brightnessMode">The adjustment mode of the brightness of the HSV adjustment.</param>
        public HSVAdjustment(double hue, double saturation, double brightness, SVAdjustmentMode saturationMode, SVAdjustmentMode brightnessMode)
        {
            h = new H((short)hue);
            s = SV.GetSVFromMode(SaturationMode = saturationMode, (float)saturation);
            v = SV.GetSVFromMode(BrightnessMode = saturationMode, (float)brightness);
        }

        public override string ToString() => $"{Hue}a{Saturation}a{Brightness}a{(int)SaturationMode}a{(int)BrightnessMode}";

        // The gay code below is unfortunately necessary
        #region Bullshit
        private abstract class BoundedValue<T>
        {
            private T v;

            /// <summary>The minimum value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public abstract T Min { get; }
            /// <summary>The maximum value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public abstract T Max { get; }
            /// <summary>The value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public T Value
            {
                get => v;
                set => v = value = Clamp(value);
            }

            /// <summary>Initializes a new instance of the <seealso cref="BoundedValue{T}"/> class.</summary>
            /// <param name="value">The value of the <seealso cref="BoundedValue{T}"/>.</param>
            /// <param name="min">The minimum value of the <seealso cref="BoundedValue{T}"/>.</param>
            /// <param name="max">The maximum value of the <seealso cref="BoundedValue{T}"/>.</param>
            public BoundedValue(T value)
            {
                Value = value;
            }

            public static implicit operator T(BoundedValue<T> b) => b.Value;

            /// <summary>Clamps the value between the two boundaries and returns the clamped result.</summary>
            /// <param name="value">The value to clamp.</param>
            protected abstract T Clamp(T value);
        }
        private abstract class BoundedFloat : BoundedValue<float>
        {
            public BoundedFloat(float value) : base(value) { }

            protected override float Clamp(float value)
            {
                if (value < Min)
                    value = Min;
                if (value > Max)
                    value = Max;
                return value;
            }
        }
        private abstract class BoundedShort : BoundedValue<short>
        {
            public BoundedShort(short value) : base(value) { }

            protected override short Clamp(short value)
            {
                if (value < Min)
                    value = Min;
                if (value > Max)
                    value = Max;
                return value;
            }
        }
        private class H : BoundedShort
        {
            public override short Min => -180;
            public override short Max => 180;

            public H(short value) : base(value) { }
        }
        private abstract class SV : BoundedFloat
        {
            protected abstract float BoundaryOffset { get; }

            public override float Min => 0 + BoundaryOffset;
            public override float Max => 2 + BoundaryOffset;

            public SV(float value) : base(value) { }

            public static SV GetSVFromMode(SVAdjustmentMode mode, float value)
            {
                switch (mode)
                {
                    case SVAdjustmentMode.Multiplicative:
                        return new UncheckedSV(value);
                    case SVAdjustmentMode.Additive:
                        return new CheckedSV(value);
                    default:
                        throw new Exception("The SV adjustment mode seriously cannot be anything other than those two, what have you done?");
                }
            }
        }
        private class UncheckedSV : SV
        {
            protected override float BoundaryOffset => 0;

            public UncheckedSV(float value) : base(value) { }
        }
        private class CheckedSV : SV
        {
            protected override float BoundaryOffset => -1;

            public CheckedSV(float value) : base(value) { }
        }
        #endregion
    }

    /// <summary>Represents the mode of the HSV adjustment for the saturation and the brightness properties.</summary>
    public enum SVAdjustmentMode : byte
    {
        Multiplicative = 0,
        Additive = 1,
    }
}
