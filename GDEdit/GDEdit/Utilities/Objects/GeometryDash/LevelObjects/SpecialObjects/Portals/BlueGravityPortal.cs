using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue gravity portal.</summary>
    public class BlueGravityPortal : Portal
    {
        /// <summary>The object ID of the blue gravity portal.</summary>
        public override int ObjectID => (int)PortalType.BlueGravity;

        /// <summary>Initializes a new instance of the <seealso cref="BlueGravityPortal"/> class.</summary>
        public BlueGravityPortal() : base() { }
    }
}
