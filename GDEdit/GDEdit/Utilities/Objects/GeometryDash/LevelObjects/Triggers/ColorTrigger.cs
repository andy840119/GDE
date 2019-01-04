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
    /// <summary>Represents a Color trigger.</summary>
    public class ColorTrigger : Trigger, IHasTargetColorID, IHasColor, IHasDuration
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Color;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public short TargetColorID { get; set; }
        /// <summary>The duration of the trigger's effect.</summary>
        public float Duration { get; set; } = 0.5f;
        /// <summary>The red part of the color.</summary>
        public byte Red { get; set; }
        /// <summary>The green part of the color.</summary>
        public byte Green { get; set; }
        /// <summary>The blue part of the color.</summary>
        public byte Blue { get; set; }
        /// <summary>The Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Opacity)]
        public float Opacity { get; set; } = 1;
        // IMPORTANT: The Player 1 and Player 2 properties are ignored because the Copied Color ID serves that purpose well
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        public short CopiedColorID { get; set; }
        /// <summary>The Blending property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Blending)]
        public bool Blending
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Copy Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopyOpacity)]
        public bool CopyOpacity
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Tint Ground property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TintGround)]
        public bool TintGround
        {
            get => TriggerBools[5];
            set => TriggerBools[5] = value;
        }
        /// <summary>The HSV of the trigger (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorHSVValues)]
        public string HSV => HSVAdjustment.ToString();

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        public ColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        /// <param name="targetID">The target ID of the trigger.</param>
        public ColorTrigger(short targetID)
        {
            TargetColorID = targetID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ColorTrigger(float duration, short targetID, bool copyOpacity = false, bool tintGround = false)
            : this(targetID)
        {
            Duration = duration;
            TargetColorID = targetID;
            CopyOpacity = copyOpacity;
            TintGround = tintGround;
        }
        // Constructors like this are useless, so many fucking parameters are required

        // TODO: Add cloning method
    }
}
