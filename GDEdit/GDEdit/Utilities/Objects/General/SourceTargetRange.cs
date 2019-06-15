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
        private int sourceFrom, sourceTo, targetFrom;

        public int SourceFrom
        {
            get => sourceFrom;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom = value, sourceTo, targetFrom, TargetTo);
        }
        public int SourceTo
        {
            get => sourceTo;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom, sourceTo = value, targetFrom, TargetTo);
        }
        public int TargetFrom
        {
            get => targetFrom;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom, sourceTo, targetFrom = value, TargetTo);
        }
        public int TargetTo => targetFrom + Range;

        public SourceTargetRange(int sourceFrom, int sourceTo, int targetFrom, int targetTo)
        public int Range => sourceTo - sourceFrom;
        public int Difference => targetFrom - sourceFrom;
        public event SourceTargetRangeChanged SourceTargetRangeChanged;
        public SourceTargetRange(int sourceStart, int sourceEnd, int targetStart)
        {
            sourceFrom = sourceStart;
            sourceTo = sourceEnd;
            targetFrom = targetStart;
        }

        public bool IsWithinSourceRange(int value) => sourceFrom <= value && value <= sourceTo;
        public void AdjustSourceFrom(int adjustment, bool maintainDifference = true)
        {
            sourceFrom += adjustment;
            if (maintainDifference)
                sourceTo += adjustment;
        }

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
            return new SourceTargetRange(ToInt32(split[0, 0]), ToInt32(split[0, length1 - 1]), ToInt32(split[1, 0]));
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

    public delegate void SourceTargetRangeChanged(int sourceFrom, int sourceTo, int targetFrom, int targetTo);
}
