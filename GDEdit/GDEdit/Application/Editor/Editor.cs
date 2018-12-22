using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Application.Editor
{
    /// <summary>The editor which edits a level.</summary>
    public class Editor
    {
        private bool dualLayerMode;

        /// <summary>The currently edited level.</summary>
        public Level Level;
        // TODO: Change all implementations of type List<GeneralObject> to LevelObjectList
        /// <summary>The currently selected objects.</summary>
        public List<GeneralObject> SelectedObjects = new List<GeneralObject>();

        /// <summary>Indicates whether the editor is in dual layer mode.</summary>
        public bool DualLayerMode
        {
            get => dualLayerMode;
            set
            {
                if (value == dualLayerMode)
                    return;
                dualLayerMode = value;
                DualLayerModeChanged?.Invoke(value);
            }
        }

        /// <summary>Occurs when the dual layer mode has been changed, including the new status.</summary>
        public event Action<bool> DualLayerModeChanged;
        /// <summary>Occurs when new objects have been added to the selection list.</summary>
        public event Action<List<GeneralObject>> SelectedObjectsAdded;
        /// <summary>Occurs when new objects have been removed from the selection list.</summary>
        public event Action<List<GeneralObject>> SelectedObjectsRemoved;
        /// <summary>Occurs when all objects have been deselected.</summary>
        public event Action AllObjectsDeselected;

        /// <summary>Initializes a new instance of the <seealso cref="Editor"/> class.</summary>
        /// <param name="level">The level to edit.</param>
        public Editor(Level level)
        {
            Level = level;
        }

        #region Object Selection
        /// <summary>Selects a number of objects.</summary>
        /// <param name="objects">The objects to add to the selection.</param>
        public void SelectObjects(List<GeneralObject> objects)
        {
            SelectedObjects.AddRange(objects);
            SelectedObjectsAdded?.Invoke(objects);
        }
        /// <summary>Deselects a number of objects.</summary>
        /// <param name="objects">The objects to remove from the selection.</param>
        public void DeselectObjects(List<GeneralObject> objects)
        {
            foreach (var o in objects)
                SelectedObjects.Remove(o);
            SelectedObjectsRemoved?.Invoke(objects);
        }
        /// <summary>Selects all objects.</summary>
        public void SelectAll()
        {
            SelectedObjects.AddRange(Level.LevelObjects);
            SelectedObjectsAdded?.Invoke(Level.LevelObjects);
        }
        /// <summary>Deselects all objects.</summary>
        public void DeselectAll()
        {
            SelectedObjects.Clear();
            AllObjectsDeselected?.Invoke();
        }
        #endregion

        #region Object Filtering
        /// <summary>Returns the object within a rectangular range.</summary>
        /// <param name="startingX">The X of the starting point.</param>
        /// <param name="startingY">The Y of the starting point.</param>
        /// <param name="endingX">The X of the ending point.</param>
        /// <param name="endingY">The Y of the ending point.</param>
        public List<GeneralObject> GetObjectsWithinRange(double startingX, double startingY, double endingX, double endingY)
        {
            // Fix starting/ending points to avoid having to fix in the editor itself
            if (startingX > endingX)
                Swap(ref startingX, ref endingX);
            if (startingY > endingY)
                Swap(ref startingY, ref endingY);
            List<GeneralObject> result = new List<GeneralObject>();
            foreach (var o in Level.LevelObjects)
                if (o.IsWithinRange(startingX, startingY, endingX, endingY))
                    result.Add(o);
            return result;
        }
        #endregion

        #region Object Editing
        // Add events for these methods?
        #region Object Movement
        /// <summary>Moves the selected objects by an amount on the X axis.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        public void MoveX(double x)
        {
            if (x != 0)
                foreach (var o in Level.LevelObjects)
                    o.X += x;
        }
        /// <summary>Moves the selected objects by an amount on the Y axis.</summary>
        /// <param name="y">The offset of Y to move the objects by.</param>
        public void MoveY(double y)
        {
            if (y != 0)
                foreach (var o in Level.LevelObjects)
                    o.Y += y;
        }
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        public void Move(double x, double y)
        {
            MoveX(x);
            MoveY(y);
        }
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        public void Move(Point p) => Move(p.X, p.Y);
        #endregion
        #region Object Rotation
        /// <summary>Rotates the selected objects by an amount.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        public void Rotate(double rotation, bool individually)
        {
            foreach (var o in Level.LevelObjects)
                o.Rotation += rotation;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in Level.LevelObjects)
                {
                    double distance = o.Location.DistanceFrom(median);
                    double xDistance = o.X - median.X;
                    double rad = (Math.Asin(distance / xDistance) + rotation) * Math.PI / 180;
                    double x = Math.Cos(rad) * distance;
                    double y = Math.Sin(rad) * distance;
                    o.Location = median + new Point(x, y);
                }
            }
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="median">The central point to take into account while rotating all objects.</param>
        public void Rotate(double rotation, Point center)
        {
            foreach (var o in Level.LevelObjects)
            {
                o.Rotation += rotation;
                double distance = o.Location.DistanceFrom(center);
                double xDistance = o.X - center.X;
                double rad = (Math.Asin(distance / xDistance) + rotation) * Math.PI / 180;
                double x = Math.Cos(rad) * distance;
                double y = Math.Sin(rad) * distance;
                o.Location = center + new Point(x, y);
            }
        }
        #endregion
        #region Object Scaling
        /// <summary>Scales the selected objects by an amount.</summary>
        /// <param name="rotation">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        public void Scale(double scaling, bool individually)
        {
            foreach (var o in Level.LevelObjects)
                o.Scaling *= scaling;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in Level.LevelObjects)
                    o.Location = (median - o.Location) * scaling + median;
            }
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The scaling to apply to all the objects.</param>
        /// <param name="median">The central point to take into account while scaling all objects.</param>
        public void Scale(double scaling, Point center)
        {
            foreach (var o in Level.LevelObjects)
            {
                o.Scaling *= scaling;
                o.Location = (center - o.Location) * scaling + center;
            }
        }
        #endregion
        #endregion

        // TODO: Add functions to do lots of stuff

        #region Private Methods
        /// <summary>Gets the median point of all the selected objects.</summary>
        private Point GetMedianPoint()
        {
            Point result = new Point();
            foreach (var o in SelectedObjects)
                result += o.Location;
            return result / SelectedObjects.Count;
        }
        private void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        #endregion
    }
}
