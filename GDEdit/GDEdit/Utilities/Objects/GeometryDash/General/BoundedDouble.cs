using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.General
{
    /// <summary>Represents a value which is bounded within the minimum and the maximum bounds.</summary>
    public class BoundedDouble
    {
        private double v;

        /// <summary>The minimum value of the <seealso cref="BoundedDouble"/>.</summary>
        public double Min { get; set; }
        /// <summary>The maximum value of the <seealso cref="BoundedDouble"/>.</summary>
        public double Max { get; set; }
        /// <summary>The value of the <seealso cref="BoundedDouble"/>.</summary>
        public double Value
        {
            get => v;
            set
            {
                if (value < Min)
                    value = Min;
                if (value > Max)
                    value = Max;
                v = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="BoundedDouble"/> class.</summary>
        /// <param name="value">The value of the <seealso cref="BoundedDouble"/>.</param>
        /// <param name="min">The minimum value of the <seealso cref="BoundedDouble"/>.</param>
        /// <param name="max">The maximum value of the <seealso cref="BoundedDouble"/>.</param>
        public BoundedDouble(double value, double min, double max)
        {
            Min = min;
            Max = max;
            Value = value;
        }

        public static implicit operator double(BoundedDouble d) => d.Value;
    }
}
