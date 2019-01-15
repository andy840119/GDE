using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectSets
{
    /// <summary>Represents an object set, which consists of a number of objects for specific purposes.</summary>
    public class ObjectSet
    {
        /// <summary>The dictionary containing the object grids mapped per the points in the <seealso cref="ObjectSet"/>.</summary>
        public Dictionary<RectanglePoints, ObjectGrid> Rectangles { get; set; }

        // TODO: Support multiple slope point combinations
        /// <summary>The dictionary containing the object grids mappes per the slopes in the <seealso cref="ObjectSet"/>.</summary>
        public Dictionary<SlopeType, ObjectGrid> Slopes { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSet"/> class.</summary>
        public ObjectSet() { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSet"/> class.</summary>
        /// <param name="rectangles">The dictionary containing the set of objects.</param>
        public ObjectSet(Dictionary<RectanglePoints, ObjectGrid> rectangles)
        {
            Rectangles = rectangles;
        }

        /// <summary>Gets or sets the object grid in the object set.</summary>
        /// <param name="s">The points of the rectangle in the object set.</param>
        public ObjectGrid this[RectanglePoints s]
        {
            get => Rectangles[s];
            set => Rectangles[s] = value;
        }
    }
    
    /// <summary>Represents the points of a rectangle.</summary>
    public enum RectanglePoints
    {
        /// <summary>No points.</summary>
        None = 0,

        /// <summary>The top left point.</summary>
        TopLeft = 1,
        /// <summary>The top right point.</summary>
        TopRight = 1 << 1,
        /// <summary>The bottom left point.</summary>
        BottomLeft = 1 << 2,
        /// <summary>The bottom right point.</summary>
        BottomRight = 1 << 3,

        /// <summary>The top side.</summary>
        TopSide = TopLeft | TopRight,
        /// <summary>The left side.</summary>
        LeftSide = TopLeft | BottomLeft,
        /// <summary>The bottom side.</summary>
        BottomSide = BottomLeft | BottomRight,
        /// <summary>The right side.</summary>
        RightSide = TopRight | BottomRight,

        /// <summary>The vertical sides (left and right).</summary>
        VerticalSides = LeftSide | RightSide,
        /// <summary>The horizontal sides (top and bottom).</summary>
        HorizontalSides = TopSide | BottomSide,

        /// <summary>All the points.</summary>
        All = VerticalSides | HorizontalSides,
    }

    /// <summary>Represents a slope type.</summary>
    public enum SlopeType
    {
        /// <summary>Represents the 45-degree slope (1:1).</summary>
        Slope45 = 1,
        /// <summary>Represents the ~26-degree slope (2:1).</summary>
        Slope26 = 2,
    }
}
