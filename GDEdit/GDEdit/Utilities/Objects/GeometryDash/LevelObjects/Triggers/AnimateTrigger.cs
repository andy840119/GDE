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
    /// <summary>Represents an Animate trigger.</summary>
    public class AnimateTrigger : Trigger, IHasTargetGroupID
    {
        private byte animationID;
        private short targetGroupID;

        public override int ObjectID => (int)TriggerType.Animate;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Animation ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.AnimationID)]
        public int AnimationID
        {
            get => animationID;
            set => animationID = (byte)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="AnimateTrigger"/> class.</summary>
        public AnimateTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="AnimateTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="animationID">The Animation ID property of the trigger.</summary>
        public AnimateTrigger(int targetGroupID, int animationID)
        {
            TargetGroupID = targetGroupID;
            AnimationID = animationID;
        }

        /// <summary>Returns a clone of this <seealso cref="AnimateTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new AnimateTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as AnimateTrigger;
            c.AnimationID = AnimationID;
            c.TargetGroupID = TargetGroupID;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
