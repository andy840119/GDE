using GDE.App.Main.Colors;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK.Graphics;

namespace GDE.App.Main.UI
{
    public class GDEButton : Button
    {
        public GDEButton()
            : base()
        {
            Height = 30;
            CornerRadius = 3;
            Masking = true;

            BackgroundColour = GDEColors.FromHex("303030");
        }

        // Okay peppy we need to fucking talk like men
        // Setting the color of the button's text to yellow by default on the framework is real mad shit
        // This is pretty much a crime against fundamental framework rules
        // While you wanted the text to appear yellow for whatever fucking reason (that color fucking sucks btw), you cannot personalize it on a framework level
        // It makes the most sense for the default color to be white
        // Since it is a neutral color and clashes with the background color which defaults to black IIRC
        // Therefore you've done mad shit and you cannot be forgiven anymore
        // Having to overload this function just because you decided to change the default color to something ugly is so bullshit
        // Just make a fucking FrameworkButton and overload the function there for your beautiful yellow color ffs
        protected override SpriteText CreateText() => new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Depth = -1,
            Font = FrameworkFont.Regular,
            Colour = Color4.White
        };
    }
}
