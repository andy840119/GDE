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
    /// <summary>Represents a Camera Offset trigger.</summary>
    public class CameraOffsetTrigger : Trigger, IHasDuration, IHasEasing
    {
        private float duration = 0.5f, easingRate, offsetX, offsetY;

        public override int ObjectID => (int)TriggerType.CameraOffset;

        /// <summary>The duration of the trigger's effect.</summary>
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The easing of the trigger.</summary>
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Offset X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.OffsetX)]
        public double OffsetX
        {
            get => offsetX;
            set => offsetX = (float)value;
        }
        /// <summary>The Offset Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.OffsetY)]
        public double OffsetY
        {
            get => offsetY;
            set => offsetY = (float)value;
        }
        /// <summary>The X Only property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.XOnly)]
        public bool XOnly
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Y Only property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.YOnly)]
        public bool YOnly
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        public CameraOffsetTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="xOnly">The X Only property of the trigger.</param>
        /// <param name="yOnly">The Y Only property of the trigger.</param>
        public CameraOffsetTrigger(double duration, bool xOnly = false, bool yOnly = false)
        {
            Duration = duration;
            XOnly = xOnly;
            YOnly = yOnly;
        }
        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="offsetX">The Offset X of the trigger.</param>
        /// <param name="offsetY">The Offset Y of the trigger.</param>
        /// <param name="xOnly">The X Only property of the trigger.</param>
        /// <param name="yOnly">The Y Only property of the trigger.</param>
        public CameraOffsetTrigger(double duration, double offsetX, double offsetY, bool xOnly = false, bool yOnly = false)
            : this(duration, xOnly, yOnly)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        /// <summary>Returns a clone of this <seealso cref="CameraOffsetTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CameraOffsetTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CameraOffsetTrigger;
            c.Duration = Duration;
            c.Easing = Easing;
            c.EasingRate = EasingRate;
            c.OffsetX = OffsetX;
            c.OffsetY = OffsetY;
            c.XOnly = XOnly;
            c.YOnly = YOnly;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
