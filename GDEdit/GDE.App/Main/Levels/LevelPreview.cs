using GDE.App.Main.Objects;
using GDEdit.Application;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using static GDEdit.Application.ApplicationDatabase;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : Container
    {
        private int i;

        public bool AllowDrag = true;

        public LevelPreview(int index)
        {
            i = index;
            Databases.Add(new Database());

            AutoSizeAxes = Axes.Both;
            
            foreach (var o in Databases[0].UserLevels[i].LevelObjects)
                Add(new ObjectBase(o)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                });
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
