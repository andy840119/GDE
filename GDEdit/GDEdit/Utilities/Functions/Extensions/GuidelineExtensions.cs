using GDEdit.Utilities.Objects.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Functions.Extensions
{
    public static class GuidelineExtensions
    {
        public static List<Guideline> Add(this List<Guideline> l, double timeStamp, double color)
        {
            Guideline g = new Guideline(timeStamp, color);
            l.Add(g);
            return l;
        }
        public static List<Guideline> Insert(this List<Guideline> l, int index, double timeStamp, double color)
        {
            Guideline g = new Guideline(timeStamp, color);
            l.Insert(index, g);
            return l;
        }
        public static int FindIndexToInsertGuideline(this List<Guideline> l, double timeStamp)
        {
            if (l.Count == 0)
                return 0;
            for (int i = 0; i < l.Count; i++)
                if (l[i].TimeStamp > timeStamp)
                    return i;
            return l.Count;
        }
    }
}
