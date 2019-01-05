using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a green size portal.</summary>
    public class GreenSizePortal : Portal
    {
        /// <summary>The object ID of the green size portal.</summary>
        public override short ObjectID => (short)(int)PortalType.GreenSize;

        /// <summary>Initializes a new instance of the <seealso cref="GreenSizePortal"/> class.</summary>
        public GreenSizePortal() : base() { }
    }
}
