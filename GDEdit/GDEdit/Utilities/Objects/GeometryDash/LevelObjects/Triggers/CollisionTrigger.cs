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
    /// <summary>Represents a Collision trigger.</summary>
    public class CollisionTrigger : Trigger, IHasTargetGroupID, IHasPrimaryBlockID, IHasSecondaryBlockID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Collision;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The primary Block ID of the trigger.</summary>
        public int PrimaryBlockID { get; set; }
        /// <summary>The secondary Block ID of the trigger.</summary>
        public int SecondaryBlockID { get; set; }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup { get; set; }
        /// <summary>The Trigger On Exit property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TriggerOnExit)]
        public bool TriggerOnExit { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="CollisionTrigger"/> class.</summary>
        public CollisionTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="CollisionTrigger"/> class.</summary>
        /// <param name="primaryBlockID">The primary Block ID of the trigger.</param>
        /// <param name="secondaryBlockID">The secondary Block ID of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group of the trigger.</param>
        /// <param name="triggerOnExit">The Trigger On Exit of the trigger.</param>
        public CollisionTrigger(int primaryBlockID, int secondaryBlockID, int targetGroupID, bool activateGroup = false, bool triggerOnExit = false)
        {
            PrimaryBlockID = primaryBlockID;
            SecondaryBlockID = secondaryBlockID;
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
            TriggerOnExit = triggerOnExit;
        }

        // TODO: Add cloning method
    }
}