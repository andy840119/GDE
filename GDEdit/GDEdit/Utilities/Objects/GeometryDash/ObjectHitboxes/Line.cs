using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a line hitbox.</summary>
    public class Line : HitboxType
    {
        /// <summary>The rotation of the line in degrees.</summary>
        public double Rotation { get; }
        /// <summary>Gets the slope ratio of this line hitbox.</summary>
        public double SlopeRatio => Math.Tan(Rotation * Math.PI / 180);

        /// <summary>Initializes a new instance of the <seealso cref="Line"/> class.</summary>
        /// <param name="rotation">The rotation of the line.</param>
        public Line(double rotation)
            : base()
        {
            Rotation = rotation;
        }

        /// <summary>Determines whether a point is within the hitbox (assuming the center of the hitbox is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool IsPointWithinHitbox(Point point) => point.Y == SlopeRatio * point.X;

        /// <summary>Returns the distance between the center of the hitbox and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public override double GetRadiusAtRotation(double rotation) => rotation % 180 == Rotation % 180 ? double.PositiveInfinity : 0;
        /// <summary>Returns the maximum distance between the center of the hitbox and its edge.</summary>
        public override double GetMaxRadius() => double.PositiveInfinity;
    }
}
