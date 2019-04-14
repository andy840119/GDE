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

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        private SpriteText loadWarning;
        private Database database;
        private LevelList levelList;
        private List<LevelCard> cards = new List<LevelCard>();
        private Toolbar toolbar;
        private OverlayPopup popUp;
        private Bindable<Level> level = new Bindable<Level>();

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
                            Delete = () => popUp.ToggleVisibility(),
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
                        levelList = new LevelList
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 260,
                            Padding = new MarginPadding
                            {
                                Top = 40,
                                Bottom = 40
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

            toolbar.Level.BindTo(level);
            level.ValueChanged += ChangeLevel;

            levelList.LevelSelected = () =>
            {
                var selectedLevel = levelList.LevelIndex > -1 ? levelList.Cards[levelList.LevelIndex].Level.Value : null;
                level.Value = selectedLevel;
                toolbar.Level.TriggerChange(); // Fuck why is this necessary?
                toolbar.Edit = levelList.LevelIndex > -1 ? (Action)(() => this.Push(new Edit.Editor(levelList.LevelIndex, selectedLevel))) : null;
                popUp.ConfirmAction = () => database.UserLevels.Remove(selectedLevel);
            };

            levelList.CompletedLoading = () => loadWarning.Text = null;
        }

        private void ChangeLevel(ValueChangedEvent<Level> obj)
        {
            Logger.Log($"Changed level to: {obj.NewValue?.LevelNameWithRevision ?? "null"}.");
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
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