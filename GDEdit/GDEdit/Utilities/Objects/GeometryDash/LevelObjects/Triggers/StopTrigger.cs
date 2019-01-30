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
    /// <summary>Represents a Stop trigger.</summary>
    public class StopTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Stop;

        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        public StopTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public StopTrigger(int targetGroupID)
        {
            TargetGroupID = targetGroupID;
        }

        /// <summary>Returns a clone of this <seealso cref="StopTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new StopTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as StopTrigger;
            c.TargetGroupID = TargetGroupID;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
