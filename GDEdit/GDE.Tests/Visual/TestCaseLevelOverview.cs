using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;
using GDE.App.Main.lvl;

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
