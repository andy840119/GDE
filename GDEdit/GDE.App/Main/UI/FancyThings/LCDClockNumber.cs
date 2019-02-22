using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osu.Framework;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colors;
using osu.Framework.Input.Events;
using System;

namespace GDE.App.Main.UI.FancyThings
{
    public class LCDClockNumber : Container
    {
        public const int Spacing = 2;

        private static readonly int[,] Numbers = new int[,]
        {
            // 0
            {
                   1,
                1,    1,
                   0,
                1,    1,
                   1,
            },
            // 1
            {
                   0,
                0,    1,
                   0,
                0,    1,
                   0,
            },
            // 2
            {
                   1,
                0,    1,
                   1,
                1,    0,
                   1,
            },
            // 3
            {
                   1,
                0,    1,
                   1,
                0,    1,
                   1,
            },
            // 4
            {
                   0,
                1,    1,
                   1,
                0,    1,
                   0,
            },
            // 5
            {
                   1,
                1,    0,
                   1,
                0,    1,
                   1,
            },
            // 6
            {
                   1,
                1,    0,
                   1,
                1,    1,
                   1,
            },
            // 7
            {
                   1,
                1,    1,
                   0,
                0,    1,
                   0,
            },
            // 8
            {
                   1,
                1,    1,
                   1,
                1,    1,
                   1,
            },
            // 9
            {
                   1,
                1,    1,
                   1,
                0,    1,
                   1,
            },
        };

        private int v;

        private LCDClockBar[] bars = new LCDClockBar[]
        {
            new LCDClockHorizontalBar
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
            },
            new LCDClockVerticalBar
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                Y = -(LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1)) / 2f - Spacing,
            },
            new LCDClockVerticalBar
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.Centre,
                Y = -(LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1)) / 2f - Spacing,
            },
            new LCDClockHorizontalBar
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            },
            new LCDClockVerticalBar
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                Y = (LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1)) / 2f + Spacing,
            },
            new LCDClockVerticalBar
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.Centre,
                Y = (LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1)) / 2f + Spacing,
            },
            new LCDClockHorizontalBar
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.Centre,
            },
        };
        
        public int Value
        {
            get => v;
            set
            {
                if (value > 9 || value < 0)
                    throw new InvalidOperationException("Cannot set the value of the LCD number to a number outside the range [0, 9].");
                v = value;
                for (int i = 0; i < bars.Length; i++)
                    bars[i].Enabled = Numbers[value, i] == 1;
            }
        }

        public LCDClockNumber(int value = 0)
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1) + Spacing * 2, LCDClockBar.Dimension * (LCDClockBar.SizeRatio + 1) * 2 + Spacing * 4);
            Children = bars;
            Value = value;
        }
    }
}
