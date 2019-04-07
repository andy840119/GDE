using GDE.App.Main.Colors;
using GDE.App.Main.Levels;
using GDE.App.Main.Objects;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class Tools : Container
    {
        private Button ObjAdd;
        private Button ObjRemove;
        private Database database;
        private Level level => database.UserLevels[0];

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
        }

        public Tools(LevelPreview Level)
        {
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 15,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = GDEColors.FromHex("1f1f1f"),
                        },
                    }
                },
                new FillFlowContainer()
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                    Padding = new MarginPadding(15),
                    Children = new Drawable[]
                    {
                        ObjAdd = new Button
                        {
                            //Wont do anything as of now.
                            //Action = () => level.LevelObjects.Add(new GeneralObject()),
                            Text = "Add Object",
                            BackgroundColour = GDEColors.FromHex("2f2f2f"),
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 30)
                        },
                        ObjRemove = new Button
                        {
                            Action = () => 
                            {
                                foreach (var o in Level.Objects)
                                    if (o.State == SelectionState.Selected)
                                        level.LevelObjects.Remove(o.LevelObject);
                            },
                            Text = "Remove Object",
                            BackgroundColour = GDEColors.FromHex("2f2f2f"),
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 30)
                        }
                    }
                }
            };
        }
    }
}
