using GDEdit.Utilities.Functions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace GDEdit.Utilities.Objects.General
{
    public class SourceTargetRange
    {
        public int SourceFrom;
        public int SourceTo;
        public int TargetFrom;
        public int TargetTo;

        public int Range => SourceTo - SourceFrom;
        public int Difference => TargetFrom - SourceFrom;

        public SourceTargetRange(int sourceFrom, int sourceTo, int targetFrom, int targetTo)
        {
            SourceFrom = sourceFrom;
            SourceTo = sourceTo;
            TargetFrom = targetFrom;
            TargetTo = targetTo;
        }

        public bool IsWithinSourceRange(int value) => SourceFrom <= value && value <= SourceTo;

        public static SourceTargetRange Parse(string str)
        {
            string[,] split = str.Split('>').Split('-');
            int length0 = split.GetLength(0);
            int length1 = split.GetLength(1);

            for (int i = 0; i < length0; i++)
                for (int j = 0; j < length1; j++)
                {
                    while (split[i, j].First() == ' ')
                        split[i, j] = split[i, j].Remove(0, 1);
                    while (split[i, j].Last() == ' ')
                        split[i, j] = split[i, j].Remove(split[i, j].Length - 1, 1);
                }
            return new SourceTargetRange(ToInt32(split[0, 0]), ToInt32(split[0, length1 - 1]), ToInt32(split[1, 0]), ToInt32(split[1, length1 - 1]));
        }
        public static List<SourceTargetRange> LoadRangesFromStringArray(string[] lines, bool ignoreEmptyLines = true)
        {
            var list = new List<SourceTargetRange>();
            foreach (string s in lines)
                if (!ignoreEmptyLines || s != "")
                    list.Add(Parse(s));
            return list;
        }

        public override string ToString() => $"{SourceFrom}{(Range > 0 ? $"-{SourceTo}" : "")} > {TargetFrom}{(Range > 0 ? $"-{TargetTo}" : "")}";
    }
}
