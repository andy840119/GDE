using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
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
using System.Threading.Tasks;

namespace GDE.App.Main.Screens.Menu.Components
{
    public class LevelList : SearchContainer
    {
        private FillFlowContainer levelList;
        private Database database;
        private LevelCollection levels;
        private TextBox searchQuery;
        private SearchCriteria search;
        private bool finishedLoading;
        private bool alreadyRun;

        public int LevelIndex;
        public Action LevelSelected;
        public Action CompletedLoading;
        public List<LevelCard> Cards;

        public LevelList()
        {
            Children = new Drawable[]
            {
                searchQuery = new TextBox
                {
                    Size = new Vector2(230, 40),
                    Margin = new MarginPadding
                    {
                        Top = 5,
                        Left = 10
                    }
                },
                new ScrollContainer
                {
                   RelativeSizeAxes = Axes.Both,
                   Margin = new MarginPadding
                   {
                       Top = 5,
                   },
                   Children = new Drawable[]
                   {
                       levelList = new FillFlowContainer
                       {
                           LayoutDuration = 100,
                           LayoutEasing = Easing.Out,
                           Spacing = new Vector2(1, 1),
                           RelativeSizeAxes = Axes.X,
                           AutoSizeAxes = Axes.Y,
                           Padding = new MarginPadding(5)
                       }
                   }
                },
            };
            search = new SearchCriteria();

            search.Criteria.Value = searchQuery.Current.Value;

            Cards = new List<LevelCard>();
            searchQuery.Current.ValueChanged += obj =>
            {
                Console.WriteLine(obj.NewValue);
                search.Criteria.Value = obj.NewValue;
            };
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];

            if (!finishedLoading && (finishedLoading = database.GetLevelsStatus >= TaskStatus.RanToCompletion))
            {
                if ((levels = database.UserLevels).Count == 0)
                {
                    AddInternal(new FillFlowContainer
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
                else if (!alreadyRun)
                {
                    for (var i = 0; i < database.UserLevels.Count; i++)
                    {
                        Cards.Add(new LevelCard(search)
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(0.9f, 60),
                            Margin = new MarginPadding(10),
                            index = i,
                            Level =
                            {
                                Value = new Level
                                {
                                    CreatorName = "Alten",
                                    Name = database.UserLevels[i].Name,
                                    LevelObjects = database.UserLevels[i].LevelObjects
                                }
                            }
                        });

                        Logger.Log($"Loaded: {database.UserLevels[i].LevelNameWithRevision}.");
                    }

                    foreach (var i in Cards)
                    {
                        levelList.Add(Cards[i.index]);

                        Cards[i.index].Action = () =>
                        {
                            LevelIndex = i.index;
                            LevelSelected?.Invoke();
                        };

                        Cards[i.index].Selected.ValueChanged += obj =>
                        {
                            if (obj.NewValue == true)
                                foreach (var j in Cards)
                                    if (j != Cards[i.index] && j.Selected.Value == true)
                                        j.Selected.Value = false;
                        };
                    }

                    CompletedLoading?.Invoke();
                    alreadyRun = true;

                    Logger.Log("Loaded all levels successfully.");
                }
            }
        }
    }
}
