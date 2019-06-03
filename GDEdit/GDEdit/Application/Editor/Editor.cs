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
using static System.Math;

namespace GDEdit.Application.Editor
{
    /// <summary>The editor which edits a level.</summary>
    public class Editor
    {
        private bool swipe, gridSnap, freeMove, dualLayerMode;
        private double gridSize = 30, zoom = 1;

        private UndoRedoSystem editorActions = new UndoRedoSystem();
        private UndoRedoSystem levelActions = new UndoRedoSystem();

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
        public bool Swipe
        {
            get => swipe;
            set => SetSwipe(value, true);
        }
        /// <summary>Indicates whether the Grid Snap option is enabled or not.</summary>
        public bool GridSnap
        {
            get => gridSnap;
            set => SetGridSnap(value, true);
        }
        /// <summary>Indicates whether the Free Move option is enabled or not.</summary>
        public bool FreeMove
        {
            get => freeMove;
            set => SetFreeMove(value, true);
        }
        #endregion

        #region Editor Preferences
        /// <summary>The customly defined movement steps in the editor.</summary>
        public List<double> CustomMovementSteps;
        /// <summary>The customly defined rotation steps in the editor.</summary>
        public List<double> CustomRotationSteps;
        #endregion

        #region Camera
        /// <summary>The size of each grid block in the editor.</summary>
        public double GridSize
        {
            get => gridSize;
            set => SetGridSize(value, true);
        }
        /// <summary>The camera zoom in the editor.</summary>
        public double Zoom
        {
            get => zoom;
            set => SetZoom(value, true);
        }

        /// <summary>Gets or sets a value indicating whether the editor is in dual layer mode.</summary>
        public bool DualLayerMode
        {
            get => dualLayerMode;
            set => SetDualLayerMode(value);
        }
        #endregion

        #region Events
        #region Editor Actions
        /// <summary>Occurs when the swipe option has been changed, including the new status.</summary>
        public event SwipeChangedHandler SwipeChanged;
        /// <summary>Occurs when the grid snap option has been changed, including the new status.</summary>
        public event GridSnapChangedHandler GridSnapChanged;
        /// <summary>Occurs when the free move option has been changed, including the new status.</summary>
        public event FreeMoveChangedHandler FreeMoveChanged;
        /// <summary>Occurs when the grid size has been changed, including the old and the new values.</summary>
        public event GridSizeChangedHandler GridSizeChanged;
        /// <summary>Occurs when the grid size has been changed, including the old and the new values.</summary>
        public event ZoomChangedHandler ZoomChanged;
        /// <summary>Occurs when the dual layer mode has been changed, including the new status.</summary>
        public event DualLayerModeChangedHandler DualLayerModeChanged;
        #endregion

        #region Level Actions
        // TODO: Create delegates
        /// <summary>Occurs when new objects have been added to the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsAdded;
        /// <summary>Occurs when new objects have been removed from the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsRemoved;
        /// <summary>Occurs when all objects have been deselected.</summary>
        public event AllObjectsDeselectedHandler AllObjectsDeselected;

        /// <summary>Occurs when objects have been created.</summary>
        public event Action<LevelObjectCollection> ObjectsCreated;
        /// <summary>Occurs when objects have been deleted.</summary>
        public event Action<LevelObjectCollection> ObjectsDeleted;

        /// <summary>Occurs when objects have been moved.</summary>
        public event MovedObjectsHandler ObjectsMoved;
        /// <summary>Occurs when objects have been rotated.</summary>
        public event RotatedObjectsHandler ObjectsRotated;
        /// <summary>Occurs when objects have been scaled.</summary>
        public event ScaledObjectsHandler ObjectsScaled;
        /// <summary>Occurs when objects have been flipped horizontally.</summary>
        public event FlippedObjectsHorizontallyHandler ObjectsFlippedHorizontally;
        /// <summary>Occurs when objects have been flipped vertically.</summary>
        public event FlippedObjectsVerticallyHandler ObjectsFlippedVertically;

        /// <summary>Occurs when objects have been copied to the clipboard.</summary>
        public event ObjectsCopiedHandler ObjectsCopied;
        /// <summary>Occurs when objects been pasted from the clipboard.</summary>
        public event ObjectsPastedHandler ObjectsPasted;
        /// <summary>Occurs when objects been ViPriNized.</summary>
        public event ObjectsCopyPastedHandler ObjectsCopyPasted;
        #endregion
        #endregion

