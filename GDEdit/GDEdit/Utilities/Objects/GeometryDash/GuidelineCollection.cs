using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a collection of guidelines.</summary>
    public class GuidelineCollection
    {
        private List<Guideline> g;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineCollection"/> class.</summary>
        public GuidelineCollection() : this(new List<Guideline>()) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineCollection"/> class.</summary>
        /// <param name="guidelines">The guidelines to create the collection out of.</param>
        public GuidelineCollection(List<Guideline> guidelines)
        {
            g = guidelines;
        }

        /// <summary>Adds a <seealso cref="Guideline"/> to the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guideline">The guideline to add to the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Add(Guideline guideline)
        {
            g.Add(guideline);
            return this;
        }
        /// <summary>Inserts a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> at a specified index and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index to insert the <seealso cref="Guideline"/> at.</param>
        /// <param name="guideline">The guideline to insert into the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Insert(int index, Guideline guideline)
        {
            g.Insert(index, guideline);
            return this;
        }
        /// <summary>Removes a <seealso cref="Guideline"/> from the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guideline">The guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Remove(Guideline guideline)
        {
            g.Remove(guideline);
            return this;
        }
        /// <summary>Removes the <seealso cref="Guideline"/> at the specified index from the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index of the guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection RemoveAt(int index)
        {
            g.RemoveAt(index);
            return this;
        }
        /// <summary>Clears the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index of the guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Clear()
        {
            g.Clear();
            return this;
        }
        /// <summary>Adds a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public GuidelineCollection Add(double timeStamp, double color)
        {
            g.Add(new Guideline(timeStamp, color));
            return this;
        }
        /// <summary>Inserts a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> at a specified index and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index to insert the <seealso cref="Guideline"/> at.</param>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public GuidelineCollection Insert(int index, double timeStamp, double color)
        {
            g.Insert(index, new Guideline(timeStamp, color));
            return this;
        }
        /// <summary>Returns the index to insert a <seealso cref="Guideline"/> into the list.</summary>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        public int FindIndexToInsertGuideline(double timeStamp)
        {
            int min = 0;
            int max = g.Count;
            int mid = 0;
            while (min < max)
            {
                mid = (min + max) / 2;
                if (timeStamp == g[mid].TimeStamp)
                    return mid; // Why do you want the same timestamp to be included?
                if (timeStamp < g[mid].TimeStamp)
                    min = mid + 1;
                else
                    max = mid;
            }
            return mid;
        }

        /// <summary>Gets or sets the <seealso cref="Guideline"/> at a specified index.</summary>
        /// <param name="index">The index of the <seealso cref="Guideline"/> to get or set.</param>
        public Guideline this[int index]
        {
            get => g[index];
            set => g[index] = value;
        }

        /// <summary>Returns the guideline string of the <seealso cref="GuidelineCollection"/>.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var g in g)
                result.Append($"{g}~");
            return result.ToString();
        }
    }
}
