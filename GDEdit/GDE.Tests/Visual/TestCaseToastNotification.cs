using GDE.App.Main.Toasts;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;

namespace GDE.Tests.Visual
{
    public class TestCaseToastNotification : TestCase
    {
        private ToastNotification toast;

        public TestCaseToastNotification()
        {
            Children = new Drawable[]
            {
                toast = new ToastNotification
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(400, 30),
                    Margin = new MarginPadding
                    {
                        Bottom = 5
                    },
                    text =
                    {
                        Text = "Hello World!"
                    }
                }
            };

            AddStep("toggle", toast.ToggleVisibility);
        }
    }
}
