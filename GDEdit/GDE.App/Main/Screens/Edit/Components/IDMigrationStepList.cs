﻿using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Containers.KeyBindingContainers;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static GDEdit.Utilities.Objects.General.SourceTargetRange;
using static System.Char;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class IDMigrationStepList : FillFlowContainer, IKeyBindingHandler<IDMigrationAction>
    {
        public const float CardMargin = 2;
        public const float CardHeight = 25;

        private bool loaded;

        private FadeSearchContainer stepList;
        private TextBox searchQuery;
        private Container stepContainer;
        private FillFlowContainer noSteps;
        private Button addNewStep;

        private IDMigrationStepCard lastClickedBeforeShift;

        private List<SourceTargetRange> clipboard;

        private Editor editor;

        public readonly IDMigrationMode IDMigrationMode;

        public List<SourceTargetRange> TabRanges => editor.GetIDMigrationSteps(IDMigrationMode);

        public SortedSet<int> SelectedStepIndices { get; private set; } = new SortedSet<int>();
        public List<SourceTargetRange> SelectedSteps { get; private set; } = new List<SourceTargetRange>();

        /// <summary>The common <seealso cref="SourceTargetRange"/> of the currently selected ID migration steps.</summary>
        public readonly Bindable<SourceTargetRange> CommonIDMigrationStep = new Bindable<SourceTargetRange>();

        /// <summary>The action to invoke when a step has been selected.</summary>
        public Action<IDMigrationStepCard> StepSelected;
        /// <summary>The action to invoke when a step has been deselected.</summary>
        public Action<IDMigrationStepCard> StepDeselected;
        /// <summary>The action to invoke when the selection has been changed more complexly.</summary>
        public Action SelectionChanged;

        public List<IDMigrationStepCard> Cards = new List<IDMigrationStepCard>();

        public IDMigrationStepList(Editor e, IDMigrationMode mode)
        {
            editor = e;
            IDMigrationMode = mode;

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
                        Top = 10,
                    },
                    Padding = new MarginPadding
                    {
                        Bottom = 40,
                    },
                    Children = new Drawable[]
                    {
                        new ScrollContainer
                        {
                            // Yeah, something needs to be done about the scrolling here
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding
                            {
                                //Bottom = 10, // I have to manually set it to that until a solution is figured out because the container otherwise extends too far down
                            },
                            Child = stepList = new FadeSearchContainer
                            {
                                LayoutDuration = 100,
                                LayoutEasing = Easing.Out,
                                Spacing = new Vector2(0, 2),
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
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
            UpdateNoStepDialogVisibility(TabRanges);

            if (TabRanges.Count > 0)
            {
                for (var i = 0; i < TabRanges.Count; i++)
                {
                    var card = CreateIDMigrationStepCard(TabRanges[i], i);

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
            var newCard = CreateIDMigrationStepCard(range, TabRanges.Count);
            Cards.Add(newCard);
            stepList.Add(newCard);
            editor.AddIDMigrationStep(range);
            UpdateNoStepDialogVisibility(TabRanges);
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
            OnSelectionChanged();
        }
        public void RemoveSelectedSteps()
        {
            int[] indices = new int[SelectedStepIndices.Count];
            SelectedStepIndices.CopyTo(indices);
            for (int i = indices.Length - 1; i >= 0; i--)
            {
                editor.RemoveIDMigrationStep(Cards[indices[i]].StepRange);
                stepList.Remove(Cards[indices[i]]);
                Cards.RemoveAt(indices[i]);
            }
            ClearSelectedSteps();
            OnSelectionChanged();

            // Fix indices
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Index = i;

            UpdateNoStepDialogVisibility(TabRanges);
        }
        public void RemoveAllSteps()
        {
            SelectAll();
            RemoveSelectedSteps();
        }
        public void CutSelectedSteps()
        {
            CopySelectedSteps();
            RemoveSelectedSteps();
        }
        public void CopySelectedSteps()
        {
            int[] indices = new int[SelectedStepIndices.Count];
            SelectedStepIndices.CopyTo(indices);
            clipboard = new List<SourceTargetRange>(indices.Length);
            foreach (var i in indices)
                clipboard.Add(Cards[i].StepRange);
        }
        public void PasteSteps()
        {
            InitializeSelectedSteps();
            foreach (var s in clipboard)
            {
                var newIndex = Cards.Count; // Hacky way in foreach loop to avoid using extra variable
                AddStep(s);
                AddSelectedStep(newIndex);
                Cards[newIndex].Select();
            }
            OnSelectionChanged();
        }
        public void LoadSteps()
        {
            // TODO: Open Shell shit
            string fileName = ""; // Change that
            var lines = File.ReadAllLines(fileName);
            var ranges = SourceTargetRange.LoadRangesFromStringArray(lines);
            RemoveAllSteps();
            foreach (var r in ranges)
            {
                var newIndex = Cards.Count; // Hacky way in foreach loop to avoid using extra variable
                AddStep(r);
                AddSelectedStep(newIndex);
                Cards[newIndex].Select();
            }
        }
        public void SaveSteps()
        {
            // TODO: Open Shell shit
            string fileName = ""; // Change that
            File.WriteAllLines(fileName, SourceTargetRange.ConvertRangesToStringArray(TabRanges));
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
            OnSelectionChanged();
        }
        public void DeselectAll()
        {
            ClearSelectedSteps();
            foreach (var c in Cards)
                c.Deselect();
            OnSelectionChanged();
        }

        public void DeselectStep(int index)
        {
            Cards[index].Deselect();
            RemoveSelectedStep(index);
            OnStepDeselected(Cards[index]);
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
                OnStepSelected(Cards[index]);
            else
                OnSelectionChanged();
        }
        public void ToggleStepSelection(int index, bool appendToSelection = false)
        {
            if (Cards[index].Selected.Value)
                DeselectStep(index);
            else
                SelectStep(index, appendToSelection);
        }

        private void OnStepSelected(IDMigrationStepCard card)
        {
            var commonSteps = new List<SourceTargetRange> { card.StepRange };
            if (CommonIDMigrationStep.Value != null)
                commonSteps.Add(CommonIDMigrationStep.Value);
            CommonIDMigrationStep.Value = GetCommon(commonSteps); // Additive logic works
            StepSelected?.Invoke(card);
        }
        private void OnStepDeselected(IDMigrationStepCard card)
        {
            CommonIDMigrationStep.Value = GetCommon(SelectedSteps);
            StepDeselected?.Invoke(card);
        }
        private void OnSelectionChanged()
        {
            CommonIDMigrationStep.Value = GetCommon(SelectedSteps);
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
            if (!e.ShiftPressed)
                lastClickedBeforeShift = c;

            // For a card to be clicked, there must be at least one card, therefore this should never throw an exception
            if (lastClickedBeforeShift == null)
                lastClickedBeforeShift = Cards[0];

            if (e.ShiftPressed)
            {
                DeselectAll();

                int start = lastClickedBeforeShift.Index;
                int end = c.Index;

                // Swap the indices
                if (start > end)
                {
                    int t = start;
                    start = end;
                    end = t;
                }

                for (int i = start; i <= end; i++)
                    SelectStep(i, true);
            }
            else
            {
                if (e.ControlPressed)
                    ToggleStepSelection(c.Index, true);
                else
                {
                    var otherSelectedSteps = SelectedStepIndices.Clone();
                    otherSelectedSteps.Remove(c.Index);
                    if (otherSelectedSteps.Count == 0)
                        ToggleStepSelection(c.Index);
                    else
                        SelectStep(c.Index);
                }
            }
        }
        private void RearrangeCards(int selectedCardIndex, DragEvent e)
        {
            int newIndex = GetCardIndexFromYPosition(Cards[selectedCardIndex].Y);
            Cards.Swap(selectedCardIndex, newIndex);
            stepList.SetLayoutPosition(Cards[selectedCardIndex], newIndex);
        }

        private static float GetCardYPositionThreshold(int index) => index * (CardHeight + CardMargin) + CardHeight / 2;
        private static int GetCardIndexFromYPosition(float y) => (int)((y + CardHeight / 2) / (CardHeight + CardMargin));

        public bool OnPressed(IDMigrationAction action)
        {
            switch (action)
            {
                case IDMigrationAction.SelectAll:
                    SelectAll();
                    return true;
                case IDMigrationAction.DeselectAll:
                    DeselectAll();
                    return true;
                case IDMigrationAction.Cut:
                    CutSelectedSteps();
                    return true;
                case IDMigrationAction.Copy:
                    CopySelectedSteps();
                    return true;
                case IDMigrationAction.Paste:
                    PasteSteps();
                    return true;
                case IDMigrationAction.Clone:
                    CloneSelectedSteps();
                    return true;
                case IDMigrationAction.Remove:
                    RemoveSelectedSteps();
                    return true;
                case IDMigrationAction.Load:
                    LoadSteps();
                    return true;
                case IDMigrationAction.Save:
                    SaveSteps();
                    return true;
            }

            return false;
        }

        public bool OnReleased(IDMigrationAction action) => true;
    }
}
