using GDE.App.Main.Objects;
using GDE.App.Main.UI.FancyThings;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;

namespace GDE.Tests.Visual
{
    public class TestCaseLCDClock : TestCase
    {
        private LCDClockHorizontalBar horizontalBar;
        private LCDClockVerticalBar verticalBar;
        private LCDClock clock;

        public TestCaseLCDClock()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.None;
            Children = new Drawable[]
            {
                //new Box
                //{
                //    Colour = new Color4(95, 95, 95, 255),
                //    RelativeSizeAxes = Axes.Both
                //},
                horizontalBar = new LCDClockHorizontalBar(true),
                verticalBar = new LCDClockVerticalBar(true),
                //clock = new LCDClock
                //{
                //    Origin = Anchor.Centre,
                //    Anchor = Anchor.Centre,
                //    Size = new Vector2(100)
                //}
            };
        }
    }
}
