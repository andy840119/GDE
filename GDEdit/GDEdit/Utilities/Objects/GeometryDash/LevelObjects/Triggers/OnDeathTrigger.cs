using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents an On Death trigger.</summary>
    public class OnDeathTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the On Death trigger.</summary>
        public override int ObjectID => (int)TriggerType.OnDeath;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        public OnDeathTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</summary>
        public OnDeathTrigger(int targetGroupID, bool activateGroup = false)
            : base()
        {
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
        }

        /// <summary>Returns a clone of this <seealso cref="OnDeathTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new OnDeathTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as OnDeathTrigger;
            c.ActivateGroup = ActivateGroup;
            c.TargetGroupID = TargetGroupID;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
