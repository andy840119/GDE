using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a slope hitbox.</summary>
    public class Slope : HitboxType, IHasWidth, IHasHeight
    {
        /// <summary>The width of the circular hitbox.</summary>
        public double Width { get; set; }
        /// <summary>The height of the circular hitbox.</summary>
        public double Height { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="both">The length of both dimensions of the circular hitbox.</param>
        public Slope(double both) : this(both, both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="width">The width of the circular hitbox.</param>
        /// <param name="height">The height of the circular hitbox.</param>
        public Slope(double width, double height)
            : base()
        {
            Width = width;
            Height = height;
        }
    }
}
