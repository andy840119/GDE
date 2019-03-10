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
    /// <summary>Represents a Collision trigger.</summary>
    public class CollisionTrigger : Trigger, IHasTargetGroupID, IHasPrimaryBlockID, IHasSecondaryBlockID
    {
        private short targetGroupID, primaryBlockID, secondaryBlockID;

        /// <summary>The Object ID of the Collision trigger.</summary>
        public override int ObjectID => (int)TriggerType.Collision;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The primary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.BlockAID)]
        public int PrimaryBlockID
        {
            get => primaryBlockID;
            set => primaryBlockID = (short)value;
        }
        /// <summary>The secondary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.BlockBID)]
        public int SecondaryBlockID
        {
            get => secondaryBlockID;
            set => secondaryBlockID = (short)value;
        }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Trigger On Exit property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TriggerOnExit)]
        public bool TriggerOnExit
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CollisionTrigger"/> class.</summary>
        public CollisionTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="CollisionTrigger"/> class.</summary>
        /// <param name="primaryBlockID">The primary Block ID of the trigger.</param>
        /// <param name="secondaryBlockID">The secondary Block ID of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group of the trigger.</param>
        /// <param name="triggerOnExit">The Trigger On Exit of the trigger.</param>
        public CollisionTrigger(int primaryBlockID, int secondaryBlockID, int targetGroupID, bool activateGroup = false, bool triggerOnExit = false)
            : base()
        {
            PrimaryBlockID = primaryBlockID;
            SecondaryBlockID = secondaryBlockID;
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
            TriggerOnExit = triggerOnExit;
        }

        /// <summary>Returns a clone of this <seealso cref="CollisionTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CollisionTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CollisionTrigger;
            c.TargetGroupID = TargetGroupID;
            c.PrimaryBlockID = PrimaryBlockID;
            c.SecondaryBlockID = SecondaryBlockID;
            c.ActivateGroup = ActivateGroup;
            c.TriggerOnExit = TriggerOnExit;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
