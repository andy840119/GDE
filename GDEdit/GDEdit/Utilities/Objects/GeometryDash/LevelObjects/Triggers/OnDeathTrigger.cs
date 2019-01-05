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
        public override short ObjectID => (short)(int)Enumerations.GeometryDash.TriggerType.OnDeath;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public short TargetGroupID { get; set; }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        public OnDeathTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="OnDeathTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</summary>
        public OnDeathTrigger(short targetGroupID, bool activateGroup = false)
        {
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
        }

        // TODO: Add cloning method
    }
}
