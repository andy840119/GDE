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
        // TODO: Implement slope support too

        /// <summary>The dictionary containing the objects mapped per the points in the <seealso cref="ObjectSet"/>.</summary>
        public Dictionary<SquarePoints, ObjectGrid> Objects { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSet"/> class.</summary>
        public ObjectSet() { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSet"/> class.</summary>
        /// <param name="objects">The dictionary containing the set of objects.</param>
        public ObjectSet(Dictionary<SquarePoints, ObjectGrid> objects)
        {
            Objects = objects;
        }

        /// <summary>Gets or sets the list of general objects in the object set.</summary>
        /// <param name="s">The points of the square in the object that will be considered</param>
        public ObjectGrid this[SquarePoints s]
        {
            get => Objects[s];
            set => Objects[s] = value;
        }
    }
    
    /// <summary>Represents the points of a square.</summary>
    public enum SquarePoints
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
}
