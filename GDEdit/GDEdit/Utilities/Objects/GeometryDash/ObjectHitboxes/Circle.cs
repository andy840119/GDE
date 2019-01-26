using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Objects.General;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a circular hitbox.</summary>
    public class Circle : HitboxType, IHasRadius
    {
        /// <summary>The radius of the circular hitbox.</summary>
        public double Radius { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Circle"/> class.</summary>
        /// <param name="radius">The radius of the circular hitbox.</param>
        public Circle(double radius)
            : base()
        {
            Radius = radius;
        }

        /// <summary>Returns the distance between the center of the circular hitbox and its edge. The rotation parameter is ignored for obvious mathematical reasons.</summary>
        /// <param name="rotation">A useless parameter.</param>
        public override double GetRadiusAtRotation(double rotation) => Radius;

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        public override bool IsPointWithinHitbox(Point point, Point hitboxCenter) => point.DistanceFrom(hitboxCenter) <= Radius;
    }
}
