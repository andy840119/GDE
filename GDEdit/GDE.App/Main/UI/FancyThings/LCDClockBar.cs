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
    public abstract class LCDClockBar : Container
    {
        public static readonly Color4 EnabledColor = new Color4(255, 255, 255, 255);
        public static readonly Color4 DisabledColor = new Color4(32, 32, 32, 255);

        /// <summary>The size ratio of the box.</summary>
        public const int SizeRatio = 15;
        /// <summary>The size of each dimension of the box.</summary>
        public const int Dimension = 6;

        private bool enabled;

        protected Box Bar;
        protected Triangle TriangleA, TriangleB;

        public bool Enabled
        {
            get => enabled;
            set => Bar.Colour = TriangleA.Colour = TriangleB.Colour = (enabled = value) ? EnabledColor : DisabledColor;
        }

        public LCDClockBar(bool enabled = false) : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InitializeBar();
            Children = new Drawable[]
            {
                Bar,
                TriangleA,
                TriangleB,
            };
            Enabled = enabled;
        }

        protected abstract void InitializeBar();
    }
}
