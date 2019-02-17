using GDE.App.Main.Colors;
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
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using System.Collections.Generic;
using osu.Framework.Configuration;
using GDE.App.Main.Levels.Metas;
using GDE.App.Main.Overlays;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen
    {
        private FillFlowContainer levelList;
        private LevelCard Card;
        private Toolbar toolbar;
        private OverlayPopup PopUp;
        private Bindable<Levels.Metas.Level> Level = new Bindable<Levels.Metas.Level>(new Levels.Metas.Level
        {
            Name = "Unknown name",
            Song = new Song
            {
                Name = "Unknown name",
            },
        });

        public MainScreen()
        {
            Databases.Add(new Database());

            Children = new Drawable[]
            {
                new DrawSizePreservingFillContainer
                {
                    Strategy = DrawSizePreservationStrategy.Average,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        toolbar = new Toolbar
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 40),
                            Level = Level,
                            Delete = () =>
                            {
                                PopUp.ToggleVisibility();
                            }
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 260,
                            Colour = GDEColors.FromHex("111111"),
                            Margin = new MarginPadding
                            {
                                Top = 40
                            },
                        },
                        new ScrollContainer
                        {
                           RelativeSizeAxes = Axes.Y,
                           Width = 260,
                           Margin = new MarginPadding
                           {
                               Top = 40
                           },
                           Children = new Drawable[]
                           {
                               levelList = new FillFlowContainer
                               {
                                   RelativeSizeAxes = Axes.Both,
                                   Direction = FillDirection.Vertical,
                               }
                           }
                        },
                        PopUp = new OverlayPopup
                        {
                            HeaderText = "Woah woah there!",
                            BodyText = "Are you really sure you want to delete this level?",
                            Button1Text = "Cancel",
                            Button2Text = "Confirm",
                            ConfirmButtonCol = GDEColors.FromHex("c6262e"),
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Size = new Vector2(750, 270)
                        }
                    }
                }
            };

            if (Databases[0].UserLevels.Count == 0)
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
                            Colour = GDEColors.FromHex("666666")
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "There doesn't seem to be anything here...",
                            Font = @"OpenSans",
                            TextSize = 24,
                            Colour = GDEColors.FromHex("666666")
                        },
                        new Button
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(220, 32),
                            Text = "Create a new level",
                            BackgroundColour = GDEColors.FromHex("242424")
                        }
                    }
                });
            }
            else
            {
                for (var i = 0; i < Databases[0].UserLevels.Count; i++)
                {
                    levelList.Add(Card = new LevelCard
                    {
                        RelativeSizeAxes = Axes.X,
                        Size = new Vector2(0.9f, 100),
                        Margin = new MarginPadding(10),
                        Level =
                        {
                            Value = new GDEdit.Utilities.Objects.GeometryDash.Level
                            {
                                CreatorName = "Alten",
                                Name = Databases[0].UserLevels[i].Name,
                                LevelObjects = Databases[0].UserLevels[i].LevelObjects
                            }
                        }
                    });
                }
            }
        }

        private void NewSelection(bool obj)
        {
            //toolbar.Level = card1.Level;
        }
    }
}


/*
 * new Levels.Metas.Level
                            {
                                Name = "Test Level",
                                AuthorName = "Unknown author",
                                Position = 0,
                                Verified = false,
                                Length = 0,
                                Song =new Levels.Metas.Song
                                {
                                    AuthorName = "Unknown author",
                                    Name = "Unknown name",
                                    AuthorNG = "Unkown author NG",
                                    ID = 000000,
                                    Link = "Unkown link"
                                },
                            }
 */
