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

        #region Constants
        /// <summary>The big movement step in units.</summary>
        public const double BigMovementStep = 150;
        /// <summary>The normal movement step in units.</summary>
        public const double NormalMovementStep = 30;
        /// <summary>The small movement step in units.</summary>
        public const double SmallMovementStep = 2;
        /// <summary>The tiny movement step in units.</summary>
        public const double TinyMovementStep = 0.5;
        #endregion

        #region Level
        /// <summary>The currently edited level.</summary>
        public Level Level;
        /// <summary>The currently selected objects.</summary>
        public LevelObjectCollection SelectedObjects = new LevelObjectCollection();
        /// <summary>The objects that are copied into the clipboard and can be pasted.</summary>
        public LevelObjectCollection ObjectClipboard = new LevelObjectCollection();
        #endregion

        #region Functions
        /// <summary>Indicates whether the Swipe option is enabled or not.</summary>
        public bool Swipe;
        /// <summary>Indicates whether the Grid Snap option is enabled or not.</summary>
        public bool GridSnap;
        /// <summary>Indicates whether the Free Move option is enabled or not.</summary>
        public bool FreeMove;
        #endregion

        #region Editor Preferences
        /// <summary>The customly defined movement steps in the editor.</summary>
        public List<double> CustomMovementSteps;
        /// <summary>The customly defined rotation steps in the editor.</summary>
        public List<double> CustomRotationSteps;
        #endregion

        #region Camera
        /// <summary>The size of each grid block in the editor.</summary>
        public double GridSize { get; private set; } = 30;
        /// <summary>The camera zoom in the editor.</summary>
        public double Zoom { get; set; } = 1;

        /// <summary>Gets or sets indicating whether the editor is in dual layer mode.</summary>
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
        #endregion

        #region Events
        /// <summary>Occurs when the dual layer mode has been changed, including the new status.</summary>
        public event Action<bool> DualLayerModeChanged;
        /// <summary>Occurs when new objects have been added to the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsAdded;
        /// <summary>Occurs when new objects have been removed from the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsRemoved;
        /// <summary>Occurs when all objects have been deselected.</summary>
        public event Action AllObjectsDeselected;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <seealso cref="Editor"/> class.</summary>
        /// <param name="level">The level to edit.</param>
        public Editor(Level level)
        {
            Level = level;
        }
        #endregion

        #region Object Selection
        /// <summary>Selects a number of objects.</summary>
        /// <param name="objects">The objects to add to the selection.</param>
        public void SelectObjects(LevelObjectCollection objects)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.AddRange(objects);
            SelectedObjectsAdded?.Invoke(objects);
        }
        /// <summary>Deselects a number of objects.</summary>
        /// <param name="objects">The objects to remove from the selection.</param>
        public void DeselectObjects(LevelObjectCollection objects)
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
        public LevelObjectCollection GetObjectsWithinRange(double startingX, double startingY, double endingX, double endingY)
        {
            // Fix starting/ending points to avoid having to fix in the editor itself
            if (startingX > endingX)
                Swap(ref startingX, ref endingX);
            if (startingY > endingY)
                Swap(ref startingY, ref endingY);
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.IsWithinRange(startingX, startingY, endingX, endingY))
                    result.Add(o);
            return result;
        }
        /// <summary>Returns all the objects that are in a specific layer.</summary>
        /// <param name="EL">The editor layer which contains the objects to retrieve.</param>
        public LevelObjectCollection GetObjectsByLayer(int EL)
        {
            if (EL == -1) // Indicates the All layer
                return Level.LevelObjects;
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.EL1 == EL || o.EL2 == EL)
                    result.Add(o);
            return result;
        }
        /// <summary>Returns a collection of objects based on a predicate.</summary>
        /// <param name="predicate">The predicate to determine the resulting object collection.</param>
        public LevelObjectCollection GetObjects(Predicate<GeneralObject> predicate)
        {
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (predicate(o))
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
                foreach (var o in SelectedObjects)
                    o.X += x;
        }
        /// <summary>Moves the selected objects by an amount on the Y axis.</summary>
        /// <param name="y">The offset of Y to move the objects by.</param>
        public void MoveY(double y)
        {
            if (y != 0)
                foreach (var o in SelectedObjects)
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
            foreach (var o in SelectedObjects)
                o.Rotation += rotation;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in SelectedObjects)
                    o.Location = o.Location.Rotate(median, rotation);
            }
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects.</param>
        public void Rotate(double rotation, Point center)
        {
            foreach (var o in SelectedObjects)
            {
                o.Rotation += rotation;
                o.Location = o.Location.Rotate(center, rotation);
            }
        }
        #endregion
        #region Object Scaling
        /// <summary>Scales the selected objects by an amount.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        public void Scale(double scaling, bool individually)
        {
            foreach (var o in SelectedObjects)
                o.Scaling *= scaling;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in SelectedObjects)
                    o.Location = (median - o.Location) * scaling + median;
            }
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects.</param>
        public void Scale(double scaling, Point center)
        {
            foreach (var o in SelectedObjects)
            {
                o.Scaling *= scaling;
                o.Location = (center - o.Location) * scaling + center;
            }
        }
        #endregion
        #region Object Flipping
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        public void FlipHorizontally(bool individually)
        {
            foreach (var o in SelectedObjects)
                o.FlippedHorizontally = !o.FlippedHorizontally;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in SelectedObjects)
                    o.X = 2 * median.X - o.X;
            }
        }
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        public void FlipHorizontally(Point center)
        {
            foreach (var o in SelectedObjects)
            {
                o.FlippedHorizontally = !o.FlippedHorizontally;
                o.X = 2 * center.X - o.X;
            }
        }
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        public void FlipVertically(bool individually)
        {
            foreach (var o in SelectedObjects)
                o.FlippedVertically = !o.FlippedVertically;
            if (!individually)
            {
                var median = GetMedianPoint();
                foreach (var o in SelectedObjects)
                    o.Y = 2 * median.Y - o.Y;
            }
        }
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        public void FlipVertically(Point center)
        {
            foreach (var o in SelectedObjects)
            {
                o.FlippedVertically = !o.FlippedVertically;
                o.Y = 2 * center.Y - o.Y;
            }
        }
        #endregion
        #endregion

        #region Editor Functions
        /// <summary>Copy the selected objects and add them to the clipboard.</summary>
        public void Copy()
        {
            ObjectClipboard.Clear();
            ObjectClipboard.AddRange(SelectedObjects);
        }
        /// <summary>Pastes the copied objects on the clipboard provided a central position to place them.</summary>
        /// <param name="center">The central position which will determine the position of the pasted objects.</param>
        public void Paste(Point center)
        {
            if (ObjectClipboard.Count == 0)
                return;
            var distance = center - GetMedianPoint(ObjectClipboard);
            var newObjects = ObjectClipboard.Clone();
            foreach (var o in newObjects)
                o.Location += distance;
            Level.LevelObjects.AddRange(newObjects);
        }
        /// <summary>ViPriNizes all the selected objects.</summary>
        public void CopyPaste() => Level.LevelObjects.AddRange(SelectedObjects.Clone());

        /// <summary>Resets the unused color channels.</summary>
        public void ResetUnusedColors()
        {
            HashSet<int> usedIDs = new HashSet<int>();
            foreach (var o in Level.LevelObjects)
            {
                usedIDs.Add(o.Color1ID);
                usedIDs.Add(o.Color2ID);
            }
            HashSet<int> copiedIDs = new HashSet<int>();
            for (int i = 0; i < 1000; i++)
                copiedIDs.Add(Level.ColorChannels[i].CopiedColorID);
            foreach (var c in copiedIDs)
                usedIDs.Add(c);
            usedIDs.Remove(0);
            usedIDs.RemoveWhere(k => k > 999);
            for (int i = 0; i < 1000; i++)
                if (!usedIDs.Contains(i))
                    Level.ColorChannels[i].Reset();
        }
        #endregion

        #region Editor Camera
        /// <summary>Reduces the grid size by setting it to half the original amount.</summary>
        public void ReduceGridSize() => GridSize /= 2;
        /// <summary>Increases the grid size by setting it to double the original amount.</summary>
        public void IncreaseGridSize() => GridSize *= 2;
        /// <summary>Reduces the camera zoom in the editor by 0.1x.</summary>
        public void ReduceZoom()
        {
            if (Zoom > 0.1)
                Zoom -= 0.1;
        }
        /// <summary>Increases the camera zoom in the editor by 0.1x.</summary>
        public void IncreaseZoom()
        {
            if (Zoom < 4.9) // Good threshold?
                Zoom += 0.1;
        }
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
        /// <summary>Gets the median point of the specified objects.</summary>
        private Point GetMedianPoint(LevelObjectCollection objects)
        {
            Point result = new Point();
            foreach (var o in objects)
                result += o.Location;
            return result / objects.Count;
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
