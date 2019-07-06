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
using System.Threading.Tasks;
using static System.Char;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class IDMigrationStepList : FillFlowContainer
    {
        private const float CardMargin = 2;
        private const float CardHeight = 25;

        private SearchContainer stepList;
        private TextBox searchQuery;
        private Container stepContainer;
        private FillFlowContainer noSteps;
        private Button addNewStep;

        private Editor editor;

        public int StepIndex;
        /// <summary>The action to invoke when a step has been selected.</summary>
        public Action<IDMigrationStepCard> StepSelected;
        /// <summary>The action to invoke when a step has been deselected.</summary>
        public Action<IDMigrationStepCard> StepDeselected;
        public List<IDMigrationStepCard> Cards = new List<IDMigrationStepCard>();

        public IDMigrationStepList(Editor e)
        {
            editor = e;

            Children = new Drawable[]
            {
                searchQuery = new TextBox
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Height = 30,
                    RelativeSizeAxes = Axes.X,
                    Margin = new MarginPadding
                    {
                        //Top = 10,
                        Left = 10,
                        Right = 10,
                    },
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
                        stepList = new SearchContainer
                        {
                            LayoutDuration = 100,
                            LayoutEasing = Easing.Out,
                            Spacing = new Vector2(1, 2),
                            RelativeSizeAxes = Axes.X,
                            //AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding(5)
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

        public void UpdateCurrentTabRanges()
        {
            var currentTabRanges = editor.CurrentlySelectedIDMigrationSteps;

            if (currentTabRanges.Count == 0)
                noSteps.Alpha = 1;
            else
            {
                for (var i = 0; i < currentTabRanges.Count; i++)
                {
                    Cards.Add(new IDMigrationStepCard(currentTabRanges[i])
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = CardHeight,
                        Margin = new MarginPadding(CardMargin),
                        Index = i,
                        CardDragged = c => RearrangeCards(i, c)
                    });
                }

                foreach (var c in Cards)
                {
                    stepList.Add(Cards[c.Index]);

                    Cards[c.Index].Selected.ValueChanged += obj =>
                    {
                        if (obj.NewValue)
                            foreach (var j in Cards)
                                if (j != Cards[c.Index] && j.Selected.Value)
                                    j.Selected.Value = false;
                        StepIndex = obj.NewValue ? c.Index : -1;
                        StepSelected?.Invoke(StepIndex > -1 ? Cards[StepIndex] : null);
                    };
                }

                foreach (var c in Cards)
                {
                    c.InitializeArrowAnimation();
                    Task.Delay(10); // Perhaps 10ms is fine, must test
                }
            }

            searchQuery.Current.ValueChanged += obj =>
            {
                stepList.SearchTerm = obj.NewValue;
            };

            addNewStep.Action = CreateNewStep;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        public void AddStep(SourceTargetRange range)
        {
            stepList.Add(CreateIDMigrationStepCard(range, editor.CurrentlySelectedIDMigrationSteps.Count));
            editor.AddIDMigrationStep(range);
        }
        public void CreateNewStep() => AddStep(new SourceTargetRange(1, 10, 11));
        public void CloneSelectedSteps()
        {
            // TODO: Do thing
        }
        public void RemoveSelectedSteps()
        {
            // TODO: Do thing
        }

        private IDMigrationStepCard CreateIDMigrationStepCard(SourceTargetRange r, int index)
        {
            var card = new IDMigrationStepCard(r)
            {
                RelativeSizeAxes = Axes.X,
                Height = CardHeight,
                Margin = new MarginPadding(CardMargin),
                Index = index,
                CardDragged = c => RearrangeCards(index, c)
            };

            card.Selected.ValueChanged += obj =>
            {
                if (obj.NewValue)
                    foreach (var j in Cards)
                        if (j != card && j.Selected.Value)
                            j.Selected.Value = false;
                StepIndex = obj.NewValue ? card.Index : -1;
                StepSelected?.Invoke(StepIndex > -1 ? Cards[StepIndex] : null);
            };

            return card;
        }

        private void RearrangeCards(int selectedCardIndex, DragEvent e)
        {
            int newIndex = GetCardIndexFromYPosition(Cards[selectedCardIndex].Y);
            Cards.Swap(selectedCardIndex, newIndex);
            stepList.SetLayoutPosition(Children[selectedCardIndex], newIndex);
        }

        private static float GetCardYPositionThreshold(int index) => index * (CardHeight + CardMargin) + CardHeight / 2;
        private static int GetCardIndexFromYPosition(float y) => (int)((y + CardHeight / 2) / (CardHeight + CardMargin));
    }
}
