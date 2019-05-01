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
    }
}
