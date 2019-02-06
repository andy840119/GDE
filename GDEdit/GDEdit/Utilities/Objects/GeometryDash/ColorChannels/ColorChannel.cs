using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace GDEdit.Utilities.Objects.GeometryDash.ColorChannels
{
    /// <summary>Represents a color channel in a level.</summary>
    public class ColorChannel
    {
        /// <summary>The red color value of the <seealso cref="ColorChannel"/>.</summary>
        public int Red { get; set; }
        /// <summary>The green color value of the <seealso cref="ColorChannel"/>.</summary>
        public int Green { get; set; }
        /// <summary>The blue color value of the <seealso cref="ColorChannel"/>.</summary>
        public int Blue { get; set; }
        /// <summary>The copied player color of the <seealso cref="ColorChannel"/>.</summary>
        public ColorChannelPlayerColor CopiedPlayerColor { get; set; }
        /// <summary>The Blending property of the color channel.</summary>
        public bool Blending { get; set; }
        /// <summary>The Color Channel ID of the color channel.</summary>
        public int ColorChannelID { get; set; }
        /// <summary>The opacity of the color channel.</summary>
        public double Opacity { get; set; }
        /// <summary>The Color Channel ID of the copied color channel.</summary>
        public int CopiedColorID { get; set; }
        /// <summary>The HSV adjustment of the copied color channel.</summary>
        public HSVAdjustment CopiedColorHSV { get; set; }
        /// <summary>The Copy Opacity property of the color channel.</summary>
        public bool CopyOpacity { get; set; }

        /// <summary>Initializes a new empty instance of the <seealso cref="ColorChannel"/> class. For private usage only.</summary>
        private ColorChannel() : this(0) { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with the default values.</summary>
        public ColorChannel(int colorChannelID) : this(colorChannelID, 255, 255, 255) { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with a specified color.</summary>
        public ColorChannel(int colorChannelID, int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>Resets this <seealso cref="ColorChannel"/>.</summary>
        public void Reset()
        {
            Red = Green = Blue = 255;
            CopiedPlayerColor = ColorChannelPlayerColor.None;
            Blending = false;
            Opacity = 1;
            CopiedColorID = 0;
            CopiedColorHSV.Reset();
            CopyOpacity = false;
        }
        
        /// <summary>Parses the color channel string into a <seealso cref="ColorChannel"/> object.</summary>
        /// <param name="colorChannel">The color channel string to parse.</param>
        public static ColorChannel Parse(string colorChannel)
        {
            string[] split = colorChannel.Split('_');
            ColorChannel result = new ColorChannel();
            for (int i = 0; i < split.Length; i += 2)
            {
                int key = ToInt32(split[i]);
                string value = split[i + 1];
                switch (key)
                {
                    case 1:
                        result.Red = ToInt32(value);
                        break;
                    case 2:
                        result.Green = ToInt32(value);
                        break;
                    case 3:
                        result.Blue = ToInt32(value);
                        break;
                    case 4:
                        result.CopiedPlayerColor = (ColorChannelPlayerColor)ToInt32(value);
                        break;
                    case 5:
                        result.Blending = value == "1";
                        break;
                    case 6:
                        result.ColorChannelID = ToInt32(value);
                        break;
                    case 7:
                        result.Opacity = ToDouble(value);
                        break;
                    case 9:
                        result.CopiedColorID = ToInt32(value);
                        break;
                    case 10:
                        result.CopiedColorHSV = HSVAdjustment.Parse(value);
                        break;
                    case 17:
                        result.CopyOpacity = value == "1";
                        break;
                    default: // We need to know more about that suspicious new thing so we keep a log of it
                        Directory.CreateDirectory("ulscsk");
                        File.WriteAllText($@"ulscsk\{key}.key", key.ToString());
                        break;
                }
            }
            return result;
        }

        public static bool operator ==(ColorChannel left, ColorChannel right)
        {
            foreach (var p in typeof(ColorChannel).GetProperties())
                if (p.GetValue(left) != p.GetValue(right))
                    return false;
            return true;
        }
        public static bool operator !=(ColorChannel left, ColorChannel right)
        {
            foreach (var p in typeof(ColorChannel).GetProperties())
                if (p.GetValue(left) == p.GetValue(right))
                    return false;
            return true;
        }

        // IMPORTANT: This may need to be changed as more information about the color channel string is discovered (especially for property IDs 8, 11, 12, 13, 15, 18 which are currently hardcoded because of that)
        /// <summary>Returns the string of the <seealso cref="ColorChannel"/>.</summary>
        public override string ToString() => $"1_{Red}_2_{Green}_3_{Blue}_4_{(int)CopiedPlayerColor}_5_{(Blending ? 1 : 0)}_6_{ColorChannelID}_7_{Opacity}_8_1_9_{CopiedColorID}_10_{CopiedColorHSV}_11_255_12_255_13_255_15_1_17_{(CopyOpacity ? 1 : 0)}_18_0";
    }
}
