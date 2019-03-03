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
    /// <summary>Represents a Pickup trigger.</summary>
    public class PickupTrigger : Trigger, IHasTargetItemID
    {
        private short targetItemID;

        /// <summary>The Object ID of the Pickup trigger.</summary>
        public override int ObjectID => (int)TriggerType.Pickup;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ItemID)]
        public int TargetItemID
        {
            get => targetItemID;
            set => targetItemID = (short)value;
        }
        /// <summary>The Count property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Count)]
        public int Count { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        public PickupTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        /// <param name="targetItemID">The target Item ID of the trigger.</param>
        /// <param name="count">The Count property of the trigger.</param>
        public PickupTrigger(int targetItemID, int count)
            : base()
        {
            TargetItemID = targetItemID;
            Count = count;
        }

        /// <summary>Returns a clone of this <seealso cref="PickupTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PickupTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PickupTrigger;
            c.TargetItemID = TargetItemID;
            c.Count = Count;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
