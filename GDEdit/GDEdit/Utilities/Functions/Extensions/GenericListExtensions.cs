using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Functions.Extensions
{
    /// <summary>Provides generic extension methods for lists.</summary>
    public static class GenericListExtensions
    {
        #region Cloning
        public static List<T> Clone<T>(this List<T> l)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < l.Count; i++)
                result.Add(l[i]);
            return result;
        }
        public static List<List<T>> Clone<T>(this List<List<T>> l)
        {
            List<List<T>> result = new List<List<T>>();
            for (int i = 0; i < l.Count; i++)
            {
                result.Add(new List<T>());
                for (int j = 0; j < l[i].Count; j++)
                    result[i].Add(l[i][j]);
            }
            return result;
        }
        #endregion

        #region Contain Checks
        public static bool ContainsAll<T>(this List<T> list, List<T> containedList)
        {
            if (containedList.Count != list.Count)
                return false;
            List<T> tempList = list.Clone();
            List<T> tempContained = containedList.Clone();
            for (int i = 0; i < tempContained.Count; i++)
                tempList.Remove(tempContained[i]);
            return tempList.Count == 0;
        }
        public static bool ContainsUnordered<T>(this List<T> list, List<T> containedList)
        {
            List<T> tempList = containedList.Clone();
            List<T> tempContained = containedList.Clone();
            for (int i = 0; i < tempList.Count; i++)
                tempList.Remove(tempContained[i]);
            return list.Count - containedList.Count == tempList.Count;
        }
        #endregion

        #region Intradimensional
        public static int[] GetLengths<T>(this List<T[]> l)
        {
            int[] lengths = new int[l.Count];
            for (int i = 0; i < l.Count; i++)
                lengths[i] = l[i].Length;
            return lengths;
        }
        public static T[,] ToTwoDimensionalArray<T>(this List<T[]> l)
        {
            T[,] ar = new T[l.Count, l.GetLengths().Max()];
            for (int i = 0; i < l.Count; i++)
                for (int j = 0; j < l[i].Length; j++)
                    ar[i, j] = l[i][j];
            return ar;
        }
        #endregion
    }
}
