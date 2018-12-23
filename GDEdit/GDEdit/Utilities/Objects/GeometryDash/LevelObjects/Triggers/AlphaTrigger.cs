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
    /// <summary>Represents a Alpha trigger.</summary>
    public class AlphaTrigger : Trigger, IHasDuration, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Alpha;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Opacity)]
        public float Opacity { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        public AlphaTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="opacity">The Opacity property of the trigger.</param>
        public AlphaTrigger(float duration, int targetGroupID, float opacity)
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            Opacity = opacity;
        }

        // TODO: Add cloning method
    }
}