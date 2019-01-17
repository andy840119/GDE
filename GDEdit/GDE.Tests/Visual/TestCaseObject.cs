using GDE.App.Main.Objects;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;

namespace GDE.Tests.Visual
{
    public class TestCaseObject : TestCase
    {
        private ObjectBase Obj;

        public TestCaseObject()
        {
            Children = new Drawable[]
            {
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
