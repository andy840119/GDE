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
    /// <summary>Represents a magenta orb.</summary>
    public class MagentaOrb : Orb
    {
        /// <summary>The object ID of the magenta orb.</summary>
        public override short ObjectID => (short)(int)OrbType.MagentaOrb;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaOrb"/> class.</summary>
        public MagentaOrb() : base() { }
    }
}
