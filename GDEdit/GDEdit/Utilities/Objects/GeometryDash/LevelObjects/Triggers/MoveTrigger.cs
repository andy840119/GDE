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
    /// <summary>Represents a Move trigger.</summary>
    public class MoveTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, targetPosGroupID;
        private float duration = 0.5f, easingRate, moveX, moveY;

        public override int ObjectID => (int)TriggerType.Move;

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
            get => TargetPosGroupID;
            set => TargetPosGroupID = value;
        }
        /// <summary>The easing of the trigger.</summary>
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Move X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MoveX)]
        public double MoveX
        {
            get => moveX;
            set => moveX = (float)value;
        }
        /// <summary>The Move Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MoveY)]
        public double MoveY
        {
            get => moveY;
            set => moveY = (float)value;
        }
        /// <summary>The Lock to Player X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerX)]
        public bool LockToPlayerX
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Lock to Player Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerY)]
        public bool LockToPlayerY
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Target Pos Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.TargetPosGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public int TargetPosGroupID
        {
            get => targetPosGroupID;
            set => targetPosGroupID = (short)value;
        }
        /// <summary>The Target Pos coordinates property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosCoordinates)]
        public MoveTargetPosCoordinates TargetPosCoordinates { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        public MoveTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            LockToPlayerX = lockToPlayerX;
            LockToPlayerY = lockToPlayerY;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="moveX">The Move X of the trigger.</param>
        /// <param name="moveY">The Move Y of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, double moveX, double moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : this(duration, targetGroupID, lockToPlayerX, lockToPlayerY)
        {
            MoveX = moveX;
            MoveY = moveY;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="moveX">The Move X of the trigger.</param>
        /// <param name="moveY">The Move Y of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, Easing easing, double easingRate, double moveX, double moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : this(duration, targetGroupID, moveX, moveY, lockToPlayerX, lockToPlayerY)
        {
            Easing = easing;
            EasingRate = easingRate;
        }
        // Add more constructors? They seem to be many already

        /// <summary>Returns a clone of this <seealso cref="MoveTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MoveTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as MoveTrigger;
            c.Duration = Duration;
            c.TargetGroupID = TargetGroupID;
            c.TargetPosGroupID = TargetPosGroupID;
            c.Easing = Easing;
            c.EasingRate = EasingRate;
            c.MoveX = MoveX;
            c.MoveY = MoveY;
            c.LockToPlayerX = LockToPlayerX;
            c.LockToPlayerY = LockToPlayerY;
            c.TargetPosCoordinates = TargetPosCoordinates;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
