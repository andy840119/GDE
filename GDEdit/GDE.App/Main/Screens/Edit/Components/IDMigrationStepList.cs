using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Char;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class IDMigrationStepList : FillFlowContainer
    {
        public const float CardMargin = 2;
        public const float CardHeight = 25;

        private bool loaded;

        private FadeSearchContainer stepList;
        private TextBox searchQuery;
        private Container stepContainer;
        private FillFlowContainer noSteps;
        private Button addNewStep;

        private Editor editor;

        public SortedSet<int> SelectedStepIndices { get; private set; } = new SortedSet<int>();
        public List<SourceTargetRange> SelectedSteps { get; private set; } = new List<SourceTargetRange>();

        /// <summary>The action to invoke when a step has been selected.</summary>
        public Action<IDMigrationStepCard> StepSelected;
        /// <summary>The action to invoke when a step has been deselected.</summary>
        public Action<IDMigrationStepCard> StepDeselected;
        /// <summary>The action to invoke when the selection has been changed more complexly.</summary>
        public Action SelectionChanged;

        public List<IDMigrationStepCard> Cards = new List<IDMigrationStepCard>();

        public IDMigrationStepList(Editor e)
        {
            editor = e;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Children = new Drawable[]
            {
                searchQuery = new TextBox
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Height = 30,
                    RelativeSizeAxes = Axes.X,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Top = 5,
                    },
                    Children = new Drawable[]
                    {
                        new ScrollContainer
                        {
                            // Yeah, something needs to be done about the scrolling here
                            RelativeSizeAxes = Axes.Both,
                            Child = stepList = new SearchContainer
                            {
                                LayoutDuration = 100,
                                LayoutEasing = Easing.Out,
                                Spacing = new Vector2(0, 2),
                                RelativeSizeAxes = Axes.X,
                            },
                        },
                        noSteps = new FillFlowContainer
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
                                    Icon = FontAwesome.Solid.Times,
                                    Size = new Vector2(64),
                                    Colour = GDEColors.FromHex("666666")
                                },
                                new SpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Text = "No migration steps are registered",
                                    Font = @"OpenSans",
                                    TextSize = 24,
                                    Colour = GDEColors.FromHex("666666")
                                },
                                addNewStep = new Button
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Size = new Vector2(220, 32),
                                    Text = "Add a new step",
                                    BackgroundColour = GDEColors.FromHex("242424")
                                }
                            },
                            Alpha = 0,
                        }
                    }
                },
            };

            UpdateCurrentTabRanges();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            loaded = true;
        }

        public void UpdateCurrentTabRanges()
        {
            var currentTabRanges = editor.CurrentlySelectedIDMigrationSteps;

            UpdateNoStepDialogVisibility(currentTabRanges);

            if (currentTabRanges.Count > 0)
            {
                for (var i = 0; i < currentTabRanges.Count; i++)
                {
                    var card = CreateIDMigrationStepCard(currentTabRanges[i], i);

                    stepList.Add(card);
                    Cards.Add(card);
                }

                if (loaded)
                    foreach (var c in Cards)
                    {
                        c.InitializeArrowAnimation();
                        Task.Delay(50); // Perhaps 50ms is fine, must test
                    }
            }

            searchQuery.Current.ValueChanged += obj =>
            {
                stepList.SearchTerm = obj.NewValue;
            };

            addNewStep.Action = CreateNewStep;
        }

        private void UpdateNoStepDialogVisibility(List<SourceTargetRange> currentTabRanges) => noSteps.FadeTo(currentTabRanges.Count == 0 ? 1 : 0, 100, Easing.InOutQuint);

        public void AddStep(SourceTargetRange range)
        {
            var currentTabRanges = editor.CurrentlySelectedIDMigrationSteps;
            var newCard = CreateIDMigrationStepCard(range, currentTabRanges.Count);
            Cards.Add(newCard);
            stepList.Add(newCard);
            editor.AddIDMigrationStep(range);
            UpdateNoStepDialogVisibility(currentTabRanges);
        }
        public void CreateNewStep() => AddStep(new SourceTargetRange(1, 10, 11));
        public void CloneSelectedSteps()
        {
            var oldSelection = SelectedStepIndices;
            InitializeSelectedSteps();
            foreach (var i in oldSelection)
            {
                var newIndex = Cards.Count; // Hacky way in foreach loop to avoid using extra variable
                var card = Cards[i];
                card.Deselect();
                AddStep(card.StepRange.Clone());
                AddSelectedStep(newIndex);
                Cards[newIndex].Select();
            }
            SelectionChanged?.Invoke();
        }
        public void RemoveSelectedSteps()
        {
            var currentTabRanges = editor.CurrentlySelectedIDMigrationSteps;
            int[] indices = new int[SelectedStepIndices.Count];
            SelectedStepIndices.CopyTo(indices);
            for (int i = indices.Length - 1; i >= 0; i--)
            {
                editor.RemoveIDMigrationStep(Cards[indices[i]].StepRange);
                stepList.Remove(Cards[indices[i]]);
                Cards.RemoveAt(indices[i]);
            }
            ClearSelectedSteps();
            SelectionChanged?.Invoke();

            // Fix indices
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Index = i;

            UpdateNoStepDialogVisibility(currentTabRanges);
        }
        public void SelectAll()
        {
            SelectedSteps.Clear();
            foreach (var c in Cards)
            {
                c.Select();
                SelectedSteps.Add(c.StepRange);
            }
            SelectedStepIndices = new SortedSet<int>(Enumerable.Range(0, Cards.Count));
            SelectionChanged?.Invoke();
        }
        public void DeselectAll()
        {
            ClearSelectedSteps();
            foreach (var c in Cards)
                c.Deselect();
            SelectionChanged?.Invoke();
        }

        public void DeselectStep(int index)
        {
            Cards[index].Deselect();
            RemoveSelectedStep(index);
            StepDeselected?.Invoke(Cards[index]);
        }
        public void SelectStep(int index, bool appendToSelection = false)
        {
            if (!appendToSelection)
            {
                var oldSelection = SelectedStepIndices;
                InitializeSelectedSteps();
                foreach (var i in oldSelection)
                    Cards[i].Deselect();
            }
            Cards[index].Select();
            AddSelectedStep(index);
            if (appendToSelection)
                StepSelected?.Invoke(Cards[index]);
            else
                SelectionChanged?.Invoke();
        }

        private IDMigrationStepCard CreateIDMigrationStepCard(SourceTargetRange r, int index)
        {
            return new IDMigrationStepCard(r)
            {
                Margin = new MarginPadding { Top = CardMargin },
                Index = index,
                CardDragged = (c, e) => RearrangeCards(index, e),
                CardClicked = HandleCardClicked,
            };
        }

        // Helper functions to avoid unwanted bugs
        private void AddSelectedStep(int index)
        {
            SelectedStepIndices.Add(index);
            SelectedSteps.Add(Cards[index].StepRange);
        }
        private void RemoveSelectedStep(int index)
        {
            SelectedStepIndices.Remove(index);
            SelectedSteps.Remove(Cards[index].StepRange);
        }
        private void ClearSelectedSteps()
        {
            SelectedStepIndices.Clear();
            SelectedSteps.Clear();
        }
        private void InitializeSelectedSteps()
        {
            SelectedStepIndices = new SortedSet<int>();
            SelectedSteps = new List<SourceTargetRange>();
        }

        private void HandleCardClicked(IDMigrationStepCard c, MouseEvent e)
        {
            if (c.Selected.Value)
                DeselectStep(c.Index);
            else
                SelectStep(c.Index, e.ShiftPressed);
        }
        private void RearrangeCards(int selectedCardIndex, DragEvent e)
        {
            int newIndex = GetCardIndexFromYPosition(Cards[selectedCardIndex].Y);
            Cards.Swap(selectedCardIndex, newIndex);
            stepList.SetLayoutPosition(Cards[selectedCardIndex], newIndex);
        }

        private static float GetCardYPositionThreshold(int index) => index * (CardHeight + CardMargin) + CardHeight / 2;
        private static int GetCardIndexFromYPosition(float y) => (int)((y + CardHeight / 2) / (CardHeight + CardMargin));
    }
}
