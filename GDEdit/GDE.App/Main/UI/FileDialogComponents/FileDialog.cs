using GDE.App.Main.Panels;
using GDE.App.Main.UI.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static GDE.App.Main.Colors.GDEColors;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Directory;
using static System.IO.Path;
using static System.String;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public abstract class FileDialog : Panel
    {
        private string fileName, currentDirectory;

        private GDEBreadcrumbNavigation<string> filePathBreadcrumbs;
        private FadeSearchContainer fileContainer;
        private FillFlowContainer fileFillFlowContainer;
        private TextBox search;

        private DrawableItem currentSelection;

        protected virtual string FileDialogAction { get; set; }

        /// <summary>The button that performs the file dialog's action.</summary>
        protected GDEButton ActionButton;

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                UpdateCurrentDirectory(GetDirectoryName(value));
                UpdateSelectedPath(value);
            }
        }
        public string CurrentDirectory
        {
            get => currentDirectory;
            set
            {
                currentDirectory = value;
                UpdateCurrentDirectory(value);
            }
        }

        public event Action<string> OnFileSelected;

        public FileDialog(string defaultDirectory = null)
        {
            CornerRadius = 10;
            Masking = true;

            LockDrag = true;

            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = FromHex("1a1a1a")
                },
                new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(10),
                    Strategy = DrawSizePreservationStrategy.Minimum,
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Text = $"{FileDialogAction} File",
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Y = 35,
                            Height = 30,
                            Child = filePathBreadcrumbs = new GDEBreadcrumbNavigation<string>()
                            {
                                Origin = Anchor.CentreLeft,
                                Anchor = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.X,
                                Height = 30,
                            }
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding
                            {
                                Top = 70,
                                Bottom = 40,
                            },
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    CornerRadius = 5,
                                    Masking = true,
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = FromHex("121212"),
                                        },
                                        new Container
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Padding = new MarginPadding(10),
                                            Child = new GDEScrollContainer
                                            {
                                                RelativeSizeAxes = Axes.Both,
                                                Children = new Drawable[]
                                                {
                                                    fileContainer = new FadeSearchContainer
                                                    {
                                                        RelativeSizeAxes = Axes.X,
                                                        AutoSizeAxes = Axes.Y,
                                                    },
                                                }
                                            },
                                        },
                                    }
                                },
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.X,
                            Height = 30,
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Padding = new MarginPadding { Right = 110 },
                                    Children = new Drawable[]
                                    {
                                        search = new TextBox
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            RelativeSizeAxes = Axes.Both,
                                            PlaceholderText = "Selected Path",
                                            OnCommit = (sender, newText) => FileName = sender.Text,
                                        },
                                        new SpriteIcon
                                        {
                                            Anchor = Anchor.CentreRight,
                                            Origin = Anchor.CentreRight,
                                            Size = new Vector2(15),
                                            Margin = new MarginPadding { Right = 10 },
                                            Icon = FontAwesome.Solid.Search,
                                        },
                                    }
                                },
                                ActionButton = new GDEButton
                                {
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.CentreRight,
                                    Width = 100,
                                    BackgroundColour = FromHex("303030"),
                                    Action = ActionButtonAction,
                                    Text = FileDialogAction,
                                },
                            }
                        }
                    }
                }
            });

            fileContainer.Add(fileFillFlowContainer = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 2.5f),
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
            });

            CurrentDirectory = defaultDirectory ?? GetFolderPath(MyDocuments);

            filePathBreadcrumbs.BreadcrumbClicked += HandleBreadcrumbClicked;
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (currentSelection != null)
            {
                var index = fileFillFlowContainer.Children.ToList().IndexOf(currentSelection);
                switch (e.Key)
                {
                    case Key.Down:
                        if (index < fileFillFlowContainer.Children.Count - 1)
                            UpdateSelectedPath((fileFillFlowContainer.Children[index + 1] as DrawableItem).ItemName);
                        break;
                    case Key.Up:
                        if (index > 0)
                            UpdateSelectedPath((fileFillFlowContainer.Children[index - 1] as DrawableItem).ItemName);
                        break;
                }
            }
            return base.OnKeyDown(e);
        }

        private void HandleBreadcrumbClicked(string dir) => CurrentDirectory = GetCurrentBreadcrumbsDirectory();

        public bool UpdateSelectedPath(string newPath)
        {
            var replaced = newPath.Replace('/', '\\');
            search.Text = replaced;
            var file = GetIndividualItemName(replaced);
            var type = DetermineItemType(replaced);
            foreach (DrawableItem item in fileFillFlowContainer)
                if (item.ItemName == file && item.ItemType == type)
                    return item.Selected = true;
            return false;
        }
        public void UpdateCurrentDirectory(string directory)
        {
            var dirs = directory.Replace('/', '\\').Split('\\');
            filePathBreadcrumbs.Items.Clear();
            filePathBreadcrumbs.Items.AddRange(dirs);

            fileFillFlowContainer.Clear();
            var directories = GetDirectories(directory);
            var files = GetFiles(directory);
            foreach (var d in directories)
                fileFillFlowContainer.Add(GetNewDrawableItem(GetIndividualItemName(d), ItemType.Directory));
            foreach (var f in files)
                fileFillFlowContainer.Add(GetNewDrawableItem(GetIndividualItemName(f), ItemType.File));
        }

        protected virtual void ActionButtonAction()
        {
            OnFileSelected?.Invoke(FileName);
        }

        private void HandleSelection(DrawableItem selectedItem)
        {
            if (currentSelection != null)
                currentSelection.Selected = false;
            currentSelection = selectedItem;
            UpdateSelectedPath(GetCurrentSelectedPath());
        }
        private void HandleClick(DrawableItem clickedItem)
        {
            if (!clickedItem.Selected)
                currentSelection = null;
        }
        private void HandleDoubleClick(DrawableItem doubleClickedItem)
        {
            if (doubleClickedItem.ItemType == ItemType.Directory)
                CurrentDirectory = GetCurrentSelectedPath();
            else
            {
                OnFileSelected?.Invoke(GetCurrentSelectedPath());
                ToggleVisibility();
            }
        }

        private string GetCurrentBreadcrumbsDirectory() => filePathBreadcrumbs.Items.Aggregate(AggregateBreadcrumbs);
        private string GetCurrentSelectedPath() => $@"{GetCurrentBreadcrumbsDirectory()}\{currentSelection.ItemName}";

        private DrawableItem GetNewDrawableItem(string name, ItemType type) => new DrawableItem(name, type)
        {
            OnSelected = HandleSelection,
            OnClicked = HandleClick,
            OnDoubleClicked = HandleDoubleClick,
        };

        private static string AggregateBreadcrumbs(string left, string right) => $@"{left}\{right}";

        private static string GetIndividualItemName(string path) => path.Replace('/', '\\').Split('\\').Last();

        private static ItemType DetermineItemType(string path)
        {
            if (path.EndsWith('\\') || IsNullOrWhiteSpace(GetExtension(path)))
                return ItemType.Directory;
            return ItemType.File;
        }
    }
}
