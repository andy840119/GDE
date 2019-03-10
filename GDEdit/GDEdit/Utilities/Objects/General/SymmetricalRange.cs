using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General
{
    /// <summary>Represents a symmetrical range.</summary>
    /// <typeparam name="T">The type of the value range.</typeparam>
    public struct SymmetricalRange<T>
    {
        /// <summary>The middle value of the range.</summary>
        public T MiddleValue;
        /// <summary>The maximum distance from the middle value.</summary>
        public T MaximumDistance;

        /// <summary>Initializes a new instance of the <seealso cref="SymmetricalRange{T}"/> struct.</summary>
        /// <param name="middleValue">The middle value of the range.</param>
        /// <param name="maximumDistance">The maximum distance from the middle value.</param>
        public SymmetricalRange(T middleValue, T maximumDistance)
        {
            MiddleValue = middleValue;
            MaximumDistance = maximumDistance;
        }
    }
}
