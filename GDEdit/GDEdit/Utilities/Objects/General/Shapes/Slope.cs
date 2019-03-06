using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a slope shape.</summary>
    public class Slope : WidthHeightShape, IHasWidth, IHasHeight
    {
        /// <summary>Gets the slope ratio of this slope shape.</summary>
        public double SlopeRatio => Height / Width;

        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="both">The length of both dimensions of the slope shape.</param>
        public Slope(double both) : base(both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Slope"/> class.</summary>
        /// <param name="width">The width of the slope shape.</param>
        /// <param name="height">The height of the slope shape.</param>
        public Slope(double width, double height) : base(width, height) { }

        /// <summary>Returns the distance between the center of the shape and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public override double GetRadiusAtRotation(double rotation)
        {
            double deg = Math.Atan(SlopeRatio) * 180 * Math.PI;
            if (rotation >= deg && rotation <= (deg + 180))
                return 0;
            return base.GetRadiusAtRotation(rotation);
        }

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point) => base.ContainsPoint(point) && (SlopeRatio * point.X) <= point.Y;
    }
}
