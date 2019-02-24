using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics;
using osuTK.Graphics;

namespace GDE.App.Main.Levels
{
    public class Grid : Container
    {
        public Grid()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    BorderColour = Color4.Black.Opacity(0.7f),
                    BorderThickness = 2,
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Transparent,
                            AlwaysPresent = true
                        }
                    }
                }
            };
        }
    }
}
