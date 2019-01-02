using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a green magenta portal.</summary>
    public class MagentaSizePortal : Portal
    {
        /// <summary>The object ID of the magenta size portal.</summary>
        public override int ObjectID => (int)PortalType.MagentaSize;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaSizePortal"/> class.</summary>
        public MagentaSizePortal() : base() { }
    }
}
