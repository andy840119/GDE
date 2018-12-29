using GDEdit.Utilities.Attributes;
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
    /// <summary>Represents an orb.</summary>
    public abstract class Orb : SpecialObject
    {
        /// <summary>Represents the Multi Activate property of the orb.</summary>
        [ObjectStringMappable(ObjectParameter.MultiActivate)]
        public bool MultiActivate { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Orb"/> class.</summary>
        public Orb() : base() { }
    }
}
