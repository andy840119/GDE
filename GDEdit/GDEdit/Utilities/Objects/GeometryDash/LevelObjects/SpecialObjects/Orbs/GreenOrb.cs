using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a green orb.</summary>
    public class GreenOrb : Orb
    {
        /// <summary>The object ID of the green orb.</summary>
        public override int ObjectID => (int)OrbType.GreenOrb;

        /// <summary>Initializes a new instance of the <seealso cref="GreenOrb"/> class.</summary>
        public GreenOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenOrb());
    }
}
