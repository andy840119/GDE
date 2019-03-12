using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;
using System.Linq;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDClock : LCDDisplay
    {
        private bool showMS;

        private LCDNumber h, m, s, ms;
        private LCDClockSeparator left, right;
        private LCDClockComma comma;

        public LCDClock(bool includeMS = false)
            : base()
        {
            Children = GetDrawables(showMS = includeMS);
        }

        public Drawable[] GetDrawables(bool includeMS) => GetStandardDrawables().Concat(includeMS ? GetMSDrawables() : new Drawable[0]).ToArray();
        public Drawable[] GetStandardDrawables() => new Drawable[]
        {
            h = new LCDNumber(0, 2, true),
            left = new LCDClockSeparator(),
            m = new LCDNumber(0, 2, false),
            right = new LCDClockSeparator(),
            s = new LCDNumber(0, 2, false),
        };
        public Drawable[] GetMSDrawables() => new Drawable[]
        {
            comma = new LCDClockComma(),
            ms = new LCDNumber(0, 3, false),
        };

        protected override void Update()
        {
            var now = DateTime.Now;
            h.Value = now.Hour;
            left.Active = !(m.DeactivateTrailingZeroes = h.Value == 0);
            m.Value = now.Minute;
            right.Active = !(s.DeactivateTrailingZeroes = h.Value == 0 && m.Value == 0);
            s.Value = now.Second;
            if (showMS)
            {
                comma.Active = !(ms.DeactivateTrailingZeroes = h.Value == 0 && m.Value == 0 && s.Value == 0);
                ms.Value = now.Millisecond;
            }
            base.Update();
        }
    }
}
