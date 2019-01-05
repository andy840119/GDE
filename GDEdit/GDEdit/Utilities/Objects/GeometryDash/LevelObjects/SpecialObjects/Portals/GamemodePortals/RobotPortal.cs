using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a robot portal.</summary>
    public class RobotPortal : GamemodePortal
    {
        /// <summary>The object ID of the robot portal.</summary>
        public override short ObjectID => (short)(int)PortalType.Robot;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Robot;

        /// <summary>Initializes a new instance of the <seealso cref="RobotPortal"/> class.</summary>
        public RobotPortal() : base() { }
    }
}
