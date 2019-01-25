using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General
{
    /// <summary>Represents a point in the editor.</summary>
    public struct Point
    {
        /// <summary>The X location of the point.</summary>
        public double X;
        /// <summary>The Y location of the point.</summary>
        public double Y;

        /// <summary>Initializes a new instance of the <seealso cref="Point"/> struct.</summary>
        /// <param name="both">The value of both locations of the point.</param>
        public Point(double both)
        {
            X = Y = both;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Point"/> struct.</summary>
        /// <param name="x">The X location of the point.</param>
        /// <param name="y">The Y location of the point.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Returns the distance from another point.</summary>
        /// <param name="p">The other point to calculate the distance from.</param>
        public double DistanceFrom(Point p)
        {
            var d = p - this;
            return Math.Sqrt(d.X * d.X + d.Y * d.Y);
        }

        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
        public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
        public static Point operator *(int left, Point right) => new Point(left * right.X, left * right.Y);
        public static Point operator *(Point left, int right) => new Point(left.X * right, left.Y * right);
        public static Point operator *(double left, Point right) => new Point(left * right.X, left * right.Y);
        public static Point operator *(Point left, double right) => new Point(left.X * right, left.Y * right);
        public static Point operator /(int left, Point right) => new Point(left / right.X, left / right.Y);
        public static Point operator /(Point left, int right) => new Point(left.X / right, left.Y / right);
        public static Point operator /(double left, Point right) => new Point(left / right.X, left / right.Y);
        public static Point operator /(Point left, double right) => new Point(left.X / right, left.Y / right);
        public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Point left, Point right) => left.X != right.X && left.Y != right.Y;
        public static bool operator <=(Point left, Point right) => left.X <= right.X && left.Y <= right.Y;
        public static bool operator >=(Point left, Point right) => left.X >= right.X && left.Y >= right.Y;
        public static bool operator <(Point left, Point right) => left.X < right.X && left.Y < right.Y;
        public static bool operator >(Point left, Point right) => left.X > right.X && left.Y > right.Y;
    }
}
