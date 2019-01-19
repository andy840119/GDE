using GDE.App.Main.UI;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace GDE.App.Main.Panels
{
    public class CloseButton : Button
    {
        public CloseButton()
        {
            Children = new Drawable[]
            {
                new SpriteIcon
                {
                    Icon = FontAwesome.fa_times_circle_o,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both
                }
            };
        }
    }
}
