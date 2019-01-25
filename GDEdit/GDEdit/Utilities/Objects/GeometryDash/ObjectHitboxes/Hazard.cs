using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.ObjectHitboxes
{
    /// <summary>Represents a hazardous object hitbox.</summary>
    public class Hazard : Hitbox
    {
        /// <summary>The hitbox type of the object.</summary>
        public override HitboxType HitboxType => HitboxType.Rectangular;

        /// <summary>Initializes a new instance of the <seealso cref="Hazard"/> class.</summary>
        public Hazard() : base() { }
    }
}
