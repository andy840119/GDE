using GDE.App.Main.Levels;
using GDE.App.Main.Objects;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osuTK;
using System;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class Camera : Container
    {
        private Editor editor;
        private Bindable<Vector2> cameraOffsetBindable = new Bindable<Vector2>();
        private GridSnappedCursorContainer snappedCursorContainer;

        public Camera(Editor Editor)
        {
            editor = Editor;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(snappedCursorContainer = new GridSnappedCursorContainer(cameraOffsetBindable));
        }

        public Vector2 ConvertMousePositionToEditor(Vector2 mousePosition) => snappedCursorContainer.ConvertMousePositionToEditor(mousePosition);

        public GeneralObject GetClonedGhostObjectLevelObject() => snappedCursorContainer.GhostObject.GetObject().Clone();

        protected override bool OnDrag(DragEvent e)
        {
            cameraOffsetBindable.Value += e.Delta;
            
            foreach (var child in Children)
            {
                if (child is IDraggable draggable)
                    if (draggable.Draggable)
                        child.Position += e.Delta;

                if (child is Grid grid)
                {
                    grid.Position = new Vector2(
                        -GetCoordinate(cameraOffsetBindable.Value.X) + cameraOffsetBindable.Value.X - 30,
                        -GetCoordinate(cameraOffsetBindable.Value.Y) + cameraOffsetBindable.Value.Y);
                }
            }

            float GetCoordinate(float c)
            {
                float r = c;
                if (r < 0)
                    r -= 30;
                return r -= r % 30;
            }

            return true;
        }

        protected override bool OnDragEnd(DragEndEvent e) => true;
        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.ShiftPressed)
                if (editor != null)
                    editor.Swipe = true;

            return base.OnKeyDown(e);
        }
        protected override bool OnKeyUp(KeyUpEvent e)
        {
            if (e.ShiftPressed)
                if (editor != null)
                    editor.Swipe = false;

            return base.OnKeyUp(e);
        }

        public void SetGhostObjectID(int id) => snappedCursorContainer.GhostObjectID = id;
        public void HideGhostObject() => snappedCursorContainer.HideGhostObject();
        public void ShowGhostObject() => snappedCursorContainer.ShowGhostObject();

        //TODO: This should be its own seperate class
        private class GridSnappedCursorContainer : CursorContainer, IRequireHighFrequencyMousePosition, IDraggable
        {
            public readonly GhostObject GhostObject;
            public bool Draggable => false;

            public int SnapResolution { get; set; }

            public int GhostObjectID
            {
                get => GhostObject.ObjectID;
                set
                {
                    GhostObject.ObjectID = value;

                    //framework dumbb
                    GhostObject.Alpha = 0.5f;
                }
            }

            public readonly Bindable<Vector2> CameraOffset = new Bindable<Vector2>();

            public GridSnappedCursorContainer(Bindable<Vector2> cameraOffsetBindable, int ghostObjectID = 0, int snapResolution = 30)
                : base()
            {
                RelativeSizeAxes = Axes.Both;
                AlwaysPresent = true;
                Children = new Drawable[] { GhostObject = new GhostObject() };
                CameraOffset.BindTo(cameraOffsetBindable);
                GhostObjectID = ghostObjectID;
                SnapResolution = snapResolution;
            }

            protected override bool OnMouseMove(MouseMoveEvent e)
            {
                foreach (var child in Children)
                    child.Position = ConvertMousePositionToEditor(e.ScreenSpaceMousePosition - CameraOffset.Value);

                return true;
            }
            protected override bool OnClick(ClickEvent e)
            {
                //foreach (var child in Children)
                //    child.Expire();
                
                return base.OnClick(e);
            }

            public Vector2 ConvertMousePositionToEditor(Vector2 mousePosition)
            {
                float x = GetCoordinate(mousePosition.X);
                float y = GetCoordinate(mousePosition.Y);
                return new Vector2(x, y) + CameraOffset.Value;
            }

            public void HideGhostObject() => GhostObject.Hide();
            public void ShowGhostObject() => GhostObject.Show();

            private float GetCoordinate(float c)
            {
                float r = c;
                if (r < 0)
                    r -= SnapResolution;
                return r -= r % SnapResolution;
            }
        }

        //TODO: This shouldnt exist
        private class GhostObject : ObjectBase
        {
            public int SnapResolution { get; set; } = 30;

            public GhostObject(GeneralObject o)
                : base(o)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.TopLeft;
                Selectable = false;
            }
            public GhostObject(int id = 1)
                : base(id)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.TopLeft;
                Selectable = false;
            }

            public GeneralObject GetObject()
            {
                //Strange precision
                LevelObject.X = GetCoordinate(Position.X) - 15 - (31 * 30);
                LevelObject.Y = GetCoordinate (- Position.Y) + 3 + (18 * 30);
                return LevelObject;
            }

            private float GetCoordinate(float c)
            {
                float r = c;
                if (r < 0)
                    r -= SnapResolution;
                return r -= r % SnapResolution;
            }
        }
    }
}
