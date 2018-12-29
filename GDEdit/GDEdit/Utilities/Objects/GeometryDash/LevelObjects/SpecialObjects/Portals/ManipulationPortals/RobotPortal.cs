using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.ManipulationPortals
{
    /// <summary>Represents a robot portal.</summary>
    public class RobotPortal : ManipulationPortal
    {
        /// <summary>The object ID of the robot portal.</summary>
        public override int ObjectID => (int)ManipulationPortalType.Robot;
        /// <summary>The gamemode the manipulation portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Robot;

        /// <summary>Initializes a new instance of the <seealso cref="RobotPortal"/> class.</summary>
        public RobotPortal() : base() { }
    }
}
