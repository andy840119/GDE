using GDE.App.Main.Colors;
using GDEdit.Utilities.Objects.GeometryDash;
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
        private SpriteText levelName, songName;

        public Bindable<Level> Level = new Bindable<Level>(new Level
        {
            Name = "Unknown name",
            CreatorName = "UnkownCreator",
        });

        public Action Edit;
        public Action Delete;

        public Toolbar()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColors.FromHex("161616")
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        levelName = new SpriteText
                        {
                            Margin = new MarginPadding(5),
                            Text = Level.Value.Name,
                            TextSize = 30
                        },
                        songName = new SpriteText
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Margin = new MarginPadding(5),
                            Colour = GDEColors.FromHex("666666"),
                            Text = Level.Value.CreatorName,
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
                            BackgroundColour = GDEColors.FromHex("c6262e"),
                            Text = "Delete",
                            Action = () => Delete?.Invoke()
                        },
                        new Button
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Margin = new MarginPadding(5),
                            Size = new Vector2(80, 30),
                            BackgroundColour = GDEColors.FromHex("242424"),
                            Text = "Edit",
                            Action = () => Edit?.Invoke()
                        }
                    }
                }
            };

            Level.ValueChanged += OnChanged;
        }

        private void OnChanged(Level obj)
        {
            levelName.Text = obj.Name;
            songName.Text = obj.CreatorName;
        }
    }
}