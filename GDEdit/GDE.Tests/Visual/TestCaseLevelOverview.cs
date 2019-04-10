using GDE.App.Main.Levels;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Testing;
using osuTK.Graphics;

namespace GDE.Tests.Visual
{
    public class TestCaseLevelOverview : TestCase
    {
        private LevelPreview lvlOverview;
        public TestCaseLevelOverview()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(95, 95, 95, 255),
                    RelativeSizeAxes = Axes.Both
                },
                lvlOverview = new LevelPreview(0)
            };
        }
    }
}
