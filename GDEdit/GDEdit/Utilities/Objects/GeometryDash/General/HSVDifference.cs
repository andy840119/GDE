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
        private SVAdjustmentMode saturationMode;
        private SVAdjustmentMode brightnessMode;

        /// <summary>The hue of the HSV adjustment.</summary>
        public readonly BoundedDouble Hue;
        /// <summary>The saturation of the HSV adjustment.</summary>
        public readonly BoundedDouble Saturation;
        /// <summary>The brightness of the HSV adjustment.</summary>
        public readonly BoundedDouble Brightness;
        /// <summary>The adjustment mode of the saturation of the HSV adjustment.</summary>
        public SVAdjustmentMode SaturationMode
        {
            get => saturationMode;
            set
            {
                Saturation.Min = -(int)value;
                Saturation.Max = 2 - (int)value;
                saturationMode = value;
            }
        }
        /// <summary>The adjustment mode of the brightness of the HSV adjustment.</summary>
        public SVAdjustmentMode BrightnessMode
        {
            get => brightnessMode;
            set
            {
                Brightness.Min = -(int)value;
                Brightness.Max = 2 - (int)value;
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
            Hue = new BoundedDouble(hue, -180, 180);
            Saturation = new BoundedDouble(saturation, -(int)saturationMode, 2 - (int)saturationMode);
            Brightness = new BoundedDouble(brightness, -(int)brightnessMode, 2 - (int)brightnessMode);
            SaturationMode = saturationMode;
            BrightnessMode = brightnessMode;
        }

        public override string ToString() => $"{Hue.Value}a{Saturation.Value}a{Brightness.Value}a{(int)SaturationMode}a{(int)BrightnessMode}";
    }

    /// <summary>Represents the mode of the HSV adjustment for the saturation and the brightness properties.</summary>
    public enum SVAdjustmentMode
    {
        Multiplicative = 0,
        Additive = 1,
    }
}
