using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents an Animate trigger.</summary>
    public class AnimateTrigger : Trigger, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Animate;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Animation ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.AnimationID)]
        public int AnimationID { get; set; }

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

        // TODO: Add cloning method
    }
}