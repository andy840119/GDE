using GDE.App.Main.Colors;
using GDE.App.Main.Panels;
using GDE.App.Main.UI.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using System;
using System.Collections.Generic;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public abstract class FileDialog : Panel
    {
        private string fileName;

        private GDEBreadcrumbNavigation<string> filePathBreadcrumbs;
        private FadeSearchContainer fileContainer;

        protected virtual string FileDialogAction { get; set; }
        protected virtual Action ActionButtonAction { get; set; }

        /// <summary>The button that performs the file dialog's action.</summary>
        protected Button ActionButton;

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                // Get the files in the directory that was specified and add update the file container and the file path breadcrumbs accordingly
            }
        }

        public event Action<string> OnFileSelected;

        private readonly string[] testValues =
        {
            "Computer",
            "C:",
            "Users",
            "Alten",
            "AppData",
            "Roaming",
            "GDE",
            "Migrations"
        };

        public FileDialog()
        {
            CornerRadius = 10;
            Masking = true;

            LockDrag = true;

            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColors.FromHex("1A1A1A")
                },
                new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(10),
                    Strategy = DrawSizePreservationStrategy.Minimum,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Children = new Drawable[]
                            {
                                new SpriteText
                                {
                                    Text = $"{FileDialogAction} File"
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Height = 30,
                                    Child = filePathBreadcrumbs = new GDEBreadcrumbNavigation<string>()
                                    {
                                        Height = 30,
                                        RelativeSizeAxes = Axes.X,
                                        Origin = Anchor.CentreLeft,
                                        Anchor = Anchor.CentreLeft,
                                    }
                                },
                                new GDEScrollContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Height = 0.89f,
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = GDEColors.FromHex("404040"),
                                        },
                                        fileContainer = new FadeSearchContainer
                                        {
                                            RelativeSizeAxes = Axes.X,
                                            AutoSizeAxes = Axes.Y,
                                            CornerRadius = 10,
                                            Masking = true,
                                        },
                                    }
                                },
                                new FillFlowContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Height = 0.05f,
                                    Direction = FillDirection.Horizontal,
                                    Spacing = new Vector2(10, 0),
                                    Padding = new MarginPadding
                                    {
                                        Vertical = 5
                                    },
                                    Children = new Drawable[]
                                    {
                                        new Container
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Width = 0.8f,
                                            Children = new Drawable[]
                                            {
                                                new TextBox
                                                {
                                                    PlaceholderText = "Search",
                                                    OnCommit = (sender, newText) => FileName = sender.Text,
                                                    RelativeSizeAxes = Axes.Both,
                                                    Anchor = Anchor.TopLeft,
                                                    Origin = Anchor.TopLeft,
                                                },
                                                new SpriteIcon
                                                {
                                                    Anchor = Anchor.CentreRight,
                                                    Origin = Anchor.CentreRight,
                                                    Size = new Vector2(15),
                                                    Icon = FontAwesome.Solid.Search,
                                                },
                                            }
                                        },
                                        ActionButton = new Button
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Width = 0.15f,
                                            BackgroundColour = GDEColors.FromHex("303030"),
                                            Action = ActionButtonAction,
                                            Text = FileDialogAction,
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            filePathBreadcrumbs.Items.AddRange(testValues);
        }
    }
}
