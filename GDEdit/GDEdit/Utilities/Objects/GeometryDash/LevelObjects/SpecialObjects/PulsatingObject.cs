using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a pulsating object.</summary>
    public class PulsatingObject : SpecialObject
    {
        protected override int[] ValidObjectIDs => ObjectLists.PulsatingObjectList;
        protected override string SpecialObjectTypeName => "pulsating object";

        /// <summary>The animation speed of the pulsating object as a ratio.</summary>
        [ObjectStringMappable(ObjectParameter.AnimationSpeed)]
        public float AnimationSpeed { get; set; }
        /// <summary>The Randomize Start property of the pulsating object.</summary>
        [ObjectStringMappable(ObjectParameter.RandomizeStart)]
        public bool RandomizeStart
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="PulsatingObject"/> class. For internal use only.</summary>
        private PulsatingObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulsatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the pulsating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public PulsatingObject(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="PulsatingObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PulsatingObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PulsatingObject;
            c.AnimationSpeed = AnimationSpeed;
            c.RandomizeStart = RandomizeStart;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
