using GDEdit.Application;
using GDE.App.Main.Objects;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Input.Events;
using System;
using osuTK;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;
using static GDEdit.Application.Database;

namespace GDE.App.Main.Levels
{
    public class LevelOverview : Container
    {
        public bool AllowDrag = true;

        public LevelOverview()
        {
            TryDecryptLevelData(out DecryptedLevelData);
            GetKeyIndices();
            GetLevels();
            UserLevels[0].LevelString = GetLevelString(0);
            TryDecryptLevelString(0, out UserLevels[0].DecryptedLevelString);
            UserLevels[0].LevelObjects = GetObjects(GetObjectString(UserLevels[0].DecryptedLevelString));

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

                if (UserLevels[0].LevelObjects[i].Scaling > 0)
                {
                    scale = (float)UserLevels[0].LevelObjects[i].Scaling;
                }

                Add(new ObjectBase
                {
                    ObjectID = UserLevels[0].LevelObjects[i].ObjectID,
                    Position = new Vector2((float)UserLevels[0].LevelObjects[i].X, (float)-UserLevels[0].LevelObjects[i].Y),
                    Size = new Vector2(30 * scale), // Set this to zoom scale later
                    Rotation = (float)UserLevels[0].LevelObjects[i].Rotation, // fix soon:tm:
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
