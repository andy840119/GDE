using GDE.App.Main.Overlays;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;

namespace GDE.Tests.Visual
{
    public class TestCaseOverlayPopup : TestCase
    {
        private OverlayPopup popup;

        public TestCaseOverlayPopup()
        {
            Children = new Drawable[]
            {
                popup = new OverlayPopup
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(750, 270),
                    HeaderText = "Message1",
                    BodyText = "Message2",
                    Button1Text = "Button1",
                    Button2Text = "Button2"
                }
            };
            AddStep("toggle", popup.ToggleVisibility);
        }
    }
}
