using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents an orb.</summary>
    public abstract class Orb : SpecialObject
    {
        /// <summary>The object ID of the orb.</summary>
        public new abstract int ObjectID { get; }

        /// <summary>Represents the Multi Activate property of the orb.</summary>
        [ObjectStringMappable(ObjectParameter.MultiActivate)]
        public bool MultiActivate
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Orb"/> class.</summary>
        public Orb()
            : base()
        {
            base.ObjectID = ObjectID;
        }

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as Orb;
            c.MultiActivate = MultiActivate;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
