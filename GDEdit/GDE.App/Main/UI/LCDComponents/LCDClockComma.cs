using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using static GDE.App.Main.UI.LCDComponents.LCDCharacterBar;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDClockComma : Container
    {
        private Circle comma;
        private bool active;

        public bool Active
        {
            get => active;
            set => comma.Colour = (active = value) ? EnabledColor : InactiveColor;
        }

        public LCDClockComma()
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;
            Children = new Drawable[]
            {
                comma = new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(DimensionSize * 1.5f),
                },
            };
        }
    }
}
