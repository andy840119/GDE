using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.ManipulationPortals
{
    /// <summary>Represents a manipulation portal (a portal that changes the player's gamemode).</summary>
    public abstract class ManipulationPortal
    {
        /// <summary>The gamemode the manipulation portal transforms the player tinto.</summary>
        public abstract Gamemode Gamemode { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ManipulationPortal"/> class.</summary>
        public ManipulationPortal()
        {

        }
    }
}
