using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Objects;
using GDE.App.Main.Overlays;
using GDE.App.Main.Panels;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class ObjectAdditionPanel : Panel
    {
        protected override string Name => "Object Addition";

        private FillFlowContainer container;

        public ObjectAdditionPanel(GDEdit.Application.Editor.Editor editor)
        {
            Add(container = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Spacing = new Vector2(5),
                Margin = new MarginPadding
                {
                    Top = 35,
                    Horizontal = 5
                },
            });

            for (var i = 1; i < 10; i++)
            {
                ObjectButton ObjButton;

                container.Add(ObjButton = new ObjectButton
                {
                    Size = new Vector2(40),
                    Object =
                    {
                        ObjectID = i
                    }
                });

                //Your turn alex.
                //ObjButton.Action = () => 
                //{
                    //ToggleVisibility();
                //}
            }
        }

        private class ObjectButton : Button
        {
            public ObjectBase Object;

            public ObjectButton()
            {
                BackgroundColour = GDEColors.FromHex("333");

                Add(Object = new ObjectBase(new GeneralObject(), null)
                {
                    Depth = -1,
                });
            }
        }
    }
}
