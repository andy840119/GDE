using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;

namespace GDE.Tests.Visual
{
    [Description("yes'nt")]
    public class TestCaseTestCase : TestCase
    {
        public TestCaseTestCase()
        {
            Children = new Drawable[]
            {
                new SpriteText
                {
                    TextSize = 50,
                    Text = "waitwhat"
                }
            };
        }
    }
}
