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

        protected override bool OnDrag(DragEvent e)
        {
            foreach (var child in Children)
                child.Position += e.Delta;

            cameraOffsetBindable.Value += e.Delta;
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

        private class GridSnappedCursorContainer : CursorContainer, IRequireHighFrequencyMousePosition
        {
            private GhostObject ghostObject;

            public int SnapResolution { get; set; }

            public int GhostObjectID
            {
                get => ghostObject.ObjectID;
                set
                {
                    ghostObject.ObjectID = value;

                    //framework dumbb
                    ghostObject.Alpha = 0.5f;
                }
            }

            public readonly Bindable<Vector2> CameraOffset = new Bindable<Vector2>();

            public GridSnappedCursorContainer(Bindable<Vector2> cameraOffsetBindable, int ghostObjectID = 0, int snapResolution = 30)
                : base()
            {
                RelativeSizeAxes = Axes.Both;
                AlwaysPresent = true;
                Children = new Drawable[] { ghostObject = new GhostObject() };
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

            public void HideGhostObject() => ghostObject.Hide();
            public void ShowGhostObject() => ghostObject.Show();

            private float GetCoordinate(float c)
            {
                float r = c;
                if (r < 0)
                    r -= SnapResolution;
                return r -= r % SnapResolution;
            }
        }

        private class GhostObject : ObjectBase
        {
            public GhostObject(GeneralObject o)
                : base(o)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.TopLeft;
            }
            public GhostObject(int id = 1)
                : base(id)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.TopLeft;
            }

            //public void Hide() => Alpha = 0;
            //public void Show() => Alpha = 0.5f;
        }
    }
}
