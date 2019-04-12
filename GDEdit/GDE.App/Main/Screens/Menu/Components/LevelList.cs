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
                }
            };

            Cards = new List<LevelCard>();
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
        }

        protected override void Update()
        {
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
                        Cards.Add(new LevelCard
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(0.9f, 100),
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
                    }

                    CompletedLoading?.Invoke();
                    alreadyRun = true;

                    Logger.Log("Loaded all levels successfully.");
                }
            }

            base.Update();
        }
    }
}
