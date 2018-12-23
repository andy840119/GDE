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
    /// <summary>Represents a Follow trigger.</summary>
    public class FollowPlayerYTrigger : Trigger, IHasDuration, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.FollowPlayerY;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Speed property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Speed)]
        public float Speed { get; set; }
        /// <summary>The Delay property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FollowDelay)]
        public float Delay { get; set; }
        /// <summary>The Max Speed property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MaxSpeed)]
        public float MaxSpeed { get; set; }
        /// <summary>The Offset property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.YOffset)]
        public int Offset { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="FollowPlayerYTrigger"/> class.</summary>
        public FollowPlayerYTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="FollowPlayerYTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public FollowPlayerYTrigger(float duration, int targetGroupID)
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="FollowPlayerYTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="speed">The Speed property of the trigger.</param>
        /// <param name="delay">The Delay property of the trigger.</param>
        /// <param name="maxSpeed">The Max Speed property of the trigger.</param>
        /// <param name="offset">The Offset property of the trigger.</param>
        public FollowPlayerYTrigger(float duration, int targetGroupID, float speed, float delay, float maxSpeed, int offset)
            : this(duration, targetGroupID)
        {
            Speed = speed;
            Delay = delay;
            MaxSpeed = maxSpeed;
            Offset = offset;
        }

        // TODO: Add cloning method
    }
}