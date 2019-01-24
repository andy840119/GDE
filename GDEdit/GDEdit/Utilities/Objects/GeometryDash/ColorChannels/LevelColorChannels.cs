using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.ColorChannels
{
    /// <summary>Represents the color channels of a level.</summary>
    public class LevelColorChannels
    {
        private ColorChannel[] colors = new ColorChannel[5000];

        /// <summary>Gets or sets the color at the specified color channel ID.</summary>
        /// <param name="colorID">The color channel ID whose color to get or set.</param>
        public ColorChannel this[int colorID]
        {
            get => colors[colorID];
            set => colors[colorID] = value;
        }
        /// <summary>Gets or sets the color at the specified special color channel ID.</summary>
        /// <param name="colorID">The special color channel ID whose color to get or set.</param>
        public ColorChannel this[SpecialColorID colorID]
        {
            get => colors[(int)colorID];
            set => colors[(int)colorID] = value;
        }
    }
}
