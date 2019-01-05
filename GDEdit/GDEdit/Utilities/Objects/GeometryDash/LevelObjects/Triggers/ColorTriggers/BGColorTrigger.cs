using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a BG Color trigger.</summary>
    public class BGColorTrigger : ColorTrigger
    {
        public override short ObjectID => (short)(int)TriggerType.BG;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new short TargetColorID => (short)(int)SpecialColorID.BG;

        /// <summary>Initializes a new instance of the <seealso cref="BGColorTrigger"/> class.</summary>
        public BGColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="BGColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public BGColorTrigger(float duration, bool tintGround = false)
            : base(duration, (short)(int)SpecialColorID.BG, false, tintGround) { }
    }
}
