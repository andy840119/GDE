using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 4 Color trigger.</summary>
    public class Color4ColorTrigger : ColorTrigger
    {
        public override int ObjectID => (int)TriggerType.Color4;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new short TargetColorID => 4;

        /// <summary>Initializes a new instance of the <seealso cref="Color4ColorTrigger"/> class.</summary>
        public Color4ColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color4ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color4ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, 4, copyOpacity, tintGround) { }
    }
}
