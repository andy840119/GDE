using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a slope hitbox.</summary>
    public class Slope : WidthHeightHitbox, IHasWidth, IHasHeight
    {
        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="both">The length of both dimensions of the slope hitbox.</param>
        public Slope(double both) : base(both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="width">The width of the slope hitbox.</param>
        /// <param name="height">The height of the slope hitbox.</param>
        public Slope(double width, double height) : base(width, height) { }

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        public override bool IsPointWithinHitbox(Point point, Point hitboxCenter)
        {
            double slope = Height / Width;
            double offset = hitboxCenter.Y - slope * hitboxCenter.X;
            return base.IsPointWithinHitbox(point, hitboxCenter) && (slope * point.X + offset) <= point.Y;
        }
    }
}
