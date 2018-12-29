using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Information.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a rotating object with rotating properties.</summary>
    public class RotatingObject : SpecialObject
    {
        /// <summary>Represents the Disable Rotation property of the rotating object.</summary>
        [ObjectStringMappable(ObjectParameter.DisableRotation)]
        public bool DisableRotation;

        /// <summary>Initializes a new instance of the <seealso cref="RotatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public RotatingObject(int objectID, double x, double y)
            : base(objectID, x, y)
        {
            if (!ObjectLists.RotatingObjectList.Contains(objectID))
                throw new InvalidCastException("This object ID does not represent a valid rotating object.");
        }
    }
}
