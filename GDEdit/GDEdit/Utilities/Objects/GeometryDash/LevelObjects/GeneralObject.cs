using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects
{
    // TODO: Remove LevelObject entirely once completely migrated to this
    /// <summary>Represents a general object.</summary>
    public class GeneralObject
    {
        /// <summary>The Object ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public int ObjectID;
        /// <summary>The X position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.X)]
        public float X;
        /// <summary>The Y position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Y)]
        public float Y;
        /// <summary>Determines whether this object is flipped horizontally or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedHorizontally)]
        public bool FlippedHorizontally;
        /// <summary>Determines whether this object is flipped vertically or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedVertically)]
        public bool FlippedVertically;
        /// <summary>The rotation of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Rotation)]
        public float Rotation;
        /// <summary>The Editor Layer 1 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL1)]
        public int EL1;
        /// <summary>The Editor Layer 2 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL2)]
        public int EL2;
        /// <summary>The Z Layer of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZLayer)]
        public int ZLayer;
        /// <summary>The Z Order of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZOrder)]
        public int ZOrder;
        /// <summary>The linked group ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.LinkedGroupID)]
        public int LinkedGroupID;
        /// <summary>Determines whether this object is the group parent or not.</summary>
        [ObjectStringMappable(ObjectParameter.GroupParent)]
        public bool GroupParent;
        /// <summary>Determines whether this object is for high detail or not.</summary>
        [ObjectStringMappable(ObjectParameter.HighDetail)]
        public bool HighDetail;
        /// <summary>Determines whether this object should have an entrance effect or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontEnter)]
        public bool DontEnter;
        /// <summary>Determines whether this object should have the fade in and out disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontFade)]
        public bool DontFade;
        /// <summary>Determines whether this object should have its glow disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DisableGlow)]
        public bool DisableGlow;

        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class with the object ID parameter set to 1.</summary>
        public GeneralObject()
        {
            ObjectID = 1;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID)
        {
            ObjectID = objectID;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, float x, float y)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="rotation">The rotation of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, float x, float y, float rotation)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
            Rotation = rotation;
        }
    }
}
