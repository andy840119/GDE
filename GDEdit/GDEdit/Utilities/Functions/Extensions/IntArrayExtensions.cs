using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Functions.Extensions
{
    /// <summary>Provides generic extension methods for <seealso cref="int"/> arrays.</summary>S
    public static class IntArrayExtensions
    {
        public static string ShowValuesWithRanges(this int[] s)
        {
            s = s.Sort(); // Sort the values
            string result = "";
            if (s.Length > 0)
            {
                int lastShownValue = s[0];
                int lastValueInCombo = s[0];
                result += lastShownValue.ToString();
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] > lastValueInCombo) // Determines whether the next value is not in a row
                    {
                        result += (result[result.Length - 1] == '-' ? s[i - 1].ToString() : "") + ", " + s[i].ToString();
                        lastValueInCombo = lastShownValue = s[i] + 1;
                    }
                    else if (s[i] == lastValueInCombo) // Determines whether the next value is in a row
                    {
                        if (i < s.Length - 1) // Determines whether the current index is not the last value
                        {
                            if (lastShownValue == lastValueInCombo) // Determines whether this is the start of a new combo
                                result += "-";
                        }
                        else if (i == s.Length - 1) // Determines whether the current index is the last value
                            result += s[i].ToString();
                        lastValueInCombo = s[i] + 1; // Set the last index in the combo to the current index
                    }
                }
            }
            return result;
        }
    }
}
