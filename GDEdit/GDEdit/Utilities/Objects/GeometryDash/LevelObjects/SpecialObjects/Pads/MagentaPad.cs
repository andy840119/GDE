using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a magenta pad.</summary>
    public class MagentaPad : Pad
    {
        /// <summary>The object ID of the magenta pad.</summary>
        public override int ObjectID => (int)PadType.MagentaPad;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaPad"/> class.</summary>
        public MagentaPad() : base() { }
    }
}
