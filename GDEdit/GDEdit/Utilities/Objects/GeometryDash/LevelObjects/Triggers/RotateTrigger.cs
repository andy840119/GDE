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
    /// <summary>Represents a rotate trigger.</summary>
    public class RotateTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Move;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Degrees property of the trigger.</summary>
        public int Degrees { get; set; }
        /// <summary>The Times 360 property of the trigger.</summary>
        public int Times360 { get; set; }
        /// <summary>The easing of the trigger.</summary>
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        public float EasingRate { get; set; }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => CenterGroupID;
            set => CenterGroupID = value;
        }
        /// <summary>The Lock Object Rotation property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockObjectRotation)]
        public bool LockObjectRotation;
        /// <summary>The Center Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.CenterGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public int CenterGroupID;

        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        public RotateTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(float duration, int targetGroupID, bool lockObjectRotation = false)
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
        public RotateTrigger(float duration, int targetGroupID, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, lockObjectRotation)
        {
            Degrees = degrees;
            Times360 = times360;
        }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="moveX">The Move X of the trigger.</param>
        /// <param name="moveY">The Move Y of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(float duration, int targetGroupID, Easing easing, float easingRate, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, degrees, times360, lockObjectRotation)
        {
            Easing = easing;
            EasingRate = easingRate;
        }

        // TODO: Add cloning method
    }
}