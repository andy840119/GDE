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
    /// <summary>Represents an On Death trigger.</summary>
    public class OnDeathTrigger : Trigger, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.OnDeath;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        public OnDeathTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</summary>
        public OnDeathTrigger(int targetGroupID, bool activateGroup = false)
        {
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
        }

        // TODO: Add cloning method
    }
}