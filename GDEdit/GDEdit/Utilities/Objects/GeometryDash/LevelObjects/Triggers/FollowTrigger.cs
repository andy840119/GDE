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
    public class FollowTrigger : Trigger, IHasDuration, IHasTargetGroupID, IHasSecondaryGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Follow;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The target Group ID of the trigger.</summary>
        public short TargetGroupID { get; set; }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public short SecondaryGroupID
        {
            get => FollowGroupID;
            set => FollowGroupID = value;
        }
        /// <summary>The X Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.XMod)]
        public float XMod { get; set; }
        /// <summary>The Y Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.YMod)]
        public float YMod { get; set; }
        /// <summary>The Follow Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.FollowGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public short FollowGroupID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        public FollowTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public FollowTrigger(float duration, short targetGroupID)
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="xMod">The X Mod of the trigger.</param>
        /// <param name="yMod">The Y Mod of the trigger.</param>
        public FollowTrigger(float duration, short targetGroupID, float xMod, float yMod)
            : this(duration, targetGroupID)
        {
            XMod = xMod;
            YMod = yMod;
        }

        // TODO: Add cloning method
    }
}
