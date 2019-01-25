using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.ObjectHitboxes
{
    /// <summary>Represents an object hitbox.</summary>
    public abstract class Hitbox
    {
        /// <summary>The hitbox type of the object.</summary>
        public abstract HitboxType HitboxType { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        public Hitbox() { }
    }

    /// <summary>Represents the type of the hitbox.</summary>
    public enum HitboxType
    {
        /// <summary>The rectangular hitbox type.</summary>
        Rectangular,
        /// <summary>The slope hitbox type.</summary>
        Slope,
    }
}
