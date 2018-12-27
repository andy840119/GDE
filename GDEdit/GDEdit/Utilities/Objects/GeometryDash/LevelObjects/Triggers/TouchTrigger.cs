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
    /// <summary>Represents a Touch trigger.</summary>
    public class TouchTrigger : Trigger, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Touch;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Hold Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.HoldMode)]
        public bool HoldMode { get; set; }
        /// <summary>The Dual Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.DualMode)]
        public bool DualMode { get; set; }
        /// <summary>The Toggle Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ToggleMode)]
        public TouchToggleMode ToggleMode { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        public TouchTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="holdMode">The Hold Mode property of the trigger.</param>
        /// <param name="dualMode">The Dual Mode property of the trigger.</param>
        /// <param name="toggleMode">The Toggle Mode property of the trigger.</param>
        public TouchTrigger(int targetGroupID, bool holdMode = false, bool dualMode = false, TouchToggleMode toggleMode = TouchToggleMode.Default)
        {
            TargetGroupID = targetGroupID;
            HoldMode = holdMode;
            DualMode = dualMode;
            ToggleMode = toggleMode;
        }

        // TODO: Add cloning method
    }
}