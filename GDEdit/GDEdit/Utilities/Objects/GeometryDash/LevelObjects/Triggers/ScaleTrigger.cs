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
    /// <summary>Represents a Scale trigger.</summary>
    [FutureProofing("2.3")]
    public class ScaleTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, centerGroupID;
        private float duration = 0.5f, easingRate, scalingMultiplier;

        /// <summary>The Object ID of the Scale trigger.</summary>
        public override int ObjectID => (int)TriggerType.Scale;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SecondaryGroupID)]
        public int SecondaryGroupID
        {
            get => CenterGroupID;
            set => CenterGroupID = value;
        }
        /// <summary>The easing of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Easing)]
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EasingRate)]
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Scaling Multiplier property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ScalingMultiplier)]
        public double ScalingMultiplier
        {
            get => scalingMultiplier;
            set => scalingMultiplier = (float)value;
        }
        /// <summary>The Center Group ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CenterGroupID)]
        public int CenterGroupID
        {
            get => centerGroupID;
            set => centerGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        public ScaleTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID)
             : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="scaling">The Scaling property of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID, double scaling)
            : this(duration, targetGroupID)
        {
            Scaling = scaling;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="scaling">The Scaling property of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID, Easing easing, double easingRate, double scaling)
            : this(duration, targetGroupID, scaling)
        {
            Easing = easing;
            EasingRate = easingRate;
        }

        /// <summary>Returns a clone of this <seealso cref="ScaleTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ScaleTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ScaleTrigger;
            c.Duration = Duration;
            c.TargetGroupID = TargetGroupID;
            c.CenterGroupID = CenterGroupID;
            c.Easing = Easing;
            c.EasingRate = EasingRate;
            c.ScalingMultiplier = ScalingMultiplier;
            c.CenterGroupID = CenterGroupID;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
