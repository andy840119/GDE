using DiscordRPC;
using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Edit.Components;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.General;
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
using static GDEdit.Utilities.Objects.General.SourceTargetRange;
using static GDE.App.Main.Colors.GDEColors;

namespace GDE.App.Main.Screens.Edit
{
    // TODO: Consider shrinking this to a popup instead of an entire screen
    public class IDMigrationScreen : Screen
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

        private Editor editor;

        /// <summary>The common <seealso cref="SourceTargetRange"/> of the currently selected ID migration steps.</summary>
        public readonly Bindable<SourceTargetRange> CommonIDMigrationStep = new Bindable<SourceTargetRange>();

        public IDMigrationStepList StepList;

        public IDMigrationScreen(Editor e)
        {
            editor = e;

            //Size = new Vector2(0.4f);

            AddRangeInternal(new Drawable[]
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
                        StepList = new IDMigrationStepList(editor)
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 500,
                            Padding = new MarginPadding
                            {
                                Top = 10,
                                //Left = 10,
                                //Right = 10
                            }
                        },
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
            });

            performAction.Enabled.Value = false;

            sourceFrom.NumberChanged += n => CommonIDMigrationStep.Value.SourceFrom = n;
            sourceTo.NumberChanged += n => CommonIDMigrationStep.Value.SourceTo = n;
            targetFrom.NumberChanged += n => CommonIDMigrationStep.Value.TargetFrom = n;
            targetTo.NumberChanged += n => CommonIDMigrationStep.Value.TargetTo = n;

            CommonIDMigrationStep.ValueChanged += v =>
            {
                var newStep = v.NewValue;
                if (newStep != null)
                    newStep.SourceTargetRangeChanged += (sf, st, tf, tt) => UpdateTextBoxes(newStep);
                UpdateTextBoxes(newStep);
            };

            StepList.StepSelected = HandleStepSelected;
            StepList.StepDeselected = HandleStepDeselected;
            StepList.SelectionChanged = HandleSelectionChanged;
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
            textBox.InvokeEvents = textBox.Enabled = enabled;
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
    }
}