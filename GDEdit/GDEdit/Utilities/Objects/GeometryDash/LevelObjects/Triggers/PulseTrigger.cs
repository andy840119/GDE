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
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Pulse;

        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The target Color ID of the trigger.</summary>
        public int TargetColorID { get; set; }
        /// <summary>The red part of the color.</summary>
        public int Red { get; set; }
        /// <summary>The green part of the color.</summary>
        public int Green { get; set; }
        /// <summary>The blue part of the color.</summary>
        public int Blue { get; set; }
        /// <summary>The Fade In property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeIn)]
        public float FadeIn;
        /// <summary>The Hold property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Hold)]
        public float Hold;
        /// <summary>The Fade Out property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeOut)]
        public float FadeOut;
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        public int CopiedColorID;
        /// <summary>The Pulse Mode of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.PulseMode)]
        public PulseMode PulseMode;
        /// <summary>The Pulse Target Type of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetType)]
        public PulseTargetType PulseTargetType;
        /// <summary>The Exclusive property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Exclusive)]
        public bool Exclusive;
        /// <summary>The HSV of the trigger (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorHSVValues)]
        public string HSV => HSVAdjustment.ToString();

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment;

        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        public PulseTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        /// <param name="fadeIn">The Fade In property of the trigger.</param>
        /// <param name="hold">The Hold property of the trigger.</param>
        /// <param name="fadeOut">The Fade Out property of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="pulseTargetType">The Pulse Target Type of the trigger.</param>
        /// <param name="pulseMode">The Pulse Mode of the trigger.</param>
        public PulseTrigger(float fadeIn, float hold, float fadeOut, int targetID, PulseTargetType pulseTargetType = PulseTargetType.ColorChannel, PulseMode pulseMode = PulseMode.Color)
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

        // TODO: Add cloning method
    }
}