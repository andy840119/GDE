using GDE.App.Main.Colours;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osuTK;
using System;

namespace GDE.App.Main.Screens.Menu.Components
{
    public class Toolbar : Container
    {
        public Bindable<string> LevelName = new Bindable<string>("Unknown level");
        public Bindable<string> SongName = new Bindable<string>("Unknown Song");
        public Action Edit;
        public Action Delete;

        public Toolbar()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColours.FromHex("161616")
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Margin = new MarginPadding(5),
                            Text = LevelName.Value,
                            TextSize = 30
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Margin = new MarginPadding(5),
                            Colour = GDEColours.FromHex("666666"),
                            Text = SongName.Value,
                            TextSize = 25,
                        }
                    }
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Button
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Margin = new MarginPadding(5),
                            Size = new Vector2(80, 30),
                            BackgroundColour = GDEColours.FromHex("c6262e"),
                            Text = "Delete",
                            Action = () => Edit?.Invoke()
                        },
                        new Button
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Margin = new MarginPadding(5),
                            Size = new Vector2(80, 30),
                            BackgroundColour = GDEColours.FromHex("242424"),
                            Text = "Edit",
                            Action = () => Delete?.Invoke()
                        }
                    }
                }
            };
        }
    }
}