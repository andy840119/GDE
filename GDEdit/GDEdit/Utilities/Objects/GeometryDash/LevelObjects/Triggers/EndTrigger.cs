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
    /// <summary>Represents an End trigger.</summary>
    [FutureProofing("2.2")]
    public class EndTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the End trigger.</summary>
        public override int ObjectID => (int)TriggerType.End;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Reversed property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Reversed)]
        public bool Reversed
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Lock Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockY)]
        public bool LockY
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="EndTrigger"/> class.</summary>
        public EndTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="EndTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="reversed">The Reversed of the trigger.</param>
        public EndTrigger(int targetGroupID, bool reversed = false, bool lockY = false)
             : base()
        {
            TargetGroupID = targetGroupID;
            Reversed = reversed;
            LockY = lockY;
        }

        /// <summary>Returns a clone of this <seealso cref="EndTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new EndTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as EndTrigger;
            c.TargetGroupID = TargetGroupID;
            c.Reversed = Reversed;
            c.LockY = LockY;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
