using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.ObjectHitboxes
{
    /// <summary>Represents a slope platform object hitbox.</summary>
    public class SlopePlatform : Hitbox
    {
        /// <summary>The hitbox type of the object.</summary>
        public override HitboxType HitboxType => HitboxType.Slope;

        /// <summary>Initializes a new instance of the <seealso cref="SlopePlatform"/> class.</summary>
        public SlopePlatform() : base() { }
    }
}
