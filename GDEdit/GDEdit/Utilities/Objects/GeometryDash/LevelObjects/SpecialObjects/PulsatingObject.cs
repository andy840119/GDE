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

        /// <summary>Initializes a new instance of the <seealso cref="PulsatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the pulsating object.</param>
        public PulsatingObject(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="PulsatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the pulsating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public PulsatingObject(int objectID, double x, double y)
            : base(objectID, x, y) { }
    }
}
