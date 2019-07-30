using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.General;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Pulse trigger.</summary>
    [ObjectID(TriggerType.Pulse)]
    public class PulseTrigger : Trigger, IHasTargetGroupID, IHasTargetColorID, IHasCopiedColorID, IHasColor
    {
        private byte red = 255, green = 255, blue = 255;
        private short targetGroupID, targetColorID, copiedColorID;
        private float fadeIn, hold, fadeOut;

        /// <summary>The Object ID of the Pulse trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Pulse;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The target Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetColorID)]
        public int TargetColorID
        {
            get => targetColorID;
            set => targetColorID = (short)value;
        }
        /// <summary>The red part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Red)]
        public int Red
        {
            get => red;
            set => red = (byte)value;
        }
        /// <summary>The green part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Green)]
        public int Green
        {
            get => green;
            set => green = (byte)value;
        }
        /// <summary>The blue part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Blue)]
        public int Blue
        {
            get => blue;
            set => blue = (byte)value;
        }
        /// <summary>The Fade In property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeIn)]
        public double FadeIn
        {
            get => fadeIn;
            set => fadeIn = (float)value;
        }
        /// <summary>The Hold property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Hold)]
        public double Hold
        {
            get => hold;
            set => hold = (float)value;
        }
        /// <summary>The Fade Out property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeOut)]
        public double FadeOut
        {
            get => fadeOut;
            set => fadeOut = (float)value;
        }
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        public int CopiedColorID
        {
            get => copiedColorID;
            set => copiedColorID = (short)value;
        }
        /// <summary>The Pulse Mode of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.PulseMode)]
        public PulseMode PulseMode { get; set; }
        /// <summary>The Pulse Target Type of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetType)]
        public PulseTargetType PulseTargetType { get; set; }
        /// <summary>The Exclusive property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Exclusive)]
        public bool Exclusive
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The HSV of the trigger (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorHSVValues)]
        public string HSV
        {
            get => HSVAdjustment.ToString();
            set => HSVAdjustment = HSVAdjustment.Parse(value);
        }

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment { get; set; } = new HSVAdjustment();

        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        public PulseTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        /// <param name="fadeIn">The Fade In property of the trigger.</param>
        /// <param name="hold">The Hold property of the trigger.</param>
        /// <param name="fadeOut">The Fade Out property of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="pulseTargetType">The Pulse Target Type of the trigger.</param>
        /// <param name="pulseMode">The Pulse Mode of the trigger.</param>
        public PulseTrigger(double fadeIn, double hold, double fadeOut, int targetID, PulseTargetType pulseTargetType = PulseTargetType.ColorChannel, PulseMode pulseMode = PulseMode.Color)
            : base()
        {
            FadeIn = fadeIn;
            Hold = hold;
            FadeOut = fadeOut;
            if (pulseTargetType == PulseTargetType.ColorChannel)
                TargetColorID = targetID;
            else
                TargetGroupID = targetID;
            PulseTargetType = pulseTargetType;
            PulseMode = pulseMode;
        }

        /// <summary>Returns a clone of this <seealso cref="PulseTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PulseTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PulseTrigger;
            c.targetGroupID = targetGroupID;
            c.targetColorID = targetColorID;
            c.red = red;
            c.green = green;
            c.blue = blue;
            c.fadeIn = fadeIn;
            c.hold = hold;
            c.fadeOut = fadeOut;
            c.copiedColorID = copiedColorID;
            c.PulseMode = PulseMode;
            c.PulseTargetType = PulseTargetType;
            c.HSVAdjustment = HSVAdjustment;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as PulseTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && targetColorID == z.targetColorID
                && red == z.red
                && green == z.green
                && blue == z.blue
                && fadeIn == z.fadeIn
                && hold == z.hold
                && fadeOut == z.fadeOut
                && copiedColorID == z.copiedColorID
                && PulseMode == z.PulseMode
                && PulseTargetType == z.PulseTargetType
                && HSVAdjustment == z.HSVAdjustment;
        }
    }
}
