using GDE.App.Main.Objects;
using GDEdit.Application;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
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

            // We will probably wont need this as of now, for optimization purposes only:tm:
            /*for (var i = 0; i < 35; i++)
            {
                for (var j = 0; j < 50; j++)
                {
                    Add(new Grid
                    {
                        Size = new Vector2(30),
                        Position = new Vector2(30 * j, 30 * i + 23)
                    });
                }
            }*/

            var userLevel = Databases[0].UserLevels[i];

            foreach (var o in userLevel.LevelObjects)
            {
                var size = new Vector2(30 * (float)o.Scaling);
                
                if (o.FlippedVertically)
                    size.Y = -size.Y;
                if (o.FlippedHorizontally)
                    size.X = -size.X;

                Add(new ObjectBase
                {
                    ObjectID = o.ObjectID,
                    Position = new Vector2((float)o.X, (float)-o.Y),
                    Size = size, // Set this to zoom scale later
                    Rotation = (float)o.Rotation, // fix soon:tm:
                    Origin = Anchor.Centre,
                    Anchor = Anchor.CentreLeft
                });
            }
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
