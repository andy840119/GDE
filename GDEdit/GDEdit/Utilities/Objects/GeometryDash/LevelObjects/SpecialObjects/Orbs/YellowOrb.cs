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
    /// <summary>Represents a yellow orb.</summary>
    public class YellowOrb : Orb
    {
        /// <summary>The object ID of the yellow orb.</summary>
        public override short ObjectID => (short)(int)OrbType.YellowOrb;

        /// <summary>Initializes a new instance of the <seealso cref="YellowOrb"/> class.</summary>
        public YellowOrb() : base() { }
    }
}
