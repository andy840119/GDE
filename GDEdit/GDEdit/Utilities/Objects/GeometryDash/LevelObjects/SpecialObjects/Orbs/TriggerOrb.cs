using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
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
    [ObjectID(OrbType.TriggerOrb)]
    public class TriggerOrb : Orb, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The object ID of the trigger orb.</summary>
        public override int ObjectID => (int)OrbType.TriggerOrb;

        /// <summary>Represents the Target Group ID of the trigger orb.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>Represents the Activate Group property of the trigger orb.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="TriggerOrb"/> class.</summary>
        public TriggerOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="TriggerOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new TriggerOrb());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as TriggerOrb;
            c.TargetGroupID = TargetGroupID;
            c.ActivateGroup = ActivateGroup;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