        #region Event Functions
        // Signatures and final invocation statements were macro-generated
        // TODO: Add support for the new object properties in 2.2
        #region Editor Actions
        /// <summary>Triggers the <seealso cref="SwipeChanged"/> event.</summary>
        /// <param name="value">The value that was set to Swipe.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSwipeChanged(bool value, bool registerUndoable = true)
        {
            void Action() => SetSwipe(value, false);
            void Undo() => SetSwipe(!value, false);
            string description = $"Set Swipe to {value}";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            SwipeChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="GridSnapChanged"/> event.</summary>
        /// <param name="value">The value that was set to Grid Snap.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGridSnapChanged(bool value, bool registerUndoable = true)
        {
            void Action() => SetGridSnap(value, false);
            void Undo() => SetGridSnap(!value, false);
            string description = $"Set Grid Snap to {value}";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            GridSnapChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="FreeMoveChanged"/> event.</summary>
        /// <param name="value">The value that was set to Free Move.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnFreeMoveChanged(bool value, bool registerUndoable = true)
        {
            void Action() => SetDualLayerMode(value, false);
            void Undo() => SetDualLayerMode(!value, false);
            string description = $"Set Free Move to {value}";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            FreeMoveChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="ZoomChanged"/> event.</summary>
        /// <param name="oldValue">The old value of Zoom.</param>
        /// <param name="newValue">The value that was set to Zoom.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnZoomChanged(double oldValue, double newValue, bool registerUndoable = true)
        {
            void Action() => SetZoom(newValue, false);
            void Undo() => SetZoom(oldValue, false);
            string description = $"Set Zoom to {oldValue}x";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            ZoomChanged?.Invoke(oldValue, newValue);
        }
        /// <summary>Triggers the <seealso cref="GridSizeChanged"/> event.</summary>
        /// <param name="oldValue">The old value of Grid Size.</param>
        /// <param name="newValue">The value that was set to Grid Size.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGridSizeChanged(double oldValue, double newValue, bool registerUndoable = true)
        {
            void Action() => SetGridSize(newValue, false);
            void Undo() => SetGridSize(oldValue, false);
            string description = $"Set Grid Size to {oldValue}x";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            GridSizeChanged?.Invoke(oldValue, newValue);
        }
        /// <summary>Triggers the <seealso cref="DualLayerModeChanged"/> event.</summary>
        /// <param name="value">The value that was set to Dual Layer Mode.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnDualLayerModeChanged(bool value, bool registerUndoable = true)
        {
            void Action() => SetDualLayerMode(value, false);
            void Undo() => SetDualLayerMode(!value, false);
            string description = $"Set Dual Layer Mode to {value}";
            if (registerUndoable)
                editorActions.AddTemporaryAction(description, Action, Undo);
            DualLayerModeChanged?.Invoke(value);
        }
        #endregion

        #region Level Actions
        /// <summary>Triggers the <seealso cref="SelectedObjectsAdded"/> event.</summary>
        /// <param name="objects">The objects that were added to the selection list.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSelectedObjectsAdded(LevelObjectCollection objects, bool registerUndoable = true)
        {
            void Action() => SelectObjects(objects, false);
            void Undo() => DeselectObjects(objects, false);
            string description = $"Add {GetAppropriateForm(objects.Count, "object")} to selection";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            SelectedObjectsAdded?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="SelectedObjectsRemoved"/> event.</summary>
        /// <param name="objects">The objects that were removed from the selection list.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSelectedObjectsRemoved(LevelObjectCollection objects, bool registerUndoable = true)
        {
            void Action() => DeselectObjects(objects, false);
            void Undo() => SelectObjects(objects, false);
            string description = $"Remove {GetAppropriateForm(objects.Count, "object")} from selection";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            SelectedObjectsRemoved?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="AllObjectsDeselected"/> event.</summary>
        /// <param name="previousSelection">The previous selection before deselecting all the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnAllObjectsDeselected(LevelObjectCollection previousSelection, bool registerUndoable = true)
        {
            void Action() => DeselectAll(false);
            void Undo() => SelectObjects(previousSelection, false);
            string description = $"Clear selection";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            AllObjectsDeselected?.Invoke(previousSelection);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCreated"/> event.</summary>
        /// <param name="objects">The objects that were added to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCreated(LevelObjectCollection objects, bool registerUndoable = true)
        {
            void Action() => AddObjects(objects, false);
            void Undo() => RemoveObjects(objects, false);
            string description = $"Add {GetAppropriateForm(objects.Count, "object")}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsCreated?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsDeleted"/> event.</summary>
        /// <param name="objects">The objects that were removed from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsDeleted(LevelObjectCollection objects, bool registerUndoable = true)
        {
            void Action() => RemoveObjects(objects, false);
            void Undo() => AddObjects(objects, false);
            string description = $"Remove {GetAppropriateForm(objects.Count, "object")}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsDeleted?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsMoved"/> event.</summary>
        /// <param name="objects">The objects that were moved.</param>
        /// <param name="offset">The difference of the previous and the new locations of the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsMoved(LevelObjectCollection objects, Point offset, bool registerUndoable = true)
        {
            void Action() => Move(objects, offset, false);
            void Undo() => Move(objects, -offset, false);
            string description = $"Move {GetAppropriateForm(objects.Count, "object")} by {offset}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsMoved?.Invoke(objects, offset);
        }
        /// <summary>Triggers the <seealso cref="ObjectsRotated"/> event.</summary>
        /// <param name="objects">The objects that were rotated.</param>
        /// <param name="offset">The difference of the previous and the new rotations of the objects.</param>
        /// <param name="centralPoint">The central point that was taken into account while rotating all objects. If <see langword="null"/>, the objects were individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsRotated(LevelObjectCollection objects, double offset, Point? centralPoint, bool registerUndoable = true)
        {
            void Action() => Rotate(objects, offset, centralPoint, false);
            void Undo() => Rotate(objects, -offset, centralPoint, false);
            string description = $"Rotate {GetAppropriateForm(objects.Count, "object")} by {offset}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsRotated?.Invoke(objects, offset, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsScaled"/> event.</summary>
        /// <param name="objects">The objects that were rotated.</param>
        /// <param name="scaling">The ratio of the previous and the new scalings of the objects, e.g. if the new scaling is 8x and the old one was 2x, this value will equal to 4, since 8 / 2 = 4.</param>
        /// <param name="centralPoint">The central point that was taken into account while rotating all objects. If <see langword="null"/>, the objects were individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsScaled(LevelObjectCollection objects, double scaling, Point? centralPoint, bool registerUndoable = true)
        {
            void Action() => Scale(objects, scaling, centralPoint, false);
            void Undo() => Scale(objects, 1 / scaling, centralPoint, false);
            string description = $"Scale {GetAppropriateForm(objects.Count, "object")} by {scaling}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsScaled?.Invoke(objects, scaling, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsFlippedHorizontally"/> event.</summary>
        /// <param name="objects">The objects that were flipped horizontally.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping all objects horizontally. If <see langword="null"/>, the objects were individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsFlippedHorizontally(LevelObjectCollection objects, Point? centralPoint, bool registerUndoable = true)
        {
            // Both functions have the same exact effect, however for consistency we keep that
            // Consider this function inheriting a function interface where it demands the implementation of the functions `void Action()` and `void Undo()`
            void Action() => FlipHorizontally(objects, centralPoint, false);
            void Undo() => FlipHorizontally(objects, centralPoint, false);
            string description = $"Flip {GetAppropriateForm(objects.Count, "object")} horizontally";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsFlippedHorizontally?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsFlippedVertically"/> event.</summary>
        /// <param name="objects">The objects that were flipped vertically.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping all objects vertically. If <see langword="null"/>, the objects were individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsFlippedVertically(LevelObjectCollection objects, Point? centralPoint, bool registerUndoable = true)
        {
            void Action() => FlipVertically(objects, centralPoint, false);
            void Undo() => FlipVertically(objects, centralPoint, false);
            string description = $"Flip {GetAppropriateForm(objects.Count, "object")} vertically";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsFlippedVertically?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCopied"/> event.</summary>
        /// <param name="newObjects">The objects that were copied and added to the clipboard.</param>
        /// <param name="oldObjects">The objects that were previously in the clipboard.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCopied(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable = true)
        {
            void Action() => Copy(newObjects, false);
            void Undo() => Copy(oldObjects, false);
            string description = $"Copy {GetAppropriateForm(newObjects.Count, "object")}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsCopied?.Invoke(newObjects, oldObjects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsPasted"/> event.</summary>
        /// <param name="objects">The new objects that were pasted.</param>
        /// <param name="centralPoint">The central point that was taken into account while pasting all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsPasted(LevelObjectCollection objects, Point centralPoint, bool registerUndoable = true)
        {
            void Action() => Paste(objects, centralPoint, false);
            void Undo() => RemoveObjects(objects, false);
            // This will not select the previously selected objects, if any; a workaround must be implemented
            // Preferably the workaround involves implementing something that registers both the deselection and the pasting using the multiple action toggle
            string description = $"Paste {GetAppropriateForm(objects.Count, "object")}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsPasted?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCopyPasted"/> event.</summary>
        /// <param name="newObjects">The objects that were pasted.</param>
        /// <param name="oldObjects">The objects that were copied.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCopyPasted(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable = true)
        {
            void Action() => CopyPaste(newObjects, false);
            void Undo()
            {
                RemoveObjects(newObjects, false);
                SelectObjects(oldObjects, false);
            }
            string description = $"Copy-paste {GetAppropriateForm(newObjects.Count, "object")}";
            if (registerUndoable)
                levelActions.AddTemporaryAction(description, Action, Undo);
            ObjectsCopyPasted?.Invoke(newObjects, oldObjects);
        }
        #endregion
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
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObject(GeneralObject obj, bool registerUndoable = true)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.Add(obj);
            OnSelectedObjectsAdded(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Selects a number of objects.</summary>
        /// <param name="objects">The objects to add to the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.AddRange(objects);
            OnSelectedObjectsAdded(objects, registerUndoable);
        }
        /// <summary>Deselects an object.</summary>
        /// <param name="obj">The object to remove from the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectObject(GeneralObject obj, bool registerUndoable = true)
        {
            SelectedObjects.Remove(obj);
            OnSelectedObjectsRemoved(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Deselects a number of objects.</summary>
        /// <param name="objects">The objects to remove from the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (objects == SelectedObjects) // Micro-optimization
                DeselectAll(registerUndoable);
            else
            {
                SelectedObjects.RemoveRange(objects);
                OnSelectedObjectsRemoved(objects, registerUndoable);
            }
        }
        /// <summary>Inverts the current selection.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void InvertSelection(bool registerUndoable = true)
        {
            var newObjects = Level.LevelObjects.Clone().RemoveRange(SelectedObjects);
            DeselectAll(registerUndoable);
            SelectObjects(newObjects, registerUndoable);
        }
        /// <summary>Selects a number of objects based on a condition.</summary>
        /// <param name="predicate">The predicate to determine the objects to select.</param>
        /// <param name="appendToSelection">Determines whether the new objects will be appended to the already existing selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObjects(Predicate<GeneralObject> predicate, bool appendToSelection = true, bool registerUndoable = true)
        {
            // Maybe there's a better way to take care of that but let it be like that for the time being
            levelActions.MultipleActionToggle = true;
            if (!appendToSelection)
                DeselectAll(registerUndoable);
            SelectObjects(GetObjects(predicate), registerUndoable);
            levelActions.MultipleActionToggle = false;
        }
        /// <summary>Selects all objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectAll(bool registerUndoable = true) => SelectObjects(Level.LevelObjects, registerUndoable);
        /// <summary>Deselects all objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectAll(bool registerUndoable = true)
        {
            var previousSelection = SelectedObjects.Clone();
            SelectedObjects.Clear();
            OnAllObjectsDeselected(previousSelection, registerUndoable);
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
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveX(double x, bool registerUndoable = true) => MoveX(SelectedObjects, x, registerUndoable);
        /// <summary>Moves the specified objects by an amount on the X axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveX(LevelObjectCollection objects, double x, bool registerUndoable = true)
        {
            if (x != 0)
            {
                foreach (var o in objects)
                    o.X += x;
                OnObjectsMoved(objects, new Point(x, 0), registerUndoable);
            }
        }
        /// <summary>Moves the selected objects by an amount on the Y axis.</summary>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveY(double y, bool registerUndoable = true) => MoveY(SelectedObjects, y, registerUndoable);
        /// <summary>Moves the specified objects by an amount on the Y axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveY(LevelObjectCollection objects, double y, bool registerUndoable = true)
        {
            if (y != 0)
            {
                foreach (var o in objects)
                    o.Y += y;
                OnObjectsMoved(objects, new Point(0, y), registerUndoable);
            }
        }
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(double x, double y, bool registerUndoable = true) => Move(new Point(x, y), registerUndoable);
        /// <summary>Moves the specified objects by an amount.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(LevelObjectCollection objects, double x, double y, bool registerUndoable = true) => Move(objects, new Point(x, y), registerUndoable);
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(Point p, bool registerUndoable = true) => Move(SelectedObjects, p, registerUndoable);
        /// <summary>Moves the specified objects by an amount.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(LevelObjectCollection objects, Point p, bool registerUndoable = true)
        {
            if (p.Y == 0)
                MoveX(p.X);
            else if (p.X == 0)
                MoveY(p.Y);
            else
            {
                foreach (var o in objects)
                {
                    o.X += p.X;
                    o.Y += p.Y;
                }
                OnObjectsMoved(objects, p, registerUndoable);
            }
        }
        // TODO: Add MoveTo* functions to prevent other code from having to calculate individual objects' offset
        #endregion
        #region Object Rotation
        // The rotation direction is probably wrongly documented; take a look at it in another branch
        /// <summary>Rotates the selected objects by an amount.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, bool individually, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, individually, registerUndoable);
        /// <summary>Rotates the specified objects by an amount.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.Rotation += rotation;
                OnObjectsRotated(objects, rotation, null, registerUndoable);
            }
            else
                Rotate(objects, rotation, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, Point center, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, center, registerUndoable);
        /// <summary>Rotates the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.Rotation += rotation;
                o.Location = o.Location.Rotate(center, rotation);
            }
            OnObjectsRotated(objects, rotation, center, registerUndoable);
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects. If <see langword="null"/>, the objects will be individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, Point? center, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, center, registerUndoable);
        /// <summary>Rotates the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects. If <see langword="null"/>, the objects will be individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                Rotate(objects, rotation, true, registerUndoable);
            else
                Rotate(objects, rotation, center.Value, registerUndoable);
        }
        #endregion
        #region Object Scaling
        /// <summary>Scales the selected objects by an amount.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, bool individually, bool registerUndoable = true) => Scale(SelectedObjects, scaling, individually, registerUndoable);
        /// <summary>Scales the specified objects by an amount.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.Scaling *= scaling;
                OnObjectsScaled(objects, scaling, null, registerUndoable);
            }
            else
                Scale(objects, scaling, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, Point center, bool registerUndoable = true) => Scale(SelectedObjects, scaling, center, registerUndoable);
        /// <summary>Scales the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.Scaling *= scaling;
                o.Location = (center - o.Location) * scaling + center;
            }
            OnObjectsScaled(objects, scaling, center, registerUndoable);
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects. If <see langword="null"/>, the objects will be individually scaled.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, Point? center, bool registerUndoable = true) => Scale(SelectedObjects, scaling, center, registerUndoable);
        /// <summary>Scales the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects. If <see langword="null"/>, the objects will be individually scaled.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                Scale(objects, scaling, true, registerUndoable);
            else
                Scale(objects, scaling, center.Value, registerUndoable);
        }
        #endregion
        #region Object Flipping
        #region Flip Horizontally
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(bool individually, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, individually, registerUndoable);
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.FlippedHorizontally = !o.FlippedHorizontally;
                OnObjectsFlippedHorizontally(objects, null, registerUndoable);
            }
            else
                FlipHorizontally(objects, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(Point center, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.FlippedHorizontally = !o.FlippedHorizontally;
                o.X = 2 * center.X - o.X;
            }
            OnObjectsFlippedHorizontally(objects, center, registerUndoable);
        }
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(Point? center, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                FlipHorizontally(objects, true, registerUndoable);
            else
                FlipHorizontally(objects, center.Value, registerUndoable);
        }
        #endregion
        #region Flip Vertically
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(bool individually, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, individually, registerUndoable);
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.FlippedVertically = !o.FlippedVertically;
                OnObjectsFlippedVertically(objects, null, registerUndoable);
            }
            else
                FlipVertically(objects, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(Point center, bool registerUndoable = true) => FlipVertically(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.FlippedVertically = !o.FlippedVertically;
                o.Y = 2 * center.Y - o.Y;
            }
            OnObjectsFlippedVertically(objects, center, registerUndoable);
        }
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(Point? center, bool registerUndoable = true) => FlipVertically(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                FlipVertically(objects, true, registerUndoable);
            else
                FlipVertically(objects, center.Value, registerUndoable);
        }
        #endregion
        #endregion
        #endregion

        #region Editor Functions
        /// <summary>Adds an object to the level.</summary>
        /// <param name="obj">The object to add to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddObject(GeneralObject obj, bool registerUndoable = true)
        {
            Level.LevelObjects.Add(obj);
            SelectObject(obj, false);
            OnObjectsCreated(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Adds a collection of objects to the level.</summary>
        /// <param name="objects">The objects to add to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            Level.LevelObjects.AddRange(objects);
            SelectObjects(objects, false);
            OnObjectsCreated(objects, registerUndoable);
        }
        /// <summary>Removes an object from the level.</summary>
        /// <param name="obj">The object to remove from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObject(GeneralObject obj, bool registerUndoable = true)
        {
            DeselectObject(obj, false);
            Level.LevelObjects.Remove(obj);
            OnObjectsDeleted(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Removes the currently selected objects from the level.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObjects(bool registerUndoable = true) => RemoveObjects(SelectedObjects, registerUndoable);
        /// <summary>Removes a collection of objects from the level.</summary>
        /// <param name="objects">The objects to remove from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            DeselectObjects(objects, false);
            Level.LevelObjects.RemoveRange(objects);
            OnObjectsDeleted(objects, registerUndoable);
        }

        /// <summary>Copies the selected objects and add them to the clipboard.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Copy(bool registerUndoable = true) => Copy(SelectedObjects, registerUndoable);
        /// <summary>Copies the specified objects and add them to the clipboard.</summary>
        /// <param name="objects">The objects to copy to the clipboard.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Copy(LevelObjectCollection objects, bool registerUndoable = true)
        {
            var old = ObjectClipboard.Clone();
            ObjectClipboard = new LevelObjectCollection();
            ObjectClipboard.AddRange(objects);
            OnObjectsCopied(ObjectClipboard, old, registerUndoable);
        }
        /// <summary>Pastes the copied objects on the clipboard provided a central position to place them.</summary>
        /// <param name="center">The central position which will determine the position of the pasted objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Paste(Point center, bool registerUndoable = true) => Paste(ObjectClipboard, center, registerUndoable);
        /// <summary>Pastes the specified objects provided a central position to place them.</summary>
        /// <param name="objects">The objects to paste to the specified central position.</param>
        /// <param name="center">The central position which will determine the position of the pasted objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Paste(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            if (objects.Count == 0)
                return;
            var distance = center - GetMedianPoint(objects);
            var newObjects = objects.Clone();
            foreach (var o in newObjects)
                o.Location += distance;
            Level.LevelObjects.AddRange(newObjects);
            SelectObjects(newObjects, false);
            OnObjectsPasted(newObjects, center, registerUndoable);
        }
        /// <summary>ViPriNizes all the selected objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void CopyPaste(bool registerUndoable = true) => CopyPaste(SelectedObjects, registerUndoable);
        /// <summary>ViPriNizes all the selected objects.</summary>
        /// <param name="objects">The objects to ViPriNize.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void CopyPaste(LevelObjectCollection objects, bool registerUndoable = true)
        {
            var cloned = objects.Clone();
            Level.LevelObjects.AddRange(cloned);
            OnObjectsCopyPasted(cloned, SelectedObjects, registerUndoable);
        }

        // TODO: Implement undo/redo on the following 3 functions
        /// <summary>Resets the unused color channels.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ResetUnusedColors(bool registerUndoable = true)
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
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ResetUnusedGroupIDs(bool registerUndoable = true)
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
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SnapTriggersToGuidelines(double maxDifference = 100, bool registerUndoable = true)
        {
            if (Level.Guidelines.Count == 0)
                return;
            var segments = Level.SpeedSegments;
            var triggers = new List<Trigger>(); // Contains the information of the triggers and their X positions
            foreach (Trigger t in Level.LevelObjects)
                if (!t.TouchTriggered)
                    triggers.Add(t);
            levelActions.MultipleActionToggle = true;
            foreach (var t in triggers)
            {
                double time = segments.ConvertXToTime(t.X);
                int index = Level.Guidelines.GetFirstIndexAfterTimeStamp(time);
                double a = Level.Guidelines[index - 1].TimeStamp;
                double b = Level.Guidelines[index].TimeStamp;
                if (index > 0 && Abs(a - time) <= maxDifference)
                    t.X = segments.ConvertTimeToX(a);
                else if (Abs(b - time) <= maxDifference)
                    t.X = segments.ConvertTimeToX(b);
            }
            levelActions.MultipleActionToggle = false;
            if (registerUndoable)
                levelActions.RegisterActions("Snap triggers to guidelines");
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

        #region Undo/Redo
        /// <summary>Undoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoLevelActions(int count = 1) => Undo(levelActions, count);
        /// <summary>Redoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoLevelActions(int count = 1) => Redo(levelActions, count);
        /// <summary>Undoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoEditorActions(int count = 1) => Undo(editorActions, count);
        /// <summary>Redoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoEditorActions(int count = 1) => Redo(editorActions, count);
        #endregion

        #region Property Setters
        /// <summary>Sets the value of Swipe. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Swipe.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetSwipe(bool value, bool registerUndoable = true) => Set(ref swipe, value, OnSwipeChanged, registerUndoable);
        /// <summary>Sets the value of Grid Snap. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Grid Snap.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetGridSnap(bool value, bool registerUndoable = true) => Set(ref gridSnap, value, OnGridSnapChanged, registerUndoable);
        /// <summary>Sets the value of Free Move. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Free Move.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetFreeMove(bool value, bool registerUndoable = true) => Set(ref freeMove, value, OnFreeMoveChanged, registerUndoable);
        /// <summary>Sets the value of Zoom. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Zoom.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetZoom(double value, bool registerUndoable = true) => Set(ref zoom, value, OnZoomChanged, registerUndoable);
        /// <summary>Sets the value of Grid Size. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Grid Size.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetGridSize(double value, bool registerUndoable = true) => Set(ref gridSize, value, OnGridSizeChanged, registerUndoable);
        /// <summary>Sets the value of Dual Layer Mode. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Dual Layer Mode.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetDualLayerMode(bool value, bool registerUndoable = true) => Set(ref dualLayerMode, value, OnDualLayerModeChanged, registerUndoable);
        #endregion

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

        private void Undo(UndoRedoSystem system, int count = 1) => system.Undo(count);
        private void Redo(UndoRedoSystem system, int count = 1) => system.Redo(count);

        private void Set(ref bool field, bool value, BoolValueChangedHandler handler, bool registerUndoable = true)
        {
            if (value == field)
                return;
            handler(field = value, registerUndoable);
        }
        private void Set(ref double field, double value, OldNewDoubleValueChangedHandler handler, bool registerUndoable = true)
        {
            if (value == field)
                return;
            double old = field;
            handler(old, field = value, registerUndoable);
        }

        private static string GetAppropriateForm(int count, string thing) => $"{count} {thing}{(count != 1 ? "" : "s")}";
        #endregion

        private class UndoRedoSystem
        {
            // I know, the naming is terrible on this one (hence the documentation), we need to find a better name
            /// <summary>Determines whether multiple actions will be logged in the undo stack.</summary>
            public bool MultipleActionToggle;

            /// <summary>The temporarily stored undoable action that is to be registered upon completing an operation.</summary>
            public UndoableAction TemporaryUndoableAction = new UndoableAction();
            /// <summary>The stack that contains all actions that are to be undone.</summary>
            public Stack<UndoableAction> UndoStack = new Stack<UndoableAction>();
            /// <summary>The stack that contains all actions that are to be redone.</summary>
            public Stack<UndoableAction> RedoStack = new Stack<UndoableAction>();

            /// <summary>Registers all the actions.</summary>
            /// <param name="description"></param>
            public void RegisterActions(string description)
            {
                TemporaryUndoableAction.Description = description;
                UndoStack.Push(TemporaryUndoableAction);
                TemporaryUndoableAction = new UndoableAction();
                RedoStack.Clear();
            }
            /// <summary>Adds a temporary action to the temporary action object and registers the undoable action if the multiple action toggle is <see langword="false"/>.</summary>
            /// <param name="description">The description of the actions.</param>
            /// <param name="action">The action. It must only perform the changes the action performs without invoking the respective events.</param>
            /// <param name="undo">The inverse action. It must only perform the changes the inverse action performs without invoking the respective events.</param>
            public void AddTemporaryAction(string description, Action action, Action undo)
            {
                TemporaryUndoableAction.Add(action, undo);
                if (!MultipleActionToggle)
                    RegisterActions(description);
            }

            /// <summary>Undoes a number of actions. If the specified count is greater than the available actions to undo, all actions are undone.</summary>
            /// <param name="count">The number of actions to undo.</param>
            public void Undo(int count = 1)
            {
                int actions = Min(count, UndoStack.Count);
                for (int i = 0; i < actions; i++)
                {
                    var action = UndoStack.Pop();
                    action.Undo();
                    RedoStack.Push(action);
                }
            }
            /// <summary>Redoes a number of actions. If the specified count is greater than the available actions to redo, all actions are redone.</summary>
            /// <param name="count">The number of actions to redo.</param>
            public void Redo(int count = 1)
            {
                int actions = Min(count, RedoStack.Count);
                for (int i = 0; i < actions; i++)
                {
                    var action = RedoStack.Pop();
                    action.Redo();
                    UndoStack.Push(action);
                }
            }
        }

        /// <summary>Contains information about an action that can be undone.</summary>
        private class UndoableAction
        {
            private readonly List<Action> undos = new List<Action>();
            private readonly List<Action> actions = new List<Action>();

            /// <summary>The description of the undoable action.</summary>
            public string Description { get; set; }

            public UndoableAction() { }
            public UndoableAction(string description) => Description = description;

            /// <summary>Adds an undoable action to the action list.</summary>
            /// <param name="action">The action to add to the list.</param>
            /// <param name="undo">The undo action to add to the list.</param>
            public void Add(Action action, Action undo)
            {
                actions.Add(action);
                undos.Add(undo);
            }

            /// <summary>Undoes all the actions in the list.</summary>
            public void Undo()
            {
                foreach (var u in undos)
                    u.Invoke();
            }
            /// <summary>Redoes all the actions in the list.</summary>
            public void Redo()
            {
                foreach (var a in actions)
                    a.Invoke();
            }
            /// <summary>Clears the action list.</summary>
            public void Clear() => actions.Clear();
        }
    }

    #region Delegates
    #region Templates
    /// <summary>Represents a function that contains information about changing a <see langword="bool"/> field, including its current value.</summary>
    /// <param name="value">The new value of the field.</param>
    public delegate void BoolValueChangedHandler(bool value, bool registerUndoable);
    /// <summary>Represents a function that contains information about changing a <see langword="double"/> field, including its current and its previous values.</summary>
    /// <param name="oldValue">The old value of the field.</param>
    /// <param name="newValue">The new value of the field.</param>
    public delegate void OldNewDoubleValueChangedHandler(double oldValue, double newValue, bool registerUndoable);
    #endregion

    #region Editor Actions
    /// <summary>Represents a function that contains information about changing the state of Swipe.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void SwipeChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Grid Snap.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void GridSnapChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Free Move.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void FreeMoveChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Dual Layer Mode.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void DualLayerModeChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the grid size.</summary>
    /// <param name="oldValue">The old value of the grid size.</param>
    /// <param name="newValue">The new value of the grid size.</param>
    public delegate void GridSizeChangedHandler(double oldValue, double newValue);
    /// <summary>Represents a function that contains information about changing the camera zoom.</summary>
    /// <param name="oldValue">The old value of the camera zoom.</param>
    /// <param name="newValue">The new value of the camera zoom.</param>
    public delegate void ZoomChangedHandler(double oldValue, double newValue);
    #endregion

    #region Level Actions
    /// <summary>Represents a function that contains information about deselecting all objects.</summary>
    /// <param name="previousSelection">The objects that were previously selected.</param>
    public delegate void AllObjectsDeselectedHandler(LevelObjectCollection previousSelection);
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
    #endregion
    #endregion
}
