using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Objects.GeometryDash.General;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Pulse trigger.</summary>
    public class PulseTrigger : Trigger, IHasTargetGroupID, IHasTargetColorID, IHasColor
    {
        private byte red, green, blue;
        private short targetGroupID, targetColorID;
        private float fadeIn, hold, fadeOut;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Pulse;

        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The target Color ID of the trigger.</summary>
        public int TargetColorID
        {
            get => targetColorID;
            set => targetColorID = (short)value;
        }
        /// <summary>The red part of the color.</summary>
        public int Red
        {
            get => red;
            set => red = (byte)value;
        }
        /// <summary>The green part of the color.</summary>
        public int Green
        {
            get => green;
            set => green = (byte)value;
        }
        /// <summary>The blue part of the color.</summary>
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
            get => CopiedColorID;
            set => CopiedColorID = (short)value;
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
        public string HSV => HSVAdjustment.ToString();

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        public PulseTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        /// <param name="fadeIn">The Fade In property of the trigger.</param>
        /// <param name="hold">The Hold property of the trigger.</param>
        /// <param name="fadeOut">The Fade Out property of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="pulseTargetType">The Pulse Target Type of the trigger.</param>
        /// <param name="pulseMode">The Pulse Mode of the trigger.</param>
        public PulseTrigger(double fadeIn, double hold, double fadeOut, int targetID, PulseTargetType pulseTargetType = PulseTargetType.ColorChannel, PulseMode pulseMode = PulseMode.Color)
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
            c.TargetGroupID = TargetGroupID;
            c.TargetColorID = TargetColorID;
            c.Red = Red;
            c.Green = Green;
            c.Blue = Blue;
            c.FadeIn = FadeIn;
            c.Hold = Hold;
            c.FadeOut = FadeOut;
            c.CopiedColorID = CopiedColorID;
            c.PulseMode = PulseMode;
            c.PulseTargetType = PulseTargetType;
            c.Exclusive = Exclusive;
            c.HSVAdjustment = HSVAdjustment;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
