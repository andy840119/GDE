using GDEdit.Application;
using GDE.App.Main.Objects;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using System;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;
using static GDEdit.Application.Database;

namespace GDE.App.Main.Levels
{
    public class LevelOverview : Screen
    {
        public LevelOverview()
        {
            TryDecryptLevelData(out DecryptedLevelData);
            GetKeyIndices();
            GetLevels();
            UserLevels[0].LevelString = GetLevelString(0);
            TryDecryptLevelString(0, out UserLevels[0].DecryptedLevelString);
            UserLevels[0].LevelObjects = GetObjects(GetObjectString(UserLevels[0].DecryptedLevelString));

            for (var i = 0; i < 60; i++)
            {
                Add(new ObjectBase
                {
                    ObjectID = UserLevels[0].LevelObjects[i].ObjectID,
                    Position = new osuTK.Vector2((float)UserLevels[0].LevelObjects[i].X, (float)-UserLevels[0].LevelObjects[i].Y),
                    Size = new osuTK.Vector2(30), // Set this to zoom scale later
                    Rotation = (float)UserLevels[0].LevelObjects[i].Rotation, // fix soon:tm:
                    Origin = Anchor.BottomLeft,
                    Anchor = Anchor.BottomLeft
                });
            }
        }
    }
}
