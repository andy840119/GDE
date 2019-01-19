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
    /// <summary>Represents a Toggle trigger.</summary>
    public class ToggleTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Toggle;

        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ToggleTrigger"/> class.</summary>
        public ToggleTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="ToggleTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</summary>
        public ToggleTrigger(int targetGroupID, bool activateGroup = false)
        {
            TargetGroupID = targetGroupID;
            ActivateGroup = activateGroup;
        }

        // TODO: Add cloning method
    }
}
