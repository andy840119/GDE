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
    /// <summary>Represents a Rotate trigger.</summary>
    public class RotateTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, centerGroupID;
        private float duration = 0.5f, easingRate;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Rotate;

        /// <summary>The duration of the trigger's effect.</summary>
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => CenterGroupID;
            set => CenterGroupID = value;
        }
        /// <summary>The easing of the trigger.</summary>
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Degrees property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Degrees)]
        public int Degrees { get; set; }
        /// <summary>The Times 360 property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Times360)]
        public int Times360 { get; set; }
        /// <summary>The Lock Object Rotation property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockObjectRotation)]
        public bool LockObjectRotation
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Center Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.CenterGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public int CenterGroupID
        {
            get => centerGroupID;
            set => centerGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        public RotateTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, bool lockObjectRotation = false)
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            LockObjectRotation = lockObjectRotation;
        }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="degrees">The Degrees property of the trigger.</param>
        /// <param name="times360">The Times 360 property of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, lockObjectRotation)
        {
            Degrees = degrees;
            Times360 = times360;
        }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="degrees">The Degrees property of the trigger.</param>
        /// <param name="times360">The Times 360 property of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, Easing easing, double easingRate, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, degrees, times360, lockObjectRotation)
        {
            Easing = easing;
            EasingRate = easingRate;
        }

        /// <summary>Returns a clone of this <seealso cref="RotateTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RotateTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RotateTrigger;
            c.Duration = Duration;
            c.TargetGroupID = TargetGroupID;
            c.CenterGroupID = CenterGroupID;
            c.Easing = Easing;
            c.EasingRate = EasingRate;
            c.Degrees = Degrees;
            c.Times360 = Times360;
            c.LockObjectRotation = LockObjectRotation;
            c.CenterGroupID = CenterGroupID;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
