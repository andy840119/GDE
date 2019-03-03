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
    /// <summary>Represents a Shake trigger.</summary>
    public class ShakeTrigger : Trigger, IHasDuration
    {
        private float duration = 0.5f, strength, interval;

        public override int ObjectID => (int)TriggerType.Shake;

        /// <summary>The duration of the trigger's effect.</summary>
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The Strength property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Strength)]
        public double Strength
        {
            get => strength;
            set => strength = (float)value;
        }
        /// <summary>The Interval property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Interval)]
        public double Interval
        {
            get => interval;
            set => interval = (float)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        public ShakeTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="strength">The Strength property of the trigger.</param>
        /// <param name="interval">The Interval property of the trigger.</param>
        public ShakeTrigger(double duration, double strength, double interval)
        {
            Duration = duration;
            Strength = strength;
            Interval = interval;
        }

        /// <summary>Returns a clone of this <seealso cref="ShakeTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ShakeTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ShakeTrigger;
            c.Duration = Duration;
            c.Strength = Strength;
            c.Interval = Interval;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
