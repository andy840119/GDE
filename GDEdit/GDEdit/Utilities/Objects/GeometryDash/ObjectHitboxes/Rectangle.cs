using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a rectangular hitbox.</summary>
    public class Rectangle : WidthHeightHitbox, IHasWidth, IHasHeight
    {
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="both">The length of both dimensions of the rectangular hitbox.</param>
        public Rectangle(double both) : base(both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="width">The width of the rectangular hitbox.</param>
        /// <param name="height">The height of the rectangular hitbox.</param>
        public Rectangle(double width, double height) : base(width, height) { }
    }
}
