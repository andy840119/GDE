using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.ManipulationPortals
{
    /// <summary>Represents a manipulation portal (a portal that changes the player's gamemode).</summary>
    public abstract class ManipulationPortal : Portal
    {
        /// <summary>The gamemode the manipulation portal transforms the player into.</summary>
        public abstract Gamemode Gamemode { get; }

        // TODO: Move that to individual manipulation portals that do have the property, unlike others
        /// <summary>The checked property of the manipulation portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ManipulationPortal"/> class.</summary>
        public ManipulationPortal() : base() { }
    }
}
