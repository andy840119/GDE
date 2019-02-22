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
    public class LCDClockHorizontalBar : LCDClockBar
    {
        public LCDClockHorizontalBar(bool enabled = false) : base(enabled) { }

        protected override void InitializeBar()
        {
            Size = new Vector2(SizeRatio * DimensionSize, DimensionSize);
            Bar = new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.None,
                Size = new Vector2(SizeRatio * DimensionSize, DimensionSize),
            };
            TriangleA = new Triangle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.None,
                X = -SizeRatio * DimensionSize / 2f - DimensionSize / 4f,
                Size = new Vector2(DimensionSize, DimensionSize / 2f),
                Rotation = -90,
            };
            TriangleB = new Triangle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.None,
                X = SizeRatio * DimensionSize / 2f + DimensionSize / 4f,
                Size = new Vector2(DimensionSize, DimensionSize / 2f),
                Rotation = 90,
            };
        }
    }
}
