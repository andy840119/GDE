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
    /// <summary>Represents a Random trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Random)]
    public class RandomTrigger : Trigger, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private byte chance;
        private short groupID1, groupID2;

        /// <summary>The Object ID of the Random trigger.</summary>
        public override int ObjectID => (int)TriggerType.Random;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => GroupID1;
            set => GroupID1 = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SecondaryGroupID)]
        public int SecondaryGroupID
        {
            get => GroupID2;
            set => GroupID2 = (short)value;
        }

        /// <summary>The Chance property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Chance)]
        public int Chance
        {
            get => chance;
            set => chance = (byte)value;
        }
        /// <summary>The Group ID 1 of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int GroupID1
        {
            get => groupID1;
            set => groupID1 = (short)value;
        }
        /// <summary>The Group ID 2 of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SecondaryGroupID)]
        public int GroupID2
        {
            get => groupID2;
            set => groupID2 = (short)value;
        }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="RandomTrigger"/> class.</summary>
        public RandomTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="RandomTrigger"/> class.</summary>
        /// <param name="groupID1">The Group ID 1 of the trigger.</param>
        /// <param name="groupID2">The Group ID 2 of the trigger.</param>
        /// <param name="chance">The Chance property of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</summary>
        public RandomTrigger(int groupID1, int groupID2, int chance, bool activateGroup = false)
             : base()
        {
            GroupID1 = groupID1;
            GroupID2 = groupID2;
            Chance = chance;
            ActivateGroup = activateGroup;
        }

        /// <summary>Returns a clone of this <seealso cref="RandomTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RandomTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RandomTrigger;
            c.GroupID1 = GroupID1;
            c.GroupID2 = GroupID2;
            c.Chance = Chance;
            c.ActivateGroup = ActivateGroup;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as RandomTrigger;
            return base.EqualsInherited(other)
                && groupID1 == z.groupID1
                && groupID2 == z.groupID2
                && chance == z.chance;
        }
        /// <summary>Determines whether this object's type is the same as another object's type</summary>
        /// <param name="other">The other object to check whether its type is the same as this one's.</param>
        protected override bool EqualsType(GeneralObject other) => other is RandomTrigger;
    }
}
