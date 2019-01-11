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
    /// <summary>Represents a fastest speed portal in the game.</summary>
    public class FastestSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the fastest speed portal.</summary>
        public override int ObjectID => (int)PortalType.FasterSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Faster;

        /// <summary>Initializes a new instance of the <seealso cref="FastestSpeedPortal"/> class.</summary>
        public FastestSpeedPortal() : base() { }
    }
}
