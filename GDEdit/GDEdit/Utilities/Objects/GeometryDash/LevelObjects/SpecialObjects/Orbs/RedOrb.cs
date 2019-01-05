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
    /// <summary>Represents a red orb.</summary>
    public class RedOrb : Orb
    {
        /// <summary>The object ID of the red orb.</summary>
        public override short ObjectID => (short)(int)OrbType.RedOrb;

        /// <summary>Initializes a new instance of the <seealso cref="RedOrb"/> class.</summary>
        public RedOrb() : base() { }
    }
}
