using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a shape.</summary>
    public abstract class Shape
    {
        /// <summary>Initializes a new instance of the <seealso cref="Shape"/> class.</summary>
        public Shape() { }

        /// <summary>Returns the distance between the center of the shape and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public abstract double GetRadiusAtRotation(double rotation);
        /// <summary>Returns the maximum distance between the center of the shape and its edge.</summary>
        public abstract double GetMaxRadius();

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public abstract bool IsPointWithinShape(Point point);
    }
}
