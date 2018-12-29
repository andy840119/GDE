using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a fast speed portal in the game.</summary>
    public class FastSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the fast speed portal</summary>
        public override int ObjectID => (int)PortalType.FastSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Fast;

        /// <summary>Initializes a new instance of the <seealso cref="FastSpeedPortal"/> class.</summary>
        public FastSpeedPortal() : base() { }
    }
}
