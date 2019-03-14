using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a green dash orb.</summary>
    [ObjectID(OrbType.GreenDashOrb)]
    public class GreenDashOrb : Orb
    {
        /// <summary>The object ID of the green dash orb.</summary>
        public override int ObjectID => (int)OrbType.GreenDashOrb;

        /// <summary>Initializes a new instance of the <seealso cref="GreenDashOrb"/> class.</summary>
        public GreenDashOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenDashOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenDashOrb());
    }
}
