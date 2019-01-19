using GDE.App.Main.Levels;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK.Graphics;

namespace GDE.Tests.Visual
{
    public class TestCaseLevelOverview : TestCase
    {
        public TestCaseLevelOverview()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(95, 95, 95, 255),
                    RelativeSizeAxes = Axes.Both
                },
                new LevelOverview()
            };
        }
    }
}
