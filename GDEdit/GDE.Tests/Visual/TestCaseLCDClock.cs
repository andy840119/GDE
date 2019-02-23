using System;
using System.Collections.Generic;
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
        public override IReadOnlyList<Type> RequiredTypes => new[] { typeof(LCDClock), typeof(LCDClockBar), typeof(LCDClockDigit) };

        private LCDClockHorizontalBar horizontalBar;
        private LCDClockVerticalBar verticalBar;
        private LCDClockDigit number;
        //private LCDClock clock;

        public TestCaseLCDClock()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                horizontalBar = new LCDClockHorizontalBar(true)
                {
                    Y = -100,
                },
                verticalBar = new LCDClockVerticalBar(true)
                {
                    Y = -100,
                },
                number = new LCDClockDigit
                {
                    Y = 100,
                },
                //clock = new LCDClock
                //{
                //    Origin = Anchor.Centre,
                //    Anchor = Anchor.Centre,
                //    Size = new Vector2(100)
                //}
            };
            
            AddToggleStep("Active number", a => number.Active = a);
            AddStep("Increase number", () => number.Value += number.Value < 9 ? 1 : 0);
            AddStep("Decrease number", () => number.Value -= number.Value > 0 ? 1 : 0);
        }
    }
}
