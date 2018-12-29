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
    /// <summary>Represents a ship portal.</summary>
    public class ShipPortal : ManipulationPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the ship portal.</summary>
        public override int ObjectID => (int)ManipulationPortalType.Ship;
        /// <summary>The gamemode the manipulation portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Ship;

        /// <summary>The checked property of the ship portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ShipPortal"/> class.</summary>
        public ShipPortal() : base() { }
    }
}
