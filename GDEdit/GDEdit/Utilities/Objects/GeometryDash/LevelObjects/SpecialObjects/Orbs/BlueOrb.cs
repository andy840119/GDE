using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
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
    /// <summary>Represents a blue orb.</summary>
    public class BlueOrb : Orb
    {
        /// <summary>The object ID of the blue orb.</summary>
        public override int ObjectID => (int)OrbType.BlueOrb;

        /// <summary>Initializes a new instance of the <seealso cref="BlueOrb"/> class.</summary>
        public BlueOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueOrb());
    }
}
