using GDE.App.Main.Screens.Edit;
using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Testing;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDE.App.Main.Objects;
using GDE.App.Main.Screens.Edit.Components;

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
