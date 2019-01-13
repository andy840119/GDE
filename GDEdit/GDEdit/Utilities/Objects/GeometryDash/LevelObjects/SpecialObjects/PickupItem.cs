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
    /// <summary>Represents a pickup item.</summary>
    public class PickupItem : SpecialObject, IHasTargetGroupID
    {
        private short targetGroupID;

        protected override int[] ValidObjectIDs => ObjectLists.PickupItemList;
        protected override string SpecialObjectTypeName => "pickup item";

        /// <summary>Represents the Pickup Mode property of the pickup item.</summary>
        [ObjectStringMappable(ObjectParameter.PickupMode)]
        public PickupItemPickupMode PickupMode { get; set; }
        /// <summary>Represents the Count property of the pickup item.</summary>
        [ObjectStringMappable(ObjectParameter.Count)]
        public int Count { get; set; }
        /// <summary>Represents the Subtract Count property of the pickup item.</summary>
        [ObjectStringMappable(ObjectParameter.SubtractCount)]
        public bool SubtractCount
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Target Group ID property of the pickup item.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>Represents the Enable Group property of the pickup item.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool EnableGroup
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="PickupItem"/> class.</summary>
        /// <param name="objectID">The object ID of the pickup item.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public PickupItem(int objectID, double x, double y)
            : base(objectID, x, y) { }
    }
}
