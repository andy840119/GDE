using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.EditorHelper
{
    // TODO: Find a better naming for these
    /// <summary>Represents an object which contains information about the objects to create from an original object that are the edge decoration in the object set.</summary>
    public class EdgeCreation
    {
        /// <summary>The object ID of the original object which will be used to create the edge decoration object from.</summary>
        public int OriginalObjectID { get; set; }

        /// <summary>The object IDs of the created object.</summary>
        public Directional<int> CreatedObjectIDs { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="EdgeCreation"/> class.</summary>
        /// <param name="originalObjectID">The original object ID.</param>
        /// <param name="createdObjectIDs">The created object IDs.</param>
        public EdgeCreation(int originalObjectID, Directional<int> createdObjectIDs)
        {
            OriginalObjectID = originalObjectID;
            CreatedObjectIDs = createdObjectIDs;
        }
    }
}
