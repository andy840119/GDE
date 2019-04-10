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
        private Point position;

        /// <summary>The position of the shape.</summary>
        public Point Position
        {
            get => position;
            set => position = value;
        }
        /// <summary>The X position of the shape.</summary>
        public double X
        {
            get => position.X;
            set => position.X = value;
        }
        /// <summary>The Y position of the shape.</summary>
        public double Y
        {
            get => position.Y;
            set => position.Y = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Shape"/> class.</summary>
        /// <param name="position">The position of the shape.</param>
        public Shape(Point position) => Position = position;

        /// <summary>Determines whether this shape overlaps with another shape.</summary>
        /// <param name="shape">The shape to check whether it overlaps with this shape.</param>
        public bool OverlapsWithAnotherShape(Shape shape)
        {
            double distance = Position.DistanceFrom(shape.Position);
            double deg = Position.GetAngle(shape.Position) * 180 / Math.PI;
            double a = GetRadiusAtRotation(deg);
            double b = shape.GetRadiusAtRotation(-deg);
            return a + b <= distance;
        }
        /// <summary>Determines whether this shape contains another shape.</summary>
        /// <param name="shape">The shape to check whether it is contained this shape.</param>
        public bool ContainsShape(Shape shape)
        {
            double distance = Position.DistanceFrom(shape.Position);
            double a = GetMaxRadius();
            double b = shape.GetMaxRadius();
            return b + distance <= a;
        }

        /// <summary>Returns the distance between the center of the shape and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public abstract double GetRadiusAtRotation(double rotation);
        /// <summary>Returns the maximum distance between the center of the shape and its edge.</summary>
        public abstract double GetMaxRadius();

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public abstract bool ContainsPoint(Point point);
    }
}
