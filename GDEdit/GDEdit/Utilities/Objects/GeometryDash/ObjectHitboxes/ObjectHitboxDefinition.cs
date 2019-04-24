using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Contains information about the object IDs that use this type of hitbox.</summary>
    public class ObjectHitboxDefinition
    {
        /// <summary>The object IDs that this hitbox is valid for.</summary>
        public List<int> ObjectIDs { get; set; }
        /// <summary>The hitbox that is valid for the object IDs.</summary>
        public Hitbox Hitbox { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectHitboxDefinition"/> class.</summary>
        /// <param name="objectID">The object ID that this hitbox is valid for.</param>
        /// <param name="hitbox">The hitbox of the object IDs.</param>
        public ObjectHitboxDefinition(int objectID, Hitbox hitbox) : this(new List<int> { objectID }, hitbox) { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectHitboxDefinition"/> class.</summary>
        /// <param name="objectIDs">The object IDs that this hitbox is valid for.</param>
        /// <param name="hitbox">The hitbox of the object IDs.</param>
        public ObjectHitboxDefinition(List<int> objectIDs, Hitbox hitbox)
        {
            ObjectIDs = objectIDs;
            Hitbox = hitbox;
        }
    }
}
