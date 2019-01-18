using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;
using GDE.App.Main.Screens.Edit;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;
using GDEdit.Application;

namespace GDE.Tests.Visual
{
    public class TestCaseLoadLevel : TestCase
    {
        public TestCaseLoadLevel()
        {
            TryDecryptLevelData(out Database.DecryptedLevelData);
            GetKeyIndices();
            GetLevels();
            Database.UserLevels[0].LevelString = GetLevelString(0);
            TryDecryptLevelString(0, out Database.UserLevels[0].DecryptedLevelString);

            Children = new Drawable[]
            {
                new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    AllowMultiline = true,
                    TextSize = 40,
                    Text = "Loaded: " + Database.UserLevels[0].LevelName
                }
            };
        }
    }
}
