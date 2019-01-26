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
        /// <summary>Gets the slope ratio of this slope hitbox.</summary>
        public double SlopeRatio => Height / Width;

        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="both">The length of both dimensions of the slope hitbox.</param>
        public Slope(double both) : base(both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="width">The width of the slope hitbox.</param>
        /// <param name="height">The height of the slope hitbox.</param>
        public Slope(double width, double height) : base(width, height) { }

        /// <summary>Returns the distance between the center of the hitbox and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public override double GetRadiusAtRotation(double rotation)
        {
            double deg = Math.Atan(SlopeRatio) * 180 * Math.PI;
            if (rotation >= deg && rotation <= (deg + 180))
                return 0;
            return base.GetRadiusAtRotation(rotation);
        }

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        public override bool IsPointWithinHitbox(Point point, Point hitboxCenter)
        {
            double offset = hitboxCenter.Y - SlopeRatio * hitboxCenter.X;
            return base.IsPointWithinHitbox(point, hitboxCenter) && (SlopeRatio * point.X + offset) <= point.Y;
        }
    }
}
