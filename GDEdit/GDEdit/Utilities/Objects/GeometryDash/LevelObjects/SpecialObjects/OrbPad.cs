using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents an orb or a pad object.</summary>
    public abstract class OrbPad : ConstantIDSpecialObject, IHasCheckedProperty
    {
        /// <summary>Represents the Switch Player Direction property of the orb or pad.</summary>
        [ObjectStringMappable(ObjectParameter.SwitchPlayerDirection)]
        public bool SwitchPlayerDirection
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Checked property of the orb or pad.</summary>
        [ObjectStringMappable(ObjectParameter.SpecialObjectChecked)]
        public bool Checked
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="OrbPad"/> class.</summary>
        public OrbPad() : base() { }
    }
}
