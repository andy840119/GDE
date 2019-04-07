using GDE.App.Main.Containers;
using GDE.App.Main.Objects;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : Container<ObjectBase>, IKeyBindingHandler<GlobalAction>
    {
        private int i;
        private Database database;
        private ObjectBase objects;
        private bool modifier;

        public IReadOnlyList<ObjectBase> Objects => Children;
        public bool AllowDrag = true;

        public Level Level => database.UserLevels[i];

        public LevelPreview(int index)
        {
            i = index;
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
            foreach (var o in Level.LevelObjects)
                Add(new ObjectBase(o));
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
            foreach (var i in Objects)
            {
                if (i.State == SelectionState.Selected)
                    switch (action)
                    {
                        case GlobalAction.ObjMoveRight:
                            i.ObjectX += modifier ? 2 : 30;
                            break;
                        case GlobalAction.ObjMoveLeft:
                            i.ObjectX -= modifier ? 2 : 30;
                            break;
                        case GlobalAction.ObjMoveUp:
                            i.ObjectY += modifier ? 2 : 30;
                            break;
                        case GlobalAction.ObjMoveDown:
                            i.ObjectY -= modifier ? 2 : 30;
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
