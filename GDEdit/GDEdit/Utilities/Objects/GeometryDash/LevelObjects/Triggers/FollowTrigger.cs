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
        private short targetGroupID, followGroupID;
        private float duration = 0.5f, xMod, yMod;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Follow;

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
            get => FollowGroupID;
            set => FollowGroupID = value;
        }
        /// <summary>The X Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.XMod)]
        public double XMod
        {
            get => xMod;
            set => xMod = (float)value;
        }
        /// <summary>The Y Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.YMod)]
        public double YMod
        {
            get => yMod;
            set => yMod = (float)value;
        }
        /// <summary>The Follow Group ID property of the trigger.</summary>
        //[ObjectStringMappable(ObjectParameter.FollowGroupID)]
        // Do not also map this property, the interface provides the definition for the one already.
        public int FollowGroupID
        {
            get => followGroupID;
            set => followGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        public FollowTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public FollowTrigger(double duration, int targetGroupID)
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="xMod">The X Mod of the trigger.</param>
        /// <param name="yMod">The Y Mod of the trigger.</param>
        public FollowTrigger(double duration, int targetGroupID, double xMod, double yMod)
            : this(duration, targetGroupID)
        {
            XMod = xMod;
            YMod = yMod;
        }

        // TODO: Add cloning method
    }
}
