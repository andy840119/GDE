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
    /// <summary>Represents a black orb.</summary>
    [ObjectID(OrbType.BlackOrb)]
    public class BlackOrb : Orb
    {
        /// <summary>The object ID of the black orb.</summary>
        public override int ObjectID => (int)OrbType.BlackOrb;

        /// <summary>Initializes a new instance of the <seealso cref="BlackOrb"/> class.</summary>
        public BlackOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlackOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlackOrb());
    }
}
