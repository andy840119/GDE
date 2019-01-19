using GDE.App.Main.Levels;
using osu.Framework.Graphics;
using osu.Framework.Testing;

namespace GDE.Tests.Visual
{
    public class TestCaseLevelOverview : TestCase
    {
        public TestCaseLevelOverview()
        {
            Children = new Drawable[]
            {
                new LevelOverview()
            };
        }
    }
}
