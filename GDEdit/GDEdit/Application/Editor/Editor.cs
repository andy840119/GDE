using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
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

        #region Editor Function Toggles
        /// <summary>Indicates whether the Swipe option is enabled or not.</summary>
        public bool Swipe { get; set; }
        /// <summary>Indicates whether the Grid Snap option is enabled or not.</summary>
        public bool GridSnap { get; set; }
        /// <summary>Indicates whether the Free Move option is enabled or not.</summary>
        public bool FreeMove { get; set; }
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

        /// <summary>Occurs when the selected objects have been moved.</summary>
        public event MovedObjectsHandler SelectedObjectsMoved;
        /// <summary>Occurs when the selected objects have been rotated.</summary>
        public event RotatedObjectsHandler SelectedObjectsRotated;
        /// <summary>Occurs when the selected objects have been scaled.</summary>
        public event ScaledObjectsHandler SelectedObjectsScaled;
        /// <summary>Occurs when the selected objects have been flipped horizontally.</summary>
        public event FlippedObjectsHorizontallyHandler SelectedObjectsFlippedHorizontally;
        /// <summary>Occurs when the selected objects have been flipped vertically.</summary>
        public event FlippedObjectsVerticallyHandler SelectedObjectsFlippedVertically;

        /// <summary>Occurs when the selected objects have been copied to the clipboard.</summary>
        public event ObjectsCopiedHandler SelectedObjectsCopied;
        /// <summary>Occurs when objects been pasted from the clipboard.</summary>
        public event ObjectsPastedHandler SelectedObjectsPasted;
        /// <summary>Occurs when objects been ViPriNized.</summary>
        public event ObjectsCopyPastedHandler SelectedObjectsCopyPasted;
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
        /// <summary>Selects an object.</summary>
        /// <param name="obj">The object to add to the selection.</param>
        public void SelectObject(GeneralObject obj)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.Add(obj);
            SelectedObjectsAdded?.Invoke(new LevelObjectCollection(obj));
        }
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
        /// <param name="obj">The object to remove from the selection.</param>
        public void DeselectObject(GeneralObject obj)
        {
            SelectedObjects.Remove(obj);
            SelectedObjectsRemoved?.Invoke(new LevelObjectCollection(obj));
        }
        /// <summary>Deselects a number of objects.</summary>
        /// <param name="objects">The objects to remove from the selection.</param>
        public void DeselectObjects(LevelObjectCollection objects)
        {
            SelectedObjects.RemoveRange(objects);
            SelectedObjectsRemoved?.Invoke(objects);
        }
        /// <summary>Inverts the current selection.</summary>
        public void InvertSelection()
        {
            LevelObjectCollection old;
            SelectedObjects = Level.LevelObjects.Clone().RemoveRange(old = SelectedObjects.Clone());
            SelectedObjectsAdded?.Invoke(SelectedObjects);
            SelectedObjectsRemoved?.Invoke(old);
        }
        /// <summary>Selects a number of objects based on a condition.</summary>
        /// <param name="predicate">The predicate to determine the objects to select.</param>
        /// <param name="appendToSelection">Determines whether the new objects will be appended to the already existing selection.</param>
        public void SelectObjects(Predicate<GeneralObject> predicate, bool appendToSelection = true)
        {
            if (!appendToSelection)
                DeselectAll();
            SelectObjects(GetObjects(predicate));
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
        // TODO: Figure out a performant way to improve the copy-pasted code model
        private void PerformAction(bool individually, Action action, Action nonIndividualAction, Action eventToInvoke)
        {
            if (individually)
            {
                action();
                eventToInvoke();
            }
            else
                nonIndividualAction();
        }
        #region Object Movement
        /// <summary>Moves the selected objects by an amount on the X axis.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        public void MoveX(double x)
        {
            if (x != 0)
            {
                foreach (var o in SelectedObjects)
                    o.X += x;
                SelectedObjectsMoved?.Invoke(SelectedObjects, new Point(x, 0));
            }
        }
        /// <summary>Moves the selected objects by an amount on the Y axis.</summary>
        /// <param name="y">The offset of Y to move the objects by.</param>
        public void MoveY(double y)
        {
            if (y != 0)
            {
                foreach (var o in SelectedObjects)
                    o.Y += y;
                SelectedObjectsMoved?.Invoke(SelectedObjects, new Point(0, y));
            }
        }
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        public void Move(double x, double y) => Move(new Point(x, y));
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        public void Move(Point p)
        {
            if (p.Y == 0)
                MoveX(p.X);
            else if (p.X == 0)
                MoveY(p.Y);
            else
            {
                foreach (var o in SelectedObjects)
                {
                    o.X += p.X;
                    o.Y += p.Y;
                }
                SelectedObjectsMoved?.Invoke(SelectedObjects, p);
            }
        }
        #endregion
        #region Object Rotation
        /// <summary>Rotates the selected objects by an amount.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        public void Rotate(double rotation, bool individually)
        {
            if (individually)
            {
                foreach (var o in SelectedObjects)
                    o.Rotation += rotation;
                SelectedObjectsRotated?.Invoke(SelectedObjects, rotation, null);
            }
            else
                Rotate(rotation, GetMedianPoint());
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
            SelectedObjectsRotated?.Invoke(SelectedObjects, rotation, center);
        }
        #endregion
        #region Object Scaling
        /// <summary>Scales the selected objects by an amount.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        public void Scale(double scaling, bool individually)
        {
            if (individually)
            {
                foreach (var o in SelectedObjects)
                    o.Scaling *= scaling;
                SelectedObjectsScaled?.Invoke(SelectedObjects, scaling, null);
            }
            else
                Scale(scaling, GetMedianPoint());
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
            SelectedObjectsScaled?.Invoke(SelectedObjects, scaling, center);
        }
        #endregion
        #region Object Flipping
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        public void FlipHorizontally(bool individually)
        {
            if (individually)
            {
                foreach (var o in SelectedObjects)
                    o.FlippedHorizontally = !o.FlippedHorizontally;
                SelectedObjectsFlippedHorizontally?.Invoke(SelectedObjects, null);
            }
            else
                FlipHorizontally(GetMedianPoint());
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
            SelectedObjectsFlippedHorizontally?.Invoke(SelectedObjects, center);
        }
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        public void FlipVertically(bool individually)
        {
            if (individually)
            {
                foreach (var o in SelectedObjects)
                    o.FlippedVertically = !o.FlippedVertically;
                SelectedObjectsFlippedVertically?.Invoke(SelectedObjects, null);
            }
            else
                FlipVertically(GetMedianPoint());
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
            SelectedObjectsFlippedVertically?.Invoke(SelectedObjects, center);
        }
        #endregion
        #endregion

        #region Editor Functions
        /// <summary>Adds an object to the level.</summary>
        /// <param name="obj">The object to add to the level.</param>
        public void AddObject(GeneralObject obj)
        {
            Level.LevelObjects.Add(obj);
            SelectObject(obj);
        }
        /// <summary>Adds a collection of objects to the level.</summary>
        /// <param name="objects">The objects to add to the level.</param>
        public void AddObjects(LevelObjectCollection objects)
        {
            Level.LevelObjects.AddRange(objects);
            SelectObjects(objects);
        }
        /// <summary>Removes an object from the level.</summary>
        /// <param name="obj">The object to remove from the level.</param>
        public void RemoveObject(GeneralObject obj)
        {
            DeselectObject(obj);
            Level.LevelObjects.Remove(obj);
        }
        /// <summary>Removes a collection of objects from the level.</summary>
        /// <param name="objects">The objects to remove from the level.</param>
        public void RemoveObjects(LevelObjectCollection objects)
        {
            DeselectObjects(objects);
            Level.LevelObjects.RemoveRange(objects);
        }

        /// <summary>Copy the selected objects and add them to the clipboard.</summary>
        public void Copy()
        {
            var old = ObjectClipboard;
            ObjectClipboard = new LevelObjectCollection();
            ObjectClipboard.AddRange(SelectedObjects);
            SelectedObjectsCopied?.Invoke(ObjectClipboard, old);
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
            SelectedObjectsPasted?.Invoke(newObjects, center);
        }
        /// <summary>ViPriNizes all the selected objects.</summary>
        public void CopyPaste()
        {
            var cloned = SelectedObjects.Clone();
            Level.LevelObjects.AddRange(cloned);
            SelectedObjectsCopyPasted(cloned, SelectedObjects);
        }

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
            for (int i = 1; i < 1000; i++)
                if (!usedIDs.Contains(i))
                    Level.ColorChannels[i].Reset();
        }
        /// <summary>Resets the Group IDs of the objects that are not targeted by any trigger.</summary>
        public void ResetUnusedGroupIDs()
        {
            HashSet<int> usedIDs = new HashSet<int>();
            foreach (var o in Level.LevelObjects)
            {
                switch (o)
                {
                    case PulseTrigger p:
                        if (p.PulseTargetType == PulseTargetType.Group)
                            usedIDs.Add(p.TargetGroupID);
                        break;
                    case PickupItem i:
                        if (i.PickupMode == PickupItemPickupMode.ToggleTriggerMode)
                            usedIDs.Add(i.TargetGroupID);
                        break;
                    case IHasTargetGroupID t:
                        usedIDs.Add(t.TargetGroupID);
                        break;
                    case IHasSecondaryGroupID s:
                        usedIDs.Add(s.SecondaryGroupID);
                        break;
                }
            }
            foreach (var o in Level.LevelObjects)
                for (int i = 0; i < o.GroupIDs.Length; i++)
                    if (!usedIDs.Contains(o.GroupIDs[i]))
                        o.GroupIDs[i] = 0;
        }

        /// <summary>Snaps all the triggers to the level's guidelines.</summary>
        /// <param name="maxDifference">The max difference between the time of the trigger and the guideline in milliseconds.</param>
        public void SnapTriggersToGuidelines(double maxDifference = 100)
        {
            if (Level.Guidelines.Count == 0)
                return;
            var segments = Level.SpeedSegments;
            var triggers = new List<Trigger>(); // Contains the information of the triggers and their X positions
            foreach (Trigger t in Level.LevelObjects)
                if (!t.TouchTriggered)
                    triggers.Add(t);
            foreach (var t in triggers)
            {
                double time = segments.ConvertXToTime(t.X);
                int index = Level.Guidelines.GetFirstIndexAfterTimeStamp(time);
                double a = Level.Guidelines[index - 1].TimeStamp;
                double b = Level.Guidelines[index].TimeStamp;
                if (index > 0 && Math.Abs(a - time) <= maxDifference)
                    t.X = segments.ConvertTimeToX(a);
                else if (Math.Abs(b - time) <= maxDifference)
                    t.X = segments.ConvertTimeToX(b);
            }
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

    /// <summary>Represents a function that contains information about an object movement action.</summary>
    /// <param name="objects">The objects that were moved.</param>
    /// <param name="offset">The offset of the movement function.</param>
    public delegate void MovedObjectsHandler(LevelObjectCollection objects, Point offset);
    /// <summary>Represents a function that contains information about an object rotation action.</summary>
    /// <param name="objects">The objects that were rotated.</param>
    /// <param name="offset">The offset of the rotation function.</param>
    /// <param name="centralPoint">The central point of the rotation (<see langword="null"/> to indicate an individual rotation).</param>
    public delegate void RotatedObjectsHandler(LevelObjectCollection objects, double offset, Point? centralPoint);
    /// <summary>Represents a function that contains information about an object scaling action.</summary>
    /// <param name="objects">The objects that were scaled.</param>
    /// <param name="scaling">The offset of the scaling function.</param>
    /// <param name="centralPoint">The central point of the scaling (<see langword="null"/> to indicate an individual scaling).</param>
    public delegate void ScaledObjectsHandler(LevelObjectCollection objects, double scaling, Point? centralPoint);
    /// <summary>Represents a function that contains information about a horizontal object flipping action.</summary>
    /// <param name="objects">The objects that were flipped horizontally.</param>
    /// <param name="centralPoint">The central point of the horizontal flipping (<see langword="null"/> to indicate an individual horizontal flipping).</param>
    public delegate void FlippedObjectsHorizontallyHandler(LevelObjectCollection objects, Point? centralPoint);
    /// <summary>Represents a function that contains information about a vertical object flipping action.</summary>
    /// <param name="objects">The objects that were flipped vertically.</param>
    /// <param name="centralPoint">The central point of the vertical flipping (<see langword="null"/> to indicate an individual vertical flipping).</param>
    public delegate void FlippedObjectsVerticallyHandler(LevelObjectCollection objects, Point? centralPoint);
    /// <summary>Represents a function that contains information about an object copy action.</summary>
    /// <param name="newObjects">The new objects that were copied.</param>
    /// <param name="oldObjects">The old copied objects.</param>
    public delegate void ObjectsCopiedHandler(LevelObjectCollection newObjects, LevelObjectCollection oldObjects);
    /// <summary>Represents a function that contains information about an object paste action.</summary>
    /// <param name="objects">The objects that were pasted.</param>
    /// <param name="centralPoint">The central point of the pasted objects.</param>
    public delegate void ObjectsPastedHandler(LevelObjectCollection objects, Point centralPoint);
    /// <summary>Represents a function that contains information about an object copy-paste action.</summary>
    /// <param name="newObjects">The new copies of the original objects.</param>
    /// <param name="oldObjects">The original objects that were copied.</param>
    public delegate void ObjectsCopyPastedHandler(LevelObjectCollection newObjects, LevelObjectCollection oldObjects);
}
