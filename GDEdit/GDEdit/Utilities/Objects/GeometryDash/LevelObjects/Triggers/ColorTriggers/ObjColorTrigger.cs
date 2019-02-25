using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents an Obj Color trigger.</summary>
    public class ObjColorTrigger : ColorTrigger
    {
        public override int ObjectID => (int)TriggerType.Obj;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public new short TargetColorID => (short)(int)SpecialColorID.Obj;

        /// <summary>Initializes a new instance of the <seealso cref="ObjColorTrigger"/> class.</summary>
        public ObjColorTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ObjColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, (short)(int)SpecialColorID.Obj, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="ObjColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ObjColorTrigger());
    }
}
