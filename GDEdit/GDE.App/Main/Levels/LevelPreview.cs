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
        public bool AllowDrag = true;

        public LevelPreview()
        {
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

            foreach (var o in Databases[0].UserLevels[0].LevelObjects)
            {
                float scale = (float)o.Scaling;

                Vector2 size = new Vector2(30 * scale);
                
                if (o.FlippedVertically)
                    size.Y *= -1;
                if (o.FlippedHorizontally)
                    size.X *= -1;
                 
                Add(new ObjectBase
                {
                    ObjectID = o.ObjectID,
                    Position = new Vector2((float)o.X, (float)-o.Y),
                    Size = size, // Set this to zoom scale later
                    Rotation = (float)o.MathRotationDegrees, // fix soon:tm:
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
