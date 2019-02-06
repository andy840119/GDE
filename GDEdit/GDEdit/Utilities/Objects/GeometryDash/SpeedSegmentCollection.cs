using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a sorted speed segment collection.</summary>
    public class SpeedSegmentCollection : SortedSet<SpeedSegment>
    {
        /// <summary>Initializes a new instance of the <seealso cref="SpeedSegmentCollection"/> class.</summary>
        public SpeedSegmentCollection() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpeedSegmentCollection"/> class.</summary>
        /// <param name="segments">The speed segments to create this <seealso cref="SpeedSegmentCollection"/> from.</param>
        public SpeedSegmentCollection(SortedSet<SpeedSegment> segments) : base(segments) { }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get or set.</param>
        public SpeedSegment this[int index]
        {
            get => this.ElementAt(index);
            set
            {
                // Why does it have to be like that?
                Remove(this.ElementAt(index));
                Add(value);
            }
        }
    }
}
