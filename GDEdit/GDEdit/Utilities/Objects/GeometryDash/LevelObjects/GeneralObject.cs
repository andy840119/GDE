using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Objects.General;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects
{
    // TODO: Remove LevelObject entirely once completely migrated to this
    /// <summary>Represents a general object.</summary>
    public class GeneralObject
    {
        private double rotation;

        /// <summary>The Object ID of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.ID)]
        public int ObjectID;
        /// <summary>The X position of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.X)]
        public double X;
        /// <summary>The Y position of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.Y)]
        public double Y;
        /// <summary>Determines whether this object is flipped horizontally or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.FlippedHorizontally)]
        public bool FlippedHorizontally;
        /// <summary>Determines whether this object is flipped vertically or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.FlippedVertically)]
        public bool FlippedVertically;
        /// <summary>The rotation of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.Rotation)]
        public double Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                if (rotation > 360 || rotation < -360)
                    rotation %= 360;
            }
        }
        /// <summary>The scaling of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.Scaling)]
        public double Scaling;
        /// <summary>The Editor Layer 1 of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.EL1)]
        public int EL1;
        /// <summary>The Editor Layer 2 of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.EL2)]
        public int EL2;
        /// <summary>The Z Layer of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.ZLayer)]
        public int ZLayer;
        /// <summary>The Z Order of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.ZOrder)]
        public int ZOrder;
        /// <summary>The linked group ID of this object.</summary>
        [ObjectStringMappable((int)ObjectParameter.LinkedGroupID)]
        public int LinkedGroupID;
        /// <summary>Determines whether this object is the group parent or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.GroupParent)]
        public bool GroupParent;
        /// <summary>Determines whether this object is for high detail or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.HighDetail)]
        public bool HighDetail;
        /// <summary>Determines whether this object should have an entrance effect or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.DontEnter)]
        public bool DontEnter;
        /// <summary>Determines whether this object should have the fade in and out disabled or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.DontFade)]
        public bool DontFade;
        /// <summary>Determines whether this object should have its glow disabled or not.</summary>
        [ObjectStringMappable((int)ObjectParameter.DisableGlow)]
        public bool DisableGlow;

        /// <summary>Gets or sets a <seealso cref="Point"/> instance with the location of the object.</summary>
        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>The rotation of this object in degrees according to math.</summary>
        public double MathRotationDegrees
        {
            get => -Rotation;
            set => Rotation = -value;
        }
        /// <summary>The rotation of this object in radians according to math.</summary>
        public double MathRotationRadians
        {
            get => MathRotationDegrees * Math.PI / 180;
            set => MathRotationDegrees = value * 180 / Math.PI;
        }

        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class with the object ID parameter set to 1.</summary>
        public GeneralObject()
        {
            ObjectID = 1;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID)
        {
            ObjectID = objectID;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="rotation">The rotation of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y, double rotation)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
            Rotation = rotation;
        }

        /// <summary>Returns a clone of this object.</summary>
        public GeneralObject Clone() => (GeneralObject)MemberwiseClone();

        /// <summary>Determines whether the object's location is within a rectangle.</summary>
        /// <param name="startingX">The starting X position of the rectangle.</param>
        /// <param name="startingY">The starting Y position of the rectangle.</param>
        /// <param name="endingX">The ending X position of the rectangle.</param>
        /// <param name="endingY">The ending Y position of the rectangle.</param>
        public bool IsWithinRange(double startingX, double startingY, double endingX, double endingY) => startingX <= X && endingX >= X && startingY <= Y && endingY >= Y;
    }
}
