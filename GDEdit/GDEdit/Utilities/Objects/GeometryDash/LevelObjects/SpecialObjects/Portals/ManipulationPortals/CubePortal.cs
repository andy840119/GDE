using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.ManipulationPortals
{
    /// <summary>Represents a cube portal.</summary>
    public class CubePortal : ManipulationPortal
    {
        /// <summary>The object ID of the cube portal.</summary>
        public override int ObjectID => (int)ManipulationPortalType.Cube;
        /// <summary>The gamemode the manipulation portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Cube;

        /// <summary>Initializes a new instance of the <seealso cref="CubePortal"/> class.</summary>
        public CubePortal() : base() { }
    }
}
