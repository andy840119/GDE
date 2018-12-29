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
        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        public SpecialObject() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public SpecialObject(int objectID, double x, double y)
            : base(objectID, x, y) { }
    }
}
