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
    /// <summary>Represents a blue pad.</summary>
    public class BluePad : Pad
    {
        /// <summary>The object ID of the blue pad.</summary>
        public override int ObjectID => (int)PadType.BluePad;

        /// <summary>Initializes a new instance of the <seealso cref="BluePad"/> class.</summary>
        public BluePad() : base() { }
    }
}
