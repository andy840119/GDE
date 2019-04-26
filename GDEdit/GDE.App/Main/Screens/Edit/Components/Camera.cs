using GDEdit.Application.Editor;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class Camera : Container
    {
        private Editor editor;

        public Camera(Editor Editor)
        {
            editor = Editor;
        }

        protected override bool OnDrag(DragEvent e)
        {
            foreach (var child in Children)
            {
                child.Position += e.Delta;
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
    }
}
