using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a special object.</summary>
    public abstract class SpecialObject : GeneralObject
    {
        /// <summary>The valid object IDs of the special object. Only override if the class does not represent a single object.</summary>
        protected virtual int[] ValidObjectIDs => null;
        protected virtual string SpecialObjectType => "special object";

        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        public SpecialObject() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public SpecialObject(int objectID, double x, double y)
            : base(objectID, x, y)
        {
            // Since this constructor must be only used by general object classes that may have different object IDs, assume the property is overriden
            if (!ValidObjectIDs.Contains(objectID))
                throw new InvalidCastException($"This object ID does not represent a valid {SpecialObjectType}.");
        }
    }
}
