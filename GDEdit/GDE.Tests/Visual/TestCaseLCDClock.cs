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
        private LCDClockNumber number;
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
                number = new LCDClockNumber(0, 7, true)
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

            AddSliderStep("Value", 0, 9999999, 0, v => number.Value = v);
            AddStep("Increase number", () => number.Value += number.Value < 9999999 ? 1 : 0);
            AddStep("Decrease number", () => number.Value -= number.Value > 0 ? 1 : 0);
            AddStep("Exponentially increase", () => number.TransformTo("Value", 5000, 1250, Easing.OutQuint));
        }
    }
}
