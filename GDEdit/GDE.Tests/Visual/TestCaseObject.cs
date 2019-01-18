using GDE.App.Main.Objects;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;

namespace GDE.Tests.Visual
{
    public class TestCaseObject : TestCase
    {
        private ObjectBase Obj;

        public TestCaseObject()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(95, 95, 95, 255),
                    RelativeSizeAxes = Axes.Both
                },
                Obj = new ObjectBase
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    ID = 1,
                    Size = new Vector2(100)
                }
            };
        }
    }
}
