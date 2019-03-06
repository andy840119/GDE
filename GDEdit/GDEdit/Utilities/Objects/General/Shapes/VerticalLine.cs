using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a vertical line shape.</summary>
    public class VerticalLine : Line
    {
        /// <summary>Initializes a new instance of the <seealso cref="VerticalLine"/> class.</summary>
        public VerticalLine() : base(90) { }

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point) => point.X == 0;
    }
}
