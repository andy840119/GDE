using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a GRND2 Color trigger.</summary>
    public class GRND2ColorTrigger : ColorTrigger
    {
        public override int ObjectID => (int)TriggerType.GRND2;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new int TargetColorID => (int)SpecialColorID.GRND2;

        /// <summary>Initializes a new instance of the <seealso cref="GRND2ColorTrigger"/> class.</summary>
        public GRND2ColorTrigger() : base((int)SpecialColorID.GRND2) { }
        /// <summary>Initializes a new instance of the <seealso cref="GRND2ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public GRND2ColorTrigger(float duration, bool tintGround = false)
            : base(duration, (int)SpecialColorID.GRND2, false, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="GRND2ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GRND2ColorTrigger());
    }
}
