using GDEdit.Application;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using static GDEdit.Application.ApplicationDatabase;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.Tests.Visual
{
    public class TestCaseLoadLevel : TestCase
    {
        public TestCaseLoadLevel()
        {
            Databases.Add(new Database());

            Children = new Drawable[]
            {
                new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    AllowMultiline = true,
                    TextSize = 40,
                    Text = "Loaded: " + Databases[0].UserLevels[0].Name
                }
            };
        }
    }
}
