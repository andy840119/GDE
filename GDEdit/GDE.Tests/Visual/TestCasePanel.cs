using osu.Framework.Graphics;
using osu.Framework.Testing;
using GDE.App.Main.Panels;

namespace GDE.Tests.Visual
{
    public class TestCasePanel : TestCase
    {
        private Panel panel;

        public TestCasePanel()
        {
            Children = new Drawable[]
            {
                panel = new Panel
                {
                    Size = new osuTK.Vector2(335, 557)
                }
            };

            AddStep("Remove", panel.Hide);
            AddStep("Add", panel.Show);

            AddStep("Drag enabled", () => panel.AllowDrag = true);
            AddStep("Drag disabled", () => panel.AllowDrag = false);
        }
    }
}
