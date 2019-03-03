using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a 3DL Color trigger.</summary>
    public class ThreeDLColorTrigger : ColorTrigger
    {
        /// <summary>The Object ID of the 3DL Color trigger.</summary>
        public override int ObjectID => (int)TriggerType.ThreeDL;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new int TargetColorID => (int)SpecialColorID.ThreeDL;

        /// <summary>Initializes a new instance of the <seealso cref="ThreeDLColorTrigger"/> class.</summary>
        public ThreeDLColorTrigger() : base((int)SpecialColorID.ThreeDL) { }
        /// <summary>Initializes a new instance of the <seealso cref="ThreeDLColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ThreeDLColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, (int)SpecialColorID.ThreeDL, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="ThreeDLColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ThreeDLColorTrigger());
    }
}
