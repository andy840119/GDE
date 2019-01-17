using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;
using GDE.App.Main.Screens.Edit;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;
using GDEdit.Application;
using GDE.App.Main.Objects;

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
                    Size = new osuTK.Vector2(100)
                }
            };
        }
    }
}
