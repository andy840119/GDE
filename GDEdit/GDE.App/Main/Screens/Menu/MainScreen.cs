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
        private LevelList levelList;
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
                        levelList = new LevelList
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 260,
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

            levelList.LevelSelected = () =>
            {
                toolbar.Edit = () => this.Push(new Edit.Editor(levelList.LevelIndex, levelList.Cards[levelList.LevelIndex].Level.Value));
                popUp.ConfirmAction = () => database.UserLevels.Remove(levelList.Cards[levelList.LevelIndex].Level.Value);
                level.Value = levelList.Cards[levelList.LevelIndex].Level.Value;
            };

            levelList.CompletedLoading = () => loadWarning.Text = null;
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