using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a magenta dash orb.</summary>
    [ObjectID(OrbType.MagentaDashOrb)]
    public class MagentaDashOrb : Orb
    {
        /// <summary>The object ID of the magenta dash orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.MagentaDashOrb;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaDashOrb"/> class.</summary>
        public MagentaDashOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaDashOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaDashOrb());
    }
}
