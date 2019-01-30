using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow dual portal.</summary>
    public class YellowDualPortal : Portal
    {
        /// <summary>The object ID of the yellow dual portal.</summary>
        public override int ObjectID => (int)PortalType.YellowDual;

        /// <summary>Initializes a new instance of the <seealso cref="YellowDualPortal"/> class.</summary>
        public YellowDualPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowDualPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowDualPortal());
    }
}
