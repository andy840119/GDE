using GDE.App.Main.Colors;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using System;

namespace GDE.App.Main.UI.FancyThings
{
    public class LCDClock : FillFlowContainer
    {
        private LCDNumber h, m, s;
        private LCDClockSeparator left, right;

        public LCDClock()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Direction = FillDirection.Horizontal;
            Spacing = new Vector2(20);
            Children = new Drawable[]
            {
                h = new LCDNumber(0, 2, true),
                left = new LCDClockSeparator(),
                m = new LCDNumber(0, 2, false),
                right = new LCDClockSeparator(),
                s = new LCDNumber(0, 2, false),
            };
            Size = new Vector2(h.Size.X * 3 + left.Size.X * 2 + (Children.Count - 1) * 20, h.Size.Y);
        }

        protected override void Update()
        {
            var now = DateTime.Now;
            h.Value = now.Hour;
            m.DeactivateTrailingZeroes = h.Value == 0;
            m.Value = now.Minute;
            s.DeactivateTrailingZeroes = h.Value == 0 && m.Value == 0;
            s.Value = now.Second;
        }
    }
}
