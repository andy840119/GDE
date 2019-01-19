using GDEdit.Application;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.Tests.Visual
{
    public class TestCaseLoadLevel : TestCase
    {
        public TestCaseLoadLevel()
        {
            TryDecryptLevelData(out Database.DecryptedLevelData);
            GetKeyIndices();
            GetLevels();

            Children = new Drawable[]
            {
                new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    AllowMultiline = true,
                    TextSize = 40,
                    Text = "Loaded: " + Database.UserLevels[0].Name
                }
            };
        }
    }
}
