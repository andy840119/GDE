using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using static GDE.App.Main.UI.FancyThings.LCDDigitBar;

namespace GDE.App.Main.UI.FancyThings
{
    public class LCDClockSeparator : Container
    {
        public LCDClockSeparator()
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Children = new Drawable[]
            {
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(DimensionSize * 1.5f),
                    Y = -DimensionSize * 3,
                },
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(DimensionSize * 1.5f),
                    Y = DimensionSize * 3
                },
            };
        }
    }
}
