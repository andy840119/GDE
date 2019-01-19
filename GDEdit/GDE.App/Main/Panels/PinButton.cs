using GDE.App.Main.UI;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace GDE.App.Main.Panels
{
    public class PinButton : ClickableContainer
    {
        public PinButton()
        {
            Children = new Drawable[]
            {
                new SpriteIcon
                {
                    Icon = FontAwesome.fa_thumb_tack,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Rotation = -45
                }
            };
        }
    }
}
