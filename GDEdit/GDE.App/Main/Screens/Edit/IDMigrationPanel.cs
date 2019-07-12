using GDE.App.Main.Containers.KeyBindingContainers;
using GDE.App.Main.Panels;
using GDE.App.Main.Screens.Edit.Components;
using GDE.App.Main.UI;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.General;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using System.Collections.Generic;
using static GDE.App.Main.Colors.GDEColors;
using static GDEdit.Utilities.Objects.General.SourceTargetRange;

namespace GDE.App.Main.Screens.Edit
{
    public class IDMigrationPanel : Panel
    {
        private NumberTextBox sourceFrom;
        private NumberTextBox sourceTo;
        private NumberTextBox targetFrom;
        private NumberTextBox targetTo;

        private FadeButton performAction;
        private FadeButton createStep;
        private FadeButton removeSteps;
        private FadeButton cloneSteps;
        private FadeButton selectAll;
        private FadeButton deselectAll;
        private FadeButton loadSteps;
        private FadeButton saveSteps;

        private IDMigrationStepList groupStepList;
        private IDMigrationStepList colorStepList;
        private IDMigrationStepList itemStepList;
        private IDMigrationStepList blockStepList;

        private Editor editor;

        /// <summary>The common <seealso cref="SourceTargetRange"/> of the currently selected ID migration steps.</summary>
        public readonly Bindable<SourceTargetRange> CommonIDMigrationStep = new Bindable<SourceTargetRange>();

        public IDMigrationStepList StepList;

