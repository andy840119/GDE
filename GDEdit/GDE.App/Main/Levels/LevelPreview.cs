using GDEdit.Application;
using GDE.App.Main.Objects;
using GDEdit.Utilities.Objects.GeometryDash;
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

            for (var i = 0; i < 0; i++)
            {
                float scale = 1;

                if (userLevel.LevelObjects[i].Scaling > 0)
                {
                    scale = (float)userLevel.LevelObjects[i].Scaling;
                }

                Vector2 size;

                if (userLevel.LevelObjects[i].FlippedVertically && userLevel.LevelObjects[i].FlippedHorizontally)
                    size = new Vector2(-30 * scale, -30 * scale);
                else if (userLevel.LevelObjects[i].FlippedVertically)
                    size = new Vector2(30 * scale, -30 * scale);
                else if (userLevel.LevelObjects[i].FlippedHorizontally)
                    size = new Vector2(-30 * scale, 30 * scale);
                else
                    size = new Vector2(30 * scale);

                Add(new ObjectBase
                {
                    ObjectID = userLevel.LevelObjects[i].ObjectID,
                    Position = new Vector2((float)userLevel.LevelObjects[i].X, (float)-userLevel.LevelObjects[i].Y),
                    Size = size, // Set this to zoom scale later
                    Rotation = (float)userLevel.LevelObjects[i].Rotation, // fix soon:tm:
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
