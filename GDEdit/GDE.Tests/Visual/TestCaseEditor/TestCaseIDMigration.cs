using System;
using System.Collections.Generic;
using GDE.App.Main.Screens.Edit;
using GDE.App.Main.Screens.Edit.Components;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Testing;
using osuTK;

namespace GDE.Tests.Visual.TestCaseEditor
{
    public class TestCaseIDMigration : TestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new Type[] { typeof(IDMigrationPanel), typeof(IDMigrationStepCard), typeof(IDMigrationStepList) };

        public TestCaseIDMigration()
        {
            var panel = new IDMigrationPanel(new Editor(new Level()));

            Add(panel);

            panel.ToggleVisibility();

            var stepList = panel.StepList;

            AddLabel("Step creation");
            AddAssert("Check whether common step is null", () => panel.CommonIDMigrationStep.Value == null); // Also a dummy to not fuck the tests
            AddStep("Create a new step", () => stepList.AddStep(new SourceTargetRange(1, 5, 15)));
            AddStep("Select step at index 0", () => stepList.SelectStep(0));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(1, 5, 15, 19));
            AddStep("Create a new step", () => stepList.AddStep(new SourceTargetRange(1, 5, 15)));
            AddStep("Select step at index 1", () => stepList.SelectStep(1));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(1, 5, 15, 19));
            AddStep("Create a new step", () => stepList.AddStep(new SourceTargetRange(20, 30, 40)));
            AddStep("Select step at index 2", () => stepList.SelectStep(2));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(20, 30, 40, 50));

            AddLabel("Step selection");
            AddStep("Deselect step at index 2", () => stepList.DeselectStep(2));
            AddStep("Append step at index 0 to selection", () => stepList.SelectStep(0, true));
            AddStep("Append step at index 1 to selection", () => stepList.SelectStep(1, true));
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { 0, 1 }));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(1, 5, 15, 19));
            AddStep("Append step at index 2 to selection", () => stepList.SelectStep(2, true));
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { 0, 1, 2 }));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(-1, -1, -1, -1));
            AddStep("Deselect all", () => stepList.DeselectAll());
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { }));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value == null);
            AddStep("Select all", () => stepList.SelectAll());
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { 0, 1, 2 }));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(-1, -1, -1, -1));

            AddLabel("Step removal");
            AddStep("Remove currently selected steps", () => stepList.RemoveSelectedSteps());
            AddAssert("Check whether common step is null", () => panel.CommonIDMigrationStep.Value == null);

            AddLabel("Step cloning");
            AddRepeatStep("Create 4 new identical steps", () => stepList.AddStep(new SourceTargetRange(1, 5, 15)), 4);
            AddStep("Select newly created steps", () =>
            {
                for (int i = 0; i < 4; i++)
                    stepList.SelectStep(i, true);
            });
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(1, 5, 15, 19));
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { 0, 1, 2, 3 }));
            AddStep("Clone currently selected steps", () => stepList.CloneSelectedSteps());
            AddAssert("Match current selection", () => stepList.SelectedStepIndices.SetEquals(new int[] { 4, 5, 6, 7 }));
            AddAssert("Check common selected values", () => panel.CommonIDMigrationStep.Value.HasValues(1, 5, 15, 19));
        }
    }
}
