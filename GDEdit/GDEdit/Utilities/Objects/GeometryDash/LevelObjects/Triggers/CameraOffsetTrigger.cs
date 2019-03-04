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
    [FutureProofing("2.2")]
    public class CameraOffsetTrigger : Trigger, IHasDuration, IHasEasing
    {
        private float duration = 0.5f, easingRate, offsetX, offsetY;

        public override int ObjectID => (int)TriggerType.CameraOffset;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The easing of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Easing)]
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EasingRate)]
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
        /// <summary>The Target Pos coordinates property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosCoordinates)]
        public TargetPosCoordinates TargetPosCoordinates { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        public CameraOffsetTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="xOnly">The X Only property of the trigger.</param>
        /// <param name="yOnly">The Y Only property of the trigger.</param>
        public CameraOffsetTrigger(double duration, TargetPosCoordinates coordinates = TargetPosCoordinates.Both)
        {
            Duration = duration;
            TargetPosCoordinates = coordinates;
        }
        /// <summary>Initializes a new instance of the <seealso cref="CameraOffsetTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="offsetX">The Offset X of the trigger.</param>
        /// <param name="offsetY">The Offset Y of the trigger.</param>
        /// <param name="xOnly">The X Only property of the trigger.</param>
        /// <param name="yOnly">The Y Only property of the trigger.</param>
        public CameraOffsetTrigger(double duration, double offsetX, double offsetY, TargetPosCoordinates coordinates = TargetPosCoordinates.Both)
            : this(duration, coordinates)
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
            c.TargetPosCoordinates = TargetPosCoordinates;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
