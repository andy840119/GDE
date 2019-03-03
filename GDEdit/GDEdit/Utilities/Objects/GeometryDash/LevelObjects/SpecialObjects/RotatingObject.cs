using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
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
        /// <summary>The valid object IDs of the special object.</summary>
        protected override int[] ValidObjectIDs => ObjectLists.RotatingObjectList;
        /// <summary>The name as a string of the special object.</summary>
        protected override string SpecialObjectTypeName => "rotating object";

        /// <summary>Represents the Disable Rotation property of the rotating object.</summary>
        [ObjectStringMappable(ObjectParameter.DisableRotation)]
        public bool DisableRotation
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="RotatingObject"/> class. For internal use only.</summary>
        private RotatingObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="RotatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        public RotatingObject(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="RotatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public RotatingObject(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="RotatingObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RotatingObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RotatingObject;
            c.DisableRotation = DisableRotation;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
