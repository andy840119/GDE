using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents an object hitbox.</summary>
    public class Hitbox
    {
        /// <summary>The hitbox type of the hitbox.</summary>
        public HitboxType HitboxType { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        /// <param name="type">The hitbox type of the hitbox.</param>
        public Hitbox(HitboxType type)
        {
            HitboxType = type;
        }
    }
}
