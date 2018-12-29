using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a speed portal in the game</summary>
    public abstract class SpeedPortal : Portal
    {
        /// <summary>The gamemode the manipulation portal transforms the player into.</summary>
        public abstract Speed Speed { get; }

        /// <summary>The checked property of the manipulation portal that determines whether the speed portal will be taken into account when converting X position to time.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ManipulationPortal"/> class.</summary>
        public SpeedPortal() : base() { }
    }
}
