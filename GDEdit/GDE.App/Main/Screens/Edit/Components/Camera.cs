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

        public Camera(Editor Editor)
        {
            editor = Editor;
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

        public void AddGhostObject(GeneralObject o) => AddGhostObject(new GhostObject(o));
        public void AddGhostObject(int id) => AddGhostObject(new GhostObject(id));

        private void AddGhostObject(GhostObject g)
        {
            Add(new GridSnappedCursorContainer(cameraOffsetBindable)
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[] { g }
            });
        }

        private class GridSnappedCursorContainer : CursorContainer, IRequireHighFrequencyMousePosition
        {
            public int SnapResolution { get; set; }

            public readonly Bindable<Vector2> CameraOffset = new Bindable<Vector2>();

            public GridSnappedCursorContainer(Bindable<Vector2> cameraOffsetBindable, int snapResolution = 30)
                : base()
            {
                CameraOffset.BindTo(cameraOffsetBindable);
                SnapResolution = snapResolution;
            }

            protected override bool OnMouseMove(MouseMoveEvent e)
            {
                foreach (var child in Children)
                    child.Position = ConvertMousePositionToEditor(e.ScreenSpaceMousePosition - CameraOffset.Value);

                return true;
            }

            public Vector2 ConvertMousePositionToEditor(Vector2 mousePosition)
            {
                float x = GetCoordinate(mousePosition.X);
                float y = GetCoordinate(mousePosition.Y);
                return new Vector2(x, y) + CameraOffset.Value;
            }

            protected override bool OnClick(ClickEvent e)
            {
                foreach (var child in Children)
                    child.Expire();

                return base.OnClick(e);
            }

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
            public GhostObject(int id)
                : base(id)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.TopLeft;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Alpha = 0.5f;
            }
        }
    }
}
