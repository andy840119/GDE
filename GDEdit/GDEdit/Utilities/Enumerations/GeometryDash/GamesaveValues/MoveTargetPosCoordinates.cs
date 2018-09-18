using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues
{
    /// <summary>This enumeration provides values for the coordinates to rely on the object in the Target Pos Group ID of a Move trigger.</summary>
    public enum MoveTargetPosCoordinates
    {
        /// <summary>Represents the value for both coordinates.</summary>
        Both,
        /// <summary>Represents the value for only the X coordinate.</summary>
        XOnly,
        /// <summary>Represents the value for only the Y coordinate.</summary>
        YOnly
    }
}
