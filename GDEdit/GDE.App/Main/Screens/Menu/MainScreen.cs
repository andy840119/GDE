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
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using System.Collections.Generic;
using osu.Framework.Configuration;
using GDE.App.Main.Levels.Metas;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen
    {
        private FillFlowContainer levelList;
        private LevelCard Card, card1;
        private Toolbar toolbar;
        private Bindable<Levels.Metas.Level> Level = new Bindable<Levels.Metas.Level>(new Levels.Metas.Level
        {
            AuthorName = "Unknown author",
            Name = "Unknown name",
            Position = 0,
            Verified = false,
            Length = 0,
            Song = new Song
            {
                AuthorName = "Unknown author",
                Name = "Unknown name",
                AuthorNG = "Unkown author NG",
                ID = 000000,
                Link = "Unkown link"
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
                    Children = new Drawable[]
                    {
                        toolbar = new Toolbar
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 40),
                            Level = Level
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
                                   Direction = FillDirection.Vertical,
                                   Children = new Drawable[]
                                   {
                                       card1 = new LevelCard
                                       {
                                           RelativeSizeAxes = Axes.X,
                                           Size = new Vector2(0.9f, 100),
                                           Margin = new MarginPadding(10),
                                           Level =
                                           {
                                               Value = new Levels.Metas.Level
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
                                           },
                                       }
                                   }
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
                    levelList.Add(Card = new LevelCard
                    {
                        RelativeSizeAxes = Axes.X,
                        Size = new Vector2(0.9f, 100),
                        Margin = new MarginPadding(10),
                    });
                }
            //}

            card1.Selected.ValueChanged += NewSelection;
        }

        private void NewSelection(bool obj)
        {
            toolbar.Level = card1.Level;
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
