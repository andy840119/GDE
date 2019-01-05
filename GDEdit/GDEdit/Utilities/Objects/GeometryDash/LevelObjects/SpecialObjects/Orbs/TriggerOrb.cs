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

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a trigger orb.</summary>
    public class TriggerOrb : Orb, IHasTargetGroupID
    {
        /// <summary>The object ID of the trigger orb.</summary>
        public override short ObjectID => (short)(int)OrbType.TriggerOrb;

        /// <summary>Represents the Target Group ID of the trigger orb.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public short TargetGroupID { get; set; }
        /// <summary>Represents the Activate Group property of the trigger orb.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
		{
			get => SpecialObjectBools[1];
			set => SpecialObjectBools[1] = value;
		}

        /// <summary>Initializes a new instance of the <seealso cref="TriggerOrb"/> class.</summary>
        public TriggerOrb() : base() { }
    }
}
