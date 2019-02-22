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
        public override IReadOnlyList<Type> RequiredTypes => new[] { typeof(LCDClock), typeof(LCDClockBar), typeof(LCDClockNumber) };

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
                number = new LCDClockNumber
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
            
            AddStep("Increase number", () => number.Value++);
            AddStep("Decrease number", () => number.Value--);
        }
    }
}
