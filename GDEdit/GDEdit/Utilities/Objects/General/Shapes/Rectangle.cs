using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a rectangular shape.</summary>
    public class Rectangle : WidthHeightShape, IHasWidth, IHasHeight
    {
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="both">The length of both dimensions of the rectangular shape.</param>
        public Rectangle(double both) : base(both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="width">The width of the rectangular shape.</param>
        /// <param name="height">The height of the rectangular shape.</param>
        public Rectangle(double width, double height) : base(width, height) { }
    }
}
