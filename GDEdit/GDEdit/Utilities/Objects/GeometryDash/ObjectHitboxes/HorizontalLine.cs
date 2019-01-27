using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a horizontal line hitbox.</summary>
    public class HorizontalLine : HitboxType
    {
        /// <summary>Initializes a new instance of the <seealso cref="HorizontalLine"/> class.</summary>
        public HorizontalLine() : base() { }

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        public override bool IsPointWithinHitbox(Point point) => point.Y == 0;

        /// <summary>Returns the maximum distance between the center of the hitbox and its edge.</summary>
        public override double GetMaxRadius() => double.PositiveInfinity;
    }
}
