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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        private bool finishedLoading;
        private bool alreadyRun = false;

        private SpriteText loadWarning;
        private Database database;
        private LevelCollection levels;
        private FillFlowContainer levelList;
        private List<LevelCard> card = new List<LevelCard>();
        private Toolbar toolbar;
        private OverlayPopup popUp;
        private Bindable<Level> level = new Bindable<Level>(new Level
        {
            Name = "Unknown name",
            CreatorName = "Unkown creator"
        });

        public MainScreen()
        {
            RPC.updatePresence("Looking for levels", null, new DiscordRPC.Assets
            {
                LargeImageKey = "gde",
                LargeImageText = "GD Edit"
            });

            AddRangeInternal(new Drawable[]
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
                            Level = level,
                            Delete = () => popUp.ToggleVisibility(),
                            Edit = () => this.Push(new Edit.Editor(0, level.Value))
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
                        popUp = new OverlayPopup
                        {
                            HeaderText = "Woah there!",
                            BodyText = "Are you really sure you want to delete this level?",
                            Button1Text = "Cancel",
                            Button2Text = "Confirm",
                            ConfirmButtonColor = GDEColors.FromHex("c6262e"),
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Size = new Vector2(750, 270)
                        },
                        loadWarning = new SpriteText
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Text = "Loading please wait",
                            Font = new FontUsage(size: 60)
                        }
                    }
                }
            });

            level.ValueChanged += ChangeLevel;
        }

        private void ChangeLevel(ValueChangedEvent<Level> obj)
        {
            Logger.Log($"Changed level to: {obj.NewValue.LevelNameWithRevision}.");
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
                        card.Add(new LevelCard
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

                    foreach (var i in card)
                    {
                        levelList.Add(card[i.index]);

                        card[i.index].Action = () =>
                        {
                            toolbar.Edit = () => this.Push(new Edit.Editor(i.index, i.Level.Value));
                            popUp.ConfirmAction = () => database.UserLevels.Remove(i.Level.Value);
                            level.Value = card[i.index].Level.Value;
                        };
                    }

                    loadWarning.Text = "";
                    alreadyRun = true;

                    Logger.Log("Loaded all levels successfully.");
                }
            }

            base.Update();
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.LordsKeys:
                    this.Push(new Lords());
                    break;
            }

            return true;
        }

        public bool OnReleased(GlobalAction action) => true;
    }
}