using GDE.App.Main.Containers;
using GDE.App.Main.Objects;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using System.Collections.Generic;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : Container<ObjectBase>, IKeyBindingHandler<GlobalAction>
    {
        private readonly int i;
        private Database database;
        private readonly ObjectBase objects;
        private bool modifier;

        public IReadOnlyList<ObjectBase> Objects => Children;
        public bool AllowDrag = true;
        private Editor editor;

        public Level Level => database.UserLevels[i];

        public LevelPreview(int index, Editor Editor)
        {
            editor = Editor;
            i = index;

            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];

            foreach (var o in Level.LevelObjects)
                Add(new ObjectBase(o, editor));
        }

        protected override bool OnDrag(DragEvent e)
        {
            if (!AllowDrag)
                return false;

            Position += e.Delta;
            return true;
        }

        protected override bool OnDragEnd(DragEndEvent e) => true;
        protected override bool OnDragStart(DragStartEvent e) => AllowDrag;

        public bool OnPressed(GlobalAction action)
        {
            var val = modifier ? Editor.SmallMovementStep : Editor.NormalMovementStep;

            foreach (var i in Objects)
            {
                if (i.State == SelectionState.Selected)
                    switch (action)
                    {
                        case GlobalAction.ObjMoveRight:
                            i.ObjectX += val;
                            break;
                        case GlobalAction.ObjMoveLeft:
                            i.ObjectX -= val;
                            break;
                        case GlobalAction.ObjMoveUp:
                            i.ObjectY += val;
                            break;
                        case GlobalAction.ObjMoveDown:
                            i.ObjectY -= val;
                            break;
                    }
            }

            switch (action)
            {
                case GlobalAction.ObjMoveModifier:
                    modifier = !modifier;
                    break;
            }

            return true;
        }

        public bool OnReleased(GlobalAction action) => true;
    }
}
