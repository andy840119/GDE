using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow gravity portal.</summary>
    public class YellowGravityPortal : Portal
    {
        /// <summary>The object ID of the yellow gravity portal.</summary>
        public override int ObjectID => (int)PortalType.YellowGravity;
    }
}
