using GDE.App.Main.Objects;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : Container
    {
        private int i;
        private Database database;

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
    }
}
