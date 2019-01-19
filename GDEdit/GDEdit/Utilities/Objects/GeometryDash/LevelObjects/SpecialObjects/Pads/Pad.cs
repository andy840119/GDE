using GDEdit.Utilities.Attributes;
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
    /// <summary>Represents a pad.</summary>
    public abstract class Pad : SpecialObject
    {
        /// <summary>The object ID of the pad.</summary>
        public new abstract int ObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Pad"/> class.</summary>
        public Pad() : base() { }
    }
}
