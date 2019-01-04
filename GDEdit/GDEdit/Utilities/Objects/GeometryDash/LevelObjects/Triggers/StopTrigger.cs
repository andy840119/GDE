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
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Stop;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public short TargetGroupID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        public StopTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public StopTrigger(short targetGroupID)
        {
            TargetGroupID = targetGroupID;
        }

        // TODO: Add cloning method
    }
}
