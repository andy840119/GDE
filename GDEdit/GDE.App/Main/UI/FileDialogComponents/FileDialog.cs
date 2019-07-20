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
using static System.Math;
using static System.String;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public abstract class FileDialog : Panel
    {
        public const float ItemSpacing = 2.5f;

        private string selectedPath, currentDirectory;

        private GDEBreadcrumbNavigation<string> filePathBreadcrumbs;
        private GDEScrollContainer scrollContainer;
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
                UpdateCurrentDirectory(GetDirectoryName(value));
                UpdateSelectedPath(value);
            }
        }
        public string CurrentDirectory
        {
            get => currentDirectory;
            set
            {
                UpdateCurrentDirectory(value);
                UpdateSelectedPath(value);
                CurrentSelection = null;
            }
        }
        protected DrawableItem CurrentSelection
        {
            get => currentSelection;
            set => ActionButton.Enabled.Value = (currentSelection = value) != null;
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
                                            Child = scrollContainer = new GDEScrollContainer
                                            {
                                                RelativeSizeAxes = Axes.Both,
                                                Children = new Drawable[]
                                                {
                                                    fileContainer = new FadeSearchContainer
                                                    {
                                                        RelativeSizeAxes = Axes.X,
                                                        AutoSizeAxes = Axes.Y,
                                                        Child = fileFillFlowContainer = new FillFlowContainer
                                                        {
                                                            Direction = FillDirection.Vertical,
                                                            Spacing = new Vector2(0, ItemSpacing),
                                                            RelativeSizeAxes = Axes.X,
                                                            AutoSizeAxes = Axes.Y,
                                                        }
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
                                ActionButton = new FadeButton
                                {
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.CentreRight,
                                    Width = 100,
                                    EnabledColor = FromHex("303030"),
                                    Action = ActionButtonAction,
                                    Text = FileDialogAction,
                                },
                            }
                        }
                    }
                }
            });

            CurrentDirectory = defaultDirectory ?? GetFolderPath(MyDocuments);

            filePathBreadcrumbs.BreadcrumbClicked += HandleBreadcrumbClicked;
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (CurrentSelection != null)
            {
                var index = fileFillFlowContainer.Children.ToList().IndexOf(CurrentSelection);
                switch (e.Key)
                {
                    case Key.Down:
                        if (index < fileFillFlowContainer.Children.Count - 1)
                            NavigateTo(index + 1);
                        break;
                    case Key.Up:
                        if (index > 0)
                            NavigateTo(index - 1);
                        break;
                    case Key.PageUp:
                        NavigateTo(Max(index - GetItemCountPerPage(), 0));
                        break;
                    case Key.PageDown:
                        NavigateTo(Min(index + GetItemCountPerPage(), fileFillFlowContainer.Count - 1));
                        break;
                    case Key.Home:
                        NavigateTo(0);
                        break;
                    case Key.End:
                        NavigateTo(fileFillFlowContainer.Count - 1);
                        break;
                }
            }
            return base.OnKeyDown(e);
        }

        private void NavigateTo(int index) => UpdateSelectedPath((fileFillFlowContainer.Children[index] as DrawableItem).ItemName);

        private void HandleBreadcrumbClicked(string dir) => CurrentDirectory = GetCurrentBreadcrumbsDirectory();

        public bool UpdateSelectedPath(string newPath)
        {
            var replaced = newPath.Replace('/', '\\');
            fileName = search.Text = replaced;
            var dirs = replaced.Split('\\');
            if (dirs.Length < filePathBreadcrumbs.Items.Count)
            {
                var file = GetIndividualItemName(replaced);
                var type = DetermineItemType(replaced);
                foreach (DrawableItem item in fileFillFlowContainer)
                    if (item.MatchesNameAndType(file, type))
                    {
                        scrollContainer.ScrollIntoView(item);
                        return item.Selected = true;
                    }
            }
            return false;
        }
        public void UpdateCurrentDirectory(string directory)
        {
            var replaced = directory.Replace('/', '\\');
            currentDirectory = replaced;
            var dirs = replaced.Split('\\');
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

        protected virtual void ActionButtonAction() => PerformAction();

        private int GetItemCountPerPage() => (int)(scrollContainer.DrawHeight / (DrawableItem.DefaultHeight + ItemSpacing) + 0.5);

        private void HandleSelection(DrawableItem selectedItem)
        {
            if (CurrentSelection != null)
                CurrentSelection.Selected = false;
            CurrentSelection = selectedItem;
            UpdateSelectedPath(GetCurrentSelectedPath());
        }
        private void HandleClick(DrawableItem clickedItem)
        {
            if (!clickedItem.Selected)
                CurrentSelection = null;
        }
        private void HandleDoubleClick(DrawableItem doubleClickedItem) => PerformAction();

        private void PerformAction()
        {
            if (CurrentSelection.ItemType == ItemType.Directory)
                NavigateToSelectedDirectory();
            else
                FinalizeSelection();
        }

        private void NavigateToSelectedDirectory() => CurrentDirectory = GetCurrentSelectedPath();
        private void FinalizeSelection()
        {
            OnFileSelected?.Invoke(FileName);
            ToggleVisibility();
        }

        private string GetCurrentBreadcrumbsDirectory() => filePathBreadcrumbs.Items.Aggregate(AggregateBreadcrumbs);
        private string GetCurrentSelectedPath() => $@"{GetCurrentBreadcrumbsDirectory()}\{CurrentSelection.ItemName}";

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
