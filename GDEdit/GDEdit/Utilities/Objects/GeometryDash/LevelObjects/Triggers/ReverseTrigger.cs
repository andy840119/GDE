using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Reverse trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Reverse)]
    public class ReverseTrigger : Trigger
    {
        public override int ObjectID => (int)TriggerType.Reverse;

        /// <summary>The Reverse property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Reversed)]
        public bool Reverse
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="ReverseTrigger"/> class.</summary>
        /// <param name="reverse">The Reverse property of the trigger.</param>
        public ReverseTrigger(bool reverse = false)
        {
            Reverse = reverse;
        }

        /// <summary>Returns a clone of this <seealso cref="ReverseTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ReverseTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ReverseTrigger;
            c.Reverse = Reverse;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object's type is the same as another object's type</summary>
        /// <param name="other">The other object to check whether its type is the same as this one's.</param>
        protected override bool EqualsType(GeneralObject other) => other is ReverseTrigger;
    }
}
