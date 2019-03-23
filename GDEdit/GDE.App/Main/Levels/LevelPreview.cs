using GDE.App.Main.Objects;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : CompositeDrawable
    {
        private int i;
        private Database database;
        private Container<ObjectBase> objectFlow;
        private ObjectBase objects;

        public IReadOnlyList<ObjectBase> Objects => objectFlow;
        public bool AllowDrag = true;

        public Level Level => database.UserLevels[i];

        public LevelPreview(int index)
        {
            i = index;
            AutoSizeAxes = Axes.Both;

            InternalChild = objectFlow = new Container<ObjectBase>
            {
                AutoSizeAxes = Axes.Both
            };
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
            foreach (var o in Level.LevelObjects)
                objectFlow.Add(new ObjectBase(o));
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
    }
}
