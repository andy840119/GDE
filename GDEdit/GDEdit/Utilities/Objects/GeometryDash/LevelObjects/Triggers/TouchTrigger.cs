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
    /// <summary>Represents a Touch trigger.</summary>
    [ObjectID(TriggerType.Touch)]
    public class TouchTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the Touch trigger.</summary>
        public override int ObjectID => (int)TriggerType.Touch;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Hold Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.HoldMode)]
        public bool HoldMode
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Dual Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.DualMode)]
        public bool DualMode
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Toggle Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ToggleMode)]
        public TouchToggleMode ToggleMode { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        public TouchTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="holdMode">The Hold Mode property of the trigger.</param>
        /// <param name="dualMode">The Dual Mode property of the trigger.</param>
        /// <param name="toggleMode">The Toggle Mode property of the trigger.</param>
        public TouchTrigger(int targetGroupID, bool holdMode = false, bool dualMode = false, TouchToggleMode toggleMode = TouchToggleMode.Default)
            : base()
        {
            TargetGroupID = targetGroupID;
            HoldMode = holdMode;
            DualMode = dualMode;
            ToggleMode = toggleMode;
        }

        /// <summary>Returns a clone of this <seealso cref="TouchTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new TouchTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as TouchTrigger;
            c.TargetGroupID = TargetGroupID;
            c.HoldMode = HoldMode;
            c.DualMode = DualMode;
            c.ToggleMode = ToggleMode;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
