using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a GRND Color trigger.</summary>
    public class GRNDColorTrigger : ColorTrigger
    {
        public override short ObjectID => (short)(int)TriggerType.GRND;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new short TargetColorID => (short)(int)SpecialColorID.GRND;

        /// <summary>Initializes a new instance of the <seealso cref="GRNDColorTrigger"/> class.</summary>
        public GRNDColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="GRNDColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public GRNDColorTrigger(float duration, bool tintGround = false)
            : base(duration, (short)(int)SpecialColorID.GRND, false, tintGround) { }
    }
}
