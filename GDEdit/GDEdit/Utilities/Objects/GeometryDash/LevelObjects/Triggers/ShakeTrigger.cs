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
    /// <summary>Represents a Shake trigger.</summary>
    public class ShakeTrigger : Trigger, IHasDuration
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Shake;

        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The Strength property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Strength)]
        public float Strength { get; set; }
        /// <summary>The Interval property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Interval)]
        public float Interval { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        public ShakeTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="strength">The Strength property of the trigger.</param>
        /// <param name="interval">The Interval property of the trigger.</param>
        public ShakeTrigger(int duration, int strength, int interval)
        {
            Duration = duration;
            Strength = strength;
            Interval = interval;
        }

        // TODO: Add cloning method
    }
}