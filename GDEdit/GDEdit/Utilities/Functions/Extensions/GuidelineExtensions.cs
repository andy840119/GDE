using GDEdit.Utilities.Objects.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Functions.Extensions
{
    /// <summary>Provides generic extension methods for <seealso cref="Guideline"/>s.</summary>
    public static class GuidelineExtensions
    {
        /// <summary>Adds a <seealso cref="Guideline"/> into the list and returns the instance of the list.</summary>
        /// <param name="l">The list to add the <seealso cref="Guideline"/> into.</param>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public static List<Guideline> Add(this List<Guideline> l, double timeStamp, double color)
        {
            l.Add(new Guideline(timeStamp, color));
            return l;
        }
        /// <summary>Inserts a <seealso cref="Guideline"/> into the list at a specified index and returns the instance of the list.</summary>
        /// <param name="l">The list to add the <seealso cref="Guideline"/> into.</param>
        /// <param name="index">The index to insert the <seealso cref="Guideline"/> at.</param>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public static List<Guideline> Insert(this List<Guideline> l, int index, double timeStamp, double color)
        {
            l.Insert(index, new Guideline(timeStamp, color));
            return l;
        }
        /// <summary>Returns the index to insert a <seealso cref="Guideline"/> into the list.</summary>
        /// <param name="l">The list to add the <seealso cref="Guideline"/> into.</param>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        public static int FindIndexToInsertGuideline(this List<Guideline> l, double timeStamp)
        {
            if (l.Count == 0)
                return 0;
            for (int i = 0; i < l.Count; i++)
                if (l[i].TimeStamp > timeStamp)
                    return i;
            return l.Count;
        }
        /// <summary>Returns the guideline string of a list of guidelines.</summary>
        /// <param name="guidelines">The list of guidelines to get the guideline string of.</param>
        public static string GetGuidelineString(this List<Guideline> guidelines)
        {
            StringBuilder result = new StringBuilder();
            foreach (var g in guidelines)
                result.Append($"{g}~");
            return result.ToString();
        }
    }
}
