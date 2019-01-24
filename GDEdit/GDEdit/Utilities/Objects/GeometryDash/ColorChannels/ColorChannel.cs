using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with the default values.</summary>
        public ColorChannel(int colorChannelID) : this(colorChannelID, 255, 255, 255) { }

        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with a specified color.</summary>
        public ColorChannel(int colorChannelID, int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
