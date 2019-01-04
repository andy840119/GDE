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
    /// <summary>Represents a Pickup trigger.</summary>
    public class PickupTrigger : Trigger, IHasTargetItemID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Pickup;
        
        /// <summary>The target Item ID of the trigger.</summary>
        public short TargetItemID { get; set; }
        /// <summary>The Count property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Count)]
        public int Count { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        public PickupTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        /// <param name="targetItemID">The target Item ID of the trigger.</param>
        /// <param name="count">The Count property of the trigger.</param>
        public PickupTrigger(short targetItemID, int count)
        {
            TargetItemID = targetItemID;
            Count = count;
        }

        // TODO: Add cloning method
    }
}
