using GDEdit.Application;
using GDE.App.Main.Objects;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Input.Events;
using System;
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

            for (var i = 0; i < 2299; i++)
            {
                float scale = 1;

                if (Databases[0].UserLevels[0].LevelObjects[i].Scaling > 0)
                {
                    scale = (float)Databases[0].UserLevels[0].LevelObjects[i].Scaling;
                }

                Vector2 size;

                if (Databases[0].UserLevels[0].LevelObjects[i].FlippedVertically && Databases[0].UserLevels[0].LevelObjects[i].FlippedHorizontally)
                    size = new Vector2(-30 * scale, -30 * scale);
                else if (Databases[0].UserLevels[0].LevelObjects[i].FlippedVertically)
                    size = new Vector2(30 * scale, -30 * scale);
                else if (Databases[0].UserLevels[0].LevelObjects[i].FlippedHorizontally)
                    size = new Vector2(-30 * scale, 30 * scale);
                else
                    size = new Vector2(30 * scale);
                 
                Add(new ObjectBase
                {
                    ObjectID = Databases[0].UserLevels[0].LevelObjects[i].ObjectID,
                    Position = new Vector2((float)Databases[0].UserLevels[0].LevelObjects[i].X, (float)-Databases[0].UserLevels[0].LevelObjects[i].Y),
                    Size = size, // Set this to zoom scale later
                    Rotation = (float)Databases[0].UserLevels[0].LevelObjects[i].Rotation, // fix soon:tm:
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
