using GDE.App.Main.Colours;
using GDE.App.Main.UI;
using GDE.App.Main.Screens.Menu.Components;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using static GDEdit.Application.ApplicationDatabase;
using GDEdit.Utilities.Objects.GeometryDash;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen
    {
        private FillFlowContainer levelList;

        public MainScreen()
        {
            Children = new Drawable[]
            {
                new DrawSizePreservingFillContainer
                {
                    Strategy = DrawSizePreservationStrategy.Average,
                    Children = new Drawable[]
                    {
                        new Toolbar
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 40),
                            // Update to currently selected level
                            LevelName =
                            {
                                Value = "Level name"
                            },
                            SongName =
                            {
                                Value = "Song name"
                            }
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Size = new Vector2(260, 1f),
                            Colour = GDEColours.FromHex("111111"),
                            Margin = new MarginPadding
                            {
                                Top = 40
                            },
                        },
                        new ScrollContainer
                        {
                           RelativeSizeAxes = Axes.Y,
                           Size = new Vector2(260, 1f),
                           Margin = new MarginPadding
                           {
                               Top = 40
                           },
                           Children = new Drawable[]
                           {
                               levelList = new FillFlowContainer
                               {
                                   RelativeSizeAxes = Axes.Both,
                                   Direction = FillDirection.Vertical
                               }
                           }
                        },
                    }
                }
            };

            /*if (Databases[0].UserLevels.Count == 0)
            {
                Add(new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Spacing = new Vector2(0, 30),
                    Children = new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Icon = FontAwesome.fa_times,
                            Size = new Vector2(192),
                            Colour = GDEColours.FromHex("666666")
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "There doesn't seem to be anything here...",
                            Font = @"OpenSans",
                            TextSize = 24,
                            Colour = GDEColours.FromHex("666666")
                        },
                        new Button
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(220, 32),
                            Text = "Create a new level",
                            BackgroundColour = GDEColours.FromHex("242424")
                        }
                    }
                });
            }
            else
            {*/
                for (var i = 0; i < 3 /*Databases[0].UserLevels.Count*/; i++)
                {
                    levelList.Add(new LevelCard
                    {
                        RelativeSizeAxes = Axes.X,
                        Size = new Vector2(0.9f, 100),
                        Margin = new MarginPadding(10)
                    });
                }
            //}
        }
    }
}
