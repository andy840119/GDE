using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.UI;
using GDE.App.Main.Screens.Menu.Components;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Configuration;
using osu.Framework.Input.Bindings;
using osuTK;
using static GDEdit.Application.ApplicationDatabase;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        private FillFlowContainer levelList;
        private LevelCard Card;
        private Toolbar toolbar;
        private OverlayPopup PopUp;
        private Bindable<Level> Level = new Bindable<Level>(new Level
        {
            Name = "Unknown name",
            CreatorName = "Unkown creator"
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
                            },
                            Edit = () => Push(new Edit.Editor(0))
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
                            HeaderText = "Woah there!",
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
                            Value = new Level
                            {
                                CreatorName = "Alten",
                                Name = Databases[0].UserLevels[i].Name,
                                LevelObjects = Databases[0].UserLevels[i].LevelObjects
                            }
                        }
                    });
                }
            }

            Card.Selected.ValueChanged += NewSelection;
        }

        private void NewSelection(bool obj)
        {
            toolbar.Level = Card.Level;
            //TODO: make level editable from selection
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.LordsKeys:
                    Push(new Lords());
                    break;
            }

            return true;
        }

        public bool OnReleased(GlobalAction action) => true;
    }
}