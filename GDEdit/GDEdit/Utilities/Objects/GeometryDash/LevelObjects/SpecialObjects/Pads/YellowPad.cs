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
    /// <summary>Represents a yellow pad.</summary>
    public class YellowPad : Pad
    {
        /// <summary>The object ID of the yellow pad.</summary>
        public override short ObjectID => (short)(int)PadType.YellowPad;

        /// <summary>Initializes a new instance of the <seealso cref="YellowPad"/> class.</summary>
        public YellowPad() : base() { }
    }
}
