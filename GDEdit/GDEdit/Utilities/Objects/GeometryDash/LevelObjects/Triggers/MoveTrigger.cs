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
    /// <summary>Represents a Move trigger.</summary>
    public class MoveTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Move;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The easing of the trigger.</summary>
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        public float EasingRate { get; set; }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => TargetPosGroupID;
            set => TargetPosGroupID = value;
        }
        /// <summary>The Move X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MoveX)]
        public float MoveX { get; set; }
        /// <summary>The Move Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MoveY)]
        public float MoveY { get; set; }
        /// <summary>The Lock to Player X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerX)]
        public bool LockToPlayerX { get; set; }
        /// <summary>The Lock to Player Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerY)]
        public bool LockToPlayerY { get; set; }
        /// <summary>The Target Pos Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.TargetPosGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public int TargetPosGroupID { get; set; }
        /// <summary>The Target Pos coordinates property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosCoordinates)]
        public MoveTargetPosCoordinates TargetPosCoordinates { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        public MoveTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(float duration, int targetGroupID, bool lockToPlayerX = false, bool lockToPlayerY = false)
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
        public MoveTrigger(float duration, int targetGroupID, float moveX, float moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
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
        public MoveTrigger(float duration, int targetGroupID, Easing easing, float easingRate, float moveX, float moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : this(duration, targetGroupID, moveX, moveY, lockToPlayerX, lockToPlayerY)
        {
            Easing = easing;
            EasingRate = easingRate;
        }
        // Add more constructors? They seem to be many already

        // TODO: Add cloning method
    }
}