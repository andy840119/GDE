using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 2 Color trigger.</summary>
    public class Color2ColorTrigger : ColorTrigger
    {
        public override int ObjectID => (int)TriggerType.Color2;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new short TargetColorID => 2;

        /// <summary>Initializes a new instance of the <seealso cref="Color2ColorTrigger"/> class.</summary>
        public Color2ColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color2ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color2ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, 2, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="Color2ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new Color2ColorTrigger());
    }
}
