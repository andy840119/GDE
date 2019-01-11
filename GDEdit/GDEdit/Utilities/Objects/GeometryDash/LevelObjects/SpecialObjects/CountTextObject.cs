using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a count text object.</summary>
    public class CountTextObject : SpecialObject, IHasPrimaryItemID
    {
        private short itemID;

        /// <summary>The object ID of the count text block.</summary>
        public new int ObjectID => (int)SpecialObjectType.CountTextObject;

        /// <summary>The Item ID the count text object displays.</summary>
        public int ItemID
        {
            get => itemID;
            set => itemID = (short)value;
        }
        /// <summary>The Item ID the count text object displays.</summary>
        public int PrimaryItemID
        {
            get => ItemID;
            set => ItemID = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CountTextObject"/> class.</summary>
        public CountTextObject() : base() { }
    }
}
