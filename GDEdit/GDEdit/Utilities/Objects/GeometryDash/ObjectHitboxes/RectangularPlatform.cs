using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.ObjectHitboxes
{
    /// <summary>Represents a rectangular platform object hitbox.</summary>
    public class RectangularPlatform : Hitbox
    {
        /// <summary>The hitbox type of the object.</summary>
        public override HitboxType HitboxType => HitboxType.Rectangular;

        /// <summary>Initializes a new instance of the <seealso cref="RectangularPlatform"/> class.</summary>
        public RectangularPlatform() : base() { }
    }
}
