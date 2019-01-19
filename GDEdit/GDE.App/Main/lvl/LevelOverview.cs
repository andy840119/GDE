using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colours;
using GDE.App.Main.UI;
using GDE.App.Main.UI.Toolbar;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System.Collections.Generic;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.App.Main.lvl
{
    public class LevelOverview : Screen
    {
        public LevelOverview()
        {
            TryDecryptLevelData(out Database.DecryptedLevelData);
            GetKeyIndices();
            GetLevels();
            Database.UserLevels[0].LevelString = GetLevelString(0);
            TryDecryptLevelString(0, out Database.UserLevels[0].DecryptedLevelString);

            System.Console.WriteLine(Database.UserLevels[0].LevelObjects);
        }
    }
}
