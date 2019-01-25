using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents an object hitbox.</summary>
    public abstract class Hitbox
    {
        /// <summary>The hitbox type of the object.</summary>
        public abstract HitboxType HitboxType { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        public Hitbox() { }
    }
}
