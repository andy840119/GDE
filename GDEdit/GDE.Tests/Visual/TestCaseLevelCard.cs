using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;
using GDE.App.Main.Screens.Menu.Components;
using osuTK;

namespace GDE.Tests.Visual
{
    public class TestCaseLevelCard : TestCase
    {
        public TestCaseLevelCard()
        {
            Children = new Drawable[]
            {
                new LevelCard
                {
                    Size = new Vector2(250, 100),
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre
                }
            };
        }
    }
}