        public IDMigrationPanel(Editor e)
        {
            editor = e;

            //RelativeSizeAxes = Axes.Both;
            Size = new Vector2(700, 650);

            groupStepList = GetNewStepList(editor, IDMigrationMode.Groups);
            colorStepList = GetNewStepList(editor, IDMigrationMode.Colors);
            itemStepList = GetNewStepList(editor, IDMigrationMode.Items);
            blockStepList = GetNewStepList(editor, IDMigrationMode.Blocks);

            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 10,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = FromHex("1a1a1a")
                        },
                        new IDMigrationActionContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Y,
                                    Width = 520,
                                    Margin = new MarginPadding
                                    {
                                        //Top = 10,
                                    },
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = FromHex("111111"),
                                        },
                                        StepList = groupStepList
                                    }
                                },
                                new FillFlowContainer
                                {
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                    Spacing = new Vector2(5),
                                    Margin = new MarginPadding { Top = 15, Right = 15 },
                                    RelativeSizeAxes = Axes.Y,
                                    Width = 150,
                                    Children = new Drawable[]
                                    {
                                        GetNewSpriteText("Source From"),
                                        sourceFrom = GetNewNumberTextBox(),
                                        GetNewSpriteText("Source To"),
                                        sourceTo = GetNewNumberTextBox(),
                                        GetNewSpriteText("Target From"),
                                        targetFrom = GetNewNumberTextBox(),
                                        GetNewSpriteText("Target To"),
                                        targetTo = GetNewNumberTextBox(),
                                    },
                                },
                                new FillFlowContainer
                                {
                                    Anchor = Anchor.BottomRight,
                                    Origin = Anchor.BottomRight,
                                    Direction = FillDirection.Vertical,
                                    Spacing = new Vector2(10),
                                    Margin = new MarginPadding { Bottom = 15, Right = 15 },
                                    RelativeSizeAxes = Axes.Y,
                                    Width = 150,
                                    Children = new Drawable[]
                                    {
                                        performAction = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Margin = new MarginPadding { Top = 25 },
                                            Text = "Perform Action",
                                            EnabledColor = FromHex("242424"),
                                            Action = editor.PerformMigration,
                                        },
                                        removeSteps = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Remove Steps",
                                            EnabledColor = FromHex("242424"),
                                            Action = StepList.RemoveSelectedSteps,
                                        },
                                        cloneSteps = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Clone Steps",
                                            EnabledColor = FromHex("242424"),
                                            Action = StepList.CloneSelectedSteps,
                                        },
                                        deselectAll = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Deselect All",
                                            EnabledColor = FromHex("242424"),
                                            Action = StepList.DeselectAll,
                                        },
                                        selectAll = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Select All",
                                            EnabledColor = FromHex("242424"),
                                            Action = StepList.SelectAll,
                                        },
                                        loadSteps = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Load Steps",
                                            EnabledColor = FromHex("242424"),
                                            //Action = null, // Make this work
                                        },
                                        saveSteps = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Save Steps",
                                            EnabledColor = FromHex("242424"),
                                            //Action = null, // Make this work
                                        },
                                        createStep = new FadeButton
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.X,
                                            Height = 32,
                                            Text = "Create Step",
                                            EnabledColor = FromHex("242424"),
                                            Action = StepList.CreateNewStep,
                                        },
                                    },
                                },
                            }
                        }
                    }
                }
            });

            performAction.Enabled.Value = false;

            sourceFrom.NumberChanged += HandleSourceFromChanged;
            sourceTo.NumberChanged += HandleSourceToChanged;
            targetFrom.NumberChanged += HandleTargetFromChanged;
            targetTo.NumberChanged += HandleTargetToChanged;

            CommonIDMigrationStep.ValueChanged += v =>
            {
                // TODO: Maybe revamp the code below since it look terrible
                var newStep = v.NewValue;
                if (newStep != null)
                {
                    newStep.SourceTargetRangeChanged += (sf, st, tf, tt) =>
                    {
                        UpdateTextBoxes(newStep);
                        foreach (var s in StepList.SelectedSteps)
                        {
                            s.SourceFrom = sf;
                            s.SourceTo = st;
                            s.TargetFrom = tf;
                        }
                    };
                    foreach (var s in StepList.SelectedSteps)
                    {
                        s.SourceFrom = newStep.SourceFrom;
                        s.SourceTo = newStep.SourceTo;
                        s.TargetFrom = newStep.TargetFrom;
                    }
                }
                UpdateTextBoxes(newStep);
            };

            StepList.StepSelected = HandleStepSelected;
            StepList.StepDeselected = HandleStepDeselected;
            StepList.SelectionChanged = HandleSelectionChanged;
        }

        private void HandleSourceFromChanged(int newValue)
        {
            if (newValue > 0)
                CommonIDMigrationStep.Value.SourceFrom = newValue;
        }
        private void HandleSourceToChanged(int newValue)
        {
            if (newValue > 0 && newValue >= CommonIDMigrationStep.Value.SourceFrom)
                CommonIDMigrationStep.Value.SourceTo = newValue;
        }
        private void HandleTargetFromChanged(int newValue)
        {
            if (newValue > 0)
                CommonIDMigrationStep.Value.TargetFrom = newValue;
        }
        private void HandleTargetToChanged(int newValue)
        {
            if (newValue > 0 && newValue >= CommonIDMigrationStep.Value.TargetFrom)
                CommonIDMigrationStep.Value.TargetTo = newValue;
        }

        private void HandleStepSelected(IDMigrationStepCard c)
        {
            var commonSteps = new List<SourceTargetRange> { c.StepRange };
            if (CommonIDMigrationStep.Value != null)
                commonSteps.Add(CommonIDMigrationStep.Value);
            CommonIDMigrationStep.Value = GetCommon(commonSteps); // Additive logic works
            UpdateFadeButtonEnabledStates();
        }
        private void HandleStepDeselected(IDMigrationStepCard c) => HandleSelectionChanged();
        private void HandleSelectionChanged()
        {
            CommonIDMigrationStep.Value = GetCommon(StepList.SelectedSteps);
            UpdateFadeButtonEnabledStates();
        }

        private void UpdateFadeButtonEnabledStates()
        {
            removeSteps.Enabled.Value = StepList.SelectedSteps.Count > 0;
            cloneSteps.Enabled.Value = StepList.SelectedSteps.Count > 0;
            performAction.Enabled.Value = editor.CurrentlySelectedIDMigrationSteps.Count > 0;
        }

        private void UpdateTextBoxes(SourceTargetRange range)
        {
            UpdateTextBox(sourceFrom, range?.SourceFrom);
            UpdateTextBox(sourceTo, range?.SourceTo);
            UpdateTextBox(targetFrom, range?.TargetFrom);
            UpdateTextBox(targetTo, range?.TargetTo);
        }
        private void UpdateTextBox(NumberTextBox textBox, int? newValue)
        {
            bool enabled = newValue.HasValue && newValue > -1;
            textBox.InvokeEvents = false;
            if (enabled)
                textBox.Number = newValue.Value;
            else
                textBox.Text = "";
            textBox.InvokeEvents = true;
            textBox.Enabled = enabled;
        }

        private static NumberTextBox GetNewNumberTextBox() => new NumberTextBox(false)
        {
            RelativeSizeAxes = Axes.X,
            Height = 30,
        };
        private static SpriteText GetNewSpriteText(string text) => new SpriteText
        {
            Text = text,
        };
        private static IDMigrationStepList GetNewStepList(Editor editor, IDMigrationMode mode) => new IDMigrationStepList(editor, mode)
        {
            RelativeSizeAxes = Axes.Y,
            Width = 500,
            Padding = new MarginPadding
            {
                Top = 10,
                Bottom = 10,
                //Left = 10,
                //Right = 10
            }
        };
    }
}