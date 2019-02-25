using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GDE.App.Main.UI.LCDComponents
{
    public abstract class LCDDisplay : FillFlowContainer
    {
        public LCDDisplay()
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Direction = FillDirection.Horizontal;
            Spacing = new Vector2(20);
            CalculateSize();
        }

        protected override void Update()
        {
            CalculateSize();
            base.Update();
        }

        private void CalculateSize()
        {
            float x = 0;
            float max = 0;
            foreach (var c in Children)
            {
                x += c.Size.X;
                if (max < c.Size.Y)
                    max = c.Size.Y;
            }
            Size = new Vector2(x + (Children.Count - 1) * 20, max);
        }
    }
}
