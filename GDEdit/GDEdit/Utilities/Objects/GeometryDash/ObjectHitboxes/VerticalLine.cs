using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a vertical line hitbox.</summary>
    public class VerticalLine : HitboxType
    {
        /// <summary>Initializes a new instance of the <seealso cref="VerticalLine"/> class.</summary>
        public VerticalLine() : base() { }

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        public override bool IsPointWithinHitbox(Point point, Point hitboxCenter) => point.X == hitboxCenter.X;
    }
}
