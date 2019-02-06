using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDEdit.Utilities.Information.GeometryDash.Speeds;

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

        /// <summary>Converts the provided X position into time.</summary>
        /// <param name="x">The X position to convert into time.</param>
        public double ConvertXToTime(double x)
        {
            double time = 0;
            int i = 1;
            for (; i < Count && this[i].X < x; i++)
                time += (this[i].X - this[i - 1].X) * GetSpeed(this[i - 1].Speed);
            int final = Math.Max(i, Count);
            return time + (x - this[final].X) * GetSpeed(this[final].Speed);
        }
        /// <summary>Converts the provided time into X position.</summary>
        /// <param name="time">The time to convert into X position.</param>
        public double ConvertTimeToX(double time)
        {
            double t = 0;
            int i = 1;
            for (; i < Count && t < time; i++)
                t += (this[i].X - this[i - 1].X) / GetSpeed(this[i - 1].Speed);
            int final = Math.Max(i, Count) - 1;
            return this[final].X + (t - time) * GetSpeed(this[final].Speed);
        }

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
