﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Alpha trigger.</summary>
    [ObjectID(TriggerType.Alpha)]
    public class AlphaTrigger : Trigger, IHasDuration, IHasTargetGroupID
    {
        private short targetGroupID;
        private float duration = 0.5f, opacity = 1;

        /// <summary>The Object ID of the Alpha trigger.</summary>
        public override int ObjectID => (int)TriggerType.Alpha;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Opacity)]
        public double Opacity
        {
            get => opacity;
            set => opacity = (float)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        public AlphaTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="opacity">The Opacity property of the trigger.</param>
        public AlphaTrigger(double duration, int targetGroupID, double opacity)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            Opacity = opacity;
        }

        /// <summary>Returns a clone of this <seealso cref="AlphaTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new AlphaTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as AlphaTrigger;
            c.Duration = Duration;
            c.TargetGroupID = TargetGroupID;
            c.Opacity = Opacity;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as AlphaTrigger;
            return base.EqualsInherited(other)
                && duration == z.duration
                && targetGroupID == z.targetGroupID
                && opacity == z.opacity;
        }
        /// <summary>Determines whether this object's type is the same as another object's type</summary>
        /// <param name="other">The other object to check whether its type is the same as this one's.</param>
        protected override bool EqualsType(GeneralObject other) => other is AlphaTrigger;
    }
}
