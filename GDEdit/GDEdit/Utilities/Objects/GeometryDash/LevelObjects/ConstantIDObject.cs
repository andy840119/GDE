using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents an object whose object ID is constant.</summary>
    public abstract class ConstantIDObject : GeneralObject
    {
        /// <summary>The Object ID of the object.</summary>
        // IMPORTANT: If we want to change the object IDs of objects through some function, this has to be reworked
        [ObjectStringMappable(ObjectParameter.ID)]
        public new abstract int ObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ConstantIDObject"/> class.</summary>
        public ConstantIDObject()
        {
            base.ObjectID = ObjectID;
        }
    }
}
