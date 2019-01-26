using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a hitbox type.</summary>
    public abstract class HitboxType
    {
        /// <summary>Initializes a new instance of the <seealso cref="HitboxType"/> class.</summary>
        public HitboxType() { }

        /// <summary>Returns the distance between the center of the hitbox and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public abstract double GetRadiusAtRotation(double rotation);

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        public abstract bool IsPointWithinHitbox(Point point, Point hitboxCenter);

        /// <summary>Determines whether a point is within the hitbox.</summary>
        /// <param name="point">The point's location.</param>
        /// <param name="hitboxCenter">The hitbox's center.</param>
        /// <param name="hitboxRotation">The rotation of the hitbox.</param>
        public bool IsPointWithinHitbox(Point point, Point hitboxCenter, double hitboxRotation) => IsPointWithinHitbox(point, hitboxCenter.Rotate(point, hitboxRotation));
    }
}
