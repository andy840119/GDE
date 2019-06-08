using GDE.App.Main.Screens.Edit.Components;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GDE.Tests.Visual.TestCaseEditor
{
    public class TestCaseCamera : TestCase
    {
        public TestCaseCamera()
        {
            Children = new Drawable[]
            {
                new Camera(null)
                {
                    Size = new Vector2(150),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Size = new Vector2(30),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        }
                    }
                }
            };
        }
    }
}
