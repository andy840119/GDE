using GDEdit.Application;
using osu.Framework.Screens;
using System;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;
using static GDEdit.Application.Database;

namespace GDE.App.Main.lvl
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

            Console.WriteLine(UserLevels[0].LevelObjects[0]);
        }
    }
}
